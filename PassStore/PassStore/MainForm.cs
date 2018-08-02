using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;

namespace PassStore
{
    public partial class MainForm : Form
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetOpenClipboardWindow();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern int GetWindowText(int hwnd, StringBuilder text, int count);


        /// <summary>
        /// Массив с данными одной строки для передачи данных между формами
        /// </summary>
        public string[] OneRow = new string[3];

        /// <summary>
        /// Флаг для запрещения/разрешения сохранения изменяющихся настроек
        /// </summary>
        bool _stopSaveParameters = true;

        /// <summary>
        /// Надо ли выходить из программы при попытке её закрытия
        /// </summary>
        bool _isNeedToExit;

        /// <summary>
        /// Информация обо всех паролях
        /// </summary>
        private List<Credential> _credentials;

        /// <summary>
        /// Точка начала работы программы
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Сброс фокуса с кнопок на список паролей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drop_Focus(object sender, EventArgs e)
        {
            PassList.Focus();
        }

        /// <summary>
        /// Получение хэш кода строки
        /// </summary>
        /// <param name="passStr"></param>
        /// <returns></returns>
        private static int GetHash(string passStr)
        {
            byte[] arr = Encoding.ASCII.GetBytes(passStr);
            int s = 1;
            int d = 1;
            foreach (byte b in arr)
            {
                s *= b | d;
                d++;
            }
            return s;
        }

