using System;
using System.Windows.Forms;
using Automation.FrameworkExtension.deviceDriver;
using Automation.FrameworkExtension.elementsInterfaces;
using Automation.FrameworkExtension.frameworkManage;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.motionDriver
{
    public class MotionCardWrapper : IDevice
    {
        public int Id { get; set; }
        public string Name { get; set; } = nameof(MotionCardWrapper);
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string ConfigFilePath { get; set; } = nameof(ConfigFilePath);

        public IMotionCard Motion { get; protected set; }

        public MotionCardWrapper()
        {

        }

        public MotionCardWrapper(IMotionCard motion)
        {
            if (motion == null)
            {
                return;
            }

            Name = (motion)?.Name;
            Id = motion.Id;
            Motion = motion;
        }


        public bool Initialize()
        {
            Motion.Initialize();
            Motion.LoadParams(ConfigFilePath);

            return true;
        }

        public bool Terminate()
        {
            Motion.Terminate();
            return true;
        }

        public UserControl CreateDeviceControl()
        {
            return null;
        }

        #region io operations
        public void SetDo(int port, int status)
        {
            Motion.SetDo(Id, port, status);
        }

        public void GetDo(int port, out int status)
        {
            Motion.GetDo(Id, port, out status);
        }

        public void GetDi(int port, out int status)
        {
            Motion.GetDi(Id, port, out status);
        }

        #endregion


        #region motion status

        
        public int GetEncPos(int axis, ref int pos)
        {
            double p = 0;
            var ret = Motion.GetEncPos(Id, axis, ref p);
            pos = (int)p;
            return ret;
        }

        public int SetEncPos(int axis, int pos)
        {
            return Motion.SetEncPos(Id, axis, pos);
        }

        public int GetCommandPos(int axis, ref int pos)
        {
            double p = 0;
            var ret = Motion.GetCmdPos(Id, axis, ref p);
            pos = (int)p;
            return ret;
        }

        public int SetCommandPos(int axis, int pos)
        {
            Motion.SetCmdPos(Id, axis, pos);
            return 0;
        }

        #endregion


        #region motion

        

        public void ServoEnable(int axis, bool enable)
        {
            Motion.Servo(Id, axis, enable);
        }

        public void MoveAbs(int axis, int pos, int vel)
        {
            Motion.AxisAbsMove(Id, axis, pos, vel);
        }

        public void MoveRel(int axis, int step, int vel)
        {
            Motion.AxisRelMove(Id, axis, step, vel);
        }

        public bool CheckMoveDone(int axis)
        {
            return Motion.IsAxisStop(Id, axis);
        }

        public void MoveStop(int axis)
        {
            Motion.AxisStop(Id, axis);
        }


        public void Home(int axis, int vel)
        {
            Motion.SetHomeVel(Id, axis, vel);
            Motion.AxisHomeMove(Id, axis);
        }

        public bool CheckHomeDone(int axis)
        {
            return !Motion.IsAxisHmv(Id, axis) && Motion.IsAxisStop(Id, axis);
        }

        #endregion

        #region axis status

        
        public bool GetAxisEnable(int axis)
        {
            return Motion.IsAxisServo(Id, axis);
        }

        public bool GetAxisAlarm(int axis)
        {
            return Motion.IsAxisAlarm(Id, axis);
        }

        public bool GetAxisEmg(int axis)
        {
            return Motion.IsAxisEmg(Id, axis);
        }

        public bool GetAxisDone(int axis)
        {
            return Motion.IsAxisStop(Id, axis);
        }

        public bool GetAxisAstp(int axis)
        {
            return Motion.IsAxisAstp(Id, axis);
        }

        public bool GetAxisInp(int axis)
        {
            return Motion.IsAxisInp(Id, axis);
        }
        public bool CheckLimit(int axis)
        {
            return Motion.IsAxisMel(Id, axis) || Motion.IsAxisPel(Id, axis);
        }

        public bool LimitMel(int axis)
        {
            return Motion.IsAxisMel(Id, axis);

        }
        public bool LimitPel(int axis)
        {
            return Motion.IsAxisPel(Id, axis);
        }

        public bool LimitOrg(int axis)
        {
            return Motion.IsAxisOrg(Id, axis);
        }

        #endregion

        public override string ToString()
        {
            return $"{Name} {Id} {Motion.GetType().Name} {ConfigFilePath}";
        }

        public string Export()
        {
            return $"{Name} {Id} {Motion.GetType().Name} {ConfigFilePath}";
        }

        public void Import(string line, StateMachine machine)
        {
            var data = line.Split(' ');
            int i = 0;
            var id = int.Parse(data[i++]);

            Name = data[i++];

            Id = int.Parse(data[i++]);
            var typeName = data[i++];
            var configFile = data[i++];


            ConfigFilePath = configFile;

            //load motion card
            var motioncard = Activator.CreateInstance(FrameworkManager.MotionCardTypes[typeName]) as IMotionCard;
            if (motioncard == null)
            {
                throw new Exception($"LOAD MOTION ERROR: {line}");
            }

            Motion = motioncard;

            if (!machine.MotionExs.ContainsKey(id))
            {
                machine.MotionExs.Add(id, this);
            }
        }

    }
}