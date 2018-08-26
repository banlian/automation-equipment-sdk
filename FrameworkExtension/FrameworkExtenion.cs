using Automation.FrameworkExtension.common;

namespace Automation.FrameworkExtension
{
    public static class FrameworkExtenion
    {
        public static bool IsSimulate { get; set; } = false;

        public static LogLevel LogLevel { get; set; } = LogLevel.Debug;
    }
}