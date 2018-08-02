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
    public partial class PaintDoublePictureForm : Form
    {
        private readonly ShadePaint _shadePaintLeft;
        private readonly ShadePaint _shadePaintRight;
        private readonly CCardWorker _cardWorker;
        private readonly CCard _cardLeftInfo;
        private readonly CCard _cardRightInfo;
        private readonly CConfigurationEngine _configurationEngine;
        private bool _isFormClosingByButton;

        public override sealed string Text
        {
            get
            {
                return base.Text;
            }

            set
            {
                base.Text = value;
            }
        }

        public PaintDoublePictureForm(string formName, CWorkersKeeper workersKeeper, CCard cardLeftInfo, CCard cardRightInfo)
        {
            InitializeComponent();

            _configurationEngine = workersKeeper.ConfigurationEngine;

            Text = formName;
            _cardWorker = workersKeeper.CardWorker;
            _cardLeftInfo = cardLeftInfo;
            _cardRightInfo = cardRightInfo;
            _shadePaintLeft = new ShadePaint(cardLeftInfo.Picture, "Левая сторона");
            _shadePaintRight = new ShadePaint(cardRightInfo.Picture, "Правая сторона");
        }


        private void PaintDoublePictureForm_Load(object sender, EventArgs e)
        {
            if (_configurationEngine.PaintDoublePictureFormLocation.X >= 0 &&
                _configurationEngine.PaintDoublePictureFormLocation.Y >= 0)
            {
                Location = _configurationEngine.PaintDoublePictureFormLocation;
            }

            _shadePaintLeft.Location = new Point(10, 10);
            _shadePaintLeft.Parent = this;

            _shadePaintRight.Location = new Point(30 + _shadePaintLeft.Width, 10);
            _shadePaintRight.Parent = this;

            Height = _shadePaintLeft.Height + 80;
            Width = _shadePaintLeft.Width + _shadePaintRight.Width + 40;
        }


        #region Посказки
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

        private void buttonDocuments_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Экспортировать карту обследования в Word", buttonDocuments);
            buttonDocuments.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDocuments_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDocuments.FlatStyle = FlatStyle.Flat;
        }
        #endregion


        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            _cardLeftInfo.Picture = new Bitmap(_shadePaintLeft.CurrentPicture);
            _cardRightInfo.Picture = new Bitmap(_shadePaintRight.CurrentPicture);

            try
            {
                _cardWorker.Update(_cardLeftInfo);
                _cardWorker.Update(_cardRightInfo);

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Закрыть форму без сохранения изменений
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            try
            {
                if (_cardLeftInfo.NotInDatabase)
                {
                    _cardWorker.Remove(_cardLeftInfo.HospitalizationId, _cardLeftInfo.VisitId);
                }

                if (_cardRightInfo.NotInDatabase)
                {
                    _cardWorker.Remove(_cardRightInfo.HospitalizationId, _cardRightInfo.VisitId);
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
        /// Экспортировать картинку в Word
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDocuments_Click(object sender, EventArgs e)
        {
            var pictures = new Bitmap[3];

            string caption = string.Empty;

            if (checkBoxCaption.Checked)
            {
                caption = Text;
            }

            if (checkBoxCreatePalette.Checked)
            {
                pictures[0] = Properties.Resources.palette;
            }

            if (_shadePaintLeft.IsExportEnabled)
            {
                pictures[1] = _shadePaintLeft.CurrentPicture;
            }

            if (_shadePaintRight.IsExportEnabled)
            {
                pictures[2] = _shadePaintRight.CurrentPicture;
            }

            CWordExportHelper.ExportPicture(caption, pictures[0], pictures[1], pictures[2]);
        }


        /// <summary>
        /// Предотвращение закрытия окна по нажатию на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void PaintPictureForm_FormClosing(object sender, FormClosingEventArgs e)
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
    }
}
