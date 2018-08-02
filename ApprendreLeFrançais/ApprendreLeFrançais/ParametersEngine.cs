using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.Drawing;

namespace ApprendreLeFrançais
{
    public class ParametersEngine
    {
        public Parameters LoadMainFormParameters(string defaultSize, string defaultLocation, string defaultColumnsWidth)
        {
            return LoadParameters("MainForm", defaultSize, defaultLocation, defaultColumnsWidth);
        }

        public void SaveMainFormParameters(Parameters parameters)
        {
            SaveFormParameters(parameters, "MainForm");
        }

        public Parameters LoadTestFormParameters(string defaultSize, string defaultLocation, string defaultColumnsWidth)
        {
            return LoadParameters("TestForm", defaultSize, defaultLocation, defaultColumnsWidth);
        }

        public void SaveTestFormParameters(Parameters parameters)
        {
            SaveFormParameters(parameters, "TestForm");
        }

        private Parameters LoadParameters(string formName, string defaultSize, string defaultLocation, string defaultColumnsWidth)
        {
            using (RegistryKey regKey = Registry.LocalMachine.CreateSubKey("Software\\ApprendreLeFrançais\\" + formName))
            {
                var size = (string)regKey.GetValue("Size", defaultSize);
                var location = (string)regKey.GetValue("Location", defaultLocation);
                var columnsWidth = (string)regKey.GetValue("ColumnsWidth", defaultColumnsWidth);

                return new Parameters(size, location, columnsWidth);
            }
        }

        public void SaveFormParameters(Parameters parameters, string formName)
        {
            using (RegistryKey regKey = Registry.LocalMachine.CreateSubKey("Software\\ApprendreLeFrançais\\" + formName))
            {
                regKey.SetValue("Size", string.Format("{0};{1}", parameters.FormSize.Width, parameters.FormSize.Height));
                regKey.SetValue("Location", string.Format("{0};{1}", parameters.FormLocation.X, parameters.FormLocation.Y));
                regKey.SetValue("ColumnsWidth", string.Join(";", parameters.ListColumnsWidth));
            }
        }
    }
}
