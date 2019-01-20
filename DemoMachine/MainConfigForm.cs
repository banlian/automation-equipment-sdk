using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkExtension.platforms.motionPlatforms;
using Automation.FrameworkExtension.stateMachine;
using WeifenLuo.WinFormsUI.Docking;

namespace DemoMachine
{
    public partial class MainConfigForm : DockContent
    {
        public MainConfigForm()
        {
            InitializeComponent();
        }

        private void MainConfigForm_Load(object sender, EventArgs e)
        {
            diControl1.DiExs = Machine.DemoMachine.Ins.DiExs.Values.ToList();
            diControl1.LoadFramework();
            diControl1.FrameworkActivate();


            doControl1.DoExs = Machine.DemoMachine.Ins.DoExs.Values.ToList();
            doControl1.LoadFramework();
            doControl1.FrameworkActivate();

            cylinderControl1.CyExs = Machine.DemoMachine.Ins.CylinderExs.Values.ToList();
            cylinderControl1.LoadFramework();
            cylinderControl1.FrameworkActivate();


            vioControl1.VioExs = Machine.DemoMachine.Ins.VioExs.Values.ToList();
            vioControl1.LoadFramework();
            vioControl1.FrameworkActivate();


            tabControlPlatforms.TabPages.Clear();


            foreach (var p in Machine.DemoMachine.Ins.Platforms.Values)
            {
                var platformControl = new PlatformControl()
                {
                    Dock = DockStyle.Fill,
                };
                platformControl.LoadPlatform(p);

                tabControlPlatforms.TabPages.Add(new TabPage($"{p.Name} {p.Description}")
                {
                    Controls =
                    {
                        platformControl,
                    }
                });
            }

            tabControlMachine.TabPages.Clear();
            foreach (var s in Machine.DemoMachine.Ins.Stations)
            {
                tabControlMachine.TabPages.Add(new TabPage($"{s.Value.Name} {s.Value.Description}")
                {
                    Controls =
                    {
                        new StationStateControl()
                        {
                            Dock = DockStyle.Fill,
                            Station = s.Value,
                            Machine = s.Value.Machine
                        },
                    }
                });

            }


            propertyGridSettings.SelectedObject = Machine.DemoMachine.Ins.Settings;
            propertyGridSettings.ExpandAllGridItems();
        }
    }
}
