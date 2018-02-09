using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;

namespace Fonotec
{
    public partial class ExportForm : Form
    {
        readonly List<string[]> m_DataToExport;
        private readonly SettingClass m_SettingClass;

        private bool m_LockChangeSettings = true;

        public ExportForm(List<string[]> dataToExport, SettingClass settingClass)
        {
            InitializeComponent();

            m_DataToExport = dataToExport;
            m_SettingClass = settingClass;
        }


        /// <summary>
        /// Сотрировать по названию диска
        /// </summary>
        private void SortByDisk()
        {
            for (int i = m_DataToExport.Count - 1; i > 1; i--)
            {
                int k = 0;
                for (int j = 1; j <= i; j++)
                {
                    if (Convert.ToInt32(m_DataToExport[k][0]) < Convert.ToInt32(m_DataToExport[j][0]) ||
                        (Convert.ToInt32(m_DataToExport[k][0]) == Convert.ToInt32(m_DataToExport[j][0]) &&
                        string.Compare(m_DataToExport[k][2], m_DataToExport[j][2]) < 0))
                    {
                        k = j;
                    }
                }
                if (k != i)
                {
                    string[] temp = m_DataToExport[i];
                    m_DataToExport[i] = m_DataToExport[k];
                    m_DataToExport[k] = temp;
                }
            }
        }


