using System;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.Base.MotionCardLibrary1
{
    public class MotionCard1 : IMotionCard
    {
        public string Name { get; set; }
        public int DeviceID { get; set; }
        public bool Initialize()
        {
            throw new NotImplementedException();
        }

        public bool Terminate()
        {
            throw new NotImplementedException();
        }

        public bool LoadParams(string configFileName, params object[] objects)
        {
            throw new NotImplementedException();
        }

        public bool ClearAlarm(int index, int i)
        {
            throw new NotImplementedException();
        }

        public int GetDo(int index, int port, out int status)
        {
            throw new NotImplementedException();
        }

        public int SetDo(int index, int port, int status)
        {
            throw new NotImplementedException();
        }

        public int GetDi(int index, int port, out int status)
        {
            throw new NotImplementedException();
        }

        public int SetDi(int index, int port, int status)
        {
            throw new NotImplementedException();
        }

        public int GetEncPos(int index, int axis, ref double d)
        {
            throw new NotImplementedException();
        }

        public int SetEncPos(int index, int axis, int pos)
        {
            throw new NotImplementedException();
        }

        public int GetCmdPos(int index, int axis, ref double d)
        {
            throw new NotImplementedException();
        }

        public int SetCmdPos(int index, int axis, int pos)
        {
            throw new NotImplementedException();
        }

        public int Servo(int index, int axis, bool enable)
        {
            throw new NotImplementedException();
        }

        public int AxisAbsMove(int index, int axis, int pos, int vel)
        {
            throw new NotImplementedException();
        }

        public int AxisRelMove(int index, int axis, int step, int vel)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisStop(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public int AxisStop(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public int SetHomeVel(int index, int axis, int vel)
        {
            throw new NotImplementedException();
        }

        public int AxisHomeMove(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisHmv(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisServo(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisAlarm(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisEmg(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisAstp(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisInp(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisMel(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisPel(int index, int axis)
        {
            throw new NotImplementedException();
        }

        public bool IsAxisOrg(int index, int axis)
        {
            throw new NotImplementedException();
        }
    }
}
