using System;
using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite.Nodes
{
    public class Circuit : NodeComponent
    {
        public Circuit(string name, string inputName) : base(name, inputName)
        {
        }

        public override void Accept(IVisitor visitor)
        {
            throw new NotImplementedException();
        }

        public override void CalculateOutput()
        {
            throw new NotImplementedException();
        }
    }
}