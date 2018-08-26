using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation.FrameworkExtension;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.elements;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.platforms.calibrations;
using Automation.FrameworkExtension.stateMachine;
using DemoMachine.Machine.Tasks;
using MotionCardLibrary1;
using VirtualCardLibrary;

namespace DemoMachine.Machine
{

    public class DemoMachineSettings : UserSettings<DemoMachineSettings>
    {

        public string Name { get; set; }


    }

    public class DemoMachine : StateMachine
    {
        #region singleton

        private DemoMachine()
        {

        }

        public static DemoMachine Ins { get; } = new DemoMachine();


        #endregion



        public IMotionWrapper Motion1;

        public IMotionWrapper VIO;


        public DemoMachineSettings Settings;



        public void Load()
        {
            //load all settings!!!
            Settings = DemoMachineSettings.Load(@".\Config\Settings.cfg");
            if (Settings == null)
            {
                throw new Exception("Load MachineSettings Fail!");
            }




            //load drivers
            Motion1 = new MotionCardWrapper(new MotionCard1());

            VIO = new MotionCardWrapper(new VirtualCard());



            //load di do axis

            DiExs.Add(1, new DiImpl());

            DoExs.Add(1, new DoImpl());

            CylinderExs.Add(1, new CyImpl());

            AxisExs.Add(1, new AxisImpl());

            //load station task
            Staitons.Add(1, new Station(1, "Station1", this));
            Tasks.Add(1, new TestTask1(1, "Test1", Staitons[1]));

            //bind signals
            if (!FrameworkExtenion.IsSimulate)
            {
                // todo : to add signal configs
                //estop
                DiEstop.Add(2, new DiImpl());

                //start/stop/reset button
                DiStart.Add(1, new DiImpl());
                DiStop.Add(1, new DiImpl());
                DiReset.Add(1, new DiImpl());

                //start/stop/reset button lamp
                DoLightGreen.Add(1, new DoImpl());
                DoLightRed.Add(1, new DoImpl());
                DoLightYellow.Add(1, new DoImpl());

                //lamp
                DoLightGreen.Add(2, new DoImpl());
                DoLightRed.Add(2, new DoImpl());
                DoLightYellow.Add(2, new DoImpl());
                DoBuzzer.Add(1, new DoImpl());


                //station pause signals
                Stations[1].PauseSignals.Add(1, new DiImpl());
            }
        }


        public void Save()
        {
            //save settings
            Settings.Save(@".\Config\Settings.cfg");

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
