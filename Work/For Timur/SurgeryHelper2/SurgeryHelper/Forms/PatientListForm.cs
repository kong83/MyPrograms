using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class PatientListForm : Form
    {
        private readonly CWorkersKeeper _workersKeeper;
        private readonly CPatientWorker _patientWorker;
        private readonly CConfigurationEngine _configurationEngine;
        private PatientViewForm _addNewPatientForm;

        private bool _stopSaveParameters;
        private string _pictureBoxInfoMessage = string.Empty;

        public PatientListForm(CWorkersKeeper workersKeeper)
        {
            _stopSaveParameters = true;
            InitializeComponent();
            _workersKeeper = workersKeeper;
            _patientWorker = _workersKeeper.PatientWorker;
            _configurationEngine = workersKeeper.ConfigurationEngine;            
        }

        public void RefreshTable()
        {
            PatientList.Columns[0].Visible = _workersKeeper.GlobalSettings.ShowDbIndexes;
            PatientList_ColumnWidthChanged(null, null);
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.PatientFormLocation.X >= 0 &&
                _configurationEngine.PatientFormLocation.Y >= 0)
            {
                Location = _configurationEngine.PatientFormLocation;
            }

            Size = _configurationEngine.PatientFormSize;

            string[] widthsList = _configurationEngine.PatientFormListWidths.Split(
                new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < Math.Min(widthsList.Length, PatientList.Columns.Count); i++)
            {
                PatientList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            PatientList.Columns[0].Visible = _workersKeeper.GlobalSettings.ShowDbIndexes;

            switch (_configurationEngine.PatientFormFilterColumnName)
            {
                case "id":
                    PatientList.Columns[0].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
                case "VisitDate":
                    PatientList.Columns[6].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
                case "DeliveryDate":
                    PatientList.Columns[7].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
                default:
                    PatientList.Columns[2].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
            }

            ShowPatients();            

            _stopSaveParameters = false;
        }


        /// <summary>
        /// Отображение полей для фильтрации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientForm_Shown(object sender, EventArgs e)
        {
            textBoxFilterFIO.Top = comboBoxFilterAgeMode.Top = textBoxFilterDiagnose.Top =
            textBoxFilterNosology.Top = dateTimePickerFilterDeliveryDateStart.Top =
            dateTimePickerFilterVisitDateStart.Top =
            dateTimePickerFilterReleaseDateStart.Top = comboBoxFilterKDMode.Top = 
            comboBoxFilterHospitalizationCntMode.Top =
            comboBoxFilterVisitCntMode.Top = comboBoxFilterOperationCntMode.Top =
            textBoxFilterOperationType.Top = PatientList.Top + PatientList.Height + 6;

            textBoxFilterAge.Top = dateTimePickerFilterDeliveryDateEnd.Top =
            dateTimePickerFilterReleaseDateEnd.Top =
            dateTimePickerFilterVisitDateEnd.Top =
            textBoxFilterHospitalizationCnt.Top = textBoxFilterVisitCnt.Top =
            textBoxFilterOperationCnt.Top = textBoxFilterKD.Top = textBoxFilterFIO.Top + 26;

            textBoxFilterFIO.Visible = comboBoxFilterAgeMode.Visible = textBoxFilterNosology.Visible =
            textBoxFilterDiagnose.Visible = dateTimePickerFilterDeliveryDateStart.Visible =
            dateTimePickerFilterVisitDateStart.Visible =
            dateTimePickerFilterReleaseDateStart.Visible = textBoxFilterKD.Visible = comboBoxFilterKDMode.Visible =
            dateTimePickerFilterReleaseDateEnd.Visible =
            comboBoxFilterHospitalizationCntMode.Visible = comboBoxFilterVisitCntMode.Visible =
            comboBoxFilterOperationCntMode.Visible = textBoxFilterAge.Visible =
            dateTimePickerFilterDeliveryDateEnd.Visible = textBoxFilterHospitalizationCnt.Visible =
            dateTimePickerFilterVisitDateEnd.Visible =
            textBoxFilterVisitCnt.Visible = textBoxFilterOperationCnt.Visible =
            textBoxFilterOperationType.Visible = true;

            ShowOrHideFilters();
        }


        /// <summary>
        /// Проверка параметров для фильтрации, проверяющих больше/меньше/равно для чилового значения
        /// </summary>
        /// <param name="modeText">Больше(>)/меньше(<!--<-->)/равно(=)</param>
        /// <param name="valueText">Введённое значение</param>
        /// <param name="curentValueText">Текущее значение</param>
        /// <returns></returns>
        private static bool CheckFilterWithMode(string modeText, string valueText, string curentValueText)
        {
            int value;
            if (!string.IsNullOrEmpty(modeText) &&
                !string.IsNullOrEmpty(valueText) &&
                int.TryParse(valueText, out value))                
            {
                int currentValue;
                if (!int.TryParse(curentValueText, out currentValue))
                {
                    return false;
                }

                switch (modeText)
                {
                    case "=":
                        if (currentValue != value)
                        {
                            return false;
                        }

                        break;
                    case "<":
                        if (currentValue >= value)
                        {
                            return false;
                        }

                        break;
                    case ">":
                        if (currentValue <= value)
                        {
                            return false;
                        }

                        break;
                }
            }

            return true;
        }


        /// <summary>
        /// Проверить, удовлетворяет ли переданный пациент условиям фильтра
        /// </summary>
        /// <param name="patientViewInfo">Информация про пациента</param>
        /// <returns></returns>
        private bool IsThisPatientSatisfyFilterOptions(CPatientView patientViewInfo)
        {
            // Проверка ФИО
            if (!string.IsNullOrEmpty(textBoxFilterFIO.Text))
            {
                if (string.IsNullOrEmpty(patientViewInfo.FullName) || 
                    !patientViewInfo.FullName.ToLower().Contains(textBoxFilterFIO.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка возраста
            if (!CheckFilterWithMode(comboBoxFilterAgeMode.Text, textBoxFilterAge.Text, patientViewInfo.Age))
            {
                return false;
            }


            // Проверка нозологии
            if (!string.IsNullOrEmpty(textBoxFilterNosology.Text))
            {
                if (string.IsNullOrEmpty(patientViewInfo.Nosology) || 
                    !patientViewInfo.Nosology.ToLower().Contains(textBoxFilterNosology.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка диагноза
            if (!string.IsNullOrEmpty(textBoxFilterDiagnose.Text))
            {
                if (string.IsNullOrEmpty(patientViewInfo.Diagnose) || 
                    !patientViewInfo.Diagnose.ToLower().Contains(textBoxFilterDiagnose.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка начальной даты поступления
            if (dateTimePickerFilterDeliveryDateStart.Checked)
            {
                var filterDeliveryDateStart = new DateTime(
                    dateTimePickerFilterDeliveryDateStart.Value.Year,
                    dateTimePickerFilterDeliveryDateStart.Value.Month,
                    dateTimePickerFilterDeliveryDateStart.Value.Day,
                    0, 
                    0, 
                    0);
                if (string.IsNullOrEmpty(patientViewInfo.DeliveryDateString) || 
                    CCompareEngine.CompareDate(filterDeliveryDateStart, CConvertEngine.StringToDateTime(patientViewInfo.DeliveryDateString)) > 0)
                {
                    return false;
                }
            }

            // Проверка конечной даты послупления
            if (dateTimePickerFilterDeliveryDateEnd.Checked)
            {
                var filterDeliveryDateEnd = new DateTime(
                    dateTimePickerFilterDeliveryDateEnd.Value.Year,
                    dateTimePickerFilterDeliveryDateEnd.Value.Month,
                    dateTimePickerFilterDeliveryDateEnd.Value.Day,
                    23, 
                    59, 
                    59);
                if (string.IsNullOrEmpty(patientViewInfo.DeliveryDateString) || 
                    CCompareEngine.CompareDate(filterDeliveryDateEnd, CConvertEngine.StringToDateTime(patientViewInfo.DeliveryDateString)) < 0)
                {
                    return false;
                }
            }

            // Проверка начальной даты выписки
            if (dateTimePickerFilterReleaseDateStart.Checked)
            {
                var filterReleaseDateStart = new DateTime(
                    dateTimePickerFilterReleaseDateStart.Value.Year,
                    dateTimePickerFilterReleaseDateStart.Value.Month,
                    dateTimePickerFilterReleaseDateStart.Value.Day,
                    0,
                    0,
                    0);
                if (string.IsNullOrEmpty(patientViewInfo.ReleaseDateString) ||
                    CCompareEngine.CompareDate(filterReleaseDateStart, CConvertEngine.StringToDateTime(patientViewInfo.ReleaseDateString)) > 0)
                {
                    return false;
                }
            }

            // Проверка конечной даты выписки
            if (dateTimePickerFilterReleaseDateEnd.Checked)
            {
                var filterReleaseDateEnd = new DateTime(
                    dateTimePickerFilterReleaseDateEnd.Value.Year,
                    dateTimePickerFilterReleaseDateEnd.Value.Month,
                    dateTimePickerFilterReleaseDateEnd.Value.Day,
                    23,
                    59,
                    59);
                if (string.IsNullOrEmpty(patientViewInfo.ReleaseDateString) ||
                    CCompareEngine.CompareDate(filterReleaseDateEnd, CConvertEngine.StringToDateTime(patientViewInfo.ReleaseDateString)) < 0)
                {
                    return false;
                }
            }

            // Проверка начальной даты консультации
            if (dateTimePickerFilterVisitDateStart.Checked)
            {
                var filterVisitDateStart = new DateTime(
                    dateTimePickerFilterVisitDateStart.Value.Year,
                    dateTimePickerFilterVisitDateStart.Value.Month,
                    dateTimePickerFilterVisitDateStart.Value.Day,
                    0,
                    0,
                    0);
                if (string.IsNullOrEmpty(patientViewInfo.VisitDateString) ||
                    CCompareEngine.CompareDate(filterVisitDateStart, CConvertEngine.StringToDateTime(patientViewInfo.VisitDateString)) > 0)
                {
                    return false;
                }
            }

            // Проверка конечной даты консультации
            if (dateTimePickerFilterVisitDateEnd.Checked)
            {
                var filterVisitDateEnd = new DateTime(
                    dateTimePickerFilterVisitDateEnd.Value.Year,
                    dateTimePickerFilterVisitDateEnd.Value.Month,
                    dateTimePickerFilterVisitDateEnd.Value.Day,
                    23,
                    59,
                    59);
                if (string.IsNullOrEmpty(patientViewInfo.VisitDateString) ||
                    CCompareEngine.CompareDate(filterVisitDateEnd, CConvertEngine.StringToDateTime(patientViewInfo.VisitDateString)) < 0)
                {
                    return false;
                }
            }           

            // Проверка к/д
            if (!CheckFilterWithMode(comboBoxFilterKDMode.Text, textBoxFilterKD.Text, patientViewInfo.KD))
            {
                return false;
            }            

            // Проверка количества госпитализаций
            if (!CheckFilterWithMode(comboBoxFilterHospitalizationCntMode.Text, textBoxFilterHospitalizationCnt.Text, patientViewInfo.HospitalizationCnt))
            {
                return false;
            }

            // Проверка количества консультаций
            if (!CheckFilterWithMode(comboBoxFilterVisitCntMode.Text, textBoxFilterVisitCnt.Text, patientViewInfo.VisitCnt))
            {
                return false;
            }

            // Проверка количества операций
            if (!CheckFilterWithMode(comboBoxFilterOperationCntMode.Text, textBoxFilterOperationCnt.Text, patientViewInfo.OperationCnt))
            {
                return false;
            }

            // Проверка типа операций
            if (!string.IsNullOrEmpty(textBoxFilterOperationType.Text))
            {
                if (string.IsNullOrEmpty(patientViewInfo.OperationTypes) ||
                    !patientViewInfo.OperationTypes.ToLower().Contains(textBoxFilterOperationType.Text.ToLower()))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Отобразить список пациентов в таблице
        /// </summary>
        public void ShowPatients()
        {
            try
            {
                int listCnt = 0;
                int patientCnt = 0;
                CPatientView[] patientViewList = _patientWorker.PatientListView;
                int patientsWithLineOfCommunication = 0;
                while (listCnt < PatientList.Rows.Count && patientCnt < patientViewList.Length)
                {
                    if (IsThisPatientSatisfyFilterOptions(patientViewList[patientCnt]))
                    {
                        PatientList.Rows[listCnt].Cells[0].Value = patientViewList[patientCnt].Id;
                        PatientList.Rows[listCnt].Cells[1].Value = (listCnt + 1).ToString();
                        PatientList.Rows[listCnt].Cells[2].Value = patientViewList[patientCnt].FullName;
                        PatientList.Rows[listCnt].Cells[3].Value = patientViewList[patientCnt].Age;
                        PatientList.Rows[listCnt].Cells[4].Value = patientViewList[patientCnt].Nosology;
                        PatientList.Rows[listCnt].Cells[5].Value = patientViewList[patientCnt].Diagnose;
                        PatientList.Rows[listCnt].Cells[6].Value = patientViewList[patientCnt].VisitDateString;
                        PatientList.Rows[listCnt].Cells[7].Value = patientViewList[patientCnt].DeliveryDateString;
                        PatientList.Rows[listCnt].Cells[8].Value = patientViewList[patientCnt].ReleaseDateString;
                        PatientList.Rows[listCnt].Cells[9].Value = patientViewList[patientCnt].KD;
                        PatientList.Rows[listCnt].Cells[10].Value = patientViewList[patientCnt].HospitalizationCnt;
                        PatientList.Rows[listCnt].Cells[11].Value = patientViewList[patientCnt].VisitCnt;
                        PatientList.Rows[listCnt].Cells[12].Value = patientViewList[patientCnt].OperationCnt;
                        PatientList.Rows[listCnt].Cells[13].Value = patientViewList[patientCnt].OperationTypes;

                        PatientList.Rows[listCnt].DefaultCellStyle.BackColor = patientViewList[patientCnt].RowColor;
                        if (patientViewList[patientCnt].IsNeedWriteLineOfCommEpicris)
                        {
                            patientsWithLineOfCommunication++;
                        }

                        listCnt++;
                    }

                    patientCnt++;
                }

                if (patientCnt == patientViewList.Length)
                {
                    while (listCnt < PatientList.Rows.Count)
                    {
                        PatientList.Rows.RemoveAt(listCnt);
                    }
                }
                else
                {
                    while (patientCnt < patientViewList.Length)
                    {
                        if (IsThisPatientSatisfyFilterOptions(patientViewList[patientCnt]))
                        {
                            var param = new[] 
                            {
                                patientViewList[patientCnt].Id,
                                (PatientList.Rows.Count + 1).ToString(),
                                patientViewList[patientCnt].FullName,
                                patientViewList[patientCnt].Age,
                                patientViewList[patientCnt].Nosology,
                                patientViewList[patientCnt].Diagnose,
                                patientViewList[patientCnt].VisitDateString,
                                patientViewList[patientCnt].DeliveryDateString,
                                patientViewList[patientCnt].ReleaseDateString,
                                patientViewList[patientCnt].KD,
                                patientViewList[patientCnt].HospitalizationCnt,
                                patientViewList[patientCnt].VisitCnt,
                                patientViewList[patientCnt].OperationCnt,
                                patientViewList[patientCnt].OperationTypes
                            };

                            int lastRowIndex = PatientList.Rows.Count;
                            PatientList.Rows.Add(param);
                            PatientList.Rows[lastRowIndex].DefaultCellStyle.BackColor = patientViewList[patientCnt].RowColor;
                            if (patientViewList[patientCnt].IsNeedWriteLineOfCommEpicris)
                            {
                                patientsWithLineOfCommunication++;
                            }
                        }

                        patientCnt++;
                    }
                }

                if (patientsWithLineOfCommunication > 0)
                {
                    pictureBoxInfo.Visible = true;
                    _pictureBoxInfoMessage = patientsWithLineOfCommunication == 1
                        ? "Обратите внимание! Есть пациент, которому необходимо написать ЭТАПНЫЙ ЭПИКРИЗ"
                        : "Обратите внимание! Есть несколько пациентов, которым необходимо написать ЭТАПНЫЙ ЭПИКРИЗ";
                }
                else
                {
                    pictureBoxInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Добавить нового пациента
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (_addNewPatientForm == null || _addNewPatientForm.IsDisposed)
            {
                try
                {
                    var patientInfo = new CPatient(_patientWorker.GetNewID());
                    _patientWorker.AddWithoutSaving(patientInfo);
                    _addNewPatientForm = new PatientViewForm(_workersKeeper, patientInfo, this, AddUpdate.Add) { MdiParent = MdiParent };
                    _addNewPatientForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.ShowDialog("Внутренняя ошибка программы при добавлении нового пациента:" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                _addNewPatientForm.Focus();
            }
        }


        /// <summary>
        /// Удалить выделенного пациента
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = PatientList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CPatient patientInfo = GetSelectedPatient();
                if (patientInfo.OpenedPatientViewForm != null && !patientInfo.OpenedPatientViewForm.IsDisposed)
                {
                    MessageBox.ShowDialog("Данный пациент заблокирован для удаления,\r\nтак как он в данный момент редактируется.\r\nЗакройте окно просмотра информации по данному пациенту\r\nи попробуйте ещё раз.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes == MessageBox.ShowDialog("Вы уверены, что хотите удалить все данные о пациенте " + patientInfo.GetFullName() + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _patientWorker.Remove(patientInfo.Id);
                }

                ShowPatients();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        ///  Получить выделенного пациента
        /// </summary>
        /// <returns></returns>
        private CPatient GetSelectedPatient()
        {
            int currentNumber = PatientList.CurrentCellAddress.Y;
            int id = Convert.ToInt32(PatientList.Rows[currentNumber].Cells[0].Value);

            try
            {
                return _patientWorker.GetById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Внутренняя ошибка программы. Пациент с id=" + id + " не найден. Обратитесь к разработчику.", ex);
            }
        }


        /// <summary>
        /// Посмотреть информацию по выделенному пациенту
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonView_Click(object sender, EventArgs e)
        {
            if (PatientList.CurrentCellAddress.Y < 0)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CPatient patientInfo = GetSelectedPatient();
            if (patientInfo.OpenedPatientViewForm == null || patientInfo.OpenedPatientViewForm.IsDisposed)
            {
                patientInfo.OpenedPatientViewForm = new PatientViewForm(_workersKeeper, patientInfo, this, AddUpdate.Update) { MdiParent = MdiParent };
                patientInfo.OpenedPatientViewForm.Show();
            }
            else
            {
                patientInfo.OpenedPatientViewForm.Focus();
            }
        }


        /// <summary>
        /// Сделать копию выделенного пациента
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (PatientList.CurrentCellAddress.Y < 0)
                {
                    MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                CPatient patientInfo = GetSelectedPatient();
                _patientWorker.Copy(patientInfo);
                ShowPatients();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Просмотр пациента при двойном клике по нему
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                buttonView_Click(null, null);
            }
        }


        /// <summary>
        /// Отображение пациентов с использованием фильтра через 500 м/с после изменения фильтров
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void timerShowPatients_Tick(object sender, EventArgs e)
        {
            timerShowPatients.Enabled = false;
            ShowPatients();
        }


        /// <summary>
        /// Заедейблить или задизейблить кнопку для убирания фильтрации
        /// </summary>
        private void EnableOrDisableRemoveFilterButton()
        {
// ReSharper disable ConditionIsAlwaysTrueOrFalse
            if (!string.IsNullOrEmpty(textBoxFilterFIO.Text) ||
                (!string.IsNullOrEmpty(textBoxFilterAge.Text) && !string.IsNullOrEmpty(comboBoxFilterAgeMode.Text)) ||
                !string.IsNullOrEmpty(textBoxFilterNosology.Text) ||
                !string.IsNullOrEmpty(textBoxFilterDiagnose.Text) ||
                dateTimePickerFilterDeliveryDateStart.Checked ||
                dateTimePickerFilterDeliveryDateEnd.Checked ||
                dateTimePickerFilterReleaseDateStart.Checked ||
                dateTimePickerFilterReleaseDateEnd.Checked ||
                dateTimePickerFilterVisitDateStart.Checked ||
                dateTimePickerFilterVisitDateEnd.Checked ||
                (!string.IsNullOrEmpty(textBoxFilterKD.Text) && !string.IsNullOrEmpty(comboBoxFilterKDMode.Text)) ||
                (!string.IsNullOrEmpty(textBoxFilterHospitalizationCnt.Text) && !string.IsNullOrEmpty(comboBoxFilterHospitalizationCntMode.Text)) ||
                (!string.IsNullOrEmpty(textBoxFilterVisitCnt.Text) && !string.IsNullOrEmpty(comboBoxFilterVisitCntMode.Text)) ||
                (!string.IsNullOrEmpty(textBoxFilterOperationCnt.Text) && !string.IsNullOrEmpty(comboBoxFilterOperationCntMode.Text)) ||
                !string.IsNullOrEmpty(textBoxFilterOperationType.Text))
            {
                buttonFilterRemove.Visible = true;
            }
            else
            {
                buttonFilterRemove.Visible = false;
            }
// ReSharper restore ConditionIsAlwaysTrueOrFalse
        }

        /// <summary>
        /// Включение/выключение фильтрации при изменении значений в полях для фильтрации
        /// </summary>
        private void ReenableFiltration()
        {
            timerShowPatients.Enabled = false;
            timerShowPatients.Enabled = true;
            EnableOrDisableRemoveFilterButton(); 
        }

        /// <summary>
        /// Обработчик изменения текстовых полей для фильтрации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            ReenableFiltration();
        }

        /// <summary>
        /// Обработчик изменения комбобоксов для фильтрации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReenableFiltration();
        }

        /// <summary>
        /// Обработчик изменения полей с датой для фильтрации
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            ReenableFiltration();
        }


        /// <summary>
        /// Убрать фильтрацию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonFilterRemove_Click(object sender, EventArgs e)
        {
            dateTimePickerFilterDeliveryDateEnd.Checked =
            dateTimePickerFilterDeliveryDateStart.Checked =
            dateTimePickerFilterReleaseDateEnd.Checked =
            dateTimePickerFilterReleaseDateStart.Checked =
            dateTimePickerFilterVisitDateEnd.Checked =
            dateTimePickerFilterVisitDateStart.Checked = false;

            textBoxFilterFIO.Text = comboBoxFilterAgeMode.Text = textBoxFilterNosology.Text =
            textBoxFilterDiagnose.Text = textBoxFilterAge.Text = 
            textBoxFilterKD.Text = comboBoxFilterKDMode.Text = 
            comboBoxFilterHospitalizationCntMode.Text =
            comboBoxFilterVisitCntMode.Text = comboBoxFilterOperationCntMode.Text =
            textBoxFilterHospitalizationCnt.Text = textBoxFilterVisitCnt.Text =
            textBoxFilterOperationCnt.Text = textBoxFilterOperationType.Text = string.Empty;

            PatientList.Focus();

            timerShowPatients.Enabled = false;
            timerShowPatients.Enabled = true;

            EnableOrDisableRemoveFilterButton();
        }


        /// <summary>
        /// Экспортировать список пациентов в Excel
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                var importedPatientIds = new List<int>();

                foreach (DataGridViewRow row in PatientList.Rows)
                {
                    importedPatientIds.Add(Convert.ToInt32(row.Cells[0].Value));
                }

                CExcelExportHelper.Export(_workersKeeper, importedPatientIds);

            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Сокрытие формы с пациентами
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PatientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            PatientList.Focus();
        }


        /// <summary>
        /// Обработчик нажатия на кнопку скрытия фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHideFilter_Click(object sender, EventArgs e)
        {
            _configurationEngine.PatientFormIsFilterShowed = false;
            ShowOrHideFilters();
        }


        /// <summary>
        /// Обработчик нажатия на кнопку отображения фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowFilter_Click(object sender, EventArgs e)
        {
            _configurationEngine.PatientFormIsFilterShowed = true;
            ShowOrHideFilters();
        }


        /// <summary>
        /// Отображение/скрытие фильтров
        /// </summary>
        private void ShowOrHideFilters()
        {
            buttonHideFilter.Visible = _configurationEngine.PatientFormIsFilterShowed;
            buttonShowFilter.Visible = !_configurationEngine.PatientFormIsFilterShowed;
            PatientList.Height = _configurationEngine.PatientFormIsFilterShowed
                ? buttonExportToExcel.Top - 65
                : PatientList.Height = buttonExportToExcel.Top - 12;
            pictureBoxInfo.Top = PatientList.Bottom + 5;

            foreach (Control control in Controls)
            {
                if (control.Name.Contains("Filter") && !control.Name.Contains("button"))
                {
                    control.Visible = _configurationEngine.PatientFormIsFilterShowed;
                }
            }
        }


        #region Сохранение параметров формы
        private void PatientForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.PatientFormLocation = Location;
        }


        private void PatientForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.PatientFormSize = Size;
        }


        private void PatientList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            int idColumnWidth = PatientList.Columns[0].Visible
                ? PatientList.Columns[0].Width
                : 0;
            textBoxFilterFIO.Left = PatientList.Left + idColumnWidth + PatientList.Columns[1].Width + 2;
            textBoxFilterFIO.Width = PatientList.Columns[2].Width - 4;

            comboBoxFilterAgeMode.Left =
            textBoxFilterAge.Left = textBoxFilterFIO.Left + textBoxFilterFIO.Width + 4;
            comboBoxFilterAgeMode.Width =
            textBoxFilterAge.Width = PatientList.Columns[3].Width - 4;

            textBoxFilterNosology.Left = textBoxFilterAge.Left + textBoxFilterAge.Width + 4;
            textBoxFilterNosology.Width = PatientList.Columns[4].Width - 4;

            textBoxFilterDiagnose.Left = textBoxFilterNosology.Left + textBoxFilterNosology.Width + 4;
            textBoxFilterDiagnose.Width = PatientList.Columns[5].Width - 4;

            dateTimePickerFilterVisitDateStart.Left =
            dateTimePickerFilterVisitDateEnd.Left = textBoxFilterDiagnose.Left + textBoxFilterDiagnose.Width + 4;
            dateTimePickerFilterVisitDateStart.Width =
            dateTimePickerFilterVisitDateEnd.Width = PatientList.Columns[6].Width - 4;

            dateTimePickerFilterDeliveryDateStart.Left =
            dateTimePickerFilterDeliveryDateEnd.Left = dateTimePickerFilterVisitDateStart.Left + dateTimePickerFilterVisitDateStart.Width + 4;
            dateTimePickerFilterDeliveryDateStart.Width =
            dateTimePickerFilterDeliveryDateEnd.Width = PatientList.Columns[7].Width - 4;

            dateTimePickerFilterReleaseDateStart.Left =
            dateTimePickerFilterReleaseDateEnd.Left = dateTimePickerFilterDeliveryDateStart.Left + dateTimePickerFilterDeliveryDateStart.Width + 4;
            dateTimePickerFilterReleaseDateStart.Width =
            dateTimePickerFilterReleaseDateEnd.Width = PatientList.Columns[8].Width - 4;

            comboBoxFilterKDMode.Left =
            textBoxFilterKD.Left = dateTimePickerFilterReleaseDateStart.Left + dateTimePickerFilterReleaseDateStart.Width + 4;
            comboBoxFilterKDMode.Width =
            textBoxFilterKD.Width = PatientList.Columns[9].Width - 4;

            comboBoxFilterHospitalizationCntMode.Left =
            textBoxFilterHospitalizationCnt.Left = comboBoxFilterKDMode.Left + comboBoxFilterKDMode.Width + 4;
            comboBoxFilterHospitalizationCntMode.Width =
            textBoxFilterHospitalizationCnt.Width = PatientList.Columns[10].Width - 4;

            comboBoxFilterVisitCntMode.Left =
            textBoxFilterVisitCnt.Left = textBoxFilterHospitalizationCnt.Left + textBoxFilterHospitalizationCnt.Width + 4;
            comboBoxFilterVisitCntMode.Width =
            textBoxFilterVisitCnt.Width = PatientList.Columns[11].Width - 4;

            comboBoxFilterOperationCntMode.Left =
            textBoxFilterOperationCnt.Left = textBoxFilterVisitCnt.Left + textBoxFilterVisitCnt.Width + 4;
            comboBoxFilterOperationCntMode.Width =
            textBoxFilterOperationCnt.Width = PatientList.Columns[12].Width - 4;

            textBoxFilterOperationType.Left = comboBoxFilterOperationCntMode.Left + comboBoxFilterOperationCntMode.Width + 4;
            textBoxFilterOperationType.Width = PatientList.Columns[13].Width - 4;

            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < PatientList.ColumnCount; i++)
            {
                widths += PatientList.Columns[i].Width + ";";
            }

            _configurationEngine.PatientFormListWidths = widths;
        }
        #endregion


        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить нового пациента", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выделенного пациента", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonView_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Посмотреть информацию по выделенному пациенту", buttonView);
            buttonView.FlatStyle = FlatStyle.Popup;
        }

        private void buttonView_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonView.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopy_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Создать копию выделенного пациента", buttonCopy);
            buttonCopy.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopy_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonCopy.FlatStyle = FlatStyle.Flat;
        }

        private void buttonHideFilter_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Скрыть поля для фильтрации", buttonHideFilter);
            buttonHideFilter.FlatStyle = FlatStyle.Popup;
        }

        private void buttonHideFilter_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonHideFilter.FlatStyle = FlatStyle.Flat;
        }

        private void buttonShowFilter_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отобразить поля для фильтрации", buttonShowFilter);
            buttonShowFilter.FlatStyle = FlatStyle.Popup;
        }

        private void buttonShowFilter_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonShowFilter.FlatStyle = FlatStyle.Flat;
        }

        private void buttonFilterRemove_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Очистить значения для фильтрации", buttonFilterRemove);
            buttonFilterRemove.FlatStyle = FlatStyle.Popup;
        }

        private void buttonFilterRemove_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonFilterRemove.FlatStyle = FlatStyle.Flat;
        }

        private void buttonExportToExcel_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Экспортировать отсортированных пациентов в Excel", buttonExportToExcel);
            buttonExportToExcel.FlatStyle = FlatStyle.Popup;
        }

        private void buttonExportToExcel_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonExportToExcel.FlatStyle = FlatStyle.Flat;
        }

        private void pictureBoxInfo_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show(_pictureBoxInfoMessage, pictureBoxInfo);
        }

        private void pictureBoxInfo_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        #endregion        

        /// <summary>
        /// Обработка сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                return;
            }

            PatientList.Columns[0].HeaderCell.SortGlyphDirection =
            PatientList.Columns[2].HeaderCell.SortGlyphDirection =
            PatientList.Columns[6].HeaderCell.SortGlyphDirection =            
            PatientList.Columns[7].HeaderCell.SortGlyphDirection = SortOrder.None;
            switch (e.ColumnIndex)
            {
                case 0:
                    if (_configurationEngine.PatientFormFilterColumnName == "id")
                    {
                        ChangeFilterDirection();
                    }

                    _configurationEngine.PatientFormFilterColumnName = "id";
                    PatientList.Columns[0].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
                case 2:
                    if (_configurationEngine.PatientFormFilterColumnName == "Name")
                    {
                        ChangeFilterDirection();
                    }

                    _configurationEngine.PatientFormFilterColumnName = "Name";
                    PatientList.Columns[2].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
                case 6:
                    if (_configurationEngine.PatientFormFilterColumnName == "VisitDate")
                    {
                        ChangeFilterDirection();
                    }

                    _configurationEngine.PatientFormFilterColumnName = "VisitDate";
                    PatientList.Columns[6].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
                case 7:
                    if (_configurationEngine.PatientFormFilterColumnName == "DeliveryDate")
                    {
                        ChangeFilterDirection();
                    }

                    _configurationEngine.PatientFormFilterColumnName = "DeliveryDate";
                    PatientList.Columns[7].HeaderCell.SortGlyphDirection = _configurationEngine.PatientFormFilterDirection;
                    break;
            }

            ShowPatients();
        }

        private void ChangeFilterDirection()
        {
            _configurationEngine.PatientFormFilterDirection = _configurationEngine.PatientFormFilterDirection == SortOrder.Descending
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }        
    }
}
