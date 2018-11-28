namespace Automation.FrameworkExtension.stateMachine
{
    public class UserEvent
    {
        public UserEventType EventType;

        public object EventSender;

        public IEventHandler EventTarget;

        public void Execute()
        {
            EventTarget.HandleEvent(this);
        }
    }
}