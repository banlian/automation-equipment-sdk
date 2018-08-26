using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Automation.FrameworkExtension.common;
using Automation.FrameworkExtension.elementInterfaces;
using Automation.FrameworkExtension.motionDriver;

namespace Automation.FrameworkExtension.stateMachine
{
    public abstract class StateMachine : IEventHandler
    {
        public event Action<string, LogLevel> AlarmEvent;

        public Dictionary<int, IDiEx> DiEstop { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiPauseSignal { get; } = new Dictionary<int, IDiEx>();


        public Dictionary<int, IDiEx> DiAuto { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiStart { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiStop { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDiEx> DiReset { get; } = new Dictionary<int, IDiEx>();


        public Dictionary<int, IDoEx> DoLightRed { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IDoEx> DoLightYellow { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IDoEx> DoLightGreen { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, IDoEx> DoBuzzer { get; } = new Dictionary<int, IDoEx>();



        public StationState State { get; protected set; }
        public StationAutoState AutoState { get; protected set; }


        public Dictionary<int, Station> Stations { get; } = new Dictionary<int, Station>();


        protected StateMachine()
        {
            State = StationState.AUTO;
            AutoState = StationAutoState.WaitReset;
        }


        public virtual void Initialize()
        {
            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo());
            DoLightGreen.All(d => d.Value.SetDo(false));
            DoBuzzer.All(d => d.Value.SetDo(false));

            _isrunning = true;
            _mainThread = new Thread(Update);
            _mainThread.IsBackground = true;
            _mainThread.Start();
        }


        public virtual void Terminate()
        {
            Stop();

            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo(false));
            DoLightGreen.All(d => d.Value.SetDo(false));
            DoBuzzer.All(d => d.Value.SetDo(false));

            _isrunning = false;
            if (_mainThread.IsAlive)
            {
                _mainThread.Join();
            }
        }

        public void Start()
        {
            DoLightGreen.All(d => d.Value.Toggle());
            Thread.Sleep(50);
            DoLightGreen.All(d => d.Value.Toggle());

            PostEvent(new UserEvent() { EventType = UserEventType.START, EventTarget = this });

            _runningFlashCount = 0;
            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo(false));
            DoLightGreen.All(d => d.Value.SetDo());

            //hide alarm ui
            OnAlarmEvent(string.Empty, LogLevel.None);
        }

        public void Pause()
        {
            DoLightRed.All(d => d.Value.Toggle());
            Thread.Sleep(50);
            DoLightRed.All(d => d.Value.Toggle());

            PostEvent(new UserEvent() { EventType = UserEventType.PAUSE, EventTarget = this });

            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo(false));
            DoLightGreen.All(d => d.Value.SetDo(true));
        }

        public void Continue()
        {
            DoLightGreen.All(d => d.Value.Toggle());
            Thread.Sleep(50);
            DoLightGreen.All(d => d.Value.Toggle());

            PostEvent(new UserEvent() { EventType = UserEventType.CONTINUE, EventTarget = this });

            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo(false));
            DoLightGreen.All(d => d.Value.SetDo());

            //hide alarm ui
            OnAlarmEvent(string.Empty, LogLevel.None);
        }

        public void Stop()
        {
            DoLightRed.All(d => d.Value.Toggle());
            Thread.Sleep(50);
            DoLightRed.All(d => d.Value.Toggle());

            PostEvent(new UserEvent() { EventType = UserEventType.STOP, EventTarget = this });

            DoLightRed?.All(d => d.Value.SetDo());
            DoLightYellow.All(d => d.Value.SetDo(false));
            DoLightGreen.All(d => d.Value.SetDo(false));
        }


        public void Reset()
        {
            DoLightYellow.All(d => d.Value.Toggle());
            Thread.Sleep(50);
            DoLightYellow.All(d => d.Value.Toggle());

            PostEvent(new UserEvent() { EventType = UserEventType.RESET, EventTarget = this });
            DoLightRed.All(d => d.Value.SetDo(false));
            DoLightYellow.All(d => d.Value.SetDo(true));
            DoLightGreen.All(d => d.Value.SetDo(false));

            //hide alarm ui
            OnAlarmEvent(string.Empty, LogLevel.None);
        }

        #region  state mechanism

        private Thread _mainThread;
        private bool _isrunning;

        private int _estopCount;
        private int _resettingFlashCount;
        private int _runningFlashCount;
        private bool _isPauseSignal;

        private void Update()
        {
            while (_isrunning || priorityQueueEvents.Count > 0)
            {
                runningStateMachine();

                foreach (var station in Stations)
                {
                    station.Value.runningStateMachine();
                }

                while (priorityQueueEvents.Count > 0)
                {
                    KeyValuePair<int, UserEvent> e;
                    if (priorityQueueEvents.TryDequeue(out e))
                    {
                        e.Value.Execute();
                    }
                }

                Thread.Sleep(16);
            }
        }

        private void runningStateMachine()
        {
            switch (State)
            {
                case StationState.ESTOP:
                    if (DiEstop.Count > 0 && DiEstop.All(e => e.Value.GetDiSts()))
                    {
                        exitEstop_enterError();
                    }

                    if (_estopCount++ > 100)
                    {
                        DoBuzzer.All(d => d.Value.SetDo(false));
                    }

                    break;

                case StationState.ERROR:
                    if (DiEstop.Count > 0 && DiEstop.Any(e => !e.Value.GetDiSts()))
                    {
                        enterEstop();
                    }
                    else if (DiReset.Count > 0 && DiReset.Any(e => e.Value.GetDiSts()))
                    {
                        exitError_enterAuto();
                    }

                    break;

                case StationState.MANUAL:
                    if (DiEstop.Count > 0 && DiEstop.Any(e => !e.Value.GetDiSts()))
                    {
                        enterEstop();
                    }
                    else if (DiAuto.Count > 0 && DiAuto.Any(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        exitManual_enterAuto();
                    }

                    break;

                case StationState.AUTO:
                    if (DiEstop.Count > 0 && DiEstop.Any(e => !e.Value.GetDiSts()))
                    {
                        Stop();
                        enterEstop();
                    }

                    runAutoState();
                    break;
            }
        }

        private void enterEstop()
        {
            OnAlarmEvent($"急停信号{string.Join(",", DiPauseSignal.Select(d => d.Value.Name))}触发", LogLevel.Error);
            _estopCount = 0;
            DoBuzzer.All(d => d.Value.SetDo());
            State = StationState.ESTOP;
            AutoState = StationAutoState.WaitReset;
        }

        private void exitEstop_enterError()
        {
            DoBuzzer.All(d => d.Value.SetDo(false));
            State = StationState.ERROR;
            AutoState = StationAutoState.WaitReset;
        }

        private void exitError_enterAuto()
        {
            State = StationState.AUTO;
            AutoState = StationAutoState.WaitReset;
        }

        private void exitManual_enterAuto()
        {
            State = StationState.AUTO;
            AutoState = StationAutoState.WaitReset;
        }

        private void exitAuto_enterManual()
        {
            State = StationState.MANUAL;
            AutoState = StationAutoState.WaitReset;

        }

        private void runAutoState()
        {
            switch (AutoState)
            {
                case StationAutoState.WaitReset:
                    if (DiReset.Count > 0 && DiReset.Any(e => e.Value.GetDiSts()))
                    {
                        _resettingFlashCount = 0;
                        Reset();
                        AutoState = StationAutoState.Resetting;
                    }
                    else if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        AutoState = StationAutoState.WaitReset;
                    }
                    else if (DiAuto.Count > 0 && !DiAuto.All(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        exitAuto_enterManual();
                    }

                    //pull up stations state
                    if (Stations.All(s => s.Value.AutoState == StationAutoState.Resetting))
                    {
                        _resettingFlashCount = 0;
                        DoLightYellow.All(d => d.Value.SetDo(true));
                        AutoState = StationAutoState.Resetting;
                    }

                    break;

                case StationAutoState.Resetting:
                    if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        AutoState = StationAutoState.WaitReset;
                        return;
                    }
                    else if (DiPauseSignal.Count > 0 && DiPauseSignal.Any(e => !e.Value.GetDiSts()))
                    {
                        //todo: show alarm
                        OnAlarmEvent($"安全信号{string.Join(",", DiPauseSignal.Select(d => d.Value.Name))}触发", LogLevel.Error);
                        Beep();
                        Stop();
                        AutoState = StationAutoState.WaitReset;
                        return;
                    }

                    //pull up stations state
                    if (Stations.All(s => s.Value.AutoState == StationAutoState.WaitRun))
                    {
                        DoLightYellow.All(d => d.Value.SetDo(false));
                        DoLightGreen.All(d => d.Value.SetDo(true));
                        AutoState = StationAutoState.WaitRun;
                        return;
                    }
                    else if (Stations.Any(s => s.Value.AutoState == StationAutoState.WaitReset))
                    {
                        //some station not reset success
                        Stop();
                        AutoState = StationAutoState.WaitReset;
                        return;
                    }

                    if (_resettingFlashCount++ > 40)
                    {
                        _resettingFlashCount = 0;
                        DoLightYellow.All(d => d.Value.Toggle());
                    }

                    break;

                case StationAutoState.WaitRun:
                    if (DiStart.Count > 0 && DiStart.Any(e => e.Value.GetDiSts()))
                    {
                        Start();
                        AutoState = StationAutoState.Running;

                    }
                    else if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        AutoState = StationAutoState.WaitReset;

                    }
                    else if (DiAuto.Count > 0 && !DiAuto.All(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        exitAuto_enterManual();
                    }

                    //pull up stations state
                    if (Stations.All(s => s.Value.AutoState == StationAutoState.Running))
                    {
                        _runningFlashCount = 0;
                        AutoState = StationAutoState.Running;
                    }
                    else if (Stations.Any(s => s.Value.AutoState == StationAutoState.WaitReset))
                    {
                        AutoState = StationAutoState.WaitReset;
                    }

                    break;

                case StationAutoState.Running:
                    if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    {
                        _isPauseSignal = false;
                        Pause();
                        AutoState = StationAutoState.Pause;
                        return;
                    }
                    else if (DiPauseSignal.Count > 0 && DiPauseSignal.Any(e => !e.Value.GetDiSts()))
                    {
                        //todo: show alarm
                        OnAlarmEvent($"暂停信号{string.Join(",", DiPauseSignal.Select(d => d.Value.Name))}触发", LogLevel.Warning);

                        _isPauseSignal = true;
                        Pause();
                        AutoState = StationAutoState.Pause;
                        return;
                    }

                    //pull up stations state
                    if (Stations.Any(s => s.Value.AutoState == StationAutoState.WaitReset))
                    {
                        //some station run error
                        AutoState = StationAutoState.WaitReset;
                        Stop();
                        return;
                    }
                    else if (Stations.Any(s => s.Value.AutoState == StationAutoState.Pause))
                    {
                        DoLightGreen.All(d => d.Value.SetDo());
                        AutoState = StationAutoState.Pause;
                        return;
                    }

                    if (_runningFlashCount++ > 40)
                    {
                        _runningFlashCount = 0;
                        DoLightGreen.All(d => d.Value.Toggle());
                    }

                    break;

                case StationAutoState.Pause:
                    //if (DiStop.Count > 0 && DiStop.Any(e => e.Value.GetDiSts()))
                    //{
                    //    AutoState = StationAutoState.WaitReset;
                    //    Stop();
                    //}
                    if (_isPauseSignal && DiPauseSignal.Count > 0 && DiPauseSignal.All(e => e.Value.GetDiSts()))
                    {
                        Continue();
                        AutoState = StationAutoState.Running;
                    }
                    else if (DiStart.Count > 0 && DiStart.Any(e => e.Value.GetDiSts()))
                    {
                        Continue();
                        AutoState = StationAutoState.Running;
                    }
                    else if (DiReset.Count > 0 && DiReset.Any(e => e.Value.GetDiSts()))
                    {
                        Stop();
                        AutoState = StationAutoState.WaitReset;
                    }

                    //pull up stations state
                    if (Stations.All(s => s.Value.AutoState == StationAutoState.Running))
                    {
                        _runningFlashCount = 0;
                        AutoState = StationAutoState.Running;
                    }
                    else if (Stations.Any(s => s.Value.AutoState == StationAutoState.WaitReset))
                    {
                        //if any station run fail
                        Stop();
                        AutoState = StationAutoState.WaitReset;
                    }

                    break;
            }
        }

        public void HandleEvent(UserEvent e)
        {
            foreach (var station in Stations)
            {
                station.Value.HandleEvent(e);
            }
        }

        public void PostEvent(UserEvent e)
        {
            switch (e.EventType)
            {
                case UserEventType.ALARM:
                    priorityQueueEvents.Enqueue(1, e);
                    break;
                default:
                    priorityQueueEvents.Enqueue(2, e);
                    break;
            }
        }

        private ConcurrentPriorityQueue<int, UserEvent> priorityQueueEvents = new ConcurrentPriorityQueue<int, UserEvent>();

        #endregion

        public virtual void OnAlarmEvent(string alarm, LogLevel level)
        {
            AlarmEvent?.Invoke(alarm, level);
        }


        public void Beep()
        {
            lock (this)
            {
                DoBuzzer.All(d => d.Value.SetDo(true));
                Thread.Sleep(500);
                DoBuzzer.All(d => d.Value.SetDo(false));
            }
        }

        #region  resource manage

        public Dictionary<int, IMotionWrapper> MotionExs { get; } = new Dictionary<int, IMotionWrapper>();
        public Dictionary<int, IDiEx> DiExs { get; } = new Dictionary<int, IDiEx>();
        public Dictionary<int, IDoEx> DoExs { get; } = new Dictionary<int, IDoEx>();
        public Dictionary<int, ICylinderEx> CylinderExs { get; } = new Dictionary<int, ICylinderEx>();
        public Dictionary<int, IAxisEx> AxisExs { get; } = new Dictionary<int, IAxisEx>();

        #endregion

        #region station & tasks

        public Dictionary<int, Station> Staitons { get; } = new Dictionary<int, Station>();
        public Dictionary<int, StationTask> Tasks { get; } = new Dictionary<int, StationTask>();

        #endregion
    }


  
}