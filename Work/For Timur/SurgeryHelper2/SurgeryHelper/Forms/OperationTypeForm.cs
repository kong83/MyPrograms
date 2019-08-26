using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class OperationTypeForm : Form
    {
        private const string ObjectBoxNameOnForm = "textBoxOperationTypes";

        private readonly OperationViewForm _operationViewForm;
        private CWorkersKeeper _workersKeeper;
        private COperationTypeWorker _operationTypeWorker;
        private string[] _selectedOperationTypes;

        public OperationTypeForm(CWorkersKeeper workersKeeper)
        {
            Initialize(workersKeeper, string.Empty);
        }

        public OperationTypeForm(
            CWorkersKeeper workersKeeper,
            OperationViewForm operationViewForm,
            string selectedOperationTypes)
        {
            Initialize(workersKeeper, selectedOperationTypes);

            _operationViewForm = operationViewForm;            
        }

        private void Initialize(CWorkersKeeper workersKeeper, string selectedOperationTypes)
        {
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _operationTypeWorker = workersKeeper.OperationTypeWorker;

            _selectedOperationTypes = selectedOperationTypes.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void OperationTypeForm_Load(object sender, EventArgs e)
        {
            ShowOperationType();
        }

        /// <summary>
        /// Показать список типов операций
        /// </summary>
        private void ShowOperationType()
        {
            treeViewOperationType.Nodes.Clear();
            treeViewOperationType.BeginUpdate();
            
            FillNodes(treeViewOperationType.Nodes, -1);

            treeViewOperationType.EndUpdate();
        }

        private bool IsSelectedOperationTypesContainOperationType(string operationTypeName)
        {
            foreach (string selectedOperationType in _selectedOperationTypes)
            {
                if (selectedOperationType == operationTypeName)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Отобразить все вложенные узлы для данного узла
        /// </summary>
        /// <param name="treeNodes">Узелы на списке узлов</param>
        /// <param name="operationTypeId">Id типа операции, который находится в этом узле</param>
        private void FillNodes(TreeNodeCollection treeNodes, int operationTypeId)
        {
            COperationType[] operationTypes = _operationTypeWorker.GetChilds(operationTypeId);

            foreach (COperationType operationType in operationTypes)
            {
                treeNodes.Add(operationType.Name);
            }

            foreach (TreeNode node in treeNodes)
            {
                if (IsSelectedOperationTypesContainOperationType(node.Text))
                {
                    node.Checked = true;
                }

                COperationType operationType = _operationTypeWorker.GetByGeneralData(node.Text);
                if (operationType.Type == NodeType.Folder)
                {
                    node.NodeFont = new Font(treeViewOperationType.Font, FontStyle.Bold);
                    node.ImageIndex = 1;
                    node.SelectedImageIndex = 1;
                    FillNodes(node.Nodes, operationType.Id);

                    if (operationType.NodeFolderStated == NodeFolderStated.Opened)
                    {
                        node.Expand();
                    }
                }
            }
        }


        /// <summary>
        /// Открыть форму для добавления нового типа операции
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (treeViewOperationType.SelectedNode == null)
            {
                new OperationTypeViewForm(_workersKeeper, null).ShowDialog();
            }
            else
            {
                COperationType operationType = _operationTypeWorker.GetByGeneralData(treeViewOperationType.SelectedNode.Text);

                if (operationType.Type == NodeType.Folder)
                {
                    new OperationTypeViewForm(_workersKeeper, null, treeViewOperationType.SelectedNode.Text).ShowDialog();
                }
                else if (treeViewOperationType.SelectedNode.Level > 0)
                {
                    new OperationTypeViewForm(_workersKeeper, null, treeViewOperationType.SelectedNode.Parent.Text).ShowDialog();
                }
                else
                {
                    new OperationTypeViewForm(_workersKeeper, null).ShowDialog();
                }
            }

            ShowOperationType();
        }


        /// <summary>
        /// Открыть форму для удаления выбранного типа операции
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (treeViewOperationType.SelectedNode == null)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new OperationTypeRemoveForm(_workersKeeper, _operationTypeWorker.GetByGeneralData(treeViewOperationType.SelectedNode.Text)).ShowDialog();
            ShowOperationType();
        }


        /// <summary>
        /// Открытие окна для редактирования выбранного типа операции
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (treeViewOperationType.SelectedNode == null)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new OperationTypeViewForm(_workersKeeper, _operationTypeWorker.GetByGeneralData(treeViewOperationType.SelectedNode.Text)).ShowDialog();
            ShowOperationType();
        }


        /// <summary>
        /// Подтверждение выбора типов операций
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (_operationViewForm != null)
            {
                var operationTypeMultilineStr = new StringBuilder();

                operationTypeMultilineStr.Append(FindCheckedNodes(treeViewOperationType.Nodes));

                if (operationTypeMultilineStr.Length > 2)
                {
                    operationTypeMultilineStr.Remove(operationTypeMultilineStr.Length - 2, 2);
                }

                _operationViewForm.PutStringToObject(ObjectBoxNameOnForm, operationTypeMultilineStr.ToString());
            }

            Close();
        }

        /// <summary>
        /// Return information about all checked nodes in the current node
        /// </summary>
        /// <param name="treeNodes">Список узлов в дереве с узлами</param>
        /// <returns></returns>
        private static string FindCheckedNodes(TreeNodeCollection treeNodes)
        {
            var operationTypeMultilineStr = new StringBuilder();

            foreach (TreeNode node in treeNodes)
            {
                if (node.Checked)
                {
                    operationTypeMultilineStr.Append(node.Text + "\r\n");
                }

                operationTypeMultilineStr.Append(FindCheckedNodes(node.Nodes));
            }           

            return operationTypeMultilineStr.ToString();
        }


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            treeViewOperationType.Focus();
        }

        /// <summary>
        /// Запрет выделения папок
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewOperationType_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            COperationType operationType = _operationTypeWorker.GetByGeneralData(e.Node.Text);
            if (operationType == null || operationType.Type == NodeType.Folder)
            {
                MessageBox.Show("Выделение папки невозможно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Запоминание состояния папки при развёртывании
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewOperationType_AfterExpand(object sender, TreeViewEventArgs e)
        {
            COperationType operationType = _operationTypeWorker.GetByGeneralData(e.Node.Text);
            operationType.NodeFolderStated = NodeFolderStated.Opened;
            _operationTypeWorker.Update(operationType);
        }

        /// <summary>
        /// Запоминание состояния папки при сворачивании
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewOperationType_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            COperationType operationType = _operationTypeWorker.GetByGeneralData(e.Node.Text);
            operationType.NodeFolderStated = NodeFolderStated.Closed;
            _operationTypeWorker.Update(operationType);
        }

        /// <summary>
        /// Выделение узла при нажатии на правую кнопку мыши и показ контекстного меню для папки
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewOperationType_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewOperationType.SelectedNode = e.Node;
            COperationType operationType = _operationTypeWorker.GetByGeneralData(e.Node.Text);
            if (operationType.Type == NodeType.Folder && e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        /// <summary>
        /// Обработка двойного нажатия на узле
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewOperationType_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewOperationType.SelectedNode = e.Node;
            COperationType operationType = _operationTypeWorker.GetByGeneralData(e.Node.Text);
            if (operationType.Type == NodeType.Type && e.Button == MouseButtons.Left)
            {
                buttonEdit_Click(sender, e);
            }
        }

        /// <summary>
        /// Обработка выбора пункта "Добавить" на контекстном меню
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void toolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            new OperationTypeViewForm(_workersKeeper, null, treeViewOperationType.SelectedNode.Text).ShowDialog();
            ShowOperationType();
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить новый тип операции", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить тип операции", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Редактировать выбранный тип операции", buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Подтвердить выбор типа операции", buttonOk);
            buttonOk.FlatStyle = FlatStyle.Popup;
        }

        private void buttonOk_MouseLeave(object sender, EventArgs e)
        {
            CToolTipShower.Hide();
            buttonOk.FlatStyle = FlatStyle.Flat;
        }
        #endregion        
    }
}
