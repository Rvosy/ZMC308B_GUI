using System;
using System.Drawing;
using System.Windows.Forms;

namespace ZMC
{
    public partial class Form1 : Form
    {
        ZmcDll zmcControl = new ZmcDll();

        // IO 调试 Tab 控件
        private Label[]  _inLeds   = new Label[40];
        private Button[] _outBtns  = new Button[16];
        private uint[]   _outState = new uint[16];

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
        // 相对运动选中
        private void rdoRelative_CheckedChanged(object sender, EventArgs e)
        {
            isAbsoluteMode = false;
            lblendpos.Text = "移动增量";
            // 清空终点位置输入框，避免模式切换混淆
            txtEndZ1.Text = "0";
            txtEndZ2.Text = "0";
            txtEndZ3.Text = "0";
            txtEndX.Text = "0";
        }

        // 绝对运动选中
        private void rdoAbsolute_CheckedChanged(object sender, EventArgs e)
        {
            isAbsoluteMode = true;
            lblendpos.Text = "终点位置";
            // 清空终点位置输入框，避免模式切换混淆
            txtEndZ1.Text = "0";
            txtEndZ2.Text = "0";
            txtEndZ3.Text = "0";
            txtEndX.Text = "0";
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
                // 解析运动参数
                float units = float.Parse(txtUnits.Text);
                float speed = float.Parse(txtSpeed.Text);
                float accel = float.Parse(txtAccel.Text);
                float decel = float.Parse(txtDecel.Text);

                // 设置 Z1 轴参数（轴号0）
                zmcControl.SetUnits(0, units);
                zmcControl.SetSpeed(0, speed);
                zmcControl.SetAccel(0, accel);
                zmcControl.SetDecel(0, decel);

                // 设置 Z2 轴参数（轴号1）
                zmcControl.SetUnits(1, units);
                zmcControl.SetSpeed(1, speed);
                zmcControl.SetAccel(1, accel);
                zmcControl.SetDecel(1, decel);

                // 设置 Z3 轴参数（轴号2）
                zmcControl.SetUnits(2, units);
                zmcControl.SetSpeed(2, speed);
                zmcControl.SetAccel(2, accel);
                zmcControl.SetDecel(2, decel);

                // 设置 X 轴参数（轴号3）
                zmcControl.SetUnits(3, units);
                zmcControl.SetSpeed(3, speed);
                zmcControl.SetAccel(3, accel);
                zmcControl.SetDecel(3, decel);

                // 读取终点位置
                float endZ1 = float.Parse(txtEndZ1.Text);
                float endZ2 = float.Parse(txtEndZ2.Text);
                float endZ3 = float.Parse(txtEndZ3.Text);
                float endX = float.Parse(txtEndX.Text);

                // 单轴运动：Z1 轴
                if (txtEndZ1.Text.Length > 0)
                {
                    if (isAbsoluteMode)
                        zmcControl.SingleMoveAbs(0, endZ1);
                    else
                        zmcControl.SingleMoveRel(0, endZ1);
                }

                // 单轴运动：Z2 轴
                if (txtEndZ2.Text.Length > 0)
                {
                    if (isAbsoluteMode)
                        zmcControl.SingleMoveAbs(1, endZ2);
                    else
                        zmcControl.SingleMoveRel(1, endZ2);
                }

                // 单轴运动：Z3 轴
                if (txtEndZ3.Text.Length > 0)
                {
                    if (isAbsoluteMode)
                        zmcControl.SingleMoveAbs(2, endZ3);
                    else
                        zmcControl.SingleMoveRel(2, endZ3);
                }

                // 单轴运动：X 轴
                if (txtEndX.Text.Length > 0)
                {
                    if (isAbsoluteMode)
                        zmcControl.SingleMoveAbs(3, endX);
                    else
                        zmcControl.SingleMoveRel(3, endX);
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
                int ret = zmcControl.TryGetDpos(0, out z1);
                if (ret != 0)
                {
                    if (IsDisposed || !IsHandleCreated) return;
                    BeginInvoke(new Action(() =>
                    {
                        zmcControl.Handle = IntPtr.Zero;
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
                if (IsDisposed || !IsHandleCreated) return;
                BeginInvoke(new Action(() =>
                {
                    zmcControl.Handle = IntPtr.Zero;
                    SetDisconnectedUI("设备已断开连接");
                    MessageBox.Show("设备通信异常，已自动断开连接：" + ex.Message, "连接错误",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                }));
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
        }

        private void SetDisconnectedUI(string statusMessage = "未连接")
        {
            timer1.Stop();
            lblStatus.Text = statusMessage;
            toolStripStatusLabel1.Text = "就绪";
            UpdateButtonStates(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateButtonStates(false);
            InitIoTab();
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
                // 解析运动参数
                float units = float.Parse(txtUnits.Text);
                float speed = float.Parse(txtSpeed.Text);
                float accel = float.Parse(txtAccel.Text);
                float decel = float.Parse(txtDecel.Text);

                // 为所有轴设置运动参数
                for (int axis = 0; axis <= 3; axis++)
                {
                    zmcControl.SetUnits(axis, units);
                    zmcControl.SetSpeed(axis, speed);
                    zmcControl.SetAccel(axis, accel);
                    zmcControl.SetDecel(axis, decel);
                }

                // 根据运动模式执行清零
                if (isAbsoluteMode)
                {
                    // 绝对运动模式：直接移动到0位置
                    zmcControl.SingleMoveAbs(0, 0);  // Z1 轴
                    zmcControl.SingleMoveAbs(1, 0);  // Z2 轴
                    zmcControl.SingleMoveAbs(2, 0);  // Z3 轴
                    zmcControl.SingleMoveAbs(3, 0);  // X 轴
                }
                else
                {
                    // 相对运动模式：读取当前位置并计算反向移动距离
                    float currentZ1 = zmcControl.GetDpos(0);
                    float currentZ2 = zmcControl.GetDpos(1);
                    float currentZ3 = zmcControl.GetDpos(2);
                    float currentX = zmcControl.GetDpos(3);

                    zmcControl.SingleMoveRel(0, -currentZ1);  // Z1 轴反向移动到0
                    zmcControl.SingleMoveRel(1, -currentZ2);  // Z2 轴反向移动到0
                    zmcControl.SingleMoveRel(2, -currentZ3);  // Z3 轴反向移动到0
                    zmcControl.SingleMoveRel(3, -currentX);   // X 轴反向移动到0
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
