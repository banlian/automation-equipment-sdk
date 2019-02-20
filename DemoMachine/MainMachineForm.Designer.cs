namespace DemoMachine
{
    partial class MainMachineForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelUser = new System.Windows.Forms.Panel();
            this.groupBoxUser = new System.Windows.Forms.GroupBox();
            this.buttonReset = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonUserLogin = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelUser.SuspendLayout();
            this.groupBoxUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 31.5568F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 68.4432F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tableLayoutPanel1.Controls.Add(this.panelUser, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonUserLogin, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.28671F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 86.71329F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelUser
            // 
            this.panelUser.Controls.Add(this.groupBoxUser);
            this.panelUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUser.Location = new System.Drawing.Point(716, 60);
            this.panelUser.Name = "panelUser";
            this.panelUser.Size = new System.Drawing.Size(81, 366);
            this.panelUser.TabIndex = 3;
            // 
            // groupBoxUser
            // 
            this.groupBoxUser.Controls.Add(this.buttonReset);
            this.groupBoxUser.Controls.Add(this.buttonStop);
            this.groupBoxUser.Controls.Add(this.buttonStart);
            this.groupBoxUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxUser.Location = new System.Drawing.Point(0, 0);
            this.groupBoxUser.Name = "groupBoxUser";
            this.groupBoxUser.Size = new System.Drawing.Size(81, 366);
            this.groupBoxUser.TabIndex = 3;
            this.groupBoxUser.TabStop = false;
            // 
            // buttonReset
            // 
            this.buttonReset.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonReset.Location = new System.Drawing.Point(3, 87);
            this.buttonReset.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 35);
            this.buttonReset.TabIndex = 5;
            this.buttonReset.Text = "复位";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonStop.Location = new System.Drawing.Point(3, 52);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 35);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Dock = System.Windows.Forms.DockStyle.Top;
            this.buttonStart.Location = new System.Drawing.Point(3, 17);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 35);
            this.buttonStart.TabIndex = 3;
            this.buttonStart.Text = "启动";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonUserLogin
            // 
            this.buttonUserLogin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonUserLogin.Location = new System.Drawing.Point(716, 3);
            this.buttonUserLogin.Name = "buttonUserLogin";
            this.buttonUserLogin.Size = new System.Drawing.Size(81, 51);
            this.buttonUserLogin.TabIndex = 4;
            this.buttonUserLogin.Text = "登录";
            this.buttonUserLogin.UseVisualStyleBackColor = true;
            this.buttonUserLogin.Click += new System.EventHandler(this.buttonUserLogin_Click);
            // 
            // MainMachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainMachineForm";
            this.Text = "设备状态";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainMachineForm_FormClosing);
            this.Load += new System.EventHandler(this.MainMachineForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelUser.ResumeLayout(false);
            this.groupBoxUser.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelUser;
        private System.Windows.Forms.GroupBox groupBoxUser;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonUserLogin;
    }
}