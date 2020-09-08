using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class NANDNode : NodeComponent
    {
        public NANDNode(string name, string inputName) : base(name, inputName)
        {
        }

        public override void CalculateOutput()
        {
            if (Values[0] == 1 && Values[1] == 1)
                Output = 0;
            else
                Output = 1;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}