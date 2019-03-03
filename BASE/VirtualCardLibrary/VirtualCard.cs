using System;
using System.Xml.Serialization;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;
using Timer = System.Threading.Timer;

namespace Automation.Base.VirtualCardLibrary
{
    [Serializable]
    public class VirtualCard : IMotionCard
    {
        public int Id { get; set; }


        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string ConfigFilePath { get; set; }

        private readonly AxisStatus[] _axisStatus;


        [XmlIgnore]
        public int[] Di { get; }

        [XmlIgnore]
        public int[] Do { get; }


        private Timer _timer;

        public VirtualCard()
        {
            Di = new int[32];
            Do = new int[32];

            _axisStatus = new AxisStatus[16];
            _timer = new Timer(OnTimer, null, 0, 100);
        }

        private void OnTimer(object state)
        {
            lock (this)
            {
                //motion simulate update
                for (var i = 0; i < _axisStatus.Length; i++)
                {
                    _axisStatus[i].Update();
                }
            }
        }


        public string Export()
        {
            throw new NotImplementedException();
        }

        public void Import(string line, StateMachine machine)
        {
            throw new NotImplementedException();
        }

        public bool Initialize()
        {
            return true;
        }

        public bool Terminate()
        {
            return true;
        }

        public bool LoadParams(string configFileName, params object[] objects)
        {
            return true;
        }

        public bool ClearAlarm(int index, int i)
        {
            lock (this)
            {
                _axisStatus[i].Alarm = false;
            }

            return true;
        }


        public int Servo(int id, int axis, bool sts)
        {
            lock (this)
            {
                _axisStatus[axis].Servo = sts;
                return 0;
            }
        }


        public int SetCmdPos(int id, int axis, int pos)
        {
            lock (this)
            {
                _axisStatus[axis].CommandPos = pos;
                return 0;
            }
        }

        public int GetCmdPos(int id, int axis, ref double pos)
        {
            lock (this)
            {
                pos = _axisStatus[axis].CommandPos;
                return 0;
            }
        }

        public int SetEncPos(int id, int axis, int encpos)
        {
            lock (this)
            {
                _axisStatus[axis].CurPos = encpos;
                return 0;
            }
        }

        public int GetEncPos(int id, int axis, ref double pos)
        {
            lock (this)
            {
                pos = _axisStatus[axis].CurPos;
                return 0;
            }
        }


        public int AxisAbsMove(int id, int axis, int pos, int vel)
        {
            lock (this)
            {
                if (_axisStatus[axis].IsMove)
                {
                    return 0;
                }

                _axisStatus[axis].Mdn = false;
                _axisStatus[axis].CommandPos = pos;
                _axisStatus[axis].Vel = vel * Math.Sign(_axisStatus[axis].CommandPos - _axisStatus[axis].CurPos);

                _axisStatus[axis].IsMove = true;
                return 0;
            }
        }

        public int AxisRelMove(int id, int axis, int pos, int vel)
        {
            lock (this)
            {
                if (_axisStatus[axis].IsMove)
                {
                    return 0;
                }

                _axisStatus[axis].Mdn = false;
                _axisStatus[axis].CommandPos = _axisStatus[axis].CurPos + pos;
                _axisStatus[axis].Vel = vel * Math.Sign(pos);

                _axisStatus[axis].IsMove = true;
                return 0;
            }
        }

        public bool IsAxisStop(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Mdn;
            }
        }


        public int AxisStop(int id, int axis)
        {
            lock (this)
            {
                _axisStatus[axis].IsMove = false;
                _axisStatus[axis].Mdn = true;
                return 0;
            }
        }

        public int SetHomeVel(int index, int axis, int vel)
        {
            _axisStatus[axis].Vel = vel;
            return 0;
        }

        public int AxisHomeMove(int index, int axis)
        {
            lock (this)
            {
                _axisStatus[axis].Org = true;
                _axisStatus[axis].Hmv = true;
                return 0;
            }
        }

        public bool IsAxisHmv(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Hmv;
            }
        }

        public bool IsAxisServo(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Servo;
            }
        }

        public bool IsAxisAlarm(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Alarm;
            }
        }

        public bool IsAxisEmg(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Alarm;
            }
        }

        public bool IsAxisAstp(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Astp;
            }
        }

        public bool IsAxisInp(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Mdn;
            }
        }

        public bool IsAxisMel(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Mel;
            }
        }

        public bool IsAxisPel(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Pel;
            }
        }

        public bool IsAxisOrg(int index, int axis)
        {
            lock (this)
            {
                return _axisStatus[axis].Org;
            }
        }


        public int GetDi(int id, int index, out int status)
        {
            status = Di[index];
            return 0;
        }

        public int SetDi(int id, int index, int status)
        {
            Di[index] = status;
            return 0;
        }

        public int SetDo(int id, int index, int status)
        {
            Do[index] = status;
            return 0;
        }

        public int GetDo(int id, int index, out int status)
        {
            status = Do[index];
            return 0;
        }
    }
}