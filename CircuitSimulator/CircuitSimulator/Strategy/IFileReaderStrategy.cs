using CircuitSimulator.Data;

namespace CircuitSimulator.Strategy
{
    public interface IFileReaderStrategy
    {
        FileData Execute(string filePath);
    }
}