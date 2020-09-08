using System;
using System.Collections.Generic;
using System.Linq;
using CircuitSimulator.Composite;
using CircuitSimulator.Data;
using CircuitSimulator.FileReading;
using CircuitSimulator.Strategy;

namespace CircuitSimulator.Mediator
{
    public class SimulatorMediator : IMediator
    {
        private CircuitData _circuitData;
        private Dictionary<string, string> _circuitInputs;

        private FileData _fileData;

        // Componenten
        private readonly FileReaderContext _fileReaderContext;
        private readonly LineInterpreter _lineInterpreter;

        // Variables
        private string userPathInput;

        public SimulatorMediator(FileReaderContext fileReaderContext, LineInterpreter lineInterpreter)
        {
            _fileReaderContext = fileReaderContext;
            _lineInterpreter = lineInterpreter;
            fileReaderContext.SetMediator(this);
        }

        public void Notify(object sender, OperationEnum ev)
        {
            if (ev == OperationEnum.StartSimulation)
            {
                Console.WriteLine(@"Please, provide a path to the circuit file!");
                userPathInput = Console.ReadLine();

                Notify(this, OperationEnum.CheckedFile);
            }

            if (ev == OperationEnum.CheckedFile)
            {
                _fileReaderContext.CheckFile(userPathInput);

                if (_fileReaderContext.fileIsValid && _fileReaderContext.fileType != null)
                    Notify(this, OperationEnum.InterpretFile);
                else
                    Notify(this, OperationEnum.RestartSimulation);
            }

            if (ev == OperationEnum.InterpretFile)
            {
                _fileData = _fileReaderContext.ReadFile(userPathInput);

                _lineInterpreter.InterpretData(_fileData.FileDictionary, _fileData.EdgeDictonary);
                _circuitInputs = _lineInterpreter.CircuitInputs;

                Notify(this, OperationEnum.BuildCircuit);
            }

            if (ev == OperationEnum.BuildCircuit)
            {
                CircuitBuilderMaker cbm = new CircuitBuilderMaker(_fileData, _circuitInputs);
                _circuitData = cbm.GetCircuit();

                if (!_circuitData.NoError)
                {
                    Console.WriteLine(_circuitData.ErrorMessage);
                    Notify(this, OperationEnum.RestartSimulation);
                }

                Notify(this, OperationEnum.RunCircuit);
            }

            if (ev == OperationEnum.RunCircuit)
            {
                foreach (NodeComponent nodeInput in _circuitData.NodeComponent.OutputNodeList)
                    nodeInput.SetValue(SetInputs(nodeInput.Name));

                Notify(this, OperationEnum.InputUser);
            }

            if (ev == OperationEnum.InputUser)
            {
                Console.WriteLine("Use on of the following commands: \"restart\" \"reset\" \"quit\"");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case CommandHandler.RESTART:
                        Notify(this, OperationEnum.RestartSimulation);
                        break;
                    case CommandHandler.RESET:
                        foreach (NodeComponent nodeInput in _circuitData.NodeComponent.OutputNodeList)
                            nodeInput.ResetCircuit();
                        Console.Clear();
                        Notify(this, OperationEnum.RunCircuit);
                        break;
                    case CommandHandler.QUIT:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        Notify(this, OperationEnum.InputUser);
                        break;
                }
            }

            if (ev == OperationEnum.RestartSimulation)
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to restart application");
                Console.ReadKey();
                Console.Clear();

                Notify(this, OperationEnum.StartSimulation);
            }
        }

        private int SetInputs(string name)
        {
            return _circuitInputs.Where(input => input.Key == name)
                .Select(input => input.Value switch
                {
                    "INPUT_HIGH" => 1,
                    "INPUT_LOW" => 0,
                    _ => throw new Exception("ERROR WITH INPUT")
                })
                .FirstOrDefault();
        }
    }
}