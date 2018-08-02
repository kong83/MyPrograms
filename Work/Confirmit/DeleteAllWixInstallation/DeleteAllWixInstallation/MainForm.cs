using System;
using System.IO;
using System.Text;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DeleteAllWixInstallation
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Распарсивание массива значений из текстовых полей
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string[] ParseInfo(string text)
        {
            return text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }


        /// <summary>
        /// Удаление баз данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDatabases_Click(object sender, EventArgs e)
        {
            string[] databases = ParseInfo(textBoxDatabases.Text);
            string sqlConnectionString = "server=" + textBoxServer.Text + ";uid=sa;password=firm";

            string report = "";
            try
            {
                foreach (string database in databases)
                {
                    using (var cn = new SqlConnection(sqlConnectionString))
                    using (var cmd = new SqlCommand("DROP DATABASE " + database, cn))
                    {
                        cn.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                            report += "База данных\t" + database + "\tуспешно удалена";
                        }
                        catch (Exception ex)
                        {
                            report += "База данных\t" + database + "\tне удалена. Сообщение: " + ex.Message;
                        }
                    }
                    MessageBox.Show(report, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        /// <summary>
        /// Удаление Event log
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEventLogs_Click(object sender, EventArgs e)
        {
            string[] eventLogs = ParseInfo(textBoxEventLogs.Text);
            string report = "";
            try
            {
                foreach (string eventLog in eventLogs)
                {
                    if (EventLog.SourceExists(eventLog))
                    {
                        EventLog.Delete(eventLog);
                        report += "Event log\t" + eventLog + "\t успешно удалён";
                    }
                    else
                    {
                        report += "Event log\t" + eventLog + "\t не найден";
                    }
                }
                MessageBox.Show(report, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Удаление файла backup-а
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBackup_Click(object sender, EventArgs e)
        {
            var fi = new FileInfo(textBoxBackup.Text);
            if (fi.Exists)
            {
                fi.Delete();
                MessageBox.Show("Удаление файла backup-а успешно заверешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Backup-файл не найден", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        /// <summary>
        /// Create involved guid from normal guid
        /// </summary>
        /// <param name="guid">Normal guid</param>
        /// <returns></returns>
        private static string CreateInvolvedGuid(string guid)
        {
            string[] guidComponents = guid.Replace("{", "").Replace("}", "").Replace(" ", "").ToUpper().Split(new[] { "-" }, StringSplitOptions.None);
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
                for (int j = 0; j < guidComponents[i].Length; j += 2)
                {
                    involvedComponent += guidComponents[i][j + 1].ToString() + guidComponents[i][j];
                }
                involvedGuid += involvedComponent;
            }

            return involvedGuid;
        }


        /// <summary>
        /// Удаление гуидов из реестра для permanent компонент
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPermanentComponents_Click(object sender, EventArgs e)
        {
            try
            {
                string[] permanentComponents = textBoxPermanentComponents.Text.Replace(" ", "").Replace("{", "").Replace("}", "").Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string report = "";
                foreach (string guid in permanentComponents)
                {
                    string involvedGuid = CreateInvolvedGuid(guid);
                    RegistryKey regKey = Registry.LocalMachine;
                    regKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Installer\UserData\S-1-5-18\Components\", true);

                    try
                    {
                        // ReSharper disable PossibleNullReferenceException
                        regKey.DeleteSubKeyTree(involvedGuid);
                        // ReSharper restore PossibleNullReferenceException
                        report += "Гуид \t" + guid + "\tуспешно удалён\r\n";
                    }
                    catch
                    {
                        report += "Гуид \t" + guid + "\tне найден\r\n";
                    }
                }
                MessageBox.Show(report, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Удаление всего
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAll_Click(object sender, EventArgs e)
        {
            buttonDatabases_Click(null, null);
            buttonEventLogs_Click(null, null);
            buttonBackup_Click(null, null);
            buttonPermanentComponents_Click(null, null);
        }



        /// <summary>
        /// Удаление Performance counter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPerformance_Click(object sender, EventArgs e)
        {
            string[] performances = ParseInfo(textBoxPerformans.Text);

            foreach (string performance in performances)
            {
                try
                {
                    if (PerformanceCounterCategory.CounterExists("Time between calls", performance))
                    {
                        PerformanceCounterCategory.Delete(performance);
                    }
                    MessageBox.Show("Удаление Performance counter успешно заверешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Удаление registry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRegistry_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey reg = Registry.LocalMachine;
                reg.DeleteSubKeyTree(textBoxRegistry.Text);
                MessageBox.Show("Удаление записей реестра успешно заверешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Удаление директорий
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDirectoryes_Click(object sender, EventArgs e)
        {
            try
            {
                string[] dirs = ParseInfo(textBoxDirectoryes.Text);
                string report = "";

                foreach (string dir in dirs)
                {
                    var di = new DirectoryInfo(dir);
                    if (di.Exists)
                    {
                        di.Delete(true);
                        report += "Директория\t" + dir + "\t успешно удалена";
                    }
                    else
                    {
                        report += "Директория\t" + dir + "\t не найдена";
                    }
                }
                MessageBox.Show(report, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Исправление лог-файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckLog_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(textBoxCheckLog.Text))
                {
                    MessageBox.Show("Указанный файл не обнаружен");
                    return;
                }

                string[] textLines;
                using (var sr = new StreamReader(textBoxCheckLog.Text))
                {
                    textLines = sr.ReadToEnd().Split(new []{"\r\n" }, StringSplitOptions.None);
                }

                var text = new StringBuilder();
                bool isAppend = true;
                foreach (string str in textLines)
                {
                    if (isAppend && str.Contains("Deleting MYDEBUG property"))
                    {
                        isAppend = false;
                    }
                    else if (!isAppend && str.Contains("MSI (c)"))
                    {
                        isAppend = true; 
                    }

                    if (isAppend)
                    {
                        text.Append(str + "\r\n");
                    }
                }

                text = text.Replace("PROPERTY CHANGE: Adding MYDEBUG property. Its value is ", "MYDEBUG = ");

                File.Delete(textBoxCheckLog.Text);
                using (var sw = new StreamWriter(textBoxCheckLog.Text))
                {
                    sw.Write(text);
                }
                MessageBox.Show("Исправление лог-файла успешно заверешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Проверка правильности установки V14
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckInstall_Click(object sender, EventArgs e)
        {
            new CheckInstallV14Form().Show();
        }

        /// <summary>
        /// Проверка правильности установки V15
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            new CheckInstallV15Form().Show();
        }

        /// <summary>
        /// Проверка правильности установки V16.5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            new CheckInstallV16Form().Show();
        }

    }
}
