using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace FilesName
{
    public partial class MainForm : Form
    {
        string _textFind, _textReplace; // Строки для поиска и замены

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

        private bool _stopExecution;


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
                        _stopExecution = true;
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
                    if (_stopExecution)
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
                if (_textFind.Length == 0) // Если строка для поиска пуста - то добавляем строку
                {                                       // для замены в конец или начало имени файла          
                    string newName = s;
                    if (comboReplaceTo.Text == "конец")            // Добавляем в конец
                    {
                        if (radioCheckFile.Checked)             // имени файла
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + Path.GetFileNameWithoutExtension(s) + _textReplace + Path.GetExtension(s);
                        }
                        else if (radioCheckExp.Checked)               // раширения
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + Path.GetFileName(s) + _textReplace;
                        }
                        else                                          // тела файла
                        {
                            var sw = new StreamWriter(s, true, Encoding.GetEncoding("1251"));
                            sw.Write(_textReplace);
                            sw.Close();
                        }
                    }
                    else                                      // Добавляем в начало
                    {
                        if (radioCheckFile.Checked)             // имени файла
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + _textReplace + Path.GetFileName(s);
                        }
                        else if (radioCheckExp.Checked)         // раширения
                        {
                            newName = Path.GetDirectoryName(s) + "\\" + Path.GetFileNameWithoutExtension(s) + "." + _textReplace + (Path.GetExtension(s) ?? string.Empty).Substring(1);
                        }
                        else                                    // тела файла
                        {
                            var sr = new StreamReader(s, Encoding.GetEncoding("windows-1251"));
                            string fileText = sr.ReadToEnd();
                            sr.Close();

                            fileText = _textReplace + fileText;
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
                    if (_stopExecution)
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
                            strWork = new StringWork(s, _textFind, 1);
                        else
                        {
                            if (!Path.HasExtension(s))
                                continue;
                            strWork = new StringWork(s, _textFind, 2);
                        }

                        int numFirst;
                        if (_textFind.Equals("*"))
                        {
                            numFirst = 0;
                            numLast = strWork.GetStr.Length;
                        }
                        else
                        {
                            int iFind;
                            if (_textFind[0] == '*')
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

                            while (iFind < _textFind.Length)
                            {
                                if (strWork.SearchNext(strWork.GetString(iFind)) == -1)
                                {
                                    numLast = -1;
                                    goto Ex;
                                }
                                iFind += strWork.GetString(iFind).Length + 1;
                                numLast = strWork.GetiStr;
                            }

                            if (_textFind[_textFind.Length - 1] == '*')
                                numLast = strWork.GetStr.Length;
                        }
                    Ex:
                        if (numFirst != -1 && numLast != -1)
                        {
                            string newName = s.Substring(0, strWork.Offset + numFirst) + _textReplace + s.Substring(strWork.Offset + numLast);

                            if (!TransferFile(s, newName))
                            {
                                continue;
                            }
                            if (_stopExecution)
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

                            fileText = fileText.Replace(_textFind, _textReplace);

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

            _textFind = "";
            if (textPath.Text.IndexOf("**") > 0)
            {
                MessageBox.Show("Неправильная запись в строке для поиска: ** ", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            _textFind = textFindString.Text;

            _textReplace = textReplaceString.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            progressBarInfo.Value = 0;
            labelInfo.Visible = progressBarInfo.Visible = true;
            Application.DoEvents();
            _stopExecution = false;
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
            string newName = Path.GetFileNameWithoutExtension(name.Replace('_', ' ').Trim()) ?? string.Empty;
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

        private static string NormalizeNameStartedWithDigit(string name)
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

        private static string ExtractFirstDigit(ref string name)
        {
            var firstDigit = new StringBuilder();
            int i = 0;
            int digit;
            while (i < name.Length && int.TryParse(name[i].ToString(), out digit))
            {
                firstDigit.Append(digit);
                i++;
            }

            name = i < name.Length ? name.Substring(i).Trim() : string.Empty;
            return firstDigit.ToString();
        }

        private static string LatinToLowerKirill(string text)
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
                    if (_stopExecution)
                    {
                        return;
                    }
                }
            }

            foreach (string fileName in Directory.GetFiles(directoryName))
            {
                var file = new FileInfo(fileName);
                string newName = file.DirectoryName + "\\" + ConvertName(file.Name);

                if (!TransferFile(fileName, newName))
                {
                    continue;
                }
                if (_stopExecution)
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
                MessageBox.Show("Не выбран тип замены", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string directoryName = textPath.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _stopExecution = false;
            UpperToLower(directoryName);

            MessageBox.Show("Выполнение программы завершено.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        /// <summary>
        /// Выставление нумерации для файлов в директории или убирание нумерации
        /// </summary>
        /// <param name="directoryName">Путь до папки</param>
        private void NumerationWorks(string directoryName)
        {
            if (checkNestingFolder.Checked)
            {
                foreach (string dir in Directory.GetDirectories(directoryName))
                {
                    NumerationWorks(dir);
                    if (_stopExecution)
                    {
                        return;
                    }
                }
            }
            
            int currentNumber = Configuration.Default.StartNumber;
            foreach (string fileName in Directory.GetFiles(directoryName))
            {
                var file = new FileInfo(fileName);
                string newFileName;
                if (Configuration.Default.NumerationSettings == "DoNumeration")
                {
                    newFileName = GetNewNumerationName(file.Name, currentNumber);
                    currentNumber++;
                }
                else if (Configuration.Default.NumerationSettings == "RemoveNumeration")
                {
                    newFileName = RemoveNumeration(file.Name);
                }
                else
                {
                    newFileName = ModifyNumeration(file.Name);
                }

                string newPathName = file.DirectoryName + "\\" + newFileName;

                if (!TransferFile(fileName, newPathName))
                {
                    continue;
                }
                if (_stopExecution)
                {
                    return;
                }
            }
        }


        /// <summary>
        /// Изменить номер, убрав или добавив нули в начало имени файла, в зависимости от минимального количества цифр
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        private static string ModifyNumeration(string fileName)
        {
            // Получаем номер, который написан в начале имени файла
            string result = fileName;
            string number = null;
            int digit;
            while (result.Length > 0 && int.TryParse(result.Substring(0, 1), out digit))
            {
                number += digit.ToString();
                result = result.Substring(1);
            }

            // Если такой номер там есть, то удаляем у него начальные нули и добавляем снова нули, если надо и столько, сколько надо
            if (!string.IsNullOrEmpty(number))
            {
                result = result.TrimStart();

                number = number.TrimStart('0');
                int numberLength = number.Length;
                for (int i = 0; i < Configuration.Default.MinCountDigits - numberLength; i++)
                {
                    number = "0" + number;
                }

                result = number + " " + result;
            }

            return result;
        }


        /// <summary>
        /// Удаляет номер с пробелом в начале названия файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns></returns>
        private static string RemoveNumeration(string fileName)
        {
            string result = fileName;
            int digit;
            while (result.Length > 0 && int.TryParse(result.Substring(0, 1), out digit))
            {
                result = result.Substring(1);
            }
            
            return result.TrimStart();
        }


        /// <summary>
        /// Добавляет номер с пробелом в начало файла. 
        /// Если минимальное количество цифр в номере больше, чем количество цифр в переданном номере, то недостающие цифры будут заполнены нулями
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="currentNumber">Номер</param>
        /// <returns></returns>
        private static string GetNewNumerationName(string fileName, int currentNumber)
        {
            string currentNumberStr = currentNumber.ToString();
            int currentNumberLength = currentNumberStr.Length;
            for (int i = 0; i < Configuration.Default.MinCountDigits - currentNumberLength; i++)
            {
                currentNumberStr = "0" + currentNumberStr;
            }

            return currentNumberStr + " " + fileName;
        }


        /// <summary>
        /// Пронумеровать или убрать нумерацию для файлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNumeration_Click(object sender, EventArgs e)
        {
            string directoryName = textPath.Text;

            if (!Directory.Exists(directoryName))
            {
                MessageBox.Show("Ошибка в имени директории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            _stopExecution = false;
            NumerationWorks(directoryName);

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
            if (textFindString.Text.StartsWith("\r\n"))
            {
                textFindString.Text = " ";
                return;
            }

            Configuration.Default.FindString = textFindString.Text;
            Configuration.Default.Save();
        }

        private void textReplaceString_TextChanged(object sender, EventArgs e)
        {
            if (textReplaceString.Text.StartsWith("\r\n"))
            {
                textReplaceString.Text = " ";
                return;
            }

            Configuration.Default.ReplaceString = textReplaceString.Text;
            Configuration.Default.Save();
        }

        private void comboReplaceTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Configuration.Default.ReplaceTo = comboReplaceTo.Text;
            Configuration.Default.Save();
        }

        private void textStartNumber_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Configuration.Default.StartNumber = Convert.ToInt32(textStartNumber.Text);
                Configuration.Default.Save();
            }
            catch
            {
                MessageBox.Show("Начальный номер должен быть числом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textMinCountDigits_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Configuration.Default.MinCountDigits = Convert.ToInt32(textMinCountDigits.Text);
                Configuration.Default.Save();
            }
            catch
            {
                MessageBox.Show("Начальный номер должен быть числом", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        private void SetNumeration_CheckedChanged(object sender, EventArgs e)
        {
            if (radioDoNumeration.Checked)
            {
                Configuration.Default.NumerationSettings = "DoNumeration";
                labelStartNumber.Enabled = labelMinCountDigits.Enabled = textStartNumber.Enabled = textMinCountDigits.Enabled = true;
            }
            else if (radioRemoveNumeration.Checked)
            {
                Configuration.Default.NumerationSettings = "RemoveNumeration";
                labelStartNumber.Enabled = labelMinCountDigits.Enabled = textStartNumber.Enabled = textMinCountDigits.Enabled = false;
            }
            else
            {
                Configuration.Default.NumerationSettings = "ModifyNumeration";
                labelStartNumber.Enabled = textStartNumber.Enabled = false;
                labelMinCountDigits.Enabled = textMinCountDigits.Enabled = true;
            }

            Configuration.Default.Save();
        }

        #endregion
    }
}