using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class TransferableEpicrisisForm : Form
    {
        private readonly PatientClass _patientInfo;
        private bool _isFormClosingByButton;
        private bool _stopSaveParameters;
        private readonly DbEngine _dbEngine;

        public TransferableEpicrisisForm(PatientClass patientInfo, DbEngine dbEngine)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _dbEngine = dbEngine;
            _patientInfo = patientInfo;
        }

        private void TransferableEpicrisisForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.TransferableEpicrisisFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.TransferableEpicrisisFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.TransferableEpicrisisFormLocation;
            }

            textBoxAdditionalInfo.Text = _patientInfo.TransferEpicrisAdditionalInfo;
            textBoxAfterOperationPeriod.Text = _patientInfo.TransferEpicrisAfterOperationPeriod;
            textBoxPlan.Text = _patientInfo.TransferEpicrisPlan;
            textBoxDisabilityList.Text = _patientInfo.TransferEpicrisDisabilityList;
            dateTimePickerDateWriting.Value = _patientInfo.TransferEpicrisWritingDate;            
            checkBoxDisabilityList.Checked = _patientInfo.TransferEpicrisIsIncludeDisabilityList;

            _stopSaveParameters = false;
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


        /// <summary>
        /// Положить введённые данные в пациента
        /// </summary>
        /// <param name="patientInfo"></param>
        private void PutDataToPatient(PatientClass patientInfo)
        {
            patientInfo.TransferEpicrisAdditionalInfo = textBoxAdditionalInfo.Text;
            patientInfo.TransferEpicrisAfterOperationPeriod = textBoxAfterOperationPeriod.Text;
            patientInfo.TransferEpicrisPlan = textBoxPlan.Text;
            patientInfo.TransferEpicrisWritingDate = dateTimePickerDateWriting.Value;
            patientInfo.TransferEpicrisDisabilityList = textBoxDisabilityList.Text;
            patientInfo.TransferEpicrisIsIncludeDisabilityList = checkBoxDisabilityList.Checked;
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

            WordExportEngine.ExportTransferableEpicrisis(tempPatientInfo, _dbEngine.GlobalSettings);
        }

        private void TransferableEpicrisisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }

        private void checkBoxDisabilityList_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDisabilityList.Enabled = checkBoxDisabilityList.Checked;
        }

        private void TransferableEpicrisisForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.TransferableEpicrisisFormLocation = Location;
        }
    }
}
