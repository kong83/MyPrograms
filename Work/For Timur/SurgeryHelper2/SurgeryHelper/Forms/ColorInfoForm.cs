using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using System.Drawing;

namespace SurgeryHelper.Forms
{
    public partial class ColorInfoForm : Form
    {
        private readonly CConfigurationEngine _configurationEngine;
        private readonly PatientListForm _patientForm;
        private bool _stopSaveParameters;

        private readonly Color _rowLightColor = Color.FromArgb(255, 230, 230, 230);
        private readonly Color _rowReleaseDateColor = Color.FromArgb(255, 180, 255, 50);
        private readonly Color _rowNoColor = Color.FromArgb(255, 255, 255, 255);
        private readonly Color _rowLineOfCommunicationColor = Color.FromArgb(255, 255, 180, 180);
        
        public ColorInfoForm(CWorkersKeeper workersKeeper, PatientListForm patientForm)
        {
            _stopSaveParameters = true;

            InitializeComponent();

            _configurationEngine = workersKeeper.ConfigurationEngine;
            _patientForm = patientForm;

            panelNoColor.BackColor = _configurationEngine.RowNoColor;
            panelLightColor.BackColor = _configurationEngine.RowLightColor;
            panelReleaseDateColor.BackColor = _configurationEngine.RowReleaseDateColor;
            panelLineOfCommunicationColor.BackColor = _configurationEngine.RowLineOfCommunicationColor;

            ShowDefaultButtonIfColorsAreDefault();
        }

        /// <summary>
        /// Отобразить кнопку для выставления дефолтных цветов, если цвета не дефолтные.
        /// Спрятать её в противном случае
        /// </summary>
        private void ShowDefaultButtonIfColorsAreDefault()
        {
            if (_rowLightColor == _configurationEngine.RowLightColor &&
                    _rowReleaseDateColor == _configurationEngine.RowReleaseDateColor &&
                    _rowNoColor == _configurationEngine.RowNoColor &&
                    _rowLineOfCommunicationColor == _configurationEngine.RowLineOfCommunicationColor)
            {
                buttonDefault.Visible = false;
            }
            else
            {
                buttonDefault.Visible = true;
            }
        }

        private void ColorInfoForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.ColorInfoFormLocation.X >= 0 &&
                _configurationEngine.ColorInfoFormLocation.Y >= 0)
            {
                Location = _configurationEngine.ColorInfoFormLocation;
            }

            _stopSaveParameters = false;
        }
        

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ColorInfoForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.ColorInfoFormLocation = Location;
        }


        /// <summary>
        /// Сохранение новых цветов при изменении цветов на панелях
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel_BackColorChanged(object sender, EventArgs e)
        {
            if (_stopSaveParameters)
            {
                return;
            }

            _configurationEngine.RowNoColor = panelNoColor.BackColor;
            _configurationEngine.RowLightColor = panelLightColor.BackColor;
            _configurationEngine.RowReleaseDateColor = panelReleaseDateColor.BackColor;
            _configurationEngine.RowLineOfCommunicationColor = panelLineOfCommunicationColor.BackColor;

            ShowDefaultButtonIfColorsAreDefault();

            _patientForm.ShowPatients();
        }
        

        /// <summary>
        /// Выбор нового цвета для панелей
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelNoColor_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;
            colorDialog1.Color = panel.BackColor;
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel.BackColor = colorDialog1.Color;
            }
        }


        /// <summary>
        /// Выставить все цвета по дефолту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDefault_Click(object sender, EventArgs e)
        {
            _stopSaveParameters = true;
            panelLightColor.BackColor = _rowLightColor;
            panelReleaseDateColor.BackColor = _rowReleaseDateColor;
            panelNoColor.BackColor = _rowNoColor;            
            panelLineOfCommunicationColor.BackColor = _rowLineOfCommunicationColor;
            _stopSaveParameters = false;

            panel_BackColorChanged(null, null);
        }

        private void DropFocus(object sender, EventArgs e)
        {
            button1.Focus();
        }


        #region Подсказки
        private void buttonOK_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Закрыть окно", buttonOK);
            buttonOK.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOK_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOK.FlatStyle = FlatStyle.Flat;
        }


        private void buttonDefault_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Выставить цвета по умолчанию", buttonDefault);
            buttonDefault.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDefault_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDefault.FlatStyle = FlatStyle.Flat;
        }
        #endregion
    }
}
