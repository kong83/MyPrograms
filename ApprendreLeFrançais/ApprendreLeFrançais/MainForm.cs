using System;
using System.Linq;
using System.Windows.Forms;

namespace ApprendreLeFrançais
{
    public partial class MainForm : Form
    {
        private readonly Wordbook _wordbook;
        private readonly ParametersEngine _parametersEngine;

        private Parameters _parameters;
        private bool _stopSavingParameters;

        private Control _currentControl;
        private int _cursorPosition;
        private int _columnIndex, _rowIndex;

        private string _newDictionaryName;

        public MainForm()
        {
            _stopSavingParameters = true;

            InitializeComponent();
            
            _wordbook = new Wordbook();
            _parametersEngine = new ParametersEngine();
        }

        public void SetNewDictionaryName(string newName)
        {
            _newDictionaryName = newName;
        }


        private void MainForm_Shown(object sender, EventArgs e)
        {
            tabControlDictionary.TabPages.Clear();
            foreach (string tabName in _wordbook.GetAllTabs())
            {
                tabControlDictionary.TabPages.Add(tabName);
            }

            tabControlDictionary.SelectedIndex = 0;
            _wordbook.ShowTab(tabControlDictionary.TabPages[0].Text, WordsList);
            
            _parameters = _parametersEngine.LoadMainFormParameters("415;631", "200;100", "130");
            this.Size = _parameters.FormSize;
            if (_parameters.FormLocation.X >= 0 && _parameters.FormLocation.Y >= 0 &&
                _parameters.FormLocation.X < Screen.FromControl(this).Bounds.Width - 10 && _parameters.FormLocation.Y < Screen.FromControl(this).Bounds.Height - 10)
            {
                this.Location = _parameters.FormLocation;
            }
            
            for (int i = 0; i < _parameters.ListColumnsWidth.Length; i++)
            {
                WordsList.Columns[i].Width = _parameters.ListColumnsWidth[i];
            }            

            _stopSavingParameters = false;
        }

        private void frenchLetters_PressLetterEvent(object sender, FrenchLetters.PressLetterEventArgs letter)
        {
            if (_currentControl == null || string.IsNullOrEmpty(_currentControl.Name))
            {
                bool isNewColumn = WordsList.SelectedCells[0].RowIndex != _rowIndex || WordsList.SelectedCells[0].ColumnIndex != _columnIndex;

                WordsList.BeginEdit(false);

                if (isNewColumn)
                {
                    _cursorPosition = 0;
                    _currentControl.Text = string.Empty;
                }
            }

            string newText = _currentControl.Text.Substring(0, _cursorPosition) + letter.Text + _currentControl.Text.Substring(_cursorPosition);
            _currentControl.Text = newText;
            _currentControl.Focus();
            ((TextBox)_currentControl).SelectionStart = _cursorPosition + 1;
            ((TextBox)_currentControl).SelectionLength = 0;
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            _currentControl = (Control)sender;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            _cursorPosition = ((TextBox)sender).SelectionStart;
        }

        private void WordsList_Enter(object sender, EventArgs e)
        {
            _currentControl = null;
        }

        private void WordsList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (WordsList.CurrentCell.EditType == typeof(DataGridViewTextBoxEditingControl) && WordsList.SelectedCells.Count > 0)
            {
                _currentControl = WordsList.EditingControl;
                _columnIndex = WordsList.SelectedCells[0].ColumnIndex;
                _rowIndex = WordsList.SelectedCells[0].RowIndex;
            }
        }

