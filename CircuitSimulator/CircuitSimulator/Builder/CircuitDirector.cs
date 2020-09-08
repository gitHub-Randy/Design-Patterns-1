using System.Collections;
using System.Collections.Generic;
using CircuitSimulator.Composite;

namespace CircuitSimulator.Builder
{
    public class CircuitDirector
    {
        private readonly Dictionary<string, IBuilder> usedBuilders = new Dictionary<string, IBuilder>();
        private Dictionary<string, IBuilder> builderList;
        private string currentNodeName;
        private Dictionary<string, string> fileDictionary;
        private Dictionary<string, string> inputDictionary;

        public NodeComponent BuildCircuit(Dictionary<string, IBuilder> builders,
            Dictionary<string, ArrayList> blueprint, Dictionary<string, string> fileDictonary,
            Dictionary<string, string> inputDictionary)
        {
            List<IBuilder> nodes = BuildAllNodes(builders, blueprint, fileDictonary, inputDictionary);
            CircuitBuilder circuit = new CircuitBuilder("Circuit");
            List<NodeComponent> inputNodes = new List<NodeComponent>();
            foreach (IBuilder inputNode in nodes) inputNodes.Add(inputNode.Node);

            circuit.AddChildren(inputNodes);
            return circuit.Node;
        }

        public List<IBuilder> BuildAllNodes(Dictionary<string, IBuilder> builders,
            Dictionary<string, ArrayList> blueprint, Dictionary<string, string> fileDictonary,
            Dictionary<string, string> inputDictionary)
        {
            fileDictionary = fileDictonary;
            this.inputDictionary = inputDictionary;
            builderList = builders;
            foreach (KeyValuePair<string, ArrayList> blueprintIndex in blueprint)
            {
                IBuilder parentBuilder = GetParentBuilder(blueprintIndex);
                List<IBuilder> childBuilders = GetChildBuilders(blueprintIndex);
                AddChildrenToParent(parentBuilder, childBuilders);
            }

            List<IBuilder> inputBuilders = new List<IBuilder>();
            foreach (KeyValuePair<string, IBuilder> builder in usedBuilders)
            {
                IBuilder tempB = builder.Value;

                if (tempB is InputBuilder) inputBuilders.Add(builder.Value);
            }

            return inputBuilders;
        }

        private IBuilder GetParentBuilder(KeyValuePair<string, ArrayList> blueprintIndex)
        {
            IBuilder parentBuilder = null;

            currentNodeName = blueprintIndex.Key;
            if (usedBuilders.ContainsKey(blueprintIndex.Key))
                return usedBuilders[blueprintIndex.Key];
            foreach (KeyValuePair<string, IBuilder> builder in builderList)
                if (builder.Value.Node.Name == blueprintIndex.Key)
                    parentBuilder = builder.Value;
                else if (builder.Value.Node.Name == TranslateDescriptorToConcreteName(blueprintIndex.Key))
                    parentBuilder = builder.Value;

            usedBuilders.Add(currentNodeName, parentBuilder);

            return parentBuilder;
        }

        private List<IBuilder> GetChildBuilders(KeyValuePair<string, ArrayList> ed)
        {
            List<IBuilder> childNodes = new List<IBuilder>();
            foreach (string childNod in ed.Value)
            {
                IBuilder childBuilder;
                //get child and add to childList
                if (usedBuilders.ContainsKey(childNod))
                {
                    childBuilder = usedBuilders[childNod];
                    childNodes.Add(childBuilder);
                }
                else
                {
                    currentNodeName = childNod;
                    foreach (KeyValuePair<string, IBuilder> builder in builderList)
                        if (builder.Key == childNod)
                        {
                            childBuilder = builder.Value;
                            childNodes.Add(childBuilder);
                            usedBuilders.Add(currentNodeName, childBuilder);
                        }
                }
            }

            return childNodes;
        }

        private void AddChildrenToParent(IBuilder parentBuilder, List<IBuilder> childBuilders)
        {
            List<NodeComponent> childrenBuilders = new List<NodeComponent>();
            foreach (IBuilder childBuilder in childBuilders) childrenBuilders.Add(childBuilder.Node);

            parentBuilder.AddChildren(childrenBuilders);
        }

        private string TranslateDescriptorToConcreteName(string descriptor)
        {
            string translatedName = "";
            foreach (KeyValuePair<string, string> entry in fileDictionary)
                if (inputDictionary.ContainsKey(descriptor))
                    return entry.Key;
                else if (entry.Key == descriptor) return entry.Value;

            return translatedName;
        }
    }
}