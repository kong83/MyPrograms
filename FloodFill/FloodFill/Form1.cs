using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace FloodFill
{
  public partial class Form1 : Form
  {    	
	   
    public Form1()
    {
      InitializeComponent();      
    }

    private void panel1_MouseUp(object sender, MouseEventArgs e)
    {
      MapFill m = new MapFill();
      Graphics g = panel1.CreateGraphics();

      if (e.X < 0 && e.Y < 0)
        return;
      m.Fill(g, new Point(e.X, e.Y), button1.BackColor, ref b);
      
      g.DrawImage(b, 0, 0);
      g.Dispose();
    }

    private Bitmap b;

    private void Form1_Load(object sender, EventArgs e)
    {
      b = new Bitmap(panel1.Width, panel1.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
      Graphics g = Graphics.FromImage(b);
      g.FillRectangle(new SolidBrush(Color.Yellow), 0, 0, panel1.Width-1, panel1.Height-1);
      g.DrawRectangle(new Pen(Color.Black), 0, 0, panel1.Width-1, panel1.Height-1);
      g.DrawLine(new Pen(Color.YellowGreen), 20, 20, 20, 40);
      g.DrawLine(new Pen(Color.YellowGreen), 20, 40, 40, 40);
      g.DrawLine(new Pen(Color.YellowGreen), 40, 40, 40, 20);

      g.DrawLine(new Pen(Color.YellowGreen), 100, 100, 120, 100);
      g.DrawLine(new Pen(Color.YellowGreen), 120, 100, 120, 120);
      g.DrawLine(new Pen(Color.YellowGreen), 120, 120, 100, 120);

      g.DrawLine(new Pen(Color.YellowGreen), 220, 200, 200, 200);
      g.DrawLine(new Pen(Color.YellowGreen), 200, 200, 200, 220);
      g.DrawLine(new Pen(Color.YellowGreen), 200, 220, 220, 220);

      g.DrawLine(new Pen(Color.YellowGreen), 300, 320, 300, 300);
      g.DrawLine(new Pen(Color.YellowGreen), 300, 300, 320, 300);
      g.DrawLine(new Pen(Color.YellowGreen), 320, 300, 320, 320);

     // g.DrawRectangle(new Pen(Color.Black), 400, 400, 500, 500);
      g.Dispose();      
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if (colorDialog1.ShowDialog() == DialogResult.OK)
        button1.BackColor = colorDialog1.Color;
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
      Graphics g = panel1.CreateGraphics();
      g.DrawImage(b, 0, 0);
      g.Dispose();
    }
  }
}