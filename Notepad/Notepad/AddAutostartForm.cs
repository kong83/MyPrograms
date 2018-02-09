using System;
using System.Windows.Forms;

using Microsoft.Win32;

namespace Notepad
{
    public partial class AddAutostartForm : Form
    {
        private readonly string mRegPath;

        public AddAutostartForm(string regPath)
        {
            InitializeComponent();
            mRegPath = regPath;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            RegistryKey regKey = Registry.LocalMachine;
            regKey = regKey.OpenSubKey(mRegPath, true);

            regKey.SetValue(textBoxName.Text, textBoxPath.Text);

            regKey.Close();
            Close();
        }

        private void buttonSelectPath_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = openFileDialog1.FileName;
            }
        }
    }
}
