using Lead.Detect.FrameworkExtension;
using Lead.Detect.FrameworkExtension.elementExtensionInterfaces;
using Lead.Detect.FrameworkExtension.stateMachine;

namespace MachineUtilityLib
{
    public class DualCarrierSensorCheck
    {

        public IDiEx DISensorCheck1;
        public IDiEx DISensorCheck2;

        public string ErrorMsg = "定位传感器检测异常";

        public bool Check(StationTask task)
        {
            //检查定位传感器
            if (!DISensorCheck1.GetDiSts() || !DISensorCheck2.GetDiSts())
            {
                task.Station.Machine.Beep();
                task.Log($"{task.Station.Name} - {task.Name} {ErrorMsg}", LogLevel.Warning);
                return false;
            }
            return true;
        }
    }
}