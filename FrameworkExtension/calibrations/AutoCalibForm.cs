using System;
using System.Windows.Forms;
using Lead.Detect.Helper;

namespace Lead.Detect.FrameworkExtension.calibrations
{
    public partial class AutoCalibForm : Form
    {
        public AutoCalib Calib { get; set; }

        public AutoCalibForm()
        {
            InitializeComponent();
        }

        private void AutoCalibForm_Load(object sender, EventArgs e)
        {
            if (Calib != null)
            {
                textBoxCalibInfo.Text = Calib.CalibInfo;
                textBoxCalibInfo.ReadOnly = true;
                Calib.LogEvent += UpdateLog;
                Calib.ProgressEvent += UpdateProgress;

                propertyGridCalib.SelectedObject = Calib;
            }



            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
            progressBar1.Value = 0;

            _res = DialogResult.Cancel;
        }


        public void UpdateLog(string log, LogLevel level)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<string, LogLevel>(UpdateLog), log, level);
            }
            else
            {
                richTextBox1.AppendText($"{DateTime.Now.ToString("yyyyMMdd-HHmmss.fff")}:{level}:{log}\r\n");
                richTextBox1.ScrollToCaret();
            }
        }


        public void UpdateProgress(int progress)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new Action<int>(UpdateProgress), progress);
            }
            else
            {
                progressBar1.Value = progress;
            }
        }

        private void buttonStartCalib_Click(object sender, EventArgs e)
        {
            if (Calib == null)
            {
                MessageBox.Show("Calib not Loaded");
                return;
            }

            progressBar1.Value = 0;

            Calib.Start();

            _res = DialogResult.OK;
        }

        private DialogResult _res;
        private void buttonStopCalib_Click(object sender, EventArgs e)
        {
            if (Calib == null)
            {
                MessageBox.Show("Calib not Loaded");
                return;
            }
            Calib.Stop();

            _res = DialogResult.Cancel;
        }

        private void AutoCalibForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = _res;
        }
    }
}