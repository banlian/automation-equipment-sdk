using System.IO;
using Automation.FrameworkExtension.common;

namespace Automation.FrameworkExtension.frameworkManage
{
    public class FrameworkConfig : UserSettings<FrameworkConfig>
    {
        public FrameworkConfig()
        {

        }

        public void Load()
        {
            var f = @".\Config\framework.cfg";
            if (!File.Exists(f))
            {
                Save(f);
            }
            else
            {
                Ins = Load(f);
            }
        }

        public static FrameworkConfig Ins { get; private set; } = new FrameworkConfig();


        public bool IsSimulate { get; set; }

        public bool IsDebug { get; set; }


        public override bool CheckIfNormal()
        {
            return true;
        }
    }
}