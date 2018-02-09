using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnyaArticle
{
    public partial class EditForm : Form
    {
        Form1 copyF;
        public EditForm(Form1 f)
        {
            InitializeComponent();
            copyF = f;

            DataGridViewRowCollection drc = copyF.ArticleList.Rows;
            DataGridViewRow dr = drc[copyF.ArticleList.CurrentCellAddress.Y];

            textBox1.Text = dr.Cells[1].Value.ToString();
            textBox2.Text = dr.Cells[2].Value.ToString();
            textBox3.Text = dr.Cells[3].Value.ToString();
            if (dr.Cells[4].Value.ToString().Equals("E"))
                comboBox1.Text = "Английский";
            else
                comboBox1.Text = "Русский";
            textBox4.Text = dr.Cells[5].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dr.Cells[6].Value);
            richTextBox1.Text = dr.Cells[7].Value.ToString();
        }

        // Кнопка выхода
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Кнопка "изменить"
        private void button1_Click(object sender, EventArgs e)
        {
            copyF.dataSet1.Tables[0].Columns[4].ReadOnly = false;
            copyF.dataSet1.Tables[0].Columns[6].ReadOnly = false;
            DataGridViewRowCollection drc = copyF.ArticleList.Rows;
            DataGridViewRow dr = drc[copyF.ArticleList.CurrentCellAddress.Y];

            dr.Cells[1].Value = textBox1.Text;
            dr.Cells[2].Value = textBox2.Text;
            dr.Cells[3].Value = textBox3.Text;
            if (comboBox1.Text.Equals("Английский"))
                dr.Cells[4].Value = 'E';
            else
                dr.Cells[4].Value = 'R';
            dr.Cells[5].Value = textBox4.Text;
            dr.Cells[6].Value = dateTimePicker1.Value;
            dr.Cells[7].Value = richTextBox1.Text;
            copyF.dataSet1.Tables[0].Columns[4].ReadOnly = true;
            copyF.dataSet1.Tables[0].Columns[6].ReadOnly = true;
            copyF.save = false;
            this.Close();
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
            toolTip1.Show("Сохранить изменения", this.button1, 15, -17);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath fp = new FilePath();

                textBox4.Text = fp.GetPath(openFileDialog1.FileName, openFileDialog1.InitialDirectory);
            }
        }
    }
}