using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class LineOfCommunicationEpicrisisForm : Form
    {
        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalizationInfo;
        private readonly CLineOfCommunicationEpicrisis _lineOfCommunicationEpicrisisInfo;
        private readonly CGlobalSettings _globalSettings;
        private readonly COperationWorker _operationWorker;
        private readonly CLineOfCommunicationEpicrisisWorker _lineOfCommunicationEpicrisisWorker;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public LineOfCommunicationEpicrisisForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            CLineOfCommunicationEpicrisis lineOfCommunicationEpicrisis)
        {
            InitializeComponent();

            _patientInfo = patientInfo;
            _hospitalizationInfo = hospitalizationInfo;
            _lineOfCommunicationEpicrisisInfo = lineOfCommunicationEpicrisis;
            _operationWorker = workersKeeper.OperationWorker;
            _lineOfCommunicationEpicrisisWorker = workersKeeper.LineOfCommunicationEpicrisisWorker;
            _globalSettings = workersKeeper.GlobalSettings;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }


        /// <summary>
        /// Обработка событий при загрузке формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void LineOfCommunicationEpicrisisForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.LineOfCommunicationFormLocation.X >= 0 &&
                _configurationEngine.LineOfCommunicationFormLocation.Y >= 0)
            {
                Location = _configurationEngine.LineOfCommunicationFormLocation;
            }

            textBoxAdditionalInfo.Text = _lineOfCommunicationEpicrisisInfo.AdditionalInfo;
            textBoxPlan.Text = _lineOfCommunicationEpicrisisInfo.Plan;
            dateTimePickerDateWriting.Value = _lineOfCommunicationEpicrisisInfo.WritingDate;
        }


        /// <summary>
        /// Сохранить информацию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            PutDataToLineOfCommunicationEpicrisis();

            try
            {
                _lineOfCommunicationEpicrisisWorker.Update(_lineOfCommunicationEpicrisisInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Положить введённые данные в этапный эпикриз
        /// </summary>        
        private void PutDataToLineOfCommunicationEpicrisis()
        {
            _lineOfCommunicationEpicrisisInfo.AdditionalInfo = textBoxAdditionalInfo.Text;
            _lineOfCommunicationEpicrisisInfo.Plan = textBoxPlan.Text;
            _lineOfCommunicationEpicrisisInfo.WritingDate = dateTimePickerDateWriting.Value;
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
                if (_lineOfCommunicationEpicrisisInfo.NotInDatabase)
                {
                    _lineOfCommunicationEpicrisisWorker.Remove(_lineOfCommunicationEpicrisisInfo.HospitalizationId);
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
        /// Сгенерировать отчёт в Worde
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToLineOfCommunicationEpicrisis();

                CWordExportHelper.ExportLineOfCommunicationEpicrisis(
                    _patientInfo,
                    _hospitalizationInfo,
                    _operationWorker,
                    _lineOfCommunicationEpicrisisInfo,
                    _globalSettings);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Предотвращение закрытия формы по нажатию на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void LineOfCommunicationEpicrisisForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.LineOfCommunicationFormLocation = Location;
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
        #endregion
    }
}
