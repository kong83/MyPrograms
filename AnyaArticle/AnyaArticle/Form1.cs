using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Reflection;
using Microsoft.Win32;

namespace AnyaArticle
{
    public partial class Form1 : Form
    {
        public bool save = true;
        private bool savePar = false;

        public FilterInfo filterInfo = new FilterInfo();
        public FilterForm saveFilterForm;

        public Form1()
        {
            InitializeComponent();
        }        

        // Раскрашивание строк
        public void ColoringRows()
        {
            Color col = Color.FromArgb(255, 240, 240, 240);
            for (int i = 0; i < ArticleList.Rows.Count; i += 2)
                ArticleList.Rows[i].DefaultCellStyle.BackColor = col;
            for (int i = 1; i < ArticleList.Rows.Count; i += 2)
                ArticleList.Rows[i].DefaultCellStyle.BackColor = Color.White;
        }

        // Сохранение параметров
        private void SaveParameter(string s)
        {
            try
            {
                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey("Software\\AnyaArticle");

                switch (s)
                {
                    case "loc":
                        regKey.SetValue("WindowState", this.WindowState.ToString());
                        if (this.WindowState == FormWindowState.Normal)
                            regKey.SetValue("Location", this.Location.X.ToString() + ";" + this.Location.Y.ToString());
                        break;
                    case "size":
                        if (this.WindowState == FormWindowState.Normal)
                            regKey.SetValue("Size", this.Size.Width.ToString() + ";" + this.Size.Height.ToString());
                        break;
                    case "columnWidth":
                        string str = "";
                        for (int i = 0; i < ArticleList.ColumnCount; i++)
                            str += ArticleList.Columns[i].Width.ToString() + ";";
                        regKey.SetValue("ColumnWidth", str);
                        break;
                }
            }
            catch
            {

            }
        }

        // Загрузка таблицы при запуске программы
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Size size = new Size();
                Point p = new Point();
                string winState = "";
                int[] columnWidth = new int[0];

                RegistryKey regKey = Registry.CurrentUser;
                regKey = regKey.CreateSubKey("Software\\AnyaArticle");
                string s;
                // Чтение значений из реестра. Если в реестре нет значений - то ставятся по умолчанию
                FilePath converter = new FilePath();
                s = "";
                s = (string)regKey.GetValue("Size", s);
                size = converter.FromStringToSize(s);

                s = "";
                s = (string)regKey.GetValue("Location", s);
                p = converter.FromStringToPoint(s);

                s = "";
                winState = (string)regKey.GetValue("WindowState", s);

                s = "";
                s = (string)regKey.GetValue("ColumnWidth", s);
                columnWidth = converter.FromStringToArray(s, ';');

                if (!size.IsEmpty)
                    this.Size = size;

                if (!p.IsEmpty)
                    this.Location = p;

                if (winState == "Maximized")
                    this.WindowState = FormWindowState.Maximized;
                else if (winState == "Minimized")
                    this.WindowState = FormWindowState.Minimized;
                else
                    this.WindowState = FormWindowState.Normal;

                if (columnWidth.Length > 0)
                {
                    int minLenght = Math.Min(columnWidth.Length, this.ArticleList.Columns.Count);
                    for (int i = 0; i < minLenght; i++)
                        this.ArticleList.Columns[i].Width = columnWidth[i];
                }
            }
            catch
            {

            }

