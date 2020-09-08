using System.Collections.Generic;
using CircuitSimulator.Composite.Nodes;
using CircuitSimulator.Visitor;

namespace CircuitSimulator.Composite
{
    public abstract class NodeComponent
    {
        protected NodeComponent(string name, string typeName)
        {
            OutputNodeList = new List<NodeComponent>();
            Values = new List<int>();

            Name = name;
            TypeName = typeName;
        }

        public List<NodeComponent> OutputNodeList { get; set; }
        public List<int> Values { get; }
        public string Name { get; }
        public string TypeName { get; set; }
        public int AmountInputs { get; set; } = 2;
        public int Output { get; set; }

        public bool SetValue(int value)
        {
            if (Values.Count < AmountInputs)
                Values.Add(value);
            else
                return false;

            if (Values.Count == AmountInputs)
            {
                CalculateOutput();
                SendOutputToChildren();
            }

            return true;
        }

        public void SendOutputToChildren()
        {
            Accept(new NodeVisitor());

            foreach (NodeComponent outNode in OutputNodeList) outNode.SetValue(Output);
        }

        public void ResetCircuit()
        {
            Values.Clear();

            foreach (NodeComponent outNode in OutputNodeList) outNode.ResetCircuit();
        }

        public abstract void CalculateOutput();

        public abstract void Accept(IVisitor visitor);

        #region ErrorCheck

        public bool NotConnectedCheck(bool noError)
        {
            if (!noError) return false;

            if (OutputNodeList.Count == 0 && !(this is OutputNode))
                noError = false;
            else
                foreach (NodeComponent outNode in OutputNodeList)
                    noError = outNode.NotConnectedCheck(noError);

            return noError;
        }

        public bool InfiniteloopCheck()
        {
            // Check self
            bool noError = CheckOutputNodes(this);

            if (!noError) return false;

            // Check children
            if (OutputNodeList.Count != 0)
                noError = CheckChildrenOuputNodes(noError, this);
            else
                return true;

            if (!noError) return false;

            foreach (NodeComponent node in OutputNodeList) noError = node.InfiniteloopCheck();

            return noError;
        }

        private bool CheckChildrenOuputNodes(bool noError, NodeComponent nodeComponent)
        {
            if (!noError) return false;

            if (OutputNodeList.Count == 0)
                return true;

            foreach (NodeComponent node in OutputNodeList) noError = node.CheckOutputNodes(this);

            if (!noError) return false;

            foreach (NodeComponent node in OutputNodeList)
            {
                noError = node.CheckChildrenOuputNodes(noError, nodeComponent);

                if (!noError) break;
            }

            return noError;
        }

        private bool CheckOutputNodes(NodeComponent nodeComponent)
        {
            foreach (NodeComponent node in OutputNodeList)
                if (nodeComponent == node)
                    return false;

            return true;
        }

        #endregion

        #region Ouput

        public void AddOutputNode(NodeComponent nodeComponent)
        {
            OutputNodeList.Add(nodeComponent);
        }

        public void RemoveOutputNode(NodeComponent nodeComponent)
        {
            OutputNodeList.Remove(nodeComponent);
        }

        #endregion
    }
}