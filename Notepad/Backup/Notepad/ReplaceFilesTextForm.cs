using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Notepad
{
    public partial class ReplaceFilesTextForm : Form
    {
        private enum Action
        {
            ChangeGuid,
            ChangeText
        }

        private Action currentAction;

        private string textFind;

        private string textReplace;


        public ReplaceFilesTextForm()
        {
            InitializeComponent();
        }

        // Кнопка "Выбор"

        private void buttonPath_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = textPath.Text;

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string directoryName = folderBrowserDialog1.SelectedPath;
                textPath.Text = directoryName;
            }
        }

        /// <summary>
        /// Кнопка "Выход" 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        bool _stopExecution;

        private void buttonReplaceGuid_Click(object sender, EventArgs e)
        {
            string directoryName = textPath.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            currentAction = Action.ChangeGuid;
            progressBarInfo.Value = 0;
            labelInfo.Visible = progressBarInfo.Visible = true;
            Application.DoEvents();
            _stopExecution = false;
            WorkArround(directoryName);
            labelInfo.Visible = progressBarInfo.Visible = false;

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Проверяет, является ли переданная строка гуидом
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool IsGuid(string text)
        {
            try
            {
                new Guid(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  Процедура обхода файлов в папке directoryName, и, если надо, в подпапках папки directoryName
        /// </summary>
        /// <param name="directoryName">Имя папки</param>
        private void WorkArround(string directoryName)
        {
            if (checkNestingFolder.Checked)
            {
                string[] directoryes = Directory.GetDirectories(directoryName);

                foreach (string dir in directoryes)
                {
                    WorkArround(dir);
                    if (_stopExecution)
                    {
                        return;
                    }
                }
            }

            string[] files = Directory.GetFiles(directoryName);

            progressBarInfo.Maximum = files.Length;
            progressBarInfo.Value = 0;
            labelInfo.Text = "Директория: " + directoryName;
            Application.DoEvents();
            foreach (string s in files)
            {
                if (!IsFilterOk(s))
                {
                    continue;
                }

                var file = new FileInfo(s);
                FileAttributes saveFileAttributes = file.Attributes;
                try
                {
                    file.Attributes = FileAttributes.Normal;
                    string fileText;
                    using (var sr = new StreamReader(s, Encoding.GetEncoding("windows-1251")))
                    {
                        fileText = sr.ReadToEnd();
                    }

                    switch (currentAction)
                    {
                        case Action.ChangeGuid:
                            fileText = ChangeGuidsInText(fileText);
                            break;
                        case Action.ChangeText:
                            fileText = ChangeStringsInText(fileText);
                            break;
                    }


                    using (var sw = new StreamWriter(s, false, Encoding.GetEncoding("windows-1251")))
                    {
                        sw.Write(fileText);
                    }
                }
                catch
                {
                    MessageBox.Show("Невозможно изменить файл " + s + ". Возможно, он используется другим приложением.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    continue;
                }
                finally
                {
                    file.Attributes = saveFileAttributes;
                }
                progressBarInfo.Value++;
                Application.DoEvents();
            } // end foreach
        }


        /// <summary>
        /// Ищет все гуиды в тексте и заменяет на новые
        /// </summary>
        /// <param name="fileText"></param>
        /// <returns></returns>
        private string ChangeGuidsInText(string fileText)
        {
            int k = 0;
            int pos;
            while (k < fileText.Length && (pos = fileText.IndexOf('-', k)) != -1)
            {
                try
                {
                    if (IsGuid(fileText.Substring(pos - 8, 36)))
                    {
                        fileText = fileText.Substring(0, pos - 8) + Guid.NewGuid().ToString().ToUpper() + fileText.Substring(pos + 28);
                        k = pos + 28;
                    }
                    else
                    {
                        k = pos + 1;
                    }
                }
                catch
                {
                    break;
                }
            }

            return fileText;
        }

                
        /// <summary>
        /// Замена выражения типа asdf*ghjk на qwer (где * - любое количество символов)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private string ChangeStringsInText(string fileText)
        {
            var textBuilder = new StringBuilder(fileText);

            //
            // Если используются рег. выражения - то разбираем строку для их поиска и корректной обработки
            //
            if (checkUseRegular.Checked)
            {
                //
                // Обработка первого символа в строке для поиска
                //
                var arrPart = new List<string>();
                int n;
                if (textFind[0] == '*')
                {
                    arrPart.Add("");
                    n = 1;
                }
                else
                {
                    n = 0;
                }

                //
                // Обработка остальных символов в строке для поиска
                // Создание массива из частей того, что надо искать
                //
                string part = "";
                while (n < textFind.Length)
                {
                    if (textFind[n] == '*' && textFind[n - 1] != '\\')
                    {
                        arrPart.Add(part);
                        part = "";
                    }
                    else
                    {
                        if (textFind[n] != '\\')
                        {
                            part += textFind[n];
                        }
                        else
                        {
                            if (n + 1 < textFind.Length && textFind[n + 1] == '*')
                            {
                                part += "*";
                                n++;
                            }
                            else
                            {
                                part += "\\";
                            }
                        }
                    }
                    n++;
                }
                arrPart.Add(part);

                /*string q = "";
                for(int i = 0; i < parts.Length; i++) {

                  q += parts[i] + "\r\n";
                }
                MessageBox.Show(q);
                return;*/

                //
                // Если в строке для поиска нет * (или только * и ничего больше)
                //
                if (arrPart.Count == 1)
                {
                    if (arrPart[0] == "")
                    {
                        return textReplace;                        
                    }
                    else
                    {
                        textBuilder.Replace(textFind, textReplace);
                    }
                }
                else
                {
                    //
                    // Если строка для поиска содержит * - то ищутся по порядку все возможных части
                    // строки для поиска (между *)
                    //
                    int num1 = 0;
                    if (arrPart[0] != "")
                    {
                        num1 = (textBuilder.ToString()).IndexOf(arrPart[0], 0);
                    }

                    while (num1 != -1 && num1 < textBuilder.Length)
                    {
                        int num2 = num1 + arrPart[0].Length;
                        for (int i = 1; i < arrPart.Count; i++)
                        {
                            if (arrPart[i] != "")
                            {
                                num2 = (textBuilder.ToString()).IndexOf(arrPart[i], num2) + arrPart[i].Length;
                                if (num2 == arrPart[i].Length - 1)
                                {
                                    return textBuilder.ToString();
                                }
                            }
                            else
                            {
                                num2 = textBuilder.Length;
                                break;
                            }
                        }

                        textBuilder.Remove(num1, num2 - num1);
                        textBuilder.Insert(num1, textReplace);
                        
                        num1 += textReplace.Length;
                        if (arrPart[0] != "")
                        {
                            num1 = (textBuilder.ToString()).IndexOf(arrPart[0], num1);
                        }
                    }
                }
            }
            else
            {
                textBuilder.Replace(textFind, textReplace);
            }

            return textBuilder.ToString();
        }

        private bool IsFilterOk(string fileName)
        {
            textBoxFilterExt.Text = textBoxFilterExt.Text.Trim();
            if (textBoxFilterExt.Text != "*")
            {
                if (textBoxFilterExt.Text.Contains("*"))
                {
                    string[] parts = textBoxFilterExt.Text.Split('*');
                    string ext = Path.GetExtension(fileName).Substring(1);
                    if (!IsStringContainsAllParts(ext, parts))
                    {
                        return false;
                    }
                }
                else
                {
                    if (Path.GetExtension(fileName) != "." + textBoxFilterExt.Text)
                    {
                        return false;
                    }
                }

            }

            textBoxFilterName.Text = textBoxFilterName.Text.Trim();
            if (textBoxFilterName.Text != "*")
            {
                if (textBoxFilterName.Text.Contains("*"))
                {
                    string[] parts = textBoxFilterName.Text.Split('*');                   
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    if (!IsStringContainsAllParts(name, parts))
                    {
                        return false;
                    }
                }
                else
                {
                    if (Path.GetFileNameWithoutExtension(fileName) != textBoxFilterName.Text)
                    {
                        return false;
                    }
                }
            }


            return true;
        }

        private bool IsStringContainsAllParts(string str, string[] parts)
        {
            int k = 0;
            int pos = 0;

            string lastPart = parts[parts.Length - 1];
            if (!string.IsNullOrEmpty(lastPart) && !str.EndsWith(lastPart))
            {
                return false;
            }

            if (!string.IsNullOrEmpty(parts[0]))
            {
                if (str.StartsWith(parts[0]))
                {
                    pos = parts[0].Length;
                }
                else
                {
                    return false;
                }
            }

            for (int i = 1; i < parts.Length - 1; i++)
            {
                string part = parts[i];
                if (string.IsNullOrEmpty(part))
                {
                    continue;
                }

                k = str.IndexOf(part, pos);
                if (k == -1)
                {
                    return false;
                }
                pos = k + part.Length;
            }

            return true;
        }

        private void buttonReplaceText_Click(object sender, EventArgs e)
        {
            string directoryName = textPath.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            try
            {
                textFind = ParseSmartString(textBoxFindString.Text);
            }
            catch (Exception ex)
            {
                textBoxFindString.Focus();
                MessageBox.Show("Ошибка в записи строка для поиска:\r\n" + ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                textReplace = ParseSmartString(textBoxReplaceString.Text);
            }
            catch (Exception ex)
            {
                textBoxReplaceString.Focus();
                MessageBox.Show("Ошибка в записи строка для замены:\r\n" + ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            currentAction = Action.ChangeText;
            progressBarInfo.Value = 0;
            labelInfo.Visible = progressBarInfo.Visible = true;
            Application.DoEvents();
            _stopExecution = false;
            WorkArround(directoryName);
            labelInfo.Visible = progressBarInfo.Visible = false;

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Замена символов переноса строк и слешей. Слеши перед * оставляем 
        /// (этим они будут отличаться от * в смысле "любое количество символов")
        /// </summary>
        /// <returns></returns>
        private string ParseSmartString(string smartString)
        {
            if (!checkUseRegular.Checked)
            {
                return smartString;
            }

            // Разбор слешей
            var sb = new StringBuilder("");
            int i;
            for (i = 0; i < smartString.Length; i++)
            {
                if (smartString[i] == '\\')
                {
                    i++;
                    if (i == smartString.Length)
                    {
                        throw new WrongSmartStringException("Ошибка в записи строки");
                    }

                    if (smartString[i] == 'n')
                    {
                        sb.Append("\r\n");
                    }
                    else if (smartString[i] == '\\')
                    {
                        sb.Append("\\");
                    }
                    else if (smartString[i] == '*')
                    {
                        sb.Append("\\");
                        i--;
                    }
                    else
                    {
                        throw new WrongSmartStringException("Ошибка в записи строки");
                    }
                }
                else
                {
                    sb.Append(smartString[i]);
                }
            }

            // Проверка на то, чтобы в строке не было двух подряд идущих символов *, 
            // обозначающих любое количество символов
            i = 0;
            while (i < smartString.Length - 1)
            {
                if (smartString[i] == '*' && !(i > 0 && smartString[i - 1] == '\\') && smartString[i + 1] == '*')
                {
                    throw new WrongSmartStringException("Ошибка в записи строки");
                }
                i++;
            }

            return sb.ToString();
        }

        private void buttonInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
@" * - любое количество символов (возможно, ни одного)
\n - перевод строки
\* - просто символ ""звёздочка""
\\ - просто слэш", 
                "Правила ввода регулярных выражений", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void checkUseRegular_CheckedChanged(object sender, EventArgs e)
        {
            buttonInfo.Enabled = checkUseRegular.Checked;
        }


    }
}
