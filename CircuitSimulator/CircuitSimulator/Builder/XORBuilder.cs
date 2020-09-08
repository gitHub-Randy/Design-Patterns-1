﻿using System.Collections.Generic;
using CircuitSimulator.Composite;
using CircuitSimulator.Composite.Nodes;

namespace CircuitSimulator.Builder
{
    public class XORBuilder : IBuilder
    {
        public XORBuilder(string inputName)
        {
            Node = new XORNode("XOR", inputName);
        }

        public NodeComponent Node { get; set; }

        public void AddChildren(List<NodeComponent> childNodes)
        {
            foreach (NodeComponent childNode in childNodes) Node.AddOutputNode(childNode);
        }
    }
}