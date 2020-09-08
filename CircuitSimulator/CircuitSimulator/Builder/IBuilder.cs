using System.Collections.Generic;
using CircuitSimulator.Composite;

namespace CircuitSimulator.Builder
{
    public interface IBuilder
    {
        public NodeComponent Node { get; set; }

        public void AddChildren(List<NodeComponent> childNodes);
    }
}