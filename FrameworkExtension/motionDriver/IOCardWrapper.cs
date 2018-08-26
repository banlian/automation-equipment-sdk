using System;

namespace Automation.FrameworkExtension.motionDriver
{
    public class IOCardWrapper : IGioWrapper
    {
        private readonly dynamic _motion;


        public int Id;

        public int Index;

        public string Name;

        public IOCardWrapper(IMotionCard motion)
        {
            Index = _motion.DeviceID;
            _motion = motion;
        }

        public void Init(string file)
        {
        }

        public void Uninit()
        {
        }

        public void GetDi(int port, out int status)
        {
            _motion.GetDi(Index, 0, port, out status);
        }

        public void SetDi(int port, int status)
        {
            throw new NotImplementedException();
        }


        public void SetDo(int port, int status)
        {
            _motion.SetDo(Index, 0, port, status);
        }

        public void GetDo(int port, out int status)
        {
            _motion.GetDo(Index, 0, port, out status);
        }
    }
}