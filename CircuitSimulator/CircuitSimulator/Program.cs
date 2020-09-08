using CircuitSimulator.Data;
using CircuitSimulator.FileReading;
using CircuitSimulator.Mediator;
using CircuitSimulator.Strategy;

namespace CircuitSimulator
{
    public class Program
    {
        private static void Main()
        {
            FileReaderContext fileReaderContext = new FileReaderContext();
            LineInterpreter lineInterpreter = new LineInterpreter();

            SimulatorMediator sim = new SimulatorMediator(fileReaderContext, lineInterpreter);
            sim.Notify(null, OperationEnum.StartSimulation);
        }
    }
}