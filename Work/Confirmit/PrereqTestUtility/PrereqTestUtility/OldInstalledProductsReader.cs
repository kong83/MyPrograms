using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace PrereqTestUtility
{
    public class OldInstalledProductsReader
    {
        [DllImport("msi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MsiEnumProducts(int iProductIndex, StringBuilder lpProductBuf);

        [DllImport("msi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int MsiGetProductInfo(string productCode, string property, [Out] StringBuilder valueBuf, ref int length);

        /// <summary>
        /// Get parameter for current product
        /// </summary>
        /// <param name="productCode">Product guid</param>
        /// <param name="parameterName">Parameter name we want to get</param>
        /// <param name="length">Length of parameter for winapi function</param>
        /// <returns></returns>
        private static string GetProductInfo(string productCode, string parameterName, int length)
        {
            var result = new StringBuilder(length);
            int errCode = MsiGetProductInfo(productCode, parameterName, result, ref length);
            if (errCode != 0)
            {
                return null;
            }

            return result.ToString();
        }


        /// <summary>
        /// Get information about all installed products
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, ProductInfo> GetInstalledProducts()
        {
            //
            // Pointer to a buffer that receives the product code. 
            // This buffer must be 39 characters long. The first 38 characters are for 
            // the GUID, and the last character is for the terminating null character.
            //
            const int productCodeLength = 39;
            const int productNameLength = 512;
            const int productversionInfoLength = 23;
            const int productInstallLocationLength = 1024;
            var sb = new StringBuilder(productCodeLength);

            int index = 0;
            int res;
            var result = new Dictionary<string, ProductInfo>();
            do
            {
                res = MsiEnumProducts(index, sb);
                string productCode = sb.ToString();
                index++;

                if (res != 0)
                {
                    continue;
                }

                string productName = GetProductInfo(productCode, "InstalledProductName", productNameLength);
                if (productName == null)
                {
                    continue;
                }

                string versionString = GetProductInfo(productCode, "VersionString", productversionInfoLength);
                if (versionString == null)
                {
                    continue;
                }

                string versionMajor = GetProductInfo(productCode, "VersionMajor", productversionInfoLength);
                if (versionMajor == null)
                {
                    continue;
                }

                string versionMinor = GetProductInfo(productCode, "VersionMinor", productversionInfoLength);
                if (versionMinor == null)
                {
                    continue;
                }

                string installLocation = GetProductInfo(productCode, "InstallLocation", productInstallLocationLength);
                if (installLocation == null)
                {
                    continue;
                }

                var productInfo = new ProductInfo
                {
                    ProductCode = productCode,
                    VersionString = versionString,
                    VersionMajor = Convert.ToInt32(versionMajor),
                    VersionMinor = Convert.ToInt32(versionMinor),
                    InstallLocation = installLocation
                };

                // A check for products without normal InstalledProductName information
                // If product have normal InstalledProductName information - remove dummy spaces from name
                if (string.IsNullOrEmpty(productName.Trim()))
                {
                    result[productName] = productInfo;
                }
                else
                {
                    result[productName.Trim()] = productInfo;
                }
            }
            while (res == 0);

            return result;
        }
    }
}
