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


        private MainMachineForm _machineForm = new MainMachineForm();
        private MainConfigForm _configForm = new MainConfigForm();

        private UserLogForm _logForm = new UserLogForm();
        private UserAlarmForm _alarmForm = new UserAlarmForm();
        private void MainForm_Load(object sender, EventArgs e)
        {
            _machineForm.Show(dockPanel1, DockState.Document);
            _logForm.Show(dockPanel1, DockState.Document);
            _alarmForm.Show(dockPanel1, DockState.Document);
            _configForm.Show(dockPanel1, DockState.Document);

            foreach (var stationTask in Machine.DemoMachine.Ins.Tasks)
            {
                stationTask.Value.LogEvent += (s, level) =>
                {
                    _logForm.UpdateLog(stationTask.Value.Name, s, level);
                };
                stationTask.Value.Log(string.Empty);
            }


            _machineForm.Activate();
        }

        private void machineToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _machineForm.Show(dockPanel1, DockState.Document);
            _machineForm.Activate();
        }
        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _configForm.Show(dockPanel1, DockState.Document);
            _configForm.Activate();
        }
        private void alarmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _alarmForm.Show(dockPanel1, DockState.Document);
            _alarmForm.Activate();
        }
        private void logToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _logForm.Show(dockPanel1, DockState.Document);
            _logForm.Activate();
        }

        private void versionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var vf = new VersionForm();
            vf.ShowDialog();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
