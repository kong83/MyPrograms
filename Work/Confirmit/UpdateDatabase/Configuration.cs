using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UpdateDatabase
{
    public class SplitTable
    {
        public string OriginalName { get; set; }

        public string TemplateName { get; set; }
    }

    internal sealed partial class Configuration
    {
        private string outputPath;

        public string Path
        {
            get
            {
                return System.IO.Path.GetDirectoryName(
                    Assembly.GetExecutingAssembly().Location.ToUpper());
            }
        }

        public string OutputFolder
        {
            get
            {
                return outputPath;
            }

            set
            {
                outputPath = value;

                if (!Directory.Exists(outputPath))
                {
                    Directory.CreateDirectory(outputPath);
                }
            }
        }

        public List<SplitTable> SplitTables
        {
            get
            {
                var splitTables = new List<SplitTable>();

                foreach (string splitTableDefinition in this.SplitTablesList)
                {
                    string[] splitAndTemplateName = splitTableDefinition.Split(';');

                    var splitTable = new SplitTable
                    { 
                        OriginalName = splitAndTemplateName[0], 
                        TemplateName = splitAndTemplateName[1] 
                    };

                    splitTables.Add(splitTable);
                }

                return splitTables;
            }
        }
    }
}
