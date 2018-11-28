using Automation.FrameworkExtension.stateMachine;

namespace Automation.FrameworkExtension.elementsInterfaces
{
    public interface IElement
    {
        int Id { get; set; }
        string Name { get; set; }

        string Export();

        void Import(string line, StateMachine machine);

    }
}
