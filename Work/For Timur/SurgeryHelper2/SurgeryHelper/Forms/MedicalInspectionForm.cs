using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class MedicalInspectionForm : Form
    {
        private readonly CWorkersKeeper _workersKeeper;
        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalizationInfo;
        private readonly CMedicalInspection _medicalInspectionInfo;
        private readonly CGlobalSettings _globalSettings;
        private readonly COperationWorker _operationWorker;
        private readonly CMedicalInspectionWorker _medicalInspectionWorker;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public MedicalInspectionForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            CMedicalInspection medicalInspectionInfo)
        {
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _patientInfo = patientInfo;
            _hospitalizationInfo = hospitalizationInfo;
            _medicalInspectionInfo = medicalInspectionInfo;
            _operationWorker = workersKeeper.OperationWorker;
            _medicalInspectionWorker = workersKeeper.MedicalInspectionWorker;
            _globalSettings = workersKeeper.GlobalSettings;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }

        private void MedicalInspectionForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.MedicalInspectionFormLocation.X >= 0 &&
               _configurationEngine.MedicalInspectionFormLocation.Y >= 0)
            {
                Location = _configurationEngine.MedicalInspectionFormLocation;
            }

            checkBoxIsPlanEnabled.Checked = _medicalInspectionInfo.IsPlanEnabled;
            comboBoxInspectionPlan.Text = _medicalInspectionInfo.InspectionPlan;
            textBoxComplaints.Text = _medicalInspectionInfo.Complaints;
            comboBoxTeoRisk.Text = _medicalInspectionInfo.TeoRisk;

            if (_medicalInspectionInfo.ExpertAnamnese == 1)
            {
                radioButtonLnWithNumber.Checked = true;
            }
            else if (_medicalInspectionInfo.ExpertAnamnese == 2)
            {
                radioButtonNewLn.Checked = true;
            }
            else
            {
                radioButtonNoLn.Checked = true;
            }

            dateTimePickerLnWithNumberStart.Value = _medicalInspectionInfo.LnWithNumberDateStart;
            dateTimePickerLnWithNumberEnd.Value = _medicalInspectionInfo.LnWithNumberDateEnd;
            dateTimePickerLnFirstStart.Value = _medicalInspectionInfo.LnFirstDateStart;
            textBoxStLocalisDescription.Text = _medicalInspectionInfo.StLocalisDescription;
            comboBoxRentgen.Text = _medicalInspectionInfo.StLocalisRentgen;

            checkBoxIsAnamnezEnabled.Checked = _medicalInspectionInfo.IsAnamneseActive;
            textBoxAnMorbi.Text = _medicalInspectionInfo.AnamneseAnMorbi;
            SetCheckBoxes(groupBoxAnVitae.Controls, _medicalInspectionInfo.AnamneseAnVitae, 13);
            SetTextBoxes(tabPageAnamnes.Controls, _medicalInspectionInfo.AnamneseTextBoxes, 1);
            SetCheckBoxes(tabPageAnamnes.Controls, _medicalInspectionInfo.AnamneseCheckboxes, 1);

            SetComboBoxes(tabPageStPraesens.Controls, _medicalInspectionInfo.StPraesensComboBoxes, 1);
            SetTextBoxes(tabPageStPraesens.Controls, _medicalInspectionInfo.StPraesensTextBoxes, 9);
            SetNumericUpDowns(tabPageStPraesens.Controls, _medicalInspectionInfo.StPraesensNumericUpDowns, 1);

            checkBoxIsUpperExtremityJoint.Checked = _medicalInspectionInfo.IsStLocalisPart1Enabled;
            comboBoxOppositionFinger.Text = _medicalInspectionInfo.StLocalisPart1OppositionFinger;
            SetTextBoxes(tabPageStLocalis1.Controls, _medicalInspectionInfo.StLocalisPart1Fields, 26);

            checkBoxIsHandDamage.Checked = _medicalInspectionInfo.IsStLocalisPart2Enabled;
            comboBoxWhichHand.Text = _medicalInspectionInfo.StLocalisPart2WhichHand;
            SetComboBoxes(tabPageStLocalis2.Controls, _medicalInspectionInfo.StLocalisPart2ComboBoxes, 7);
            SetTextBoxes(tabPageStLocalis2.Controls, _medicalInspectionInfo.StLocalisPart2TextBoxes, 100);
            numericUpDown5.Value = _medicalInspectionInfo.StLocalisPart2NumericUpDown;
            SetComboBoxes(groupBoxLeftHand.Controls, _medicalInspectionInfo.StLocalisPart2LeftHand, 100);
            SetComboBoxes(groupBoxRightHand.Controls, _medicalInspectionInfo.StLocalisPart2RightHand, 200);
        }

        private static void SetComboBoxes(Control.ControlCollection controls, string[] values, int startNumber)
        {
            for (int i = startNumber; i < startNumber + values.Length; i++)
            {
                controls["comboBox" + i].Text = values[i - startNumber];
            }
        }

        private static void SetNumericUpDowns(Control.ControlCollection controls, int[] values, int startNumber)
        {
            for (int i = startNumber; i < startNumber + values.Length; i++)
            {
                ((NumericUpDown)controls["numericUpDown" + i]).Value = values[i - startNumber];
            }
        }

        private static void SetCheckBoxes(Control.ControlCollection controls, bool[] values, int startNumber)
        {
            for (int i = startNumber; i < startNumber + values.Length; i++)
            {
                ((CheckBox)controls["checkBox" + i]).Checked = values[i - startNumber];
            }
        }

        private static void SetTextBoxes(Control.ControlCollection controls, string[] values, int startNumber)
        {
            for (int i = startNumber; i < startNumber + values.Length; i++)
            {
                controls["textBox" + i].Text = values[i - startNumber];
            }
        }

        private static string[] GetComboBoxes(Control.ControlCollection controls, int startNumber, int length)
        {
            var values = new string[length];
            for (int i = startNumber; i < startNumber + length; i++)
            {
                values[i - startNumber] = controls["comboBox" + i].Text;
            }

            return values;
        }

        private static int[] GetNumericUpDowns(Control.ControlCollection controls, int startNumber, int length)
        {
            var values = new int[length];
            for (int i = startNumber; i < startNumber + length; i++)
            {
                values[i - startNumber] = (int)((NumericUpDown)controls["numericUpDown" + i]).Value;
            }

            return values;
        }

        private static bool[] GetCheckBoxes(Control.ControlCollection controls, int startNumber, int length)
        {
            var values = new bool[length];
            for (int i = startNumber; i < startNumber + length; i++)
            {
                values[i - startNumber] = ((CheckBox)controls["checkBox" + i]).Checked;
            }

            return values;
        }

        private static string[] GetTextBoxes(Control.ControlCollection controls, int startNumber, int length)
        {
            var values = new string[length];
            for (int i = startNumber; i < startNumber + length; i++)
            {
                values[i - startNumber] = controls["textBox" + i].Text;
            }

            return values;
        }

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

        private void linkLabelCopyAnMorbi_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Скопировать данные из анамнеза для пациента\r\n(текущее значение будет заменено)", linkLabelCopyAnMorbi, 15, -42);
        }

        private void linkLabelCopyAnMorbi_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }

        private void linkLabelStLocalis_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Скопировать данные из поля 'Объективно' из последней консультации пациента\r\n(текущее значение будет заменено)", linkLabelStLocalis, 15, -42);
        }

        private void linkLabelStLocalis_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
        }

        /// <summary>
        /// Сгенерировать отчёт в Worde
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToMedicalInspection();

                CWordExportHelper.ExportMedicalInspection(
                    _patientInfo,
                    _hospitalizationInfo,
                    _medicalInspectionInfo,
                    _operationWorker,
                    _globalSettings);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Положить введённые данные в осмотр в отделении
        /// </summary>        
        private void PutDataToMedicalInspection()
        {
            _medicalInspectionInfo.IsPlanEnabled = checkBoxIsPlanEnabled.Checked;
            _medicalInspectionInfo.InspectionPlan = comboBoxInspectionPlan.Text;
            _medicalInspectionInfo.Complaints = textBoxComplaints.Text;
            _medicalInspectionInfo.TeoRisk = comboBoxTeoRisk.Text;

            if (radioButtonLnWithNumber.Checked)
            {
                _medicalInspectionInfo.ExpertAnamnese = 1;
            }
            else if (radioButtonNewLn.Checked)
            {
                _medicalInspectionInfo.ExpertAnamnese = 2;
            }
            else
            {
                _medicalInspectionInfo.ExpertAnamnese = 3;
            }

            _medicalInspectionInfo.LnWithNumberDateStart = dateTimePickerLnWithNumberStart.Value;
            _medicalInspectionInfo.LnWithNumberDateEnd = dateTimePickerLnWithNumberEnd.Value;
            _medicalInspectionInfo.LnFirstDateStart = dateTimePickerLnFirstStart.Value;
            _medicalInspectionInfo.StLocalisDescription = textBoxStLocalisDescription.Text;
            _medicalInspectionInfo.StLocalisRentgen = comboBoxRentgen.Text;

            _medicalInspectionInfo.IsAnamneseActive = checkBoxIsAnamnezEnabled.Checked;
            _medicalInspectionInfo.AnamneseAnMorbi = textBoxAnMorbi.Text;
            _medicalInspectionInfo.AnamneseAnVitae = GetCheckBoxes(groupBoxAnVitae.Controls, 13, 4);
            _medicalInspectionInfo.AnamneseTextBoxes = GetTextBoxes(tabPageAnamnes.Controls, 1, 8);
            _medicalInspectionInfo.AnamneseCheckboxes = GetCheckBoxes(tabPageAnamnes.Controls, 1, 12);

            _medicalInspectionInfo.StPraesensComboBoxes = GetComboBoxes(tabPageStPraesens.Controls, 1, 4);
            _medicalInspectionInfo.StPraesensTextBoxes = GetTextBoxes(tabPageStPraesens.Controls, 9, 17);
            _medicalInspectionInfo.StPraesensNumericUpDowns = GetNumericUpDowns(tabPageStPraesens.Controls, 1, 4);

            _medicalInspectionInfo.IsStLocalisPart1Enabled = checkBoxIsUpperExtremityJoint.Checked;
            _medicalInspectionInfo.StLocalisPart1OppositionFinger = comboBoxOppositionFinger.Text;
            _medicalInspectionInfo.StLocalisPart1Fields = GetTextBoxes(tabPageStLocalis1.Controls, 26, 62);

            _medicalInspectionInfo.IsStLocalisPart2Enabled = checkBoxIsHandDamage.Checked;
            _medicalInspectionInfo.StLocalisPart2WhichHand = comboBoxWhichHand.Text;
            _medicalInspectionInfo.StLocalisPart2ComboBoxes = GetComboBoxes(tabPageStLocalis2.Controls, 7, 10);
            _medicalInspectionInfo.StLocalisPart2TextBoxes = GetTextBoxes(tabPageStLocalis2.Controls, 100, 11);
            _medicalInspectionInfo.StLocalisPart2NumericUpDown = (int)numericUpDown5.Value;
            _medicalInspectionInfo.StLocalisPart2LeftHand = GetComboBoxes(groupBoxLeftHand.Controls, 100, 24);
            _medicalInspectionInfo.StLocalisPart2RightHand = GetComboBoxes(groupBoxRightHand.Controls, 200, 24);
        }

        /// <summary>
        /// Сохранить информацию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToMedicalInspection();

                _medicalInspectionWorker.Update(_medicalInspectionInfo);

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
                if (_medicalInspectionInfo.NotInDatabase)
                {
                    _medicalInspectionWorker.Remove(_medicalInspectionInfo.HospitalizationId);
                }

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MedicalInspectionForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.MedicalInspectionFormLocation = Location;
        }

        private void checkBoxIsAnamnezEnabled_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var component in tabPageAnamnes.Controls)
            {
                if (component is TextBox)
                {
                    ((TextBox)component).Enabled = checkBoxIsAnamnezEnabled.Checked;
                }
                else if (component is LinkLabel)
                {
                    ((LinkLabel)component).Enabled = checkBoxIsAnamnezEnabled.Checked;
                }
                else if (component is CheckBox)
                {
                    if (((CheckBox)component).Name != "checkBoxIsAnamnezEnabled")
                    {
                        ((CheckBox)component).Enabled = checkBoxIsAnamnezEnabled.Checked;
                    }
                }
            }

            foreach (var component in groupBoxAnVitae.Controls)
            {
                if (component is CheckBox)
                {
                    ((CheckBox)component).Enabled = checkBoxIsAnamnezEnabled.Checked;
                }
            }
        }

        private void checkBoxIsPlanEnabled_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxInspectionPlan.Enabled = checkBoxIsPlanEnabled.Checked;
        }

        private void radioButtonNewLn_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerLnFirstStart.Enabled = radioButtonNewLn.Checked;
        }

        private void radioButtonLnWithNumber_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerLnWithNumberStart.Enabled = dateTimePickerLnWithNumberEnd.Enabled = radioButtonLnWithNumber.Checked;
        }

        /// <summary>
        /// Включение/выключение полей для детальных повреждений левой и правой рук 
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void comboBoxWhichHand_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var component in groupBoxLeftHand.Controls)
            {
                if (component is ComboBox)
                {
                    if (comboBoxWhichHand.Text == "правая, левая" || comboBoxWhichHand.Text == "левая")
                    {
                        ((ComboBox)component).Enabled = comboBoxWhichHand.Enabled;
                    }
                    else
                    {
                        ((ComboBox)component).Enabled = false;
                    }
                }
            }

            foreach (var component in groupBoxRightHand.Controls)
            {
                if (component is ComboBox)
                {
                    if (comboBoxWhichHand.Text == "правая, левая" || comboBoxWhichHand.Text == "правая")
                    {
                        ((ComboBox)component).Enabled = comboBoxWhichHand.Enabled;
                    }
                    else
                    {
                        ((ComboBox)component).Enabled = false;
                    }
                }
            }
        }

        /// <summary>
        /// Включение/выключение полей для детальных повреждений левой и правой рук
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void comboBoxWhichHand_EnabledChanged(object sender, EventArgs e)
        {
            comboBoxWhichHand_SelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// Включение/выключение всех компонентов на вкладке со второй частью локалиса
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxIsHandDamage_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var component in tabPageStLocalis2.Controls)
            {
                if (component is ComboBox)
                {
                    ((ComboBox)component).Enabled = checkBoxIsHandDamage.Checked;
                }
                else if (component is TextBox)
                {
                    ((TextBox)component).Enabled = checkBoxIsHandDamage.Checked;
                }
                else if (component is NumericUpDown)
                {
                    ((NumericUpDown)component).Enabled = checkBoxIsHandDamage.Checked;
                }
            }

            comboBox8_SelectedIndexChanged(null, null);
        }

        /// <summary>
        /// Включение/выключение всех компонентов на вкладке с первой частью локалиса
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxUpperExtremityJoint_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var component in tabPageStLocalis1.Controls)
            {
                if (component is ComboBox)
                {
                    ((ComboBox)component).Enabled = checkBoxIsUpperExtremityJoint.Checked;
                }
                else if (component is TextBox)
                {
                    ((TextBox)component).Enabled = checkBoxIsUpperExtremityJoint.Checked;
                }
            }
        }

        /// <summary>
        /// Показ данные по пальцам, если есть нарушение оси
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox8.Text == "нет")
            {
                comboBox9.Enabled = comboBox10.Enabled = false;
            }
            else
            {
                comboBox9.Enabled = comboBox10.Enabled = checkBoxIsHandDamage.Checked;
            }
        }

        private void numericUpDown1_Enter(object sender, EventArgs e)
        {
            numericUpDown1.Select(0, 10);
        }

        private void numericUpDown2_Enter(object sender, EventArgs e)
        {
            numericUpDown2.Select(0, 10);
        }

        private void numericUpDown3_Enter(object sender, EventArgs e)
        {
            numericUpDown3.Select(0, 10);
        }

        private void numericUpDown4_Enter(object sender, EventArgs e)
        {
            numericUpDown4.Select(0, 10);
        }

        private void numericUpDown5_Enter(object sender, EventArgs e)
        {
            numericUpDown5.Select(0, 10);
        }


        /// <summary>
        /// Скопировать поле an.morbi из анамнеза для пациента
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void linkLabelCopyAnMorbi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CAnamnese anamneseInfo = _workersKeeper.AnamneseWorker.GetByPatientId(_patientInfo.Id);
            if (string.IsNullOrEmpty(anamneseInfo.AnMorbi))
            {
                MessageBox.Show("Поле An.Morbi в анамензе пациента не задано", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            textBoxAnMorbi.Text = anamneseInfo.AnMorbi;
        }

        /// <summary>
        /// Скопировать поле объективно из последней консультации, если она есть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabelStLocalis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CVisit[] visits = _workersKeeper.VisitWorker.GetListByPatientId(_patientInfo.Id);

            if (visits.Length == 0)
            {
                MessageBox.Show("У пациента нет консультаций", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            CVisit visit = visits[visits.Length - 1];
            if (string.IsNullOrEmpty(visit.Evenly))
            {
                MessageBox.Show("Поле 'Объективно' в последней консультации не задано", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            textBoxStLocalisDescription.Text = visit.Evenly;
        }
    }
}
