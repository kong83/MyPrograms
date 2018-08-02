using System.Windows.Forms;

namespace ApprendreLeFrançais
{
    public partial class DictionaryNameForm : Form
    {
        private readonly MainForm _mainForm;

        public DictionaryNameForm(MainForm mainForm)
            : this(mainForm, string.Empty)
        {

        }
        public DictionaryNameForm(MainForm mainForm, string currentName)
        {
            InitializeComponent();

            _mainForm = mainForm;
            textBoxTabName.Text = currentName;
            textBoxTabName.Focus();
        }

        private void textBoxTabName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                buttonOk_Click(null, null);
            }

            if (e.KeyChar == 27)
            {
                Close();
            }
        }

        private void DictionaryNameForm_Shown(object sender, System.EventArgs e)
        {
            Left = _mainForm.Left + 318;
            Top = _mainForm.Top + 70;       
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            _mainForm.SetNewDictionaryName(textBoxTabName.Text);
            Close();
        }
    }
}
