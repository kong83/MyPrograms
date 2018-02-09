using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace Sudoku
{
    class SaveSettingsClass
    {
        private const string registryPath = @"Software\Sudoku";

        /// <summary>
        /// Сохранение настроек в реестре
        /// </summary>
        /// <param name="values"></param>
        public void SaveParameters(Dictionary<string, string> values)
        {            
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(registryPath);

            foreach(string key in values.Keys)
            {
                regKey.SetValue(key, values[key]);
            }
            regKey.Close();
        }


        /// <summary>
        /// Загрузка из реестра сохранённых настроек
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> LoadParameters(List<string> keys)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(registryPath);

            foreach (string key in keys)
            {
                string s = "";
                values[key] = (string)regKey.GetValue(key, (string)s);
            }
            regKey.Close();

            return values;
        }

    }
}
