using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkUtilityLib.UtilViews;
using WeifenLuo.WinFormsUI.Docking;

namespace DemoMachine
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        private UserLogForm _logForm = new UserLogForm();
        private UserAlarmForm _alarmForm = new UserAlarmForm();

        private MainConfigForm _configForm = new MainConfigForm();
        private void MainForm_Load(object sender, EventArgs e)
        {
            _logForm.Show(dockPanel1, DockState.Document);
            _alarmForm.Show(dockPanel1, DockState.Document);

            _configForm.Show(dockPanel1, DockState.Document);

            _logForm.Activate();






        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vf = new VersionForm();
            vf.ShowDialog();
        }
    }
}
