using System;

namespace Automation.FrameworkExtension.motionDriver
{
    public class MotionCardWrapper : IMotionWrapper
    {
        public dynamic Motion { get; }
        public int Id { get; }

        public int Index { get; }

        public string Name { get; }


        public MotionCardWrapper(IMotionCard motion)
        {
            if (motion == null)
            {
                return;
            }

            Index = motion.DeviceID;
            Motion = motion;
        }


        public void Init(string file)
        {
           
        }

        public void Uninit()
        {
          
        }


        public void SetDo(int port, int status)
        {
            Motion.SetDo(Index, 0, port, status);
        }

        public void GetDo(int port, out int status)
        {
            Motion.GetDo(Index, 0, port, out status);
        }

        public void GetDi(int port, out int status)
        {
            Motion.GetDi(Index, 0, port, out status);
        }


        public int GetEncPos(int axis, ref int pos)
        {
            double p = 0;
            var ret = Motion.GetAxisPositionF(axis, ref p);
            pos = (int)p;
            return ret;
        }

        public int SetEncPos(int axis, int pos)
        {
            return Motion.SetAxisPositionOrFeedbackPules(axis, pos);
        }

        public int GetCommandPos(int axis, ref int pos)
        {
            double p = 0;
            var ret = Motion.GetAxisCommandF(axis, ref p);
            pos = (int)p;
            return ret;
        }

        public int SetCommandPos(int axis, int pos)
        {
            Motion.SetAxisPositionOrFeedbackPules(axis, pos);
            return 0;
        }


        public void ServoEnable(int axis, bool enable)
        {
            Motion.AxisSetEnable(Index, axis, enable);
        }

        public void MoveAbs(int axis, int pos, int vel)
        {
            Motion.AxisAbsMove(Index, axis, pos, vel);
        }

        public void MoveRel(int axis, int step, int vel)
        {
            Motion.AxisRelMove(Index, axis, step, vel);
        }

        public bool CheckMoveDone(int axis)
        {
            return Motion.AxisIsStop(Index, axis);
        }

        public void MoveStop(int axis)
        {
            Motion.AxisStopMove(Index, axis);
        }


        public void Home(int axis, int vel)
        {
            Motion.AxisHomeMove(Index, axis);
        }

        public bool CheckHomeDone(int axis)
        {
            return !Motion.AxisHMV(Motion.DevIndex, axis) && Motion.AxisIsStop(Motion.DevIndex, axis);
        }

        public bool GetAxisEnable(int axis)
        {
            return Motion.AxisIsEnble(Index, axis);
        }

        public bool GetAxisAlarm(int axis)
        {
            return Motion.AxisIsAlarm(Index, axis) || Motion.AxisAstp(Index, axis);
        }

        public bool GetAxisEmg(int axis)
        {
            return Motion.AxisSingalEMG(Index, axis);
        }

        public bool GetAxisDone(int axis)
        {
            return Motion.AxisIsStop(Index, axis);
        }

        public bool CheckLimit(int axis)
        {
            return Motion.LimitMel(Motion.DevIndex, axis) || Motion.LimitPel(Motion.DevIndex, axis);
        }

        public bool LimitMel(int axis)
        {
            return Motion.LimitMel(Motion.DevIndex, axis);

        }
        public bool LimitPel(int axis)
        {
            return Motion.LimitPel(Motion.DevIndex, axis);
        }

        public override string ToString()
        {
            return $"{Name}:{Id}";
        }
    }
}