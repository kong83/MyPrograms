using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace SHRegistrationTool
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            string inputData = textBoxInputData.Text.TrimEnd(new[] { '\r', '\n' });
            
            textBoxPassword.Text = GeneratePassword(inputData);
        }

        private static string GeneratePassword(string inputData)
        {
            var sr = new StringBuilder();
            foreach (byte b in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes("SecretWord" + inputData + "Some Numbers 123")))
            {
                sr.Append(b.ToString("x2"));
            }

            return sr.ToString();
        }
    }
}
