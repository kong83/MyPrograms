using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnyaArticle
{
    public partial class AddForm : Form
    {
        private Form1 copyF;

        public AddForm(Form1 f)
        {
            InitializeComponent();
            copyF = f;
            openFileDialog1.InitialDirectory = Application.StartupPath + "\\Articles";
            comboBox1.Text = "Английский";
        }

        // Кнопка выхода

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Кнопка "Добавить"

        private void button1_Click(object sender, EventArgs e)
        {
            DataRow row = copyF.dataSet1.Tables[0].NewRow();

            row[1] = textBox1.Text;
            row[2] = textBox2.Text;
            row[3] = textBox3.Text;

            if (comboBox1.Text.Equals("Английский"))
                row[4] = 'E';
            else
                row[4] = 'R';

            row[5] = textBox4.Text;
            row[6] = dateTimePicker1.Value;
            row[7] = richTextBox1.Text;

            copyF.dataSet1.Tables[0].Rows.Add(row);
            copyF.save = false;
        }

        // Кнопка выбора пути к файлу

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath fp = new FilePath();

                textBox4.Text = fp.GetPath(openFileDialog1.FileName, openFileDialog1.InitialDirectory);
            }
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Закрыть окно", this.button2, 15, -17);
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button2);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Добавить новую запись", this.button1, 15, -17);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button1);
        }
    }
}