using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnyaArticle
{
    public partial class FilterForm : Form
    {
        Form1 copyF;

        public FilterForm(Form1 f1)
        {
            InitializeComponent();
            copyF = f1;
            FilterInfo filterInfo = copyF.filterInfo;
            textBox1.Text = filterInfo.textBox1;
            textBox2.Text = filterInfo.textBox2;
            textBox3.Text = filterInfo.textBox3;
            textBox4.Text = filterInfo.textBox4;
            textBox5.Text = filterInfo.textBox5;
            textBox6.Text = filterInfo.textBox6;
            comboBox1.Text = filterInfo.comboBox1;
        }

        // Закрытие окна фильтрации

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Кнопка фильтрации

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox5.Text))
                {
                    DateTime dt = Convert.ToDateTime(textBox5.Text);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка в записи даты", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
                textBox5.Focus();
                return;
            }

            DataView dv = new DataView(copyF.dataSet1.Tables[0]);

            dv.RowFilter = "";
            if (!textBox1.Text.Equals(""))
                dv.RowFilter += "Name like '%" + textBox1.Text + "%'";

            if (!textBox2.Text.Equals(""))
                if (dv.RowFilter.Equals(""))
                    dv.RowFilter += "Autor like '%" + textBox2.Text + "%'";
                else
                    dv.RowFilter += "and Autor like '%" + textBox2.Text + "%'";

            if (!textBox3.Text.Equals(""))
                if (dv.RowFilter.Equals(""))
                    dv.RowFilter += "Tema like '%" + textBox3.Text + "%'";
                else
                    dv.RowFilter += "and Tema like '%" + textBox3.Text + "%'";

            if (!comboBox1.Text.Equals(""))
                if (dv.RowFilter.Equals(""))
                {
                    if (comboBox1.Text.Equals("Английский"))
                        dv.RowFilter += "Language='E'";
                    else
                        dv.RowFilter += "Language='R'";
                }
                else
                {
                    if (comboBox1.Text.Equals("Английский"))
                        dv.RowFilter += "and Language='E'";
                    else
                        dv.RowFilter += "and Language='R'";
                }

            if (!textBox4.Text.Equals(""))
                if (dv.RowFilter.Equals(""))
                    dv.RowFilter += "Path like '%" + textBox4.Text + "%'";
                else
                    dv.RowFilter += "and Path like '%" + textBox4.Text + "%'";

            if (!textBox5.Text.Equals(""))
                if (dv.RowFilter.Equals(""))
                    dv.RowFilter += "Year ='" + textBox5.Text + "'";
                else
                    dv.RowFilter += "and Year ='" + textBox5.Text + "'";

            if (!textBox6.Text.Equals(""))
                if (dv.RowFilter.Equals(""))
                    dv.RowFilter += "About like '%" + textBox6.Text + "%'";
                else
                    dv.RowFilter += "and About like '%" + textBox6.Text + "%'";

            copyF.ArticleList.DataMember = "";
            copyF.ArticleList.DataSource = dv;
            copyF.ColoringRows();

            FilterInfo filterInfo = new FilterInfo();
            filterInfo.textBox1 = textBox1.Text;
            filterInfo.textBox2 = textBox2.Text;
            filterInfo.textBox3 = textBox3.Text;
            filterInfo.textBox4 = textBox4.Text;
            filterInfo.textBox5 = textBox5.Text;
            filterInfo.textBox6 = textBox6.Text;
            filterInfo.comboBox1 = comboBox1.Text;
            copyF.filterInfo = filterInfo;
        }

        // Кнопка показать все

        private void button3_Click(object sender, EventArgs e)
        {
            DataView dv = new DataView(copyF.dataSet1.Tables[0]);

            dv.RowFilter = "";
            copyF.ArticleList.DataMember = "";
            copyF.ArticleList.DataSource = dv;
            copyF.ColoringRows();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FilePath fp = new FilePath();

                textBox4.Text = fp.GetPath(openFileDialog1.FileName, openFileDialog1.InitialDirectory);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            monthCalendar1.Visible = !monthCalendar1.Visible;
            if (monthCalendar1.Visible)
                button5.FlatStyle = FlatStyle.Popup;
            else
                button5.FlatStyle = FlatStyle.Standard;
        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            textBox5.Text = monthCalendar1.SelectionStart.ToShortDateString();
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            button5_Click(null, null);
        }        
    }
}