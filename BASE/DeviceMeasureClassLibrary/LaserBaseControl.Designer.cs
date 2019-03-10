namespace Automation.Base.DeviceMeasureClassLibrary
{
    partial class LaserBaseControl
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOpen = new System.Windows.Forms.Button();
            this.groupBoxDev = new System.Windows.Forms.GroupBox();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonTrigger = new System.Windows.Forms.Button();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.groupBoxDev.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(6, 20);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(151, 40);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "打开";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // groupBoxDev
            // 
            this.groupBoxDev.Controls.Add(this.richTextBoxLog);
            this.groupBoxDev.Controls.Add(this.buttonTrigger);
            this.groupBoxDev.Controls.Add(this.buttonClose);
            this.groupBoxDev.Controls.Add(this.buttonOpen);
            this.groupBoxDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDev.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDev.Name = "groupBoxDev";
            this.groupBoxDev.Size = new System.Drawing.Size(587, 399);
            this.groupBoxDev.TabIndex = 1;
            this.groupBoxDev.TabStop = false;
            this.groupBoxDev.Text = "groupBoxDev";
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(163, 20);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(151, 40);
            this.buttonClose.TabIndex = 0;
            this.buttonClose.Text = "关闭";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonTrigger
            // 
            this.buttonTrigger.Location = new System.Drawing.Point(430, 20);
            this.buttonTrigger.Name = "buttonTrigger";
            this.buttonTrigger.Size = new System.Drawing.Size(151, 40);
            this.buttonTrigger.TabIndex = 0;
            this.buttonTrigger.Text = "触发";
            this.buttonTrigger.UseVisualStyleBackColor = true;
            this.buttonTrigger.Click += new System.EventHandler(this.buttonTrigger_Click);
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxLog.Location = new System.Drawing.Point(7, 90);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.Size = new System.Drawing.Size(574, 303);
            this.richTextBoxLog.TabIndex = 1;
            this.richTextBoxLog.Text = "";
            this.richTextBoxLog.DoubleClick += new System.EventHandler(this.richTextBoxLog_DoubleClick);
            // 
            // LaserBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDev);
            this.Name = "LaserBaseControl";
            this.Size = new System.Drawing.Size(587, 399);
            this.groupBoxDev.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.GroupBox groupBoxDev;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonTrigger;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
    }
}
