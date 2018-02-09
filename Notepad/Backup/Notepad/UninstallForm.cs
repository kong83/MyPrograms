using System;
using System.Text;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Notepad
{
    public partial class UninstallForm : Form
    {
        /// <summary>
        /// Структура, содержащяя информацию об установленных продуктах
        /// </summary>
        struct IstallProgramInfo
        {
            /// <summary>
            /// Отображаемое имя
            /// </summary>
            public string DisplayName;

            /// <summary>
            /// Ключ реестра
            /// </summary>
            public string Key;

            /// <summary>
            /// Версия
            /// </summary>
            public string Version;

            /// <summary>
            /// Путь установки
            /// </summary>
            public string InstallPath;
        }


        /// <summary>
        /// Массив с данными об установленных программах
        /// </summary>
        IstallProgramInfo[] m_InstallProgramInfo;

        /// <summary>
        /// Указатель на главную форму
        /// </summary>
        readonly MainForm m_MainForm;

        /// <summary>
        /// Разрешение обработки событий типа ХХХ_Changed
        /// </summary>
        private bool m_AppStart;

        /// <summary>
        /// Файл с различными функциями
        /// </summary>
        private readonly ActionClass m_ActClass;

        /// <summary>
        /// Информация с данными для сохранения в реестре
        /// </summary>
        private UninstallParametersInfo m_ParamInfo;


        /// <summary>
        /// Форма для получения списка установленных программ
        /// </summary>
        /// <param name="mf"></param>
        public UninstallForm(MainForm mf)
        {
            InitializeComponent();

            m_MainForm = mf;
            m_ActClass = new ActionClass();
            m_ActClass.LoadParameter(out m_ParamInfo);            
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallForm_Load(object sender, EventArgs e)
        {
            Location = m_ParamInfo.UninstLocation;
            Size = m_ParamInfo.UninstSize;
            for(int i=0; i< m_ParamInfo.UninstColumnsWidth.Length; i++)
            {
                try
                {
                    ProgramList.Columns[i].Width = Convert.ToInt32(m_ParamInfo.UninstColumnsWidth[i]);
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }
            
            buttonGetProgram_Click(null, null);
            m_AppStart = true;
        }


        /// <summary>
        /// Получение всех значений из реестра
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetProgram_Click(object sender, EventArgs e)
        {            
            int n = 0;
            ProgramList.Rows.Clear();
            RegistryKey reg = Registry.LocalMachine;
            reg = reg.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            if (reg == null)
            {
                MessageBox.Show("Запись в реестре об установленных продуктах не найдена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            string[] subkeys = reg.GetSubKeyNames();
            foreach (string s in subkeys)
            {
                RegistryKey regSub = reg.OpenSubKey(s);
                if (regSub == null)
                {
                    MessageBox.Show("Запись в реестре об установленных продуктах не найдена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                var name = (string)regSub.GetValue("DisplayName");
                var version = (string)regSub.GetValue("DisplayVersion") ?? "";
                var path = (string)regSub.GetValue("InstallLocation") ?? "";                
                if (name != null)
                {
                    ProgramList.Rows.Add(new object[] { name, s, version, path });
                    n++;
                }
            }
            m_InstallProgramInfo = new IstallProgramInfo[n];

            for (int i = 0; i < ProgramList.RowCount; i++)
            {
                m_InstallProgramInfo[i].DisplayName = ProgramList.Rows[i].Cells[0].Value.ToString();
                m_InstallProgramInfo[i].Key = ProgramList.Rows[i].Cells[1].Value.ToString();
                m_InstallProgramInfo[i].Version = ProgramList.Rows[i].Cells[2].Value.ToString();
                m_InstallProgramInfo[i].InstallPath = ProgramList.Rows[i].Cells[3].Value.ToString();
            }

            labelProgramCnt.Text = "Всего программ: " + n;
            if (textBoxFilter0.Text != "" || textBoxFilter1.Text != "" || textBoxFilter2.Text != "" || textBoxFilter3.Text != "")
            {
                timerFilter.Enabled = true;
            }
            else
            {
                buttonRemoveFilters.Enabled = false;
            }
        }


        /// <summary>
        /// Сохранение в буфер значения из ячейки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PassList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && ProgramList.Rows.Count > 0 && 
                ProgramList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null && 
                ProgramList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
            {
                Clipboard.SetText(ProgramList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
                labelInfo.Text = "Скопировано в буфер: " + ProgramList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            }
        }


        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Изменение языка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            m_MainForm.MainForm_InputLanguageChanged(sender, e);
        }


        /// <summary>
        /// Изменение позиции формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallForm_LocationChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
                return;

            m_ParamInfo.UninstLocation = Location;
            m_ActClass.SaveParameter(m_ParamInfo);
        }


        /// <summary>
        /// Копирование полученных данных в основное окно блокнота
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopyData_Click(object sender, EventArgs e)
        {
            var resStr = new StringBuilder("");

            for (int i = 0; i < ProgramList.Rows.Count; i++)
            {
                resStr.Append(ProgramList.Rows[i].Cells[0].Value + "\t\t" +
                    ProgramList.Rows[i].Cells[1].Value + "\t\t" +
                    ProgramList.Rows[i].Cells[2].Value + "\t\t" +
                    ProgramList.Rows[i].Cells[3].Value + "\r\n");
            }

            resStr.Remove(resStr.Length - 2, 2);
            m_MainForm.textWindow.SelectedText = resStr.ToString();
            Close();
        }

        /// <summary>
        /// Изменение размера 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallForm_SizeChanged(object sender, EventArgs e)
        {
            if (!m_AppStart)
                return;

            m_ParamInfo.UninstSize = Size;
            m_ActClass.SaveParameter(m_ParamInfo);
        }

        /// <summary>
        /// Изменение размера колонок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgramList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (!m_AppStart)
                return;

            m_ParamInfo.UninstColumnsWidth = new string[ProgramList.Columns.Count];
            int left = ProgramList.Left;
            const int shift = 2;
            for (int i = 0; i < ProgramList.ColumnCount; i++)
            {
                m_ParamInfo.UninstColumnsWidth[i] = "" + ProgramList.Columns[i].Width;
                var textBoxFilter = (TextBox)Controls["textBoxFilter" + i];
                if (textBoxFilter != null)
                {
                    textBoxFilter.Left = left + shift;
                    textBoxFilter.Width = ProgramList.Columns[i].Width - (shift * 2);
                    left += ProgramList.Columns[i].Width;
                }
            }
            m_ActClass.SaveParameter(m_ParamInfo);
        }

        /// <summary>
        /// Отмена сохранения изменения размера колонок в таблице при закрытии формы
        /// (возможно не нужно)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_AppStart = false;
        }

        #region Подсказки
        private void buttonCopyData_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Скопировать полученные в данные в блокнот", buttonCopyData, -10, -17);
        }
        private void buttonCopyData_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonCopyData);
        }
        private void buttonGetProgram_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Обновить список установленных программ", buttonGetProgram, -10, -17);
        }
        private void buttonGetProgram_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonGetProgram);
        }
        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Закрыть окно", buttonClose, -10, -17);
        }
        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonClose);
        }
        private void buttonRemoveFilters_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Снять фильтры", buttonRemoveFilters, -10, -17);
        }
        private void buttonRemoveFilters_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRemoveFilters);
        }
        #endregion

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


        /// <summary>
        /// Изменение размеров полей для фильтрации после загрузки формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UninstallForm_Activated(object sender, EventArgs e)
        {
            int left = ProgramList.Left;
            const int shift = 2;
            for (int i = 0; i < ProgramList.ColumnCount; i++)
            {
                var textBoxFilter = (TextBox)Controls["textBoxFilter" + i];
                if (textBoxFilter != null)
                {
                    textBoxFilter.Left = left + shift;
                    textBoxFilter.Width = ProgramList.Columns[i].Width - (shift * 2);
                    left += ProgramList.Columns[i].Width;
                }
            }
        }


        /// <summary>
        /// Организация ожидания перед фильтрацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFilter_Tick(object sender, EventArgs e)
        {
            timerFilter.Enabled = false;

            ProgramList.Rows.Clear();
            int n = 0;            

            for (int i = 0; i < m_InstallProgramInfo.Length; i++)
            {
                bool isContains = true;
                if(textBoxFilter0.Text != "" && !m_InstallProgramInfo[i].DisplayName.ToLower().Contains(textBoxFilter0.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter1.Text != "" && !m_InstallProgramInfo[i].Key.ToLower().Contains(textBoxFilter1.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter2.Text != "" && !m_InstallProgramInfo[i].Version.ToLower().Contains(textBoxFilter2.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter3.Text != "" && !m_InstallProgramInfo[i].InstallPath.ToLower().Contains(textBoxFilter3.Text.ToLower()))
                {
                    isContains = false;
                }
                if(isContains)
                {
                    ProgramList.Rows.Add(new object[] { 
                        m_InstallProgramInfo[i].DisplayName, 
                        m_InstallProgramInfo[i].Key,
                        m_InstallProgramInfo[i].Version,
                        m_InstallProgramInfo[i].InstallPath});
                    n++;
                }               
            }

            labelProgramCnt.Text = "Всего программ: " + n;
            if (n != m_InstallProgramInfo.Length)
            {
                labelProgramCnt.Text += " из " + m_InstallProgramInfo.Length;
            }
        }


        /// <summary>
        /// Переинициализация времени ожидания перед фильтрацией данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            timerFilter.Enabled = false;

            if (textBoxFilter0.Text == "" && textBoxFilter1.Text == "" && textBoxFilter2.Text == "" && textBoxFilter3.Text == "")
            {
                buttonRemoveFilters.Enabled = false;
            }
            else
            {
                buttonRemoveFilters.Enabled = true;
            }

            timerFilter.Enabled = true;

        }


        /// <summary>
        /// Удаление всех фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveFilters_Click(object sender, EventArgs e)
        {
            buttonRemoveFilters.Enabled = false;
            textBoxFilter0.Text = textBoxFilter1.Text = textBoxFilter2.Text = textBoxFilter3.Text = "";
        }

    }
}