        private void WordsList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            _cursorPosition = ((TextBox)_currentControl).SelectionStart;
            SaveCurrentWords();
        }

        private bool IsTabHasUniqName()
        {
            return tabControlDictionary.TabPages.Cast<TabPage>().All(tab => tab.Text != _newDictionaryName);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _newDictionaryName = string.Empty;
            new DictionaryNameForm(this).ShowDialog();
            if (string.IsNullOrEmpty(_newDictionaryName))
            {
                return;
            }

            if (!IsTabHasUniqName())
            {
                MessageBox.Show("Закладка с таким именем уже существует. Имя закладки должно быть уникальным.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _wordbook.AddTab(_newDictionaryName);

            tabControlDictionary.TabPages.Add(_newDictionaryName);
            tabControlDictionary.SelectedIndex = tabControlDictionary.TabPages.Count - 1;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            int currentIndex = tabControlDictionary.SelectedIndex;
            if (currentIndex < 0)
            {
                return;
            }

            if (DialogResult.No == MessageBox.Show("Вы уверены, что хотите удалить текущую вкладку?\r\nВсе слова на этой вкладке также будут удалены безвозвратно.", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                return;
            }

            string tabName = tabControlDictionary.SelectedTab.Text;
            tabControlDictionary.TabPages.RemoveAt(currentIndex);
            tabControlDictionary.SelectedIndex = Math.Min(tabControlDictionary.TabPages.Count - 1, currentIndex);

            _wordbook.DeleteTab(tabName);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            int curentIndex = tabControlDictionary.SelectedIndex;
            if (curentIndex < 0)
            {
                return;
            }

            string tabName = tabControlDictionary.SelectedTab.Text;
            _newDictionaryName = string.Empty;
            new DictionaryNameForm(this, tabControlDictionary.TabPages[curentIndex].Text).ShowDialog();
            if (tabName == _newDictionaryName || string.IsNullOrEmpty(_newDictionaryName))
            {
                return;
            }

            if (!IsTabHasUniqName())
            {
                MessageBox.Show("Закладка с таким именем уже существует. Имя закладки должно быть уникальным.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _wordbook.RenameTab(tabName, _newDictionaryName);
            tabControlDictionary.TabPages[curentIndex].Text = _newDictionaryName;
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {
            if (tabControlDictionary.TabPages.Count <= 1)
            {
                MessageBox.Show("Нет закладок для копирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int curentIndex = tabControlDictionary.SelectedIndex;
            if (curentIndex < 0)
            {
                return;
            }

            string tabName = tabControlDictionary.SelectedTab.Text;
            _newDictionaryName = string.Empty;
            new DictionaryNameForm(this, tabControlDictionary.TabPages[0].Text).ShowDialog();
            if (tabName == _newDictionaryName || string.IsNullOrEmpty(_newDictionaryName))
            {
                return;
            }

            if (IsTabHasUniqName())
            {
                MessageBox.Show("Указанная закладка не найдена.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _wordbook.CopyTab(tabName, _newDictionaryName);
        }

        private void TimesList_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            textBoxSearchRussion.Left = WordsList.Left + 1;
            textBoxSearchRussion.Width = WordsList.Columns[0].Width - 2;

            textBoxSearchFrench.Left = textBoxSearchRussion.Left + textBoxSearchRussion.Width + 3;
            textBoxSearchFrench.Width = WordsList.Columns[1].Width - 2;

            textBoxSearchRussion.Top = textBoxSearchFrench.Top = this.Height - 70;

            if (_stopSavingParameters)
            {
                return;
            }

            _parameters.ListColumnsWidth[0] = WordsList.Columns[0].Width;
            _parametersEngine.SaveMainFormParameters(_parameters);
        }

        private void RemoveSelectedRow()
        {
            int saveRowIndex = WordsList.SelectedCells[0].RowIndex;
            if (WordsList.Rows[saveRowIndex].IsNewRow)
            {
                return;
            }

            WordsList.Rows.RemoveAt(saveRowIndex);
            WordsList.Rows[saveRowIndex].Cells[0].Selected = true;

            SaveCurrentWords();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RemoveSelectedRow();
        }

        private void WordsList_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
            {
                RemoveSelectedRow();
            }
        }

        private void WordsList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex > -1)
            {
                WordsList.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
            }
            else if (e.Button == MouseButtons.Left && e.RowIndex == -1)
            {
                timer.Enabled = true;
            }
        }

        private void WordsList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip.Show(Left + WordsList.Left + e.X, Top + WordsList.Top + e.Y);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;

            SaveCurrentWords();
        }

        private void SaveCurrentWords()
        {
            _wordbook.FillTab(tabControlDictionary.SelectedTab.Text, WordsList);
        }

        private void tabControlDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlDictionary.SelectedTab != null)
            {
                _wordbook.ShowTab(tabControlDictionary.SelectedTab.Text, WordsList);
            }
        }

        private void FilterRows()
        {
            bool isREmpty = string.IsNullOrEmpty(textBoxSearchRussion.Text);
            bool isFEmpty = string.IsNullOrEmpty(textBoxSearchFrench.Text);
            for (int i = 0; i < WordsList.Rows.Count; i++)
            {
                if (WordsList.Rows[i].IsNewRow)
                {
                    continue;
                }

                if ((WordsList.Rows[i].Cells[0].Value == null && !isREmpty) ||
                    (WordsList.Rows[i].Cells[1].Value == null && !isFEmpty) ||
                    (!isREmpty && !WordsList.Rows[i].Cells[0].Value.ToString().Contains(textBoxSearchRussion.Text)) ||
                    (!isFEmpty && !WordsList.Rows[i].Cells[1].Value.ToString().Contains(textBoxSearchFrench.Text)))
                {
                    WordsList.Rows[i].Visible = false;
                }
                else
                {
                    WordsList.Rows[i].Visible = true;
                }
            }
        }

        private void textBoxSearchRussion_TextChanged(object sender, EventArgs e)
        {
            FilterRows();
        }

        private void textBoxSearchFrench_TextChanged(object sender, EventArgs e)
        {
            FilterRows();
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            new TestForm(_wordbook.GetAllWordsByTab(tabControlDictionary.SelectedTab.Text, radioButtonFtoR.Checked), radioButtonFtoR.Checked).Show();
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            WordsList.Focus();
        }

        #region Подсказки
        private void buttonAdd_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Добавить новую вкладку", buttonAdd, -20, -20);
        }

        private void buttonAdd_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonAdd);
        }

        private void buttonRemove_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Удалить текущую вкладку", buttonRemove, -20, -20);
        }

        private void buttonRemove_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonRemove);
        }

        private void buttonEdit_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Изменить имя текущей вкладки", buttonEdit, -20, -20);
        }

        private void buttonEdit_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonEdit);
        }

        private void buttonCopy_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Скопировать текущую вкладку в другую", buttonCopy, -20, -20);
        }

        private void buttonCopy_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonCopy);
        }

        private void buttonTest_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Начать тестирование для выбранной закладки", buttonTest, -20, -20);
        }

        private void buttonTest_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonTest);
        }
        #endregion        

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (_stopSavingParameters)
            {
                return;
            }

            _parameters.FormSize = this.Size;
            _parametersEngine.SaveMainFormParameters(_parameters);
        }

        private void MainForm_LocationChanged(object sender, EventArgs e)
        {
            if (_stopSavingParameters || (this.Location.X <= 0 && this.Location.Y <= 0))
            {
                return;
            }

            _parameters.FormLocation = this.Location;
            _parametersEngine.SaveMainFormParameters(_parameters);
        }
    }
}
