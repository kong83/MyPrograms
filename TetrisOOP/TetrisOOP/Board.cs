using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TetrisOOP
{
  public class Board
  {
    public int[,] field;

    protected int maxTop;
    public int MaxTop 
    {
      get 
      {
        return maxTop;
      }
    }

    protected int maxWidth;
    public int MaxWidth
    {
      get 
      {
        return maxWidth;
      }
    }

    private const int len = 20;
    public int Len
    {
      get 
      {
        return len;
      }
    }

    protected int score = 0;
    /// <summary>
    /// Получить количество начисленных очков
    /// </summary>
    /// <returns></returns>
    public string ScoreInfo
    {
      get { return "Очки:\n" + score.ToString(); }
    }

    protected double speed = 1;
    public double Speed
    {
      get { return speed; }
    }
    public string SpeedInfo
    {
      get 
      {
        if (speed <= 10)
        {
          return "Скорость:\n" + speed.ToString();
        }
        else
        {
          return "Скорость:\nMAX";
        }
      }
    }

    /// <summary>
    /// Конструктор поля
    /// </summary>
    /// <param name="maxT">Максимальное количество строк</param>
    /// <param name="maxW">Максимальное количество столбцов</param>
    public Board(int maxT, int maxW)
    {
      maxTop = maxT;
      maxWidth = maxW;
      field = new int[maxTop, maxWidth];
      for (int i = 0; i < maxTop; i++)
        for (int j = 0; j < maxWidth; j++)
          field[i, j] = 0;
    }

    /// <summary>
    /// Получить изображение нарисованных фигур
    /// </summary>
    /// <returns></returns>
    public Image GetImageField()
    {
      Bitmap pict = new Bitmap(len * maxWidth, len * maxTop, System.Drawing.Imaging.PixelFormat.Format32bppArgb);      
      Graphics g = Graphics.FromImage(pict);
      Pen pen = new Pen(Color.Black);
      Brush brush;

      for (int i = 0; i < maxTop; i++)
      {
        for (int j = 0; j < maxWidth; j++)
        {
          if (field[i, j] == 0)
          {
            brush = new SolidBrush(Color.White);            
          }
          else if (field[i, j] == 1)
          {
            brush = new SolidBrush(Color.Blue);
          }
          else
          {
            brush = new SolidBrush(Color.Red);
          }
          g.FillRectangle(brush, j * len, i * len, len, len);
          g.DrawRectangle(pen, j * len, i * len, len, len);
          brush.Dispose();          
        }
      }
      pen.Dispose();      
      return pict;
    }

    /// <summary>
    /// Очистить заполненные строки и перевести отметки фигуры из 10 в 1
    /// </summary>
    public int EraseRows()
    {
      int cntRow = 0;

      for (int i = 0; i < maxTop; i++)
      {
        int cnt = 0;
        for (int j = 0; j < maxWidth; j++)
        {
          if (field[i, j] == 10)
          {
            field[i, j] = 1;
          }
          if (field[i, j] == 1)
            cnt++;
        }
        if (cnt == maxWidth)
        {
          for (int l = i; l > 0; l--)
          {
            for (int k = 0; k < maxWidth; k++)
            {
              field[l, k] = field[l - 1, k];
            }
          }
          for (int k = 0; k < maxWidth; k++)
          {
            field[0, k] = 0;
          }
          cntRow++;
        }
      }
      return cntRow;      
    }

    /// <summary>
    /// Начисляет очки за убранные строки
    /// </summary>
    /// <param name="cntRow"></param>
    public virtual void SetScore(int cntRow)
    {
      score += cntRow * 10 + cntRow * 5;
      if (score >= speed * 1000) 
      {
        if (speed < 10)
        {
          speed++;          
        }
        else
        {
          speed = 10.5;
        }
      }
    }    

    /// <summary>
    /// Сохранение информации о текущей игре
    /// </summary>
    public virtual void SaveGame() { }
  }
}
