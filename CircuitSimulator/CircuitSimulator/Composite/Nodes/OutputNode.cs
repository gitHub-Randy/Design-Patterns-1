﻿using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class OutputNode : NodeComponent
    {
        public OutputNode(string name, string inputName) : base(name, inputName)
        {
            AmountInputs = 1;
        }

        public override void CalculateOutput()
        {
            Output = Values[0] == 1 ? 1 : 0;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.VisitNode(this);
        }
    }
}