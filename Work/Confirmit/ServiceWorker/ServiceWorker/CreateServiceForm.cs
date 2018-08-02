using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServiceWorker
{
    public partial class CreateServiceForm : Form
    {
        public CreateServiceForm()
        {
            InitializeComponent();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            if (!File.Exists(textBoxFilePath.Text))
            {
                MessageBox.Show("File " + textBoxFilePath.Text + " is not exist.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxFilePath.Focus();
                return;
            }

            ServiceEngine.CreateService(textBoxServiceName.Text, textBoxDisplayName.Text, textBoxFilePath.Text, textBoxCommandLine.Text);

            if (checkBoxStartAfterCreate.Checked)
            {
                new ServiceEngine(textBoxServiceName.Text).StartService();
            }
        }
    }
}
