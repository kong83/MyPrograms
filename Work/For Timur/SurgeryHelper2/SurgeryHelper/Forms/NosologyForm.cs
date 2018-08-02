using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class NosologyForm : Form
    {
        private readonly PatientViewForm _patientViewForm;
        private CWorkersKeeper _workersKeeper;
        private CNosologyWorker _nosologyWorker;
        private List<string> _selectedNosologies;
        private bool _hideRemoveAndEditButtons;
        private bool _stopCheckCatching;

        public NosologyForm(CWorkersKeeper workersKeeper, bool hideRemoveAndEditButtons)
        {
            Initialize(workersKeeper, new List<string>(), hideRemoveAndEditButtons);
        }

        public NosologyForm(CWorkersKeeper workersKeeper, PatientViewForm patientViewForm, List<string> selectedNosologies)
        {
            Initialize(workersKeeper, selectedNosologies, true);

            _patientViewForm = patientViewForm;
        }

        private void Initialize(CWorkersKeeper workersKeeper, List<string> selectedNosologies, bool hideRemoveAndEditButtons)
        {
            InitializeComponent();

            _workersKeeper = workersKeeper;
            _nosologyWorker = workersKeeper.NosologyWorker;
            _selectedNosologies = selectedNosologies;

            _hideRemoveAndEditButtons = hideRemoveAndEditButtons;            
        }

        private void NosologyForm_Load(object sender, EventArgs e)
        {            
            ShowNosologyes();
        }


        /// <summary>
        /// Показать список нозологий
        /// </summary>
        private void ShowNosologyes()
        {
            _stopCheckCatching = true;
            treeViewNosologyes.Nodes.Clear();
            treeViewNosologyes.BeginUpdate();

            FillNodes(treeViewNosologyes.Nodes, -1);

            treeViewNosologyes.EndUpdate();
            _stopCheckCatching = false;
        }

        private bool IsSelectedNosologysContainNosology(string nosologyName)
        {            
            foreach (string selectedNosology in _selectedNosologies)
            {
                if (selectedNosology == nosologyName)
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
        /// <param name="nosologyId">Id нозологии, который находится в этом узле</param>
        private void FillNodes(TreeNodeCollection treeNodes, int nosologyId)
        {
            CNosology[] nosologies = _nosologyWorker.GetChilds(nosologyId);

            foreach (CNosology nosology in nosologies)
            {
                treeNodes.Add(nosology.Name);
            }

            foreach (TreeNode node in treeNodes)
            {
                if (IsSelectedNosologysContainNosology(node.Text))
                {                    
                    _stopCheckCatching = false;
                    node.Checked = true;
                    _stopCheckCatching = true;
                }

                CNosology nosology = _nosologyWorker.GetByGeneralData(node.Text);
                if (nosology.Type == NodeType.Folder)
                {
                    FillNodes(node.Nodes, nosology.Id);

                    if (nosology.NodeFolderStated == NodeFolderStated.Opened && node.Nodes.Count > 0)
                    {
                        node.Expand();
                    }
                }
            }
        }


        /// <summary>
        /// Добавить новую нозологию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (treeViewNosologyes.SelectedNode == null)
            {
                new NosologyViewForm(_workersKeeper, null, null).ShowDialog();
            }
            else
            {
                CNosology nosology = _nosologyWorker.GetByGeneralData(treeViewNosologyes.SelectedNode.Text);

                if (nosology.Type == NodeType.Folder)
                {
                    new NosologyViewForm(_workersKeeper, null, treeViewNosologyes.SelectedNode.Text, null).ShowDialog();
                }
                else if (treeViewNosologyes.SelectedNode.Level > 0)
                {
                    new NosologyViewForm(_workersKeeper, null, treeViewNosologyes.SelectedNode.Parent.Text, null).ShowDialog();
                }
                else
                {
                    new NosologyViewForm(_workersKeeper, null, null).ShowDialog();
                }
            }

            ShowNosologyes();
        }


        /// <summary>
        /// Удалить выделенную нозологию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_hideRemoveAndEditButtons)
            {
                MessageBox.ShowDialog("Для редактирования или удаления нозологий необходимо закрыть все формы с пациентами и переоткрыть форму", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
    
            if (treeViewNosologyes.SelectedNode == null)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new NosologyRemoveForm(_workersKeeper, _nosologyWorker.GetByGeneralData(treeViewNosologyes.SelectedNode.Text)).ShowDialog();            
            ShowNosologyes();
        }
        

        /// <summary>
        /// Редактировать выделенную нозологию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (_hideRemoveAndEditButtons)
            {
                MessageBox.ShowDialog("Для редактирования или удаления нозологий необходимо закрыть все формы с пациентами и переоткрыть форму", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (treeViewNosologyes.SelectedNode == null)
            {
                MessageBox.ShowDialog("Нет выделенных записей", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            new NosologyViewForm(_workersKeeper, _nosologyWorker.GetByGeneralData(treeViewNosologyes.SelectedNode.Text), null).ShowDialog();
            ShowNosologyes();
        }


        /// <summary>
        /// Отобразить на форме с операциями выбранную нозологию
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (treeViewNosologyes.SelectedNode == null)
            {
                Close();
                return;
            }

            if (_patientViewForm != null)
            {
                _patientViewForm.SelectedNosologyList = GetKeyNosologyList();
            }

            Close();
        }

        /// <summary>
        /// Получить список выбранных ключевых нозологий. Это конечные листы в дереве
        /// </summary>
        /// <returns></returns>
        private List<string> GetKeyNosologyList()
        {
            var keyNosologyNames = new List<string>();
            foreach (TreeNode node in treeViewNosologyes.Nodes)
            {
                if (node.Checked)
                {
                    CollectLastNodeTextes(node, keyNosologyNames);
                    break;
                }
            }

            return keyNosologyNames;
        }

        /// <summary>
        /// Найти последний выделенный лист в папке
        /// </summary>
        /// <param name="rootNode">Корневой узел</param>
        /// <param name="keyNosologyNames">Список ключевых нозологий</param>
        /// <returns></returns>
        private static void CollectLastNodeTextes(TreeNode rootNode, List<string> keyNosologyNames)
        {
            if (rootNode.Nodes.Count > 0)
            {
                int saveKeyNamesCount = keyNosologyNames.Count;
                foreach (TreeNode node in rootNode.Nodes)
                {
                    if (node.Checked)
                    {
                        CollectLastNodeTextes(node, keyNosologyNames);
                    }
                }

                if (saveKeyNamesCount == keyNosologyNames.Count)
                {
                    keyNosologyNames.Add(rootNode.Text);
                }
            }
            else
            {
                keyNosologyNames.Add(rootNode.Text);
            }
        }


        /// <summary>
        /// Сброс фокуса с кнопок при нажатии
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void button_DropFocus(object sender, EventArgs e)
        {
            treeViewNosologyes.Focus();
        }

        /// <summary>
        /// Запоминание состояния папки при развёртывании
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewNosologyes_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (_stopCheckCatching)
            {
                return;
            }

            CNosology nosology = _nosologyWorker.GetByGeneralData(e.Node.Text);
            nosology.NodeFolderStated = NodeFolderStated.Closed;
            _nosologyWorker.Update(nosology);
        }

        /// <summary>
        /// Запоминание состояния папки при сворачивании
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewNosologyes_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (_stopCheckCatching)
            {
                return;
            }

            CNosology nosology = _nosologyWorker.GetByGeneralData(e.Node.Text);
            nosology.NodeFolderStated = NodeFolderStated.Opened;
            _nosologyWorker.Update(nosology);
        }

        /// <summary>
        /// Выделение узла при нажатии на правую кнопку мыши и показ контекстного меню для папки
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewNosologyes_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeViewNosologyes.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }

        /// <summary>
        /// Обработка нажатия на контекстное меню пункт "Добавить"
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void еoolStripMenuItemAdd_Click(object sender, EventArgs e)
        {
            //new NosologyViewForm(_workersKeeper, null, treeViewNosologyes.SelectedNode.Text).ShowDialog();
            //ShowNosologyes();

            buttonAdd_Click(sender, e);
        }

        /// <summary>
        /// Обработка нажатия на контекстное меню пункт "Изменить"
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void toolStripMenuItemChange_Click(object sender, EventArgs e)
        {
            buttonEdit_Click(sender, e);
        }

        /// <summary>
        /// Выделение и снятие выделения со всех нужных узлов при выделении узла
        /// При выделении, мы должны выделить всех родителей и снять выделение со всех узлов на корневом уровне и во всех их дочерних узлах
        /// При снятии выделения мы должны снять выделение со всех вложенных в данный узел узлов
        /// </summary>
        /// <param name="sender">Объект, пославший сообщение</param>
        /// <param name="e">Объект, содержащий данные посланного сообщения</param>
        private void treeViewNosologyes_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (_stopCheckCatching)
            {
                return;
            }

            _stopCheckCatching = true;

            TreeNode selectedNode = e.Node;

            if (selectedNode.Checked)
            {
                TreeNode parentNode = e.Node;
                while (parentNode.Parent != null)
                {
                    parentNode = parentNode.Parent;
                    parentNode.Checked = true;
                }

                foreach (TreeNode node in treeViewNosologyes.Nodes)
                {
                    if (node.Text != parentNode.Text)
                    {
                        RemoveAllCheckFromChildNodes(node);
                    }
                }
            }
            else
            {
                RemoveAllCheckFromChildNodes(selectedNode);
            }

            _stopCheckCatching = false;
        }

        /// <summary>
        /// Удалить все выделения с узлов, вложенных в данный узел
        /// </summary>
        /// <param name="parentNode"></param>
        private static void RemoveAllCheckFromChildNodes(TreeNode parentNode)
        {
            foreach (TreeNode treeNode in parentNode.Nodes)
            {
                treeNode.Checked = false;
                RemoveAllCheckFromChildNodes(treeNode);
            }

            parentNode.Checked = false;
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Добавить новую нозологию", buttonAdd);
            buttonAdd.FlatStyle = FlatStyle.Popup;
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonAdd.FlatStyle = FlatStyle.Flat;
        }

        private void buttonDelete_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Удалить выбранную нозологию", buttonDelete);
            buttonDelete.FlatStyle = FlatStyle.Popup;
        }

        private void buttonDelete_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonDelete.FlatStyle = FlatStyle.Flat;
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show("Редактировать выбранную нозологию", buttonEdit);
            buttonEdit.FlatStyle = FlatStyle.Popup;
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
           CToolTipShower.Hide();
            buttonEdit.FlatStyle = FlatStyle.Flat;
        }

        private void buttonOk_MouseEnter(object sender, EventArgs e)
        {
            CToolTipShower.Show(_hideRemoveAndEditButtons ? "Закрыть форму" : "Подтвердить выбор нозологии", buttonOk);
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
