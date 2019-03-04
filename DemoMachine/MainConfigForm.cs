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
            HideOnClose = true;

            diControl1.DiExs = DemoMachine.Ins.DiExs.Values.ToList();
            diControl1.LoadFramework();
            diControl1.FrameworkActivate();


            doControl1.DoExs = DemoMachine.Ins.DoExs.Values.ToList();
            doControl1.LoadFramework();
            doControl1.FrameworkActivate();

            cylinderControl1.CyExs = DemoMachine.Ins.CylinderExs.Values.ToList();
            cylinderControl1.LoadFramework();
            cylinderControl1.FrameworkActivate();


            vioControl1.VioExs = DemoMachine.Ins.VioExs.Values.ToList();
            vioControl1.LoadFramework();
            vioControl1.FrameworkActivate();


            tabControlPlatforms.TabPages.Clear();

            foreach (var p in DemoMachine.Ins.Platforms.Values)
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



            tabControlDevices.TabPages.Clear();

            foreach (var p in DemoMachine.Ins.Devices.Values)
            {
                var uc = p.CreateDeviceControl();
                uc.Dock = DockStyle.Fill;

                tabControlDevices.TabPages.Add(new TabPage($"{p.Name} {p.Description}")
                {
                    Controls =
                    {
                        uc,
                    }
                });
            }


            tabControlMachine.TabPages.Clear();
            foreach (var s in DemoMachine.Ins.Stations)
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


            propertyGridSettings.SelectedObject = DemoMachine.Ins.Settings;
            propertyGridSettings.ExpandAllGridItems();
        }

        private void MainConfigForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
