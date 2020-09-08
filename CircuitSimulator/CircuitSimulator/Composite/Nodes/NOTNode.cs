using System;
using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class NOTNode : NodeComponent
    {
        public NOTNode(string name, string inputName) : base(name, inputName)
        {
            AmountInputs = 1;
        }

        public override void CalculateOutput()
        {
            Output = Values[0] switch
            {
                0 => 1,
                1 => 0,
                _ => throw new Exception("Input not supported")
            };
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}