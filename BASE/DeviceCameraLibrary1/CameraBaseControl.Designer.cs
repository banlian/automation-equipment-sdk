namespace Automation.Base.DeviceCameraLibrary1
{
    partial class CameraBaseControl
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
            this.groupBoxDev = new System.Windows.Forms.GroupBox();
            this.pictureBoxImage = new System.Windows.Forms.PictureBox();
            this.buttonSnap = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.groupBoxDev.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxDev
            // 
            this.groupBoxDev.Controls.Add(this.pictureBoxImage);
            this.groupBoxDev.Controls.Add(this.buttonSnap);
            this.groupBoxDev.Controls.Add(this.buttonClose);
            this.groupBoxDev.Controls.Add(this.buttonOpen);
            this.groupBoxDev.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxDev.Location = new System.Drawing.Point(0, 0);
            this.groupBoxDev.Name = "groupBoxDev";
            this.groupBoxDev.Size = new System.Drawing.Size(534, 428);
            this.groupBoxDev.TabIndex = 2;
            this.groupBoxDev.TabStop = false;
            this.groupBoxDev.Text = "groupBoxDev";
            // 
            // pictureBoxImage
            // 
            this.pictureBoxImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxImage.Location = new System.Drawing.Point(6, 118);
            this.pictureBoxImage.Name = "pictureBoxImage";
            this.pictureBoxImage.Size = new System.Drawing.Size(525, 304);
            this.pictureBoxImage.TabIndex = 1;
            this.pictureBoxImage.TabStop = false;
            // 
            // buttonSnap
            // 
            this.buttonSnap.Location = new System.Drawing.Point(377, 20);
            this.buttonSnap.Name = "buttonSnap";
            this.buttonSnap.Size = new System.Drawing.Size(151, 40);
            this.buttonSnap.TabIndex = 0;
            this.buttonSnap.Text = "采集图像";
            this.buttonSnap.UseVisualStyleBackColor = true;
            this.buttonSnap.Click += new System.EventHandler(this.buttonSnap_Click);
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
            // CameraBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxDev);
            this.Name = "CameraBaseControl";
            this.Size = new System.Drawing.Size(534, 428);
            this.groupBoxDev.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxDev;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.PictureBox pictureBoxImage;
        private System.Windows.Forms.Button buttonSnap;
    }
}
