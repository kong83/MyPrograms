using System;
using System.Collections;
using System.Windows.Forms;
using Microsoft.Win32;

namespace TicTacToe
{
    public partial class ServerConnectForm : Form
    {
        private readonly MainForm m_MainForm;

        public ServerConnectForm(MainForm mainForm)
        {
            InitializeComponent();

            m_MainForm = mainForm;
            m_MainForm.IpConnect = "";
            // Получить 10 последних значений из лога
            GetLastValues(comboBoxIpConnect);
        }        

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }        

       // Подключение к серверу
        private void buttonOK_Click(object sender, EventArgs e)
        {
            m_MainForm.IpConnect = comboBoxIpConnect.Text;
            SetLastValues(comboBoxIpConnect.Text);
            Close();
        }

        // Отлов нажатия кнопок на форме
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (!buttonOK.Focused && !buttonCancel.Focused)
            {
                if (keyData == Keys.Enter)
                {
                    buttonOK_Click(new object(), new EventArgs());
                    return true;
                }
                if (keyData == Keys.Escape)
                {
                    buttonCancel_Click(new object(), new EventArgs());
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        #region Сохранение и получение 10 последних введённых ip-адресов
        /// <summary>
        /// Запись десяти последних использованных значений в переданный ComboBox
        /// </summary>
        /// <param name="comboBox">ComboBox, в который записывать значения</param>
        public void GetLastValues(ComboBox comboBox)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey(@"Software\GrigoryTicTacToe\LastUsed");

                string s = "";
                if (regKey != null)
                {
                    s = (string)regKey.GetValue("ipConnect", s);
                }

                if (s != "")
                {
                    comboBox.Items.Clear();
                    ArrayList values = ParseString(s, "^#!^");
                    for (int i = 0; i < values.Count; i++)
                        comboBox.Items.Add(values[i]);
                    if (comboBox.Items.Count > 0)
                        comboBox.Text = comboBox.Items[0].ToString();
                }
            }
            catch { }
        }

        /// <summary>
        /// Добавление нового значения к списку 10-ти последних использованных значений 
        /// </summary>
        /// <param name="value">Добавляемое значение</param>
        public void SetLastValues(string value)
        {
            if (value == "")
                return;

            try
            {
                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey(@"Software\GrigoryTicTacToe\LastUsed");

                string s = "";
                if (regKey != null)
                {
                    s = (string)regKey.GetValue("ipConnect", s);

                    if (s != "")
                    {
                        ArrayList values = ParseString(s, "^#!^");

                        for (int i = 0; i < values.Count; i++)
                            if (value == (string)values[i])
                                values.RemoveAt(i);

                        s = value;
                        for (int i = 0; i < values.Count && i < 9; i++)
                            s += "^#!^" + (string)values[i];

                        regKey.SetValue("ipConnect", s);
                    }
                    else
                    {
                        regKey.SetValue("ipConnect", value);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// Разделяет строку s на подстроки, разделенные подстрокой subS
        /// </summary>
        /// <param name="s">Строка</param>
        /// <param name="subS">Подстрока для разделения</param>
        /// <returns>Массив строк, записанный в динамическом массиве</returns>
        public ArrayList ParseString(string s, string subS)
        {
            var mas = new ArrayList();
            if (subS == "")
            {
                mas.Add(s);
                return mas;
            }

            while (s != "")
            {
                int n = s.IndexOf(subS, 0);
                if (n == -1)
                {
                    mas.Add(s);
                    break;
                }
    
                mas.Add(s.Substring(0, n));
                s = s.Substring(n + subS.Length, s.Length - n - subS.Length);
            }
            return mas;
        }
        #endregion
    }
}
