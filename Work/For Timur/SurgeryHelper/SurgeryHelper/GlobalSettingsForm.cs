using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class GlobalSettingsForm : Form
    {
        private readonly DbEngine _dbEngine;

        public GlobalSettingsForm(DbEngine dbEngine)
        {
            InitializeComponent();

            _dbEngine = dbEngine;

            textBoxBranchManager.Text = _dbEngine.GlobalSettings.BranchManager;
            textBoxDepartmentName.Text = _dbEngine.GlobalSettings.DepartmentName;
            textBoxDischargeEpicrisisHeader.Text = _dbEngine.GlobalSettings.DischargeEpicrisisHeaderFileName;
            checkBoxIsLoggingEnabled.Checked = _dbEngine.GlobalSettings.IsLoggingEnabled;
        }


        /// <summary>
        /// Закрытие формы без сохранения изменений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Сохранение глобальных настроек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxBranchManager.Text) ||
                string.IsNullOrEmpty(textBoxDepartmentName.Text) ||
                string.IsNullOrEmpty(textBoxDischargeEpicrisisHeader.Text))
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var globalSettings = new GlobalSettingsClass
                {
                    BranchManager = textBoxBranchManager.Text,
                    DepartmentName = textBoxDepartmentName.Text,
                    DischargeEpicrisisHeaderFileName = textBoxDischargeEpicrisisHeader.Text,
                    IsLoggingEnabled = checkBoxIsLoggingEnabled.Checked
                };

                _dbEngine.GlobalSettings = globalSettings;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}
