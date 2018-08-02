using System;
using System.Collections.Generic;
using System.Linq;

namespace PrereqTestUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Dictionary<string, ProductInfo> result;
                if (args.Length > 0 && args[0] == "/old")
                {
                    Console.WriteLine("Try to get products by OLD algorithm");
                    result = new OldInstalledProductsReader().GetInstalledProducts();
                }
                else
                {
                    Console.WriteLine("Try to get products by NEW algorithm. Use 'PrereqTestUtility.exe /old' to get products by old algorithm");
                    result = new InstalledProductsReader().GetInstalledProducts();
                }
                
                Console.WriteLine("Successfully got information about {0} products", result.Count());
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
                Console.WriteLine("Stack trace:\r\n" + ex);
            }
        }
    }
}
