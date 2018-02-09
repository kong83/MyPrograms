using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TetrisOOP
{
  public class Square : Shape
  {
    /// <summary>
    /// Сдвиг фигуры влево на одно поле
    /// </summary>
    public override void MouveLeft()
    {
      if (leftTop[0, 1] > 0 &&
        board.field[leftTop[0, 0], leftTop[0, 1] - 1] == 0 &&
        board.field[leftTop[0, 0] + 1, leftTop[0, 1] - 1] == 0)
      {
        board.field[leftTop[0, 0], leftTop[0, 1] - 1] = 10;
        board.field[leftTop[0, 0] + 1, leftTop[0, 1] - 1] = 10;
        board.field[leftTop[0, 0], leftTop[0, 1] + 1] = 0;
        board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] = 0;
        leftTop[0, 1] -= 1;
      }
    }

    /// <summary>
    /// Сдвиг фигуры вправо на одно поле
    /// </summary>
    public override void MouveRight()
    {
      if (leftTop[0, 1] + 2 < board.MaxWidth &&
       board.field[leftTop[0, 0], leftTop[0, 1] + 2] == 0 &&
       board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 2] == 0)
      {
        board.field[leftTop[0, 0], leftTop[0, 1] + 2] = 10;
        board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 2] = 10;
        board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
        board.field[leftTop[0, 0] + 1, leftTop[0, 1]] = 0;
        leftTop[0, 1] += 1;
      }
    }

    /// <summary>
    /// Сдвиг фигуры вниз на одно поле
    /// </summary>
    public override void MouveDown()
    {
      if (leftTop[0, 0] + 2 < board.MaxTop &&
       board.field[leftTop[0, 0] + 2, leftTop[0, 1]] == 0 &&
       board.field[leftTop[0, 0] + 2, leftTop[0, 1] + 1] == 0)
      {
        board.field[leftTop[0, 0] + 2, leftTop[0, 1]] = 10;
        board.field[leftTop[0, 0] + 2, leftTop[0, 1] + 1] = 10;
        board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
        board.field[leftTop[0, 0], leftTop[0, 1] + 1] = 0;
        leftTop[0, 0] += 1;
      }
      else 
      {
        OccurShapeDown();
      }
    }

    /// <summary>
    /// Картинка с данной фигурой
    /// </summary>
    /// <returns></returns>
    public override Image GetImage()
    {
      Bitmap pict = new Bitmap(board.Len * 4, board.Len * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
      Graphics g = Graphics.FromImage(pict);
      Pen pen = new Pen(Color.Black);
      Brush brush = new SolidBrush(Color.White);
      g.FillRectangle(brush, 0, 0, pict.Width, pict.Height);

      brush = new SolidBrush(Color.Red);
      g.FillRectangle(brush, board.Len * 1, board.Len * 1, board.Len * 2, board.Len * 2);
      brush.Dispose();

      for (int i = 0; i < 4; i++)
      {
        for (int j = 0; j < 4; j++)
        {
          g.DrawRectangle(pen, j * board.Len, i * board.Len, board.Len, board.Len);          
        }
      }
      pen.Dispose();

      return pict;
    }

    /// <summary>
    /// Инициализация фигуры на поле
    /// </summary>
    /// <returns></returns>
    public override void Initialize()
    {
      bool isEnd = false;
      if (board.field[leftTop[0, 0], leftTop[0, 1]] != 0 ||
          board.field[leftTop[0, 0] + 1, leftTop[0, 1]] != 0 ||
          board.field[leftTop[0, 0], leftTop[0, 1] + 1] != 0 ||
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] != 0)
      {
        isEnd = true;
       
      }

      board.field[leftTop[0, 0], leftTop[0, 1]] = 10;
      board.field[leftTop[0, 0] + 1, leftTop[0, 1]] = 10;
      board.field[leftTop[0, 0], leftTop[0, 1] + 1] = 10;
      board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] = 10;

      if (isEnd)
      {
        leftTop[0, 0] = -1;
        OccurShapeDown();
      }
    }

    /// <summary>
    /// Конструктор квадрата
    /// </summary>
    /// <param name="boardClass"></param>
    public Square(Board boardClass)
      : base(boardClass)
    {
     
    }
  }
}
