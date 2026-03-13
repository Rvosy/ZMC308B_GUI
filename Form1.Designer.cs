namespace ZMC
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpConnect = new System.Windows.Forms.GroupBox();
            this.txtIp = new System.Windows.Forms.ComboBox();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.grpAxis = new System.Windows.Forms.GroupBox();
            this.lblz1 = new System.Windows.Forms.Label();
            this.lblz2 = new System.Windows.Forms.Label();
            this.lblz3 = new System.Windows.Forms.Label();
            this.lblx = new System.Windows.Forms.Label();
            this.lblpos = new System.Windows.Forms.Label();
            this.txtZ1 = new System.Windows.Forms.TextBox();
            this.txtZ2 = new System.Windows.Forms.TextBox();
            this.txtZ3 = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.lblendpos = new System.Windows.Forms.Label();
            this.txtEndZ1 = new System.Windows.Forms.TextBox();
            this.txtEndZ2 = new System.Windows.Forms.TextBox();
            this.txtEndZ3 = new System.Windows.Forms.TextBox();
            this.txtEndX = new System.Windows.Forms.TextBox();
            this.lblAxisStateLabel = new System.Windows.Forms.Label();
            this.txtStateZ1 = new System.Windows.Forms.TextBox();
            this.txtStateZ2 = new System.Windows.Forms.TextBox();
            this.txtStateZ3 = new System.Windows.Forms.TextBox();
            this.txtStateX = new System.Windows.Forms.TextBox();
            this.rdoAbsolute = new System.Windows.Forms.RadioButton();
            this.rdoRelative = new System.Windows.Forms.RadioButton();
            this.grpParams = new System.Windows.Forms.GroupBox();
            this.labelUnits = new System.Windows.Forms.Label();
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.labelAccel = new System.Windows.Forms.Label();
            this.txtAccel = new System.Windows.Forms.TextBox();
            this.labelDecel = new System.Windows.Forms.Label();
            this.txtDecel = new System.Windows.Forms.TextBox();
            this.grpControl = new System.Windows.Forms.GroupBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnZero = new System.Windows.Forms.Button();
            this.btnSetZero = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.grpConnect.SuspendLayout();
            this.grpAxis.SuspendLayout();
            this.grpParams.SuspendLayout();
            this.grpControl.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpConnect
            // 
            this.grpConnect.Controls.Add(this.txtIp);
            this.grpConnect.Controls.Add(this.btnOpen);
            this.grpConnect.Controls.Add(this.btnClose);
            this.grpConnect.Controls.Add(this.btnScan);
            this.grpConnect.Controls.Add(this.lblStatus);
            this.grpConnect.Location = new System.Drawing.Point(8, 8);
            this.grpConnect.Name = "grpConnect";
            this.grpConnect.Size = new System.Drawing.Size(484, 48);
            this.grpConnect.TabIndex = 0;
            this.grpConnect.TabStop = false;
            this.grpConnect.Text = "设备连接";
            // 
            // txtIp
            // 
            this.txtIp.FormattingEnabled = true;
            this.txtIp.Location = new System.Drawing.Point(8, 17);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(115, 20);
            this.txtIp.TabIndex = 0;
            this.txtIp.Text = "127.0.0.1";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(142, 16);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 1;
            this.btnOpen.Text = "连接";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(222, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "断开";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(325, 16);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(418, 21);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 12);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "未连接";
            // 
            // grpAxis
            // 
            this.grpAxis.Controls.Add(this.lblz1);
            this.grpAxis.Controls.Add(this.lblz2);
            this.grpAxis.Controls.Add(this.lblz3);
            this.grpAxis.Controls.Add(this.lblx);
            this.grpAxis.Controls.Add(this.lblpos);
            this.grpAxis.Controls.Add(this.txtZ1);
            this.grpAxis.Controls.Add(this.txtZ2);
            this.grpAxis.Controls.Add(this.txtZ3);
            this.grpAxis.Controls.Add(this.txtX);
            this.grpAxis.Controls.Add(this.lblendpos);
            this.grpAxis.Controls.Add(this.txtEndZ1);
            this.grpAxis.Controls.Add(this.txtEndZ2);
            this.grpAxis.Controls.Add(this.txtEndZ3);
            this.grpAxis.Controls.Add(this.txtEndX);
            this.grpAxis.Controls.Add(this.lblAxisStateLabel);
            this.grpAxis.Controls.Add(this.txtStateZ1);
            this.grpAxis.Controls.Add(this.txtStateZ2);
            this.grpAxis.Controls.Add(this.txtStateZ3);
            this.grpAxis.Controls.Add(this.txtStateX);
            this.grpAxis.Location = new System.Drawing.Point(8, 62);
            this.grpAxis.Name = "grpAxis";
            this.grpAxis.Size = new System.Drawing.Size(484, 114);
            this.grpAxis.TabIndex = 1;
            this.grpAxis.TabStop = false;
            this.grpAxis.Text = "轴位置";
            // 
            // lblz1
            // 
            this.lblz1.AutoSize = true;
            this.lblz1.Location = new System.Drawing.Point(105, 14);
            this.lblz1.Name = "lblz1";
            this.lblz1.Size = new System.Drawing.Size(29, 12);
            this.lblz1.TabIndex = 0;
            this.lblz1.Text = "Z1轴";
            // 
            // lblz2
            // 
            this.lblz2.AutoSize = true;
            this.lblz2.Location = new System.Drawing.Point(204, 14);
            this.lblz2.Name = "lblz2";
            this.lblz2.Size = new System.Drawing.Size(29, 12);
            this.lblz2.TabIndex = 1;
            this.lblz2.Text = "Z2轴";
            // 
            // lblz3
            // 
            this.lblz3.AutoSize = true;
            this.lblz3.Location = new System.Drawing.Point(303, 14);
            this.lblz3.Name = "lblz3";
            this.lblz3.Size = new System.Drawing.Size(29, 12);
            this.lblz3.TabIndex = 2;
            this.lblz3.Text = "Z3轴";
            // 
            // lblx
            // 
            this.lblx.AutoSize = true;
            this.lblx.Location = new System.Drawing.Point(407, 14);
            this.lblx.Name = "lblx";
            this.lblx.Size = new System.Drawing.Size(23, 12);
            this.lblx.TabIndex = 3;
            this.lblx.Text = "X轴";
            // 
            // lblpos
            // 
            this.lblpos.AutoSize = true;
            this.lblpos.Location = new System.Drawing.Point(8, 32);
            this.lblpos.Name = "lblpos";
            this.lblpos.Size = new System.Drawing.Size(53, 12);
            this.lblpos.TabIndex = 4;
            this.lblpos.Text = "当前位置";
            // 
            // txtZ1
            // 
            this.txtZ1.Location = new System.Drawing.Point(80, 28);
            this.txtZ1.Name = "txtZ1";
            this.txtZ1.ReadOnly = true;
            this.txtZ1.Size = new System.Drawing.Size(80, 21);
            this.txtZ1.TabIndex = 5;
            this.txtZ1.Text = "0.000";
            this.txtZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZ2
            // 
            this.txtZ2.Location = new System.Drawing.Point(179, 28);
            this.txtZ2.Name = "txtZ2";
            this.txtZ2.ReadOnly = true;
            this.txtZ2.Size = new System.Drawing.Size(80, 21);
            this.txtZ2.TabIndex = 6;
            this.txtZ2.Text = "0.000";
            this.txtZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZ3
            // 
            this.txtZ3.Location = new System.Drawing.Point(278, 28);
            this.txtZ3.Name = "txtZ3";
            this.txtZ3.ReadOnly = true;
            this.txtZ3.Size = new System.Drawing.Size(80, 21);
            this.txtZ3.TabIndex = 7;
            this.txtZ3.Text = "0.000";
            this.txtZ3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(377, 28);
            this.txtX.Name = "txtX";
            this.txtX.ReadOnly = true;
            this.txtX.Size = new System.Drawing.Size(80, 21);
            this.txtX.TabIndex = 8;
            this.txtX.Text = "0.000";
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblendpos
            // 
            this.lblendpos.AutoSize = true;
            this.lblendpos.Location = new System.Drawing.Point(8, 58);
            this.lblendpos.Name = "lblendpos";
            this.lblendpos.Size = new System.Drawing.Size(53, 12);
            this.lblendpos.TabIndex = 9;
            this.lblendpos.Text = "终点位置";
            // 
            // txtEndZ1
            // 
            this.txtEndZ1.Location = new System.Drawing.Point(80, 54);
            this.txtEndZ1.Name = "txtEndZ1";
            this.txtEndZ1.Size = new System.Drawing.Size(80, 21);
            this.txtEndZ1.TabIndex = 10;
            this.txtEndZ1.Text = "0";
            this.txtEndZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndZ2
            // 
            this.txtEndZ2.Location = new System.Drawing.Point(179, 54);
            this.txtEndZ2.Name = "txtEndZ2";
            this.txtEndZ2.Size = new System.Drawing.Size(80, 21);
            this.txtEndZ2.TabIndex = 11;
            this.txtEndZ2.Text = "0";
            this.txtEndZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndZ3
            // 
            this.txtEndZ3.Location = new System.Drawing.Point(278, 54);
            this.txtEndZ3.Name = "txtEndZ3";
            this.txtEndZ3.Size = new System.Drawing.Size(80, 21);
            this.txtEndZ3.TabIndex = 12;
            this.txtEndZ3.Text = "0";
            this.txtEndZ3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndX
            // 
            this.txtEndX.Location = new System.Drawing.Point(377, 54);
            this.txtEndX.Name = "txtEndX";
            this.txtEndX.Size = new System.Drawing.Size(80, 21);
            this.txtEndX.TabIndex = 13;
            this.txtEndX.Text = "0";
            this.txtEndX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblAxisStateLabel
            // 
            this.lblAxisStateLabel.AutoSize = true;
            this.lblAxisStateLabel.Location = new System.Drawing.Point(8, 82);
            this.lblAxisStateLabel.Name = "lblAxisStateLabel";
            this.lblAxisStateLabel.Size = new System.Drawing.Size(53, 12);
            this.lblAxisStateLabel.TabIndex = 14;
            this.lblAxisStateLabel.Text = "运动状态";
            // 
            // txtStateZ1
            // 
            this.txtStateZ1.Location = new System.Drawing.Point(80, 80);
            this.txtStateZ1.Name = "txtStateZ1";
            this.txtStateZ1.ReadOnly = true;
            this.txtStateZ1.Size = new System.Drawing.Size(80, 21);
            this.txtStateZ1.TabIndex = 15;
            this.txtStateZ1.Text = "停止";
            this.txtStateZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStateZ2
            // 
            this.txtStateZ2.Location = new System.Drawing.Point(179, 80);
            this.txtStateZ2.Name = "txtStateZ2";
            this.txtStateZ2.ReadOnly = true;
            this.txtStateZ2.Size = new System.Drawing.Size(80, 21);
            this.txtStateZ2.TabIndex = 16;
            this.txtStateZ2.Text = "停止";
            this.txtStateZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStateZ3
            // 
            this.txtStateZ3.Location = new System.Drawing.Point(278, 80);
            this.txtStateZ3.Name = "txtStateZ3";
            this.txtStateZ3.ReadOnly = true;
            this.txtStateZ3.Size = new System.Drawing.Size(80, 21);
            this.txtStateZ3.TabIndex = 17;
            this.txtStateZ3.Text = "停止";
            this.txtStateZ3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtStateX
            // 
            this.txtStateX.Location = new System.Drawing.Point(377, 80);
            this.txtStateX.Name = "txtStateX";
            this.txtStateX.ReadOnly = true;
            this.txtStateX.Size = new System.Drawing.Size(80, 21);
            this.txtStateX.TabIndex = 18;
            this.txtStateX.Text = "停止";
            this.txtStateX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rdoAbsolute
            // 
            this.rdoAbsolute.Checked = true;
            this.rdoAbsolute.Location = new System.Drawing.Point(154, 23);
            this.rdoAbsolute.Name = "rdoAbsolute";
            this.rdoAbsolute.Size = new System.Drawing.Size(85, 16);
            this.rdoAbsolute.TabIndex = 0;
            this.rdoAbsolute.TabStop = true;
            this.rdoAbsolute.Text = "绝对运动";
            this.rdoAbsolute.UseVisualStyleBackColor = true;
            this.rdoAbsolute.CheckedChanged += new System.EventHandler(this.rdoAbsolute_CheckedChanged);
            // 
            // rdoRelative
            // 
            this.rdoRelative.Location = new System.Drawing.Point(154, 49);
            this.rdoRelative.Name = "rdoRelative";
            this.rdoRelative.Size = new System.Drawing.Size(85, 16);
            this.rdoRelative.TabIndex = 1;
            this.rdoRelative.Text = "相对运动";
            this.rdoRelative.UseVisualStyleBackColor = true;
            this.rdoRelative.CheckedChanged += new System.EventHandler(this.rdoRelative_CheckedChanged);
            // 
            // grpParams
            // 
            this.grpParams.Controls.Add(this.rdoRelative);
            this.grpParams.Controls.Add(this.rdoAbsolute);
            this.grpParams.Controls.Add(this.labelUnits);
            this.grpParams.Controls.Add(this.txtUnits);
            this.grpParams.Controls.Add(this.labelSpeed);
            this.grpParams.Controls.Add(this.txtSpeed);
            this.grpParams.Controls.Add(this.labelAccel);
            this.grpParams.Controls.Add(this.txtAccel);
            this.grpParams.Controls.Add(this.labelDecel);
            this.grpParams.Controls.Add(this.txtDecel);
            this.grpParams.Location = new System.Drawing.Point(8, 182);
            this.grpParams.Name = "grpParams";
            this.grpParams.Size = new System.Drawing.Size(245, 135);
            this.grpParams.TabIndex = 3;
            this.grpParams.TabStop = false;
            this.grpParams.Text = "运动参数";
            // 
            // labelUnits
            // 
            this.labelUnits.AutoSize = true;
            this.labelUnits.Location = new System.Drawing.Point(8, 22);
            this.labelUnits.Name = "labelUnits";
            this.labelUnits.Size = new System.Drawing.Size(53, 12);
            this.labelUnits.TabIndex = 0;
            this.labelUnits.Text = "脉冲当量";
            // 
            // txtUnits
            // 
            this.txtUnits.Location = new System.Drawing.Point(78, 18);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(60, 21);
            this.txtUnits.TabIndex = 1;
            this.txtUnits.Text = "1000";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(8, 48);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(53, 12);
            this.labelSpeed.TabIndex = 2;
            this.labelSpeed.Text = "速    度";
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(78, 44);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(60, 21);
            this.txtSpeed.TabIndex = 3;
            this.txtSpeed.Text = "100";
            // 
            // labelAccel
            // 
            this.labelAccel.AutoSize = true;
            this.labelAccel.Location = new System.Drawing.Point(8, 74);
            this.labelAccel.Name = "labelAccel";
            this.labelAccel.Size = new System.Drawing.Size(41, 12);
            this.labelAccel.TabIndex = 4;
            this.labelAccel.Text = "加速度";
            // 
            // txtAccel
            // 
            this.txtAccel.Location = new System.Drawing.Point(78, 70);
            this.txtAccel.Name = "txtAccel";
            this.txtAccel.Size = new System.Drawing.Size(60, 21);
            this.txtAccel.TabIndex = 5;
            this.txtAccel.Text = "1000";
            // 
            // labelDecel
            // 
            this.labelDecel.AutoSize = true;
            this.labelDecel.Location = new System.Drawing.Point(8, 100);
            this.labelDecel.Name = "labelDecel";
            this.labelDecel.Size = new System.Drawing.Size(41, 12);
            this.labelDecel.TabIndex = 6;
            this.labelDecel.Text = "减速度";
            // 
            // txtDecel
            // 
            this.txtDecel.Location = new System.Drawing.Point(78, 96);
            this.txtDecel.Name = "txtDecel";
            this.txtDecel.Size = new System.Drawing.Size(60, 21);
            this.txtDecel.TabIndex = 7;
            this.txtDecel.Text = "1000";
            // 
            // grpControl
            // 
            this.grpControl.Controls.Add(this.btnRun);
            this.grpControl.Controls.Add(this.btnStop);
            this.grpControl.Controls.Add(this.btnZero);
            this.grpControl.Controls.Add(this.btnSetZero);
            this.grpControl.Location = new System.Drawing.Point(258, 182);
            this.grpControl.Name = "grpControl";
            this.grpControl.Size = new System.Drawing.Size(234, 135);
            this.grpControl.TabIndex = 4;
            this.grpControl.TabStop = false;
            this.grpControl.Text = "控制";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(8, 20);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(100, 45);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(118, 20);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 45);
            this.btnStop.TabIndex = 1;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnZero
            // 
            this.btnZero.Location = new System.Drawing.Point(8, 78);
            this.btnZero.Name = "btnZero";
            this.btnZero.Size = new System.Drawing.Size(100, 45);
            this.btnZero.TabIndex = 2;
            this.btnZero.Text = "重置";
            this.btnZero.UseVisualStyleBackColor = true;
            this.btnZero.Click += new System.EventHandler(this.btnZero_Click);
            // 
            // btnSetZero
            // 
            this.btnSetZero.Location = new System.Drawing.Point(118, 78);
            this.btnSetZero.Name = "btnSetZero";
            this.btnSetZero.Size = new System.Drawing.Size(100, 45);
            this.btnSetZero.TabIndex = 3;
            this.btnSetZero.Text = "设为零点";
            this.btnSetZero.UseVisualStyleBackColor = true;
            this.btnSetZero.Click += new System.EventHandler(this.btnSetZero_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 329);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(500, 22);
            this.statusStrip1.TabIndex = 5;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(485, 17);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "就绪";
            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 351);
            this.Controls.Add(this.grpConnect);
            this.Controls.Add(this.grpAxis);
            this.Controls.Add(this.grpParams);
            this.Controls.Add(this.grpControl);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "ZMC 运动控制器";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpConnect.ResumeLayout(false);
            this.grpConnect.PerformLayout();
            this.grpAxis.ResumeLayout(false);
            this.grpAxis.PerformLayout();
            this.grpParams.ResumeLayout(false);
            this.grpParams.PerformLayout();
            this.grpControl.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpConnect;
        private System.Windows.Forms.GroupBox grpAxis;
        private System.Windows.Forms.GroupBox grpParams;
        private System.Windows.Forms.GroupBox grpControl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox txtUnits;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnZero;
        private System.Windows.Forms.Button btnSetZero;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox txtSpeed;
        private System.Windows.Forms.TextBox txtAccel;
        private System.Windows.Forms.TextBox txtDecel;
        private System.Windows.Forms.TextBox txtEndZ1;
        private System.Windows.Forms.TextBox txtEndZ2;
        private System.Windows.Forms.TextBox txtEndZ3;
        private System.Windows.Forms.TextBox txtEndX;
        private System.Windows.Forms.TextBox txtZ2;
        private System.Windows.Forms.TextBox txtZ3;
        private System.Windows.Forms.TextBox txtZ1;
        private System.Windows.Forms.TextBox txtX;
        private System.Windows.Forms.ComboBox txtIp;
        private System.Windows.Forms.Label labelUnits;
        private System.Windows.Forms.Label labelSpeed;
        private System.Windows.Forms.Label labelAccel;
        private System.Windows.Forms.Label labelDecel;
        private System.Windows.Forms.Label lblpos;
        private System.Windows.Forms.Label lblendpos;
        private System.Windows.Forms.Label lblz1;
        private System.Windows.Forms.Label lblz2;
        private System.Windows.Forms.Label lblz3;
        private System.Windows.Forms.Label lblx;
        private System.Windows.Forms.RadioButton rdoRelative;
        private System.Windows.Forms.RadioButton rdoAbsolute;
        private System.Windows.Forms.Label lblAxisStateLabel;
        private System.Windows.Forms.TextBox txtStateZ1;
        private System.Windows.Forms.TextBox txtStateZ2;
        private System.Windows.Forms.TextBox txtStateZ3;
        private System.Windows.Forms.TextBox txtStateX;
        private bool isAbsoluteMode = true; // 默认绝对运动模式
    }
}
