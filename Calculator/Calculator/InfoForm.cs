using System;
using System.Windows.Forms;

namespace Calculator
{
  public partial class InfoForm : Form
  {
    public InfoForm()
    {
      InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}