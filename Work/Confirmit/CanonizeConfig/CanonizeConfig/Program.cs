using System;
using System.Xml;

namespace CanonizeConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "app.config";

            if (args.Length > 0)
            {
                fileName = args[0];
            }

            Console.WriteLine("Start canonize \"" + fileName + "\"");

            try
            {               
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);

                xmlDoc.Save(fileName);

                Console.WriteLine("Canonize has finished successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error was occured:\r\n" + ex);
            }
        }
    }
}
