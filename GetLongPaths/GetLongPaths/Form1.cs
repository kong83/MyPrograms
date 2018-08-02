using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace GetLongPaths
{
    public partial class Form1 : Form
    {
        private StringBuilder _longPaths;
        private const int MaxLength = 259;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonGetLongPaths_Click(object sender, EventArgs e)
        {
            _longPaths = new StringBuilder();

            FindLongPaths(textBoxRootPath.Text);

            richTextBoxPaths.Text = _longPaths.ToString();
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                textBoxRootPath.Text = openFileDialog.FileName;
            }
        }

        private void FindLongPaths(string path)
        {
            if (path.Length >= 259)
            {
                _longPaths.Append(path.Length + ": " + path + "\r\n");
                return;
            }

            foreach (var filePath in Directory.GetFiles(path))
            {
                if (filePath.Length >= 259)
                {
                    _longPaths.Append(filePath.Length + ": " + filePath + "\r\n");
                }
            }

            foreach (var directoryPath in Directory.GetDirectories(path))
            {
                FindLongPaths(directoryPath);
            }
        }
    }
}
