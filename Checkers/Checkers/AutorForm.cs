using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Checkers
{
    public partial class AutorForm : Form
    {
        public AutorForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text.ToLower();
            if (s.Equals("pfzw") || s.Equals("cvtcm") || s.Equals("заяц") || s.Equals("смесь"))
                MessageBox.Show("Сделано для Зайки любящим Зайцем\nЛюблю тебя, моя маленькая Зайка.\n Эту программу я делал, скучая по тебе :)");
            this.Close();
        }
    }
}