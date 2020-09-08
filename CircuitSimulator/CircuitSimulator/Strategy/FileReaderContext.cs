using System;
using System.IO;
using CircuitSimulator.Data;
using CircuitSimulator.Factory;
using CircuitSimulator.Mediator;

namespace CircuitSimulator.Strategy
{
    public class FileReaderContext : BaseComponent
    {
        public bool fileIsValid;
        public string fileType;
        private IFileReaderStrategy strategy;
        private StrategyFactory strategyFactory;

        public FileData ReadFile(string filePath)
        {
            return strategy.Execute(filePath);
        }

        public void CheckFile(string inputFilePath)
        {
            if (!File.Exists(inputFilePath))
            {
                Console.WriteLine("File does not exist!");
                fileIsValid = false;
                return;
            }

            fileType = GetFileType(inputFilePath);

            if (fileType == null)
            {
                Console.WriteLine("No file type detected!");
                fileIsValid = false;
                return;
            }

            if (!SetStrategy())
            {
                Console.WriteLine("File type not supported!");
                fileIsValid = false;
                return;
            }

            fileIsValid = true;
        }

        private bool SetStrategy()
        {
            strategyFactory = StrategyFactory.GetInstance();

            string formattenFileTypeExtension = fileType.Substring(0, 1).ToUpper() + fileType.Substring(1);
            Type t = Type.GetType("CircuitSimulator.Strategy." + formattenFileTypeExtension + "FileReaderStrategy");

            if (t == null)
                return false;

            strategyFactory.AddStrategyType(formattenFileTypeExtension, t);
            IFileReaderStrategy concretStrategy = strategyFactory.GetStrategy(formattenFileTypeExtension);

            strategy = concretStrategy;
            return true;
        }

        private string GetFileType(string filePath)
        {
            string type = filePath.Substring(filePath.LastIndexOf('.') + 1);
            return type == filePath ? null : type;
        }
    }
}