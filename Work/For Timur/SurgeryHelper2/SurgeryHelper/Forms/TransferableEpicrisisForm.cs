using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class TransferableEpicrisisForm : Form
    {
        private readonly CPatient _patientInfo;
        private readonly CHospitalization _hospitalizationInfo;
        private readonly CTransferableEpicrisis _transferableEpicrisisInfo;
        private readonly CGlobalSettings _globalSettings;
        private readonly COperationWorker _operationWorker;
        private readonly CTransferableEpicrisisWorker _transferableEpicrisisWorker;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public TransferableEpicrisisForm(
            CWorkersKeeper workersKeeper,
            CPatient patientInfo,
            CHospitalization hospitalizationInfo,
            CTransferableEpicrisis transferableEpicrisisInfo)
        {
            InitializeComponent();

            _patientInfo = patientInfo;
            _hospitalizationInfo = hospitalizationInfo;
            _transferableEpicrisisInfo = transferableEpicrisisInfo;
            _operationWorker = workersKeeper.OperationWorker;
            _transferableEpicrisisWorker = workersKeeper.TransferableEpicrisisWorker;
            _globalSettings = workersKeeper.GlobalSettings;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }


        private void TransferableEpicrisisForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.TransferableEpicrisisFormLocation.X >= 0 &&
                _configurationEngine.TransferableEpicrisisFormLocation.Y >= 0)
            {
                Location = _configurationEngine.TransferableEpicrisisFormLocation;
            }

            textBoxAdditionalInfo.Text = _transferableEpicrisisInfo.AdditionalInfo;
            textBoxAfterOperationPeriod.Text = _transferableEpicrisisInfo.AfterOperationPeriod;
            textBoxPlan.Text = _transferableEpicrisisInfo.Plan;
            textBoxDisabilityList.Text = _transferableEpicrisisInfo.DisabilityList;
            dateTimePickerDateWriting.Value = _transferableEpicrisisInfo.WritingDate;
            checkBoxDisabilityList.Checked = _transferableEpicrisisInfo.IsIncludeDisabilityList;
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
                PutDataToTransferableEpicrisis();

                _transferableEpicrisisWorker.Update(_transferableEpicrisisInfo);

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
                if (_transferableEpicrisisInfo.NotInDatabase)
                {
                    _transferableEpicrisisWorker.Remove(_transferableEpicrisisInfo.HospitalizationId);
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
        /// Положить введённые данные в пациента
        /// </summary>
        private void PutDataToTransferableEpicrisis()
        {
            _transferableEpicrisisInfo.AdditionalInfo = textBoxAdditionalInfo.Text;
            _transferableEpicrisisInfo.AfterOperationPeriod = textBoxAfterOperationPeriod.Text;
            _transferableEpicrisisInfo.Plan = textBoxPlan.Text;
            _transferableEpicrisisInfo.WritingDate = dateTimePickerDateWriting.Value;
            _transferableEpicrisisInfo.DisabilityList = textBoxDisabilityList.Text;
            _transferableEpicrisisInfo.IsIncludeDisabilityList = checkBoxDisabilityList.Checked;
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
                PutDataToTransferableEpicrisis();

                CWordExportHelper.ExportTransferableEpicrisis(
                    _patientInfo,
                    _hospitalizationInfo,
                    _operationWorker,
                    _transferableEpicrisisInfo,
                    _globalSettings);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Отмена закрытия формы через крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void TransferableEpicrisisForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.TransferableEpicrisisFormLocation = Location;
        }


        /// <summary>
        /// Отображение поля для ввода листа нетрудоспособности
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxDisabilityList_CheckedChanged(object sender, EventArgs e)
        {
            textBoxDisabilityList.Enabled = checkBoxDisabilityList.Checked;
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
