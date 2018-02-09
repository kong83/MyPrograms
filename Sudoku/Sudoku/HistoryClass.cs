using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sudoku
{
    /// <summary>
    ///  Класс для запоминания изменений на поле
    /// </summary>
    public class HistoryClass
    {
        /// <summary>
        /// Список изменений клеток (старые значения)
        /// </summary>
        List<Cell> historyListOld;

        /// <summary>
        /// Список изменений клеток (новые значения)
        /// </summary>
        List<Cell> historyListNew;

        /// <summary>
        /// Текущее положение в списке изменений
        /// </summary>
        int curIndex;

        public HistoryClass()
        {
            historyListOld = new List<Cell>();
            historyListNew = new List<Cell>();
            curIndex = -1;
        }

        /// <summary>
        /// Добавить одну запись в историю
        /// </summary>
        public void AddStep(Cell oldCell, Cell newCell)
        {
            if (curIndex < historyListOld.Count - 1)
            {
                historyListOld.RemoveRange(curIndex + 1, historyListOld.Count - curIndex - 1);
                historyListNew.RemoveRange(curIndex + 1, historyListNew.Count - curIndex - 1);
            }
            historyListOld.Add(oldCell);
            historyListNew.Add(newCell);
            curIndex++;
        }

        /// <summary>
        ///  Взять запись из истории (вернуть ход)
        /// </summary>
        public Cell Redo()
        {
            if (curIndex < historyListNew.Count - 1)
            {
                return historyListNew[++curIndex];
            }
            else
            {
                throw new NotNodeException("Нет доступных значений");
            }
        }

        /// <summary>
        ///  Взять запись из истории (отменить ход)
        /// </summary>
        public Cell Undo()
        {
            if (curIndex > -1)
            {
                return historyListOld[curIndex--];
            }
            else
            {
                throw new NotNodeException("Нет доступных значений");
            }
        }
    }

    /// <summary>
    /// Исключение при невозможности получить ход
    /// </summary>
    public class NotNodeException : Exception
    {
        public override string Message
        {
            get
            {
                return base.Message;
            }
        }
        public NotNodeException() : base() { }
        public NotNodeException(string message) : base(message) { }
        
    }
}
