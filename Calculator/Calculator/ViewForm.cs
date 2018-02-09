using System;
using System.Windows.Forms;

namespace Calculator
{
	public partial class ViewForm : Form
	{
		public ViewForm()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void button1_MouseEnter(object sender, EventArgs e)
		{
			toolTip1.Show("Закрыть", button1, 15, -17);
		}

		private void button1_MouseLeave(object sender, EventArgs e)
		{
			toolTip1.Hide(button1);
		}
	}
}