        /// <summary>
        /// Сотрировать по названию фильма
        /// </summary>
        private void SortByFilm()
        {
            for (int i = m_DataToExport.Count - 1; i > 1; i--)
            {
                int k = 0;
                for (int j = 1; j <= i; j++)
                {
                    if (string.Compare(m_DataToExport[k][2], m_DataToExport[j][2]) < 0)
                        k = j;
                }
                if (k != i)
                {
                    string[] temp = m_DataToExport[i];
                    m_DataToExport[i] = m_DataToExport[k];
                    m_DataToExport[k] = temp;
                }
            }
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportForm_Load(object sender, EventArgs e)
        {
            Location = m_SettingClass.ExportFormLocation;
            Size = m_SettingClass.ExportFormSize;
            checkBoxAll.Checked = m_SettingClass.IsCheckBoxAllChecked;
            checkBoxWithoutDiskInfo.Checked = m_SettingClass.IsCheckBoxWithoutDiskInfoChecked;
            checkBoxWithoutFilmInfo.Checked = m_SettingClass.IsCheckBoxWithoutFilmInfoChecked;
            checkBoxWithoutNumber.Checked = m_SettingClass.IsCheckBoxWithoutNumberChecked;
            radioButtonView1.Checked = m_SettingClass.IsRadioButtonView1Checked;
            radioButtonView2.Checked = m_SettingClass.IsRadioButtonView2Checked;
            radioButtonView3.Checked = m_SettingClass.IsRadioButtonView3Checked;
            m_LockChangeSettings = false;


            if (radioButtonView1.Checked)
                SortByFilm();
            else
                SortByDisk();
            ShowPriview();
            if (checkBoxWithoutNumber.Checked)
            {
                dataGridViewPreview.Columns[0].Visible = false;
            }
            if (checkBoxWithoutDiskInfo.Checked)
            {
                dataGridViewPreview.Columns[1].Visible = false;
            }
            if (checkBoxWithoutFilmInfo.Checked)
            {
                dataGridViewPreview.Columns[3].Visible = false;
            }
        }


        /// <summary>
        /// Показать примерное отображение данных в Excel в зависимости от выбранных настроек
        /// </summary>
        private void ShowPriview()
        {
            dataGridViewPreview.Rows.Clear();
            for (int i = 0; i < m_DataToExport.Count; i++)
            {
                if (radioButtonView3.Checked)
                {
                    if (i == 0 || (m_DataToExport[i - 1][0] != m_DataToExport[i][0]))
                    {
                        dataGridViewPreview.Rows.Add(new[] { 
                            m_DataToExport[i][0],
                            m_DataToExport[i][1],
                            string.Empty,
                            string.Empty});
                    }
                    dataGridViewPreview.Rows.Add(new[] { 
                        string.Empty,
                        string.Empty,
                        m_DataToExport[i][2],
                        m_DataToExport[i][3]});
                }
                else
                {
                    dataGridViewPreview.Rows.Add(new[] { 
                        m_DataToExport[i][0],
                        m_DataToExport[i][1],
                        m_DataToExport[i][2],
                        m_DataToExport[i][3]});
                }
            }

            if (checkBoxWithoutNumber.Checked)
                dataGridViewPreview.Columns[0].Visible = false;
            if (checkBoxWithoutNumber.Checked)
                dataGridViewPreview.Columns[1].Visible = false;
            if (checkBoxWithoutNumber.Checked)
                dataGridViewPreview.Columns[3].Visible = false;
        }

        /// <summary>
        /// Кнопка отмены
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        Excel.Application m_OXl;
        Excel._Workbook m_OWb;
        Excel._Worksheet m_OWs;
        Excel.Range m_OWr;

        /// <summary>
        /// Кнопка экспорта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                // Стартуем Excel-приложение
                m_OXl = new Excel.Application();

                // Создаем новую книгу
                m_OWb = m_OXl.Workbooks.Add(Missing.Value);

                m_OWs = (Excel._Worksheet)m_OWb.Sheets[3];
                m_OWs.Delete();
                m_OWs = (Excel._Worksheet)m_OWb.Sheets[2];
                m_OWs.Delete();

                m_OWs = (Excel._Worksheet)m_OWb.Sheets[1];

                m_OWs.Cells.WrapText = true;
                m_OWs.Cells.VerticalAlignment = 2;
                m_OWs.Cells.HorizontalAlignment = 2;

                m_OWr = m_OWs.get_Range("A1", "D1");
                m_OWr.MergeCells = true;
                m_OWr.Font.Bold = true;
                m_OWr.Font.Size = 14;
                m_OWr.RowHeight = 30;
                m_OWr.HorizontalAlignment = 3;
                m_OWs.Cells[1, 1] = "Список фильмов на " + DateTime.Now.ToShortDateString();

                m_OWr = m_OWs.get_Range("A2", "A2");
                m_OWr.ColumnWidth = 10;
                m_OWr.Font.Bold = true;
                m_OWr.HorizontalAlignment = 3;
                m_OWr.Value2 = "Номер диска";

                m_OWr = m_OWs.get_Range("B2", "B2");
                m_OWr.ColumnWidth = 15;
                m_OWr.Font.Bold = true;
                m_OWr.HorizontalAlignment = 3;
                m_OWr.Value2 = "Тип диска";

                m_OWr = m_OWs.get_Range("C2", "C2");
                m_OWr.ColumnWidth = 50;
                m_OWr.Font.Bold = true;
                m_OWr.HorizontalAlignment = 3;
                m_OWr.Value2 = "Название фильма";

                m_OWr = m_OWs.get_Range("D2", "D2");
                m_OWr.ColumnWidth = 50;
                m_OWr.Font.Bold = true;
                m_OWr.HorizontalAlignment = 3;
                m_OWr.Value2 = "Информация о фильме";

                for (int i = 0; i < dataGridViewPreview.Rows.Count; i++)
                {
                    m_OWs.Cells[i + 3, 1] = dataGridViewPreview.Rows[i].Cells[0].Value.ToString();
                    m_OWs.Cells[i + 3, 2] = dataGridViewPreview.Rows[i].Cells[1].Value.ToString();
                    m_OWs.Cells[i + 3, 3] = dataGridViewPreview.Rows[i].Cells[2].Value.ToString();
                    m_OWs.Cells[i + 3, 4] = dataGridViewPreview.Rows[i].Cells[3].Value.ToString().Replace("\r\n", "\n");
                }

                if (checkBoxWithoutFilmInfo.Checked)
                {
                    m_OWr = m_OWs.get_Range("D1", "D" + (dataGridViewPreview.Rows.Count + 3));
                    m_OWr.Delete(-4159);
                }

                if (checkBoxWithoutDiskInfo.Checked)
                {
                    m_OWr = m_OWs.get_Range("B1", "B" + (dataGridViewPreview.Rows.Count + 3));
                    m_OWr.Delete(-4159);
                }

                if (checkBoxWithoutNumber.Checked)
                {
                    m_OWr = m_OWs.get_Range("A1", "A" + (dataGridViewPreview.Rows.Count + 3));
                    m_OWr.Delete(-4159);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (m_OXl != null)
                {
                    m_OXl.Visible = true;
                    m_OXl.UserControl = true;

                    if (m_OWb != null)
                    {
                        Marshal.ReleaseComObject(m_OWb);
                        m_OWb = null;
                    }
                    if (m_OWs != null)
                    {
                        Marshal.ReleaseComObject(m_OWs);
                        m_OWs = null;
                    }
                    if (m_OWr != null)
                    {
                        Marshal.ReleaseComObject(m_OWr);
                        m_OWr = null;
                    }
                    
                    Marshal.ReleaseComObject(m_OXl);
                    m_OXl = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }
            }
        }


