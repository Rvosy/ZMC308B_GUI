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
            this.txtUnits = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtSpeed = new System.Windows.Forms.TextBox();
            this.txtAccel = new System.Windows.Forms.TextBox();
            this.txtDecel = new System.Windows.Forms.TextBox();
            this.txtEndZ1 = new System.Windows.Forms.TextBox();
            this.txtEndZ2 = new System.Windows.Forms.TextBox();
            this.txtEndZ3 = new System.Windows.Forms.TextBox();
            this.txtEndX = new System.Windows.Forms.TextBox();
            this.txtZ2 = new System.Windows.Forms.TextBox();
            this.txtZ3 = new System.Windows.Forms.TextBox();
            this.txtZ1 = new System.Windows.Forms.TextBox();
            this.txtX = new System.Windows.Forms.TextBox();
            this.txtIp = new System.Windows.Forms.TextBox();
            this.labelUnits = new System.Windows.Forms.Label();
            this.labelSpeed = new System.Windows.Forms.Label();
            this.labelAccel = new System.Windows.Forms.Label();
            this.labelDecel = new System.Windows.Forms.Label();
            this.lblpos = new System.Windows.Forms.Label();
            this.lblendpos = new System.Windows.Forms.Label();
            this.lblz1 = new System.Windows.Forms.Label();
            this.lblz2 = new System.Windows.Forms.Label();
            this.lblz3 = new System.Windows.Forms.Label();
            this.lblx = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtUnits
            // 
            this.txtUnits.Location = new System.Drawing.Point(74, 170);
            this.txtUnits.Name = "txtUnits";
            this.txtUnits.Size = new System.Drawing.Size(56, 21);
            this.txtUnits.TabIndex = 0;
            this.txtUnits.Text = "100";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(12, 47);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(41, 12);
            this.lblStatus.TabIndex = 12;
            this.lblStatus.Text = "未连接";
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(130, 9);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(81, 25);
            this.btnOpen.TabIndex = 13;
            this.btnOpen.Text = "连接";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(217, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(81, 25);
            this.btnClose.TabIndex = 14;
            this.btnClose.Text = "断开";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(150, 170);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(83, 45);
            this.btnRun.TabIndex = 15;
            this.btnRun.Text = "启动";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(150, 231);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(83, 45);
            this.btnStop.TabIndex = 16;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtSpeed
            // 
            this.txtSpeed.Location = new System.Drawing.Point(74, 197);
            this.txtSpeed.Name = "txtSpeed";
            this.txtSpeed.Size = new System.Drawing.Size(56, 21);
            this.txtSpeed.TabIndex = 17;
            this.txtSpeed.Text = "100";
            // 
            // txtAccel
            // 
            this.txtAccel.Location = new System.Drawing.Point(74, 224);
            this.txtAccel.Name = "txtAccel";
            this.txtAccel.Size = new System.Drawing.Size(56, 21);
            this.txtAccel.TabIndex = 18;
            this.txtAccel.Text = "1000";
            // 
            // txtDecel
            // 
            this.txtDecel.Location = new System.Drawing.Point(74, 251);
            this.txtDecel.Name = "txtDecel";
            this.txtDecel.Size = new System.Drawing.Size(56, 21);
            this.txtDecel.TabIndex = 19;
            this.txtDecel.Text = "1000";
            // 
            // txtEndZ1
            // 
            this.txtEndZ1.Location = new System.Drawing.Point(74, 119);
            this.txtEndZ1.Name = "txtEndZ1";
            this.txtEndZ1.Size = new System.Drawing.Size(70, 21);
            this.txtEndZ1.TabIndex = 20;
            this.txtEndZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndZ2
            // 
            this.txtEndZ2.Location = new System.Drawing.Point(150, 119);
            this.txtEndZ2.Name = "txtEndZ2";
            this.txtEndZ2.Size = new System.Drawing.Size(70, 21);
            this.txtEndZ2.TabIndex = 21;
            this.txtEndZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndZ3
            // 
            this.txtEndZ3.Location = new System.Drawing.Point(226, 119);
            this.txtEndZ3.Name = "txtEndZ3";
            this.txtEndZ3.Size = new System.Drawing.Size(70, 21);
            this.txtEndZ3.TabIndex = 22;
            this.txtEndZ3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtEndX
            // 
            this.txtEndX.Location = new System.Drawing.Point(330, 119);
            this.txtEndX.Name = "txtEndX";
            this.txtEndX.Size = new System.Drawing.Size(70, 21);
            this.txtEndX.TabIndex = 23;
            // 
            // txtZ2
            // 
            this.txtZ2.Location = new System.Drawing.Point(150, 92);
            this.txtZ2.Name = "txtZ2";
            this.txtZ2.Size = new System.Drawing.Size(70, 21);
            this.txtZ2.TabIndex = 24;
            this.txtZ2.Text = "0";
            this.txtZ2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZ3
            // 
            this.txtZ3.Location = new System.Drawing.Point(226, 92);
            this.txtZ3.Name = "txtZ3";
            this.txtZ3.Size = new System.Drawing.Size(70, 21);
            this.txtZ3.TabIndex = 25;
            this.txtZ3.Text = "0";
            this.txtZ3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtZ1
            // 
            this.txtZ1.Location = new System.Drawing.Point(74, 92);
            this.txtZ1.Name = "txtZ1";
            this.txtZ1.Size = new System.Drawing.Size(70, 21);
            this.txtZ1.TabIndex = 26;
            this.txtZ1.Text = "0";
            this.txtZ1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtX
            // 
            this.txtX.Location = new System.Drawing.Point(330, 92);
            this.txtX.Name = "txtX";
            this.txtX.Size = new System.Drawing.Size(70, 21);
            this.txtX.TabIndex = 27;
            this.txtX.Text = "0";
            this.txtX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(12, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(88, 21);
            this.txtIp.TabIndex = 28;
            this.txtIp.Text = "192.168.5.200";
            // 
            // labelUnits
            // 
            this.labelUnits.AutoSize = true;
            this.labelUnits.Location = new System.Drawing.Point(12, 179);
            this.labelUnits.Name = "labelUnits";
            this.labelUnits.Size = new System.Drawing.Size(53, 12);
            this.labelUnits.TabIndex = 29;
            this.labelUnits.Text = "脉冲当量";
            // 
            // labelSpeed
            // 
            this.labelSpeed.AutoSize = true;
            this.labelSpeed.Location = new System.Drawing.Point(12, 206);
            this.labelSpeed.Name = "labelSpeed";
            this.labelSpeed.Size = new System.Drawing.Size(29, 12);
            this.labelSpeed.TabIndex = 30;
            this.labelSpeed.Text = "速度";
            // 
            // labelAccel
            // 
            this.labelAccel.AutoSize = true;
            this.labelAccel.Location = new System.Drawing.Point(12, 233);
            this.labelAccel.Name = "labelAccel";
            this.labelAccel.Size = new System.Drawing.Size(41, 12);
            this.labelAccel.TabIndex = 31;
            this.labelAccel.Text = "加速度";
            // 
            // labelDecel
            // 
            this.labelDecel.AutoSize = true;
            this.labelDecel.Location = new System.Drawing.Point(10, 260);
            this.labelDecel.Name = "labelDecel";
            this.labelDecel.Size = new System.Drawing.Size(41, 12);
            this.labelDecel.TabIndex = 32;
            this.labelDecel.Text = "减速度";
            // 
            // lblpos
            // 
            this.lblpos.AutoSize = true;
            this.lblpos.Location = new System.Drawing.Point(12, 95);
            this.lblpos.Name = "lblpos";
            this.lblpos.Size = new System.Drawing.Size(53, 12);
            this.lblpos.TabIndex = 33;
            this.lblpos.Text = "当前位置";
            // 
            // lblendpos
            // 
            this.lblendpos.AutoSize = true;
            this.lblendpos.Location = new System.Drawing.Point(12, 122);
            this.lblendpos.Name = "lblendpos";
            this.lblendpos.Size = new System.Drawing.Size(53, 12);
            this.lblendpos.TabIndex = 34;
            this.lblendpos.Text = "终点位置";
            // 
            // lblz1
            // 
            this.lblz1.AutoSize = true;
            this.lblz1.Location = new System.Drawing.Point(99, 77);
            this.lblz1.Name = "lblz1";
            this.lblz1.Size = new System.Drawing.Size(17, 12);
            this.lblz1.TabIndex = 35;
            this.lblz1.Text = "z1";
            // 
            // lblz2
            // 
            this.lblz2.AutoSize = true;
            this.lblz2.Location = new System.Drawing.Point(175, 77);
            this.lblz2.Name = "lblz2";
            this.lblz2.Size = new System.Drawing.Size(17, 12);
            this.lblz2.TabIndex = 36;
            this.lblz2.Text = "z2";
            // 
            // lblz3
            // 
            this.lblz3.AutoSize = true;
            this.lblz3.Location = new System.Drawing.Point(252, 77);
            this.lblz3.Name = "lblz3";
            this.lblz3.Size = new System.Drawing.Size(17, 12);
            this.lblz3.TabIndex = 37;
            this.lblz3.Text = "z3";
            // 
            // lblx
            // 
            this.lblx.AutoSize = true;
            this.lblx.Location = new System.Drawing.Point(358, 77);
            this.lblx.Name = "lblx";
            this.lblx.Size = new System.Drawing.Size(11, 12);
            this.lblx.TabIndex = 38;
            this.lblx.Text = "x";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(436, 286);
            this.Controls.Add(this.lblx);
            this.Controls.Add(this.lblz3);
            this.Controls.Add(this.lblz2);
            this.Controls.Add(this.lblz1);
            this.Controls.Add(this.lblendpos);
            this.Controls.Add(this.lblpos);
            this.Controls.Add(this.labelDecel);
            this.Controls.Add(this.labelAccel);
            this.Controls.Add(this.labelSpeed);
            this.Controls.Add(this.labelUnits);
            this.Controls.Add(this.txtIp);
            this.Controls.Add(this.txtX);
            this.Controls.Add(this.txtZ1);
            this.Controls.Add(this.txtZ3);
            this.Controls.Add(this.txtZ2);
            this.Controls.Add(this.txtEndX);
            this.Controls.Add(this.txtEndZ3);
            this.Controls.Add(this.txtEndZ2);
            this.Controls.Add(this.txtEndZ1);
            this.Controls.Add(this.txtDecel);
            this.Controls.Add(this.txtAccel);
            this.Controls.Add(this.txtSpeed);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtUnits);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUnits;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnStop;
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
        private System.Windows.Forms.TextBox txtIp;
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
    }
}

