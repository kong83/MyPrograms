using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace PassStore
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Массив с данными одной строки для передачи данных между формами
        /// </summary>
        public string[] oneRow = new string[3];

        /// <summary>
        /// Флаг для запрещения/разрешения сохранения изменяющихся настроек
        /// </summary>
        bool stopSaveParameters = true;


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
        private int GetHash(string passStr)
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
        private void SaveCnages()
        {
            if (GetHash(passStr) == 991202944)
            {
                string strAll = "";
                foreach (DataGridViewRow dRow in PassList.Rows)
                {
                    strAll += (string)dRow.Cells[0].Value + "\t" + (string)dRow.Cells[1].Value + "\t" + (string)dRow.Cells[2].Value + "\n";
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
            toolTip1.Show("Добавить новую запись", buttonAdd, 15, -17);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }
        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }
        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выделенную запись", buttonDelete, 15, -17);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }
        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }
        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать выделенную запись", buttonEdit, 15, -17);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }
        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выход из программы", buttonClose, 15, -17);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
        #endregion


        /// <summary>
        /// Обработчик нажатия на кнопку добавления записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm(this);
            addForm.ShowDialog();

            if (oneRow[0] != null && oneRow[1] != null && oneRow[2] != null)
            {
                PassList.Rows.Add(oneRow);
            }

            SaveCnages();
        }


        /// <summary>
        /// Обработчик нажатия на кнопку удаления записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, EventArgs e)
        {
            if (PassList.Rows.Count > 0)
            {
                int numStr = PassList.CurrentCellAddress.Y;
                DataGridViewRow rowPass = PassList.Rows[numStr];
                oneRow = new string[3] { (string)rowPass.Cells[0].Value, (string)rowPass.Cells[1].Value, (string)rowPass.Cells[2].Value };
                DeleteForm deleteForm = new DeleteForm(this);
                deleteForm.ShowDialog();

                if (oneRow[0] == null && oneRow[1] == null && oneRow[2] == null)
                {
                    PassList.Rows.RemoveAt(numStr);
                }

                SaveCnages();
            }
        }


        /// <summary>
        /// Обработчик нажатия на кнопку редактирования записи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, EventArgs e)
        {
            if (PassList.Rows.Count > 0)
            {
                int numStr = PassList.CurrentCellAddress.Y;
                DataGridViewRow rowPass = PassList.Rows[numStr];
                oneRow = new string[3] { (string)rowPass.Cells[0].Value, (string)rowPass.Cells[1].Value, (string)rowPass.Cells[2].Value };
                EditForm editForm = new EditForm(this);
                editForm.ShowDialog();

                if (oneRow[0] != null && oneRow[1] != null && oneRow[2] != null)
                {
                    PassList.Rows[numStr].Cells[0].Value = oneRow[0];
                    PassList.Rows[numStr].Cells[1].Value = oneRow[1];
                    PassList.Rows[numStr].Cells[2].Value = oneRow[2];
                }

                SaveCnages();
            }
        }


        /// <summary>
        /// Обработчик нажатия на кнопку выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public string passStr = "";
        /// <summary>
        /// Обработчик открытия формы и извлечения всех записей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            PassForm passForm = new PassForm(this);
            passForm.ShowDialog();

            if (GetHash(passStr) == 991202944)
            {
                string strAll = ZlibClass.GetSavedString();
                if (strAll == "")
                {
                    return;
                }

                string[] rows = strAll.Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string row in rows)
                {
                    string[] cells = row.Split(new char[1] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    PassList.Rows.Add(cells);
                }

                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey("Software\\PassStore\\");
                string s = "";
                s = (string)regKey.GetValue("FormSize", s);
                string[] arrVal = s.Split(new char[1] { ';' });
                try
                {
                    this.Size = new Size(Convert.ToInt32(arrVal[0]), Convert.ToInt32(arrVal[1]));
                }
                catch { }

                s = "";
                s = (string)regKey.GetValue("FormLocation", s);
                arrVal = s.Split(new char[1] { ';' });
                try
                {
                    this.Location = new Point(Convert.ToInt32(arrVal[0]), Convert.ToInt32(arrVal[1]));
                }
                catch { }

                s = "";
                s = (string)regKey.GetValue("PassListWidth", s);
                arrVal = s.Split(new char[1] { ';' });
                try
                {
                    PassList.Columns[0].Width = Convert.ToInt32(arrVal[0]);
                    PassList.Columns[1].Width = Convert.ToInt32(arrVal[1]);
                    PassList.Columns[2].Width = Convert.ToInt32(arrVal[2]);
                }
                catch { }
            }
            else
            {
                this.Text += ": доступ закрыт";
            }
            stopSaveParameters = false;
        }


        /// <summary>
        /// Обработчик закрытия формы и сохранения всех записей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveCnages();
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
                Clipboard.SetText(PassList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                labelInfo.Text = "Скопировано в буфер: " + PassList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        }


        /// <summary>
        /// Сохранение новых размеров формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (stopSaveParameters || this.WindowState == FormWindowState.Minimized)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");
            regKey.SetValue("FormSize", this.Size.Width + ";" + this.Size.Height);
        }


        /// <summary>
        /// Изменение положения формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (stopSaveParameters || this.WindowState == FormWindowState.Minimized)
                return;

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");
            regKey.SetValue("FormLocation", this.Location.X + ";" + this.Location.Y);
        }


        /// <summary>
        /// Сохранение новой ширины колонок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (stopSaveParameters)
                return;

            RegistryKey regKey = Registry.CurrentUser;
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
            if (this.WindowState == FormWindowState.Minimized)
                this.Visible = false;
        }


        /// <summary>
        /// Восстановление формы из трея
        /// </summary>
        private void RestoreForm()
        {
            stopSaveParameters = true;
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            stopSaveParameters = false;
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
            this.Close();
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
            MessageForm messageForm = new MessageForm(message);
            messageForm.ShowDialog();            
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// Сообщение в 5 часов о том, что надо послать письмо про проделанную работу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerNotification_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday && DateTime.Now.DayOfWeek != DayOfWeek.Saturday && 
                DateTime.Now.Hour == 18 && DateTime.Now.Minute == 0)
            {
                ShowMessage("Пошли отчёт о проделанной за день работе!");
            }

            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.CreateSubKey("Software\\PassStore\\");
            string s = "";
            s = (string)regKey.GetValue("ChangeConfirmitPassDate", s);
            bool showMessage = false;
            try
            {
                DateTime dateChangePass = Convert.ToDateTime(s);
                if (dateChangePass < DateTime.Now)
                {
                    showMessage = true;                    
                    throw new Exception();
                }
            }
            catch
            {
                DateTime dateChangePass;
                if (DateTime.Now.Day < 15)
                {
                    dateChangePass = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15, 9, 0, 0);
                }
                else
                {
                    dateChangePass = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1, 9, 0, 0);
                    dateChangePass = dateChangePass.AddMonths(1);
                }
                regKey.SetValue("ChangeConfirmitPassDate", dateChangePass.ToString());
            }

            if (showMessage)
            {
                ShowMessage("Смени пароль на\r\nhttps://owa.firmglobal.com/owa/auth/logon.aspx?url=https://owa.firmglobal.com/owa/&reason=0");
            }
        }
    }
}
