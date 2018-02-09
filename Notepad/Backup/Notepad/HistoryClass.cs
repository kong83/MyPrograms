using System;
using System.Collections;

namespace Notepad
{
    /// <summary>
    /// Структура, содержащая информацию для ведения истории действий
    /// </summary>
    public struct HistoryInfo
    {
        /// <summary>
        /// Положение курсора
        /// </summary>
        public int SelectionStart;

        /// <summary>
        /// Длина выделенного текста
        /// </summary>
        public int SelectionLength;

        /// <summary>
        /// Положение вертикального скроллера
        /// </summary>
        public int VertScrollPosition;

        /// <summary>
        /// Положение горизонтального скроллера
        /// </summary>
        public int HorizScrollPosition;

        /// <summary>
        /// Текст на экране
        /// </summary>
        public string ScrinText;

        /// <summary>
        /// Указатель того, заполнена эта структура или нет (новая)
        /// </summary>
        public bool Valid;
    }

    /// <summary>
    /// Класс для ведения истории действий
    /// </summary>
    public class HistoryClass : IDisposable
    {
        /// <summary>
        /// Массив, куда складывается вся история действий
        /// </summary>
        private readonly ArrayList _historyArray;
        /// <summary>
        /// Указатель на позицию в массиве, куда надо вставлять новую запись 
        /// (или с которой надо брать ранее сохранённые)
        /// </summary>
        private int _cntArray;

        public HistoryClass(HistoryInfo hisInfo)
        {
            _historyArray = new ArrayList();
            hisInfo.Valid = true;
            _historyArray.Add(hisInfo);
            _cntArray = 0;
        }


        /// <summary>
        /// Добавить новую запись для сохранения
        /// </summary>
        /// <param name="hisInfo"></param>
        public void AddNode(HistoryInfo hisInfo)
        {
            //http://wm-help.net/books-online/book/59464/59464-7.html
            if (_cntArray > 10000)
            {
                throw new Exception("Внимание: слишком длинная история действий. Во избежании переполнения памяти история действий больше не ведётся.");
            }
            while (_cntArray + 1 < _historyArray.Count)
            {
                _historyArray.RemoveAt(_cntArray + 1);
            }

            hisInfo.Valid = true;
            _historyArray.Add(hisInfo);
            _cntArray++;
        }

        /// <summary>
        /// Вернуть предыдущую сохраненную позицию
        /// </summary>
        /// <returns></returns>
        public HistoryInfo Undo()
        {
            if (_cntArray > 0)
            {
                _cntArray--;
                return (HistoryInfo)_historyArray[_cntArray];
            }
            return new HistoryInfo();
        }

        /// <summary>
        /// Вернуть следующую сохранённую позицию, если она есть
        /// </summary>
        /// <returns></returns>
        public HistoryInfo Redo()
        {
            if (_cntArray < _historyArray.Count - 1)
            {
                _cntArray++;
                return (HistoryInfo)_historyArray[_cntArray];
            }
            return new HistoryInfo();
        }

        /// <summary>
        /// Доступна ли отмена
        /// </summary>
        /// <returns></returns>
        public bool IsUndoValid()
        {
            if (_cntArray > 0)
                return true;

            return false;
        }

        /// <summary>
        /// Доступно ли возвращение
        /// </summary>
        /// <returns></returns>
        public bool IsRedoValid()
        {
            if (_cntArray < _historyArray.Count - 1)
                return true;

            return false;
        }


        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            while (_historyArray.Count > 0)
            {
                _historyArray.RemoveAt(0);
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
