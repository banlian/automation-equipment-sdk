using System;
using System.IO;
using System.Windows.Forms;
using Automation.Base.VirtualCardLibrary;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.frameworkManage;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;
using DemoMachine.Machine.Tasks;

namespace DemoMachine.Machine
{

    public class DemoMachineSettings : UserSettings<DemoMachineSettings>
    {

        public string Name { get; set; }


        public override bool CheckIfNormal()
        {
            return true;
        }
    }

    public class DemoMachine : StateMachine
    {
        #region singleton
        private DemoMachine()
        {
        }
        public static DemoMachine Ins { get; } = new DemoMachine();
        #endregion



        public MotionCardWrapper Motion1;
        public MotionCardWrapper VIO;


        public DemoMachineSettings Settings;



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

            //try
            //{
            //    Import();
            //}
            //catch (Exception ex)
            //{

            //    MessageBox.Show($"导入设备参数失败：{ex.Message}");
            //}



            //load drivers
            Motion1 = new MotionCardWrapper(new VirtualCard());
            VIO = new MotionCardWrapper(new VirtualCard());

            MotionExs.Add(1, Motion1);
            MotionExs.Add(2, VIO);

            //load di do axis

            DiExs.Add(1, new DiEx() { Driver = Motion1 });

            DoExs.Add(1, new DoEx() { Driver = Motion1 });

            CylinderExs.Add(1, new CylinderEx() { Driver1 = Motion1, Driver2 = Motion1 });

            AxisExs.Add(1, new AxisEx() { Driver = Motion1 });

            //load station task
            var station1 = new Station(1, "Station1", this);
            var testTask1 = new TestTask1(1, "Test1", station1);

            //bind signals
            if (!FrameworkManager.IsSimulate)
            {
                // todo : to add signal configs
                //estop
                DiEstop.Add(2, new DiEx() { Driver = Motion1 });

                //start/stop/reset button
                DiStart.Add(1, new DiEx() { Driver = Motion1 });
                DiStop.Add(1, new DiEx() { Driver = Motion1 });
                DiReset.Add(1, new DiEx() { Driver = Motion1 });

                //start/stop/reset button lamp
                DoLightGreen.Add(1, new DoEx() { Driver = Motion1 });
                DoLightRed.Add(1, new DoEx() { Driver = Motion1 });
                DoLightYellow.Add(1, new DoEx() { Driver = Motion1 });

                //lamp
                DoLightGreen.Add(2, new DoEx() { Driver = Motion1 });
                DoLightRed.Add(2, new DoEx() { Driver = Motion1 });
                DoLightYellow.Add(2, new DoEx() { Driver = Motion1 });
                DoBuzzer.Add(1, new DoEx() { Driver = Motion1 });


                //station pause signals
                Stations[1].PauseSignals.Add(1, new DiEx() { Driver = Motion1 });
            }
        }


        public override void Save()
        {
            //save settings
            Settings.Save(@".\Config\Settings.cfg");

            try
            {
                Export();
            }
            catch (Exception ex)
            {

                MessageBox.Show($"导出设备参数失败：{ex.Message}");
            }



        }



        public override void Initialize()
        {
            //user define Initialize
            // todo



            //-------------------------------------
            //base start main thread, signals check
            //base.Initialize();
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