        #region Выбор отображаемых столбцов
        private void SaveCheckBoxValues()
        {
            m_SettingClass.IsCheckBoxAllChecked = checkBoxAll.Checked;
            m_SettingClass.IsCheckBoxWithoutNumberChecked = checkBoxWithoutNumber.Checked;
            m_SettingClass.IsCheckBoxWithoutDiskInfoChecked = checkBoxWithoutDiskInfo.Checked;
            m_SettingClass.IsCheckBoxWithoutFilmInfoChecked = checkBoxWithoutFilmInfo.Checked;
        }

        bool m_IsStopCheckChanged;

        private void checkBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (m_IsStopCheckChanged)
                return;

            m_IsStopCheckChanged = true;
            if (checkBoxAll.Checked)
            {
                checkBoxWithoutDiskInfo.Checked =
                checkBoxWithoutFilmInfo.Checked =
                checkBoxWithoutNumber.Checked = false;
                dataGridViewPreview.Columns[0].Visible =
                dataGridViewPreview.Columns[1].Visible =
                dataGridViewPreview.Columns[3].Visible = true;
            }
            else
            {
                checkBoxWithoutDiskInfo.Checked =
                checkBoxWithoutFilmInfo.Checked =
                checkBoxWithoutNumber.Checked = true;
                dataGridViewPreview.Columns[0].Visible =
                dataGridViewPreview.Columns[1].Visible =
                dataGridViewPreview.Columns[3].Visible = false;
            }
            SaveCheckBoxValues();
            m_IsStopCheckChanged = false;
        }


        private void checkBoxWithoutNumber_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (m_IsStopCheckChanged)
                return;

            m_IsStopCheckChanged = true;
            if (checkBoxWithoutNumber.Checked)
            {
                checkBoxAll.Checked = false;
                dataGridViewPreview.Columns[0].Visible = false;
            }
            else
            {
                dataGridViewPreview.Columns[0].Visible = true;
                if (!checkBoxWithoutDiskInfo.Checked && !checkBoxWithoutFilmInfo.Checked)
                {
                    checkBoxAll.Checked = true;
                }
            }
            SaveCheckBoxValues();
            m_IsStopCheckChanged = false;
        }

        private void checkBoxWithoutDiskInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (m_IsStopCheckChanged)
                return;

            m_IsStopCheckChanged = true;
            if (checkBoxWithoutDiskInfo.Checked)
            {
                checkBoxAll.Checked = false;
                dataGridViewPreview.Columns[1].Visible = false;
            }
            else
            {
                dataGridViewPreview.Columns[1].Visible = true;
                if (!checkBoxWithoutNumber.Checked && !checkBoxWithoutFilmInfo.Checked)
                {
                    checkBoxAll.Checked = true;
                }
            }
            SaveCheckBoxValues();
            m_IsStopCheckChanged = false;
        }

        private void checkBoxWithoutFilmInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (m_IsStopCheckChanged)
                return;

            m_IsStopCheckChanged = true;
            if (checkBoxWithoutFilmInfo.Checked)
            {
                checkBoxAll.Checked = false;
                dataGridViewPreview.Columns[3].Visible = false;
            }
            else
            {
                dataGridViewPreview.Columns[3].Visible = true;
                if (!checkBoxWithoutDiskInfo.Checked && !checkBoxWithoutNumber.Checked)
                {
                    checkBoxAll.Checked = true;
                }
            }
            SaveCheckBoxValues();
            m_IsStopCheckChanged = false;
        }
        #endregion

        #region Изменение стиля экспорта
        private void radioButtonView1_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (radioButtonView1.Checked)
            {
                SortByFilm();
                ShowPriview();
            }
            m_SettingClass.IsRadioButtonView1Checked = radioButtonView1.Checked;
        }

        private void radioButtonView2_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (radioButtonView2.Checked)
            {
                SortByDisk();
                ShowPriview();
            }
            m_SettingClass.IsRadioButtonView2Checked = radioButtonView2.Checked;
        }

        private void radioButtonView3_CheckedChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            if (radioButtonView3.Checked)
            {
                SortByDisk();
                ShowPriview();
            }
            m_SettingClass.IsRadioButtonView3Checked = radioButtonView3.Checked;
        }
        #endregion

        private void ExportForm_SizeChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            m_SettingClass.ExportFormSize = Size;
        }

        private void ExportForm_LocationChanged(object sender, EventArgs e)
        {
            if (m_LockChangeSettings)
                return;

            m_SettingClass.ExportFormLocation = Location;
        }

    }
}
