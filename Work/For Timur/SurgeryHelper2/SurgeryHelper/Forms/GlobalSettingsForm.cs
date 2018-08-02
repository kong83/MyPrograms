using System;
using System.Windows.Forms;

using SurgeryHelper.Tools;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class GlobalSettingsForm : Form
    {
        private readonly CGlobalSettings _globalSettings;
        private readonly PatientListForm _patientListForm;

        public GlobalSettingsForm(CWorkersKeeper workersKeeper, PatientListForm patientListForm)
        {
            InitializeComponent();

            _globalSettings = workersKeeper.GlobalSettings;
            _patientListForm = patientListForm;
            textBoxBranchManager.Text = _globalSettings.BranchManager;
            textBoxDepartmentName.Text = _globalSettings.DepartmentName;
            textBoxDischargeEpicrisisHeader.Text = _globalSettings.DischargeEpicrisisHeaderFileName;
            textBoxHeAnaesthetist.Text = _globalSettings.HeAnaesthetist;
            textBoxSheAnaesthetist.Text = _globalSettings.SheAnaesthetist;
            checkBoxShowDbIndexes.Checked = _globalSettings.ShowDbIndexes;
        }


        /// <summary>
        /// Закрытие формы без сохранения изменений
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Сохранение глобальных настроек
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxBranchManager.Text) ||
                string.IsNullOrEmpty(textBoxDepartmentName.Text) ||
                string.IsNullOrEmpty(textBoxDischargeEpicrisisHeader.Text) ||
                string.IsNullOrEmpty(textBoxHeAnaesthetist.Text) ||
                string.IsNullOrEmpty(textBoxSheAnaesthetist.Text))
            {
                MessageBox.ShowDialog("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _globalSettings.BranchManager = textBoxBranchManager.Text;
                _globalSettings.DepartmentName = textBoxDepartmentName.Text;
                _globalSettings.DischargeEpicrisisHeaderFileName = textBoxDischargeEpicrisisHeader.Text;
                _globalSettings.HeAnaesthetist = textBoxHeAnaesthetist.Text;
                _globalSettings.SheAnaesthetist = textBoxSheAnaesthetist.Text;
                _globalSettings.ShowDbIndexes = checkBoxShowDbIndexes.Checked;

                _patientListForm.RefreshTable();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void checkBoxShowDbIndexes_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Рекомендуется выставлять эту настроку только для исследования ошибок с исчезнувшими объектами", checkBoxShowDbIndexes);
        }

        private void checkBoxShowDbIndexes_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
        }
    }
}
