using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class DischargeEpicrisisForm : Form
    {
        private readonly PatientClass _patientInfo;
        private bool _isFormClosingByButton;
        private bool _stopSaveParameters;
        private readonly DbEngine _dbEngine;

        public DischargeEpicrisisForm(PatientClass patientInfo, DbEngine dbEngine)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _dbEngine = dbEngine;
            _patientInfo = patientInfo;
        }

        private void DischargeEpicrisisForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.DischargeEpicrisisFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.DischargeEpicrisisFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.DischargeEpicrisisFormLocation;
            }

            comboBoxRentgen.SelectedIndex = comboBoxShowing.SelectedIndex =
            comboBoxGipsDay.SelectedIndex = 2;
            dateTimePickerReasonStartDate.Value = _patientInfo.DeliveryDate;
            if (_patientInfo.ReleaseDate.HasValue)
            {
                dateTimePickerReasonEndDate.Value = _patientInfo.ReleaseDate.Value;
            }

            textBoxConservativeTherapy.Text = _patientInfo.DischargeEpicrisConservativeTherapy;
            textBoxAfterOperation.Text = _patientInfo.DischargeEpicrisAfterOperation;
            textBoxEkg.Text = _patientInfo.DischargeEpicrisEkg;
            textBoxOakErotrocits.Text = _patientInfo.DischargeEpicrisOakEritrocits;
            textBoxOakHb.Text = _patientInfo.DischargeEpicrisOakHb;
            textBoxOakLekocits.Text = _patientInfo.DischargeEpicrisOakLekocits;
            textBoxOakSoe.Text = _patientInfo.DischargeEpicrisOakSoe;
            textBoxOamDensity.Text = _patientInfo.DischargeEpicrisOamDensity;
            comboBoxOamColor.Text = _patientInfo.DischargeEpicrisOamColor;
            textBoxOamErotrocits.Text = _patientInfo.DischargeEpicrisOamEritrocits;
            textBoxOamLekocits.Text = _patientInfo.DischargeEpicrisOamLekocits;
            textBoxBakBillirubin.Text = _patientInfo.DischargeEpicrisBakBillirubin;
            textBoxBakGeneralProtein.Text = _patientInfo.DischargeEpicrisBakGeneralProtein;
            textBoxBakPTI.Text = _patientInfo.DischargeEpicrisBakPTI;
            textBoxBakSugar.Text = _patientInfo.DischargeEpicrisBakSugar;

            textBoxAdditionalAnalises.Text = _patientInfo.DischargeEpicrisAdditionalAnalises;

            if (_patientInfo.DischargeEpicrisRecomendations.Count == 1 &&
                _patientInfo.DischargeEpicrisRecomendations[0] == "notdefined")
            {
                _patientInfo.DischargeEpicrisRecomendations.Clear();
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = true;
            }
            else
            {
                foreach (string str in _patientInfo.DischargeEpicrisRecomendations)
                {
                    string[] words = str.Split(' ');
                    switch (str.Substring(0, 8))
                    {
                        case "больничн":
                        case "ученичес":
                            checkBox1.Checked = true;
                            comboBoxReason.Text = words[0] + " " + words[1];
                            dateTimePickerReasonStartDate.Value = DateTime.Parse(words[5]);
                            dateTimePickerReasonEndDate.Value = DateTime.Parse(words[7].TrimEnd('.'));
                            break;
                        case "явка к х":
                            checkBox2.Checked = true;
                            dateTimePickerSurgeryDate.Value = DateTime.Parse(words[5].TrimEnd('.'));
                            break;
                        case "наблюден":
                            checkBox3.Checked = true;
                            textBoxSpetialists.Text = str.Substring(words[0].Length + words[1].Length + 2);
                            break;
                        case "рентген-":
                            checkBox4.Checked = true;
                            numericUpDownRentgen.Value = Convert.ToInt32(words[2]);
                            comboBoxRentgen.Text = words[3];
                            break;
                        case "явка в о":
                            checkBox5.Checked = true;
                            numericUpDownShowing.Value = Convert.ToInt32(words[6]);
                            comboBoxShowing.Text = words[7];
                            break;
                        case "гипс до ":
                        case "брейс до":
                            checkBox6.Checked = true;
                            comboBoxGips.Text = words[0];
                            numericUpDownGipsLength.Value = Convert.ToInt32(words[2]);
                            comboBoxGipsDay.Text = words[3];
                            break;
                        case "косыночн":
                            checkBox6.Checked = true;
                            comboBoxGips.Text = "косынка";
                            numericUpDownGipsLength.Value = Convert.ToInt32(words[3]);
                            comboBoxShowing.Text = words[4];
                            break;
                    }
                }
            }

            if (_patientInfo.DischargeEpicrisAdditionalRecomendations.Count == 1 &&
                _patientInfo.DischargeEpicrisAdditionalRecomendations[0] == "notdefined")
            {
                _patientInfo.DischargeEpicrisAdditionalRecomendations.Clear();

                if (_dbEngine.GlobalSettings.IsDepartmentNameStartWithNumber("7"))
                {
                    _patientInfo.DischargeEpicrisAdditionalRecomendations.Add("«Ходьба с костылями без нагрузки на ногу»");
                    _patientInfo.DischargeEpicrisAdditionalRecomendations.Add("«Дозированная нагрузка»");
                }
            }

            textBoxAdditionalRecomendations.Text = ListToString(_patientInfo.DischargeEpicrisAdditionalRecomendations);
            _stopSaveParameters = false;
        }

        /// <summary>
        /// Конвертируем список строк в одну строк, разделяем переводом строк
        /// </summary>
        /// <param name="list">Список для разделения</param>
        /// <returns></returns>
        private static string ListToString(IEnumerable<string> list)
        {
            var listStr = new StringBuilder();
            foreach (string str in list)
            {
                listStr.Append(str + "\r\n");
            }

            if (listStr.Length > 2)
            {
                listStr.Remove(listStr.Length - 2, 2);
            }

            return listStr.ToString();
        }

        /// <summary>
        /// Строку, разделённую переводами строк, конвертируем в список строк
        /// </summary>
        /// <param name="allStr">Строка с переводами строк</param>
        /// <returns></returns>
        private static List<string> StringToList(string allStr)
        {
            string[] strArray = allStr.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(strArray);
        }

        private void DischargeEpicrisisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }

        private bool IsFormHasEmptyNeededFields()
        {
            if (string.IsNullOrEmpty(textBoxConservativeTherapy.Text) ||
                string.IsNullOrEmpty(textBoxAfterOperation.Text) ||
                string.IsNullOrEmpty(textBoxEkg.Text) ||
                string.IsNullOrEmpty(textBoxOakErotrocits.Text) ||
                string.IsNullOrEmpty(textBoxOakLekocits.Text) ||
                string.IsNullOrEmpty(textBoxOakHb.Text) ||
                string.IsNullOrEmpty(textBoxOakSoe.Text) ||
                string.IsNullOrEmpty(textBoxOamErotrocits.Text) ||
                string.IsNullOrEmpty(textBoxOamLekocits.Text) ||
                string.IsNullOrEmpty(comboBoxOamColor.Text) ||
                string.IsNullOrEmpty(textBoxOamDensity.Text))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Сохранить информацию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsFormHasEmptyNeededFields())
                {
                    MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                PutDataToPatient(_patientInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрыть форму без сохранения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Положить введённые данные в пациента
        /// </summary>
        /// <param name="patientInfo"></param>
        private void PutDataToPatient(PatientClass patientInfo)
        {
            patientInfo.DischargeEpicrisConservativeTherapy = textBoxConservativeTherapy.Text.Replace("\r\n", " ");
            patientInfo.DischargeEpicrisAfterOperation = textBoxAfterOperation.Text;
            patientInfo.DischargeEpicrisEkg = textBoxEkg.Text;
            patientInfo.DischargeEpicrisOakEritrocits = textBoxOakErotrocits.Text;
            patientInfo.DischargeEpicrisOakHb = textBoxOakHb.Text;
            patientInfo.DischargeEpicrisOakLekocits = textBoxOakLekocits.Text;
            patientInfo.DischargeEpicrisOakSoe = textBoxOakSoe.Text;
            patientInfo.DischargeEpicrisOamDensity = textBoxOamDensity.Text;
            patientInfo.DischargeEpicrisOamColor = comboBoxOamColor.Text;
            patientInfo.DischargeEpicrisOamEritrocits = textBoxOamErotrocits.Text;
            patientInfo.DischargeEpicrisOamLekocits = textBoxOamLekocits.Text;
            patientInfo.DischargeEpicrisBakBillirubin = textBoxBakBillirubin.Text;
            patientInfo.DischargeEpicrisBakGeneralProtein = textBoxBakGeneralProtein.Text;
            patientInfo.DischargeEpicrisBakPTI = textBoxBakPTI.Text;
            patientInfo.DischargeEpicrisBakSugar = textBoxBakSugar.Text;
            patientInfo.DischargeEpicrisAdditionalAnalises = textBoxAdditionalAnalises.Text;
            patientInfo.DischargeEpicrisRecomendations = StringToList(textBoxRecomendations.Text);
            patientInfo.DischargeEpicrisAdditionalRecomendations = StringToList(textBoxAdditionalRecomendations.Text);
        }

        /// <summary>
        /// Сгенерировать отчёт в Worde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dischargeEpicrisisHeaderFilePath = Path.Combine(Application.StartupPath, "Headers\\" + _dbEngine.GlobalSettings.DischargeEpicrisisHeaderFileName);
            if (!File.Exists(dischargeEpicrisisHeaderFilePath))
            {
                MessageBox.Show("Файл '" + _dbEngine.GlobalSettings.DischargeEpicrisisHeaderFileName + "' с шапкой для выписного экпикриза не найден в папке Headers", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tempPatientInfo = new PatientClass(_patientInfo);

            PutDataToPatient(tempPatientInfo);

            WordExportEngine.ExportDischargeEpicrisisFor8Department(tempPatientInfo, _dbEngine.GlobalSettings, dischargeEpicrisisHeaderFilePath);
        }

        #region Включение/выключение полей
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            comboBoxReason.Enabled = dateTimePickerReasonStartDate.Enabled =
            dateTimePickerReasonEndDate.Enabled = checkBox1.Checked;
            timerCreateRecomendations.Enabled = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            dateTimePickerSurgeryDate.Enabled = checkBox2.Checked;
            timerCreateRecomendations.Enabled = true;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            textBoxSpetialists.Enabled = checkBox3.Checked;
            timerCreateRecomendations.Enabled = true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            comboBoxRentgen.Enabled = numericUpDownRentgen.Enabled = checkBox4.Checked;
            timerCreateRecomendations.Enabled = true;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            comboBoxShowing.Enabled = numericUpDownShowing.Enabled = checkBox5.Checked;
            timerCreateRecomendations.Enabled = true;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            comboBoxGips.Enabled = comboBoxGipsDay.Enabled =
            numericUpDownGipsLength.Enabled = checkBox6.Checked;
            timerCreateRecomendations.Enabled = true;
        }
        #endregion

        #region Сброс таймера при изменении значений в полях
        private void ReloadTimer()
        {
            timerCreateRecomendations.Enabled = false;
            timerCreateRecomendations.Enabled = true;
        }

        private void dateTimePickerReasonStartDate_ValueChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }

        private void textBoxSpetialists_TextChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }

        private void numericUpDownRentgen_ValueChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }

        private void comboBoxReason_TextChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }
        #endregion

        /// <summary>
        /// Создание рекомендаций после изменения настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCreateRecomendations_Tick(object sender, EventArgs e)
        {
            timerCreateRecomendations.Enabled = false;
            string str = string.Empty;

            if (checkBox1.Checked)
            {
                str += comboBoxReason.Text + " № _____________ c " +
                    dateTimePickerReasonStartDate.Value.ToString("dd.MM.yyyy") + " по " +
                    dateTimePickerReasonEndDate.Value.ToString("dd.MM.yyyy") + ".\r\n";
            }

            if (checkBox2.Checked)
            {
                str += "явка к хирургу в поликлинику " +
                    dateTimePickerSurgeryDate.Value.ToString("dd.MM.yyyy") + ".\r\n";
            }

            if (checkBox3.Checked)
            {
                str += "наблюдение специалистов: " +
                    textBoxSpetialists.Text + "\r\n";
            }

            if (checkBox4.Checked)
            {
                str += "рентген-контроль через " +
                    numericUpDownRentgen.Value + " " + comboBoxRentgen.Text + " после операции.\r\n";
            }

            if (checkBox5.Checked)
            {
                str += "явка в отделение для консультации через " +
                    numericUpDownShowing.Value + " " + comboBoxShowing.Text + "\r\n";
            }

            if (checkBox6.Checked)
            {
                string gipsValue = comboBoxGips.Text == "косынка"
                    ? "косыночная повязка"
                    : comboBoxGips.Text;

                str += gipsValue + " до " + numericUpDownGipsLength.Value + " " + comboBoxGipsDay.Text + " после операции.\r\n";
            }

            textBoxRecomendations.Text = str.TrimEnd(new[] { '\r', '\n' });
        }

        /// <summary>
        /// Кнопка добавления данных к полю с консервативным лечением
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddTherapy_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                if (textBoxConservativeTherapy.Text.Length > 0)
                {
                    str = ", ";
                }

                str += comboBoxDrug.Text + " ";

                if (!string.IsNullOrEmpty(comboBoxPerOneCnt.Text))
                {
                    str += comboBoxPerOneCnt.Text + " ";
                }

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
                        while (cnt > 100)
                        {
                            cnt -= 100;
                        }

                        int lastValue = cnt;
                        while (lastValue >= 10)
                        {
                            lastValue -= 10;
                        }

                        if (lastValue >= 2 && lastValue <= 4 && (cnt < 12 || cnt > 14))
                        {
                            text = " раза в день ";
                        }
                        else
                        {
                            text = " раз в день ";
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

                if (!string.IsNullOrEmpty(comboBoxDuration.Text))
                {
                    str += comboBoxDuration.Text + " дня";
                }

                textBoxConservativeTherapy.Text += str.TrimEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void numericUpDownRentgen_Enter(object sender, EventArgs e)
        {
            numericUpDownRentgen.Select(0, 10);
        }

        private void numericUpDownShowing_Enter(object sender, EventArgs e)
        {
            numericUpDownShowing.Select(0, 10);
        }

        private void numericUpDownGipsLength_Enter(object sender, EventArgs e)
        {
            numericUpDownGipsLength.Select(0, 10);
        }

        private void DischargeEpicrisisForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.DischargeEpicrisisFormLocation = Location;
        }

        /// <summary>
        /// Изменение значений в полях для дозировки, в зависимости от выбранного лекарства
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxDrug_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDrug.Text == "S. Ceftriaxoni")
            {
                comboBoxPerOneCnt.Text = "1,0";
                comboBoxPerDayCnt.Text = "1";
                comboBoxReceivingMethod.Text = "в/м";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. Ketoroli")
            {
                comboBoxPerOneCnt.Text = "1,0";
                comboBoxPerDayCnt.Text = "3";
                comboBoxReceivingMethod.Text = "в/м";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "ПСС")
            {
                comboBoxPerOneCnt.Text = string.Empty;
                comboBoxPerDayCnt.Text = string.Empty;
                comboBoxReceivingMethod.Text = string.Empty;
                comboBoxDuration.Text = string.Empty;
            }
            else if (comboBoxDrug.Text == "S. Analgini")
            {
                comboBoxPerOneCnt.Text = "50% - 2ml";
                comboBoxPerDayCnt.Text = "3";
                comboBoxReceivingMethod.Text = "в/м";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. Dimedroli")
            {
                comboBoxPerOneCnt.Text = "1% - 2ml";
                comboBoxPerDayCnt.Text = "3";
                comboBoxReceivingMethod.Text = "в/м";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. NaCl")
            {
                comboBoxPerOneCnt.Text = "0,9% - 400";
                comboBoxPerDayCnt.Text = "1";
                comboBoxReceivingMethod.Text = "в/в, кап.";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. Glucosae")
            {
                comboBoxPerOneCnt.Text = "5% - 400";
                comboBoxPerDayCnt.Text = "1";
                comboBoxReceivingMethod.Text = "в/в, кап.";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. Dexasoni")
            {
                comboBoxPerOneCnt.Text = "0,008";
                comboBoxPerDayCnt.Text = "1";
                comboBoxReceivingMethod.Text = "в/м";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. Reopolyglucini")
            {
                comboBoxPerOneCnt.Text = "400 ml";
                comboBoxPerDayCnt.Text = "1";
                comboBoxReceivingMethod.Text = "в/в, кап.";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "S. Trentali")
            {
                comboBoxPerOneCnt.Text = "5 ml";
                comboBoxPerDayCnt.Text = "1";
                comboBoxReceivingMethod.Text = "в/в, кап.";
                comboBoxDuration.Text = "3";
            }
            else if (comboBoxDrug.Text == "Магнитотерапия")
            {
                comboBoxPerOneCnt.Text = string.Empty;
                comboBoxPerDayCnt.Text = string.Empty;
                comboBoxReceivingMethod.Text = string.Empty;
                comboBoxDuration.Text = "7";
            }
            else if (comboBoxDrug.Text == "ЛФК")
            {
                comboBoxPerOneCnt.Text = string.Empty;
                comboBoxPerDayCnt.Text = string.Empty;
                comboBoxReceivingMethod.Text = string.Empty;
                comboBoxDuration.Text = string.Empty;
            }
        }

        #region Подсказки
        private void buttonDocuments_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сгенерировать отчёт в Word", buttonDocuments, 15, -20);
            buttonDocuments.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDocuments_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonDocuments);
            buttonDocuments.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сохранить изменения", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Закрыть форму без сохранения изменений", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        private void buttonAddTherapy_MouseEnter(object sender, EventArgs e)
        {
            buttonAddTherapy.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAddTherapy_MouseLeave(object sender, EventArgs e)
        {
            buttonAddTherapy.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
