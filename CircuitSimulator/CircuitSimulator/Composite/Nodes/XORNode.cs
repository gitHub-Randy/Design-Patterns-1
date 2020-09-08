using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class XORNode : NodeComponent
    {
        public XORNode(string name, string inputName) : base(name, inputName)
        {
        }

        public override void CalculateOutput()
        {
            Output = Values[0] == Values[1] ? 0 : 1;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}