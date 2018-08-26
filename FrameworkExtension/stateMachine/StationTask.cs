using System;
using System.Threading;
using Automation.FrameworkExtension.common;

namespace Automation.FrameworkExtension.stateMachine
{
    public abstract class StationTask : BaseObject, IEventHandler
    {
        public event Action<string, LogLevel> LogEvent;

        public Station Station { get; set; }

        protected StationTask(int id, string name, Station station)
        {
            Id = id;
            Name = name;

            Station = station;
            if (station != null)
            {
                station.Tasks.Add(id, this);
                LogEvent += station.ShowAlarm;
            }

            IsRunning = false;
            IsPause = false;

            State = TaskState.None;
        }


        #region task state machine run

        public void HandleEvent(UserEvent e)
        {
            switch (e.EventType)
            {
                case UserEventType.START:
                    Start();
                    break;
                case UserEventType.STOP:
                    Stop();
                    break;
                case UserEventType.RESET:
                    Reset();
                    break;
                case UserEventType.PAUSE:
                    Pause();
                    break;
                case UserEventType.CONTINUE:
                    Continue();
                    break;
            }
        }

        public void Start()
        {
            if (IsRunning)
            {
                return;
            }

            if (_thread != null)
            {
                ThrowException($"{Name} not Stop Normally!");
            }

            _thread = new Thread(Running);
            _thread.IsBackground = true;
            _thread.Start();
        }
        public void Stop()
        {
            if (IsRunning || IsPause)
            {
                IsRunning = false;
                if (_thread != null)
                {
                    _thread.Join();
                    _thread = null;
                    IsPause = false;
                }
            }
        }

        public void Pause()
        {
            if (IsRunning)
            {
                IsPause = true;
            }
        }

        public void Continue()
        {
            if (IsRunning && IsPause)
            {
                IsPause = false;
            }
        }

        public void Reset()
        {
            if (IsRunning)
            {
                return;
            }
            if (_thread != null)
            {
                //should not run to here
                ThrowException($"{Name} not Stop Normally!");
            }
            _thread = new Thread(Resetting);
            _thread.IsBackground = true;
            _thread.Start();
        }

        private Thread _thread;

        public bool IsRunning { get; protected set; }

        public bool IsPause { get; protected set; }

        public TaskState State { get; protected set; }

        private void Resetting()
        {
            try
            {
                State = TaskState.Resetting;
                IsRunning = true;

                Log($"{Name} ResetLoop Start...", LogLevel.Debug);
                ResetLoop();
                Log($"{Name} ResetLoop Finish", LogLevel.Debug);

                State = TaskState.WaitRun;
            }
            catch (Exception ex)
            {
                Log($"[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Debug);
                State = TaskState.WaitReset;
                foreach (var t in Station.Tasks)
                {
                    t.Value.IsRunning = false;
                }
                if (!(ex is TaskCancelException))
                {
                    Log($"[{Station.Name}-{Station.Id}]:[{Name}-{Id}]:{ex.Message}", LogLevel.Error);
                    //MessageBox.Show($"复位异常:{Station.Name}-{Name}-{Id}-{ex.Message}", "复位异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                IsPause = false;
                IsRunning = false;
                _thread = null;
            }
        }

        private void Running()
        {
            try
            {
                State = TaskState.Running;
                IsRunning = true;

                while (IsRunning)
                {
                    Log($"{Name} RunLoop Start...", LogLevel.Debug);
                    if (RunLoop() != 0)
                    {
                        Log($"{Name} RunLoop Break", LogLevel.Debug);
                        break;
                    }
                    Log($"{Name} RunLoop Finish", LogLevel.Debug);
                }
            }
            catch (Exception ex)
            {
                Log($"{Station.Name}-{Station.Id}:{Name}-{Id}:{ex.Message}", LogLevel.Debug);
                State = TaskState.WaitReset;
                foreach (var t in Station.Tasks)
                {
                    t.Value.IsRunning = false;
                }

                if (!(ex is TaskCancelException))
                {
                    Log($"{Station.Name}-{Station.Id}:{Name}-{Id}:{ex.Message}", LogLevel.Error);
                    //MessageBox.Show($"运行异常:{Station.Name}-{Name}-{Id}-{ex.Message}", "运行异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                State = TaskState.WaitReset;
                IsPause = false;
                IsRunning = false;
                _thread = null;
            }
        }

        #endregion

        protected virtual int ResetLoop()
        {
            return 0;
        }

        protected virtual int RunLoop()
        {
            return 0;
        }

        public virtual void Log(string log, LogLevel level = LogLevel.Debug)
        {
            LogEvent?.Invoke(log, level);
        }


        public void JoinIfPause()
        {
            if (IsPause)
            {
                while (IsPause)
                {
                    AbortIfCancel("JoinIfPause");
                    Thread.Sleep(1);
                }
            }
        }

        /// <summary>
        /// check if task is cancelled
        /// </summary>
        /// <param name="msg"></param>
        public void AbortIfCancel(string msg)
        {
            if (!IsRunning)
            {
                throw new TaskCancelException(this, msg);
            }
        }

        /// <summary>
        /// end the task by throw exception
        /// </summary>
        /// <param name="msg"></param>
        public void ThrowException(string msg = null)
        {
            throw new Exception(msg);
        }
    }
}