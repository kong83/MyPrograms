using SurgeryHelper.Engines;
using SurgeryHelper.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SurgeryHelper
{
    public partial class PrescriptionForm : Form
    {
        private bool _isFormClosingByButton;

        private readonly PatientClass _patientInfo;
        private readonly DbEngine _dbEngine;
        private bool _stopSaveParameters;

        public PrescriptionForm(DbEngine dbEngine, PatientClass patientInfo)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _dbEngine = dbEngine;
            _patientInfo = patientInfo;
        }

        private void PrescriptionForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.PrescriptionFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.PrescriptionFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.PrescriptionFormLocation;
            }

            Size = _dbEngine.ConfigEngine.PrescriptionFormSize;

            string[] widthsList = _dbEngine.ConfigEngine.PrescriptionFormTherapyListWidths.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                TherapyList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            widthsList = _dbEngine.ConfigEngine.PrescriptionFormSurveyListWidths.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < widthsList.Length; i++)
            {
                SurveyList.Columns[i].Width = Convert.ToInt32(widthsList[i]);
            }

            FillCuresCombobox();
            FillSurveysCombobox();

            ShowTherapy();

            ShowSurveys();

            _stopSaveParameters = false;
        }

        /// <summary>
        /// Показать список с назначенными лекарствами
        /// </summary>
        private void ShowTherapy()
        {
            int listCnt = 0;
            int therapyCnt = 0;
            while (listCnt < TherapyList.Rows.Count && therapyCnt < _patientInfo.PrescriptionTherapy.Count)
            {
                string[] data = _patientInfo.PrescriptionTherapy[therapyCnt].Split(new[] { '&' }, StringSplitOptions.None);
                if (data.Length != 2)
                {
                    continue;
                }

                TherapyList.Rows[listCnt].Cells[0].Value = data[0];
                TherapyList.Rows[listCnt].Cells[1].Value = data[1];
                listCnt++;
                therapyCnt++;
            }

            if (therapyCnt == _patientInfo.PrescriptionTherapy.Count)
            {
                while (listCnt < TherapyList.Rows.Count)
                {
                    TherapyList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (therapyCnt < _patientInfo.PrescriptionTherapy.Count)
                {
                    string[] data = _patientInfo.PrescriptionTherapy[therapyCnt].Split(new[] { '&' }, StringSplitOptions.None);
                    
                    TherapyList.Rows.Add(data);
                    therapyCnt++;
                }
            }
        }

        /// <summary>
        /// Показать список с назначенными дополнительными методами обследования
        /// </summary>
        private void ShowSurveys()
        {
            int listCnt = 0;
            int methodsCnt = 0;
            while (listCnt < SurveyList.Rows.Count && methodsCnt < _patientInfo.PrescriptionSurveys.Count)
            {
                string[] data = _patientInfo.PrescriptionSurveys[methodsCnt].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                if (data.Length != 2)
                {
                    continue;
                }

                SurveyList.Rows[listCnt].Cells[0].Value = data[0];
                SurveyList.Rows[listCnt].Cells[1].Value = data[1];
                SurveyList.Rows[listCnt].Cells[2].Value = data[2];
                listCnt++;
                methodsCnt++;
            }

            if (methodsCnt == _patientInfo.PrescriptionSurveys.Count)
            {
                while (listCnt < SurveyList.Rows.Count)
                {
                    SurveyList.Rows.RemoveAt(listCnt);
                }
            }
            else
            {
                while (methodsCnt < _patientInfo.PrescriptionSurveys.Count)
                {
                    string[] data = _patientInfo.PrescriptionSurveys[methodsCnt].Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

                    SurveyList.Rows.Add(data);
                    methodsCnt++;
                }
            }
        }

        /// <summary>
        /// Поместить строку в указанный объект
        /// </summary>
        /// <param name="objectName">Название объекта, куда класть текст</param>
        /// <param name="str">Текст, который надо положить в объект</param>
        public void PutStringToObject(string objectName, string str)
        {
            switch (objectName)
            {
                case "comboBoxCure":
                    comboBoxCure.Text = str;
                    break;
                case "comboBoxSurvey":
                    comboBoxSurvey.Text = str;
                    break;
            }
        }

        /// <summary>
        /// Кнопка добавления данных к списку назначенных лекарств
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddTherapy_Click(object sender, EventArgs e)
        {
            try
            {
                string str = comboBoxCure.Text + " ";                

                if (!string.IsNullOrEmpty(comboBoxPerDayCnt.Text) ||
                    !string.IsNullOrEmpty(comboBoxReceivingMethod.Text))
                {
                    str += "- ";
                }

                if (!string.IsNullOrEmpty(comboBoxPerDayCnt.Text))
                {
                    string text;
                    int cnt;
                    if (int.TryParse(comboBoxPerDayCnt.Text, out cnt))
                    {
                        int rem = cnt > 10 ? cnt % 10 : cnt;

                        if ((cnt >= 5 && cnt <= 20) || rem < 2 || rem > 4)
                        {
                            text = " раз в день ";
                        }
                        else
                        {
                            text = " раза в день ";
                        }
                    }
                    else
                    {
                        text = " раз в день ";
                    }

                    str += comboBoxPerDayCnt.Text + text;
                }

                if (!string.IsNullOrEmpty(comboBoxReceivingMethod.Text))
                {
                    str += comboBoxReceivingMethod.Text + " ";
                }                

                if (str.Contains("&"))
                {
                    MessageBox.Show("Использование символа '&' в данном месте запрещено т.к. может привести к внутренней ошибке программы. Используйте другой символ.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                TherapyList.Rows.Add(new object[] { str.TrimEnd(), comboBoxDuration.Text, ConvertEngine.GetRightDateString(dateTimePickerPrescriptionStartDate.Value) });

                AddLastUsedPrescription();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Добавить последнее использованное лекарство в списку последних использованных лекарств
        /// </summary>
        private void AddLastUsedPrescription()
        {
            var lastList = new List<string> { comboBoxCure.Text };
            foreach (string prescription in comboBoxCure.Items)
            {
                if (lastList.Count >= 20)
                {
                    break;
                }

                if (!lastList.Contains(prescription) && !string.IsNullOrEmpty(prescription))
                {
                    lastList.Add(prescription);
                }
            }

            _dbEngine.ConfigEngine.PrescriptionFormLastUsedPrescription = lastList.ToArray();

            FillCuresCombobox();
        }

        /// <summary>
        /// Добавить последний использованный дополнительный метод обследования в списку последних использованных методов
        /// </summary>
        private void AddLastUsedSurvey()
        {
            var lastList = new List<string> { comboBoxSurvey.Text };
            foreach (string method in comboBoxSurvey.Items)
            {
                if (lastList.Count >= 20)
                {
                    break;
                }

                if (!lastList.Contains(method) && !string.IsNullOrEmpty(method))
                {
                    lastList.Add(method);
                }
            }

            _dbEngine.ConfigEngine.PrescriptionFormLastUsedSurvey = lastList.ToArray();

            FillSurveysCombobox();
        }

        /// <summary>
        /// Заполнить список последних использованных лекарств
        /// </summary>
        private void FillCuresCombobox()
        {
            comboBoxCure.Items.Clear();
            comboBoxCure.Items.AddRange(_dbEngine.ConfigEngine.PrescriptionFormLastUsedPrescription);
        }

        /// <summary>
        /// Заполнить список последних использованных дополнительных методов обследования
        /// </summary>
        private void FillSurveysCombobox()
        {
            comboBoxSurvey.Items.Clear();
            comboBoxSurvey.Items.AddRange(_dbEngine.ConfigEngine.PrescriptionFormLastUsedSurvey);
        }

        /// <summary>
        /// Кнопка добавления данных в список с дополнительными методами обследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddSurvey_Click(object sender, EventArgs e)
        {
            if (comboBoxSurvey.Text.Contains("&"))
            {
                MessageBox.Show("Использование символа '&' в данном месте запрещено т.к. может привести к внутренней ошибке программы. Используйте другой символ.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SurveyList.Rows.Add(new object[] { comboBoxSurvey.Text, ConvertEngine.GetRightDateString(dateTimePickerSurveyStartDate.Value) });

            AddLastUsedSurvey();
        }

        /// <summary>
        /// Удаление выделенного назначения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteTherapy_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = TherapyList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.Show("Нет выделенных назначений", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить выделенное назначение?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    TherapyList.Rows.RemoveAt(currentNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Удаление выделенного дополнительного метода обследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteSurvey_Click(object sender, EventArgs e)
        {
            try
            {
                int currentNumber = SurveyList.CurrentCellAddress.Y;
                if (currentNumber < 0)
                {
                    MessageBox.Show("Нет выделенных методов обследования", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (DialogResult.Yes == MessageBox.Show("Вы уверены, что хотите удалить выделенный метод обследования?", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    SurveyList.Rows.RemoveAt(currentNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Сдвиг выделенной строки с терапией на шаг вверх
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTherapyUp_Click(object sender, EventArgs e)
        {
            int currentNumber = TherapyList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных назначений", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentNumber == 0)
            {
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                var temp = TherapyList.Rows[currentNumber].Cells[i].Value;
                TherapyList.Rows[currentNumber].Cells[i].Value = TherapyList.Rows[currentNumber - 1].Cells[i].Value;
                TherapyList.Rows[currentNumber - 1].Cells[i].Value = temp;
            }

            TherapyList.CurrentCell = TherapyList.Rows[currentNumber - 1].Cells[0];
        }

        /// <summary>
        ///  Сдвиг выделенной строки с терапией на шаг вниз
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonTherapyDown_Click(object sender, EventArgs e)
        {
            int currentNumber = TherapyList.CurrentCellAddress.Y;
            if (currentNumber < 0)
            {
                MessageBox.Show("Нет выделенных назначений", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (currentNumber == TherapyList.Rows.Count - 1)
            {
                return;
            }

            for (int i = 0; i < 3; i++)
            {
                var temp = TherapyList.Rows[currentNumber].Cells[i].Value;
                TherapyList.Rows[currentNumber].Cells[i].Value = TherapyList.Rows[currentNumber + 1].Cells[i].Value;
                TherapyList.Rows[currentNumber + 1].Cells[i].Value = temp;
            }

            TherapyList.CurrentCell = TherapyList.Rows[currentNumber + 1].Cells[0];
        }

        /// <summary>
        /// Закрытие формы при нажатии кнопку ОК
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Обработка закрытия формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrescriptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если форма была закрыта по крестику - игнорируем это
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
                return;
            }

            // Сохранение списка назначений и дополнительных методов обследования            
            PutDataToPescriptionTherapy(_patientInfo);

            PutDataToSurveys(_patientInfo);
        }

        /// <summary>
        /// Заполнить список терапии
        /// </summary>
        /// <param name="patientInfo"></param>
        private void PutDataToPescriptionTherapy(PatientClass patientInfo)
        {
            patientInfo.PrescriptionTherapy = new List<string>();
            foreach (DataGridViewRow row in TherapyList.Rows)
            {
                patientInfo.PrescriptionTherapy.Add(GetValue(row.Cells[0]) + "&" + GetValue(row.Cells[1]) + "&" + GetValue(row.Cells[2]));
            }
        }

        /// <summary>
        /// Заполнить список дополнительных методов обследования
        /// </summary>
        /// <param name="patientInfo"></param>
        private void PutDataToSurveys(PatientClass patientInfo)
        {
            patientInfo.PrescriptionSurveys = new List<string>();
            foreach (DataGridViewRow row in SurveyList.Rows)
            {
                patientInfo.PrescriptionSurveys.Add(GetValue(row.Cells[0]) + "&" + GetValue(row.Cells[1]));
            }
        }

        /// <summary>
        /// Получить текст из ячейки таблицы без символа &
        /// </summary>
        /// <param name="cell">Ячейка</param>
        /// <returns></returns>
        private string GetValue(DataGridViewCell cell)
        {            
            return cell.Value?.ToString().Replace("&", "");
        }

        /// <summary>
        /// Сгенерировать лист назначений в ворде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportPrescriptionList_Click(object sender, EventArgs e)
        {
            var tempPatientInfo = new PatientClass(_patientInfo);

            PutDataToPescriptionTherapy(tempPatientInfo);

            new WordExportEngine(_dbEngine).ExportPrescriptionTherapy(tempPatientInfo);
        }

        /// <summary>
        /// Сгенерировать лист дополнительных методов обследования в ворде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExportSurveys_Click(object sender, EventArgs e)
        {
            var tempPatientInfo = new PatientClass(_patientInfo);

            PutDataToSurveys(tempPatientInfo);

            new WordExportEngine(_dbEngine).ExportSurveys(tempPatientInfo);
        }

        /// <summary>
        /// Открыть форму со списком лекарств для выбора лекарства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelCure_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new CureForm(_dbEngine, this).ShowDialog();
        }

        /// <summary>
        /// Открыть форму с дополнительными методами обследования для выбора метода обследования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelSurvey_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new SurveyForm(_dbEngine, this).ShowDialog();
        }

        /// <summary>
        /// Выставление дефолтных значений для выбранного лекарства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxCure_TextChanged(object sender, EventArgs e)
        {
            CureClass cureInfo = _dbEngine.GetCureByName(comboBoxCure.Text);

            if (cureInfo != null)
            {
                comboBoxPerDayCnt.Text = cureInfo.DefaultPerDayCount;
                comboBoxReceivingMethod.Text = cureInfo.DefaultReceivingMethod;
                comboBoxDuration.Text = cureInfo.DefaultDuration;
            }
        }

        #region Сохранение параметров
        private void PrescriptionForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.PrescriptionFormLocation = Location;
        }

        private void PrescriptionForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.PrescriptionFormSize = Size;
        }

        private void TherapyList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < TherapyList.ColumnCount; i++)
            {
                widths += TherapyList.Columns[i].Width + ";";
            }

            _dbEngine.ConfigEngine.PrescriptionFormTherapyListWidths = widths;
        }

        private void SurveyList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            string widths = string.Empty;
            for (int i = 0; i < SurveyList.ColumnCount; i++)
            {
                widths += SurveyList.Columns[i].Width + ";";
            }

            _dbEngine.ConfigEngine.PrescriptionFormSurveyListWidths = widths;
        }
        #endregion

        #region Подсказки
        private void buttonAddTherapy_MouseEnter(object sender, EventArgs e)
        {
            buttonAddTherapy.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAddTherapy_MouseLeave(object sender, EventArgs e)
        {
            buttonAddTherapy.FlatStyle = FlatStyle.Flat;
        }

        private void buttonExportPrescriptionList_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сгенерировать лист назначений в ворд", buttonExportPrescriptionList, 15, -20);
            buttonExportPrescriptionList.FlatStyle = FlatStyle.Popup;
        }

        private void buttonExportPrescriptionList_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonExportPrescriptionList);
            buttonExportPrescriptionList.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDeleteTherapy_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выделенную строку", buttonDeleteTherapy, 15, -20);
            buttonDeleteTherapy.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDeleteTherapy_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDeleteTherapy);
            buttonDeleteTherapy.FlatStyle = FlatStyle.Flat;
        }    

        private void buttonAddSurvey_MouseEnter(object sender, EventArgs e)
        {
            buttonAddSurvey.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAddSurvey_MouseLeave(object sender, EventArgs e)
        {
            buttonAddSurvey.FlatStyle = FlatStyle.Flat;
        }

        private void buttonExportSurveys_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сгенерировать лист дополнительных методов обследования в ворд", buttonExportSurveys, 15, -20);
            buttonExportSurveys.FlatStyle = FlatStyle.Popup;
        }

        private void buttonExportSurveys_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonExportSurveys);
            buttonExportSurveys.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDeleteSurvey_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Удалить выделенную строку", buttonDeleteSurvey, 15, -20);
            buttonDeleteSurvey.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDeleteSurvey_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDeleteSurvey);
            buttonDeleteSurvey.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Вернуться к окну с данными о пациенте", buttonOK, 15, -20);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOK);
            buttonOK.FlatStyle = FlatStyle.Flat;
        }

        private void pictureBoxInfo_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Для редактирования данных в таблицах нужно дважды кликнуть на нужной ячейке", pictureBoxInfo, 15, -20);
        }

        private void pictureBoxInfo_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(pictureBoxInfo);
        }

        private void linkLabelCure_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать лекарство из списока лекарств", linkLabelCure, 15, -20);
        }

        private void linkLabelCure_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelCure);
        }

        private void linkLabelSurvey_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Выбрать обследование из списка дополнительных методов обследования", linkLabelSurvey, 15, -20);
        }

        private void linkLabelSurvey_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(linkLabelSurvey);
        }

        private void buttonTherapyUp_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдвинуть выделенную строку на шаг вверх", buttonTherapyUp, 15, -20);
            buttonTherapyUp.FlatStyle = FlatStyle.Popup;
        }

        private void buttonTherapyUp_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonTherapyUp);
            buttonTherapyUp.FlatStyle = FlatStyle.Flat;
        }

        private void buttonTherapyDown_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдвинуть выделенную строку на шаг вниз", buttonTherapyDown, 15, -20);
            buttonTherapyDown.FlatStyle = FlatStyle.Popup;
        }

        private void buttonTherapyDown_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonTherapyDown);
            buttonTherapyDown.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
