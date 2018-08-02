using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class RangeOfMotionCardForm : Form
    {
        private readonly CRangeOfMotionCard _rangeOfMotionCardInfo;
        private readonly CRangeOfMotionCardWorker _rangeOfMotionCardWorker;
        private readonly CConfigurationEngine _configurationEngine;
        private bool _isFormClosingByButton;

        public RangeOfMotionCardForm(
            CWorkersKeeper workersKeeper, CRangeOfMotionCard rangeOfMotionCard)
        {
            InitializeComponent();

            _rangeOfMotionCardInfo = rangeOfMotionCard;
            _rangeOfMotionCardWorker = workersKeeper.RangeOfMotionCardWorker;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }

        private void RangeOfMotionCardForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.RangeOfMotionCardFormLocation.X >= 0 &&
               _configurationEngine.RangeOfMotionCardFormLocation.Y >= 0)
            {
                Location = _configurationEngine.RangeOfMotionCardFormLocation;
            }

            comboBoxOppositionFinger.Text = _rangeOfMotionCardInfo.OppositionFinger;
            for (int i = 26; i < 26 + _rangeOfMotionCardInfo.Fields.Length; i++)
            {
                Controls["textBox" + i].Text = _rangeOfMotionCardInfo.Fields[i - 26];
            }
        }


        /// <summary>
        /// Запрет закрытия формы при нажатии на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void RangeOfMotionCardForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.RangeOfMotionCardFormLocation = Location;
        }


        /// <summary>
        /// Экспортировать объём движений в ворд
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToRangeOfMotionCard();

                CWordExportHelper.ExportRangeOfMotionCard(_rangeOfMotionCardInfo);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Положить введённые данные в карту объёма движений
        /// </summary>        
        private void PutDataToRangeOfMotionCard()
        {
            _rangeOfMotionCardInfo.OppositionFinger = comboBoxOppositionFinger.Text;
            
            for (int i = 26; i < 26 + _rangeOfMotionCardInfo.Fields.Length; i++)
            {
                _rangeOfMotionCardInfo.Fields[i - 26] = Controls["textBox" + i].Text;
            }
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
                PutDataToRangeOfMotionCard();

                _rangeOfMotionCardWorker.Update(_rangeOfMotionCardInfo);

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
                if (_rangeOfMotionCardInfo.NotInDatabase)
                {
                    _rangeOfMotionCardWorker.Remove(_rangeOfMotionCardInfo.HospitalizationId, _rangeOfMotionCardInfo.VisitId);
                }

                _isFormClosingByButton = true;
                Close();
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
