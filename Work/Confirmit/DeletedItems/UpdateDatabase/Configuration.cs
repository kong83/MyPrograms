using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace UpdateDatabase
{
    class SplitTable
    {
        public string OriginalName { get; set; }
        public string TemplateName { get; set; }
    }

    internal sealed partial class Configuration
    {
        private string m_OutputPath;

        public string Path
        {
            get
            {
                return System.IO.Path.GetDirectoryName(
// ReSharper disable PossibleNullReferenceException
                    Assembly.GetExecutingAssembly().Location.ToUpper());
// ReSharper restore PossibleNullReferenceException
            }
        }

        public string OutputFolder
        {
            get
            {
                return m_OutputPath;
            }
            set
            {
                m_OutputPath = value;

                if (!Directory.Exists(m_OutputPath))
                    Directory.CreateDirectory(m_OutputPath);
            }
        }

        public List<SplitTable> SplitTables
        {
            get
            {
                var splitTables = new List<SplitTable>();

                foreach(string splitTableDefinition in SplitTablesList)
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
