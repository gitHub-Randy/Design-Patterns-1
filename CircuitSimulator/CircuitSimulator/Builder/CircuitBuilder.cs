﻿using System.Collections.Generic;
using CircuitSimulator.Composite;
using CircuitSimulator.Composite.Nodes;

namespace CircuitSimulator.Builder
{
    public class CircuitBuilder : IBuilder
    {
        public CircuitBuilder(string inputName)
        {
            Node = new Circuit("Circuit", inputName);
        }

        public NodeComponent Node { get; set; }

        public void AddChildren(List<NodeComponent> childNodes)
        {
            foreach (NodeComponent childNode in childNodes) Node.AddOutputNode(childNode);
        }
    }
}