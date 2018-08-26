using System;
using System.Collections.Generic;
using System.Linq;
using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.platforms.points;
using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.platforms.platformBase
{
    public class PlatformEx<TPos> where TPos : IPos
    {
        public PlatformEx(string name, IAxisEx[] axis, StationTask task, List<TPos> positions)
        {
            Name = name;
            Task = task;

            Axis = axis;
            Vel = axis.Select(a => a.Vel).ToArray();
            Acc = axis.Select(a => a.Acc).ToArray();

            Positions = positions;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public StationTask Task { get; }

        public IAxisEx[] Axis { get; }
        public double[] Vel { get; }
        public double[] Acc { get; }

        public List<TPos> Positions { get; }


        public virtual bool CheckTeachPos()
        {
            return true;
        }

        public virtual bool SafeCheck()
        {
            return true;
        }


        public bool Home(int[] order = null, int timeout = -1)
        {
            if (order == null)
            {
                return Axis.Home(Task, Vel, timeout);
            }
            else
            {
                if (order.Length != Axis.Length)
                {
                    throw new Exception("Home Order Params Error");
                    //return false;
                }

                var orderMin = order.Min();
                var orderMax = order.Max();

                for (int thisOrder = orderMin; thisOrder <= orderMax; thisOrder++)
                {
                    List<IAxisEx> homeAxis = new List<IAxisEx>();

                    for (int j = 0; j < order.Length; j++)
                    {
                        if (order[j] == thisOrder)
                        {
                            homeAxis.Add(Axis[j]);
                        }
                    }

                    if (homeAxis.Count > 0)
                    {
                        var ret = homeAxis.ToArray().Home(Task, Vel, timeout);
                        if (!ret)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
        public void TeachPos(string name, string description)
        {

        }

        public void JogOn(int axis, bool dir, double speed)
        {

        }

        public void JogOff(int axis)
        {

        }

        public bool MoveAbs(string position, int timeout = -1, bool checkLimit = true)
        {
            var pos = Positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new Exception($"MoveAbs Position {position} not Exist");
            }

            return Axis.MoveAbs(Task, pos.Data(), Vel, timeout, checkLimit);
        }


        public bool Jump(string position, double jumpHeight = 0, int timeout = -1, bool checkLimit = true, double zLimit = 0d)
        {
            var pos = Positions.FirstOrDefault(p => p.Name == position);
            if (pos == null)
            {
                throw new Exception($"Jump Position {position} not Exist");
            }

            return Axis.Jump(Task, pos.Data(), Vel, jumpHeight, timeout, checkLimit, zLimit);
        }

    }
}
