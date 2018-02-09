using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using ClassTransmittor;
using System.Threading;

namespace ProbaServer
{
	public partial class Form1 : Form
	{
		private Transmittor trans;																	// Remote object to instantiate here		
		private Transmittor trans1;
		private delegate void SetFormChangeDelegate(string str);		// For updating label

		public Form1()
		{
			InitializeComponent();

			// Register our tcp channel
			ChannelServices.RegisterChannel(new TcpChannel(50051), false);// 8080 - для подключения по сети

			// Register an object created by the server
			trans = new Transmittor();
			ObjRef refTrans = RemotingServices.Marshal(trans, "Transmittor");			

			// Обработка события на запрос получения времени
			trans.GetTimeEvent += new Transmittor.GetTimeEventHandler(Server_GetTime);

			// Обработка события на запрос получения списка времён
			trans.GetTimeArrayEvent += new Transmittor.GetTimeArrayEventHandler(Server_GetTimeArray);

			// Обработка события на запрос получения списка времён			
			trans.GetFromClientNumberEvent += new Transmittor.GetFromClientNumberEventHandler(Client_GetFromClientNumber);

			label2.Text = DateTime.Now.ToString();
			timer1.Enabled = true;

			trans1 = (Transmittor)Activator.GetObject(typeof(Transmittor), "tcp://localhost:50050/Transmittor");
		}

		private void SetFormChange(string str)
		{			
			textBox1.Text = str;
		}

		public string Server_GetTime()
		{
			this.BeginInvoke(new SetFormChangeDelegate(SetFormChange), new object[] { "Вызов пришел" });				
			return label2.Text;
		}

		private string[] Server_GetTimeArray(int cnt) 
		{
			Thread.Sleep(5000);
			string[] mas = new string[cnt];
			DateTime dt = Convert.ToDateTime(label2.Text);
			DateTime dt2 = dt.AddSeconds(cnt);
			int i = 0;
			while (dt < dt2)
			{
				mas[i++] = dt.ToString();
				dt = dt.AddSeconds(1);
			}
			return mas;			
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			label2.Text = DateTime.Now.ToString();
		}

		private int Raschet(int n)
		{
			if (n % 2 != 0)
				return 0;

			int k = 1;
			for (int i = 0; i < n; i++)
				k *= 10;
			k--;

			int[] chisla = new int[n];
			int cnt = 0;
			for (int i = 1; i <= k; i++)
			{
				for (int j = 0; j < n; j++)
					chisla[j] = 0;

				int q = i;
				int w = n - 1;
				while (q > 0)
				{
					chisla[w] = q % 10;
					w--;
					q = (int)q / 10;
				}

				int s1 = 0, s2 = 0;
				for (int j = 0; j < n / 2; j++)
				{
					s1 += chisla[j];
					s2 += chisla[n / 2 + j];
				}

				if (s1 == s2)
					cnt++;
			}
			return cnt;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				MessageBox.Show(Raschet(Convert.ToInt32(textBox1.Text)).ToString());
			}
			catch
			{ 
				ShowError("Значение неверно. Должно быть чётное число.");
			}

			try
			{
				MessageBox.Show(trans1.GetFromClientNumber(5).ToString());
			}
			catch (Exception ex)
			{
				ShowError("Вызов не прошёл. Возможно, не подсоединён клиент.\n" + ex.Message);
			}
		}

		private void ShowError(string msg)
		{
			MessageBox.Show(this, msg, "Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public int Client_GetFromClientNumber(int i)
		{
			return i * i;
		}
	}
}