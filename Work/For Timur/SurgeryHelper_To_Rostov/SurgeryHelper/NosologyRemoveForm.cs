using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SurgeryHelper.Engines;
using SurgeryHelper.Entities;

namespace SurgeryHelper
{
    public partial class NosologyRemoveForm : Form
    {
        private readonly DbEngine _dbEngine;
        private readonly NosologyClass _nosologyInfo;
        private readonly List<PatientClass> _patientsWithCurrentNoslogy;

        public NosologyRemoveForm(DbEngine dbEngine, NosologyClass nosologyInfo)
        {
            InitializeComponent();

            _dbEngine = dbEngine;
            _nosologyInfo = nosologyInfo;
            _patientsWithCurrentNoslogy = _dbEngine.GetPatientByNosology(_nosologyInfo.LastNameWithInitials);
        }

        private void NosologyRemoveForm_Load(object sender, EventArgs e)
        {
            if (_patientsWithCurrentNoslogy.Count > 0)
            {
                labelRemoveInfo.Text = "Вы собираетесь удалить нозологию \"" + _nosologyInfo.LastNameWithInitials + "\".\r\nВнимание! Удаляемая нозология прописана у " + _patientsWithCurrentNoslogy.Count + " пациентов.\r\nВы должны выбрать другую нозологию для этих пациентов.";
                Height = 190;
                comboBoxNosologyNewName.Visible = label1.Visible = true;                

                foreach (NosologyClass nosologyInfo in _dbEngine.NosologyList)
                {
                    if (nosologyInfo.LastNameWithInitials != _nosologyInfo.LastNameWithInitials)
                    {
                        comboBoxNosologyNewName.Items.Add(nosologyInfo.LastNameWithInitials);
                    }
                }

                comboBoxNosologyNewName.SelectedIndex = 0;
            }
            else
            {
                labelRemoveInfo.Text = "Вы собираетесь удалить нозологию \"" + _nosologyInfo.LastNameWithInitials + "\".\r\nВнимание! Данная операция необратима.";
            }
        }

        /// <summary>
        /// Удалить/переименовать нозологию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (_patientsWithCurrentNoslogy.Count > 0)
                {
                    _dbEngine.ChangeNosologyForAllPatients(_nosologyInfo.LastNameWithInitials, comboBoxNosologyNewName.Text);
                }

                _dbEngine.RemoveNosology(_nosologyInfo.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }

        /// <summary>
        /// Закрыть форму без удаления нозологии
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подтвердить удаление нозологии", buttonOk, 15, -20);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonOk);
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отменить удаление нозологии", buttonClose, 15, -20);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(buttonClose);
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
    }
}
