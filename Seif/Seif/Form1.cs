using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Seif
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		int[] mas = new int[5];
		int cnt;

		private void button1_Click(object sender, EventArgs e)
		{
      // Создание 5 разных цифр
			Random r = new Random();
			for (int i = 0; i < 5; i++)
			{
				int n = r.Next(0, 10);
				bool flag = true;
				for(int j=0; j< i; j++)
					if (mas[j] == n)
					{
						flag = false;
						break;
					}

				if (flag)
					mas[i] = n;
				else
					i--;
			}
			cnt = 0;
      label1.Text = "";// +mas[0].ToString() + mas[1].ToString() + mas[2].ToString() + mas[3].ToString() + mas[4].ToString();
      TableList.Rows.Clear();
      text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = text5.Enabled = button2.Enabled = true;
			text1.Focus();      
		}

		private void button2_Click(object sender, EventArgs e)
		{
      int[] newMas = new int[0];
      try
      {
        newMas = new int[5] { Convert.ToInt32(text1.Text), Convert.ToInt32(text2.Text), 
			  Convert.ToInt32(text3.Text), Convert.ToInt32(text4.Text), Convert.ToInt32(text5.Text)};
      }
      catch
      {
        MessageBox.Show("Ошибка в записи номеров. Повторите ввод");
        return;
      }

			int right = 0, concur = 0;
			for (int i = 0; i < 5; i++)
			{
				if (mas[i] == newMas[i])
					right++;

				for (int j = 0; j < 5; j++)
					if (newMas[j] == mas[i])
						concur++;
			}

			string[] param = new string[7] { text1.Text, text2.Text, text3.Text, text4.Text, text5.Text,
			concur.ToString(), right.ToString() };

			cnt++;
			TableList.Rows.Add(param);
      TableList.Rows[TableList.Rows.Count - 1].Selected = true;

			if (right == 5)
			{
				MessageBox.Show("Вы выиграли");
				label1.Text = "Вы угадали шифр за " + cnt.ToString();
        while (cnt > 10)
          cnt /= 10;
        string att = " попыток";
        if(cnt == 1)
          att = " попытку";
        else if (cnt > 1 && cnt < 5)
          att = " попытки";
        label1.Text += att;

        text1.Enabled = text2.Enabled = text3.Enabled = text4.Enabled = text5.Enabled = button2.Enabled = false;
        button1.Focus();
			}
			text1.Text = text2.Text = text3.Text = text4.Text = text5.Text = "";
			text1.Focus();
		}

		private void text1_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (Convert.ToInt32(text1.Text) >= 0 && Convert.ToInt32(text1.Text) < 10)
					text2.Focus();
			}
			catch { }
		}

		private void text2_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (Convert.ToInt32(text2.Text) >= 0 && Convert.ToInt32(text2.Text) < 10)
					text3.Focus();
			}
			catch { }
		}

		private void text3_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (Convert.ToInt32(text3.Text) >= 0 && Convert.ToInt32(text3.Text) < 10)
					text4.Focus();
			}
			catch { }
		}

		private void text4_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (Convert.ToInt32(text4.Text) >= 0 && Convert.ToInt32(text4.Text) < 10)
					text5.Focus();
			}
			catch { }
		}

		private void text5_TextChanged(object sender, EventArgs e)
		{
			try
			{
				if (Convert.ToInt32(text4.Text) >= 0 && Convert.ToInt32(text4.Text) < 10)
					button2.Focus();
			}
			catch { }
		}

    private void button3_Click(object sender, EventArgs e)
    {
      RuleForm ruleForm = new RuleForm();
      ruleForm.ShowDialog();
    }
	}
}