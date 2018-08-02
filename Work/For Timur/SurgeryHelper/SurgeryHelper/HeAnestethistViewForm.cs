using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class HeAnestethistViewForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly HeAnestethistClass _heAnestethistInfo;
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

        public HeAnestethistViewForm(DbEngine dbEngine, HeAnestethistClass heAnestethistInfo)
        {
            InitializeComponent();

            _dbEngine = dbEngine;            

            if (heAnestethistInfo == null)
            {
                _heAnestethistInfo = new HeAnestethistClass();
                Text = "Добавление нового анестезиолога";
            }
            else
            {
                _heAnestethistInfo = heAnestethistInfo;
                Text = "Редактирование анестезиолога";
                textBoxHeAnestethistName.Text = _heAnestethistInfo.LastNameWithInitials;
            }
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отменить", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }

        /// <summary>
        /// Сохранение изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxHeAnestethistName.Text))
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _heAnestethistInfo.LastNameWithInitials = textBoxHeAnestethistName.Text;

                if (_heAnestethistInfo.Id == 0)
                {
                    _dbEngine.AddHeAnestethist(_heAnestethistInfo);
                }
                else
                {
                    _dbEngine.UpdateHeAnestethist(_heAnestethistInfo);
                }

                _isFormClosingByButton = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            _isFormClosingByButton = true;
            Close();
        }

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData"></param>
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

        private void HeAnestethistViewForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_isFormClosingByButton)
            {
                e.Cancel = true;
            }
        }
    }
}
