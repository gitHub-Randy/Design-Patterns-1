using System.Collections.Generic;
using CircuitSimulator.Composite;
using CircuitSimulator.Composite.Nodes;

namespace CircuitSimulator.Builder
{
    public class NOTBuilder : IBuilder
    {
        public NOTBuilder(string inputName)
        {
            Node = new NOTNode("NOT", inputName);
        }

        public NodeComponent Node { get; set; }

        public void AddChildren(List<NodeComponent> childNodes)
        {
            foreach (NodeComponent childNode in childNodes) Node.AddOutputNode(childNode);
        }
    }
}