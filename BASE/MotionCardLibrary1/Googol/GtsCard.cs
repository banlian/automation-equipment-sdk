using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;
using LMotionCardLib.LDriver.Googol;

namespace Automation.Base.MotionCardLibrary1.Googol
{
    public class GtsCard : IMotionCard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public string Version { get; set; }
        public string ConfigFilePath { get; set; }

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
            var ret = mc.GT_Open((short)Id, 0, 1);
            if (ret != 0)
            {
                return false;
            }

            ret = mc.GT_Reset((short)Id);
            if (ret != 0)
            {
                return false;
            }

            ret = mc.GT_ClrSts((short)Id, 1, 8);
            if (ret != 0)
            {
                return false;
            }

            return true;
        }

        public bool Terminate()
        {
            var ret = mc.GT_Close((short)Id);
            if (ret != 0)
            {
                return false;
            }

            return true;
        }

        public UserControl CreateDeviceControl()
        {
            return null;
        }

        public bool LoadParams(string configFileName, params object[] objects)
        {
            var ret = mc.GT_LoadConfig((short)Id, configFileName);
            if (ret != 0)
            {
                return false;
            }

            return true;
        }

        public bool ClearAlarm(int index, int i)
        {
            var ret = mc.GT_SetDoBit((short)Id, mc.MC_CLEAR, (short)i, 1);
            if (ret != 0)
            {
                return false;
            }

            Thread.Sleep(50);

            ret = mc.GT_SetDoBit((short)Id, mc.MC_CLEAR, (short)i, 0);
            if (ret != 0)
            {
                return false;
            }

            ret = mc.GT_ClrSts((short)Id, (short)i, 1);
            if (ret != 0)
            {
                return false;
            }

            ret = mc.GT_GetSts((short)Id, (short)i, out var value, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }

            if ((value & 0x0002) == 0x0002)
            {
                return false;
            }

            return true;
        }

        public int GetDo(int index, int port, out int status)
        {
            status = 0;
            var ret = mc.GT_GetDi((short)Id, mc.MC_GPO, out var value);
            if (ret != 0)
            {
                return -1;
            }

            status = (value & (0x1 << (short)port)) == 0 ? 0 : 1;
            return ret;
        }

        public int SetDo(int index, int port, int status)
        {
            status = status != 0 ? 0 : 1;
            var ret = mc.GT_SetDoBit((short)Id, mc.MC_GPO, (short)(port + 1), (short)status);
            if (ret != 0)
            {
                return -1;
            }

            return ret;
        }

        public int GetDi(int index, int port, out int status)
        {
            status = 0;
            var ret = mc.GT_GetDi((short)Id, mc.MC_GPI, out var value);
            if (ret != 0)
            {
                return -1;
            }

            status = (value & (0x1 << (short)port)) == 0 ? 0 : 1;
            return ret;
        }

        public int SetDi(int index, int port, int status)
        {
            throw new NotImplementedException();
        }

        public int GetEncPos(int index, int axis, ref double d)
        {
            var ret = mc.GT_GetEncPos((short)Id, (short)axis, out var value, 1, out var clock);
            if (ret != 0)
            {
                return -1;
            }

            d = value;
            return ret;
        }

        public int SetEncPos(int index, int axis, int pos)
        {
            var ret = mc.GT_SetEncPos((short)Id, (short)axis, pos);
            if (ret != 0)
            {
                return -1;
            }

            return 0;
        }

        public int GetCmdPos(int index, int axis, ref double d)
        {
            var ret = mc.GT_GetPrfPos((short)Id, (short)axis, out var value, 1, out var clock);
            if (ret != 0)
            {
                return -1;
            }

            d = value;
            return ret;
        }

        public int SetCmdPos(int index, int axis, int pos)
        {
            throw new NotImplementedException();
        }

        public int Servo(int index, int axis, bool enable)
        {
            short ret;
            if (enable)
            {
                ret = mc.GT_AxisOn((short)Id, (short)axis);
            }
            else
            {
                ret = mc.GT_AxisOff((short)Id, (short)axis);
            }

            return ret;
        }

        public int AxisAbsMove(int index, int axis, int pos, int vel)
        {
            var ret = mc.GT_ClrSts((short)Id, (short)axis, 1);
            if (ret != 0)
            {
                return -1;
            }
            //0.3s for acc or dec
            //to do test
            var trapPrm = new mc.TTrapPrm
            {
                acc = vel / 0.3 / 1000000,
                dec = vel / 0.3 / 1000000,
                smoothTime = 10
            };
            ret = mc.GT_PrfTrap((short)Id, (short)axis);
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_SetTrapPrm((short)Id, (short)axis, ref trapPrm);
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_SetVel((short)Id, (short)axis, (double)vel / 1000);
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_SetPos((short)Id, (short)axis, Convert.ToInt32(pos));
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_Update((short)Id, 0x1 << (axis - 1));
            if (ret != 0)
            {
                return -1;
            }

            return 0;
        }

        public int AxisRelMove(int index, int axis, int step, int vel)
        {
            var ret = mc.GT_ClrSts((short)Id, (short)axis, 1);
            if (ret != 0)
            {
                return -1;
            }
            var encPos = 0d;
            GetCmdPos(index, (short)axis, ref encPos);

            //0.3s for acc or dec
            //to do test
            var trapPrm = new mc.TTrapPrm
            {
                acc = vel / 0.3 / 1000000,
                dec = vel / 0.3 / 1000000,
                smoothTime = 10
            };
            ret = mc.GT_PrfTrap((short)Id, (short)axis);
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_SetTrapPrm((short)Id, (short)axis, ref trapPrm);
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_SetVel((short)Id, (short)axis, (double)vel / 1000);
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_SetPos((short)Id, (short)axis, Convert.ToInt32(step + encPos));
            if (ret != 0)
            {
                return -1;
            }

            ret = mc.GT_Update((short)Id, 0x1 << (axis - 1));
            if (ret != 0)
            {
                return -1;
            }

            return 0;
        }

        public bool IsAxisStop(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0800) == 1;
        }

        public int AxisStop(int index, int axis)
        {
            int ret = mc.GT_Stop((short)Id, 1 << ((short)axis - 1), axis - 1);
            if (ret != 0)
            {
                return -1;
            }
            return 0;
        }

        public int SetHomeVel(int index, int axis, int vel)
        {
            mc.GT_HomeInit((short)Id);
            return 0;
        }

        public int AxisHomeMove(int index, int axis)
        {
            var ret = mc.GT_Home((short)Id, (short)axis, int.MaxValue, 100, 1000, 0);
            if (ret != 0)
            {
                return -1;
            }
            return 0;
        }

        public bool IsAxisHmv(int index, int axis)
        {
            var ret = mc.GT_HomeSts((short)Id, (short)axis, out var sts);
            if (ret != 0)
            {
                return false;
            }
            return sts != 0;
        }

        public bool IsAxisServo(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0200) == 1;
        }

        public bool IsAxisAlarm(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0002) == 1;
        }

        public bool IsAxisEmg(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0002) == 1;
        }

        public bool IsAxisAstp(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0002) == 1;
        }

        public bool IsAxisInp(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0800) == 1;
        }

        public bool IsAxisMel(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0040) == 1;
        }

        public bool IsAxisPel(int index, int axis)
        {
            var ret = mc.GT_GetSts((short)Id, (short)axis, out var sts, 1, out var clock);
            if (ret != 0)
            {
                return false;
            }
            return (sts & 0x0020) == 1;
        }

        public bool IsAxisOrg(int index, int axis)
        {
            var ret = mc.GT_GetDi((short)Id, mc.MC_HOME, out var sts);
            if (ret != 0)
            {
                return false;
            }
            return (sts & (0x1 << axis - 1)) == 1;
        }
    }
}