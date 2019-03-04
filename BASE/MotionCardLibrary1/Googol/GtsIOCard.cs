using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;
using LMotionCardLib.LDriver.Googol;

namespace Automation.Base.MotionCardLibrary1.Googol
{
    public class GtsIOCard : IMotionCard
    {
        private int _parentId;

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
            return true;
        }

        public bool Terminate()
        {
            var ret = mc.GT_CloseExtMdl((short)_parentId);
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
            var configs = configFileName.Split('_');
            _parentId = int.Parse(configs[1]);
            Id = int.Parse(configs[2]);

            var ret = mc.GT_OpenExtMdl((short)_parentId, "gts.dll");
            if (ret != 0)
            {
                return false;
            }

            ret = mc.GT_LoadExtConfig((short)_parentId, configs[0] + ".cfg");
            if (ret != 0)
            {
                return false;
            }
            return true;
        }

        public bool ClearAlarm(int index, int i)
        {
            throw new NotImplementedException();
        }

        public int GetDo(int index, int port, out int status)
        {
            ushort sts = 0;
            status = 0;
            int ret = mc.GT_GetExtDoValue((short)_parentId, (short)Id, ref sts);
            if (ret != 0)
            {
                return -1;
            }
            status = (sts & (1 << index)) != 0 ? 0 : 1;
            return 0;
        }

        public int SetDo(int index, int port, int status)
        {
            status = status != 0 ? 0 : 1;
            int ret = mc.GT_SetExtIoBit((short)_parentId, (short)Id, (short)port, (ushort)status);
            if (ret != 0)
            {
                return -1;
            }
            return 0;
        }

        public int GetDi(int index, int port, out int status)
        {
            ushort sts = 0;
            status = 0;
            int ret = mc.GT_GetExtIoValue((short)_parentId, (short)Id, ref sts);
            if (ret != 0)
            {
                return -1;
            }

            status = (sts & (1 << port)) == 0 ? 1 : 0;
            return 0;
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
