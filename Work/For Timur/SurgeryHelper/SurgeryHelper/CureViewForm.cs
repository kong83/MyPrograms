using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class CureViewForm : Form
    {
        private readonly DbEngine _dbEngine;
        private bool _isFormClosingByButton;

        private readonly string _cureSaveName;

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

        public CureViewForm(DbEngine dbEngine, CureClass cureInfo)
        {
            InitializeComponent();

            _dbEngine = dbEngine;            

            if (cureInfo == null)
            {
                Text = "Добавление нового лекарства";
                _cureSaveName = null;
            }
            else
            {
                Text = "Редактирование лекарства";
                _cureSaveName = cureInfo.Name;
                textBoxCureName.Text = cureInfo.Name;
                comboBoxPerDayCnt.Text = cureInfo.DefaultPerDayCount;
                comboBoxReceivingMethod.Text = cureInfo.DefaultReceivingMethod;
                comboBoxDuration.Text = cureInfo.DefaultDuration;
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
            if (string.IsNullOrEmpty(textBoxCureName.Text))
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var cureInfo = new CureClass();
                cureInfo.Name = textBoxCureName.Text;
                cureInfo.DefaultPerDayCount = comboBoxPerDayCnt.Text;
                cureInfo.DefaultReceivingMethod = comboBoxReceivingMethod.Text;
                cureInfo.DefaultDuration = comboBoxDuration.Text;

                if (string.IsNullOrEmpty(_cureSaveName))
                {
                    if (_dbEngine.GetCureByName(cureInfo.Name) != null)
                    {
                        MessageBox.Show("Лекарство с таким именем уже существует. Используйте другое имя.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (textBoxCureName.Text.Contains("&") || comboBoxPerDayCnt.Text.Contains("&") || comboBoxReceivingMethod.Text.Contains("&") || comboBoxDuration.Text.Contains("&"))
                    {
                        MessageBox.Show("Использование символа '&' в полях запрещено т.к. может привести к внутренней ошибке программы. Используйте другой символ.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _dbEngine.AddCure(cureInfo);
                }
                else
                {
                    if (_cureSaveName != cureInfo.Name && _dbEngine.GetCureByName(cureInfo.Name) != null)
                    {
                        MessageBox.Show("Лекарство с таким именем уже существует. Используйте другое имя.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (textBoxCureName.Text.Contains("&") || comboBoxPerDayCnt.Text.Contains("&") || comboBoxReceivingMethod.Text.Contains("&") || comboBoxDuration.Text.Contains("&"))
                    {
                        MessageBox.Show("Использование символа '&' в полях запрещено т.к. может привести к внутренней ошибке программы. Используйте другой символ.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _dbEngine.UpdateCure(_cureSaveName, cureInfo);
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
