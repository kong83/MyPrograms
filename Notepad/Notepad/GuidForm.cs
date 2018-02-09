using System;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Notepad
{
    public partial class GuidForm : Form
    {
        public GuidForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Close button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Create involved guid from normal guid
        /// </summary>
        /// <param name="guid">Normal guid</param>
        /// <returns></returns>
        private static string CreateInvolvedGuid(string guid)
        {
            const string wrongGuidErrorMessage = "Гуид должен быть вида\r\n{XXXXXXXX-XXXX-XXXX-XXXX-XXXXXXXXXXXX}\r\n(можно без {} )";

            string[] guidComponents = guid.Replace("{", "").Replace("}", "").Replace(" ", "").ToUpper().Split(new[] { "-" }, StringSplitOptions.None);

            new Guid(guid);

            if (guidComponents.Length != 5)
            {
                throw new Exception(wrongGuidErrorMessage);
            }

            string involvedGuid = "";
            string involvedComponent;

            for (int i = 0; i < 3; i++)
            {
                involvedComponent = "";
                foreach (char ch in guidComponents[i])
                {
                    involvedComponent = ch + involvedComponent;
                }

                involvedGuid += involvedComponent;
            }

            for (int i = 3; i < 5; i++)
            {
                involvedComponent = "";
                if (guidComponents[i].Length % 2 != 0)
                {
                    throw new Exception(wrongGuidErrorMessage); 
                }

                for (int j = 0; j < guidComponents[i].Length; j += 2)
                {
                    involvedComponent += guidComponents[i][j + 1].ToString() + guidComponents[i][j];
                }

                involvedGuid += involvedComponent;
            }

            return involvedGuid;
        }


        /// <summary>
        /// Create normal guid from involved guid
        /// </summary>
        /// <param name="involvedGuid"></param>
        /// <returns></returns>
        private static string CreateGuid(string involvedGuid)
        {
            string guid = "";

            for (int i = 7; i >= 0; i--)
            {
                guid += involvedGuid[i].ToString();
            }
            guid += "-";
            
            for (int i = 11; i >= 8; i--)
            {
                guid += involvedGuid[i].ToString();
            }
            guid += "-";

            for (int i = 15; i >= 12; i--)
            {
                guid += involvedGuid[i].ToString();
            }
            guid += "-";

            for (int i = 16; i <= 19; i+=2)
            {
                guid += involvedGuid[i + 1].ToString() + involvedGuid[i];
            }
            guid += "-";
            for (int i = 20; i <= 31; i += 2)
            {
                guid += involvedGuid[i + 1].ToString() + involvedGuid[i];
            }          

            return guid;
        }


        /// <summary>
        /// Convert button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConvert_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxInvolvedGuid.Text = CreateInvolvedGuid(textBoxGuid.Text);
            }
            catch (Exception ex)
            {
                textBoxInvolvedGuid.Text = ex.Message;
                textBoxGuid.Focus();
            }
        }


        /// <summary>
        /// Back convert button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonConvertBack_Click(object sender, EventArgs e)
        {            
            try
            {
                if (textBoxInvolvedGuid.Text.Length != 32)
                {
                    throw new Exception("Длина \"хитрого\" гуида гуида должна составлять 32 символа");
                }

                string guid = CreateGuid(textBoxInvolvedGuid.Text);
                new Guid(guid);
                textBoxGuid.Text = guid;
            }
            catch (Exception ex)
            {
                textBoxGuid.Text = ex.Message;
                textBoxInvolvedGuid.Focus();
            }
        }


        /// <summary>
        /// Check button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheck_Click(object sender, EventArgs e)
        {            
            try
            {
                if (string.IsNullOrEmpty(textBoxInvolvedGuid.Text))
                {
                    textBoxInvolvedGuid.Text = CreateInvolvedGuid(textBoxGuid.Text);
                }
                
                string namesStr = GetRegistryInfo(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Components\" + textBoxInvolvedGuid.Text));
                namesStr += GetRegistryInfo(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UpgradeCodes\" + textBoxInvolvedGuid.Text));
                namesStr += GetRegistryInfo(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\Installer\UpgradeCodes\" + textBoxInvolvedGuid.Text));
                namesStr += GetRegistryInfo(Registry.ClassesRoot.OpenSubKey(@"Installer\UpgradeCodes\" + textBoxInvolvedGuid.Text));
                
                if (string.IsNullOrEmpty(namesStr))
                {
                    namesStr = "Данный гуид не найден";
                }
                
                textBoxValueNames.Text = namesStr;
            }
            catch (Exception ex)
            {
                textBoxValueNames.Text = ex.Message;
                textBoxGuid.Focus();
            }
        }

        private string GetRegistryInfo(RegistryKey regKey)
        {
            if (regKey == null)
            {
                return "";
            }

            string namesStr = "Гуид найден в " + regKey + "\r\n";
            string[] names = regKey.GetValueNames();

            foreach (string s in names)
            {
                object value = regKey.GetValue(s);

                namesStr += "\t" + s + "\t=\t" + value + "\r\n";
            }

            return namesStr;
        }

        /// <summary>
        /// Remove button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxInvolvedGuid.Text))
            {
                textBoxInvolvedGuid.Text = CreateInvolvedGuid(textBoxGuid.Text);
            }

            textBoxValueNames.Clear();

            DeleteRegistryKey(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Components\" + textBoxInvolvedGuid.Text));
            DeleteRegistryKey(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UpgradeCodes\" + textBoxInvolvedGuid.Text));
            DeleteRegistryKey(Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Classes\Installer\UpgradeCodes\" + textBoxInvolvedGuid.Text));
            DeleteRegistryKey(Registry.ClassesRoot.OpenSubKey(@"Installer\UpgradeCodes\" + textBoxInvolvedGuid.Text));
        }

        private void DeleteRegistryKey(RegistryKey regKey)
        {
            try
            {
                if (regKey == null)
                {
                    return;
                }

                regKey.DeleteSubKeyTree(textBoxInvolvedGuid.Text);
                textBoxValueNames.Text += "Ключ успешно удалён\r\n";
            }
            catch (Exception ex)
            {
                textBoxValueNames.Text += ex.Message + "\r\n";
            }
        }

        private void buttonGotoReplaceForm_Click(object sender, EventArgs e)
        {
            new ReplaceFilesTextForm().ShowDialog();
        }

        #region Подсказки
        private void buttonCheck_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Проверить, имеется ли указанный \"хитрый\" гуид в реестре", buttonCheck, -10, -17);
        }

        private void buttonCheck_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonCheck);
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Удалить \"хитрый\" гуид, если он есть", buttonDelete, -10, -17);
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonDelete);
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Закрыть форму", buttonClose, -10, -17);
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
        }

        private void buttonConvertBack_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Перевести \"хитрый\" гуид в обычный", buttonConvertBack, -10, -17);
        }

        private void buttonConvertBack_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonConvertBack);
        }

        private void buttonConvert_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Перевести обычный гуид в \"хитрый\"", buttonConvert, -10, -17);
        }

        private void buttonConvert_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonConvert);
        }

        private void buttonReplaceGuid_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Открыть форму замены гуидов в групе файлов", buttonConvert, -10, -17);
        }

        private void buttonReplaceGuid_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonConvert);
        }
        #endregion
    }
}
