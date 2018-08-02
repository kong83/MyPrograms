using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class OperationProtocolForm : Form
    {
        private readonly OperationClass _operationInfo;
        private readonly PatientClass _patientInfo;
        private bool _isFormClosingByButton;
        private bool _stopSaveParameters;
        private readonly DbEngine _dbEngine;

        public OperationProtocolForm(OperationClass operationInfo, PatientClass patientInfo, DbEngine dbEngine)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _dbEngine = dbEngine;
            _operationInfo = operationInfo;
            _patientInfo = patientInfo;
        }

        private void OperationProtocolForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.MedicalInspectionFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.MedicalInspectionFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.MedicalInspectionFormLocation;
            }

            checkBoxDnevnik.Checked = _operationInfo.BeforeOperationEpicrisisIsDairyEnabled;

            numericUpDownADFirst.Value = _operationInfo.BeforeOperationEpicrisisADFirst;
            numericUpDownADSecond.Value = _operationInfo.BeforeOperationEpicrisisADSecond;
            comboBoxBreath.Text = _operationInfo.BeforeOperationEpicrisisBreath;
            numericUpDownChDD.Value = _operationInfo.BeforeOperationEpicrisisChDD;
            textBoxComplaints.Text = _operationInfo.BeforeOperationEpicrisisComplaints;
            comboBoxState.Text = _operationInfo.BeforeOperationEpicrisisState;
            comboBoxHeartRhythm.Text = _operationInfo.BeforeOperationEpicrisisHeartRhythm;
            comboBoxHeartSounds.Text = _operationInfo.BeforeOperationEpicrisisHeartSounds;
            numericUpDownPulse.Value = _operationInfo.BeforeOperationEpicrisisPulse;
            textBoxStLocalis.Text = _operationInfo.BeforeOperationEpicrisisStLocalis;
            textBoxStomach.Text = _operationInfo.BeforeOperationEpicrisisStomach;
            textBoxStool.Text = _operationInfo.BeforeOperationEpicrisisStool;
            textBoxTemperature.Text = _operationInfo.BeforeOperationEpicrisisTemperature;
            comboBoxState.Text = _operationInfo.BeforeOperationEpicrisisState;
            dateTimePickerTimeWritingEpicrisis.Value = 
                _operationInfo.BeforeOperationEpicrisisTimeWriting.Year == 1 
                ? DateTime.Now 
                : _operationInfo.BeforeOperationEpicrisisTimeWriting;

            textBoxUrination.Text = _operationInfo.BeforeOperationEpicrisisUrination;
            textBoxWheeze.Text = _operationInfo.BeforeOperationEpicrisisWheeze;

            checkBoxPlan.Checked = _patientInfo.IsTreatmentPlanActiveInOperationProtocol;
            comboBoxInspectionPlan.Text = _patientInfo.TreatmentPlanInspection;
            dateTimePickerDatePlan.Value = _patientInfo.TreatmentPlanDate.Year == 1 
                ? _patientInfo.DeliveryDate 
                : _patientInfo.TreatmentPlanDate;

            textBoxOperationCourse.Text = _operationInfo.OperationCourse;

            _stopSaveParameters = false;
        }

        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            var tempOperationInfo = new OperationClass(_operationInfo);
            var tempPatientInfo = new PatientClass(_patientInfo);

            PutDataToOperationAndPatient(tempOperationInfo, tempPatientInfo);

            WordExportEngine.ExportOperationProtocol(tempOperationInfo, tempPatientInfo, _dbEngine.GlobalSettings);
        }

        /// <summary>
        /// Положить введённые данные в операцию и пациента
        /// </summary>
        /// <param name="operationInfo">Информация про операцию</param>
        /// <param name="patientInfo">Информация о пациенте</param>
        private void PutDataToOperationAndPatient(OperationClass operationInfo, PatientClass patientInfo)
        {
            operationInfo.BeforeOperationEpicrisisIsDairyEnabled = checkBoxDnevnik.Checked;

            operationInfo.BeforeOperationEpicrisisADFirst = (int)numericUpDownADFirst.Value;
            operationInfo.BeforeOperationEpicrisisADSecond = (int)numericUpDownADSecond.Value;
            operationInfo.BeforeOperationEpicrisisBreath = comboBoxBreath.Text;
            operationInfo.BeforeOperationEpicrisisChDD = (int)numericUpDownChDD.Value;
            operationInfo.BeforeOperationEpicrisisComplaints = textBoxComplaints.Text;
            operationInfo.BeforeOperationEpicrisisState = comboBoxState.Text;
            operationInfo.BeforeOperationEpicrisisHeartRhythm = comboBoxHeartRhythm.Text;
            operationInfo.BeforeOperationEpicrisisHeartSounds = comboBoxHeartSounds.Text;
            operationInfo.BeforeOperationEpicrisisPulse = (int)numericUpDownPulse.Value;
            operationInfo.BeforeOperationEpicrisisStLocalis = textBoxStLocalis.Text;
            operationInfo.BeforeOperationEpicrisisStomach = textBoxStomach.Text;
            operationInfo.BeforeOperationEpicrisisStool = textBoxStool.Text;
            operationInfo.BeforeOperationEpicrisisTemperature = textBoxTemperature.Text;
            operationInfo.BeforeOperationEpicrisisTimeWriting = dateTimePickerTimeWritingEpicrisis.Value;
            operationInfo.BeforeOperationEpicrisisUrination = textBoxUrination.Text;
            operationInfo.BeforeOperationEpicrisisWheeze = textBoxWheeze.Text;

            patientInfo.IsTreatmentPlanActiveInOperationProtocol = checkBoxPlan.Checked;
            patientInfo.TreatmentPlanInspection = comboBoxInspectionPlan.Text;
            patientInfo.TreatmentPlanDate = dateTimePickerDatePlan.Value;

            operationInfo.OperationCourse = textBoxOperationCourse.Text.TrimEnd(new[] { '\r', '\n' });
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToOperationAndPatient(_operationInfo, _patientInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
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


        private void OperationProtocolForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Разрешение/запрещение на ввод данных для плана обследования и лечения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxPlan_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxInspectionPlan.Enabled = dateTimePickerDatePlan.Enabled = checkBoxPlan.Checked;
        }

        /// <summary>
        /// Разрешение/запрещение на ввод данных для дневника предоперационного эпикриза
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBoxDnevnik_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTemperature.Enabled = textBoxComplaints.Enabled = comboBoxState.Enabled =
            textBoxUrination.Enabled = textBoxStool.Enabled = textBoxStLocalis.Enabled = 
            numericUpDownChDD.Enabled = comboBoxBreath.Enabled = textBoxWheeze.Enabled =
            comboBoxHeartSounds.Enabled = comboBoxHeartRhythm.Enabled = textBoxStomach.Enabled =
            numericUpDownPulse.Enabled = numericUpDownADFirst.Enabled = 
            numericUpDownADSecond.Enabled = checkBoxDnevnik.Checked;
        }

        private void OperationProtocolForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.MedicalInspectionFormLocation = Location;
        }
    }
}
