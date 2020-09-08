using System;
using System.Collections.Generic;
using CircuitSimulator.Builder;

namespace CircuitSimulator.Factory
{
    public class BuilderFactory
    {
        private static BuilderFactory instance;
        private static readonly object padlock = new object();
        private readonly Dictionary<string, Type> types;
        private IBuilder builder;

        public BuilderFactory()
        {
            types = new Dictionary<string, Type>();
        }

        public static BuilderFactory GetInstance()
        {
            {
                lock (padlock)
                {
                    if (instance == null) instance = new BuilderFactory();
                    return instance;
                }
            }
        }

        public void AddBuilderType(string name, Type type, Dictionary<string, string> inputs)
        {
            if (inputs != null && inputs.ContainsKey(name))
            {
                if (inputs[name] == "PROBE")
                    types[name] = typeof(OutputBuilder);
                else
                    types[name] = typeof(InputBuilder);
            }

            types[name] = type;
        }

        public IBuilder GetBuilder(string type)
        {
            Type t = types[type];
            if (t == typeof(InputBuilder) || t == typeof(OutputBuilder))
                builder = (IBuilder) Activator.CreateInstance(t, type, type);
            else
                builder = (IBuilder) Activator.CreateInstance(t, type);
            return builder;
        }
    }
}