using CircuitSimulator.Composite;

namespace CircuitSimulator.Visitor
{
    public interface IVisitor
    {
        void VisitNode(NodeComponent node);
    }
}