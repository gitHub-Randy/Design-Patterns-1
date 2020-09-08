using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class ORNode : NodeComponent
    {
        public ORNode(string name, string inputName) : base(name, inputName)
        {
        }

        public override void CalculateOutput()
        {
            if (Values[0] == 1 || Values[1] == 1)
                Output = 1;
            else
                Output = 0;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}