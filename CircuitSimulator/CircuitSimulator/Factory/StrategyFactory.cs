using System;
using System.Collections.Generic;
using CircuitSimulator.Strategy;

namespace CircuitSimulator.Factory
{
    public class StrategyFactory
    {
        private static StrategyFactory instance;
        private static readonly object padlock = new object();
        private readonly Dictionary<string, Type> types;
        private IFileReaderStrategy strategy;

        public StrategyFactory()
        {
            types = new Dictionary<string, Type>();
        }

        public static StrategyFactory GetInstance()
        {
            {
                lock (padlock)
                {
                    if (instance == null) instance = new StrategyFactory();

                    return instance;
                }
            }
        }

        public void AddStrategyType(string name, Type type)
        {
            types[name] = type;
        }

        public IFileReaderStrategy GetStrategy(string type)
        {
            Type t = types[type];

            strategy = (IFileReaderStrategy) Activator.CreateInstance(t);

            return strategy;
        }
    }
}