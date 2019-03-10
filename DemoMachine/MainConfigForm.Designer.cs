namespace DemoMachine
{
    partial class MainConfigForm
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
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageIO = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.diControl1 = new Automation.FrameworkExtension.userControls.DiControl();
            this.doControl1 = new Automation.FrameworkExtension.userControls.DoControl();
            this.tabPageCY = new System.Windows.Forms.TabPage();
            this.cylinderControl1 = new Automation.FrameworkExtension.userControls.CylinderControl();
            this.tabPageVIO = new System.Windows.Forms.TabPage();
            this.vioControl1 = new Automation.FrameworkExtension.userControls.VioControl();
            this.tabPagePLATFORMS = new System.Windows.Forms.TabPage();
            this.tabControlPlatforms = new System.Windows.Forms.TabControl();
            this.tabPagePlatform = new System.Windows.Forms.TabPage();
            this.tabPageDEVICES = new System.Windows.Forms.TabPage();
            this.tabControlDevices = new System.Windows.Forms.TabControl();
            this.tabPageDevice = new System.Windows.Forms.TabPage();
            this.tabPageSETTINGS = new System.Windows.Forms.TabPage();
            this.propertyGridSettings = new System.Windows.Forms.PropertyGrid();
            this.tabPageMachine = new System.Windows.Forms.TabPage();
            this.tabControlMachine = new System.Windows.Forms.TabControl();
            this.tabPageScriptTasks = new System.Windows.Forms.TabPage();
            this.tabControlScripts = new System.Windows.Forms.TabControl();
            this.tabControlMain.SuspendLayout();
            this.tabPageIO.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPageCY.SuspendLayout();
            this.tabPageVIO.SuspendLayout();
            this.tabPagePLATFORMS.SuspendLayout();
            this.tabControlPlatforms.SuspendLayout();
            this.tabPageDEVICES.SuspendLayout();
            this.tabControlDevices.SuspendLayout();
            this.tabPageSETTINGS.SuspendLayout();
            this.tabPageMachine.SuspendLayout();
            this.tabPageScriptTasks.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabPageIO);
            this.tabControlMain.Controls.Add(this.tabPageCY);
            this.tabControlMain.Controls.Add(this.tabPageVIO);
            this.tabControlMain.Controls.Add(this.tabPagePLATFORMS);
            this.tabControlMain.Controls.Add(this.tabPageDEVICES);
            this.tabControlMain.Controls.Add(this.tabPageSETTINGS);
            this.tabControlMain.Controls.Add(this.tabPageMachine);
            this.tabControlMain.Controls.Add(this.tabPageScriptTasks);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.ItemSize = new System.Drawing.Size(60, 40);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(800, 450);
            this.tabControlMain.TabIndex = 0;
            // 
            // tabPageIO
            // 
            this.tabPageIO.Controls.Add(this.splitContainer1);
            this.tabPageIO.Location = new System.Drawing.Point(4, 44);
            this.tabPageIO.Name = "tabPageIO";
            this.tabPageIO.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageIO.Size = new System.Drawing.Size(792, 402);
            this.tabPageIO.TabIndex = 0;
            this.tabPageIO.Text = "输入输出";
            this.tabPageIO.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.diControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.doControl1);
            this.splitContainer1.Size = new System.Drawing.Size(786, 396);
            this.splitContainer1.SplitterDistance = 350;
            this.splitContainer1.TabIndex = 0;
            // 
            // diControl1
            // 
            this.diControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.diControl1.Location = new System.Drawing.Point(0, 0);
            this.diControl1.Name = "diControl1";
            this.diControl1.Size = new System.Drawing.Size(350, 396);
            this.diControl1.TabIndex = 0;
            // 
            // doControl1
            // 
            this.doControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.doControl1.Location = new System.Drawing.Point(0, 0);
            this.doControl1.Name = "doControl1";
            this.doControl1.Size = new System.Drawing.Size(432, 396);
            this.doControl1.TabIndex = 0;
            // 
            // tabPageCY
            // 
            this.tabPageCY.Controls.Add(this.cylinderControl1);
            this.tabPageCY.Location = new System.Drawing.Point(4, 44);
            this.tabPageCY.Name = "tabPageCY";
            this.tabPageCY.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCY.Size = new System.Drawing.Size(792, 402);
            this.tabPageCY.TabIndex = 1;
            this.tabPageCY.Text = "气缸";
            this.tabPageCY.UseVisualStyleBackColor = true;
            // 
            // cylinderControl1
            // 
            this.cylinderControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cylinderControl1.Location = new System.Drawing.Point(3, 3);
            this.cylinderControl1.Name = "cylinderControl1";
            this.cylinderControl1.Size = new System.Drawing.Size(786, 396);
            this.cylinderControl1.TabIndex = 0;
            // 
            // tabPageVIO
            // 
            this.tabPageVIO.Controls.Add(this.vioControl1);
            this.tabPageVIO.Location = new System.Drawing.Point(4, 44);
            this.tabPageVIO.Name = "tabPageVIO";
            this.tabPageVIO.Size = new System.Drawing.Size(792, 402);
            this.tabPageVIO.TabIndex = 3;
            this.tabPageVIO.Text = "交互信号";
            this.tabPageVIO.UseVisualStyleBackColor = true;
            // 
            // vioControl1
            // 
            this.vioControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vioControl1.Location = new System.Drawing.Point(0, 0);
            this.vioControl1.Name = "vioControl1";
            this.vioControl1.Size = new System.Drawing.Size(792, 402);
            this.vioControl1.TabIndex = 0;
            // 
            // tabPagePLATFORMS
            // 
            this.tabPagePLATFORMS.Controls.Add(this.tabControlPlatforms);
            this.tabPagePLATFORMS.Location = new System.Drawing.Point(4, 44);
            this.tabPagePLATFORMS.Name = "tabPagePLATFORMS";
            this.tabPagePLATFORMS.Size = new System.Drawing.Size(792, 402);
            this.tabPagePLATFORMS.TabIndex = 2;
            this.tabPagePLATFORMS.Text = "平台运动";
            this.tabPagePLATFORMS.UseVisualStyleBackColor = true;
            // 
            // tabControlPlatforms
            // 
            this.tabControlPlatforms.Controls.Add(this.tabPagePlatform);
            this.tabControlPlatforms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPlatforms.ItemSize = new System.Drawing.Size(60, 30);
            this.tabControlPlatforms.Location = new System.Drawing.Point(0, 0);
            this.tabControlPlatforms.Name = "tabControlPlatforms";
            this.tabControlPlatforms.SelectedIndex = 0;
            this.tabControlPlatforms.Size = new System.Drawing.Size(792, 402);
            this.tabControlPlatforms.TabIndex = 0;
            // 
            // tabPagePlatform
            // 
            this.tabPagePlatform.Location = new System.Drawing.Point(4, 34);
            this.tabPagePlatform.Name = "tabPagePlatform";
            this.tabPagePlatform.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePlatform.Size = new System.Drawing.Size(784, 364);
            this.tabPagePlatform.TabIndex = 0;
            this.tabPagePlatform.Text = "平台";
            this.tabPagePlatform.UseVisualStyleBackColor = true;
            // 
            // tabPageDEVICES
            // 
            this.tabPageDEVICES.Controls.Add(this.tabControlDevices);
            this.tabPageDEVICES.Location = new System.Drawing.Point(4, 44);
            this.tabPageDEVICES.Name = "tabPageDEVICES";
            this.tabPageDEVICES.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDEVICES.Size = new System.Drawing.Size(792, 402);
            this.tabPageDEVICES.TabIndex = 6;
            this.tabPageDEVICES.Text = "设备组件";
            this.tabPageDEVICES.UseVisualStyleBackColor = true;
            // 
            // tabControlDevices
            // 
            this.tabControlDevices.Controls.Add(this.tabPageDevice);
            this.tabControlDevices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDevices.ItemSize = new System.Drawing.Size(60, 30);
            this.tabControlDevices.Location = new System.Drawing.Point(3, 3);
            this.tabControlDevices.Name = "tabControlDevices";
            this.tabControlDevices.SelectedIndex = 0;
            this.tabControlDevices.Size = new System.Drawing.Size(786, 396);
            this.tabControlDevices.TabIndex = 1;
            // 
            // tabPageDevice
            // 
            this.tabPageDevice.Location = new System.Drawing.Point(4, 34);
            this.tabPageDevice.Name = "tabPageDevice";
            this.tabPageDevice.Size = new System.Drawing.Size(778, 358);
            this.tabPageDevice.TabIndex = 0;
            this.tabPageDevice.Text = "组件";
            this.tabPageDevice.UseVisualStyleBackColor = true;
            // 
            // tabPageSETTINGS
            // 
            this.tabPageSETTINGS.Controls.Add(this.propertyGridSettings);
            this.tabPageSETTINGS.Location = new System.Drawing.Point(4, 44);
            this.tabPageSETTINGS.Name = "tabPageSETTINGS";
            this.tabPageSETTINGS.Size = new System.Drawing.Size(792, 402);
            this.tabPageSETTINGS.TabIndex = 4;
            this.tabPageSETTINGS.Text = "配置参数";
            this.tabPageSETTINGS.UseVisualStyleBackColor = true;
            // 
            // propertyGridSettings
            // 
            this.propertyGridSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridSettings.Location = new System.Drawing.Point(0, 0);
            this.propertyGridSettings.Name = "propertyGridSettings";
            this.propertyGridSettings.Size = new System.Drawing.Size(792, 402);
            this.propertyGridSettings.TabIndex = 0;
            // 
            // tabPageMachine
            // 
            this.tabPageMachine.Controls.Add(this.tabControlMachine);
            this.tabPageMachine.Location = new System.Drawing.Point(4, 44);
            this.tabPageMachine.Name = "tabPageMachine";
            this.tabPageMachine.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMachine.Size = new System.Drawing.Size(792, 402);
            this.tabPageMachine.TabIndex = 5;
            this.tabPageMachine.Text = "设备信息";
            this.tabPageMachine.UseVisualStyleBackColor = true;
            // 
            // tabControlMachine
            // 
            this.tabControlMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMachine.Location = new System.Drawing.Point(3, 3);
            this.tabControlMachine.Name = "tabControlMachine";
            this.tabControlMachine.SelectedIndex = 0;
            this.tabControlMachine.Size = new System.Drawing.Size(786, 396);
            this.tabControlMachine.TabIndex = 0;
            // 
            // tabPageScriptTasks
            // 
            this.tabPageScriptTasks.Controls.Add(this.tabControlScripts);
            this.tabPageScriptTasks.Location = new System.Drawing.Point(4, 44);
            this.tabPageScriptTasks.Name = "tabPageScriptTasks";
            this.tabPageScriptTasks.Size = new System.Drawing.Size(792, 402);
            this.tabPageScriptTasks.TabIndex = 7;
            this.tabPageScriptTasks.Text = "任务管理";
            this.tabPageScriptTasks.UseVisualStyleBackColor = true;
            // 
            // tabControlScripts
            // 
            this.tabControlScripts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlScripts.Location = new System.Drawing.Point(0, 0);
            this.tabControlScripts.Name = "tabControlScripts";
            this.tabControlScripts.SelectedIndex = 0;
            this.tabControlScripts.Size = new System.Drawing.Size(792, 402);
            this.tabControlScripts.TabIndex = 1;
            // 
            // MainConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlMain);
            this.Name = "MainConfigForm";
            this.Text = "配置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.MainConfigForm_Load);
            this.tabControlMain.ResumeLayout(false);
            this.tabPageIO.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPageCY.ResumeLayout(false);
            this.tabPageVIO.ResumeLayout(false);
            this.tabPagePLATFORMS.ResumeLayout(false);
            this.tabControlPlatforms.ResumeLayout(false);
            this.tabPageDEVICES.ResumeLayout(false);
            this.tabControlDevices.ResumeLayout(false);
            this.tabPageSETTINGS.ResumeLayout(false);
            this.tabPageMachine.ResumeLayout(false);
            this.tabPageScriptTasks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageIO;
        private System.Windows.Forms.TabPage tabPageCY;
        private System.Windows.Forms.TabPage tabPagePLATFORMS;
        private System.Windows.Forms.TabPage tabPageVIO;
        private System.Windows.Forms.TabPage tabPageSETTINGS;
        private System.Windows.Forms.TabControl tabControlPlatforms;
        private System.Windows.Forms.TabPage tabPagePlatform;
        private Automation.FrameworkExtension.userControls.VioControl vioControl1;
        private Automation.FrameworkExtension.userControls.CylinderControl cylinderControl1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Automation.FrameworkExtension.userControls.DiControl diControl1;
        private Automation.FrameworkExtension.userControls.DoControl doControl1;
        private System.Windows.Forms.PropertyGrid propertyGridSettings;
        private System.Windows.Forms.TabPage tabPageMachine;
        private System.Windows.Forms.TabControl tabControlMachine;
        private System.Windows.Forms.TabPage tabPageDEVICES;
        private System.Windows.Forms.TabControl tabControlDevices;
        private System.Windows.Forms.TabPage tabPageDevice;
        private System.Windows.Forms.TabPage tabPageScriptTasks;
        private System.Windows.Forms.TabControl tabControlScripts;
    }
}