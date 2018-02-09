using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;

namespace TetrisOOP
{
  sealed class Line : Shape
  {    
    /// <summary>
    /// Сдвиг фигуры влево на одно поле
    /// </summary>
    public override void MouveLeft()
    {
      if (leftTop[0, 1] > 0) 
      {
        if (currentState == FigureState.horisontal)
        {
          if (board.field[leftTop[0, 0], leftTop[0, 1] - 1] == 0)
          {
            board.field[leftTop[0, 0], leftTop[0, 1] - 1] = 10;
            board.field[leftTop[0, 0], leftTop[0, 1] + 3] = 0;
            leftTop[0, 1] -= 1;
          }
        }
        else
        {
          if (board.field[leftTop[0, 0], leftTop[0, 1] - 1] == 0 &&
            board.field[leftTop[0, 0] + 1, leftTop[0, 1] - 1] == 0 &&
            board.field[leftTop[0, 0] + 2, leftTop[0, 1] - 1] == 0 &&
            board.field[leftTop[0, 0] + 3, leftTop[0, 1] - 1] == 0)
          {
            board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
            board.field[leftTop[0, 0] + 1, leftTop[0, 1]] = 0;
            board.field[leftTop[0, 0] + 2, leftTop[0, 1]] = 0;
            board.field[leftTop[0, 0] + 3, leftTop[0, 1]] = 0;
            board.field[leftTop[0, 0], leftTop[0, 1] - 1] = 10;
            board.field[leftTop[0, 0] + 1, leftTop[0, 1] - 1] = 10;
            board.field[leftTop[0, 0] + 2, leftTop[0, 1] - 1] = 10;
            board.field[leftTop[0, 0] + 3, leftTop[0, 1] - 1] = 10;
            leftTop[0, 1] -= 1;
          }
        }
      }      
    }

    /// <summary>
    /// Сдвиг фигуры вправо на одно поле
    /// </summary>
    public override void MouveRight()
    {      
      if (currentState == FigureState.horisontal)
      {
        if (leftTop[0, 1] + 4 < board.MaxWidth && 
          board.field[leftTop[0, 0], leftTop[0, 1] + 4] == 0)
        {                    
          board.field[leftTop[0, 0], leftTop[0, 1] + 4] = 10;
          board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
          leftTop[0, 1] += 1;
        }
      }
      else
      {
        if (leftTop[0, 1] + 1 < board.MaxWidth &&
          board.field[leftTop[0, 0], leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 2, leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 3, leftTop[0, 1] + 1] == 0)
        {
          board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0] + 2, leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0] + 3, leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0], leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0] + 2, leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0] + 3, leftTop[0, 1] + 1] = 10;
          leftTop[0, 1] += 1;
        }
      }
    }

    /// <summary>
    /// Сдвиг фигуры вниз на одно поле
    /// </summary>
    public override void MouveDown()
    {
      if (currentState == FigureState.horisontal)
      {
        if (leftTop[0, 0] + 1 < board.MaxTop &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1]] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 2] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 3] == 0)
        {
          board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0], leftTop[0, 1] + 1] = 0;
          board.field[leftTop[0, 0], leftTop[0, 1] + 2] = 0;
          board.field[leftTop[0, 0], leftTop[0, 1] + 3] = 0;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1]] = 10;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 2] = 10;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 3] = 10;
          leftTop[0, 0] += 1;
        }
        else 
        {
          OccurShapeDown();
        }
      }
      else
      {
        if (leftTop[0, 0] + 4 < board.MaxTop &&
          board.field[leftTop[0, 0] + 4, leftTop[0, 1]] == 0)
        {
          board.field[leftTop[0, 0], leftTop[0, 1]] = 0;         
          board.field[leftTop[0, 0] + 4, leftTop[0, 1]] = 10;
          leftTop[0, 0] += 1;
        }
        else
        {
          OccurShapeDown();
        }
      }     
    }

    /// <summary>
    /// Поворот фигуры
    /// </summary>
    public override void Rotate()
    {
      if (currentState == FigureState.horisontal)
      {
        if (leftTop[0, 0] > 0 &&
          leftTop[0, 0] + 2 < board.MaxTop &&
          board.field[leftTop[0, 0] - 1, leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 2, leftTop[0, 1] + 1] == 0)
        {
          board.field[leftTop[0, 0] - 1, leftTop[0, 1] + 1] = 10;          
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0] + 2, leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0], leftTop[0, 1] + 2] = 0;
          board.field[leftTop[0, 0], leftTop[0, 1] + 3] = 0;
          leftTop[0, 0] -= 1;
          leftTop[0, 1] += 1;
          currentState = FigureState.vertical;
        }        
      }
      else
      {
        if (leftTop[0, 1] > 0 &&
          leftTop[0, 1] + 2 < board.MaxWidth &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] - 1] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] == 0 &&
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 2] == 0)
        {
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] - 1] = 10;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 1] = 10;
          board.field[leftTop[0, 0] + 1, leftTop[0, 1] + 2] = 10;
          board.field[leftTop[0, 0], leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0] + 2, leftTop[0, 1]] = 0;
          board.field[leftTop[0, 0] + 3, leftTop[0, 1]] = 0;
          leftTop[0, 0] += 1;
          leftTop[0, 1] -= 1;
          currentState = FigureState.horisontal;
        }        
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
      g.FillRectangle(brush, board.Len * 1, board.Len * 0, board.Len * 1, board.Len * 4);
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
        board.field[leftTop[0, 0] + 2, leftTop[0, 1]] != 0 ||
        board.field[leftTop[0, 0] + 3, leftTop[0, 1]] != 0)
      {
        isEnd = true;
      }

      currentState = FigureState.vertical;
      board.field[leftTop[0, 0], leftTop[0, 1]] = 10;
      board.field[leftTop[0, 0] + 1, leftTop[0, 1]] = 10;
      board.field[leftTop[0, 0] + 2, leftTop[0, 1]] = 10;
      board.field[leftTop[0, 0] + 3, leftTop[0, 1]] = 10;
        
      if(isEnd)
      {
        leftTop[0, 0] = -1;
        OccurShapeDown();
      }
    }

    /// <summary>
    /// Конструктор линии
    /// </summary>
    /// <param name="boardClass"></param>
    public Line(Board boardClass) 
      : base(boardClass)
    {
      
    }
  }
}
