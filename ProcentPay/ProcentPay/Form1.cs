using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProcentPay
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Проверка суммы и процента
		/// </summary>
		/// <returns></returns>
		private bool CheckData()
		{
			double cntSum = 0;
			try
			{
				cntSum = Convert.ToDouble(textSumm.Text);
				if (cntSum < 0 || cntSum > 1000000000)
				{
					MessageBox.Show("Сумма должна быть от 0 до 1000000000");
					textSumm.Focus();
					return false;
				}
			}
			catch
			{
				MessageBox.Show("Неверная сумма");
				textSumm.Focus();
				return false;
			}

			double cntProcent = 0;
			try
			{
				cntProcent = Convert.ToInt32(textProcent.Text);
				if (cntProcent < 0 || cntProcent > 100)
				{
					MessageBox.Show("Процент должен быть от 0 до 100");
					textProcent.Focus();
					return false;
				}				
			}
			catch
			{
				MessageBox.Show("Неверный процент");
				textProcent.Focus();
				return false;
			}
			return true;
		}


		// Подсчёт суммы
		private void button1_Click(object sender, EventArgs e)
		{
			if (!CheckData())
				return;

			double cntSum = Convert.ToDouble(textSumm.Text);
      double cntProcent = (Convert.ToDouble(textProcent.Text) / 12) / 100.0;
			int cntMonth = 0;
			try
			{
				cntMonth = Convert.ToInt32(textMonth.Text);
				if(cntMonth < 0 || cntMonth > 10000)
				{
					MessageBox.Show("Количество месяцев должно быть от 0 до 10000");
					textMonth.Focus();
					return;
				}
			}
			catch
			{
				MessageBox.Show("Неверное количество месяцев");
				textMonth.Focus();
				return;
			}

			double rez = cntSum;
			for (int i = 0; i < cntMonth; i++)
				rez += rez * cntProcent;

			textResult1.Text = ((int)rez).ToString();
		}

		// Подсчёт кол-ва месяцев для погашения
		private void button2_Click(object sender, EventArgs e)
		{
			if (!CheckData())
				return;

			double cntSum = Convert.ToDouble(textSumm.Text);
			double cntProcent = (Convert.ToDouble(textProcent.Text) / 12) / 100.0;
			double cntPay = 0;
			try
			{
				cntPay = Convert.ToDouble(textPay.Text);
				if (cntPay - cntSum * cntProcent < 1 || cntPay > cntSum)
				{
					MessageBox.Show("Размер ежемесячной выплаты должен быть от " + Convert.ToString(cntSum * cntProcent) + " до " + cntSum.ToString());
					textPay.Focus();
					return;
				}
			}
			catch
			{
				MessageBox.Show("Неверный размер ежемесячной выплаты");
				textPay.Focus();
				return;
			}

			int cnt = 0;
      double sumAdd = 0;

			while (cntSum > 0)
			{
        sumAdd += cntSum * cntProcent;
				cntSum += cntSum * cntProcent - cntPay;
				cnt++;
			}

			textResult2.Text = cnt.ToString();
      textPayAdd.Text = ((int)sumAdd).ToString("");
		}

		private void textPay_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
				button2_Click(null, null);
		}

		private void textMonth_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13)
				button1_Click(null, null);
		}
	}
}