        /// <summary>
        /// Сохранение изменений в списке паролей
        /// </summary>
        private void SaveChanges()
        {
            if (GetHash(PassStr) == 991202944)
            {
                string strAll = "";
                foreach (Credential credential in _credentials)
                {
                    strAll += credential.Name + "\t" + credential.Login + "\t" + credential.Password + "\n";
                }

                if (strAll != "")
                {
                    ZlibClass.SaveString(strAll);
                }
            }
        }


        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить новую запись", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }
        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }
        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выделенную запись", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }
        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }
        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выделенную запись", buttonEdit, 15, -20);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }
        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выход из программы", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
        private void buttonUp_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Переместить строку на 1 вверх", buttonUp, 15, -20);
            buttonUp.FlatStyle = FlatStyle.Popup;
        }
        private void buttonUp_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonUp);
            buttonUp.FlatStyle = FlatStyle.Flat;
        }
        private void buttonDown_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Переместить строку на 1 вниз", buttonDown, 15, -20);
            buttonDown.FlatStyle = FlatStyle.Popup;
        }
        private void buttonDown_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDown);
            buttonDown.FlatStyle = FlatStyle.Flat;
        }
        private void buttonExportToFile_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Экспортировать данные о паролях из реестра в файл", buttonExportToFile, 15, -20);
            buttonExportToFile.FlatStyle = FlatStyle.Popup;
        }
        private void buttonExportToFile_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonExportToFile);
            buttonExportToFile.FlatStyle = FlatStyle.Flat;
        }
        private void buttonImportFromFile_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Импортировать данные о паролях в реестр из файла", buttonImportFromFile, 15, -20);
            buttonImportFromFile.FlatStyle = FlatStyle.Popup;
        }
        private void buttonImportFromFile_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonImportFromFile);
            buttonImportFromFile.FlatStyle = FlatStyle.Flat;
        }
        #endregion

        private Credential GetCredential(int numStr)
        {
            DataGridViewRow rowPass = PassList.Rows[numStr];

            var credential = _credentials.First(x => x.Name == (string)rowPass.Cells[0].Value && x.Login == (string)rowPass.Cells[1].Value && x.Password == (string)rowPass.Cells[2].Value);

            if (credential == null)
            {
                MessageBox.Show("Внутренняя ошибка. Не найдена запись во внутреннем массиве.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            return credential;
        }

        private Credential GetSelectedCredential()
        {
            return GetCredential(PassList.CurrentCellAddress.Y);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку добавления записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            var addForm = new AddForm(this);
            addForm.ShowDialog();

            if (OneRow[0] != null && OneRow[1] != null && OneRow[2] != null)
            {
                _credentials.Add(new Credential(OneRow));

                SaveChanges();

                FillPassList();
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку удаления записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, EventArgs e)
        {
            if (PassList.Rows.Count == 0)
            {
                return;
            }

            int numStr = PassList.CurrentCellAddress.Y;
            DataGridViewRow rowPass = PassList.Rows[numStr];
            OneRow = new[] { (string)rowPass.Cells[0].Value, (string)rowPass.Cells[1].Value, (string)rowPass.Cells[2].Value };
            var deleteForm = new DeleteForm(this);
            deleteForm.ShowDialog();

            if (OneRow[0] != null && OneRow[1] != null && OneRow[2] != null)
            {
                var credential = GetSelectedCredential();

                _credentials.Remove(credential);

                SaveChanges();

                FillPassList();
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку редактирования записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, EventArgs e)
        {
            if (PassList.Rows.Count == 0)
            {
                return;
            }

            int numStr = PassList.CurrentCellAddress.Y;
            DataGridViewRow rowPass = PassList.Rows[numStr];
            OneRow = new[] { (string)rowPass.Cells[0].Value, (string)rowPass.Cells[1].Value, (string)rowPass.Cells[2].Value };
            var editForm = new EditForm(this);
            editForm.ShowDialog();

            if (OneRow[0] != null && OneRow[1] != null && OneRow[2] != null)
            {
                var credential = GetSelectedCredential();

                credential.Name = OneRow[0];
                credential.Login = OneRow[1];
                credential.Password = OneRow[2];

                SaveChanges();

                FillPassList();
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (PassList.Rows.Count == 0)
            {
                return;
            }

            int numStr = PassList.CurrentCellAddress.Y;
            if (numStr == 0)
            {
                return;
            }

            var currentCredential = GetSelectedCredential();
            var previousCredential = GetCredential(numStr - 1);
            string[] previosRowValues = new[] { previousCredential.Name, previousCredential.Login, previousCredential.Password };

            previousCredential.Name = currentCredential.Name;
            previousCredential.Login = currentCredential.Login;
            previousCredential.Password = currentCredential.Password;

            currentCredential.Name = previosRowValues[0];
            currentCredential.Login = previosRowValues[1];
            currentCredential.Password = previosRowValues[2];

            PassList.CurrentCell = PassList.Rows[numStr - 1].Cells[0];

            SaveChanges();

            FillPassList();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку вниз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (PassList.Rows.Count == 0)
            {
                return;
            }

            int numStr = PassList.CurrentCellAddress.Y;
            if (numStr + 1 == PassList.Rows.Count)
            {
                return;
            }

            var currentCredential = GetSelectedCredential();
            var nextCredential = GetCredential(numStr + 1);
            string[] nextRowValues = new[] { nextCredential.Name, nextCredential.Login, nextCredential.Password };

            nextCredential.Name = currentCredential.Name;
            nextCredential.Login = currentCredential.Login;
            nextCredential.Password = currentCredential.Password;

            currentCredential.Name = nextRowValues[0];
            currentCredential.Login = nextRowValues[1];
            currentCredential.Password = nextRowValues[2];

            PassList.CurrentCell = PassList.Rows[numStr + 1].Cells[0];

            SaveChanges();

            FillPassList();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, EventArgs e)
        {
            _isNeedToExit = true;
            Close();
        }


        public string PassStr = "";

        /// <summary>
        /// Обработчик открытия формы и извлечения всех записей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            var passForm = new PassForm(this);
            passForm.ShowDialog();

            if (GetHash(PassStr) == 991202944)
            {
                LoadCredentials();

                FillPassList();

                RegistryKey regKey = Registry.LocalMachine;
                regKey = regKey.CreateSubKey("Software\\PassStore\\");
                string s = "";
                s = (string)regKey.GetValue("FormSize", s);
                string[] arrVal = s.Split(new[] { ';' });
                try
                {
                    Size = new Size(Convert.ToInt32(arrVal[0]), Convert.ToInt32(arrVal[1]));
                }
                catch { }

                s = "";
                s = (string)regKey.GetValue("FormLocation", s);
                arrVal = s.Split(new[] { ';' });
                try
                {
                    Location = new Point(Convert.ToInt32(arrVal[0]), Convert.ToInt32(arrVal[1]));
                }
                catch { }

                s = "";
                s = (string)regKey.GetValue("PassListWidth", s);
                arrVal = s.Split(new[] { ';' });
                try
                {
                    PassList.Columns[0].Width = Convert.ToInt32(arrVal[0]);
                    PassList.Columns[1].Width = Convert.ToInt32(arrVal[1]);
                    PassList.Columns[2].Width = Convert.ToInt32(arrVal[2]);
                }
                catch { }

                timerNotification.Enabled = true;
                timerNotification_Tick(null, null);
            }
            else
            {
                MessageBox.Show("Доступ закрыт", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }
            _stopSaveParameters = false;
        }

        private void LoadCredentials()
        {
            string strAll = ZlibClass.GetSavedString();
            if (strAll == "")
            {
                return;
            }

            _credentials = new List<Credential>();

            string[] rows = strAll.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in rows)
            {
                _credentials.Add(new Credential(row));
            }
        }

        private void FillPassList()
        {
            int n = 0;
            foreach (Credential credential in _credentials)
            {
                if (IsAcceptedForFilters(credential))
                {
                    if (n < PassList.Rows.Count)
                    {
                        PassList.Rows[n].Cells[0].Value = credential.Name;
                        PassList.Rows[n].Cells[1].Value = credential.Login;
                        PassList.Rows[n].Cells[2].Value = credential.Password;
                    }
                    else
                    {
                        PassList.Rows.Add(credential.Name, credential.Login, credential.Password);
                    }

                    n++;
                }
            }

            while (PassList.Rows.Count > n)
            {
                PassList.Rows.RemoveAt(PassList.Rows.Count - 1);

            }
        }

        /// <summary>
        /// Проверить, что очередная строка удовлетворяет фильтрам
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        private bool IsAcceptedForFilters(Credential credential)
        {
            if (!string.IsNullOrEmpty(tbFilterName.Text) && !credential.Name.ToLowerInvariant().Contains(tbFilterName.Text.ToLowerInvariant()))
                return false;

            if (!string.IsNullOrEmpty(tbFilterLogin.Text) && !credential.Login.ToLowerInvariant().Contains(tbFilterLogin.Text.ToLowerInvariant()))
                return false;

            if (!string.IsNullOrEmpty(tbFilterPass.Text))
            {
                if (string.IsNullOrEmpty(credential.Password) && tbFilterPass.Text == " ")
                    return true;

                if (!credential.Password.ToLowerInvariant().Contains(tbFilterPass.Text.ToLowerInvariant()))
                    return false;
            }

            return true;
        }


        /// <summary>
        /// Обработчик закрытия формы и сохранения всех записей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isNeedToExit)
            {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            }
        }


        /// <summary>
        /// Копирование информации из ячеек в буфер обмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0 && PassList.Rows.Count > 0 &&
                PassList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null &&
                PassList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                try
                {
                    Clipboard.Clear();
                    Clipboard.SetDataObject(PassList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), false, 5, 200);
                    labelInfo.Text = "Скопировано в буфер: " + PassList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                }
                catch
                {
                    MessageBox.Show(string.Format("Не получилось поместить текcт в буфер. Вероятно, помешал процесс '{0}'. Попробуйте закрыть его и попробовать ещё раз.", GetOpenClipboardWindowText()));
                }
            }
        }

        /// <summary>
        /// Получение имени процесса, который залочил буфер обмена
        /// </summary>
        /// <returns></returns>
        private string GetOpenClipboardWindowText()
        {
            IntPtr hwnd = GetOpenClipboardWindow();
            StringBuilder sb = new StringBuilder(501);
            GetWindowText(hwnd.ToInt32(), sb, 500);
            return sb.ToString();
            // example:
            // skype_plugin_core_proxy_window: 02490E80
        }

        /// <summary>
        /// Сохранение новых размеров формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || WindowState == FormWindowState.Minimized)
                return;

            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");
            regKey.SetValue("FormSize", Size.Width + ";" + Size.Height);
        }


        /// <summary>
        /// Изменение положения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters || WindowState == FormWindowState.Minimized)
                return;

            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");
            regKey.SetValue("FormLocation", Location.X + ";" + Location.Y);
        }


        /// <summary>
        /// Сохранение новой ширины колонок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            tbFilterName.Left = PassList.Left + 2;
            tbFilterName.Width = PassList.Columns[0].Width - 4;
            tbFilterLogin.Left = tbFilterName.Left + tbFilterName.Width + 4;
            tbFilterLogin.Width = PassList.Columns[1].Width - 4;
            tbFilterPass.Left = tbFilterLogin.Left + tbFilterLogin.Width + 4;
            tbFilterPass.Width = PassList.Columns[2].Width - 4;

            if (_stopSaveParameters)
                return;

            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");
            regKey.SetValue("PassListWidth", PassList.Columns[0].Width + ";" + PassList.Columns[1].Width + ";" + PassList.Columns[2].Width);
        }


        /// <summary>
        /// Исчезание нашей формы при сворачивании в трей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Visible = false;
            }
            else
            {
                const string copiedInfo = "Скопировано в буфер: ";
                if (labelInfo.Text.StartsWith(copiedInfo))
                {
                    string clipboardText;
                    try
                    {
                        clipboardText = Clipboard.GetText(TextDataFormat.Text);
                    }
                    catch
                    {
                        clipboardText = null;
                    }

                    string copiedTextInfo = labelInfo.Text.Substring(copiedInfo.Length);

                    if (clipboardText != copiedTextInfo)
                    {
                        labelInfo.Text = "Кликните дважды на ячейке для копирования хранящейся в ней информации";
                    }
                }
            }
        }


        /// <summary>
        /// Восстановление формы из трея
        /// </summary>
        private void RestoreForm()
        {
            _stopSaveParameters = true;
            Visible = true;
            WindowState = FormWindowState.Normal;
            _stopSaveParameters = false;
        }


        /// <summary>
        /// Обработчик нажатия на пункт меню "Восстановить" (для иконки в трее)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void восстановитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RestoreForm();
        }


        /// <summary>
        /// Обработчик нажатия на пункт меню "Выход" (для иконки в трее)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _isNeedToExit = true;
            Close();
        }


        /// <summary>
        /// Восстановление нашей формы при нажатии мышкой на иконку в трее
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                RestoreForm();
            }
        }


        /// <summary>
        /// Показ напоминания
        /// </summary>
        /// <param name="message"></param>
        private void ShowMessage(string message)
        {
            RestoreForm();
            BringToFront();
            var messageForm = new MessageForm(message);
            messageForm.ShowDialog();
            WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Сообщение в 6 часов о том, что надо послать письмо про проделанную работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerNotification_Tick(object sender, EventArgs e)
        {
            try
            {
                const string skypePath = @"c:\Users\Grigoryk\AppData\Local\Temp\SkypeSetup.exe";
                if (File.Exists(skypePath))
                {
                    File.Delete(skypePath);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("An error occured during removing of SkypeSetup.exe: " + ex.Message);
            }

            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday && DateTime.Now.DayOfWeek != DayOfWeek.Saturday &&
                DateTime.Now.Hour == 18 && DateTime.Now.Minute == 0)
            {
                ShowMessage("Пошли отчёт о проделанной за день работе!");
            }
        }

        /// <summary>
        /// Обработчик изменения фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterName_TextChanged(object sender, EventArgs e)
        {
            FillPassList();
        }

        /// <summary>
        /// Экспортировать зашифрованные пароли из реестра в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportToFile_Click(object sender, EventArgs e)
        {
            var filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            filePath = Path.Combine(filePath, "PassData.txt");
            var passData = ZlibClass.GetPassData();

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            File.WriteAllText(filePath, passData);

            MessageBox.Show("Зашифрованные пароли были экспортированы из реестра в файл\r\n" + filePath);
        }

        /// <summary>
        /// Импортировать зашифрованные пароли из файла в реестр
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImportFromFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите импортировать пароли из файла? Текущие пароли будут потеряны безвозвратно!", "Предупреждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            var currentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filePath = Path.Combine(currentDirectory, "PassData.txt");

            if (!File.Exists(filePath))
            {
                openFileDialog.InitialDirectory = currentDirectory;
                if (openFileDialog.ShowDialog() == DialogResult.Cancel)
                {
                    return;
                }

                filePath = openFileDialog.FileName;
            }

            var passData = File.ReadAllText(filePath);

            ZlibClass.SetPassData(passData);

            MessageBox.Show("Зашифрованные пароли были импортированы в реестр из файла\r\n" + filePath);

            LoadCredentials();

            FillPassList();
        }
    }
}
