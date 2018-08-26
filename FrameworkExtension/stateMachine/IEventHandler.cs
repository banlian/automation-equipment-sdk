namespace Automation.FrameworkExtension.stateMachine
{
    public interface IEventHandler
    {
        void HandleEvent(UserEvent e);
    }
}