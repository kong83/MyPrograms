using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class OperationTypeRemoveForm : Form
    {
        private readonly COperationType _operationTypeInfo;
        private readonly COperationTypeWorker _operationTypeWorker;
        private readonly COperationWorker _operationWorker;
        private readonly int _operationsWithCurrentOperationTypeCnt;

        public OperationTypeRemoveForm(CWorkersKeeper workersKeeper, COperationType operationTypeInfo)
        {
            InitializeComponent();

            _operationTypeInfo = operationTypeInfo;
            _operationTypeWorker = workersKeeper.OperationTypeWorker;
            _operationWorker = workersKeeper.OperationWorker;
            _operationsWithCurrentOperationTypeCnt = _operationWorker.GetCountContainedOperationType(_operationTypeInfo.Name);
        }
       

        private void OperationTypeRemoveForm_Load(object sender, EventArgs e)
        {
            if (_operationTypeInfo.Type == NodeType.Folder && _operationTypeWorker.IsFolderHasChilds(_operationTypeInfo.Id))
            {
                labelRemoveInfo.Text = "Вы не можете удалить тип операции \"" + _operationTypeInfo.Name + "\" поскольку он является папкой с вложенными элементами.\r\nДля удаления данного типа операции необходимо сначала удалить все вложенные элементы.";
                buttonOk.Visible = false;
            }
            else if (_operationsWithCurrentOperationTypeCnt > 0)
            {
                labelRemoveInfo.Text = "Вы собираетесь удалить тип операции \"" + _operationTypeInfo.Name + "\".\r\nВнимание! Удаляемый тип прописан у " + _operationsWithCurrentOperationTypeCnt + " операций.\r\nВы должны выбрать другой тип для этих операций.";
                Height = 190;
                comboBoxOperationTypeNewName.Visible = label1.Visible = true;

                foreach (COperationType operationTypeInfo in _operationTypeWorker.OperationTypeList)
                {
                    if (operationTypeInfo.Name != _operationTypeInfo.Name && operationTypeInfo.Type != NodeType.Folder)
                    {
                        comboBoxOperationTypeNewName.Items.Add(operationTypeInfo.Name);
                    }
                }

                comboBoxOperationTypeNewName.SelectedIndex = 0;
            }
            else
            {
                labelRemoveInfo.Text = "Вы собираетесь удалить тип операции \"" + _operationTypeInfo.Name + "\".\r\nВнимание! Данная операция необратима.";
            }
        }


        /// <summary>
        /// Удалить/переименовать тип операции
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (_operationsWithCurrentOperationTypeCnt > 0)
                {
                    _operationWorker.ChangeOperationType(_operationTypeInfo.Name, comboBoxOperationTypeNewName.Text);
                }

                _operationTypeWorker.Remove(_operationTypeInfo.Id);
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
            CToolTipShower.Show("Подтвердить удаление типа операции", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }

        private void buttonClose_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Отменить удаление типа операции", buttonClose);
            buttonClose.FlatStyle = FlatStyle.Popup;
        }

        private void buttonClose_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonClose.FlatStyle = FlatStyle.Flat;
        }
    }
}
