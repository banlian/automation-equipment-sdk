using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Automation.FrameworkExtension.platforms.motionPlatforms;

namespace Automation.FrameworkExtension.platforms.safeCheckObjects
{
    public abstract class SafeCheckObject
    {
        public SafeCheckType Target { get; protected set; }
        public bool Enable { get; set; } = true;

        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public virtual string Description { get; }

        public string Error { get; protected set; }
        public abstract bool Check(PlatformEx platform, int i);

        public override string ToString()
        {
            return $"{GetType().Name} {Target}";
        }
    }
}