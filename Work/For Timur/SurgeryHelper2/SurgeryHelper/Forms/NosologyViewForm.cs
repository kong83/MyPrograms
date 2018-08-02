using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class NosologyViewForm : Form
    {
        private readonly CWorkersKeeper _workersKeeper;
        private readonly CNosology _nosologyInfo;
        private readonly CNosologyWorker _nosologyWorker;
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
        public NosologyViewForm(CWorkersKeeper workersKeeper, CNosology nosologyInfo, string notEditableNosologyName)
            : this(workersKeeper, nosologyInfo, string.Empty, notEditableNosologyName)
        { 
            
        }

        public NosologyViewForm(CWorkersKeeper workersKeeper, CNosology nosologyInfo, string defaultParentFolderName, string notEditableNosologyName)
        {
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _nosologyWorker = workersKeeper.NosologyWorker;

            if (!string.IsNullOrEmpty(notEditableNosologyName))
            {
                textBoxNosologyName.ReadOnly = true;
                textBoxNosologyName.Text = notEditableNosologyName;
            }

            if (nosologyInfo == null)
            {
                _nosologyInfo = new CNosology();
                Text = "Добавление новой нозологии";
                AddAllParentNosologyesToComboBox(string.Empty);
                comboBoxParentNodeName.Text = defaultParentFolderName;
            }
            else
            {
                _nosologyInfo = new CNosology(nosologyInfo);
                Text = "Редактирование нозологии";
                textBoxNosologyName.Text = _nosologyInfo.Name;

                AddAllParentNosologyesToComboBox(nosologyInfo.Name);

                if (nosologyInfo.IdParent != -1)
                {
                    CNosology nosology = _nosologyWorker.GetById(nosologyInfo.IdParent);
                    comboBoxParentNodeName.Text = nosology.Name;
                }
            }
        }

        
        /// <summary>
        /// Добавить список возможных нозологий в качестве родительских папок
        /// </summary>
        /// <param name="excludeNosologyName">Нозология, которая не может быть родителем</param>
        private void AddAllParentNosologyesToComboBox(string excludeNosologyName)
        {
            comboBoxParentNodeName.Items.Clear();
            comboBoxParentNodeName.Items.Add("Корневая папка");

            foreach (CNosology nosology in _nosologyWorker.NosologyList)
            {
                if (nosology.Name != excludeNosologyName)
                {
                    comboBoxParentNodeName.Items.Add(nosology.Name);
                }
            }

            comboBoxParentNodeName.SelectedIndex = 0;
        }


        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxNosologyName.Text))
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string oldNosologyName = _nosologyInfo.Name;
                _nosologyInfo.Name = textBoxNosologyName.Text;

                if (comboBoxParentNodeName.Text == "Корневая папка")
                {
                    _nosologyInfo.IdParent = -1;
                }
                else
                {
                    CNosology parentNosology = _nosologyWorker.GetByGeneralData(comboBoxParentNodeName.Text);
                    _nosologyInfo.IdParent = parentNosology.Id;                    
                }

                if (_nosologyInfo.Id == 0)
                {
                    _nosologyWorker.Add(_nosologyInfo);
                }
                else
                {
                    _nosologyWorker.Update(_nosologyInfo);
                    _workersKeeper.PatientWorker.ChangeNosology(oldNosologyName, _nosologyInfo.Name);
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
        /// Закрытие формы
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Нажатая клавиша</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                buttonOk_Click(null, null);
                return true;
            }

            if (keyData == Keys.Escape)
            {
                buttonClose_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// Запрет закрытия фрмы при нажатии на крестик
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void NosologyViewForm_FormClosing(object sender, FormClosingEventArgs e)
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
        }


        #region Подсказки
        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отменить", buttonClose);
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

