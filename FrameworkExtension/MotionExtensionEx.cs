using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.motionDriver;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension
{
    public static class MotionExtensionEx
    {
        #region  IVioEx

        public static bool GetVioSts(this IVioEx vioex, bool status = true)
        {
            int sts;
            vioex.DriverImpl.GetDo(vioex.Port, out sts);
            return (sts == 1) == status;
        }

        public static bool SetVio(this IVioEx vioex, StationTask task, bool status = true)
        {
            vioex.DriverImpl.SetDo(vioex.Port, status ? 1 : 0);
            task?.Log($"{vioex.Name} SetVio {vioex.Port} {status} success", LogLevel.Debug);
            return true;
        }

        public static bool WaitVio(this IVioEx vioex, StationTask task, bool status = true, int timeout = -1)
        {
            timeout = timeout < 0 ? int.MaxValue : timeout;

            var err = $"{vioex.Name} WaitVio {vioex.Port} {status}";
            var t = 0;
            while (t++ < timeout)
            {
                var sts = 0;
                vioex.DriverImpl.GetDo(vioex.Port, out sts);
                if (sts == 1 == status)
                {
                    task?.Log($"{err} success", LogLevel.Debug);
                    return true;
                }

                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel("WaitVio");
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            task?.Log($"{err} timeout", LogLevel.Error);
            return false;
        }

        #endregion

        #region  ICylinderEx

        public static void SetDoAsync(this ICylinderEx[] cyex, StationTask task, bool status = true)
        {
            for (int i = 0; i < cyex.Length; i++)
            {
                cyex[i].DriverImpl.SetDo(cyex[i].DoOrg, status ? 0 : 1);
                cyex[i].DriverImpl.SetDo(cyex[i].DoWork, status ? 1 : 0);
            }
        }

        public static bool SetDo(this ICylinderEx cyex, StationTask task, bool status = true, int timeout = 3000, bool isErrorOnSignalError = true)
        {
            return SetDo(new[] {cyex}, task, new[] {status}, timeout, isErrorOnSignalError);
        }

        public static bool SetDo(this ICylinderEx[] cyex, StationTask task, bool[] status, int timeout = 3000, bool isErrorOnSignalError = true)
        {
            for (int i = 0; i < cyex.Length; i++)
            {
                cyex[i].DriverImpl.SetDo(cyex[i].DoOrg, status[i] ? 0 : 1);
                cyex[i].DriverImpl.SetDo(cyex[i].DoWork, status[i] ? 1 : 0);
            }

            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }


            return WaitDi(cyex, task, status, timeout, isErrorOnSignalError);
        }

        public static bool WaitDi(this ICylinderEx cyex, StationTask task, bool status = true, int timeout = -1, bool isErrorOnSignalError = true)
        {
            return WaitDi(new[] {cyex.DiOrg, cyex.DiWork}, task, new[] {cyex.DriverImpl, cyex.DriverImpl}, new[] {!status, status}, timeout);
        }

        public static bool WaitDi(this ICylinderEx[] cyex, StationTask task, bool[] status, int timeout = -1, bool isErrorOnSignalError = true)
        {
            var motion1 = cyex.Select(c => c.DriverImpl);
            var motion2 = cyex.Select(c => c.DriverImpl);
            var motion = motion1.Concat(motion2).ToArray();

            var diOrg = cyex.Select(c => c.DiOrg);
            var diWork = cyex.Select(c => c.DiWork);

            var diOrgSts = status.Select(s => !s);
            var diWorkSts = status.Select(s => s);

            var waitdi = diOrg.Concat(diWork).ToArray();
            var waitSts = diOrgSts.Concat(diWorkSts).ToArray();

            var err = $"{string.Join(",", cyex.Select(c => c.Name))} SetDo {status} {timeout}";
            var ret = WaitDi(waitdi, task, motion, waitSts, timeout);
            if (!ret)
            {
                task?.Log($"{err} error", isErrorOnSignalError ? LogLevel.Warning : LogLevel.Debug);
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Debug);
            }

            return ret;
        }

        #endregion

        #region  IDoEx IDiEx

        public static bool GetDoSts(this IDoEx doex, bool status = true)
        {
            int sts;
            doex.DriverImpl.GetDo(doex.Port, out sts);
            if (sts == 1 == status) return true;
            return false;
        }

        public static bool SetDo(this IDoEx doex, bool status = true)
        {
            doex.DriverImpl.SetDo(doex.Port, status ? 1 : 0);
            return true;
        }

        public static bool SetDo(this IDoEx[] doex, bool[] status)
        {
            for (int i = 0; i < doex.Length; i++)
            {
                doex[i].DriverImpl.SetDo(doex[i].Port, status[i] ? 1 : 0);
            }

            return true;
        }

        public static bool Toggle(this IDoEx doex)
        {
            SetDo(doex, !GetDoSts(doex));
            return true;
        }


        private static object _getdi = new object();

        public static bool GetDiSts(this IDiEx diex, bool status = true)
        {
            if (FrameworkExtenion.IsSimulate)
            {
                return true;
            }

            lock (_getdi)
            {
                int sts;
                diex.DriverImpl.GetDi(diex.Port, out sts);
                return (sts == 1) == status;
            }
        }

        public static bool WaitDi(this IDiEx diex, StationTask task, bool status = true, int timeout = -1)
        {
            return WaitDi(diex.Port, task, diex.DriverImpl, status, timeout);
        }

        public static bool WaitDi(this int diex, StationTask task, IMotionWrapper motion, bool status = true, int timeout = -1)
        {
            return WaitDi(new[] {diex}, task, new[] {motion}, new[] {status}, timeout);
        }

        public static bool WaitDi(this int[] diex, StationTask task, IMotionWrapper[] motion, bool[] status, int timeout = -1)
        {
            int[] sts = new int[diex.Length];

            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                //check di status
                for (int i = 0; i < diex.Length; i++)
                {
                    motion[i].GetDi(diex[i], out sts[i]);
                }

                bool ret = true;
                for (int i = 0; i < diex.Length; i++)
                {
                    ret = ((sts[i] == 1) == status[i]);
                    if (!ret)
                    {
                        break;
                    }
                }

                if (ret)
                {
                    return true;
                }

                //check task status
                if (task != null)
                {
                    task.JoinIfPause();
                    task.AbortIfCancel("WaitDi");
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            return false;
        }

        #endregion

        #region  IAxisEx

        public static double[] GetPos(this IAxisEx[] axis)
        {
            return axis.Select(GetPos).ToArray();
        }

        public static double GetPos(this IAxisEx axis)
        {
            int pos = 0;
            axis.DriverImpl.GetEncPos(axis.Channel, ref pos);
            return (int) (100 * axis.ToMm(pos)) / 100d;
        }


        public static bool ServoEnable(this IAxisEx axis, StationTask task, bool status)
        {
            return ServoEnable(new[] {axis}, task, status);
        }

        public static bool ServoEnable(this IAxisEx[] axis, StationTask task, bool status)
        {
            //start servo
            foreach (var a in axis)
            {
                if (a.DriverImpl.GetAxisEnable(a.Channel))
                {
                    a.DriverImpl.ServoEnable(a.Channel, false);
                    Thread.Sleep(300);
                }

                a.DriverImpl.ServoEnable(a.Channel, status);
            }

            //wait servo
            Thread.Sleep(500);
            var err = $"{string.Join(",", axis.Select(e => e.Name))} ServoEnable {status}";
            var ret = axis.All(e => e.DriverImpl.GetAxisEnable(e.Channel));
            if (!ret)
            {
                task?.Log($"{err} error", LogLevel.Error);
                task?.ThrowException($"{err} error");
            }
            else
            {
                task?.Log($"{err} success", LogLevel.Debug);
            }

            return ret;
        }


        public static bool Home(this IAxisEx axis, StationTask task, double vel, int timeout = -1)
        {
            return Home(new[] {axis}, task, new[] {vel}, timeout);
        }

        public static bool Home(this IAxisEx[] axis, StationTask task, double[] vel, int timeout = -1)
        {
            if (axis.Any(a => !a.DriverImpl.GetAxisEnable(a.Channel)))
            {
                task?.ThrowException("Home Servo Error");
                return false;
            }

            //start home
            foreach (var a in axis)
            {
                a.DriverImpl.Home(a.Channel, a.ToPls(a.HomeVm));
            }

            //wait home done
            var err = $"{string.Join(",", axis.Select(e => e.Name))} Home";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                if (axis.All(a => a.DriverImpl.CheckHomeDone(a.Channel)))
                {
                    task?.Log($"{err} success", LogLevel.Debug);
                    return true;
                }

                if (axis.Any(a => a.DriverImpl.GetAxisAlarm(a.Channel)))
                {
                    task?.Log($"{err} alarm error", LogLevel.Error);
                    task?.ThrowException($"{err} alarm error");
                    return false;
                }

                //check task status
                if (task != null)
                {
                    if (!task.IsRunning)
                    {
                        foreach (var a in axis)
                        {
                            a.DriverImpl.MoveStop(a.Channel);
                        }
                    }

                    if (task.IsPause)
                    {
                        foreach (var a in axis)
                        {
                            a.DriverImpl.MoveStop(a.Channel);
                        }

                        task.JoinIfPause();
                        foreach (var a in axis)
                        {
                            a.DriverImpl.Home(a.Channel, a.ToPls(a.HomeVm));
                        }
                    }

                    task.AbortIfCancel("Home");
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            //home timeout
            task?.Log($"{err} timeout", LogLevel.Error);
            task?.ThrowException($"{err} timeout");
            return false;
        }


        public static bool MoveAbs(this IAxisEx axis, double pos, double vel, int timeout = -1)
        {
            return MoveAbs(axis, null, pos, vel, timeout);
        }

        public static bool MoveAbs(this IAxisEx[] axis, double[] pos, double[] vel, int timeout = -1)
        {
            return MoveAbs(axis, null, pos, vel, timeout);
        }


        public static bool MoveAbs(this IAxisEx axis, StationTask task, double pos, double vel, int timeout = -1, bool isCheckLimit = true)
        {
            return MoveAbs(new[] {axis}, task, new[] {pos}, new[] {vel}, timeout, isCheckLimit);
        }

        public static bool MoveAbs(this IAxisEx[] axis, StationTask task, double[] pos, double[] vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (axis.Any(e => !e.DriverImpl.GetAxisEnable(e.Channel)))
            {
                task?.ThrowException("MoveAbs Servo error");
                return false;
            }

            //start move
            for (int i = 0; i < axis.Length; i++)
            {
                axis[i].DriverImpl.MoveAbs(axis[i].Channel, axis[i].ToPls(pos[i]), axis[i].ToPls(vel[i]));
            }

            //wait done
            var err = $"{string.Join(",", axis.Select(e => e.Name))} MoveAbs {string.Join(",", pos.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            var t = 0;
            timeout = timeout < 0 ? int.MaxValue : timeout;
            while (t++ < timeout)
            {
                //check alarm
                if (axis.Any(a => a.DriverImpl.GetAxisAlarm(a.Channel) || isCheckLimit && a.DriverImpl.CheckLimit(a.Channel)))
                {
                    var msg = $"{err} limit {axis.Any(a => a.DriverImpl.CheckLimit(a.Channel))} or alarm {axis.Any(a => a.DriverImpl.GetAxisAlarm(a.Channel))} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }

                //check done
                if (axis.All(a => a.DriverImpl.CheckMoveDone(a.Channel)))
                {
                    var fail = axis.Any(IsMoveErrorHappen);
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {string.Join(",", axis.Select(e => e.Error))}", LogLevel.Error);
                        task?.ThrowException(err);
                    }
                    else
                    {
                        task?.Log($"{err} success", LogLevel.Debug);
                    }

                    return !fail;
                }

                //check task status
                if (task != null)
                {
                    if (!task.IsRunning)
                    {
                        foreach (var a in axis)
                        {
                            a.DriverImpl.MoveStop(a.Channel);
                        }
                    }

                    if (task.IsPause)
                    {
                        foreach (var a in axis)
                        {
                            a.DriverImpl.MoveStop(a.Channel);
                        }

                        task.JoinIfPause();
                        for (int i = 0; i < axis.Length; i++)
                        {
                            axis[i].DriverImpl.MoveAbs(axis[i].Channel, axis[i].ToPls(pos[i]), axis[i].ToPls(vel[i]));
                        }
                    }

                    task.AbortIfCancel("MoveAbs");
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            //MoveAbs timeout
            task?.Log($"{err} timeout", LogLevel.Error);
            task?.ThrowException($"{err} timeout");
            return false;
        }

        public static bool MoveRel(this IAxisEx[] axis, double[] pos, double[] vel, int timeout = -1)
        {
            return MoveRel(axis, null, pos, vel, timeout);
        }

        public static bool MoveRel(this IAxisEx axis, StationTask task, double step, double vel, int timeout = -1, bool isCheckLimit = true)
        {
            return MoveRel(new[] {axis}, task, new[] {step}, new[] {vel}, timeout, isCheckLimit);
        }

        public static bool MoveRel(this IAxisEx[] axis, StationTask task, double[] step, double[] vel, int timeout = -1, bool isCheckLimit = true)
        {
            if (axis.Any(a => !a.DriverImpl.GetAxisEnable(a.Channel)))
            {
                task?.ThrowException("MoveRel Servo error");
                return false;
            }

            //start move
            int[] startpls = new int[axis.Length];
            int[] pausepls = new int[axis.Length];
            for (int i = 0; i < axis.Length; i++)
            {
                axis[i].DriverImpl.GetEncPos(axis[i].Channel, ref startpls[i]);
                axis[i].DriverImpl.MoveRel(axis[i].Channel, axis[i].ToPls(step[i]), axis[i].ToPls(vel[i]));
            }

            //wait done
            var err = $"{string.Join(",", axis.Select(e => e.Name))} MoveRel {string.Join(",", step.Select(p => p.ToString("F2")))} {string.Join(",", vel.Select(p => p.ToString("F2")))}";
            timeout = timeout < 0 ? int.MaxValue : timeout;
            var t = 0;
            while (t++ < timeout)
            {
                //check alarm
                if (axis.Any(a => a.DriverImpl.GetAxisAlarm(a.Channel) || isCheckLimit && a.DriverImpl.CheckLimit(a.Channel)))
                {
                    var msg =
                        $"{err} LIMIT: MEL {axis.Any(a => a.DriverImpl.LimitMel(a.Channel))} PEL {axis.Any(a => a.DriverImpl.LimitPel(a.Channel))} or ALARM {axis.Any(a => a.DriverImpl.GetAxisAlarm(a.Channel))} error";
                    task?.Log(msg, LogLevel.Error);
                    task?.ThrowException(msg);
                    return false;
                }

                if (axis.All(a => a.DriverImpl.CheckMoveDone(a.Channel)))
                {
                    var fail = axis.Any(IsMoveErrorHappen);
                    if (fail)
                    {
                        task?.Log($"{err} move pulse error {string.Join(",", axis.Select(e => e.Error))}", LogLevel.Error);
                        task?.ThrowException(err);
                    }
                    else
                    {
                        task?.Log($"{err} success", LogLevel.Debug);
                    }

                    return !fail;
                }

                //check axis status
                if (task != null)
                {
                    if (!task.IsRunning)
                    {
                        foreach (var a in axis)
                        {
                            a.DriverImpl.MoveStop(a.Channel);
                        }
                    }

                    if (task.IsPause)
                    {
                        for (int i = 0; i < axis.Length; i++)
                        {
                            axis[i].DriverImpl.MoveStop(axis[i].Channel);
                            axis[i].DriverImpl.GetEncPos(axis[i].Channel, ref pausepls[i]);
                        }

                        task.JoinIfPause();
                        for (int i = 0; i < axis.Length; i++)
                        {
                            axis[i].DriverImpl.MoveRel(axis[i].Channel, axis[i].ToPls(step[i]) - (pausepls[i] - startpls[i]), axis[i].ToPls(vel[i]));
                        }
                    }

                    task.AbortIfCancel("MoveRel");
                }
                else
                {
                    Application.DoEvents();
                }

                Thread.Sleep(1);
            }

            //MoveRel timeout
            task?.Log($"{err} timeout", LogLevel.Error);
            task?.ThrowException($"{err} timeout");
            return false;
        }

        public static bool Jump(this IAxisEx[] axis, double[] pos, double[] vel, double jump = -50, int timeout = -1, bool isCheckLimit = true, double zMinLimit = 0)
        {
            return Jump(axis, null, pos, vel, jump, timeout, isCheckLimit, zMinLimit);
        }

        public static bool Jump(this IAxisEx[] axis, StationTask task, double[] pos, double[] vel, double jump = -50, int timeout = -1, bool isCheckLimit = true, double zMinLimit = 0)
        {
            if (pos == null)
            {
                task?.ThrowException("Jump Params Error");
                return false;
            }

            //Z Range Check
            axis[2].Error = string.Empty;
            var curpos = axis[2].GetPos();
            if (curpos + jump < zMinLimit)
            {
                var err = $"Jump Z Over Range {curpos + jump} < {zMinLimit} error";
                axis[2].Error = err;
                task?.ThrowException(err);
                task?.Log(err, LogLevel.Error);
                return false;
            }
            else
            {
                MoveRel(axis[2], task, jump, vel[2], timeout, isCheckLimit);
            }

            MoveAbs(new[] {axis[0], axis[1]}, task, new[] {pos[0], pos[1]}, new[] {vel[0], vel[1]}, timeout, isCheckLimit);
            MoveAbs(axis[2], task, pos[2], vel[2], timeout, isCheckLimit);
            return true;
        }

        private static bool IsMoveErrorHappen(IAxisEx axis)
        {
            Thread.Sleep(50);
            var p1 = 0;
            axis.DriverImpl.GetCommandPos(axis.Channel, ref p1);
            var p2 = 0;
            axis.DriverImpl.GetEncPos(axis.Channel, ref p2);

            int limit = 1000;
            if (p2 - p1 > limit || p1 - p2 > limit)
            {
                axis.Error = $"PLS ERROR {Math.Abs(p2 - p1)} > {limit}";
                return true;
            }

            return false;
        }

        #endregion
    }
}