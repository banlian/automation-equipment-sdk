using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MachineUtilityLib
{
    public class TestProcessControl<TData>
    {
        public event Action<TData> TestStartEvent;
        public event Action<TData> TestingEvent;
        public event Action<TData> TestFinishEvent;

        public virtual void OnTestStartEvent(TData obj)
        {
            TestStartEvent?.Invoke(obj);
        }

        public virtual void OnTestingEvent(TData obj)
        {
            TestingEvent?.Invoke(obj);
        }

        public virtual void OnTestFinishEvent(TData obj)
        {
            TestFinishEvent?.Invoke(obj);
        }
    }
}
