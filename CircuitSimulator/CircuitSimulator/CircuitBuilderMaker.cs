using System;
using System.Collections.Generic;
using CircuitSimulator.Builder;
using CircuitSimulator.Composite;
using CircuitSimulator.Data;
using CircuitSimulator.Factory;

namespace CircuitSimulator
{
    public class CircuitBuilderMaker
    {
        private readonly BuilderFactory builderFactory;
        private readonly Dictionary<string, IBuilder> builderList;
        private readonly Dictionary<string, string> ci;
        private readonly CircuitDirector circuitDirector;
        private readonly FileData fileData;

        public CircuitBuilderMaker(FileData data, Dictionary<string, string> input)
        {
            fileData = data;
            ci = input;
            circuitDirector = new CircuitDirector();
            builderFactory = BuilderFactory.GetInstance();
            builderList = new Dictionary<string, IBuilder>();
        }

        public CircuitData GetCircuit()
        {
            string errorMessage = null;

            AddTypesToBuilderFactory();
            MakeBuilderList();

            NodeComponent circuit =
                circuitDirector.BuildCircuit(builderList, fileData.EdgeDictonary, fileData.FileDictionary, ci);

            bool noError = circuit.InfiniteloopCheck();
            if (!noError)
            {
                errorMessage = "Infinite loop detected";
            }
            else
            {
                noError = circuit.NotConnectedCheck(true);
                if (!noError) errorMessage = "Not Connected Detected";
            }

            return new CircuitData(noError, errorMessage, circuit);
        }

        private void AddTypesToBuilderFactory()
        {
            Dictionary<string, string> circuitNodes = fileData.FileDictionary;
            foreach (KeyValuePair<string, string> circuitNode in circuitNodes)
            {
                Type t;
                if (ci.ContainsKey(circuitNode.Key))
                {
                    if (ci[circuitNode.Key] == "PROBE")
                        t = typeof(OutputBuilder);
                    else
                        t = typeof(InputBuilder);

                    builderFactory.AddBuilderType(circuitNode.Key, t, ci);
                }
                else
                {
                    t = Type.GetType("CircuitSimulator.Builder." + circuitNode.Value + "Builder");

                    builderFactory.AddBuilderType(circuitNode.Value, t, null);
                }
            }
        }

        private void MakeBuilderList()
        {
            Dictionary<string, string> fd = fileData.FileDictionary;
            foreach (KeyValuePair<string, string> nodeDescription in fd)
            {
                builderList.Add(nodeDescription.Key,
                    ci.ContainsKey(nodeDescription.Key)
                        ? builderFactory.GetBuilder(nodeDescription.Key)
                        : builderFactory.GetBuilder(nodeDescription.Value));

                builderList[nodeDescription.Key].Node.TypeName = nodeDescription.Key;
            }
        }
    }
}