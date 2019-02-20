using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DemoMachine
{
    public partial class MainMachineForm : DockContent
    {
        public MainMachineForm()
        {
            InitializeComponent();
        }

        private void MainMachineForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonUserLogin_Click(object sender, EventArgs e)
        {

        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            Machine.DemoMachine.Ins.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            Machine.DemoMachine.Ins.Stop();
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            Machine.DemoMachine.Ins.Reset();
        }

        private void MainMachineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
