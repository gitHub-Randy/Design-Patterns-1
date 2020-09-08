using System;
using System.Linq;
using CircuitSimulator.Composite;

namespace CircuitSimulator.Visitor
{
    public class NodeVisitor : IVisitor
    {
        public void VisitNode(NodeComponent node)
        {
            Console.WriteLine(node.Name + ": ");
            if (node.OutputNodeList.Count == 0)
            {
                Console.WriteLine($"Done!!! Output was: {node.Output}" + "\n");
            }
            else
            {
                string text = $@"{node.TypeName} got input ";

                // TODO: Shorten
                text = node.Values.Aggregate(text, (current, value) => current + $@"{value}, ");
                text += "\n";

                text += $@"Sending output: {node.Output} to -> ";

                text = node.OutputNodeList.Aggregate(text,
                    (current, outNode) => current + $@"{outNode.TypeName}({outNode.Name}), ");

                Console.WriteLine(text);
            }
        }
    }
}