            try
            {
                dataSet1.ReadXml("AnyaTableProba.xml");
                this.dataSet1.AcceptChanges();
                ColoringRows();
            }
            catch
            {
                MessageBox.Show("Ошибка при загрузке базы данных: " + Application.StartupPath + "AnyaTableProba.xml");
            }
            savePar = true;
        }

        // Закрытие формы
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (save == false)
            {
                DialogResult d = MessageBox.Show("База данных не сохранена. Сохранить?", "Подтверждение", MessageBoxButtons.YesNoCancel);

                if (d == DialogResult.Yes)
                    dataSet1.WriteXml("AnyaTableProba.xml");
                else if (d == DialogResult.Cancel)
                    e.Cancel = true;
            }
            if (!e.Cancel)
            {
                ExitForm ef = new ExitForm();
                ef.ShowDialog();
            }
        }

        // Кнопка "Принять"
        private void Fix_Click(object sender, EventArgs e)
        {
            this.dataSet1.AcceptChanges();
            ColoringRows();
        }

        // Кнопка "Откатить"
        private void Refresh_Click(object sender, EventArgs e)
        {
            this.dataSet1.RejectChanges();
            ColoringRows();
        }

        // Кнопка "Добавить"
        private void Add_Click(object sender, EventArgs e)
        {
            int numStr = ArticleList.CurrentCellAddress.Y;
            AddForm AddUserForm = new AddForm(this);
            AddUserForm.ShowDialog();
            if (numStr > -1)
                ArticleList.CurrentCell = ArticleList.Rows[numStr].Cells[0];
            ColoringRows();
        }

        // Кнопка "Изменить"
        private void Edit_Click(object sender, EventArgs e)
        {
            if (ArticleList.Rows.Count > 0)
            {
                int numStr = ArticleList.CurrentCellAddress.Y;
                EditForm EditUserForm = new EditForm(this);
                EditUserForm.ShowDialog();
                ArticleList.CurrentCell = ArticleList.Rows[numStr].Cells[0];
                ColoringRows();
            }
        }

        // Кнопка "Удалить"
        private void Delete_Click(object sender, EventArgs e)
        {
            if (ArticleList.Rows.Count > 0)
            {
                int numStr = ArticleList.CurrentCellAddress.Y;
                DeleteForm DeleteUserForm = new DeleteForm(this);
                DeleteUserForm.ShowDialog();
                if (ArticleList.Rows.Count > 0)
                    if (ArticleList.Rows.Count <= numStr)
                        ArticleList.CurrentCell = ArticleList.Rows[ArticleList.Rows.Count - 1].Cells[0];
                    else
                        ArticleList.CurrentCell = ArticleList.Rows[numStr].Cells[0];

                ColoringRows();
            }
        }

        // Кнопка "Сохранить"
        private void Save_Click(object sender, EventArgs e)
        {
            this.dataSet1.AcceptChanges();
            dataSet1.WriteXml(Application.StartupPath + "\\AnyaTableProba.xml");
            save = true;
            ColoringRows();
        }

        // Кнопка "Выход"
        private void Exit_Click(object sender, EventArgs e)
        {
            if (save == false)
            {
                DialogResult d = MessageBox.Show("База данных не сохранена. Сохранить?", "Подтверждение", MessageBoxButtons.YesNoCancel);

                if (d == DialogResult.Yes)
                    dataSet1.WriteXml("AnyaTableProba.xml");
                else if (d == DialogResult.Cancel)
                    return;
            }
            save = true;
            this.Close();
        }
        
        // Кнопка "Фильтр"
        private void Filter_Click(object sender, EventArgs e)
        {
            if (saveFilterForm != null && !saveFilterForm.IsDisposed)
            {
                saveFilterForm.Focus();
            }
            else
            {
                FilterForm f4 = new FilterForm(this);
                saveFilterForm = f4;
                f4.Show();
            }
        }

        // Кнопка "Показать все"
        private void ShowAll_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(dataSet1.Tables[0]);

            dv.RowFilter = "";
            ArticleList.DataMember = "";
            ArticleList.DataSource = dv;
            ColoringRows();
        }

        //  Экспорт данных из базы в Excel-файл
        private void Print_Click(object sender, EventArgs e)
        {
            // Создаем массив строк str, состоящий из элементов dataGridView1
            DataGridViewRowCollection drc = ArticleList.Rows;
            string[,] str = new string[ArticleList.Rows.Count, ArticleList.Columns.Count];

            int n = 0, m;
            foreach (DataGridViewRow dr in drc)
            {
                m = 0;
                for (int i = 0; i < dr.Cells.Count; i++)
                {
                    if (dr.Cells[i].Value != null)
                        str[n, m] += dr.Cells[i].Value.ToString();
                    else
                        str[n, m] += "";
                    if (str[n, m].Length > 14)
                    {
                        string s = str[n, m].Substring(11);
                        if (s.Equals("0:00:00"))
                            str[n, m] = str[n, m].Substring(0, 10);
                    }
                    m++;
                }
                n++;
            }
            n = ArticleList.Rows.Count;
            m = ArticleList.Columns.Count;

            object oExcel = new object();
            object[] args;
            string sAppProgID = "Excel.Application";
            try
            {
                // Получение указателя на активный Excel
                oExcel = Marshal.GetActiveObject(sAppProgID);
            }
            catch
            {
                // Создаём Excel и получаем на него указатель
                Type tExcelObj = Type.GetTypeFromProgID(sAppProgID);
                oExcel = Activator.CreateInstance(tExcelObj);
            }

            // Создание объекта для коллекции книг
            object oWorkbooks = oExcel.GetType().InvokeMember("Workbooks", BindingFlags.GetProperty, null, oExcel, null);

            //Создаем новую книгу 
            object oWorkbook = oWorkbooks.GetType().InvokeMember("Add", BindingFlags.InvokeMethod, null, oWorkbooks, null);

            // Создаем переменную под коллекцию листов для oWorkbook
            object oWorksheets = oWorkbook.GetType().InvokeMember("Worksheets", BindingFlags.GetProperty, null, oWorkbook, null);

            // Получаем ссылку на лист 1 
            args = new object[1];
            args[0] = 1;
            object oWorksheet = oWorksheets.GetType().InvokeMember("Item", BindingFlags.GetProperty, null, oWorksheets, args);

            // Устанавливаем ширину столбцов = 25
            object oColumn = oWorksheet.GetType().InvokeMember("Columns", BindingFlags.GetProperty, null, oWorksheet, new object[] { "A:Z" });
            oColumn.GetType().InvokeMember("ColumnWidth", BindingFlags.SetProperty, null, oColumn, new object[] { 25 });

            // Заливаем верхнюю строку (первые m ячеек)
            object oRange;
            object oInterior;
            object oFont;
            char ch = (char)((int)'A' - 1);
            for (int j = 0; j < m; j++)
            {
                ch = (char)((int)ch + 1);
                oRange = oWorksheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, oWorksheet, new object[] { Convert.ToString(ch) + "1" });
                oInterior = oRange.GetType().InvokeMember("Interior", BindingFlags.GetProperty, null, oRange, null);
                oFont = oRange.GetType().InvokeMember("Font", BindingFlags.GetProperty, null, oRange, null);

                oInterior.GetType().InvokeMember("ColorIndex", BindingFlags.SetProperty, null, oInterior, new object[] { 48 });
                oInterior.GetType().InvokeMember("Pattern", BindingFlags.SetProperty, null, oInterior, new object[] { 1 });

                oFont.GetType().InvokeMember("Bold", BindingFlags.SetProperty, null, oFont, new object[] { true });

                oRange.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, null, oRange, new object[] { "@" });
                oRange.GetType().InvokeMember("HorizontalAlignment", BindingFlags.SetProperty, null, oRange, new object[] { -4108 });

                oRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, oRange, new object[] { ArticleList.Columns[j].HeaderText });
            }

            // Записываем в Excel все значения из str и форматируем ячейки
            for (int i = 0; i < n; i++)
            {
                ch = (char)((int)'A' - 1);
                for (int j = 0; j < m; j++)
                {
                    ch = (char)((int)ch + 1);
                    oRange = oWorksheet.GetType().InvokeMember("Range", BindingFlags.GetProperty, null, oWorksheet, new object[] { Convert.ToString(ch) + Convert.ToString(i + 2) });
                    oRange.GetType().InvokeMember("VerticalAlignment", BindingFlags.SetProperty, null, oRange, new object[] { -4107 });
                    oRange.GetType().InvokeMember("NumberFormat", BindingFlags.SetProperty, null, oRange, new object[] { "@" });
                    oRange.GetType().InvokeMember("WrapText", BindingFlags.SetProperty, null, oRange, new object[] { true });
                    oRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, oRange, new object[] { str[i, j] });
                }
            }

            // Закрываем книгу с принятием всех изменений и под определенным названием    
            args = new object[2];
            args[0] = true;
            args[1] = Application.StartupPath + "\\AnyaArticle.xls";
            // Пробуем закрыть  книгу 
            try
            {
                oWorkbook.GetType().InvokeMember("Close", BindingFlags.InvokeMethod, null, oWorkbook, args);
            }
            catch
            {
                MessageBox.Show("Ошибка при закрытии Excel-приложения.");
                return;
            }
            // Уничтожение объекта Excel. 
            Marshal.ReleaseComObject(oExcel);
            // Вызываем сборщик мусора для немедленной очистки памяти
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            MessageBox.Show("Содержимое таблицы успешно экспортировано в файл \"AnyaArticle.xls\".");
            this.BringToFront();
        }

        #region Подсказки
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Зафиксировать изменения", this.button3, 15, -17);
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button3);
        }
        private void button7_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отменить все изменения", this.button7, 15, -17);
        }
        private void button7_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button7);
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить запись", this.button2, 15, -17);
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button2);
        }
        private void button9_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Редактировать запись", this.button9, 15, -17);
        }
        private void button9_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button9);
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить запись", this.button4, 15, -17);
        }
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button4);
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сохранить всё", this.button1, 15, -17);
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button1);
        }
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выход", this.button5, 15, -17);
        }
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button5);
        }
        private void button8_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отфильтровать записи", this.button8, 15, -17);
        }
        private void button8_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button8);
        }
        private void button6_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Показать всё", this.button6, 15, -17);
        }
        private void button6_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button6);
        }
        private void button10_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Экспортировать базу в Excel", this.button10, 15, -17);
        }
        private void button10_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button10);
        }
        #endregion

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            SaveParameter("size");
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            SaveParameter("loc");
        }

        // Сохранение параметров при изменении ширины колонок
        private void ArticleList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (savePar)
                SaveParameter("columnWidth");
        }

        // Показать возможность добавления, если тыкают на пустом месте
        private void ArticleList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y);
        }

        // Выделение строки и показ контекстного меню, когда пользователь выделяет строку правой кнопкой мыши
        private void ArticleList_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    ArticleList.CurrentCell = ArticleList.Rows[e.RowIndex].Cells[0];
                    contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
                }
            }
        }

        // Редактирование при двойном нажатии на строчке
        private void ArticleList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Edit_Click(sender, e);
        }
    }
}
