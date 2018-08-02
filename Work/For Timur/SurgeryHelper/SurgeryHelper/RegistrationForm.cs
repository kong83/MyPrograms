using System;
using System.Windows.Forms;
using SurgeryHelper.Engines;

namespace SurgeryHelper
{
    internal partial class RegistrationForm : Form
    {
        private readonly MasterKeyEngine _masterKey;

        internal RegistrationForm(MasterKeyEngine masterKey)
        {
            InitializeComponent();

            _masterKey = masterKey;
        }

        private void RegistrationForm_Shown(object sender, EventArgs e)
        {
            textBoxInputData.Text = _masterKey.GetInfoToGenerateMasterKey();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            _masterKey.CreateMasterKeyFile(textBoxPassword.Text);

            MessageBox.Show("Регистрация успешно завершена.", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Close();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            buttonEnter.Enabled = _masterKey.HashInfoHardDisks == textBoxPassword.Text;
        }
    }
}
