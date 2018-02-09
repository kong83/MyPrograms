using System;
using System.IO;
using System.Windows.Forms;

namespace Notepad
{
    public partial class AttributesForm : Form
    {
        public AttributesForm(string path)
        {
            InitializeComponent();

            textBoxPath.Text = path;

            var fi = new FileInfo(path);
            if (fi.Attributes.ToString().Contains(FileAttributes.Archive.ToString()))
            {
                checkBoxArchive.Checked = true;
            }

            if (fi.Attributes.ToString().Contains(FileAttributes.Hidden.ToString()))
            {
                checkBoxHidden.Checked = true;
            }

            if (fi.Attributes.ToString().Contains(FileAttributes.System.ToString()))
            {
                checkBoxSystem.Checked = true;
            }

            if (fi.Attributes.ToString().Contains(FileAttributes.ReadOnly.ToString()))
            {
                checkBoxReadOnly.Checked = true;
            }
        }


        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Ок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            var fi = new FileInfo(textBoxPath.Text) 
            {
                Attributes = FileAttributes.Normal
            };

            if (checkBoxReadOnly.Checked)
            {
                fi.Attributes = fi.Attributes | FileAttributes.ReadOnly; 
            }

            if (checkBoxHidden.Checked)
            {
                fi.Attributes = fi.Attributes | FileAttributes.Hidden;
            }

            if (checkBoxSystem.Checked)
            {
                fi.Attributes = fi.Attributes | FileAttributes.System;
            }

            if (checkBoxArchive.Checked)
            {
                fi.Attributes = fi.Attributes | FileAttributes.Archive;
            }
            Close();
        }
    }
}
