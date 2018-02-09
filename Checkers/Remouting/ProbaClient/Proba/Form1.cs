using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;		// for asynchronous callbacks
using ClassTransmittor;  

namespace Proba
{	
	public partial class Form1 : Form
	{
		private Transmittor trans;													// Наш remote-объект				
		private Transmittor trans1;
		public Form1()
		{
			InitializeComponent();			

			// Register our tcp channel			
			ChannelServices.RegisterChannel(new TcpChannel(50050), false);
			
			trans = (Transmittor)Activator.GetObject(typeof(Transmittor), "tcp://localhost:50051/Transmittor");
			// tcp://192.168.1.16:8080/Transmittor - для передачи по сети
		  textBoxResult.Text = "";
			comboAction.SelectedIndex = 0;

			trans1 = new Transmittor();
			ObjRef refTrans = RemotingServices.Marshal(trans1, "Transmittor");			

			// Обработка события на запрос получения времени
			trans1.GetFromClientNumberEvent += new Transmittor.GetFromClientNumberEventHandler(Client_GetFromClientNumber);
		}

		// Синхронный вызов
		private void btnCallSynch_Click(object sender, EventArgs e)
		{
			textBoxResult.Text = "";
			Application.DoEvents();

			try
			{
				if (comboAction.SelectedIndex == 0)
					textBoxResult.Text = trans.GetTime();
				else
					textBoxResult.Lines = trans.GetTimeArray((int)numericUpDown1.Value);				
			}
			catch (Exception ex)
			{
				ShowError("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message);
			}
		}




		private void ShowError(string msg)
		{
			MessageBox.Show(this, msg, "Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void comboAction_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (comboAction.SelectedIndex == 0)
				numericUpDown1.Visible = false;
			else
				numericUpDown1.Visible = true;
		}




		// Асинхоронный вызов
		
		private delegate string GetTimeDelegate();
		private delegate string[] GetTimeArrayDelegate(int cnt);

		private delegate void SetTextBoxDelegate(string str);
		private delegate void SetTextBoxArrayDelegate(string[] str);

		public void SetTextBox(string str)
		{
			textBoxResult.Text = str;
		}

		public void SetTextBoxArray(string[] str)
		{
			textBoxResult.Lines = str;
		}

		// Доплнительная функция для асинхронного вызова
		public void GetTimeCallBack(IAsyncResult ar)
		{
			GetTimeDelegate d = (GetTimeDelegate)((AsyncResult)ar).AsyncDelegate;

			try
			{
				string str = d.EndInvoke(ar);
				this.BeginInvoke(new SetTextBoxDelegate(SetTextBox), new object[] {str});
			}
			catch (Exception ex)
			{
				ShowError("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message);
			}
		}

		// Доплнительная функция для асинхронного вызова
		public void GetTimeArrayCallBack(IAsyncResult ar)
		{
			GetTimeArrayDelegate d = (GetTimeArrayDelegate)((AsyncResult)ar).AsyncDelegate;

			try
			{
				string[] str = d.EndInvoke(ar);
				this.BeginInvoke(new SetTextBoxArrayDelegate(SetTextBoxArray), new object[] { str });
			}
			catch (Exception ex)
			{
				ShowError("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message);
			}
		}

		// Асинхронный вызов
		private void btnCallAsynch_Click(object sender, EventArgs e)
		{
			textBoxResult.Text = "";
			Application.DoEvents();

			if (comboAction.SelectedIndex == 0)
			{
				AsyncCallback cb = new AsyncCallback(this.GetTimeCallBack);
				GetTimeDelegate d = new GetTimeDelegate(trans.GetTime);
				IAsyncResult ar = d.BeginInvoke(cb, null);
			}
			else
			{
				AsyncCallback cb = new AsyncCallback(this.GetTimeArrayCallBack);
				GetTimeArrayDelegate d = new GetTimeArrayDelegate(trans.GetTimeArray);
				IAsyncResult ar = d.BeginInvoke((int)numericUpDown1.Value, cb, null);
			}
		}


		public int Client_GetFromClientNumber(int i)
		{			
			return i * i;
		}
	}
}