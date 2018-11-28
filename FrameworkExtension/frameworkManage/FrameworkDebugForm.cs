using System;
using System.Drawing;
using System.Windows.Forms;

namespace Automation.FrameworkExtension.frameworkManage
{
    public partial class FrameworkDebugForm : Form
    {
        public FrameworkDebugForm()
        {
            InitializeComponent();
        }

        private void FrameworkDebugForm_Load(object sender, EventArgs e)
        {
            Location = new Point(0, 0);
        }


        public void UpdateLog(string log)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string>(UpdateLog), log);
            }
            else
            {
                richTextBox1.AppendText($"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}: {log}\r\n");
            }
        }
    }
}