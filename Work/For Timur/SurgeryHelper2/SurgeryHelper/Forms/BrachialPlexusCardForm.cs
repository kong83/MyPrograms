using System;
using System.Drawing;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.MyControls;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class BrachialPlexusCardForm : Form
    {
        private readonly ShadePaint _shadePaint;
        private readonly CBrachialPlexusCard _brachialPlexusCardInfo;
        private readonly CBrachialPlexusCardWorker _brachialPlexusCardWorker;
        private readonly CConfigurationEngine _configurationEngine;
        private bool _isFormClosingByButton;

        public BrachialPlexusCardForm(CWorkersKeeper workersKeeper, CBrachialPlexusCard brachialPlexusCard)
        {
            InitializeComponent();

            _configurationEngine = workersKeeper.ConfigurationEngine;

            _brachialPlexusCardWorker = workersKeeper.BrachialPlexusCardWorker;
            _brachialPlexusCardInfo = brachialPlexusCard;
            _shadePaint = new ShadePaint(_brachialPlexusCardInfo.Picture, string.Empty);            
        }

        private void BrachialPlexusCardForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.PaintDoublePictureFormLocation.X >= 0 &&
                _configurationEngine.PaintDoublePictureFormLocation.Y >= 0)
            {
                Location = _configurationEngine.PaintDoublePictureFormLocation;
            }

            _shadePaint.Location = new Point(390, 6);
            _shadePaint.Parent = this;

            comboBoxSide.Text = _brachialPlexusCardInfo.SideOfCard == CardSide.Left
                ? "Левая сторона"
                : "Правая сторона";
            comboBoxHornersSyndrom.Text = _brachialPlexusCardInfo.HornersSyndrome;
            textBoxVascularStatus.Text = _brachialPlexusCardInfo.VascularStatus;
            textBoxDiafragm.Text = _brachialPlexusCardInfo.Diaphragm;
            textBoxTinnelsSymptome.Text = _brachialPlexusCardInfo.TinelsSymptom;
            checkBoxIsMielographyEnabled.Checked = _brachialPlexusCardInfo.IsMyelographyEnabled;
            comboBoxMielographyType.Text = _brachialPlexusCardInfo.MyelographyType;
            dateTimePickerMielographiDate.Value = _brachialPlexusCardInfo.MyelographyDate;
            textBoxMielography.Text = _brachialPlexusCardInfo.Myelography;
            checkBoxIsEMNGEnabled.Checked = _brachialPlexusCardInfo.IsEMNGEnabled;
            dateTimePickerEMNGDate.Value = _brachialPlexusCardInfo.EMNGDate;
            textBoxEMNG.Text = _brachialPlexusCardInfo.EMNG;
        }


        /// <summary>
        /// Запрет закрытия формы по нажатию на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void BrachialPlexusCardForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.PaintDoublePictureFormLocation = Location;
        }


        /// <summary>
        /// Включение/выключение полей для миелографии, в зависимости от того, 
        /// есть миелография или нет
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxIsMielographyEnabled_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerMielographiDate.Enabled = comboBoxMielographyType.Enabled =
            textBoxMielography.Enabled = checkBoxIsMielographyEnabled.Checked;
        }


        /// <summary>
        /// Включение/выключение полей для ЭМНГ, в зависимости от того, 
        /// есть ЭМНГ или нет
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void checkBoxIsEMNGEnabled_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePickerEMNGDate.Enabled = textBoxEMNG.Enabled = checkBoxIsEMNGEnabled.Checked;
        }


        /// <summary>
        /// Положить введённые данные в карту
        /// </summary>        
        private void PutDataToBrachialPlexusCard()
        {
            _brachialPlexusCardInfo.SideOfCard = comboBoxSide.Text == "Левая сторона"
                ? CardSide.Left
                : CardSide.Right;

            _brachialPlexusCardInfo.HornersSyndrome = comboBoxHornersSyndrom.Text;
            _brachialPlexusCardInfo.VascularStatus = textBoxVascularStatus.Text;
            _brachialPlexusCardInfo.Diaphragm = textBoxDiafragm.Text;
            _brachialPlexusCardInfo.TinelsSymptom = textBoxTinnelsSymptome.Text;
            _brachialPlexusCardInfo.IsMyelographyEnabled = checkBoxIsMielographyEnabled.Checked;
            _brachialPlexusCardInfo.MyelographyType = comboBoxMielographyType.Text;
            _brachialPlexusCardInfo.MyelographyDate = dateTimePickerMielographiDate.Value;
            _brachialPlexusCardInfo.Myelography = textBoxMielography.Text;
            _brachialPlexusCardInfo.IsEMNGEnabled = checkBoxIsEMNGEnabled.Checked;
            _brachialPlexusCardInfo.EMNGDate = dateTimePickerEMNGDate.Value;
            _brachialPlexusCardInfo.EMNG = textBoxEMNG.Text;
            _brachialPlexusCardInfo.Picture = new Bitmap(_shadePaint.CurrentPicture);
        }


        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            PutDataToBrachialPlexusCard();

            try
            {
                _brachialPlexusCardWorker.Update(_brachialPlexusCardInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрытие формы без сохранения изменений
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_brachialPlexusCardInfo.NotInDatabase)
                {
                    _brachialPlexusCardWorker.Remove(_brachialPlexusCardInfo.HospitalizationId, _brachialPlexusCardInfo.VisitId);
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
        /// Экспортировать карту в ворд
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToBrachialPlexusCard();

                CWordExportHelper.ExportBrachialPlexusCard(_brachialPlexusCardInfo, _shadePaint.IsExportEnabled);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
