using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class LineOfCommunicationEpicrisisForm : Form
    {
        private readonly PatientClass _patientInfo;
        private bool _isFormClosingByButton;
        private bool _stopSaveParameters;
        private readonly DbEngine _dbEngine;

        public LineOfCommunicationEpicrisisForm(PatientClass patientInfo, DbEngine dbEngine)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _dbEngine = dbEngine;
            _patientInfo = patientInfo;
        }

        private void LineOfCommunicationEpicrisisForm_Load(object sender, EventArgs e)
        {
            if (_dbEngine.ConfigEngine.LineOfCommunicationFormLocation.X >= 0 &&
                _dbEngine.ConfigEngine.LineOfCommunicationFormLocation.Y >= 0)
            {
                Location = _dbEngine.ConfigEngine.LineOfCommunicationFormLocation;
            }

            textBoxAdditionalInfo.Text = _patientInfo.LineOfCommEpicrisAdditionalInfo;
            textBoxPlan.Text = _patientInfo.LineOfCommEpicrisPlan;
            dateTimePickerDateWriting.Value = _patientInfo.LineOfCommEpicrisWritingDate;
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
        /// Положить введённые данные в пациента
        /// </summary>
        /// <param name="patientInfo"></param>
        private void PutDataToPatient(PatientClass patientInfo)
        {
            patientInfo.LineOfCommEpicrisAdditionalInfo = textBoxAdditionalInfo.Text;
            patientInfo.LineOfCommEpicrisPlan = textBoxPlan.Text;
            patientInfo.LineOfCommEpicrisWritingDate = dateTimePickerDateWriting.Value;
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
        /// Сгенерировать отчёт в Worde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            var tempPatientInfo = new PatientClass(_patientInfo);

            PutDataToPatient(tempPatientInfo);

            WordExportEngine.ExportLineOfCommunicationEpicrisis(tempPatientInfo, _dbEngine.GlobalSettings);
        }

        private void LineOfCommunicationEpicrisisForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }

        private void LineOfCommunicationEpicrisisForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _dbEngine.ConfigEngine.LineOfCommunicationFormLocation = Location;
        }
    }
}
