using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CircuitSimulator.Mediator;

namespace CircuitSimulator.FileReading
{
    public class LineInterpreter : BaseComponent
    {
        public LineInterpreter()
        {
            CircuitBlueprint = new List<KeyValuePair<string, ArrayList>>();
            CircuitInputs = new Dictionary<string, string>();
        }

        private Dictionary<string, string> FileDictonary { get; set; }
        private Dictionary<string, ArrayList> EdgeDictonary { get; set; }
        public List<KeyValuePair<string, ArrayList>> CircuitBlueprint { get; set; }
        public Dictionary<string, string> CircuitInputs { get; set; }

        public void InterpretData(Dictionary<string, string> fd, Dictionary<string, ArrayList> ed)
        {
            FileDictonary = fd;
            EdgeDictonary = ed;

            // make inputsDictonary
            foreach ((string key, string value) in FileDictonary)
                if (CheckIfInput(key))
                    CircuitInputs.Add(key, value);

            for (int i = 0; i < EdgeDictonary.Count; i++)
                CircuitBlueprint.Add(AddToBlueprint(EdgeDictonary.ElementAt(i).Key, EdgeDictonary.ElementAt(i).Value));
        }

        private KeyValuePair<string, ArrayList> AddToBlueprint(string key, IEnumerable value)
        {
            ArrayList translatedValues = new ArrayList();

            string translatedKey = !CheckIfInput(key) ? FileDictonary[key] : key;
            //translate value arraylist
            foreach (object listValue in value)
                translatedValues.Add(!CheckIfInput(listValue.ToString())
                    ? FileDictonary[listValue.ToString()]
                    : listValue.ToString());
            KeyValuePair<string, ArrayList> list = new KeyValuePair<string, ArrayList>(translatedKey, translatedValues);
            return list;
        }

        private bool CheckIfInput(string key)
        {
            return !key.Contains("NODE");
        }
    }
}