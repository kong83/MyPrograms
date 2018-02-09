using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace Checkers
{
    public class RegistryClass
    {
        /// <summary>
        /// Указатель на родительскую форму
        /// </summary>
        MainForm mf;

        /// <summary>
        /// Можно ли использовать реестр
        /// </summary>
        private bool useRegister = true;

        /// <summary>
        /// Путь в реестре
        /// </summary>
        private string regPath = "Software\\Checkers\\";

        public RegistryClass(MainForm mainForm)
        {
            mf = mainForm;
        }

        /// <summary>
        /// Сохранение параметров
        /// </summary>
        public void SaveParameters()
        {
            if (!useRegister)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey(regPath);

            // Сохранение изменяемых параметров
            OrderGame typeOrd = mf.typeOrder;
            regKey.SetValue("typeOrder", typeOrd.ToString());
        }

        /// <summary>
        /// Загрузка параметров из реестра
        /// </summary>
        /// <returns>Структура, содержащая все загруженные параметры</returns>
        public void LoadParameter()
        {
            mf.typeOrder = OrderGame.UserUser;

            if (!useRegister)
                return;

            RegistryKey regKey;
            string s;
            // Чтение значений из реестра
            s = "";
            try
            {
                regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey(regPath);
                s = (string)regKey.GetValue("typeOrder", s);
            }
            catch
            {
                useRegister = false;
                return;
            }
            if (s == OrderGame.CompComp.ToString())
            {
                mf.typeOrder = OrderGame.CompComp;
            }
            else if (s == OrderGame.CompUser.ToString())
            {
                mf.typeOrder = OrderGame.CompUser;
            }
            else if (s == OrderGame.UserComp.ToString())
            {
                mf.typeOrder = OrderGame.UserComp;
            }
            else
            {
                mf.typeOrder = OrderGame.UserUser;
            }
        }
    }
}
