using CircuitSimulator.Data;

namespace CircuitSimulator.Mediator
{
    public interface IMediator
    {
        void Notify(object sender, OperationEnum ev);
    }
}