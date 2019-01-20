using System;
using System.Threading;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.Base.MotionCardLibrary1
{
    public class LeiSaiCard : IMotionCard
    {
        public string Name { get; set; }
        public int DeviceID { get; set; }
        public bool Initialize()
        {
            LTDMC.dmc_board_init();
            return true;
        }

        public bool Terminate()
        {
            LTDMC.dmc_board_close();
            return true;
        }

        public bool LoadParams(string configFileName, params object[] objects)
        {
            var ret = LTDMC.dmc_download_configfile((ushort)DeviceId, configFileName);

            for (int i = 0; i < 6; i++)
            {
                LTDMC.dmc_set_home_el_return((ushort)DeviceId, (ushort)i, 1);
            }

            if (ret != 0)
            {
                throw new Exception("load config file fail");
            }

            return true;
        }

        public bool ClearAlarm(int index, int axis)
        {
            LTDMC.dmc_write_erc_pin((ushort)DeviceId, (ushort)axis, (ushort)0);
            Thread.Sleep(50);
            LTDMC.dmc_write_erc_pin((ushort)DeviceId, (ushort)axis, (ushort)1);
            Thread.Sleep(50);
            LTDMC.dmc_write_erc_pin((ushort)DeviceId, (ushort)axis, (ushort)0);
            Thread.Sleep(50);
            return true;
        }

        public int DeviceId { get; set; }

        public int SetDo(int index, int port, int status)
        {
            status = status == 1 ? 0 : 1;

            var ret = LTDMC.dmc_write_outbit((ushort)DeviceId, (ushort)port, (ushort)status);
            if (ret != 0)
            {
                throw new Exception("WriteSingleDOutput fail");
            }
            return 0;
        }

        public int GetDo(int index, int port, out int status)
        {
            status = LTDMC.dmc_read_outbit((ushort)DeviceId, (ushort)port);
            status = status == 0 ? 1 : 0;
            return 0;
        }

        public int GetDi(int index, int port, out int status)
        {
            status = LTDMC.dmc_read_inbit((ushort)DeviceId, (ushort)port);
            status = status == 0 ? 1 : 0;
            return 0;
        }

        public int SetDi(int index, int port, int status)
        {
            throw new NotImplementedException();
        }

        public int GetEncPos(int index, int axis, ref double d)
        {
            d = LTDMC.dmc_get_position((ushort)DeviceId, (ushort)axis);
            return 0;
        }

        public int SetEncPos(int index, int axis, int pos)
        {
            return LTDMC.dmc_set_position((ushort)DeviceId, (ushort)axis, pos);
        }

        public int GetCmdPos(int index, int axis, ref double d)
        {
            d = LTDMC.dmc_get_target_position((ushort)DeviceId, (ushort)axis);
            return 0;
        }

        public int SetCmdPos(int index, int axis, int pos)
        {
            throw new NotImplementedException();
        }

        public int Servo(int index, int axis, bool enable)
        {
            LTDMC.dmc_write_erc_pin((ushort)DeviceId, (ushort)axis, (ushort)0);
            Thread.Sleep(50);
            LTDMC.dmc_write_erc_pin((ushort)DeviceId, (ushort)axis, (ushort)1);
            Thread.Sleep(50);
            LTDMC.dmc_write_erc_pin((ushort)DeviceId, (ushort)axis, (ushort)0);
            Thread.Sleep(50);
            return LTDMC.dmc_set_sevon_enable((ushort)DeviceId, (ushort)axis, (ushort)(enable ? 1 : 0));
        }

        public int AxisAbsMove(int index, int axis, int pos, int vel)
        {
            var minVel = 0d;
            var maxVel = 0d;
            var tAcc = 0d;
            var tDec = 0d;
            var stopVel = 0d;
            LTDMC.dmc_get_profile((ushort)DeviceId, (ushort)axis, ref minVel, ref maxVel, ref tAcc, ref tDec, ref stopVel);
            LTDMC.dmc_set_profile((ushort)DeviceId, (ushort)axis, minVel, vel, tAcc, tDec, stopVel);
            return LTDMC.dmc_pmove((ushort)DeviceId, (ushort)axis, pos, 1);
        }

        public int AxisRelMove(int index, int axis, int step, int vel)
        {
            var minVel = 0d;
            var maxVel = 0d;
            var tAcc = 0d;
            var tDec = 0d;
            var stopVel = 0d;
            LTDMC.dmc_get_profile((ushort)DeviceId, (ushort)axis, ref minVel, ref maxVel, ref tAcc, ref tDec, ref stopVel);
            LTDMC.dmc_set_profile((ushort)DeviceId, (ushort)axis, minVel, vel, tAcc, tDec, stopVel);
            return LTDMC.dmc_pmove((ushort)DeviceId, (ushort)axis, step, 0);
        }

        public bool IsAxisStop(int index, int axis)
        {
            return LTDMC.dmc_check_done((ushort)DeviceId, (ushort)axis) == 1;
        }

        public int AxisStop(int index, int axis)
        {

            return LTDMC.dmc_stop((ushort)DeviceId, (ushort)axis, 0);
        }

        public int AxisSetAcc(int index, int axis, double acc)
        {
            return LTDMC.dmc_set_profile((ushort)DeviceId, (ushort)axis, acc / 4, acc / 2, acc / 50 * 0.01, acc / 50 * 0.01, acc);
        }

        public int AxisSetDec(int index, int axis, double dec)
        {
            return LTDMC.dmc_set_profile((ushort)DeviceId, (ushort)axis, dec / 4, dec / 2, dec / 50 * 0.01, dec / 50 * 0.01, dec);
        }

        public int SetHomeVel(int index, int axis, int vel)
        {
            var minVel = 0d;
            var maxVel = 0d;
            var tAcc = 0d;
            var tDec = 0d;
            var stopVel = 0d;
            LTDMC.dmc_get_profile((ushort)DeviceId, (ushort)axis, ref minVel, ref maxVel, ref tAcc, ref tDec, ref stopVel);
            LTDMC.dmc_set_profile((ushort)DeviceId, (ushort)axis, vel / 3d, vel, tAcc, tDec, stopVel);
            return 0;
        }

        public int AxisHomeMove(int index, int axis)
        {
            return LTDMC.dmc_home_move((ushort)DeviceId, (ushort)axis);
        }

        public bool IsAxisHmv(int index, int axis)
        {
            ushort state = 0;
            var ret = LTDMC.dmc_get_home_result((ushort)DeviceId, (ushort)axis, ref state);
            return state != 1;
        }

        public bool IsAxisServo(int index, int axis)
        {
            return LTDMC.dmc_get_sevon_enable((ushort)DeviceId, (ushort)axis) == 1;
        }

        public bool IsAxisAlarm(int index, int axis)
        {
            var sts = LTDMC.dmc_axis_io_status((ushort)DeviceId, (ushort)axis);
            return (sts & (1 << 0)) > 0;
        }

        public bool IsAxisEmg(int index, int axis)
        {
            var sts = LTDMC.dmc_axis_io_status((ushort)DeviceId, (ushort)axis);
            return (sts & (1 << 3)) > 0;
        }

        public bool IsAxisMel(int index, int axis)
        {
            var sts = LTDMC.dmc_axis_io_status((ushort)DeviceId, (ushort)axis);
            return (sts & (1 << 2)) > 0;
        }

        public bool IsAxisPel(int motionDeviceId, int axis)
        {
            var sts = LTDMC.dmc_axis_io_status((ushort)DeviceId, (ushort)axis);
            return (sts & (1 << 1)) > 0;
        }

        public bool IsAxisOrg(int motionDeviceId, int axis)
        {
            var sts = LTDMC.dmc_axis_io_status((ushort)DeviceId, (ushort)axis);
            return (sts & (1 << 4)) > 0;
        }

        public bool IsAxisAstp(int index, int axis)
        {
            var sts = LTDMC.dmc_axis_io_status((ushort)DeviceId, (ushort)axis);
            return (sts & (1 << 3)) > 0;
        }

        public bool IsAxisInp(int index, int axisChannel)
        {
            return true;
        }
    }
}