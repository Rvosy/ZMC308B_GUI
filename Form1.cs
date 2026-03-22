using System;
using System.Drawing;
using System.Windows.Forms;
using ZMC.Lib;

namespace ZMC
{
    public partial class Form1 : Form
    {
        ZmcDevice zmcControl = new ZmcDevice();
        private bool isAbsoluteMode = true; // 默认绝对运动模式

        // IO 调试 Tab 控件
        private Label[]  _inLeds   = new Label[40];
        private Button[] _outBtns  = new Button[16];
        private uint[]   _outState = new uint[16];

        // 限位控制
        private const float SoftLimitMax   = 400f;
        private const int   HomeLimitInput = 0;    // 零点限位 IN0
        private bool  isHoming             = false;
        private bool  _homingWaitIdle      = false; // 等待轴停止后再归零坐标
        private bool  _prevLimitHit        = false;
        private Label  lblHomeLimitStatus;
        private Button btnHome;

        public Form1()
        {
            InitializeComponent();
        }
        // 连接
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (zmcControl.IsConnected)
            {
                MessageBox.Show("已连接，请先断开");
                return;
            }

            string ip = txtIp.Text.Trim();
            int ret = zmcControl.OpenEth(ip);
            if (ret != 0)
            {
                lblStatus.Text = "连接失败";
                MessageBox.Show("连接失败，错误码：" + ret);
            }
            else
            {
                lblStatus.Text = "已连接 ";
                timer1.Start();
                UpdateButtonStates(true);
                toolStripStatusLabel1.Text = "就绪";
            }
        }
        // 断开
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                MessageBox.Show("未连接");
                return;
            }

            int ret = zmcControl.Close(); // 始终将 Handle 置零，无论硬件是否响应
            SetDisconnectedUI();
            if (ret != 0)
                MessageBox.Show($"设备通信断开失败（错误码：{ret}），已强制重置连接状态。", "断开连接",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBox.Show("已断开连接", "断开连接", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        // 运动模式切换（绝对/相对）
        private void rdoRelative_CheckedChanged(object sender, EventArgs e) => UpdateMotionMode();
        private void rdoAbsolute_CheckedChanged(object sender, EventArgs e) => UpdateMotionMode();

        private void UpdateMotionMode()
        {
            isAbsoluteMode = rdoAbsolute.Checked;
            lblendpos.Text = isAbsoluteMode ? "终点位置" : "移动增量";
            txtEndZ1.Text = txtEndZ2.Text = txtEndZ3.Text = txtEndX.Text = "0";
        }
        // 公共辅助：解析 UI 参数并应用到所有轴
        private void ApplyMotionParams()
        {
            float units = float.Parse(txtUnits.Text);
            float speed = float.Parse(txtSpeed.Text);
            float accel = float.Parse(txtAccel.Text);
            float decel = float.Parse(txtDecel.Text);
            for (int axis = 0; axis <= 3; axis++)
            {
                zmcControl.SetUnits(axis, units);
                zmcControl.SetSpeed(axis, speed);
                zmcControl.SetAccel(axis, accel);
                zmcControl.SetDecel(axis, decel);
            }
        }

        // 公共辅助：按当前模式执行单轴运动
        private void MoveAxis(int axis, float target)
        {
            if (isAbsoluteMode)
                zmcControl.SingleMoveAbs(axis, target);
            else
                zmcControl.SingleMove(axis, target);
        }

        // 启动
        private void btnRun_Click(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                MessageBox.Show("未连接设备", "运动控制", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ApplyMotionParams();

                // 读取终点位置
                float[] ends = {
                    float.Parse(txtEndZ1.Text), float.Parse(txtEndZ2.Text),
                    float.Parse(txtEndZ3.Text), float.Parse(txtEndX.Text)
                };
                TextBox[] endBoxes = { txtEndZ1, txtEndZ2, txtEndZ3, txtEndX };
                string[] axisNames = { "Z1", "Z2", "Z3", "X" };

                // 软限位校验
                for (int i = 0; i < 4; i++)
                {
                    float target = isAbsoluteMode ? ends[i] : zmcControl.GetDpos(i) + ends[i];
                    if (!CheckSoftLimit(target, axisNames[i])) return;
                }

                // 执行运动
                for (int i = 0; i < 4; i++)
                {
                    if (endBoxes[i].Text.Length > 0)
                        MoveAxis(i, ends[i]);
                }

                lblStatus.Text = "运动中...";
                toolStripStatusLabel1.Text = "运动中...";
            }
            catch (Exception ex)
            {
                MessageBox.Show("运动控制失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 停止
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                MessageBox.Show("未连接设备", "急停", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 立即停止所有轴
                int ret = zmcControl.Rapidstop(2); // 2=立即停止
                if (ret == 0)
                {
                    lblStatus.Text = "已急停";
                    toolStripStatusLabel1.Text = "已急停";
                    MessageBox.Show("已执行急停操作", "急停", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("急停失败，错误码: " + ret, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("急停失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 扫描
        private void btnScan_Click(object sender, EventArgs e)
        {
            string ipList = zmcControl.SearchEthList(500); // 500ms 超时
            if (string.IsNullOrEmpty(ipList) || ipList.Trim() == "")
            {
                MessageBox.Show("未扫描到任何设备", "扫描结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 清空现有项目
            txtIp.Items.Clear();

            // 解析 IP 列表并添加到下拉菜单
            string[] ips = ipList.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string ip in ips)
            {
                string trimmedIp = ip.Trim();
                if (!string.IsNullOrEmpty(trimmedIp) && !txtIp.Items.Contains(trimmedIp))
                {
                    txtIp.Items.Add(trimmedIp);
                }
            }

            // 如果有找到设备，选中第一个
            if (txtIp.Items.Count > 0)
            {
                txtIp.SelectedIndex = 0;
                MessageBox.Show($"扫描完成，找到 {txtIp.Items.Count} 个设备", "扫描结果",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // 定时器刷新状态：立即停止计时器，将 DLL 调用放到后台线程，避免网络超时时卡死 UI
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            if (!zmcControl.IsConnected) { SetDisconnectedUI(); return; }

            bool doIo = tabControl1.SelectedTab == tabPage2; // 在 UI 线程读取控件状态

            System.Threading.Tasks.Task.Run(() => PollDevice(doIo));
        }

        private void PollDevice(bool doIo)
        {
            try
            {
                float z1, z2, z3, x;
                int ret = zmcControl.GetDpos(0, out z1);
                if (ret != 0)
                {
                    if (IsDisposed || !IsHandleCreated) return;
                    BeginInvoke(new Action(() =>
                    {
                        zmcControl.Disconnect();
                        SetDisconnectedUI("设备已断开连接");
                        MessageBox.Show($"设备通信异常（错误码：{ret}），连接已断开。", "连接中断",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                    return;
                }
                z2 = zmcControl.GetDpos(1);
                z3 = zmcControl.GetDpos(2);
                x  = zmcControl.GetDpos(3);

                bool[] idles = new bool[4];
                for (int axis = 0; axis <= 3; axis++)
                    idles[axis] = zmcControl.GetIfIdle(axis) != 0;

                uint in0State = zmcControl.GetIn(HomeLimitInput);

                uint[] ins  = null;
                uint[] ops  = null;
                if (doIo)
                {
                    ins = new uint[40];
                    ops = new uint[16];
                    for (int i = 0; i < 40; i++) ins[i] = zmcControl.GetIn(i);
                    for (int i = 0; i < 16; i++) ops[i] = zmcControl.GetOp(i);
                }

                if (IsDisposed || !IsHandleCreated) return;
                BeginInvoke(new Action(() =>
                {
                    if (!zmcControl.IsConnected) { SetDisconnectedUI(); return; }

                    txtZ1.Text = z1.ToString("F3");
                    txtZ2.Text = z2.ToString("F3");
                    txtZ3.Text = z3.ToString("F3");
                    txtX.Text  = x.ToString("F3");

                    bool allIdle = true;
                    TextBox[] stateBoxes = { txtStateZ1, txtStateZ2, txtStateZ3, txtStateX };
                    for (int axis = 0; axis <= 3; axis++)
                    {
                        stateBoxes[axis].Text = idles[axis] ? "停止" : "运动中";
                        if (!idles[axis]) allIdle = false;
                    }

                    string sl = toolStripStatusLabel1.Text;
                    if (allIdle && (sl == "运动中..." || sl == "清零中..."))
                    {
                        toolStripStatusLabel1.Text = "就绪";
                        lblStatus.Text = "已连接 ";
                    }

                    // IN0 限位状态显示（低电平触发，GetIn 返回 1 表示被拉低/触发）
                    bool limitHit = (in0State != 0);
                    lblHomeLimitStatus.Text      = "IN0: " + (limitHit ? "触发" : "正常");
                    lblHomeLimitStatus.BackColor = limitHit
                        ? System.Drawing.Color.OrangeRed
                        : System.Drawing.Color.LimeGreen;

                    // 回零：限位上升沿 → 急停，进入等待轴停止状态
                    if (isHoming && limitHit && !_prevLimitHit)
                    {
                        zmcControl.Rapidstop(2);
                        _homingWaitIdle = true;
                        isHoming = false;
                    }
                    _prevLimitHit = limitHit;

                    // 等待 Z1 轴完全停止后再执行设为零点
                    if (_homingWaitIdle && idles[0])
                    {
                        zmcControl.SetDpos(0, 0f);
                        _homingWaitIdle = false;
                        btnHome.Enabled = true;
                        toolStripStatusLabel1.Text = "回零完成";
                        lblStatus.Text = "已连接 ";
                    }

                    if (doIo && ins != null)
                    {
                        for (int i = 0; i < 40; i++)
                        {
                            uint disp = ins[i] == 0 ? 1u : 0u;
                            _inLeds[i].Text      = "IN" + i + "\n" + disp;
                            _inLeds[i].BackColor = disp != 0 ? Color.LimeGreen : Color.LightGray;
                        }
                        for (int i = 0; i < 16; i++)
                        {
                            _outState[i] = ops[i];
                            uint disp = ops[i] == 0 ? 1u : 0u;
                            _outBtns[i].Text      = "OUT" + i + "\n" + disp;
                            _outBtns[i].BackColor = disp != 0 ? Color.Orange : SystemColors.Control;
                        }
                    }

                    timer1.Start(); // 本次轮询完成后重启计时器
                }));
            }
            catch (Exception ex)
            {
                try
                {
                    if (IsDisposed || !IsHandleCreated) return;
                    BeginInvoke(new Action(() =>
                    {
                        zmcControl.Disconnect();
                        SetDisconnectedUI("设备已断开连接");
                        MessageBox.Show("设备通信异常，已自动断开连接：" + ex.Message, "连接错误",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
                catch (ObjectDisposedException) { }
                catch (InvalidOperationException) { }
            }
        }

        private void UpdateButtonStates(bool connected)
        {
            btnRun.Enabled = connected;
            btnStop.Enabled = connected;
            btnZero.Enabled = connected;
            btnSetZero.Enabled = connected;
            btnClose.Enabled = connected;
            btnOpen.Enabled = !connected;
            if (btnHome != null) btnHome.Enabled = connected && !isHoming;
        }

        private void SetDisconnectedUI(string statusMessage = "未连接")
        {
            timer1.Stop();
            lblStatus.Text = statusMessage;
            toolStripStatusLabel1.Text = "就绪";
            isHoming = false;
            _homingWaitIdle = false;
            _prevLimitHit = false;
            if (lblHomeLimitStatus != null)
            {
                lblHomeLimitStatus.Text = "IN0: ---";
                lblHomeLimitStatus.BackColor = System.Drawing.Color.LightGray;
            }
            UpdateButtonStates(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateButtonStates(false);
            InitIoTab();
            InitLimitControls();
        }

        private void InitIoTab()
        {
            // --- 数字输入 GroupBox（IN0~IN39，8列×5行）---
            const int cols = 8;
            const int cellW = 54, cellH = 26, cellMargin = 2;
            const int grpPadLeft = 8, grpPadTop = 22;

            var grpIn = new GroupBox
            {
                Text     = "数字输入 IN0~IN39",
                Location = new Point(4, 4),
                Size     = new Size(462, grpPadTop + 5 * (cellH + cellMargin) + 6)
            };

            for (int i = 0; i < 40; i++)
            {
                int col = i % cols;
                int row = i / cols;
                var lbl = new Label
                {
                    Text      = "IN" + i + "\n0",
                    Tag       = i,
                    Size      = new Size(cellW, cellH),
                    Location  = new Point(grpPadLeft + col * (cellW + cellMargin),
                                          grpPadTop  + row * (cellH + cellMargin)),
                    TextAlign = ContentAlignment.MiddleCenter,
                    BackColor = Color.LightGray,
                    BorderStyle = BorderStyle.FixedSingle,
                    Font      = new Font("Courier New", 7f)
                };
                _inLeds[i] = lbl;
                grpIn.Controls.Add(lbl);
            }

            // --- 数字输出 GroupBox（OUT0~OUT15，8列×2行）---
            const int btnW = 54, btnH = 30;
            int grpInBottom = grpIn.Bottom;

            var grpOut = new GroupBox
            {
                Text     = "数字输出 OUT0~OUT15",
                Location = new Point(4, grpInBottom + 4),
                Size     = new Size(462, grpPadTop + 2 * (btnH + cellMargin) + 8)
            };

            for (int i = 0; i < 16; i++)
            {
                int col = i % cols;
                int row = i / cols;
                int idx = i; // capture for lambda
                var btn = new Button
                {
                    Text     = "OUT" + i + "\n0",
                    Tag      = idx,
                    Size     = new Size(btnW, btnH),
                    Location = new Point(grpPadLeft + col * (btnW + cellMargin),
                                         grpPadTop  + row * (btnH + cellMargin)),
                    Font     = new Font("Courier New", 7f)
                };
                btn.Click += (s, e) =>
                {
                    if (!zmcControl.IsConnected) return;
                    uint newVal = _outState[idx] != 0 ? 0u : 1u;
                    zmcControl.SetOp(idx, newVal);
                };
                _outBtns[i] = btn;
                grpOut.Controls.Add(btn);
            }

            tabPage2.Controls.Add(grpIn);
            tabPage2.Controls.Add(grpOut);
        }
        // 重置（归零到机械原点）
        private void btnZero_Click(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                MessageBox.Show("未连接设备", "清零", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ApplyMotionParams();

                for (int axis = 0; axis <= 3; axis++)
                {
                    if (isAbsoluteMode)
                        zmcControl.SingleMoveAbs(axis, 0);
                    else
                        zmcControl.SingleMove(axis, -zmcControl.GetDpos(axis));
                }

                lblStatus.Text = "清零中...";
                toolStripStatusLabel1.Text = "清零中...";
                MessageBox.Show("已开始清零操作，所有轴将移动到0位置", "清零",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("清零失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitLimitControls()
        {
            tabControl1.Height += 56;
            statusStrip1.Top   += 56;
            this.ClientSize = new System.Drawing.Size(ClientSize.Width, ClientSize.Height + 56);

            var grpLimit = new System.Windows.Forms.GroupBox
            {
                Text     = "限位控制",
                Location = new System.Drawing.Point(4, 287),
                Size     = new System.Drawing.Size(466, 52)
            };

            lblHomeLimitStatus = new System.Windows.Forms.Label
            {
                Text        = "IN0: ---",
                Location    = new System.Drawing.Point(8, 18),
                Size        = new System.Drawing.Size(100, 24),
                TextAlign   = System.Drawing.ContentAlignment.MiddleCenter,
                BackColor   = System.Drawing.Color.LightGray,
                BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            };

            var lblSoftLimit = new System.Windows.Forms.Label
            {
                Text      = "软限位: 0 ~ 400",
                Location  = new System.Drawing.Point(118, 20),
                Size      = new System.Drawing.Size(120, 20),
                TextAlign = System.Drawing.ContentAlignment.MiddleLeft
            };

            btnHome = new System.Windows.Forms.Button
            {
                Text     = "回零",
                Location = new System.Drawing.Point(362, 14),
                Size     = new System.Drawing.Size(92, 30),
                Enabled  = false
            };
            btnHome.Click += btnHome_Click;

            grpLimit.Controls.Add(lblHomeLimitStatus);
            grpLimit.Controls.Add(lblSoftLimit);
            grpLimit.Controls.Add(btnHome);
            tabPage1.Controls.Add(grpLimit);
        }

        private bool CheckSoftLimit(float targetPos, string axisName)
        {
            if (targetPos < 0 || targetPos > SoftLimitMax)
            {
                MessageBox.Show(
                    $"{axisName}轴目标位置 {targetPos:F3} 超出软限位范围 (0 ~ {SoftLimitMax})",
                    "软限位触发", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                MessageBox.Show("未连接设备", "回零", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 如果 IN0 当前已触发，直接将 Z1 坐标置零，无需运动
            if (zmcControl.GetIn(HomeLimitInput) != 0)
            {
                zmcControl.SetDpos(0, 0f);
                toolStripStatusLabel1.Text = "回零完成（已在零点）";
                return;
            }

            try
            {
                float homingSpeed = Math.Max(5f, Math.Min(100f, float.Parse(txtSpeed.Text) / 10f));
                zmcControl.SetUnits(0, float.Parse(txtUnits.Text));
                zmcControl.SetSpeed(0, homingSpeed);
                zmcControl.SetAccel(0, float.Parse(txtAccel.Text));
                zmcControl.SetDecel(0, float.Parse(txtDecel.Text));

                isHoming = true;
                btnHome.Enabled = false;
                toolStripStatusLabel1.Text = "回零中...";
                lblStatus.Text = "回零中...";

                zmcControl.SingleVmove(0, -1); // -1=负方向连续运动
            }
            catch (Exception ex)
            {
                isHoming = false;
                btnHome.Enabled = true;
                MessageBox.Show("回零启动失败: " + ex.Message, "错误",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 设为零点（类似电子秤去皮功能）
        private void btnSetZero_Click(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                MessageBox.Show("未连接设备", "设为零点", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 确保所有轴已停止，防止运动中修改坐标系
            for (int axis = 0; axis <= 3; axis++)
            {
                if (zmcControl.GetIfIdle(axis) == 0)
                {
                    MessageBox.Show("有电机正在运动，请等待所有轴停止后再设为零点。", "设为零点",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                // 为所有轴设置零点（将当前位置设为0）
                for (int axis = 0; axis <= 3; axis++)
                {
                    int ret = zmcControl.SetDpos(axis, 0);
                    if (ret != 0)
                    {
                        MessageBox.Show($"轴 {axis} 设为零点失败，错误码: {ret}", "错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                lblStatus.Text = "已设为零点";
                toolStripStatusLabel1.Text = "已设为零点";
                MessageBox.Show("所有轴当前位置已设为零点", "设为零点",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("设为零点失败: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
