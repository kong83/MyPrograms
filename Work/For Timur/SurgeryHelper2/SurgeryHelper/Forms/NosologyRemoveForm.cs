using System;
using System.Collections.Generic;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class NosologyRemoveForm : Form
    {
        private readonly CNosology _nosologyInfo;
        private readonly CNosologyWorker _nosologyWorker;
        private readonly CPatientWorker _patientWorker;
        private readonly int _patientsWithCurrentNoslogyCnt;

        public NosologyRemoveForm(CWorkersKeeper workersKeeper, CNosology nosologyInfo)
        {
            InitializeComponent();

            _nosologyInfo = nosologyInfo;
            _nosologyWorker = workersKeeper.NosologyWorker;
            _patientWorker = workersKeeper.PatientWorker;
            _patientsWithCurrentNoslogyCnt = _patientWorker.GetCountContainedNosology(_nosologyInfo.Name);
        }


        private void NosologyRemoveForm_Load(object sender, EventArgs e)
        {
            if (_nosologyInfo.Type == NodeType.Folder && _nosologyWorker.IsFolderHasChilds(_nosologyInfo.Id))
            {
                labelRemoveInfo.Text = "Вы не можете удалить нозологию \"" + _nosologyInfo.Name + "\" поскольку она является папкой с вложенными элементами.\r\nДля удаления данной нозологии необходимо сначала удалить все вложенные элементы.";
                buttonOk.Visible = false;
            }
            else if (_patientsWithCurrentNoslogyCnt > 0)
            {
                labelRemoveInfo.Text = "Вы собираетесь удалить нозологию \"" + _nosologyInfo.Name + "\".\r\nВнимание! Удаляемая нозология прописана у " + _patientsWithCurrentNoslogyCnt + " пациентов.\r\nВы должны выбрать другую нозологию для этих пациентов.";
                Height = 190;
                comboBoxNosologyNewName.Visible = label1.Visible = true;

                foreach (CNosology nosologyInfo in _nosologyWorker.NosologyList)
                {
                    if (nosologyInfo.Name != _nosologyInfo.Name)
                    {
                        comboBoxNosologyNewName.Items.Add(nosologyInfo.Name);
                    }
                }

                comboBoxNosologyNewName.SelectedIndex = 0;
            }
            else
            {
                labelRemoveInfo.Text = "Вы собираетесь удалить нозологию \"" + _nosologyInfo.Name + "\".\r\nВнимание! Данная операция необратима.";
            }
        }


        /// <summary>
        /// Удалить/переименовать нозологию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (_patientsWithCurrentNoslogyCnt > 0)
                {
                    _patientWorker.ChangeNosology(_nosologyInfo.Name, comboBoxNosologyNewName.Text);
                }

                _nosologyWorker.Remove(_nosologyInfo.Id);
            }
            catch (Exception ex)
            {
                MessageBox.ShowDialog(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Close();
        }


        /// <summary>
        /// Закрыть форму без удаления нозологии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить удаление нозологии", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отменить удаление нозологии", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
    }
}
