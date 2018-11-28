using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace MachineUtilityLib
{
    public class DualStartButton
    {
        public DualStartButton()
        {

        }

        public IDiEx DIStart1;
        public IDiEx DIStart2;

        public bool WaitStart(StationTask task)
        {
            //wait start
            while ((!DIStart1.GetDiSts() || !DIStart2.GetDiSts()))
            {
                Thread.Sleep(100);
                task.JoinIfPause();
                task.AbortIfCancel("cancel trans wait start");

            }
            task.Station.ShowAlarm(string.Empty, LogLevel.None);

            return true;
        }
    }
}
