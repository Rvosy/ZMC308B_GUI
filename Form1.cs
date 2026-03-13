using System;
using System.Windows.Forms;

namespace ZMC
{
    public partial class Form1 : Form
    {
        ZmcDll zmcControl = new ZmcDll();

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

            timer1.Stop();
            int ret = zmcControl.Close();
            if (ret != 0)
            {
                lblStatus.Text = "断开失败";
                MessageBox.Show("断开失败，错误码：" + ret);
            }
            else
            {
                lblStatus.Text = "未连接";
                UpdateButtonStates(false);
                toolStripStatusLabel1.Text = "就绪";
                MessageBox.Show("已断开连接");
            }
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

        // 定时器刷新状态
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!zmcControl.IsConnected)
            {
                lblStatus.Text = "未连接";
                timer1.Stop();
                return;
            }

            try
            {
                // 读取 Z1 轴位置
                txtZ1.Text = zmcControl.GetDpos(0).ToString("F3");

                // 读取 Z2 轴位置
                txtZ2.Text = zmcControl.GetDpos(1).ToString("F3");

                // 读取 Z3 轴位置
                txtZ3.Text = zmcControl.GetDpos(2).ToString("F3");

                // 读取 X 轴位置
                txtX.Text = zmcControl.GetDpos(3).ToString("F3");

                // 读取各轴运动状态
                bool allIdle = true;
                TextBox[] stateBoxes = { txtStateZ1, txtStateZ2, txtStateZ3, txtStateX };
                for (int axis = 0; axis <= 3; axis++)
                {
                    bool idle = zmcControl.GetIfIdle(axis) != 0;
                    stateBoxes[axis].Text = idle ? "停止" : "运动中";
                    if (!idle) allIdle = false;
                }

                // 所有轴停止后自动复位状态栏
                string sl = toolStripStatusLabel1.Text;
                if (allIdle && (sl == "运动中..." || sl == "清零中..."))
                {
                    toolStripStatusLabel1.Text = "就绪";
                    lblStatus.Text = "已连接 ";
                }
            }
            catch (Exception ex)
            {
                lblStatus.Text = "读取状态异常: " + ex.Message;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateButtonStates(false);
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
