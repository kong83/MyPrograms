using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class AnamneseForm : Form
    {
        private readonly CAnamnese _anamneseInfo;
        private readonly CAnamneseWorker _anamneseWorker;
        private bool _isFormClosingByButton;
        private readonly CConfigurationEngine _configurationEngine;

        public AnamneseForm(CWorkersKeeper workersKeeper, CAnamnese anamnese)
        {
            InitializeComponent();

            _anamneseInfo = anamnese;
            _anamneseWorker = workersKeeper.AnamneseWorker;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }


        /// <summary>
        /// Обработка событий при загрузке формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void AnamneseForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.AnamneseFormLocation.X >= 0 &&
                _configurationEngine.AnamneseFormLocation.Y >= 0)
            {
                Location = _configurationEngine.AnamneseFormLocation;
            }

            textBoxAnMorbi.Text = _anamneseInfo.AnMorbi;
            if (_anamneseInfo.TraumaDate.HasValue)
            {
                dateTimePickerDateTrauma.Checked = true;
                dateTimePickerDateTrauma.Value = _anamneseInfo.TraumaDate.Value;
            }
            else
            {
                dateTimePickerDateTrauma.Checked = false;
            }
        }


        /// <summary>
        /// Положить введённые данные в анамнез
        /// </summary>        
        private void PutDataToAnamnese()
        {
            _anamneseInfo.AnMorbi = textBoxAnMorbi.Text;
            if (dateTimePickerDateTrauma.Checked)
            {
                _anamneseInfo.TraumaDate = dateTimePickerDateTrauma.Value;
            }
            else
            {
                _anamneseInfo.TraumaDate = null;
            }
        }


        /// <summary>
        /// Сохранить информацию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            PutDataToAnamnese();

            try
            {
                _anamneseWorker.Update(_anamneseInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_anamneseInfo.NotInDatabase)
                {
                    _anamneseWorker.Remove(_anamneseInfo.PatientId);
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
        /// Предотвращение закрытия формы по нажатию на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void AnamneseForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.AnamneseFormLocation = Location;
        }


        #region Подсказки
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
