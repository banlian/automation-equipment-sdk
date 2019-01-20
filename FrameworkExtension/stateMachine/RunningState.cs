namespace Automation.FrameworkExtension.stateMachine
{
    public enum RunningState
    {
        None,

        WaitReset,
        Resetting,

        WaitRun,
        Running,
        Pause,
    }
}