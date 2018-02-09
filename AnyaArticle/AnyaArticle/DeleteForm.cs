using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace AnyaArticle
{
    public partial class DeleteForm : Form
    {
        private Form1 copyF;

        public DeleteForm(Form1 f1)
        {
            InitializeComponent();
            copyF = f1;
            DataGridViewRowCollection drc = copyF.ArticleList.Rows;
            DataGridViewRow dr = drc[copyF.ArticleList.CurrentCellAddress.Y];

            textBox1.Text = dr.Cells[0].Value.ToString();
            textBox1.SelectionStart = textBox1.Text.Length;
        }

        // �����

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // �������� ������

        private void button1_Click(object sender, EventArgs e)
        {
            ArrayList mas = new ArrayList();
            ArrayList minus = new ArrayList();

            string s = textBox1.Text;
            string sOK = "";

            int a, b, n;

            // �������� �������� � �������� �� ���������� ������� (�����, ������ � �������)
            for (int i = 0; i < s.Length; i++)
            { // - = 45
                // , = 44
                //   = 32
                // 0 = 48
                // 9 = 57				
                n = s[i];
                if (n == 32)
                    continue;
                if ((n >= 48 && n <= 57) || n == 45 || n == 44)
                    sOK += s[i];
                else
                {
                    MessageBox.Show("������ � ������ ������� �����.");
                    return;
                }
            }

            // ������ ����� ������ � ������������ �������� � �������� ��������� �����
            s = sOK;
            n = 0;
            while (n < s.Length)
            {
                // �������� ������ ����� �� �������
                if ((int)s[n] < 48 || (int)s[n] > 57)
                {
                    MessageBox.Show("������ � ������ ������� �����.");
                    return;
                }
                sOK = "";
                while (n < s.Length && (int)s[n] >= 48 && (int)s[n] <= 57)
                {
                    sOK += s[n];
                    n++;
                }

                // ��������� ��� �� ������������
                try
                {
                    Convert.ToInt32(sOK);
                }
                catch
                {
                    MessageBox.Show("������ � ������ ������ ������.");
                    return;
                }

                // �������, ���� ��� ��������� ����� - �� ����������� ������
                if (n == s.Length)
                {
                    mas.Add(sOK);
                    break;
                }

                // ���� ����� ����� ���� ������� - �� �������� ��������� ����� ����, 
                // �� ��������� �������
                if ((int)s[n] == 44)
                {
                    mas.Add(sOK);
                    n++;
                    if (n == s.Length)
                    {
                        MessageBox.Show("������ � ������ ������� �����.");
                        return;
                    }
                    continue;
                }

                // ���� ����� �� ���� - ������ ����� ����� ������ ���� �����. 
                // �������� ����� ����� ������
                minus.Add(sOK);
                n++;
                if (n == s.Length || (int)s[n] < 48 || (int)s[n] > 57)
                {
                    MessageBox.Show("������ � ������ ������� �����.");
                    return;
                }
                sOK = "";
                while (n < s.Length && (int)s[n] >= 48 && (int)s[n] <= 57)
                {
                    sOK += s[n];
                    n++;
                }

                // ��������� ��� �� ������������
                try
                {
                    Convert.ToInt32(sOK);
                    minus.Add(sOK);
                }
                catch
                {
                    MessageBox.Show("������ � ������ ������� �����.");
                    return;
                }
                a = Convert.ToInt32((string)minus[minus.Count - 2]);
                b = Convert.ToInt32((string)minus[minus.Count - 1]);
                if (a > b)
                {
                    MessageBox.Show("������ � ������ ������� �����.");
                    return;
                }


                // �������, ����� ����� ����� ���� ������� ��� ����� ������
                if (n == s.Length)
                    break;

                if ((int)s[n] != 44)
                {
                    MessageBox.Show("������ � ������ ������� �����.");
                    return;
                }
                n++;
            }

            // �������� ������ ����� �� ������� minus

            n = 0;
            while (n < minus.Count)
            {
                a = Convert.ToInt32((string)minus[n]);
                b = Convert.ToInt32((string)minus[n + 1]);
                for (int i = a; i <= b; i++)
                    mas.Add(i.ToString());
                n += 2;
            }

            // ��������� ������ � ������� �����������
            string[] masString = new string[mas.Count];
            mas.CopyTo(masString);
            Array.Sort(masString);

            for (int j = masString.Length - 1; j >= 0; j--)
            {
                DataRowCollection Rows = copyF.dataSet1.Tables[0].Rows;
                for (int i = 0; i < copyF.dataSet1.Tables[0].Rows.Count; i++)
                {
                    DataRow CurrRow = Rows[i];
                    try
                    {
                        if (CurrRow.RowState == DataRowState.Deleted)
                            continue;

                        if (Convert.ToInt32(CurrRow[0]) == Convert.ToInt32(masString[j]))
                        {
                            copyF.dataSet1.Tables[0].Rows[i].Delete();
                            copyF.save = false;
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("���������� ������� ������ �" + masString[j] + ".\r\n������: " + ex.Message + "������� ������ 'Ok' � ��������� �������.", "������");
                        return;
                    }
                }
            }
            this.Close();
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("��������� ������ ����: 1-5,10,13,7-8,16-20", this.textBox1, 15, -17);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.textBox1);
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("������� ��������� ������", this.button1, 15, -17);
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button1);
        }

        private void button4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("������� ����", this.button4, 15, -17);
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(this.button4);
        }
    }
}