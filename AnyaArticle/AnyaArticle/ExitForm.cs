using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AnyaArticle
{
    public partial class ExitForm : Form
    {
        public ExitForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text.ToLower();
            if (s.Equals("pfzw") || s.Equals("cvtcm") || s.Equals("����") || s.Equals("�����"))
                MessageBox.Show("������� ��� ����� ������� ������\n����� ����, ��� ��������� �����.\n ��������� ���� ���������� � ��������� ���� ������� ������ :)");
            this.Close();
        }
    }
}