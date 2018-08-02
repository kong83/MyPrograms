using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SampleCreator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            try
            {
                var sb = new StringBuilder();

                var variables = new List<Variable>();
                string[] values = textBoxSingleNames.Text.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string singleName in values)
                {
                    variables.Add(new Variable(singleName, VariableType.Single, Convert.ToInt32(textBoxSingleFrom.Text), Convert.ToInt32(textBoxSingleTo.Text))); 
                }

                values = textBoxNumericNames.Text.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string numericName in values)
                {
                    variables.Add(new Variable(numericName, VariableType.Numeric, Convert.ToInt32(textBoxNumericFrom.Text), Convert.ToInt32(textBoxNumericTo.Text))); 
                }

                values = textBoxOpenNames.Text.Replace(" ", "").Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string openName in values)
                {
                    variables.Add(new Variable(openName, VariableType.Open, textBoxOpenPrefix.Text, Convert.ToInt32(textBoxOpenFrom.Text))); 
                }                

                int firstNumber = Convert.ToInt32(textBoxFirstNumber.Text);
                int seconndNumber = Convert.ToInt32(textBoxSecondNumber.Text);

                string prefix = textBoxPrefix.Text;
                string firstValue = textBoxFirstValue.Text;
                string secondValue = textBoxSecondValue.Text;

                int maxCnt = Convert.ToInt32(textBoxMaxCntInFile.Text);
                int sepNum = 0;
                int cnt = 0;

                for (int i = firstNumber; i <= seconndNumber; i++)
                {
                    if (cnt == 0)
                    {
                        sb.Append(textBoxFirstColumnName.Text + "\t" + textBoxSecondColumnName.Text + "\t" + textBoxThirdColumnName.Text);
                        foreach (Variable var in variables)
                        {
                            sb.Append("\t" + var.Name);
                        }
                        sb.Append("\r\n");
                    }

                    sb.Append(prefix + i + "\t" + firstValue + "\t" + secondValue);

                    foreach (Variable variable in variables)
                    {
                        sb.Append("\t" + variable.CurrentValue);
                        variable.NextValue();
                    }
                    sb.Append("\r\n");

                    cnt++;
                    if (cnt == maxCnt)
                    {
                        string fileName = textBoxFileName.Text + "_" + sepNum + ".txt";
                        using (var sw = new StreamWriter(fileName, false))
                        {
                            sw.Write(sb.ToString().TrimEnd(new []{'\r', '\n'}));
                        }
                        sepNum++;
                        cnt = 0;
                        sb = new StringBuilder();
                    }
                }

                if (cnt > 0)
                {
                    string fileName = textBoxFileName.Text + "_" + sepNum + ".txt";
                    using (var sw = new StreamWriter(fileName, false))
                    {
                        sw.Write(sb.ToString().TrimEnd(new[] { '\r', '\n' }));
                    }
                }

                MessageBox.Show("Выполнение программы завершено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
