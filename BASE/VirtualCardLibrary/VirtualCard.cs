using System;
using System.Xml.Serialization;
using Automation.FrameworkExtension.motionDriver;
using VirtualCardLibrary.cardDataStructures;
using Timer = System.Threading.Timer;

namespace VirtualCardLibrary
{
    [Serializable]
    public class VirtualCard : IMotionCard
    {
        #region  origin impletions

        private readonly CardAxisMotion[] _cardAxisMotion;


        [XmlIgnore]
        public int[] Di { get; }

        [XmlIgnore]
        public int[] Do { get; }


        private Timer _timer;

        public VirtualCard()
        {
            Di = new int[32];
            Do = new int[32];

            _cardAxisMotion = new CardAxisMotion[16];
            _timer = new Timer(OnTimer, null, 0, 100);

        }



        private void OnTimer(object state)
        {
            lock (this)
            {
                //motion simulate update
                for (var i = 0; i < _cardAxisMotion.Length; i++)
                {
                    _cardAxisMotion[i].Update();
                }
            }
        }


        public int InitialCard(int cardNum, string configFileName)
        {
            return 0;
        }

        public int ReleaseCard(int cardNum)
        {
            return 0;
        }

        public int LoadParams(string path)
        {
            return 0;
        }

        public void ClrSts(int cardId, int axis)
        {
        }

        public int ClearServoAlarm(int id, int axis)
        {
            lock (this)
            {
                _cardAxisMotion[axis].Alarm = false;
                _cardAxisMotion[axis].Servo = false;
                return 0;
            }
        }

        public int SetServo(int id, int axis, bool sts)
        {
            lock (this)
            {
                _cardAxisMotion[axis].Servo = sts;
                return 0;
            }
        }

        #region axis move

        public int SetAxisBand(int id, int axis, int band_Pulse, int time_ms)
        {
            return 0;
        }

        public int SetCommandPos(int id, int axis, int pos)
        {
            lock (this)
            {
                _cardAxisMotion[axis].CommandPos = pos;
                return 0;
            }
        }

        public int GetCommandPos(int id, int axis, ref int pos)
        {
            lock (this)
            {
                pos = _cardAxisMotion[axis].CommandPos;
                return 0;
            }
        }

        public int SetEncPos(int id, int axis, int encpos)
        {
            lock (this)
            {
                _cardAxisMotion[axis].CurPos = encpos;
                return 0;
            }
        }

        public int GetEncPos(int id, int axis, ref int pos)
        {
            lock (this)
            {
                pos = _cardAxisMotion[axis].CurPos;
                return 0;
            }
        }


        public int GetMotionIO(int id, int axis, ref int sts)
        {
            lock (this)
            {
                sts = 0;

                if (_cardAxisMotion[axis].Alarm)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_ALM);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_ALM);
                }

