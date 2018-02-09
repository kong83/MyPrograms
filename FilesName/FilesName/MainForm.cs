using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FilesName
{
    public partial class MainForm : Form
    {
        string m_TextFind, m_TextReplace; // Строки для поиска и замены

        public MainForm()
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
        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        bool m_StopExecution;


        /// <summary>
        /// Move file from oldPath to newPath
        /// </summary>
        /// <param name="oldPath"></param>
        /// <param name="newPath"></param>
        private bool TransferFile(string oldPath, string newPath)
        {
            if (oldPath == newPath)
            {
                return true;
            }
            FileInfo file;

            if (File.Exists(newPath))
            {
                if (oldPath.ToUpper() != newPath.ToUpper())
                {
                    DialogResult dialogResult = MessageBox.Show("Файл\r\n" + newPath + "\r\nуже существует. Заменить?", "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (dialogResult == DialogResult.No)
                    {
                        return true;
                    }

                    if (dialogResult == DialogResult.Yes)
                    {
                        try
                        {
                            file = new FileInfo(newPath) 
                            {
                                Attributes = FileAttributes.Normal
                            };
                            file.Delete();
                        }
                        catch (IOException)
                        {
                            MessageBox.Show("Невозможно удалить файл\r\n" + newPath + ".\r\nВозможно, он используется другим приложением.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("В процессе переименования файла\r\n" + oldPath + "\r\nпроизошла неизвестная ошибка:\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }

                    if (dialogResult == DialogResult.Cancel)
                    {
                        m_StopExecution = true;
                        return true;
                    }
                }
            }

            file = new FileInfo(oldPath);
            FileAttributes saveFileAttributes = file.Attributes;
            try
            {
                file.Attributes = FileAttributes.Normal;
                file.MoveTo(newPath);
                file.Attributes = saveFileAttributes;
            }
            catch (IOException)
            {
                MessageBox.Show("Невозможно переименовать файл\r\n" + oldPath + ".\r\nВозможно, он используется другим приложением.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("В процессе переименования файла\r\n" + oldPath + "\r\nпроизошла неизвестная ошибка:\r\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                file.Attributes = saveFileAttributes;
            }
            return true;
        }


        /// <summary>
        ///  Процедура обхода файлов в папке directoryName, и, если надо, в подпапках папки directoryName
        /// </summary>
        /// <param name="directoryName">Имя папки</param>
        private void WorkArround(string directoryName)
        {
            if (checkNestingFolder.Checked)
            {
                foreach (string dir in Directory.GetDirectories(directoryName))
                {
                    WorkArround(dir);
                    if (m_StopExecution)
                    {
                        return;
                    }
                }
            }

            int numLast = -1;
            string[] files = Directory.GetFiles(directoryName);

            progressBarInfo.Maximum = files.Length;
            progressBarInfo.Value = 0;
            labelInfo.Text = "Директория: " + directoryName;
            Application.DoEvents();
            foreach (string s in files)
            {
                if (m_TextFind.Length == 0) // Если строка для поиска пуста - то добавляем строку
                {                                       // для замены в конец или начало имени файла          
                    string newName = s;
                    if (comboReplaceTo.Text == "конец")            // Добавляем в конец
                    {
                        if (radioCheckFile.Checked)             // имени файла
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + Path.GetFileNameWithoutExtension(s) + m_TextReplace + Path.GetExtension(s);
                        }
                        else if (radioCheckExp.Checked)               // раширения
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + Path.GetFileName(s) + m_TextReplace;
                        }
                        else                                          // тела файла
                        {
                            var sw = new StreamWriter(s, true, Encoding.GetEncoding("1251"));
                            sw.Write(m_TextReplace);
                            sw.Close();
                        }
                    }
                    else                                      // Добавляем в начало
                    {
                        if (radioCheckFile.Checked)             // имени файла
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + m_TextReplace + Path.GetFileName(s);
                        }
                        else if (radioCheckExp.Checked)         // раширения
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + Path.GetFileNameWithoutExtension(s) + "." + m_TextReplace + Path.GetExtension(s).Substring(1);
                        }
                        else                                    // тела файла
                        {
                            var sr = new StreamReader(s, Encoding.GetEncoding("windows-1251"));
                            string fileText = sr.ReadToEnd();
                            sr.Close();

                            fileText = m_TextReplace + fileText;
                            var sw = new StreamWriter(s, false, Encoding.GetEncoding("windows-1251"));
                            sw.Write(fileText);
                            sw.Close();
                        }
                    }

                    // Копируем в новый файл содержимое старого
                    if (!TransferFile(s, newName))
                    {
                        continue;
                    }
                    if (m_StopExecution)
                    {
                        return;
                    }
                }
                else                       // Ищем строку для поиска и заменяем её
                {
                    if (radioCheckFile.Checked || radioCheckExp.Checked) // в имени или расширении
                    {
                        StringWork strWork;
                        if (radioCheckFile.Checked)
                            strWork = new StringWork(s, m_TextFind, 1);
                        else
                        {
                            if (!Path.HasExtension(s))
                                continue;
                            strWork = new StringWork(s, m_TextFind, 2);
                        }

                        int numFirst;
                        if (m_TextFind.Equals("*"))
                        {
                            numFirst = 0;
                            numLast = strWork.GetStr.Length;
                        }
                        else
                        {
                            int iFind;
                            if (m_TextFind[0] == '*')
                            {
                                numFirst = 0;
                                iFind = 1;
                            }
                            else
                            {
                                numFirst = strWork.SearchNext(strWork.GetString(0));
                                if (numFirst == -1)
                                    goto Ex;
                                iFind = strWork.GetString(0).Length + 1;
                                numLast = strWork.GetiStr;
                            }

                            while (iFind < m_TextFind.Length)
                            {
                                if (strWork.SearchNext(strWork.GetString(iFind)) == -1)
                                {
                                    numLast = -1;
                                    goto Ex;
                                }
                                iFind += strWork.GetString(iFind).Length + 1;
                                numLast = strWork.GetiStr;
                            }

                            if (m_TextFind[m_TextFind.Length - 1] == '*')
                                numLast = strWork.GetStr.Length;
                        }
                    Ex:
                        if (numFirst != -1 && numLast != -1)
                        {
                            string newName = s.Substring(0, strWork.Offset + numFirst) + m_TextReplace + s.Substring(strWork.Offset + numLast);

                            if (!TransferFile(s, newName))
                            {
                                continue;
                            }
                            if (m_StopExecution)
                            {
                                return;
                            }                            
                        }
                    } // end if
                    else                          // В теле файла
                    {
                        var file = new FileInfo(s);
                        FileAttributes saveFileAttributes = file.Attributes;
                        try
                        {
                            file.Attributes = FileAttributes.Normal;
                            var sr = new StreamReader(s, Encoding.GetEncoding("windows-1251"));
                            string fileText = sr.ReadToEnd();
                            sr.Close();

                            fileText = fileText.Replace(m_TextFind, m_TextReplace);

                            var sw = new StreamWriter(s, false, Encoding.GetEncoding("windows-1251"));
                            sw.Write(fileText);
                            sw.Close();
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
                    } // end else
                } // end else
                progressBarInfo.Value++;
                Application.DoEvents();
            } // end foreach
        }


        /// <summary>
        /// Обработка нажатия на кнопку "Заменить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartReplace_Click(object sender, EventArgs e)
        {
            string directoryName = textPath.Text;

            m_TextFind = "";
            if (textPath.Text.IndexOf("**") > 0)
            {
                MessageBox.Show("Неправильная запись в строке для поиска: ** ", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            m_TextFind = textFindString.Text;

            m_TextReplace = textReplaceString.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            progressBarInfo.Value = 0;
            labelInfo.Visible = progressBarInfo.Visible = true;
            Application.DoEvents();
            m_StopExecution = false;
            WorkArround(directoryName);
            labelInfo.Visible = progressBarInfo.Visible = false;

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Возвращает тот или иной аттрибут, в зависимости от выбранного пользователем
        /// </summary>
        /// <returns></returns>
        private StringBuilder GetNames(string directoryName)
        {
            var names = new StringBuilder(); 

            if (radioFolderName.Checked)
            {
                foreach (string subDirectoryName in Directory.GetDirectories(directoryName))
                {
                    names.Append(subDirectoryName + "\r\n");
                }
            }
            else 
            {
                foreach (string fileName in Directory.GetFiles(directoryName))
                {
                    if (radioFileNameWithExtentions.Checked)
                    {
                        names.Append(Path.GetFileName(fileName) + "\r\n");
                    }
                    else
                    {
                        names.Append(Path.GetFileNameWithoutExtension(fileName) + "\r\n");
                    }
                }
            }

            return names;
        }


        /// <summary>
        /// Установка аттрибутов на все найденные файлы
        /// </summary>
        /// <param name="directoryName"></param>
        private StringBuilder CollectNames(string directoryName)
        {
            var result = new StringBuilder();
            if (checkNestingFolder.Checked)
            {
                foreach (string dir in Directory.GetDirectories(directoryName))
                    result.Append(CollectNames(dir));
            }

            result.Append(GetNames(directoryName));

            return result;
        }


        /// <summary>
        /// Скопировать в буфер имена файлов или папок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopyInBuffer_Click(object sender, EventArgs e)
        {
            string directoryName = textPath.Text;           

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            StringBuilder names = CollectNames(directoryName);

            Clipboard.SetText(names.ToString(), TextDataFormat.UnicodeText);

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Возвращает тот или иной аттрибут, в зависимости от выбранного пользователем
        /// </summary>
        /// <returns></returns>
        private FileAttributes GetFileAttribute()
        {
            if (radioAttrReadOnly.Checked)
                return FileAttributes.ReadOnly;
            if (radioAttrSys.Checked)
                return FileAttributes.System;
            if (radioAttrHide.Checked)
                return FileAttributes.Hidden;
            if (radioAttrTemporary.Checked)
                return FileAttributes.Temporary;
            if (radioAttrArchive.Checked)
                return FileAttributes.Archive;
            return FileAttributes.Normal;
        }


        /// <summary>
        /// Установка аттрибутов на все найденные файлы
        /// </summary>
        /// <param name="directoryName"></param>
        private void SetAttributes(string directoryName)
        {
            if (checkNestingFolder.Checked)
            {
                foreach (string dir in Directory.GetDirectories(directoryName))
                    SetAttributes(dir);
            }

            foreach (string fileName in Directory.GetFiles(directoryName))
            {
                try
                {
                    new FileInfo(fileName)
                    {
                        Attributes = GetFileAttribute()
                    };
                }
                catch
                {
                    MessageBox.Show("Невозможно изменить аттрибуты у файла " + fileName + ". Возможно, он используется другим приложением.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    continue;
                }
            }
        }


        /// <summary>
        /// Нажатие на кнопку для установки указанных аттрибутов на все найденные файлы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAttributes_Click(object sender, EventArgs e)
        {
            string directoryName = textPath.Text;            

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SetAttributes(directoryName);

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Проведение преобразование названия файла в зависимости от выбранного типа
        /// </summary>
        /// <param name="name">Текущее название файла</param>
        /// <returns></returns>
        private string ConvertName(string name)
        {
            string ext = Path.GetExtension(name);
            string newName = Path.GetFileNameWithoutExtension(name.Replace('_', ' ').Trim());
            while (newName.IndexOf("  ") > 0)
            {
                newName = newName.Replace("  ", " ");
            }

            switch (comboConvertion.SelectedIndex)
            {
                case 0:
                    newName = newName.Substring(0, 1).ToUpper() + newName.Substring(1).ToLower();
                    break;
                case 1:
                    newName = NormalizeNameStartedWithDigit(newName);
                    break;
                case 2:
                    newName = LatinToLowerKirill(newName);
                    newName = newName.Substring(0, 1).ToUpper() + newName.Substring(1).ToLower();
                    break;
                case 3:
                    newName = LatinToLowerKirill(newName);
                    newName = NormalizeNameStartedWithDigit(newName);
                    break;
            }

            return newName + ext;
        }

        private string NormalizeNameStartedWithDigit(string name)
        {
            string newName = ExtractFirstDigit(ref name);
            if (string.IsNullOrEmpty(name))
            {
                return newName;
            }
            
            if (!string.IsNullOrEmpty(newName))
            {
                newName += " ";
            }

            return newName + name.Substring(0, 1).ToUpper() + name.Substring(1).ToLower();
        }

        private string ExtractFirstDigit(ref string name)
        {
            var firstDigit = new StringBuilder();
            int i=0;
            int digit;
            while (i < name.Length && int.TryParse(name[i].ToString(), out digit))
            {
                firstDigit.Append(digit);
                i++;
            }

            name = i < name.Length ? name.Substring(i).Trim() : string.Empty;
            return firstDigit.ToString();
        }

        private string LatinToLowerKirill(string text)
        {
            text = text.ToLower();
            string res = "";
            int i = 0;
            while (i < text.Length)
            {
                if (i < text.Length - 1 && text[i] == 'z' && text[i + 1] == 'h')
                {
                    res += 'ж'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 'c' && text[i + 1] == 'h')
                {
                    res += 'ч'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 's' && text[i + 1] == 'h')
                {
                    res += 'ш'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 'c' && text[i] == 's' && text[i + 1] == 'h')
                {
                    res += 'щ'; i += 2;
                }
                else if (i < text.Length - 1 && text[i] == 'y' && text[i + 1] == 'u')
                {
                    res += 'ю'; i++;
                }
                else if (i < text.Length - 1 && text[i] == 'y' && text[i + 1] == 'a')
                {
                    res += 'я'; i++;
                }
                else if (text[i] == 'a')
                    res += 'а';
                else if (text[i] == 'b')
                    res += 'б';
                else if (text[i] == 'v')
                    res += 'в';
                else if (text[i] == 'g')
                    res += 'г';
                else if (text[i] == 'd')
                    res += 'д';
                else if (text[i] == 'e')
                    res += 'е';
                else if (text[i] == 'z')
                    res += 'з';
                else if (text[i] == 'i')
                    res += 'и';
                else if (text[i] == 'j')
                    res += 'й';
                else if (text[i] == 'k')
                    res += 'к';
                else if (text[i] == 'l')
                    res += 'л';
                else if (text[i] == 'm')
                    res += 'м';
                else if (text[i] == 'n')
                    res += 'н';
                else if (text[i] == 'o')
                    res += 'о';
                else if (text[i] == 'p')
                    res += 'п';
                else if (text[i] == 'r')
                    res += 'р';
                else if (text[i] == 's')
                    res += 'с';
                else if (text[i] == 't')
                    res += 'т';
                else if (text[i] == 'u')
                    res += 'у';
                else if (text[i] == 'f')
                    res += 'ф';
                else if (text[i] == 'h')
                    res += 'х';
                else if (text[i] == 'c')
                    res += 'ц';
                else if (text[i] == '"')
                    res += 'ъ';
                else if (text[i] == '\'')
                    res += 'ь';
                else if (text[i] == 'y')
                    res += 'ы';
                else
                    res += text[i];
                i++;
            }

            return res;
        }

        /// <summary>
        /// Замена имен файлов в соответствии с выбранным преобразованием (возможен просмотр вложенных папок)
        /// </summary>
        /// <param name="directoryName">Название директории</param>
        private void UpperToLower(string directoryName)
        {
            if (checkNestingFolder.Checked)
            {
                foreach (string dir in Directory.GetDirectories(directoryName))
                {
                    UpperToLower(dir);
                    if (m_StopExecution)
                    {
                        return;
                    }
                }
            }

            foreach (string fileName in Directory.GetFiles(directoryName))
            {
                var file = new FileInfo(fileName);
                string newName = file.DirectoryName + "\\" + ConvertName(file.Name);

                if(!TransferFile(fileName, newName))
                {
                    continue;
                }
                if(m_StopExecution)
                {
                    return;
                }                
            }
        }


        /// <summary>
        /// Запуск выбранной замены имен файлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPath_Click_Click(object sender, EventArgs e)
        {
            if (comboConvertion.SelectedIndex < 0)
            {
                MessageBox.Show("Не выбран тип замены", "Предупреждение",  MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string directoryName = textPath.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            m_StopExecution = false;
            UpperToLower(directoryName);

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }


        /// <summary>
        /// О программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var infoForm = new InfoForm();
            infoForm.ShowDialog();
        }


        /// <summary>
        /// Загрузка сохранённых значений из конфига
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            textPath.Text = folderBrowserDialog1.SelectedPath = Configuration.Default.FolderPath;
            checkNestingFolder.Checked = Configuration.Default.CheckNestingFolder;
            textFindString.Text = Configuration.Default.FindString;
            textReplaceString.Text = Configuration.Default.ReplaceString;
            comboReplaceTo.Text = Configuration.Default.ReplaceTo;
            switch (Configuration.Default.TypeOfReplace)
            { 
                case "File":
                    radioCheckFile.Checked = true;
                    break;
                case "Exp":
                    radioCheckExp.Checked = true;
                    break;
                default:
                    radioCheckBody.Checked = true;
                    break;
            }
            comboConvertion.Text = Configuration.Default.Convertion;
            switch (Configuration.Default.CopyBufferSettings)
            {
                case "File":
                    radioFileName.Checked = true;
                    break;
                case "FileWithExt":
                    radioFileNameWithExtentions.Checked = true;
                    break;
                default:
                    radioFolderName.Checked = true;
                    break;
            }
            switch (Configuration.Default.SetAttributeSettings)
            {
                case "No":
                    radioAttrNo.Checked = true;
                    break;
                case "ReadOnly":
                    radioAttrReadOnly.Checked = true;
                    break;
                case "Archive":
                    radioAttrArchive.Checked = true;
                    break;
                case "Hide":
                    radioAttrHide.Checked = true;
                    break;
                case "Sys":
                    radioAttrSys.Checked = true;
                    break;               
                default:
                    radioAttrTemporary.Checked = true;
                    break;
            }
        }

        #region Сохранение настроек в конфиг
        private void textPath_TextChanged(object sender, EventArgs e)
        {
            Configuration.Default.FolderPath = textPath.Text;
            Configuration.Default.Save();
        }

        private void checkNestingFolder_CheckedChanged(object sender, EventArgs e)
        {
            Configuration.Default.CheckNestingFolder = checkNestingFolder.Checked;
            Configuration.Default.Save();
        }

        private void textFindString_TextChanged(object sender, EventArgs e)
        {
            Configuration.Default.FindString = textFindString.Text;
            Configuration.Default.Save();
        }

        private void textReplaceString_TextChanged(object sender, EventArgs e)
        {
            Configuration.Default.ReplaceString = textReplaceString.Text;
            Configuration.Default.Save();
        }

        private void comboReplaceTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.Default.ReplaceTo = comboReplaceTo.Text;
            Configuration.Default.Save();
        }

        private void SaveTypeOfReplace_CheckedChanged(object sender, EventArgs e)
        {
            if (radioCheckFile.Checked)
            {
                Configuration.Default.TypeOfReplace = "File";
            }
            else if (radioCheckExp.Checked)
            {
                Configuration.Default.TypeOfReplace = "Exp";
            }
            else if (radioCheckBody.Checked)
            {
                Configuration.Default.TypeOfReplace = "Body";
            }
            Configuration.Default.Save();
        }

        private void comboConvertion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.Default.Convertion = comboConvertion.Text;
            Configuration.Default.Save();
        }

        private void SaveCopyBuffer_CheckedChanged(object sender, EventArgs e)
        {
            if (radioFileName.Checked)
            {
                Configuration.Default.CopyBufferSettings = "File";
            }
            else if (radioFileNameWithExtentions.Checked)
            {
                Configuration.Default.CopyBufferSettings = "FileWithExt";
            }
            else if (radioFolderName.Checked)
            {
                Configuration.Default.CopyBufferSettings = "Folder";
            }
            Configuration.Default.Save();
        }

        private void SaveSetAttribute_CheckedChanged(object sender, EventArgs e)
        {
            if (radioAttrNo.Checked)
            {
                Configuration.Default.SetAttributeSettings = "No";
            }
            else if (radioAttrReadOnly.Checked)
            {
                Configuration.Default.SetAttributeSettings = "ReadOnly";
            }
            else if (radioAttrArchive.Checked)
            {
                Configuration.Default.SetAttributeSettings = "Archive";
            }
            else if (radioAttrHide.Checked)
            {
                Configuration.Default.SetAttributeSettings = "Hide";
            }
            else if (radioAttrSys.Checked)
            {
                Configuration.Default.SetAttributeSettings = "Sys";
            }
            else
            {
                Configuration.Default.SetAttributeSettings = "Temp";
            }
            Configuration.Default.Save();            
        }
        #endregion
    }
}