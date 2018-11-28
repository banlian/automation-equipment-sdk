using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Automation.FrameworkExtension.stateMachine;

namespace DemoMachine.Machine.Tasks
{
    class TestTask1 : StationTask
    {
        public TestTask1(int id, string name, Station station) : base(id, name, station)
        {
        }


        protected override int ResetLoop()
        {
            //when reset finish return -1
            return -1;
        }


        protected override int RunLoop()
        {
            //when error happens return -1, return 0 to run RunLoop again
            return -1;
        }
    }
}
