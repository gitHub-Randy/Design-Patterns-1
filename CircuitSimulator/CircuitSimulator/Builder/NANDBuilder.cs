using System.Collections.Generic;
using CircuitSimulator.Composite;
using CircuitSimulator.Composite.Nodes;

namespace CircuitSimulator.Builder
{
    public class NANDBuilder : IBuilder
    {
        public NANDBuilder(string inputName)
        {
            Node = new NANDNode("NAND", inputName);
        }

        public NodeComponent Node { get; set; }

        public void AddChildren(List<NodeComponent> childNodes)
        {
            foreach (NodeComponent childNode in childNodes) Node.AddOutputNode(childNode);
        }
    }
}