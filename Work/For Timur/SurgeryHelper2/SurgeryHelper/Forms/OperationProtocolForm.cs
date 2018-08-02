using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class OperationProtocolForm : Form
    {
        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalizationInfo;
        private readonly COperationProtocol _operationProtocolInfo;
        private readonly COperationProtocolWorker _operationProtocolWorker;
        private readonly CGlobalSettings _globalSettings;
        private readonly COperation _operationInfo;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public OperationProtocolForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            COperation operationInfo,
            COperationProtocol operationProtocolInfo)
        {
            InitializeComponent();

            _patientInfo = patientInfo;
            _hospitalizationInfo = hospitalizationInfo;
            _operationProtocolInfo = operationProtocolInfo;
            _operationInfo = operationInfo;
            _operationProtocolWorker = workersKeeper.OperationProtocolWorker;
            _globalSettings = workersKeeper.GlobalSettings;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }

        private void OperationProtocolForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.MedicalInspectionFormLocation.X >= 0 &&
                _configurationEngine.MedicalInspectionFormLocation.Y >= 0)
            {
                Location = _configurationEngine.MedicalInspectionFormLocation;
            }

            checkBoxDnevnik.Checked = _operationProtocolInfo.IsDairyEnabled;

            numericUpDownADFirst.Value = _operationProtocolInfo.ADFirst;
            numericUpDownADSecond.Value = _operationProtocolInfo.ADSecond;
            comboBoxBreath.Text = _operationProtocolInfo.Breath;
            numericUpDownChDD.Value = _operationProtocolInfo.ChDD;
            textBoxComplaints.Text = _operationProtocolInfo.Complaints;
            comboBoxState.Text = _operationProtocolInfo.State;
            comboBoxHeartRhythm.Text = _operationProtocolInfo.HeartRhythm;
            comboBoxHeartSounds.Text = _operationProtocolInfo.HeartSounds;
            numericUpDownPulse.Value = _operationProtocolInfo.Pulse;
            textBoxStLocalis.Text = _operationProtocolInfo.StLocalis;
            textBoxStomach.Text = _operationProtocolInfo.Stomach;
            textBoxStool.Text = _operationProtocolInfo.Stool;
            textBoxTemperature.Text = _operationProtocolInfo.Temperature;
            comboBoxState.Text = _operationProtocolInfo.State;
            dateTimePickerTimeWritingEpicrisis.Value =
                _operationProtocolInfo.TimeWriting.Year == 1
                ? DateTime.Now
                : _operationProtocolInfo.TimeWriting;

            textBoxUrination.Text = _operationProtocolInfo.Urination;
            textBoxWheeze.Text = _operationProtocolInfo.Wheeze;

            checkBoxPlan.Checked = _operationProtocolInfo.IsTreatmentPlanActiveInOperationProtocol;
            comboBoxInspectionPlan.Text = _operationProtocolInfo.TreatmentPlanInspection;
            dateTimePickerDatePlan.Value = _operationProtocolInfo.TreatmentPlanDate.Year == 1
                ? _hospitalizationInfo.DeliveryDate
                : _operationProtocolInfo.TreatmentPlanDate;

            textBoxOperationCourse.Text = _operationProtocolInfo.OperationCourse;
        }

        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToOperationProtocol();

                CWordExportHelper.ExportOperationProtocol(
                    _patientInfo,
                    _operationInfo,
                    _operationProtocolInfo,
                    _hospitalizationInfo,
                    _globalSettings);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Положить введённые данные в протокол операции
        /// </summary>
        private void PutDataToOperationProtocol()
        {
            _operationProtocolInfo.IsDairyEnabled = checkBoxDnevnik.Checked;

            _operationProtocolInfo.ADFirst = (int)numericUpDownADFirst.Value;
            _operationProtocolInfo.ADSecond = (int)numericUpDownADSecond.Value;
            _operationProtocolInfo.Breath = comboBoxBreath.Text;
            _operationProtocolInfo.ChDD = (int)numericUpDownChDD.Value;
            _operationProtocolInfo.Complaints = textBoxComplaints.Text;
            _operationProtocolInfo.State = comboBoxState.Text;
            _operationProtocolInfo.HeartRhythm = comboBoxHeartRhythm.Text;
            _operationProtocolInfo.HeartSounds = comboBoxHeartSounds.Text;
            _operationProtocolInfo.Pulse = (int)numericUpDownPulse.Value;
            _operationProtocolInfo.StLocalis = textBoxStLocalis.Text;
            _operationProtocolInfo.Stomach = textBoxStomach.Text;
            _operationProtocolInfo.Stool = textBoxStool.Text;
            _operationProtocolInfo.Temperature = textBoxTemperature.Text;
            _operationProtocolInfo.TimeWriting = dateTimePickerTimeWritingEpicrisis.Value;
            _operationProtocolInfo.Urination = textBoxUrination.Text;
            _operationProtocolInfo.Wheeze = textBoxWheeze.Text;

            _operationProtocolInfo.IsTreatmentPlanActiveInOperationProtocol = checkBoxPlan.Checked;
            _operationProtocolInfo.TreatmentPlanInspection = comboBoxInspectionPlan.Text;
            _operationProtocolInfo.TreatmentPlanDate = dateTimePickerDatePlan.Value;

            _operationProtocolInfo.OperationCourse = textBoxOperationCourse.Text.TrimEnd(new[] { '\r', '\n' });
        }


        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToOperationProtocol();

                _operationProtocolWorker.Update(_operationProtocolInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_operationProtocolInfo.NotInDatabase)
                {
                    _operationProtocolWorker.Remove(_operationProtocolInfo.OperationId);
                }

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        private void OperationProtocolForm_FormClosing(object sender, FormClosingEventArgs e)
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

        /// <summary>
        /// Разрешение/запрещение на ввод данных для плана обследования и лечения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxPlan_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxInspectionPlan.Enabled = dateTimePickerDatePlan.Enabled = checkBoxPlan.Checked;
        }

        /// <summary>
        /// Разрешение/запрещение на ввод данных для дневника предоперационного эпикриза
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxDnevnik_CheckedChanged(object sender, EventArgs e)
        {
            textBoxTemperature.Enabled = textBoxComplaints.Enabled = comboBoxState.Enabled =
            textBoxUrination.Enabled = textBoxStool.Enabled = textBoxStLocalis.Enabled =
            numericUpDownChDD.Enabled = comboBoxBreath.Enabled = textBoxWheeze.Enabled =
            comboBoxHeartSounds.Enabled = comboBoxHeartRhythm.Enabled = textBoxStomach.Enabled =
            numericUpDownPulse.Enabled = numericUpDownADFirst.Enabled =
            numericUpDownADSecond.Enabled = checkBoxDnevnik.Checked;
        }
    }
}
