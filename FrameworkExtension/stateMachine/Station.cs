using System.Collections.Generic;
using System.Linq;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.elementInterfaces;

namespace Automation.FrameworkExtension.stateMachine
{
    public sealed class Station : BaseObject, IEventHandler
    {
        public Station(int id, string name, StateMachine machine)
        {
            Id = id;
            Name = name;

            Machine = machine;
            machine.Stations.Add(id, this);

            State = StationState.AUTO;
            AutoState = StationAutoState.WaitReset;
        }

        public StateMachine Machine { get; }

        public bool Enable { get; set; } = true;
        public StationState State { get; private set; }
        public StationAutoState AutoState { get; private set; }

        public Dictionary<int, StationTask> Tasks { get; } = new Dictionary<int, StationTask>();

        public Dictionary<int, IDiEx> PauseSignals { get; } = new Dictionary<int, IDiEx>();


        public void HandleEvent(UserEvent e)
        {
            if (!Enable)
            {
                return;
            }

            switch (e.EventType)
            {
                case UserEventType.START:
                    if (State == StationState.AUTO && AutoState == StationAutoState.WaitRun)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Running;
                    }
                    break;
                case UserEventType.STOP:
                    if (State == StationState.AUTO &&
                        (AutoState == StationAutoState.Running
                         || AutoState == StationAutoState.Resetting
                         || AutoState == StationAutoState.Pause
                         || AutoState == StationAutoState.WaitRun))
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.WaitReset;
                    }
                    break;
                case UserEventType.RESET:
                    if (State == StationState.AUTO && AutoState == StationAutoState.WaitReset)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Resetting;
                    }
                    break;
                case UserEventType.PAUSE:
                    if (State == StationState.AUTO && AutoState == StationAutoState.Running)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Pause;
                    }
                    break;
                case UserEventType.CONTINUE:
                    if (State == StationState.AUTO && AutoState == StationAutoState.Pause)
                    {
                        foreach (var t in Tasks)
                        {
                            t.Value.HandleEvent(e);
                        }
                        AutoState = StationAutoState.Running;
                    }
                    break;
            }
        }

        private bool _isPauseSignal = false;
        public void runningStateMachine()
        {
            if (State == StationState.AUTO)
            {
                switch (AutoState)
                {
                    case StationAutoState.Pause:
                        if (_isPauseSignal && PauseSignals.Count > 0 && PauseSignals.All(e => e.Value.GetDiSts()))
                        {
                            _isPauseSignal = false;
                            foreach (var t in Tasks)
                            {
                                t.Value.Continue();
                            }
                            AutoState = StationAutoState.Running;
                        }

                        //pull up task states
                        if (Tasks.Any(t => t.Value.State == TaskState.WaitReset))
                        {
                            //stop this station
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }
                        else if (Tasks.All(t => t.Value.State == TaskState.WaitRun))
                        {
                            AutoState = StationAutoState.WaitRun;
                        }
                        break;


                    case StationAutoState.Running:
                        if (PauseSignals.Count > 0 && PauseSignals.Any(e => !e.Value.GetDiSts()))
                        {
                            foreach (var t in Tasks)
                            {
                                t.Value.Pause();
                            }
                            _isPauseSignal = true;
                            AutoState = StationAutoState.Pause;
                        }

                        //pull up task states
                        if (Tasks.Any(t => t.Value.State == TaskState.WaitReset))
                        {
                            //stop this station
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }
                        break;

                    case StationAutoState.Resetting:
                        if (PauseSignals.Count > 0 && PauseSignals.Any(e => !e.Value.GetDiSts()))
                        {
                            //todo: show alarm
                            Machine.Beep();
                            ShowAlarm($"{Name} 安全信号{string.Join(",", PauseSignals.Select(d => d.Value.Name))}触发", LogLevel.Error);
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }


                        //pull up task states
                        if (Tasks.Any(t => t.Value.State == TaskState.WaitReset))
                        {
                            //stop this station
                            foreach (var t in Tasks)
                            {
                                t.Value.Stop();
                            }
                            AutoState = StationAutoState.WaitReset;
                        }
                        else if (Tasks.All(t => t.Value.State == TaskState.WaitRun))
                        {
                            AutoState = StationAutoState.WaitRun;
                        }
                        break;
                }
            }
        }

        public void ShowAlarm(string s, LogLevel level)
        {
            //push alarm to machine to show alarm
            Machine.OnAlarmEvent(s, level);

            //pull task alarm to this station
            //if (level == LogLevel.Error || level == LogLevel.Fatal)
            //{
            //    Machine.PostEvent(new UserEvent() { EventType = UserEventType.STOP, EventTarget = this });
            //}
            //else if (level == LogLevel.Warning)
            //{
            //    Machine.PostEvent(new UserEvent() { EventType = UserEventType.PAUSE, EventTarget = this });
            //}
        }
    }
}