namespace Automation.FrameworkScriptExtension.FrameworkScript
{
    partial class PyScriptControl
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
            this.scintilla = new ScintillaNET.Scintilla();
            this.groupBoxScript = new System.Windows.Forms.GroupBox();
            this.buttonSaveScript = new System.Windows.Forms.Button();
            this.groupBoxScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // scintilla
            // 
            this.scintilla.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintilla.Location = new System.Drawing.Point(6, 63);
            this.scintilla.Name = "scintilla";
            this.scintilla.Size = new System.Drawing.Size(788, 531);
            this.scintilla.TabIndex = 0;
            this.scintilla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.scintilla_KeyPress);
            // 
            // groupBoxScript
            // 
            this.groupBoxScript.Controls.Add(this.buttonSaveScript);
            this.groupBoxScript.Controls.Add(this.scintilla);
            this.groupBoxScript.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxScript.Location = new System.Drawing.Point(0, 0);
            this.groupBoxScript.Name = "groupBoxScript";
            this.groupBoxScript.Size = new System.Drawing.Size(800, 600);
            this.groupBoxScript.TabIndex = 1;
            this.groupBoxScript.TabStop = false;
            this.groupBoxScript.Text = "TaskName";
            // 
            // buttonSaveScript
            // 
            this.buttonSaveScript.Location = new System.Drawing.Point(6, 20);
            this.buttonSaveScript.Name = "buttonSaveScript";
            this.buttonSaveScript.Size = new System.Drawing.Size(104, 37);
            this.buttonSaveScript.TabIndex = 1;
            this.buttonSaveScript.Text = "保存";
            this.buttonSaveScript.UseVisualStyleBackColor = true;
            this.buttonSaveScript.Click += new System.EventHandler(this.buttonSaveScript_Click);
            // 
            // PyScriptControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxScript);
            this.Name = "PyScriptControl";
            this.Size = new System.Drawing.Size(800, 600);
            this.Load += new System.EventHandler(this.PyScriptControl_Load);
            this.groupBoxScript.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScintillaNET.Scintilla scintilla;
        private System.Windows.Forms.GroupBox groupBoxScript;
        private System.Windows.Forms.Button buttonSaveScript;
    }
}
