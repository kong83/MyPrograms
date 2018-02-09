using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proba
{
    public partial class Form1 : Form
    {
      private bool flag = false;
      private byte b = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          try
          {
            b = 0;
            this.timer1.Interval = Convert.ToInt32(Convert.ToDouble(this.textBox1.Text)*1000);
            flag = true;
            this.button6.Text = "Стоп";
            this.timer1.Enabled = true;            
          }
          catch
          {
            MessageBox.Show("Ошибка в значении временного промежутка."+"\n"+"Введите величину задержки ещё раз.");
            return;
          }
        }

      private void timer1_Tick(object sender, EventArgs e)
      {
        if (!flag)
        {
          this.timer1.Enabled = false;
          MessageBox.Show("Вы проиграли");
          this.button1.Enabled = true;
          this.button2.Enabled = true;
          this.button3.Enabled = true;
          this.button4.Enabled = true;
        }
        else
        {
          int i;
          if (this.button1.Enabled == true)
            i = 0;
          else if (this.button2.Enabled == true)
            i = 1;
          else if (this.button3.Enabled == true)
            i = 2;
          else 
            i = 3;          

          this.button1.Enabled = false;
          this.button2.Enabled = false;
          this.button3.Enabled = false;
          this.button4.Enabled = false;
          this.button1.BackColor = SystemColors.Control;
          this.button2.BackColor = SystemColors.Control;
          this.button3.BackColor = SystemColors.Control;
          this.button4.BackColor = SystemColors.Control;

          Color backColor = Color.Aqua;
          Random R = new Random();
          int k;
          while ((k = R.Next(4)) == i);
          switch (k)
          {
            case 0: this.button1.Enabled = true; button1.BackColor = backColor; break;
            case 1: this.button2.Enabled = true; button2.BackColor = backColor; break;
            case 2: this.button3.Enabled = true; button3.BackColor = backColor; break;
            case 3: this.button4.Enabled = true; button4.BackColor = backColor; break;
          }          
          b++;
          if (b>10)
          {
            this.textBox1.Text = Convert.ToString(Convert.ToDouble(this.textBox1.Text)-0.1);
            this.timer1.Interval = Convert.ToInt32(Convert.ToDouble(this.textBox1.Text)*1000);
            b = 0;
          }
          flag = false;
        }
      }

      private void выходToolStripMenuItem_Click(object sender, EventArgs e)
      {
        this.Close();
      }

      private void button6_Click(object sender, EventArgs e)
      {
        if (this.timer1.Enabled == true)
        {
          this.timer1.Enabled = false;
          this.button6.Text = "Выход";
          this.button1.Enabled = true;
          this.button2.Enabled = true;
          this.button3.Enabled = true;
          this.button4.Enabled = true;
        }
        else
          this.Close();
      }

      private void button1_MouseDown(object sender, MouseEventArgs e)
      {
        flag = true;
        this.button1.BackColor = SystemColors.ButtonShadow;
        this.timer1.Enabled = false;
        this.timer1.Interval = Convert.ToInt32(Convert.ToDouble(this.textBox1.Text) * 1000);
        this.timer1.Enabled = true;
      }

      private void button2_MouseDown(object sender, MouseEventArgs e)
      {
        flag = true;
        this.button2.BackColor = SystemColors.ButtonShadow;
        this.timer1.Enabled = false;
        this.timer1.Interval = Convert.ToInt32(Convert.ToDouble(this.textBox1.Text) * 1000);
        this.timer1.Enabled = true;
      }

      private void button3_MouseDown(object sender, MouseEventArgs e)
      {
        flag = true;
        this.button3.BackColor = SystemColors.ButtonShadow;
        this.timer1.Enabled = false;
        this.timer1.Interval = Convert.ToInt32(Convert.ToDouble(this.textBox1.Text) * 1000);
        this.timer1.Enabled = true;
      }

      private void button4_MouseDown(object sender, MouseEventArgs e)
      {
        flag = true;
        this.button4.BackColor = SystemColors.ButtonShadow;
        this.timer1.Enabled = false;
        this.timer1.Interval = Convert.ToInt32(Convert.ToDouble(this.textBox1.Text) * 1000);
        this.timer1.Enabled = true;
      }

    }
}