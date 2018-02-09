using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class AboutForm : Form
    {
        private readonly MainForm _mainForm;

        public AboutForm(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }
              

        /// <summary>
        /// Копирование блокнота в системные директории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetDefault_Click(object sender, EventArgs e)
        {
            try
            {
                string fileSource = Environment.SystemDirectory + @"\notepad.exe";
                var fi = new FileInfo(fileSource);
                if (fi.Exists)
                {
                    int n = 1;
                    while (File.Exists(Application.StartupPath + @"\notepadold" + n + ".exe"))
                    {
                        n++;
                    }

                    string fileDest = Application.StartupPath + @"\notepadold" + n + ".exe";                    
                    fi.CopyTo(fileDest, true);
                }
                else
                {
                    MessageBox.Show("Файл " + fileSource + " не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string systemRoot = Environment.GetEnvironmentVariable("SYSTEMROOT") ?? @"c:\Windows";
                fi = new FileInfo(Application.ExecutablePath);
                var fiZlib = new FileInfo(Path.Combine(Application.StartupPath, "zlib.net.dll"));

                if (File.Exists(Path.Combine(Environment.SystemDirectory, @"dllcache\notepad.exe")))
                {
                    fi.CopyTo(Path.Combine(Environment.SystemDirectory, @"dllcache\notepad.exe"), true);
                    fiZlib.CopyTo(Path.Combine(Environment.SystemDirectory, @"dllcache\zlib.net.dll"), true);
                }
                
                if (File.Exists(Path.Combine(Environment.SystemDirectory, @"notepad.exe")))
                {                    
                    fi.CopyTo(Path.Combine(Environment.SystemDirectory, @"notepad.exe"), true);
                    fiZlib.CopyTo(Path.Combine(Environment.SystemDirectory, @"zlib.net.dll"), true);
                }

                if (File.Exists(Path.Combine(systemRoot, @"SysWOW64\notepad.exe")))
                {
                    fi.CopyTo(Path.Combine(systemRoot, @"SysWOW64\notepad.exe"), true);
                    fiZlib.CopyTo(Path.Combine(systemRoot, @"SysWOW64\zlib.net.dll"), true);
                }

                if (File.Exists(Path.Combine(systemRoot, @"notepad.exe")))
                {
                    fi.CopyTo(Path.Combine(systemRoot, @"notepad.exe"), true);
                    fiZlib.CopyTo(Path.Combine(systemRoot, @"zlib.net.dll"), true);
                }

                MessageBox.Show("Если появится сообщение о нераспознанных файлах - то нажмите сначала \"Отмена\", потом \"Да\"", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Замена невозможна: " + exc.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }             
        

        /// <summary>
        /// Показать подсказку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSetDefault_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Для WinXP работает. Для других версий - не факт", buttonSetDefault, 15, -17);
        }
        private void buttonSetDefault_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonSetDefault);
        }

        /// <summary>
        /// Смена языка ввода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AboutForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            _mainForm.MainForm_InputLanguageChanged(sender, e);
        }

        /// <summary>
        /// Выход при нажатии Esc
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Если у Вас Windows 7 или Windows Vista, то по умолчанию доступ к файлам notepad.exe закрыт. Смените владельца для файлов notepad.exe в папках Windows и Windows\\System32 и дайте новому владельцу полный доступ на изменение этих файлов.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
