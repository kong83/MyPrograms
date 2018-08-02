using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class MedicalInspectionForm : Form
    {
        private readonly PatientClass _patientInfo;
        private bool _isFormClosingByButton;
        private bool _stopSaveParameters;
        private readonly DbEngine _dbEngine;

        public MedicalInspectionForm(PatientClass patientInfo, DbEngine dbEngine)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _dbEngine = dbEngine;
            _patientInfo = patientInfo;
        }
        
        private void MedicalInspectionForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.MedicalInspectionFormLocation.X >= 0 &&
               _dbEngine.ConfigEngine.MedicalInspectionFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.MedicalInspectionFormLocation;
            }

            checkBoxIsPlanEnabled.Checked = _patientInfo.MedicalInspectionIsPlanEnabled;
            comboBoxInspectionPlan.Text = _patientInfo.MedicalInspectionInspectionPlan;
            textBoxComplaints.Text = _patientInfo.MedicalInspectionComplaints;
            comboBoxTeoRisk.Text = _patientInfo.MedicalInspectionTeoRisk;

            if (_patientInfo.MedicalInspectionExpertAnamnese == 1)
            { 
                radioButtonLnWithNumber.Checked = true;
            }
            else if (_patientInfo.MedicalInspectionExpertAnamnese == 2)
            {
                radioButtonNewLn.Checked = true;
            }
            else 
            {
                radioButtonNoLn.Checked = true;
            }

            dateTimePickerLnWithNumberStart.Value = _patientInfo.MedicalInspectionLnWithNumberDateStart;
            dateTimePickerLnWithNumberEnd.Value = _patientInfo.MedicalInspectionLnWithNumberDateEnd;
            dateTimePickerLnFirstStart.Value = _patientInfo.MedicalInspectionLnFirstDateStart;
            textBoxStLocalisDescription.Text = _patientInfo.MedicalInspectionStLocalisDescription;
            comboBoxRentgen.Text = _patientInfo.MedicalInspectionStLocalisRentgen;

            checkBoxIsAnamnezEnabled.Checked = _patientInfo.MedicalInspectionIsAnamneseActive;
            dateTimePickerDateTrauma.Checked = _patientInfo.MedicalInspectionAnamneseTraumaDate.HasValue;
            if (_patientInfo.MedicalInspectionAnamneseTraumaDate.HasValue)
            {
                dateTimePickerDateTrauma.Value = _patientInfo.MedicalInspectionAnamneseTraumaDate.Value;
            }

            textBoxAnMorbi.Text = _patientInfo.MedicalInspectionAnamneseAnMorbi;
            SetCheckBoxes(groupBoxAnVitae.Controls, _patientInfo.MedicalInspectionAnamneseAnVitae, 13);
            SetTextBoxes(tabPageAnamnes.Controls, _patientInfo.MedicalInspectionAnamneseTextBoxes, 1);
            SetCheckBoxes(tabPageAnamnes.Controls, _patientInfo.MedicalInspectionAnamneseCheckboxes, 1);

            SetComboBoxes(tabPageStPraesens.Controls, _patientInfo.MedicalInspectionStPraesensComboBoxes, 1);
            SetTextBoxes(tabPageStPraesens.Controls, _patientInfo.MedicalInspectionStPraesensTextBoxes, 9);
            SetNumericUpDowns(tabPageStPraesens.Controls, _patientInfo.MedicalInspectionStPraesensNumericUpDowns, 1);
            textBoxStPraesensOther.Text = _patientInfo.MedicalInspectionStPraesensOthers;

            checkBoxIsUpperExtremityJoint.Checked = _patientInfo.MedicalInspectionIsStLocalisPart1Enabled;
            comboBoxOppositionFinger.Text = _patientInfo.MedicalInspectionStLocalisPart1OppositionFinger;
            SetTextBoxes(tabPageStLocalis1.Controls, _patientInfo.MedicalInspectionStLocalisPart1Fields, 26);

            checkBoxIsHandDamage.Checked = _patientInfo.MedicalInspectionIsStLocalisPart2Enabled;
            comboBoxWhichHand.Text = _patientInfo.MedicalInspectionStLocalisPart2WhichHand;
            SetComboBoxes(tabPageStLocalis2.Controls, _patientInfo.MedicalInspectionStLocalisPart2ComboBoxes, 7);
            SetTextBoxes(tabPageStLocalis2.Controls, _patientInfo.MedicalInspectionStLocalisPart2TextBoxes, 100);
            numericUpDown5.Value = _patientInfo.MedicalInspectionStLocalisPart2NumericUpDown;
            SetComboBoxes(groupBoxLeftHand.Controls, _patientInfo.MedicalInspectionStLocalisPart2LeftHand, 100);
            SetComboBoxes(groupBoxRightHand.Controls, _patientInfo.MedicalInspectionStLocalisPart2RightHand, 200);

            _stopSaveParameters = false;
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

        /// <summary>
        /// Сгенерировать отчёт в Worde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            var tempPatientInfo = new PatientClass(_patientInfo);

            PutDataToPatient(tempPatientInfo);

            WordExportEngine.ExportMedicalInspection(tempPatientInfo, _dbEngine.GlobalSettings);
        }

        /// <summary>
        /// Положить введённые данные в пациента
        /// </summary>
        /// <param name="patientInfo"></param>
        private void PutDataToPatient(PatientClass patientInfo)
        {
            patientInfo.MedicalInspectionIsPlanEnabled = checkBoxIsPlanEnabled.Checked;
            patientInfo.MedicalInspectionInspectionPlan = comboBoxInspectionPlan.Text;
            patientInfo.MedicalInspectionComplaints = textBoxComplaints.Text;
            patientInfo.MedicalInspectionTeoRisk = comboBoxTeoRisk.Text;

            if (radioButtonLnWithNumber.Checked)
            {
                patientInfo.MedicalInspectionExpertAnamnese = 1;
            }
            else if (radioButtonNewLn.Checked)
            {
                patientInfo.MedicalInspectionExpertAnamnese = 2;
            }
            else
            {
                patientInfo.MedicalInspectionExpertAnamnese = 3;
            }

            patientInfo.MedicalInspectionLnWithNumberDateStart = dateTimePickerLnWithNumberStart.Value;
            patientInfo.MedicalInspectionLnWithNumberDateEnd = dateTimePickerLnWithNumberEnd.Value;
            patientInfo.MedicalInspectionLnFirstDateStart = dateTimePickerLnFirstStart.Value;
            patientInfo.MedicalInspectionStLocalisDescription = textBoxStLocalisDescription.Text;
            patientInfo.MedicalInspectionStLocalisRentgen = comboBoxRentgen.Text;

            patientInfo.MedicalInspectionIsAnamneseActive = checkBoxIsAnamnezEnabled.Checked;
            if (dateTimePickerDateTrauma.Checked)
            {
                patientInfo.MedicalInspectionAnamneseTraumaDate = dateTimePickerDateTrauma.Value;
            }
            else
            {
                patientInfo.MedicalInspectionAnamneseTraumaDate = null;
            }
            
            patientInfo.MedicalInspectionAnamneseAnMorbi = textBoxAnMorbi.Text;
            patientInfo.MedicalInspectionAnamneseAnVitae = GetCheckBoxes(groupBoxAnVitae.Controls, 13, 4);
            patientInfo.MedicalInspectionAnamneseTextBoxes = GetTextBoxes(tabPageAnamnes.Controls, 1, 8);
            patientInfo.MedicalInspectionAnamneseCheckboxes = GetCheckBoxes(tabPageAnamnes.Controls, 1, 12);

            patientInfo.MedicalInspectionStPraesensComboBoxes = GetComboBoxes(tabPageStPraesens.Controls, 1, 4);
            patientInfo.MedicalInspectionStPraesensTextBoxes = GetTextBoxes(tabPageStPraesens.Controls, 9, 17);
            patientInfo.MedicalInspectionStPraesensNumericUpDowns = GetNumericUpDowns(tabPageStPraesens.Controls, 1, 4);
            patientInfo.MedicalInspectionStPraesensOthers = textBoxStPraesensOther.Text;

            patientInfo.MedicalInspectionIsStLocalisPart1Enabled = checkBoxIsUpperExtremityJoint.Checked;
            patientInfo.MedicalInspectionStLocalisPart1OppositionFinger = comboBoxOppositionFinger.Text;
            patientInfo.MedicalInspectionStLocalisPart1Fields = GetTextBoxes(tabPageStLocalis1.Controls, 26, 62);

            patientInfo.MedicalInspectionIsStLocalisPart2Enabled = checkBoxIsHandDamage.Checked;
            patientInfo.MedicalInspectionStLocalisPart2WhichHand = comboBoxWhichHand.Text;
            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes = GetComboBoxes(tabPageStLocalis2.Controls, 7, 10);
            patientInfo.MedicalInspectionStLocalisPart2TextBoxes = GetTextBoxes(tabPageStLocalis2.Controls, 100, 11);
            patientInfo.MedicalInspectionStLocalisPart2NumericUpDown = (int)numericUpDown5.Value;
            patientInfo.MedicalInspectionStLocalisPart2LeftHand = GetComboBoxes(groupBoxLeftHand.Controls, 100, 24);
            patientInfo.MedicalInspectionStLocalisPart2RightHand = GetComboBoxes(groupBoxRightHand.Controls, 200, 24);            
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

        private void MedicalInspectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }

        private void checkBoxIsAnamnezEnabled_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var component in tabPageAnamnes.Controls)
            {
                if (component is TextBox)
                {
                    ((TextBox)component).Enabled = checkBoxIsAnamnezEnabled.Checked;
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

            dateTimePickerDateTrauma.Enabled = checkBoxIsAnamnezEnabled.Checked;
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxWhichHand_EnabledChanged(object sender, EventArgs e)
        {
            comboBoxWhichHand_SelectedIndexChanged(sender, e);
        } 

        /// <summary>
        /// Включение/выключение всех компонентов на вкладке со второй частью локалиса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void MedicalInspectionForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.MedicalInspectionFormLocation = Location;
        }
    }
}
