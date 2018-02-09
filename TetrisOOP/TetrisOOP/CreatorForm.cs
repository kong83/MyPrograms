using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TetrisOOP
{
  public partial class CreatorForm : Form
  {
    int[] clearRows = new int[4];
    Board board;

    public CreatorForm()
    {
      InitializeComponent();
      board = new Board(20, 10);
      pictureBoard.Image = board.GetImageField();
    }

    /// <summary>
    /// Загрузить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button2_Click(object sender, EventArgs e)
    {
      board = new Board(20, 10);
      ZLibClass zlibClass = new ZLibClass();
      try
      {
        if (openFileDialog.ShowDialog() == DialogResult.OK)
        {
          FileStream fs = new FileStream(openFileDialog.FileName, FileMode.Open);
          byte[] lvlInfo = new byte[fs.Length];
          fs.Read(lvlInfo, 0, (int)fs.Length);
          fs.Close();
          byte[] unpackLvlInfo = zlibClass.Unpack(lvlInfo);
          string lvlStr = Encoding.GetEncoding("windows-1251").GetString(unpackLvlInfo);
          string[] data = lvlStr.Split(new string[1] { "," }, StringSplitOptions.RemoveEmptyEntries);
          for (int i = 0; i < 4; i++)
            clearRows[i] = Convert.ToInt32(data[i]);
          for (int i = 0; i < board.MaxTop; i++)
          {
            for (int j = 0; j < board.MaxWidth; j++)
            {
              board.field[i, j] = Convert.ToInt32(data[i * board.MaxWidth + j + 4]);
            }
          }
          pictureBoard.Image = board.GetImageField();
          textBox1.Text = clearRows[0].ToString();
          textBox2.Text = clearRows[1].ToString();
          textBox3.Text = clearRows[2].ToString();
          textBox4.Text = clearRows[3].ToString();
        }
      }
      catch
      {
        MessageBox.Show("Файл " + openFileDialog.FileName + " повреждён", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }
    }

    /// <summary>
    /// Рисование очередной ячейки
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void SetCell(int x, int y)
    {                   
      if (x >= board.MaxTop)
        x = board.MaxTop - 1;
      if (x < 0)
        x = 0;
      if (y >= board.MaxWidth)
        y = board.MaxWidth - 1;
      if (y < 0)
        y = 0;
      if (radioDraw.Checked)
        board.field[x, y] = 1;
      else
        board.field[x, y] = 0;
      pictureBoard.Image = board.GetImageField();
    }

    //
    // Рисование с помощью движения мыши
    //
    bool isDown = false;
    private void pictureBoard_MouseDown(object sender, MouseEventArgs e)
    {
      SetCell(e.Y / board.Len, e.X / board.Len); 
      isDown = true;
    }
    private void pictureBoard_MouseMove(object sender, MouseEventArgs e)
    {
      if (isDown)
      {
        SetCell(e.Y / board.Len, e.X / board.Len);
      }
    }
    private void pictureBoard_MouseUp(object sender, MouseEventArgs e)
    {
      SetCell(e.Y / board.Len, e.X / board.Len);
      isDown = false;
    }

    /// <summary>
    /// Сохранить
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void button1_Click(object sender, EventArgs e)
    {
      try
      {
        int n = Convert.ToInt32(textBox1.Text);
        if (n < 0 && n > 100)
        {
          MessageBox.Show("Неправильная запись параметра. Допустимое значение от 0 до 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          textBox1.Focus();
          return;
        }

      }
      catch
      {
        MessageBox.Show("Неправильная запись параметра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        textBox1.Focus();
        return;
      }
      try
      {
        int n = Convert.ToInt32(textBox2.Text);
        if (n < 0 && n > 100)
        {
          MessageBox.Show("Неправильная запись параметра. Допустимое значение от 0 до 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          textBox2.Focus();
          return;
        }

      }
      catch
      {
        MessageBox.Show("Неправильная запись параметра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        textBox2.Focus();
        return;
      }
      try
      {
        int n = Convert.ToInt32(textBox3.Text);
        if (n < 0 && n > 100)
        {
          MessageBox.Show("Неправильная запись параметра. Допустимое значение от 0 до 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          textBox3.Focus();
          return;
        }

      }
      catch
      {
        MessageBox.Show("Неправильная запись параметра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        textBox3.Focus();
        return;
      }
      try
      {
        int n = Convert.ToInt32(textBox4.Text);
        if (n < 0 && n > 100)
        {
          MessageBox.Show("Неправильная запись параметра. Допустимое значение от 0 до 100", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
          textBox4.Focus();
          return;
        }

      }
      catch
      {
        MessageBox.Show("Неправильная запись параметра", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        textBox4.Focus();
        return;
      }

      if (Convert.ToInt32(textBox1.Text) + Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text) == 0)
      {
        MessageBox.Show("Неправильная запись параметров. Хотя бы одно значение должно быть больше 0", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        textBox1.Focus();
        return;
      }
      if (saveFileDialog.ShowDialog() == DialogResult.OK)
      {
        ZLibClass zlibClass = new ZLibClass();
        string allInfo = "";
        allInfo += textBox1.Text + "," + textBox2.Text + "," + textBox3.Text + "," + textBox4.Text + ",";
        
        for (int i = 0; i < board.MaxTop; i++)
        {
          for (int j = 0; j < board.MaxWidth; j++)
          {
            allInfo += board.field[i, j] + ",";
          }
        }
        allInfo = allInfo.Substring(0, allInfo.Length - 1);



        byte[] arrInfo = Encoding.GetEncoding("windows-1251").GetBytes(allInfo);
        byte[] packInfo = zlibClass.Pack(arrInfo);
        FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);
        fs.Write(packInfo, 0, packInfo.Length);
        fs.Close();
      }
    }
  }
}