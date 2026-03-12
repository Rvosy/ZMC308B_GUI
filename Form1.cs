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
                lblStatus.Text = "已连接 " + ip;
                timer1.Start();
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
                MessageBox.Show("已断开连接");
            }
        }
        // 停止
        private void btnStop_Click(object sender, EventArgs e)
        {

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
            // 暂时只显示连接状态，后续再加坐标读取
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
