using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class DischargeEpicrisisForm : Form
    {
        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalizationInfo;
        private readonly CDischargeEpicrisis _dischargeEpicrisisInfo;
        private readonly CMedicalInspection _medicalInspection;
        private readonly CGlobalSettings _globalSettings;
        private readonly COperationWorker _operationWorker;
        private readonly CDischargeEpicrisisWorker _dischargeEpicrisisWorker;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public DischargeEpicrisisForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            CMedicalInspection medicalInspection,
            CDischargeEpicrisis dischargeEpicrisisInfo)
        {
            InitializeComponent();

            _patientInfo = patientInfo;
            _hospitalizationInfo = hospitalizationInfo;
            _dischargeEpicrisisInfo = dischargeEpicrisisInfo;
            _medicalInspection = medicalInspection;
            _operationWorker = workersKeeper.OperationWorker;
            _dischargeEpicrisisWorker = workersKeeper.DischargeEpicrisisWorker;
            _globalSettings = workersKeeper.GlobalSettings;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }

        private void DischargeEpicrisisForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.DischargeEpicrisisFormLocation.X >= 0 &&
                _configurationEngine.DischargeEpicrisisFormLocation.Y >= 0)
            {
                Location = _configurationEngine.DischargeEpicrisisFormLocation;
            }

            comboBoxRentgen.SelectedIndex = comboBoxShowing.SelectedIndex =
            comboBoxGipsDay.SelectedIndex = 2;
            dateTimePickerReasonStartDate.Value = _hospitalizationInfo.DeliveryDate;
            if (_hospitalizationInfo.ReleaseDate.HasValue)
            {
                dateTimePickerReasonEndDate.Value = _hospitalizationInfo.ReleaseDate.Value;
            }

            textBoxConservativeTherapy.Text = _dischargeEpicrisisInfo.ConservativeTherapy;
            dateTimePickerAnalysisDate.Value = _dischargeEpicrisisInfo.AnalysisDate.HasValue ? _dischargeEpicrisisInfo.AnalysisDate.Value : DateTime.Now;
            textBoxAfterOperation.Text = _dischargeEpicrisisInfo.AfterOperation;
            textBoxEkg.Text = _dischargeEpicrisisInfo.Ekg;
            textBoxOakErotrocits.Text = _dischargeEpicrisisInfo.OakEritrocits;
            textBoxOakHb.Text = _dischargeEpicrisisInfo.OakHb;
            textBoxOakLekocits.Text = _dischargeEpicrisisInfo.OakLekocits;
            textBoxOakSoe.Text = _dischargeEpicrisisInfo.OakSoe;
            textBoxOamDensity.Text = _dischargeEpicrisisInfo.OamDensity;
            comboBoxOamColor.Text = _dischargeEpicrisisInfo.OamColor;
            textBoxOamErotrocits.Text = _dischargeEpicrisisInfo.OamEritrocits;
            textBoxOamLekocits.Text = _dischargeEpicrisisInfo.OamLekocits;
            textBoxAdditionalAnalises.Text = _dischargeEpicrisisInfo.AdditionalAnalises;

            if (_dischargeEpicrisisInfo.Recomendations.Count == 1 &&
                _dischargeEpicrisisInfo.Recomendations[0] == "notdefined")
            {
                _dischargeEpicrisisInfo.Recomendations.Clear();
                checkBox1.Checked = checkBox2.Checked = checkBox3.Checked = true;
            }
            else
            {
                foreach (string str in _dischargeEpicrisisInfo.Recomendations)
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

            textBoxAdditionalRecomendations.Text = StringListToString(_dischargeEpicrisisInfo.AdditionalRecomendations);
        }

        /// <summary>
        /// Конвертируем список строк в одну строк, разделяем переводом строк
        /// </summary>
        /// <param name="list">Список для разделения</param>
        /// <returns></returns>
        private static string StringListToString(IEnumerable<string> list)
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
        private static List<string> StringToStringList(string allStr)
        {
            string[] strArray = allStr.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            return new List<string>(strArray);
        }

        private void DischargeEpicrisisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                DialogResult dialogResult = MessageBox.ShowDialog("Вы хотите сохранить изменения?", "Закрытие окна", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    buttonOk_Click(sender, e);
                }
                else if (dialogResult == DialogResult.No)
                {
                    buttonClose_Click(sender, e);
                }
                else
                {
                    e.Cancel = true;
                }
            }

            _configurationEngine.DischargeEpicrisisFormLocation = Location;
        }

        #region Подсказки
        private void buttonDocuments_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сгенерировать отчёт в Word", buttonDocuments);
            buttonDocuments.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDocuments_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDocuments.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Сохранить изменения", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть форму без сохранения изменений", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
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
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PutDataToDischargeEpicrisis();

            try
            {
                _dischargeEpicrisisWorker.Update(_dischargeEpicrisisInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрыть форму без сохранения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_dischargeEpicrisisInfo.NotInDatabase)
                {
                    _dischargeEpicrisisWorker.Remove(_dischargeEpicrisisInfo.HospitalizationId);
                }

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Положить введённые данные в выписной эпикриз
        /// </summary>
        private void PutDataToDischargeEpicrisis()
        {
            _dischargeEpicrisisInfo.ConservativeTherapy = string.IsNullOrEmpty(textBoxConservativeTherapy.Text)
                ? string.Empty
                : textBoxConservativeTherapy.Text.Replace("\r\n", " ");
            _dischargeEpicrisisInfo.AnalysisDate = dateTimePickerAnalysisDate.Value;
            _dischargeEpicrisisInfo.AfterOperation = textBoxAfterOperation.Text;
            _dischargeEpicrisisInfo.Ekg = textBoxEkg.Text;
            _dischargeEpicrisisInfo.OakEritrocits = textBoxOakErotrocits.Text;
            _dischargeEpicrisisInfo.OakHb = textBoxOakHb.Text;
            _dischargeEpicrisisInfo.OakLekocits = textBoxOakLekocits.Text;
            _dischargeEpicrisisInfo.OakSoe = textBoxOakSoe.Text;
            _dischargeEpicrisisInfo.OamDensity = textBoxOamDensity.Text;
            _dischargeEpicrisisInfo.OamColor = comboBoxOamColor.Text;
            _dischargeEpicrisisInfo.OamEritrocits = textBoxOamErotrocits.Text;
            _dischargeEpicrisisInfo.OamLekocits = textBoxOamLekocits.Text;
            _dischargeEpicrisisInfo.AdditionalAnalises = textBoxAdditionalAnalises.Text;
            _dischargeEpicrisisInfo.Recomendations = StringToStringList(textBoxRecomendations.Text);
            _dischargeEpicrisisInfo.AdditionalRecomendations = StringToStringList(textBoxAdditionalRecomendations.Text);
        }

        /// <summary>
        /// Сгенерировать отчёт в Worde
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            if (IsFormHasEmptyNeededFields())
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string dischargeEpicrisisHeaderFilePath = Path.Combine(Application.StartupPath, "Headers\\" + _globalSettings.DischargeEpicrisisHeaderFileName);
            if (!File.Exists(dischargeEpicrisisHeaderFilePath))
            {
                MessageBox.Show("Файл '" + _globalSettings.DischargeEpicrisisHeaderFileName + "' с шапкой для выписного экпикриза не найден в папке Headers", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                PutDataToDischargeEpicrisis();

                CWordExportHelper.ExportDischargeEpicrisis(
                    _patientInfo,
                    _hospitalizationInfo,
                    _dischargeEpicrisisInfo,
                    _medicalInspection,
                    _operationWorker,
                    _globalSettings,
                    dischargeEpicrisisHeaderFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void comboBoxRecomendation_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }

        private void dateTimePickerReasonStartDate_ValueChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }

        private void textBoxRecomendation_TextChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }

        private void numericUpDownRentgen_ValueChanged(object sender, EventArgs e)
        {
            ReloadTimer();
        }
        #endregion

        /// <summary>
        /// Создание рекомендаций после изменения настроек
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
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
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAddTherapy_Click(object sender, EventArgs e)
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
            comboBoxDrug.Focus();
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


        /// <summary>
        /// Изменение значений в полях для дозировки, в зависимости от выбранного лекарства
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
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
    }
}
