using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CircuitSimulator.Data;

namespace CircuitSimulator.Strategy
{
    public class TxtFileReaderStrategy : IFileReaderStrategy
    {
        private Dictionary<string, ArrayList> edgeDictonary;
        private bool edgeSection;
        private Dictionary<string, string> fileDictonary;
        private string[] input;
        private bool nodeSection;

        public FileData Execute(string filePath)
        {
            input = File.ReadAllLines(filePath);

            fileDictonary = new Dictionary<string, string>();
            edgeDictonary = new Dictionary<string, ArrayList>();

            foreach (string line in input)
                if (line.Contains("#") || line.Length == 0)
                {
                    if (line.Contains("nodes")) nodeSection = true;

                    if (line.Contains("edges"))
                    {
                        nodeSection = false;
                        edgeSection = true;
                    }
                }
                else
                {
                    if (nodeSection) MakeDescription(line);

                    if (edgeSection) MakeEdgeDescription(line);
                }

            return new FileData(fileDictonary, edgeDictonary);
        }

        private void MakeDescription(string line)
        {
            // takes the substring till ":"
            string descriptor = line.Substring(0, line.IndexOf(":"));
            //takes everything after ":" and trims whitespace off
            string value = line.Substring(line.IndexOf(":") + 1).Trim();
            //takes the value and trims of the ";"
            string formattedValue = value.Substring(0, value.Length - 1);
            // add descriptor(i.e.: NODE1) and formatted vlaue (i.e.: OR) and puts it in the filedictonary
            fileDictonary.Add(descriptor, formattedValue);
        }

        private void MakeEdgeDescription(string line)
        {
            // get substring till :
            string descriptor = line.Substring(0, line.IndexOf(":"));
            // get substring from : and trim all whitespace(NODE3,NODE7,NODE10;)
            string value1 = line.Substring(line.IndexOf(":") + 1).Trim();
            //counts amount of ","
            int count = value1.Count(f => f == ',');
            ArrayList edges = new ArrayList();
            for (int i = 0; i < count + 1; i++)
                // if there iss still a "," in the substring it takes the beginning of the substring till the first ","  and removes the taken portion of the substring
                if (value1.Contains(","))
                {
                    edges.Add(value1.Substring(0, value1.IndexOf(",")));
                    value1 = value1.Remove(0, value1.IndexOf(",") + 1);
                }
                // otherwise it takes everything till the end(";") of the substring
                else
                {
                    edges.Add(value1.Substring(0, value1.IndexOf(";")));
                }

            edgeDictonary.Add(descriptor, edges);
        }
    }
}