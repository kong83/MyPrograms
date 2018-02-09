using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TetrisOOP
{
  public abstract class Shape
  {
    protected enum FigureState
    {
      vertical,
      horisontal,
      left, 
      top, 
      rigth, 
      down
    }

    /// <summary>
    /// Текущее положение фигуры
    /// </summary>
    protected FigureState currentState;

    /// <summary>
    /// Указатель на класс для рисования поля, содержащий массив с текущей расстановкой фигур
    /// </summary>
    protected Board board;
    
    /// <summary>
    /// Координаты левого верхнего угла фигуры
    /// Первая координата - сверху вниз, вторая - слева направо
    /// </summary>
    protected int[,] leftTop;

    /// <summary>
    /// Сдвиг фигуры влево на одно поле
    /// </summary>    
    public abstract void MouveLeft();

    /// <summary>
    /// Сдвиг фигуры вправо на одно поле
    /// </summary>
    public abstract void MouveRight();

    /// <summary>
    /// Сдвиг фигуры вниз на одно поле
    /// </summary>
    public abstract void MouveDown();

    /// <summary>
    /// Картинка с данной фигурой
    /// </summary>
    /// <returns></returns>
    public abstract Image GetImage();

    /// <summary>
    /// Инициализация фигуры на поле
    /// </summary>
    /// <returns></returns>
    public abstract void Initialize();

    /// <summary>
    /// Поворот фигуры
    /// </summary>
    public virtual void Rotate() { }

    // Описание события, возникающего при полном опускании фигуры
    public delegate void ShapeDown();				
    public event ShapeDown OnShapeDown;
    public delegate void EndGame();
    public event ShapeDown OnEndGame;
    protected void OccurShapeDown()
    {
      if (leftTop[0, 0] < 0 && OnEndGame != null)
      {
        OnEndGame.Invoke();
      }
      else if (OnShapeDown != null)
      {
        OnShapeDown.Invoke();
      }
    }

    /// <summary>
    /// Конструктор фигуры
    /// </summary>
    /// <param name="boardClass"></param>
    public Shape(Board boardClass)
    {
      board = boardClass;

      leftTop = new int[1, 2];
      leftTop[0, 0] = 0;
      leftTop[0, 1] = 4;      
    }

    
  }
}
