namespace Automation.FrameworkExtension.stateMachine
{
    public class UserEvent
    {
        public UserEventType EventType;

        public object EventSender;

        public IEventHandler EventTarget;

        public object EventArgs;


        public void Execute()
        {
            EventTarget.HandleEvent(this);
        }
    }
}