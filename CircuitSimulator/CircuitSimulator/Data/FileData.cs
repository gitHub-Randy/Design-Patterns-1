using System.Collections;
using System.Collections.Generic;

namespace CircuitSimulator.Data
{
    public class FileData
    {
        public FileData(Dictionary<string, string> fileDictionary, Dictionary<string, ArrayList> edgeDictonary)
        {
            FileDictionary = new Dictionary<string, string>();
            EdgeDictonary = new Dictionary<string, ArrayList>();

            FileDictionary = fileDictionary;
            EdgeDictonary = edgeDictonary;
        }

        public Dictionary<string, string> FileDictionary { get; }
        public Dictionary<string, ArrayList> EdgeDictonary { get; }
    }
}