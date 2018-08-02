using System;
using System.Windows.Forms;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;
using MessageBox = SurgeryHelper.Tools.MessageBox;

namespace SurgeryHelper.Forms
{
    public partial class OperationTypeViewForm : Form
    {
        private readonly COperationType _operationTypeInfo;
        private readonly COperationTypeWorker _operationTypeWorker;
        private readonly COperationWorker _operationWorker;
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

        public OperationTypeViewForm(CWorkersKeeper workersKeeper, COperationType operationTypeInfo)
            : this(workersKeeper, operationTypeInfo, string.Empty)
        {
        }

        public OperationTypeViewForm(CWorkersKeeper workersKeeper, COperationType operationTypeInfo, string defaultParentFolderName)
        {
            InitializeComponent();
            comboBoxNodeType.SelectedIndex = 0;
            _operationTypeWorker = workersKeeper.OperationTypeWorker;
            _operationWorker = workersKeeper.OperationWorker;

            if (operationTypeInfo == null)
            {
                _operationTypeInfo = new COperationType();
                Text = "Добавление нового типа операции";
                AddAllParentTypesToComboBox(string.Empty);
                comboBoxParentNodeName.Text = defaultParentFolderName;
            }
            else
            {
                _operationTypeInfo = new COperationType(operationTypeInfo);
                Text = "Редактирование типа операции";
                textBoxOperationTypeName.Text = _operationTypeInfo.Name;

                AddAllParentTypesToComboBox(_operationTypeInfo.Name);

                if (_operationTypeInfo.Type == NodeType.Folder)
                {
                    comboBoxNodeType.SelectedIndex = 1;
                }

                comboBoxNodeType.Enabled = false;

                if (_operationTypeInfo.IdParent != -1)
                {
                    COperationType operationType = _operationTypeWorker.GetById(_operationTypeInfo.IdParent);
                    comboBoxParentNodeName.Text = operationType.Name;
                }
            }            
        }


        /// <summary>
        /// Добавить список возможных папок для выбора родительской папки
        /// </summary>
        /// <param name="excludeOperationTypeName">Тип операции, который не может быть родителем</param>
        private void AddAllParentTypesToComboBox(string excludeOperationTypeName)
        {
            comboBoxParentNodeName.Items.Clear();
            comboBoxParentNodeName.Items.Add("Корневая папка");

            foreach (COperationType operationType in _operationTypeWorker.OperationTypeList)
            {
                if (operationType.Type == NodeType.Folder && operationType.Name != excludeOperationTypeName)
                {
                    comboBoxParentNodeName.Items.Add(operationType.Name);
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
            if (string.IsNullOrEmpty(textBoxOperationTypeName.Text))
            {
                MessageBox.Show("Поля, отмеченные звёздочкой, обязательны для заполнения", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string oldOperationTypeName = _operationTypeInfo.Name;
                _operationTypeInfo.Name = textBoxOperationTypeName.Text;
                _operationTypeInfo.Type = comboBoxNodeType.Text == "Папка" ? NodeType.Folder : NodeType.Type;
                
                if (comboBoxParentNodeName.Text == "Корневая папка")
                {
                    _operationTypeInfo.IdParent = -1;
                }
                else
                {
                    COperationType operationType = _operationTypeWorker.GetByGeneralData(comboBoxParentNodeName.Text);
                    _operationTypeInfo.IdParent = operationType.Id;
                }

                if (_operationTypeInfo.Id == 0)
                {
                    _operationTypeWorker.Add(_operationTypeInfo);
                }
                else
                {
                    _operationTypeWorker.Update(_operationTypeInfo);
                    _operationWorker.ChangeOperationType(oldOperationTypeName, _operationTypeInfo.Name);
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
        private void OperationTypeViewForm_FormClosing(object sender, FormClosingEventArgs e)
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
