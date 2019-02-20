using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Automation.Base.VirtualCardLibrary;
using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.frameworkManage;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;
using DemoMachine.Machine.Tasks;

namespace DemoMachine.Machine
{
    public class DemoMachine : StateMachine
    {
        #region singleton

        private DemoMachine()
        {
        }

        public static DemoMachine Ins { get; } = new DemoMachine();

        #endregion

        public DemoMachineSettings Settings { get; set; }

        public override void Load()
        {
            if (!Directory.Exists(@".\Config"))
            {
                Directory.CreateDirectory(@".\Config");
            }


            //load all settings!!!
            try
            {
                Settings = DemoMachineSettings.Load(@".\Config\Settings.cfg");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载配置文件失败：{ex.Message}");
            }

            if (Settings == null)
            {
                Settings = new DemoMachineSettings();
                Settings.Save(@".\Config\Settings.cfg");
            }

            try
            {
                Import();

                OutputScriptResource();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导入设备参数失败：{ex.Message}");
            }
        }

        private void OutputScriptResource()
        {
            using (var fs = new FileStream(@".\Scripts\resource.py", FileMode.OpenOrCreate))
            {
                using (var sw = new StreamWriter(fs, Encoding.UTF8))
                {
                    sw.WriteLine("DiExs");
                    foreach (var obj in this.DiExs)
                    {
                        sw.WriteLine(obj.Value.Name);
                    }

                    sw.WriteLine();
                    sw.WriteLine("DoExs");
                    foreach (var obj in this.DoExs)
                    {
                        sw.WriteLine(obj.Value.Name);
                    }

                    sw.WriteLine();
                    sw.WriteLine("VioExs");
                    foreach (var obj in this.VioExs)
                    {
                        sw.WriteLine(obj.Value.Name);
                    }

                    sw.WriteLine();
                    sw.WriteLine("CylinderExs");
                    foreach (var obj in this.CylinderExs)
                    {
                        sw.WriteLine(obj.Value.Name);
                    }

                    sw.WriteLine();
                    sw.WriteLine("AxisExs");
                    foreach (var obj in this.AxisExs)
                    {
                        sw.WriteLine(obj.Value.Name);
                    }

                    sw.WriteLine();
                    sw.WriteLine("Platforms");
                    foreach (var obj in this.Platforms)
                    {
                        sw.WriteLine(obj.Value.Name);
                    }
                }
            }
        }

        public override void Save()
        {
            //save settings
            Settings.Save(@".\Config\Settings.cfg");

            try
            {
                //Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导出设备参数失败：{ex.Message}");
            }
        }


        private void _import_test()
        {
            {
                //load drivers
                var Motion1 = new MotionCardWrapper(new VirtualCard());
                var VIO = new MotionCardWrapper(new VirtualCard());

                MotionExs.Add(1, Motion1);
                MotionExs.Add(2, VIO);

                //load di do axis

                DiExs.Add(1, new DiEx {Driver = Motion1});

                DoExs.Add(1, new DoEx {Driver = Motion1});

                CylinderExs.Add(1, new CylinderEx {Driver1 = Motion1, Driver2 = Motion1});

                AxisExs.Add(1, new AxisEx {Driver = Motion1});

                //load station task
                var station1 = new Station(1, "Station1", this);
                var testTask1 = new TestTask1(1, "Test1", station1);

                //bind signals
                if (!FrameworkManager.IsSimulate)
                {
                    // todo : to add signal configs
                    //estop
                    DiEstop.Add(2, new DiEx {Driver = Motion1});

                    //start/stop/reset button
                    DiStart.Add(1, new DiEx {Driver = Motion1});
                    DiStop.Add(1, new DiEx {Driver = Motion1});
                    DiReset.Add(1, new DiEx {Driver = Motion1});

                    //start/stop/reset button lamp
                    DoLightGreen.Add(1, new DoEx {Driver = Motion1});
                    DoLightRed.Add(1, new DoEx {Driver = Motion1});
                    DoLightYellow.Add(1, new DoEx {Driver = Motion1});

                    //lamp
                    DoLightGreen.Add(2, new DoEx {Driver = Motion1});
                    DoLightRed.Add(2, new DoEx {Driver = Motion1});
                    DoLightYellow.Add(2, new DoEx {Driver = Motion1});
                    DoBuzzer.Add(1, new DoEx {Driver = Motion1});


                    //station pause signals
                    Stations[1].PauseSignals.Add(1, new DiEx {Driver = Motion1});
                }
            }
        }


        public override void Initialize()
        {
            //user define Initialize
            // todo


            //-------------------------------------
            //base start main thread, signals check
            base.Initialize();
        }


        public override void Terminate()
        {
            //base stop main thread first
            base.Terminate();
            //-------------------------------------

            //user define Terminate
            // todo
        }
    }
}