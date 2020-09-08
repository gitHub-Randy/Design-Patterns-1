using System.Collections.Generic;
using CircuitSimulator.Composite;
using CircuitSimulator.Composite.Nodes;

namespace CircuitSimulator.Builder
{
    public class ANDBuilder : IBuilder
    {
        public ANDBuilder(string inputName)
        {
            Node = new ANDNode("AND", inputName);
        }

        public NodeComponent Node { get; set; }

        public void AddChildren(List<NodeComponent> childNodes)
        {
            foreach (NodeComponent childNode in childNodes) Node.AddOutputNode(childNode);
        }
    }
}