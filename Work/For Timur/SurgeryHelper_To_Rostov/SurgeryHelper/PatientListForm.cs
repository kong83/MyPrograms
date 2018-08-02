using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class PatientListForm : Form
    {
        private readonly DbEngine _dbEngine;
        private bool _stopSaveParameters;

        private string _pictureBoxInfoMessage = string.Empty;
        private PatientViewForm _addNewPatientForm;

        public PatientListForm(DbEngine dbEngine)
        {
            _stopSaveParameters = true;
            InitializeComponent();

            _dbEngine = dbEngine;
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.PatientFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.PatientFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.PatientFormLocation;
            }

            Size = _dbEngine.ConfigEngine.PatientFormSize;

            string[] widthsList = _dbEngine.ConfigEngine.PatientFormListWidths.Split(
                new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                PatientList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            switch (_dbEngine.ConfigEngine.PatientFormFilterColumnName)
            { 
                case "DeliveryDate":
                    PatientList.Columns[3].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
                case "ReleaseDate":
                    PatientList.Columns[4].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
                case "OperationDate":
                    PatientList.Columns[5].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
                default:
                    PatientList.Columns[2].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
            }

            ShowPatients();

            _stopSaveParameters = false;
        }

        /// <summary>
        /// Отображение полей для фильтрации
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>        
        private void PatientForm_Shown(object sender, EventArgs e)
        {
            textBoxFilterFIO.Top = textBoxFilterDiagnose.Top =
            dateTimePickerFilterDeliveryDateStart.Top = dateTimePickerFilterReleaseDateStart.Top =
            dateTimePickerFilterOperationDateStart.Top = comboBoxFilterOperationCntMode.Top =
            textBoxFilterDN.Top = comboBoxFilterKDMode.Top = textBoxFilterDoctor.Top =
            textBoxFilterNosology.Top = PatientList.Top + PatientList.Height + 6;

            dateTimePickerFilterDeliveryDateEnd.Top = dateTimePickerFilterReleaseDateEnd.Top =
            dateTimePickerFilterOperationDateEnd.Top = textBoxFilterOperationCnt.Top = 
            textBoxFilterKD.Top = textBoxFilterFIO.Top + 34;

            textBoxFilterFIO.Visible = textBoxFilterNosology.Visible =
            textBoxFilterDiagnose.Visible = dateTimePickerFilterDeliveryDateStart.Visible =
            dateTimePickerFilterReleaseDateStart.Visible = dateTimePickerFilterOperationDateStart.Visible =
            textBoxFilterDN.Visible = textBoxFilterKD.Visible = comboBoxFilterKDMode.Visible =
            textBoxFilterDoctor.Visible = comboBoxFilterOperationCntMode.Visible =
            dateTimePickerFilterDeliveryDateEnd.Visible = dateTimePickerFilterReleaseDateEnd.Visible =
            dateTimePickerFilterOperationDateEnd.Visible = textBoxFilterOperationCnt.Visible = true;

            ShowOrHideFilters();
        }

        /// <summary>
        /// Проверка параметров для фильтрации, проверяющих больше/меньше/равно для чилового значения
        /// </summary>
        /// <param name="modeText">Больше/меньше/равно</param>
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
        /// <param name="patientInfo">Информация про пациента</param>
        /// <returns></returns>
        private bool IsThisPatientSatisfyFilterOptions(PatientClass patientInfo)
        {
            // Проверка ФИО
            if (!string.IsNullOrEmpty(textBoxFilterFIO.Text))
            {
                if (!patientInfo.GetFullName().ToLower().Contains(textBoxFilterFIO.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка даты поступления
            if ((dateTimePickerFilterDeliveryDateStart.Checked && DateTime.Compare(dateTimePickerFilterDeliveryDateStart.Value.Date, patientInfo.DeliveryDate.Date) > 0) ||
                 (dateTimePickerFilterDeliveryDateEnd.Checked && DateTime.Compare(dateTimePickerFilterDeliveryDateEnd.Value.Date, patientInfo.DeliveryDate.Date) < 0))
            {
                return false;
            }

            // Проверка даты выписки
            if (dateTimePickerFilterReleaseDateStart.Checked && 
                (!patientInfo.ReleaseDate.HasValue ||
                DateTime.Compare(dateTimePickerFilterReleaseDateStart.Value.Date, patientInfo.ReleaseDate.Value.Date) > 0))
            {
                 return false;
            }

            if (dateTimePickerFilterReleaseDateEnd.Checked &&
                (!patientInfo.ReleaseDate.HasValue ||                
                DateTime.Compare(dateTimePickerFilterReleaseDateEnd.Value.Date, patientInfo.ReleaseDate.Value.Date) < 0))
            {
                return false;
            }

            // Проверка даты последней операции
            int operationsCount = patientInfo.Operations.Count;

            if (dateTimePickerFilterOperationDateStart.Checked &&
                (operationsCount == 0 ||
                DateTime.Compare(dateTimePickerFilterOperationDateStart.Value.Date, patientInfo.Operations[operationsCount - 1].DataOfOperation) > 0))
                 
            {
                return false;
            }

            if (dateTimePickerFilterOperationDateEnd.Checked &&
                (operationsCount == 0 ||                
                 DateTime.Compare(dateTimePickerFilterOperationDateEnd.Value.Date, patientInfo.Operations[operationsCount - 1].DataOfOperation) < 0))
            {
                return false;
            }

            // Проверка нозологии
            if (!string.IsNullOrEmpty(textBoxFilterNosology.Text))
            {
                if (!patientInfo.Nosology.ToLower().Contains(textBoxFilterNosology.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка диагноза
            if (!string.IsNullOrEmpty(textBoxFilterDiagnose.Text))
            {
                if (!patientInfo.Diagnose.ToLower().Contains(textBoxFilterDiagnose.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка количества операций
            if (!CheckFilterWithMode(comboBoxFilterOperationCntMode.Text, textBoxFilterOperationCnt.Text, patientInfo.Operations.Count.ToString()))
            {
                return false;
            }

            // Проверка Д/Н
            if (!string.IsNullOrEmpty(textBoxFilterDN.Text))
            {
                if (string.IsNullOrEmpty(patientInfo.GetDN()) ||
                    !patientInfo.GetDN().ToLower().Contains(textBoxFilterDN.Text.ToLower()))
                {
                    return false;
                }
            }

            // Проверка к/д
            if (!CheckFilterWithMode(comboBoxFilterKDMode.Text, textBoxFilterKD.Text, patientInfo.GetKD()))
            {
                return false;
            }

            // Проверка врача
            if (!string.IsNullOrEmpty(textBoxFilterDoctor.Text))
            {
                if (string.IsNullOrEmpty(patientInfo.DoctorInChargeOfTheCase) ||
                    !patientInfo.DoctorInChargeOfTheCase.ToLower().Contains(textBoxFilterDoctor.Text.ToLower()))
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
                _dbEngine.GeneratePatientList();
                while (listCnt < PatientList.Rows.Count && patientCnt < _dbEngine.PatientList.Length)
                {
                    if (IsThisPatientSatisfyFilterOptions(_dbEngine.PatientList[patientCnt]))
                    {
                        PatientList.Rows[listCnt].Cells[0].Value = _dbEngine.PatientList[patientCnt].Id.ToString();
                        PatientList.Rows[listCnt].Cells[1].Value = (listCnt + 1).ToString();
                        PatientList.Rows[listCnt].Cells[2].Value = _dbEngine.PatientList[patientCnt].GetFullName();
                        PatientList.Rows[listCnt].Cells[3].Value = _dbEngine.PatientList[patientCnt].DeliveryDate.ToString("dd.MM.yyyy HH:mm");

                        PatientList.Rows[listCnt].Cells[4].Value = _dbEngine.PatientList[patientCnt].ReleaseDate.HasValue
                            ? _dbEngine.PatientList[patientCnt].ReleaseDate.Value.ToString("dd.MM.yyyy")
                            : string.Empty;

                        int opeartionCount = _dbEngine.PatientList[patientCnt].Operations.Count;

                        PatientList.Rows[listCnt].Cells[5].Value = opeartionCount > 0
                            ? _dbEngine.PatientList[patientCnt].Operations[opeartionCount - 1].DataOfOperation.ToString("dd.MM.yyyy")
                            : string.Empty;

                        PatientList.Rows[listCnt].Cells[6].Value = _dbEngine.PatientList[patientCnt].Nosology;
                        PatientList.Rows[listCnt].Cells[7].Value = _dbEngine.PatientList[patientCnt].Diagnose.Replace("\r\n", " ");
                        PatientList.Rows[listCnt].Cells[8].Value = opeartionCount.ToString();
                        PatientList.Rows[listCnt].Cells[9].Value = _dbEngine.PatientList[patientCnt].GetDN();
                        PatientList.Rows[listCnt].Cells[10].Value = _dbEngine.PatientList[patientCnt].GetKD();
                        PatientList.Rows[listCnt].Cells[11].Value = _dbEngine.PatientList[patientCnt].DoctorInChargeOfTheCase;
                        listCnt++;
                    }

                    patientCnt++;
                }

                if (patientCnt == _dbEngine.PatientList.Length)
                {
                    while (listCnt < PatientList.Rows.Count)
                    {
                        PatientList.Rows.RemoveAt(listCnt);
                    }
                }
                else
                {
                    while (patientCnt < _dbEngine.PatientList.Length)
                    {
                        if (IsThisPatientSatisfyFilterOptions(_dbEngine.PatientList[patientCnt]))
                        {
                            string releaseDateStr = string.Empty;
                            if (_dbEngine.PatientList[patientCnt].ReleaseDate.HasValue)
                            {
                                releaseDateStr = _dbEngine.PatientList[patientCnt].ReleaseDate.Value.ToString("dd.MM.yyyy");
                            }

                            int opeartionCount = _dbEngine.PatientList[patientCnt].Operations.Count;
                            string operationDateStr = string.Empty;
                            if (opeartionCount > 0)
                            {
                                operationDateStr = _dbEngine.PatientList[patientCnt].Operations[opeartionCount - 1].DataOfOperation.ToString("dd.MM.yyyy");
                            }

                            var param = new[] 
                            {
                                _dbEngine.PatientList[patientCnt].Id.ToString(),
                                (PatientList.Rows.Count + 1).ToString(),
                                _dbEngine.PatientList[patientCnt].GetFullName(),
                                _dbEngine.PatientList[patientCnt].DeliveryDate.ToString("dd.MM.yyyy HH:mm"),
                                releaseDateStr,
                                operationDateStr,
                                _dbEngine.PatientList[patientCnt].Nosology,
                                _dbEngine.PatientList[patientCnt].Diagnose.Replace("\r\n", " "),
                                opeartionCount.ToString(),
                                _dbEngine.PatientList[patientCnt].GetDN(),
                                _dbEngine.PatientList[patientCnt].GetKD(),
                                _dbEngine.PatientList[patientCnt].DoctorInChargeOfTheCase
                            };
                            PatientList.Rows.Add(param);
                        }

                        patientCnt++;
                    }
                }

                Color lightColor = Color.FromArgb(255, 230, 230, 230);
                Color releaseDateColor = Color.FromArgb(255, 180, 255, 50);
                Color noColor = Color.FromArgb(255, 255, 255, 255);
                for (int i = 0; i < PatientList.Rows.Count; i++)
                {
                    PatientClass patientInfo = _dbEngine.GetPatientById(Convert.ToInt32(PatientList.Rows[i].Cells[0].Value));
                    if (!patientInfo.ReleaseDate.HasValue ||
                        ConvertEngine.CompareDateTimes(patientInfo.ReleaseDate.Value, DateTime.Now, false) > 0)
                    {
                        PatientList.Rows[i].DefaultCellStyle.BackColor = lightColor;
                    }
                    else if (ConvertEngine.CompareDateTimes(patientInfo.ReleaseDate.Value, DateTime.Now, false) == 0)
                    {
                        PatientList.Rows[i].DefaultCellStyle.BackColor = releaseDateColor;
                    }
                    else
                    {
                        PatientList.Rows[i].DefaultCellStyle.BackColor = noColor;
                    }
                }

                int patientsWithLineOfCommunication = 0;
                Color lineOfCommunicationColor = Color.FromArgb(255, 255, 180, 180);
                for (int i = 0; i < PatientList.Rows.Count; i++)
                {
                    if (PatientList.Rows[i].DefaultCellStyle.BackColor == lightColor)
                    {
                        PatientClass patientInfo = _dbEngine.GetPatientById(Convert.ToInt32(PatientList.Rows[i].Cells[0].Value));

                        DateTime tempDate = DateTime.Now.AddDays(-14);

                        while (ConvertEngine.CompareDateTimes(tempDate, patientInfo.DeliveryDate, false) > 0)
                        {
                            tempDate = tempDate.AddDays(-14);
                        }

                        if (ConvertEngine.CompareDateTimes(tempDate, patientInfo.DeliveryDate, false) == 0)
                        {
                            PatientList.Rows[i].DefaultCellStyle.BackColor = lineOfCommunicationColor;
                            patientsWithLineOfCommunication++;
                        }
                    }
                }

                if (patientsWithLineOfCommunication > 0)
                {
                    pictureBoxInfo.Visible = true;
                    _pictureBoxInfoMessage = patientsWithLineOfCommunication == 1 
                        ? "Обратите внимание! Есть один пациент, которому необходимо написать ЭТАПНЫЙ ЭПИКРИЗ" 
                        : "Обратите внимание! Есть несколько пациентов, которым необходимо написать ЭТАПНЫЙ ЭПИКРИЗ";
                }
                else
                {
                    pictureBoxInfo.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавить нового пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (_addNewPatientForm == null || _addNewPatientForm.IsDisposed)
            {
                _addNewPatientForm = new PatientViewForm(this, _dbEngine, null) { MdiParent = MdiParent };
                _addNewPatientForm.Show();
            }
            else
            {
                _addNewPatientForm.Focus();
            }
        }

        /// <summary>
        /// Удалить выделенного пациента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = PatientList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                PatientClass patientInfo = GetSelectedPatient();
                if (patientInfo.OpenedPatientViewForm != null && !patientInfo.OpenedPatientViewForm.IsDisposed)
                {
                    MessageBox.Show("Данный пациент заблокирован для удаления,\r\nтак как он в данный момент редактируется.\r\nЗакройте окно просмотра информации по данному пациенту\r\nи попробуйте ещё раз.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить все данные о пациенте " + patientInfo.GetFullName() + "?\r\nДанная операция необратима.", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    _dbEngine.RemovePatient(patientInfo.Id);
                }

                ShowPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///  Получить выделенного пациента
        /// </summary>
        /// <returns></returns>
        private PatientClass GetSelectedPatient()
        {
            int currentNumber = PatientList.CurrentCellAddress.Y;
            int id = Convert.ToInt32(PatientList.Rows[currentNumber].Cells[0].Value);

            foreach (PatientClass patientInfo in _dbEngine.PatientList)
            {
                if (patientInfo.Id == id)
                {
                    return patientInfo;
                }
            }

            throw new Exception("Внутренняя ошибка программы. Пациент с id=" + id + " не найден. Обратитесь к разработчику.");
        }

        /// <summary>
        /// Посмотреть информацию по выделенному пациенту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonView_Click(object sender, EventArgs e)
        {
            if (PatientList.CurrentCellAddress.Y < 0)
            {
                MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            PatientClass patientInfo = GetSelectedPatient();
            if (patientInfo.OpenedPatientViewForm == null || patientInfo.OpenedPatientViewForm.IsDisposed)
            {
                patientInfo.OpenedPatientViewForm = new PatientViewForm(this, _dbEngine, patientInfo) { MdiParent = MdiParent };
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                if (PatientList.CurrentCellAddress.Y < 0)
                {
                    MessageBox.Show("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var newPatientInfo = new PatientClass(GetSelectedPatient()) { Id = 0 };
                newPatientInfo.Patronymic += " COPY";
                _dbEngine.AddPatient(newPatientInfo);
                ShowPatients();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Просмотр пациента при двойном клике по нему
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientList_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                buttonView_Click(null, null);
            }
        }

        #region Сохранение параметров формы
        private void PatientForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.PatientFormLocation = Location;
        }

        private void PatientForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.PatientFormSize = Size;
        }

        private void PatientList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            const int distance = 4;
            textBoxFilterFIO.Left = PatientList.Left + PatientList.Columns[1].Width + distance / 2;
            textBoxFilterFIO.Width = PatientList.Columns[2].Width - distance;

            dateTimePickerFilterDeliveryDateStart.Left =
            dateTimePickerFilterDeliveryDateEnd.Left = textBoxFilterFIO.Left + textBoxFilterFIO.Width + distance;
            dateTimePickerFilterDeliveryDateStart.Width =
            dateTimePickerFilterDeliveryDateEnd.Width = PatientList.Columns[3].Width - distance;

            dateTimePickerFilterReleaseDateStart.Left =
            dateTimePickerFilterReleaseDateEnd.Left = dateTimePickerFilterDeliveryDateStart.Left + dateTimePickerFilterDeliveryDateStart.Width + distance;
            dateTimePickerFilterReleaseDateStart.Width =
            dateTimePickerFilterReleaseDateEnd.Width = PatientList.Columns[4].Width - distance;

            dateTimePickerFilterOperationDateStart.Left =
            dateTimePickerFilterOperationDateEnd.Left = dateTimePickerFilterReleaseDateStart.Left + dateTimePickerFilterReleaseDateStart.Width + distance;
            dateTimePickerFilterOperationDateStart.Width =
            dateTimePickerFilterOperationDateEnd.Width = PatientList.Columns[5].Width - distance;

            textBoxFilterNosology.Left = dateTimePickerFilterOperationDateStart.Left + dateTimePickerFilterOperationDateStart.Width + distance;
            textBoxFilterNosology.Width = PatientList.Columns[6].Width - distance;

            textBoxFilterDiagnose.Left = textBoxFilterNosology.Left + textBoxFilterNosology.Width + distance;
            textBoxFilterDiagnose.Width = PatientList.Columns[7].Width - distance;

            comboBoxFilterOperationCntMode.Left =
            textBoxFilterOperationCnt.Left = textBoxFilterDiagnose.Left + textBoxFilterDiagnose.Width + distance;
            comboBoxFilterOperationCntMode.Width =
            textBoxFilterOperationCnt.Width = PatientList.Columns[8].Width - distance;

            textBoxFilterDN.Left = comboBoxFilterOperationCntMode.Left + comboBoxFilterOperationCntMode.Width + distance;
            textBoxFilterDN.Width = PatientList.Columns[9].Width - distance;

            comboBoxFilterKDMode.Left =
            textBoxFilterKD.Left = textBoxFilterDN.Left + textBoxFilterDN.Width + distance;
            comboBoxFilterKDMode.Width =
            textBoxFilterKD.Width = PatientList.Columns[10].Width - distance;

            textBoxFilterDoctor.Left = textBoxFilterKD.Left + textBoxFilterKD.Width + distance;
            textBoxFilterDoctor.Width = PatientList.Columns[11].Width - distance;

            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < PatientList.ColumnCount; i++)
            {
                widths += PatientList.Columns[i].Width + ";";
            }

            _dbEngine.ConfigEngine.PatientFormListWidths = widths;
        }
        #endregion

        /// <summary>
        /// Отображение пользователей с использованием фильтра через 500 м/с после изменения фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            ShowPatients();
        }

        /// <summary>
        /// Заедейблить или задизейблить кнопку для убирания фильтрации
        /// </summary>
        private void EnableOrDisableRemoveFilterButton()
        {
            if (!string.IsNullOrEmpty(textBoxFilterFIO.Text) ||
                !string.IsNullOrEmpty(textBoxFilterNosology.Text) ||
                !string.IsNullOrEmpty(textBoxFilterDiagnose.Text) ||
                dateTimePickerFilterDeliveryDateStart.Checked ||
                dateTimePickerFilterDeliveryDateEnd.Checked ||
                dateTimePickerFilterReleaseDateStart.Checked ||
                dateTimePickerFilterReleaseDateEnd.Checked ||
                dateTimePickerFilterOperationDateStart.Checked ||
                dateTimePickerFilterOperationDateEnd.Checked ||
                !string.IsNullOrEmpty(textBoxFilterDN.Text) ||
                (!string.IsNullOrEmpty(textBoxFilterKD.Text) && !string.IsNullOrEmpty(comboBoxFilterKDMode.Text)) ||
                !string.IsNullOrEmpty(textBoxFilterDoctor.Text) ||
                (!string.IsNullOrEmpty(textBoxFilterOperationCnt.Text) && !string.IsNullOrEmpty(comboBoxFilterOperationCntMode.Text)))
            {
                buttonFilterRemove.Visible = true;
            }
            else
            {
                buttonFilterRemove.Visible = false;
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
            EnableOrDisableRemoveFilterButton();
        }

        private void comboBoxFilterOperationCntMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
            EnableOrDisableRemoveFilterButton();
        }

        private void dateTimePickerFilter_ValueChanged(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Enabled = true;
            EnableOrDisableRemoveFilterButton();
        }

        /// <summary>
        /// Удалить фильтрацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilterRemove_Click(object sender, EventArgs e)
        {
            textBoxFilterFIO.Text = textBoxFilterNosology.Text =
            textBoxFilterDiagnose.Text = textBoxFilterDN.Text =
            textBoxFilterKD.Text = comboBoxFilterKDMode.Text = textBoxFilterDoctor.Text =
            comboBoxFilterOperationCntMode.Text = textBoxFilterOperationCnt.Text = string.Empty;

            dateTimePickerFilterDeliveryDateEnd.Checked = dateTimePickerFilterDeliveryDateStart.Checked =
            dateTimePickerFilterReleaseDateEnd.Checked = dateTimePickerFilterReleaseDateStart.Checked =
            dateTimePickerFilterOperationDateEnd.Checked = dateTimePickerFilterOperationDateStart.Checked = false;

            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        /// <summary>
        /// Экспортировать список пациентов в Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportToExcel_Click(object sender, EventArgs e)
        {
            var exportedPatientList = new List<PatientClass>();
            for (int i = 0; i < PatientList.Rows.Count; i++)
            {
                exportedPatientList.Add(_dbEngine.GetPatientById(Convert.ToInt32(PatientList.Rows[i].Cells[0].Value.ToString())));
            }

            ExcelExportEngine.Export(exportedPatientList, _dbEngine);
        }

        /// <summary>
        /// Импортировать новый список КСГ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonImportKSG_Click(object sender, EventArgs e)
        {
            new ImportKSGForm(_dbEngine).ShowDialog();
        }

        private void PatientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        /// <summary>
        /// Отображение/скрытие фильтров
        /// </summary>
        private void ShowOrHideFilters()
        {
            buttonHideFilter.Visible = _dbEngine.ConfigEngine.PatientFormIsFilterShowed;
            buttonShowFilter.Visible = !_dbEngine.ConfigEngine.PatientFormIsFilterShowed;
            PatientList.Height = _dbEngine.ConfigEngine.PatientFormIsFilterShowed
                ? buttonExportToExcel.Top - 65
                : PatientList.Height = buttonExportToExcel.Top - 12;
            pictureBoxInfo.Top = _dbEngine.ConfigEngine.PatientFormIsFilterShowed
                ? PatientList.Bottom + 28
                : PatientList.Bottom + 5;

            foreach (Control control in Controls)
            {
                if (control.Name.Contains("Filter") && !control.Name.Contains("button"))
                {
                    control.Visible = _dbEngine.ConfigEngine.PatientFormIsFilterShowed;
                }
            }
        }

        /// <summary>
        /// Обработчик нажатия на кнопку отображения фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowFilter_Click(object sender, EventArgs e)
        {
            _dbEngine.ConfigEngine.PatientFormIsFilterShowed = true;
            ShowOrHideFilters();
        }

        /// <summary>
        /// Обработчик нажатия на кнопку скрытия фильтров
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHideFilter_Click(object sender, EventArgs e)
        {
            _dbEngine.ConfigEngine.PatientFormIsFilterShowed = false;
            ShowOrHideFilters();
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить нового пациента", buttonAdd, 15, -20);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выделенного пациента", buttonDelete, 15, -20);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonView_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Посмотреть информацию по выделенному пациенту", buttonView, 15, -20);
            buttonView.FlatStyle = FlatStyle.Popup;
        }

        private void buttonView_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonView);
            buttonView.FlatStyle = FlatStyle.Flat;
        }

        private void buttonCopy_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Создать копию выделенного пациента", buttonCopy, 15, -20);
            buttonCopy.FlatStyle = FlatStyle.Popup;
        }

        private void buttonCopy_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonCopy);
            buttonCopy.FlatStyle = FlatStyle.Flat;
        }

        private void buttonFilterRemove_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить фильтрацию", buttonFilterRemove, 15, -20);
            buttonFilterRemove.FlatStyle = FlatStyle.Popup;
        }

        private void buttonFilterRemove_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonFilterRemove);
            buttonFilterRemove.FlatStyle = FlatStyle.Flat;
        }

        private void buttonExportToExcel_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Экспортировать список пациентов в Excel", buttonExportToExcel, 15, -20);
            buttonExportToExcel.FlatStyle = FlatStyle.Popup;
        }

        private void buttonExportToExcel_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonExportToExcel);
            buttonExportToExcel.FlatStyle = FlatStyle.Flat;
        }

        private void buttonImportKSG_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Импортировать новый список КСГ", buttonImportKSG, 15, -20);
            buttonImportKSG.FlatStyle = FlatStyle.Popup;
        }

        private void buttonImportKSG_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonImportKSG);
            buttonImportKSG.FlatStyle = FlatStyle.Flat;
        }

        private void buttonHideFilter_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Скрыть поля для фильтрации", buttonHideFilter, 15, -21);
            buttonHideFilter.FlatStyle = FlatStyle.Popup;
        }

        private void buttonHideFilter_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonHideFilter);
            buttonHideFilter.FlatStyle = FlatStyle.Flat;
        }

        private void buttonShowFilter_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отобразить поля для фильтрации", buttonShowFilter, 15, -21);
            buttonShowFilter.FlatStyle = FlatStyle.Popup;
        }

        private void buttonShowFilter_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonShowFilter);
            buttonShowFilter.FlatStyle = FlatStyle.Flat;
        }

        private void dateTimePickerFilterDeliveryDateStart_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Начальная дата поступления. Будут убраны все пользователи с более ранней датой поступления.", dateTimePickerFilterDeliveryDateStart, 15, -20);
        }

        private void dateTimePickerFilterDeliveryDateStart_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(dateTimePickerFilterDeliveryDateStart);
        }

        private void dateTimePickerFilterDeliveryDateEnd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Конечная дата поступления. Будут убраны все пользователи с более поздней датой поступления.", dateTimePickerFilterDeliveryDateEnd, 15, -20);
        }

        private void dateTimePickerFilterDeliveryDateEnd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(dateTimePickerFilterDeliveryDateEnd);
        }

        private void dateTimePickerFilterReleaseDateStart_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Начальная дата выписки. Будут убраны все пользователи с более ранней датой выписки.", dateTimePickerFilterReleaseDateStart, 15, -20);
        }

        private void dateTimePickerFilterReleaseDateStart_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(dateTimePickerFilterReleaseDateStart);
        }

        private void dateTimePickerFilterReleaseDateEnd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Конечная дата выписки. Будут убраны все пользователи с более поздней датой выписки.", dateTimePickerFilterReleaseDateEnd, 15, -20);
        }

        private void dateTimePickerFilterReleaseDateEnd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(dateTimePickerFilterReleaseDateEnd);
        }

        private void dateTimePickerFilterOperationDateStart_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Начальная дата операции. Будут убраны все пользователи с более ранней датой операции.", dateTimePickerFilterOperationDateStart, 15, -20);
        }

        private void dateTimePickerFilterOperationDateStart_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(dateTimePickerFilterOperationDateStart);
        }

        private void dateTimePickerFilterOperationDateEnd_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Конечная дата операции. Будут убраны все пользователи с более поздней датой операции.", dateTimePickerFilterOperationDateEnd, 15, -20);
        }

        private void dateTimePickerFilterOperationDateEnd_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(dateTimePickerFilterOperationDateEnd);
        }

        private void pictureBoxInfo_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show(_pictureBoxInfoMessage, pictureBoxInfo, 15, -20);
        }

        private void pictureBoxInfo_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(pictureBoxInfo);
        }

        #endregion        

        /// <summary>
        /// Изменение сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PatientList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                return;
            }

            PatientList.Columns[2].HeaderCell.SortGlyphDirection = 
            PatientList.Columns[3].HeaderCell.SortGlyphDirection =
            PatientList.Columns[4].HeaderCell.SortGlyphDirection =
            PatientList.Columns[5].HeaderCell.SortGlyphDirection = SortOrder.None;
            switch (e.ColumnIndex)
            {
                case 2:
                    if (_dbEngine.ConfigEngine.PatientFormFilterColumnName == "Name")
                    {
                        ChangeFilterDirection();
                    }
                    _dbEngine.ConfigEngine.PatientFormFilterColumnName = "Name";
                    PatientList.Columns[2].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
                case 3:
                    if (_dbEngine.ConfigEngine.PatientFormFilterColumnName == "DeliveryDate")
                    {
                        ChangeFilterDirection();
                    }
                    _dbEngine.ConfigEngine.PatientFormFilterColumnName = "DeliveryDate";
                    PatientList.Columns[3].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
                case 4:
                    if (_dbEngine.ConfigEngine.PatientFormFilterColumnName == "ReleaseDate")
                    {
                        ChangeFilterDirection();
                    }
                    _dbEngine.ConfigEngine.PatientFormFilterColumnName = "ReleaseDate";
                    PatientList.Columns[4].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
                case 5:
                    if (_dbEngine.ConfigEngine.PatientFormFilterColumnName == "OperationDate")
                    {
                        ChangeFilterDirection();
                    }
                    _dbEngine.ConfigEngine.PatientFormFilterColumnName = "OperationDate";
                    PatientList.Columns[5].HeaderCell.SortGlyphDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection;
                    break;
            }

            ShowPatients();
        }

        private void ChangeFilterDirection()
        {
            _dbEngine.ConfigEngine.PatientFormFilterDirection = _dbEngine.ConfigEngine.PatientFormFilterDirection == SortOrder.Descending
                ? SortOrder.Ascending
                : SortOrder.Descending;
        }
    }
}
