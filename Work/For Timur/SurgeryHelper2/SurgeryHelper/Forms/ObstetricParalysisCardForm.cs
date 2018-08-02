using System;
using System.Drawing;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class ObstetricParalysisCardForm : Form
    {
        private readonly CObstetricParalysisCard _obstetricParalysisCardInfo;
        private readonly CObstetricParalysisCardWorker _obstetricParalysisCardWorker;
        private readonly CConfigurationEngine _configurationEngine;
        private bool _isFormClosingByButton;

        public ObstetricParalysisCardForm(
            CWorkersKeeper workersKeeper, CObstetricParalysisCard obstetricParalysisCard)
        {
            InitializeComponent();

            _obstetricParalysisCardInfo = obstetricParalysisCard;
            _obstetricParalysisCardWorker = workersKeeper.ObstetricParalysisCardWorker;
            _configurationEngine = workersKeeper.ConfigurationEngine;
        }

        private void ObstetricParalysisCardForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.ObstetricParalysisCardFormLocation.X >= 0 &&
               _configurationEngine.ObstetricParalysisCardFormLocation.Y >= 0)
            {
                Location = _configurationEngine.ObstetricParalysisCardFormLocation;
            }

            for (int i = 1; i < 1 + _obstetricParalysisCardInfo.ComboBoxes.Length; i++)
            {
                groupBoxHandFunction.Controls["comboBox" + i].Text = _obstetricParalysisCardInfo.ComboBoxes[i - 1];
            }

            comboBoxCardSide.Text = _obstetricParalysisCardInfo.SideOfCard == CardSide.Left
                ? "Левая сторона"
                : "Правая сторона";

            PutMarkToPanel(
                _obstetricParalysisCardInfo.GlobalAbductionPicturesSelection,
                "GlobalAbduction");

            PutMarkToPanel(
                _obstetricParalysisCardInfo.GlobalExternalRotationPicturesSelection,
                "GlobalExternalRotation");

            PutMarkToPanel(
                _obstetricParalysisCardInfo.HandToNeckPicturesSelection,
                "HandToNeck");

            PutMarkToPanel(
                _obstetricParalysisCardInfo.HandToSpinePicturesSelection,
                "HandToSpine");

            PutMarkToPanel(
                _obstetricParalysisCardInfo.HandToMouthPicturesSelection,
                "HandToMouth");
        }


        /// <summary>
        /// Поместить галочку на правильную картинку
        /// </summary>
        /// <param name="array">Массив, в котором только одно поле - true</param>
        /// <param name="arrayName">Название массива. По названию и номеру true-элемента будет найдена нужная картинка</param>
        private void PutMarkToPanel(bool[] array, string arrayName)
        {
            int panelNumber = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (array[i])
                {
                    panelNumber = i;
                    break;
                }
            }

            Controls["groupBox" + arrayName].Controls["panel" + arrayName + "0"].Controls["pictureBox" + arrayName].Parent = Controls["groupBox" + arrayName].Controls["panel" + arrayName + panelNumber];
        }


        /// <summary>
        /// Запрет закрытия формы при нажатии на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void ObstetricParalysisCardForm_FormClosing(object sender, FormClosingEventArgs e)
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

            _configurationEngine.ObstetricParalysisCardFormLocation = Location;
        }


        /// <summary>
        /// Экспортировать акушерский паралич в ворд
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            try
            {
                PutDataToObstetricParalysisCard();

                // Рисование галок на картинке для ворда
                var picture = new Bitmap(Properties.Resources.MalletClassification);

                using (Graphics g = Graphics.FromImage(picture))
                {
                    int x = 165;
                    int y = 120;
                    const int step = 97;
                    for (int i = 0; i < 5; i++)
                    {
                        if (_obstetricParalysisCardInfo.GlobalAbductionPicturesSelection[i])
                        {
                            g.DrawImage(Properties.Resources.OK, x, y);
                            break;
                        }

                        x += step;
                    }

                    x = 165;
                    y = 240;
                    for (int i = 0; i < 5; i++)
                    {
                        if (_obstetricParalysisCardInfo.GlobalExternalRotationPicturesSelection[i])
                        {
                            g.DrawImage(Properties.Resources.OK, x, y);
                            break;
                        }

                        x += step;
                    }

                    x = 165;
                    y = 330;
                    for (int i = 0; i < 5; i++)
                    {
                        if (_obstetricParalysisCardInfo.HandToNeckPicturesSelection[i])
                        {
                            g.DrawImage(Properties.Resources.OK, x, y);
                            break;
                        }

                        x += step;
                    }

                    x = 165;
                    y = 450;
                    for (int i = 0; i < 5; i++)
                    {
                        if (_obstetricParalysisCardInfo.HandToSpinePicturesSelection[i])
                        {
                            g.DrawImage(Properties.Resources.OK, x, y);
                            break;
                        }

                        x += step;
                    }

                    x = 165;
                    y = 570;
                    for (int i = 0; i < 5; i++)
                    {
                        if (_obstetricParalysisCardInfo.HandToMouthPicturesSelection[i])
                        {
                            g.DrawImage(Properties.Resources.OK, x, y);
                            break;
                        }

                        x += step;
                    }
                }

                CWordExportHelper.ExportObstetricParalysisCard(_obstetricParalysisCardInfo, picture);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Положить введённые данные в карту с акушерским параличом
        /// </summary>        
        private void PutDataToObstetricParalysisCard()
        {
            _obstetricParalysisCardInfo.SideOfCard = comboBoxCardSide.Text == "Левая сторона"
                ? CardSide.Left
                : CardSide.Right;

            for (int i = 1; i < 1 + _obstetricParalysisCardInfo.ComboBoxes.Length; i++)
            {
                _obstetricParalysisCardInfo.ComboBoxes[i - 1] = groupBoxHandFunction.Controls["comboBox" + i].Text;
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
                PutDataToObstetricParalysisCard();

                _obstetricParalysisCardWorker.Update(_obstetricParalysisCardInfo);

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
                if (_obstetricParalysisCardInfo.NotInDatabase)
                {
                    _obstetricParalysisCardWorker.Remove(_obstetricParalysisCardInfo.HospitalizationId, _obstetricParalysisCardInfo.VisitId);
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


        /// <summary>
        /// Получить номер панели
        /// </summary>
        /// <param name="panel">Панель, для которой надо получить номер</param>
        /// <returns></returns>
        private static int GetPanelNumber(Panel panel)
        {
            return Convert.ToInt32(panel.Name.Substring(panel.Name.Length - 1));
        }


        /// <summary>
        /// Заполнить все элементы массива значениями false, а элемент с переданным номером - значением true
        /// </summary>
        /// <param name="array">Массив типа bool</param>
        /// <param name="number">Номер в массиве, который должен быть помечен как true</param>
        private static void SetTrueToSelectedCellOfArray(bool[] array, int number)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = false;
            }

            array[number] = true;
        }


        /// <summary>
        /// Выделение Global Abduction
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelGlobalAbduction_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;

            int panelNumber = GetPanelNumber(panel);

            pictureBoxGlobalAbduction.Parent = panel;

            SetTrueToSelectedCellOfArray(_obstetricParalysisCardInfo.GlobalAbductionPicturesSelection, panelNumber);
        }


        /// <summary>
        /// Выделение Global External Rotation
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelGlobalExternalRotation_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;

            int panelNumber = GetPanelNumber(panel);

            pictureBoxGlobalExternalRotation.Parent = panel;

            SetTrueToSelectedCellOfArray(_obstetricParalysisCardInfo.GlobalExternalRotationPicturesSelection, panelNumber);
        }


        /// <summary>
        /// Выделение Hand To Neck
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelHandToNeck_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;

            int panelNumber = GetPanelNumber(panel);

            pictureBoxHandToNeck.Parent = panel;

            SetTrueToSelectedCellOfArray(_obstetricParalysisCardInfo.HandToNeckPicturesSelection, panelNumber);
        }


        /// <summary>
        /// Выделение Hand To Spine
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelHandToSpine_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;

            int panelNumber = GetPanelNumber(panel);

            pictureBoxHandToSpine.Parent = panel;

            SetTrueToSelectedCellOfArray(_obstetricParalysisCardInfo.HandToSpinePicturesSelection, panelNumber);
        }


        /// <summary>
        /// Выделение Hand To Mouth
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void panelHandToMouth_MouseClick(object sender, MouseEventArgs e)
        {
            var panel = (Panel)sender;

            int panelNumber = GetPanelNumber(panel);

            pictureBoxHandToMouth.Parent = panel;

            SetTrueToSelectedCellOfArray(_obstetricParalysisCardInfo.HandToMouthPicturesSelection, panelNumber);
        }
    }
}
