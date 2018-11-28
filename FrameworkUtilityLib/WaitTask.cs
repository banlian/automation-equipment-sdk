using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace MachineUtilityLib
{
    public static class WaitTask
    {
        public static bool WaitResetFinish(this StationTask curTask, StationTask waitTask)
        {
            //wait measure task
            while (waitTask.State != TaskState.WaitRun || waitTask.State != TaskState.Running)
            {
                curTask.AbortIfCancel("cancel wait tasks");
                System.Threading.Thread.Sleep(1);
            }

            return true;
        }
    }
}
