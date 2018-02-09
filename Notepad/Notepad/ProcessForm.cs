using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Notepad
{
    public partial class ProcessForm : Form
    {
        /// <summary>
        /// Структура, содержащяя информацию об установленных продуктах
        /// </summary>
        struct ProcessesInfo
        {
            /// <summary>
            /// Имя процесса
            /// </summary>
            public string ProcessName;

            /// <summary>
            /// Путь до файла
            /// </summary>
            public string FileName;

            /// <summary>
            /// Время запуска
            /// </summary>
            public string StartTime;

            /// <summary>
            /// Параметры запуска
            /// </summary>
            public string StartInfo;

            /// <summary>
            /// Сам процесс
            /// </summary>
            public Process CurrentProcess;

            public string Id;
        }

        /// <summary>
        /// Массив с данными о процессе
        /// </summary>
        private ProcessesInfo[] _processes;

        /// <summary>
        /// Информация с данными для сохранения в реестре
        /// </summary>
        private ProcessParametersInfo _paramInfo;

        /// <summary>
        /// Указатель на главную форму
        /// </summary>
        readonly MainForm _mainForm;

        /// <summary>
        /// Файл с различными функциями
        /// </summary>
        private readonly ActionClass _actClass;

        /// <summary>
        /// Разрешение обработки событий типа ХХХ_Changed
        /// </summary>
        private bool _appStart;

        public ProcessForm(MainForm mf)
        {
            InitializeComponent();

            _mainForm = mf;
            _actClass = new ActionClass();
            _paramInfo = new ProcessParametersInfo();
            _actClass.LoadParameter(out _paramInfo);
        }

        private void ProcessForm_Load(object sender, EventArgs e)
        {
            Location = _paramInfo.ProcessLocation;
            Size = _paramInfo.ProcessSize;
            for (int i = 0; i < _paramInfo.ProcessColumnsWidth.Length; i++)
            {
                try
                {
                    ProcessList.Columns[i].Width = Convert.ToInt32(_paramInfo.ProcessColumnsWidth[i]);
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch { }
                // ReSharper restore EmptyGeneralCatchClause
            }

            buttonGetProgram_Click(null, null);
            _appStart = true;
        }


        /// <summary>
        /// Заполнение таблицы значениями
        /// </summary>
        private void FillTable()
        {
            Process[] processes = Process.GetProcesses();
            ProcessList.Rows.Clear();
            _processes = new ProcessesInfo[processes.Length];
            int n = 0;
            foreach (Process proc in processes)
            {
                string fileName;
                try
                {
                    fileName = proc.MainModule.FileName;
                }
                catch 
                {
                    fileName = string.Empty;
                }

                string startTime;
                try
                {
                    startTime = proc.StartTime.ToString();
                }
                catch 
                {
                    startTime = string.Empty;
                }
                
                _processes[n].ProcessName = proc.ProcessName;
                _processes[n].FileName = fileName;
                _processes[n].StartInfo = proc.StartInfo.Arguments;
                _processes[n].StartTime = startTime;
                _processes[n].Id = proc.Id.ToString();
                _processes[n++].CurrentProcess = proc;                             
            }

            ProcessList.Rows.Clear();
            Application.DoEvents();
            n = 0;

            foreach (ProcessesInfo procInfo in _processes)
            {
                bool isContains = true;
                if (textBoxFilter0.Text != "" && !procInfo.Id.ToLower().Contains(textBoxFilter0.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter1.Text != "" && !procInfo.ProcessName.ToLower().Contains(textBoxFilter1.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter2.Text != "" && !procInfo.FileName.ToLower().Contains(textBoxFilter2.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter3.Text != "" && !procInfo.StartInfo.ToLower().Contains(textBoxFilter3.Text.ToLower()))
                {
                    isContains = false;
                }

                if (textBoxFilter4.Text != "" && !procInfo.StartTime.ToLower().Contains(textBoxFilter4.Text.ToLower()))
                {
                    isContains = false;
                }                

                if (isContains)
                {
                    ProcessList.Rows.Add(new[] { procInfo.Id, procInfo.ProcessName, procInfo.FileName, procInfo.StartInfo, procInfo.StartTime });
                    n++;
                }
            }

            labelProcessCnt.Text = "Всего процессов: " + n;
            if (n != _processes.Length)
            {
                labelProcessCnt.Text += " из " + _processes.Length;
            }
        }

        /// <summary>
        /// Обновить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetProgram_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Получение процесса по имени
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private Process GetProcessById(string id)
        {
            foreach (ProcessesInfo proc in _processes)
            {
                if (proc.Id == id)
                {
                    return proc.CurrentProcess;
                }
            }
            return null;
        }


        private Process GetSelectedProcess()
        {
            if (ProcessList.CurrentCellAddress.Y >= 0 && ProcessList.CurrentCellAddress.Y < ProcessList.Rows.Count)
            {
                return GetProcessById(ProcessList.Rows[ProcessList.CurrentCellAddress.Y].Cells[0].Value.ToString());
            }

            return null;
        }

        /// <summary>
        /// Убить процесс
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveProcess_Click(object sender, EventArgs e)
        {
            Process killProcess = GetSelectedProcess();

            if (killProcess == null)
            {
                MessageBox.Show("Процесс не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show(string.Format("Sы действительно хотите удалить процесс {0}, id = {1}", killProcess.ProcessName, killProcess.Id), "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            try
            {
                killProcess.Kill();
                Thread.Sleep(1000);
                FillTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// Посмотреть дополнительную информацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonViewInfo_Click(object sender, EventArgs e)
        {
            var processInfoForm = new ProcessInfoForm(GetSelectedProcess());
            processInfoForm.ShowDialog();
        }


        /// <summary>
        /// Изменение размера колонок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (!_appStart)
                return;

            _paramInfo.ProcessColumnsWidth = new string[ProcessList.Columns.Count];
            int left = ProcessList.Left;
            const int shift = 2;
            for (int i = 0; i < ProcessList.ColumnCount; i++)
            {
                _paramInfo.ProcessColumnsWidth[i] = "" + ProcessList.Columns[i].Width;
                var textBoxFilter = (TextBox)Controls["textBoxFilter" + i];
                if (textBoxFilter != null)
                {
                    textBoxFilter.Left = left + shift;
                    textBoxFilter.Width = ProcessList.Columns[i].Width - (shift * 2);
                    left += ProcessList.Columns[i].Width;
                }
            }
            _actClass.SaveParameter(_paramInfo);
        }
        

        /// <summary>
        /// Организация ожидания перед фильтрацией
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerFilter_Tick(object sender, EventArgs e)
        {
            timerFilter.Enabled = false;

            FillTable();
        }


        /// <summary>
        /// Отмена сохранения изменения размера колонок в таблице при закрытии формы
        /// (возможно не нужно)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _appStart = false;
        }


        /// <summary>
        /// Изменение языка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessForm_InputLanguageChanged(object sender, InputLanguageChangedEventArgs e)
        {
            _mainForm.MainForm_InputLanguageChanged(sender, e);
        }


        /// <summary>
        /// Изменение размеров полей для фильтрации после загрузки формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessForm_Activated(object sender, EventArgs e)
        {
            int left = ProcessList.Left;
            const int shift = 2;
            for (int i = 0; i < ProcessList.ColumnCount; i++)
            {
                var textBoxFilter = (TextBox)Controls["textBoxFilter" + i];
                if (textBoxFilter != null)
                {
                    textBoxFilter.Left = left + shift;
                    textBoxFilter.Width = ProcessList.Columns[i].Width - (shift * 2);
                    left += ProcessList.Columns[i].Width;
                }
            }
        }


        /// <summary>
        /// Изменение позиции формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessForm_LocationChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;

            _paramInfo.ProcessLocation = Location;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Изменение размера 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProcessForm_SizeChanged(object sender, EventArgs e)
        {
            if (!_appStart)
                return;

            _paramInfo.ProcessSize = Size;
            _actClass.SaveParameter(_paramInfo);
        }


        /// <summary>
        /// Переинициализация времени ожидания перед фильтрацией данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            timerFilter.Enabled = false;

            if (textBoxFilter0.Text == "" && textBoxFilter1.Text == "" && textBoxFilter2.Text == "" && textBoxFilter3.Text == "" && textBoxFilter4.Text == "")
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
            textBoxFilter0.Text = textBoxFilter1.Text = textBoxFilter2.Text = textBoxFilter3.Text = textBoxFilter4.Text = "";
        }


        #region Подсказки
        private void buttonRemoveProcess_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Удалить выделенный процесс", buttonRemoveProcess, -10, -17);
        }

        private void buttonRemoveProcess_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRemoveProcess);
        }

        private void buttonViewInfo_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Посмотреть дополнительную информацию по выделенному процессу", buttonViewInfo, -10, -17);
        }

        private void buttonViewInfo_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonViewInfo);
        }

        private void buttonRemoveFilters_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Снять фильтры", buttonRemoveFilters, -10, -17);
        }

        private void buttonRemoveFilters_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRemoveFilters);
        }

        private void buttonGetProgram_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Обновить список процессов", buttonGetProgram, -10, -17);
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
        #endregion

        private void ProcessList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            buttonViewInfo_Click(null, null);
        }
    }
}
