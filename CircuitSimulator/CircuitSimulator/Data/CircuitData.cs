using CircuitSimulator.Composite;

namespace CircuitSimulator.Data
{
    public class CircuitData
    {
        public CircuitData(bool noError, string error, NodeComponent nodeComponent)
        {
            NoError = noError;
            ErrorMessage = error;
            NodeComponent = nodeComponent;
        }

        public bool NoError { get; }
        public string ErrorMessage { get; }
        public NodeComponent NodeComponent { get; }
    }
}