                if (_cardAxisMotion[axis].Pel)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_PEL);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_PEL);
                }

                if (_cardAxisMotion[axis].Mel)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_MEL);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_MEL);
                }

                if (_cardAxisMotion[axis].Org)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_ORG);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_ORG);
                }

                if (_cardAxisMotion[axis].Astp)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_EMG);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_EMG);
                }

                if (_cardAxisMotion[axis].Servo)
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_IO_SVON);
                }
                else
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_IO_SVON);
                }

                return 0;
            }
        }

        public int GetMotionSts(int id, int axis, ref int sts)
        {
            lock (this)
            {
                sts = 0;

                if (!_cardAxisMotion[axis].Mdn)
                {
                    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_STS_MDN);
                }
                else
                {
                    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_STS_MDN);
                }

                //if (_cardAxisMotion[axis].Astp || _cardAxisMotion[axis].Mel || _cardAxisMotion[axis].Pel || !_cardAxisMotion[axis].Servo)
                //{
                //    BitOperators.SET_BITS(ref sts, MotionAxisDefine.MOTION_STS_ASTP);
                //}
                //else
                //{
                //    BitOperators.CLEAR_BITS(ref sts, MotionAxisDefine.MOTION_STS_ASTP);
                //}

                return 0;
            }
        }

        public int SetMoveParm(int cardId, int axis, double Acc, double Dec)
        {
            _cardAxisMotion[axis].Acc = (int)Acc;
            _cardAxisMotion[axis].Dec = (int)Dec;
            return 0;
        }

        public int ABSMove(int id, int axis, int pos, int vel)
        {
            lock (this)
            {
                if (_cardAxisMotion[axis].IsMove)
                {
                    return 0;
                }

                _cardAxisMotion[axis].Mdn = false;
                _cardAxisMotion[axis].CommandPos = pos;
                _cardAxisMotion[axis].Vel = vel * Math.Sign(_cardAxisMotion[axis].CommandPos - _cardAxisMotion[axis].CurPos);

                _cardAxisMotion[axis].IsMove = true;
                return 0;
            }
        }

        public int RELMove(int id, int axis, int pos, int vel)
        {
            lock (this)
            {
                if (_cardAxisMotion[axis].IsMove)
                {
                    return 0;
                }

                _cardAxisMotion[axis].Mdn = false;
                _cardAxisMotion[axis].CommandPos = _cardAxisMotion[axis].CurPos + pos;
                _cardAxisMotion[axis].Vel = vel * Math.Sign(pos);

                _cardAxisMotion[axis].IsMove = true;
                return 0;
            }
        }

        public int ZeroPos(int id, int axis)
        {
            lock (this)
            {
                _cardAxisMotion[axis].CurPos = 0;
                _cardAxisMotion[axis].CommandPos = 0;
            }

            return 0;
        }

        public int EMGStop(int id, int axis)
        {
            lock (this)
            {
                _cardAxisMotion[axis].IsMove = false;
                _cardAxisMotion[axis].Mdn = true;
                return 0;
            }
        }

        public int StopAxis(int id, int axis)
        {
            lock (this)
            {
                _cardAxisMotion[axis].IsMove = false;
                _cardAxisMotion[axis].Mdn = true;
                return 0;
            }
        }

        #endregion

        #region home

        public int HomeMove(int id, int axis, int vel)
        {
            return 0;
        }

        public int SetCaptureModeHome(int id, int axis)
        {
            return 0;
        }

        public int GetCaptureHome(int id, int axis, ref int CaptureSts, ref int CapturePos)
        {
            CaptureSts = 1;
            CapturePos = 0;
            return 0;
        }

        public int SetCaptureModeIndex(int id, int axis)
        {
            return 0;
        }

        public int GetCaptureIndex(int id, int axis, ref int CaptureSts, ref int CapturePos)
        {
            CaptureSts = 1;
            CapturePos = 0;
            return 0;
        }

        public int SearchForHome(int id, int axis, int length, double vel)
        {
            lock (this)
            {
                _cardAxisMotion[axis].Mel = false;
                _cardAxisMotion[axis].Pel = false;
                _cardAxisMotion[axis].Org = true;
                return 0;
            }
        }

        public int SearchForIndex(int id, int axis, int length, double vel)
        {
            lock (this)
            {
                _cardAxisMotion[axis].Mel = false;
                _cardAxisMotion[axis].Pel = false;
                _cardAxisMotion[axis].Org = true;
                return 0;
            }
        }

        public int SearchForLimit(int id, int axis, int length, double vel)
        {
            lock (this)
            {
                if (length > 0)
                {
                    _cardAxisMotion[axis].Mel = false;
                    _cardAxisMotion[axis].Pel = true;
                    _cardAxisMotion[axis].Org = false;
                }
                else
                {
                    _cardAxisMotion[axis].Mel = true;
                    _cardAxisMotion[axis].Pel = false;
                    _cardAxisMotion[axis].Org = false;
                }

                return 0;
            }
        }

        #endregion

        #region di do

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



        #endregion

        #endregion


        public int DeviceID { get; set; }
        public int GetDo(int index, int i, int port, out int status)
        {
            return GetDo(index, port, out status);
        }

        public int SetDo(int index, int i, int port, int status)
        {
            return SetDo(index, port, status);
        }

        public int GetDi(int index, int i, int port, out int status)
        {
            return GetDi(index, port, out status);
        }
    }
}