using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class NORNode : NodeComponent
    {
        public NORNode(string name, string inputName) : base(name, inputName)
        {
        }

        public override void CalculateOutput()
        {
            if (Values[0] == 0 && Values[1] == 0)
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