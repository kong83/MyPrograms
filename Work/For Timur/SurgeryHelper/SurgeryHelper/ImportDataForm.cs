using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class ImportDataForm : Form
    {
        private readonly PatientListForm _patientForm;
        private readonly DbEngine _dbEngine;

        private List<PatientClass> _foreignPatientList;
        private List<NosologyClass> _foreignNosologyList;

        public ImportDataForm(PatientListForm patientListForm, DbEngine dbEngine)
        {
            InitializeComponent();

            _patientForm = patientListForm;
            _dbEngine = dbEngine;

            if (_patientForm.IsDisposed)
            {
                _patientForm.ShowPatients();
            }
        }

        /// <summary>
        /// Импорт данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                var importedPatientList = new List<PatientClass>();
                var importedNosologyList = new List<NosologyClass>();
 
                foreach (int checkedIndex in checkedListBoxForeignPatients.CheckedIndices)
                {
                    importedPatientList.Add(_foreignPatientList[checkedIndex]);                    
                }

                foreach (int checkedIndex in checkedListBoxForeignNosology.CheckedIndices)
                {
                    importedNosologyList.Add(_foreignNosologyList[checkedIndex]);
                }

                _dbEngine.ImportData(importedPatientList, importedNosologyList);

                MessageBox.Show("Импорт данных успешно завершён", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _patientForm.ShowPatients();
                Close();
            }
            catch (Exception ex)
            {
                buttonOk.Enabled = false;
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Выбор папки с файлами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        /// <summary>
        /// Загрузить данные о пациентах и нозогиях из внешней базы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetData_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine(textBoxPath.Text, "patients.save")))
            {
                MessageBox.Show("Файл patients.save в указанной папке не обнаружен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPath.Focus();
                return;
            }

            if (!File.Exists(Path.Combine(textBoxPath.Text, "nosologys.save")))
            {
                MessageBox.Show("Файл nosologys.save в указанной папке не обнаружен", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPath.Focus();
                return;
            }

            try
            {
                _dbEngine.GetImportedData(textBoxPath.Text, out _foreignPatientList, out _foreignNosologyList);

                checkedListBoxForeignPatients.Items.Clear();
                checkedListBoxForeignNosology.Items.Clear();
                
                foreach (PatientClass patientInfo in _foreignPatientList)
                {
                    checkedListBoxForeignPatients.Items.Add(patientInfo.GetFullName());
                }

                foreach (NosologyClass nosologyInfo in _foreignNosologyList)
                {
                    checkedListBoxForeignNosology.Items.Add(nosologyInfo.LastNameWithInitials);
                }

                if (_foreignPatientList.Count == 0 &&
                    _foreignNosologyList.Count == 0)
                {
                    MessageBox.Show("Нет доступных пациентов и нозологий для импорта", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    buttonOk.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Подсказки
        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Импортировать данные", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отказаться от импорта и закрыть форму", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOpen_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать путь до папки с импортируемыми данными", buttonOpen, 15, -20);
            buttonOpen.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOpen_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOpen);
            buttonOpen.FlatStyle = FlatStyle.Flat;
        }

        private void buttonGetData_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Загрузить данные из внешней базы", buttonGetData, 15, -20);
            buttonGetData.FlatStyle = FlatStyle.Popup;
        }

        private void buttonGetData_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonGetData);
            buttonGetData.FlatStyle = FlatStyle.Flat;
        }        
        #endregion
    }
}
