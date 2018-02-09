using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace Sudoku
{
    class SolveClass
    {
        /// <summary>
        /// Текущая расстановка цифр на поле
        /// </summary>
        private int[,] board;

        /// <summary>
        /// Указатель на главную форму
        /// </summary>
        private MainForm mainForm;

        /// <summary>
        /// Допустимые цифры для каждой клетки
        /// </summary>
        private ArrayList[,] possibleDigits;

        public ArrayList[,] PossibleDigits
        { 
            get
            {
                return possibleDigits;
            }
        }

        /// <summary>
        /// Текущая клетка для просмотра массива с клетками
        /// </summary>
        private int[] index;

        /// <summary>
        /// Переход к следующей пустой клеткe
        /// </summary>
        private void IndexNext(int[,] possibleDigitsIndex)
        {
            if (index.Length != 2)
            {
                throw new ArgumentException("Неправильное значение");
            }

            do
            {
                if (index[1] < 8)
                {
                    index[1]++;
                }
                else
                {
                    if (index[0] < 8)
                    {
                        index[0]++;
                        index[1] = 0;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
            while (possibleDigitsIndex[index[0], index[1]] == -1);
        }


        /// <summary>
        /// Переход к предыдущей пустой клетке
        /// </summary>
        private void IndexPref(int[,] possibleDigitsIndex)
        {
            if (index.Length != 2)
            {
                throw new ArgumentException("Неправильное значение");
            }

            do
            {
                if (index[1] > 0)
                {
                    index[1]--;
                }
                else
                {
                    if (index[0] > 0)
                    {
                        index[0]--;
                        index[1] = 8;
                    }
                    else
                    {
                        throw new IndexOutOfRangeException();
                    }
                }
            }
            while (possibleDigitsIndex[index[0], index[1]] == -1);
        }


        /// <summary>
        /// Массив с информацией о клетках
        /// </summary>
        /// <param name="board"></param>
        public SolveClass(int[,] board, MainForm mainForm)
        {
            this.board = board;
            this.mainForm = mainForm;
            possibleDigits = new ArrayList[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    possibleDigits[i, j] = new ArrayList();
                    if (board[i, j] == 0)
                    {
                        for (int val = 1; val < 10; val++)
                        {
                            possibleDigits[i, j].Add(val);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Попытка решить текущую позицию
        /// </summary>
        /// <returns></returns>
        public int[,] SolveSudoku()
        {
            if (IsLinesContainsDoublicate())
            {
                MessageBox.Show("Данное судоку решить невозможно, потому что в текущей позиции имеются дубликаты).", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }

            //
            // Делаем проходы до тех пор, пока можно их делать
            //
            while (LogicStep()) ;

            string question = "";
            if (IsSudokuSolved())
            {
                question = "Данное судоку успешно решено логически.\r\nПоказать решение?";
            }
            else
            {
                if (mainForm.useRunning)
                {
                    SearchBoard();

                    if (IsSudokuSolved())
                    {
                        question = "Данное судоку успешно решено с использованием перебора.\r\nПоказать решение?";
                    }
                    else
                    {
                        question = "Данное судоку решить не удалось\r\n(скорее всего при разгадке допущена ошибка).\r\nПоказать полученный результат?";
                    }
                }
                else
                {
                    question = "Данное судоку не удалось решить логически\r\n(решение перебором не проводилось, т.к. его использование отключено в настройках программы).\r\nПоказать полученный результат?";
                }
            }

            if (MessageBox.Show(question, "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return board;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// Перебор по незаполненным клеткам среди допустимых значений для этих клеток
        /// </summary>
        private void SearchBoard()
        {
            //
            // Сохранение начальной позиции
            //
            int[,] saveBoard = new int[9, 9];
            // Номер рассматриваемой дополнительной метки
            int[,] possibleDigitsIndex = new int[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    saveBoard[i, j] = board[i, j];
                    if (board[i, j] != 0)
                    {
                        possibleDigitsIndex[i, j] = -1;
                    }
                    else
                    {
                        possibleDigitsIndex[i, j] = 0;
                    }
                }
            }

            try
            {
                index = new int[2] { 0, -1 };
                IndexNext(possibleDigitsIndex);
                try
                {
                    board[index[0], index[1]] = (int)possibleDigits[index[0], index[1]][possibleDigitsIndex[index[0], index[1]]];
                }
                catch
                { 
                    throw new IndexOutOfRangeException();
                }

                while (true)
                {
                    if (IsLinesContainsDoublicate())
                    {
                        do
                        {
                            if (possibleDigitsIndex[index[0], index[1]] < possibleDigits[index[0], index[1]].Count - 1)
                            {
                                possibleDigitsIndex[index[0], index[1]]++;
                                board[index[0], index[1]] = (int)possibleDigits[index[0], index[1]][possibleDigitsIndex[index[0], index[1]]];
                            }
                            else
                            {
                                possibleDigitsIndex[index[0], index[1]] = 0;
                                board[index[0], index[1]] = 0;
                                while (true)
                                {
                                    IndexPref(possibleDigitsIndex);
                                    if (possibleDigitsIndex[index[0], index[1]] < possibleDigits[index[0], index[1]].Count - 1)
                                    {
                                        possibleDigitsIndex[index[0], index[1]]++;
                                        board[index[0], index[1]] = (int)possibleDigits[index[0], index[1]][possibleDigitsIndex[index[0], index[1]]];
                                        break;
                                    }
                                    else
                                    {
                                        possibleDigitsIndex[index[0], index[1]] = 0;
                                        board[index[0], index[1]] = 0;
                                    }
                                }
                            }
                        }
                        while (IsLinesContainsDoublicate());
                    }
                    else
                    {
                        IndexNext(possibleDigitsIndex);
                        board[index[0], index[1]] = (int)possibleDigits[index[0], index[1]][possibleDigitsIndex[index[0], index[1]]];
                    }
                }
            }
            catch (IndexOutOfRangeException)
            {
                if (!IsSudokuSolved())
                {
                    //
                    // Судоку решить не удалось
                    // Восстановление начальной позиции
                    //
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            board[i, j] = saveBoard[i, j];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        board[i, j] = saveBoard[i, j];
                    }
                }
                MessageBox.Show("В процессе работы программы произошла ошибка:\r\n" + ex.Message + "\r\nСообщите разработчикам");
            }
        }


        /// <summary>
        /// Проделать один проход по массиву с целью откинуть невозможные цифры в клетках
        /// Возвращается true - если удалось сделать хоть что-то
        /// </summary>
        /// <returns></returns>
        private bool LogicStep()
        {
            bool isSuccess = false;

            //
            // Проходим по всей доске и убираем невозможные цифры
            // Смотрим на заполненную клетку и прохoдим по всем зависимым от неё клеткам
            // (в столбце, в строке и в квадрате), 
            // помечая невозможность установить в них эту цифру
            //
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] != 0)
                    {
                        // Удаление из строки
                        for (int col = 0; col < 9; col++)
                        {
                            if (possibleDigits[i, col].Contains(board[i, j]))
                            {
                                isSuccess = true;
                                possibleDigits[i, col].Remove(board[i, j]);
                            }
                        }

                        // Удаление из столбца
                        for (int row = 0; row < 9; row++)
                        {
                            if (possibleDigits[row, j].Contains(board[i, j]))
                            {
                                isSuccess = true;
                                possibleDigits[row, j].Remove(board[i, j]);
                            }
                        }

                        // Удаление из квадрата
                        int fRow = i / 3;
                        int fCol = j / 3;
                        for (int row = fRow * 3; row < fRow * 3 + 3; row++)
                        {
                            for (int col = fCol * 3; col < fCol * 3 + 3; col++)
                            {
                                if (possibleDigits[row, col].Contains(board[i, j]))
                                {
                                    isSuccess = true;
                                    possibleDigits[row, col].Remove(board[i, j]);
                                }
                            }
                        }
                    }
                }
            }

            //
            // Ищем в каждом из выделенных квадратов цифры, образующие линию и
            // помечаем во всех клетках этой линии невозможность поставить там эти цифры
            // Например, в первом квадрате в первой и второй клетках можно поставить цифру 1.
            // Во всех остальных клетках первого квадрата нигде больше цифру 1 поставить нельзя.
            // Проходим по первой строке и удаляем из доступных цифр цифру 1.
            // Допустим, в третьей и шестой клетке можно поставить цифру 2. Нигде больше цифру 2
            // в этом квадрате поставить нельзя. Проходим по третьему столбцу и помечаем во всех 
            // клетках, что цифру 2 там поставить нельзя
            //
            for (int i = 0; i < 9; i++) 
            {
                for (int j = 0; j < 9; j++) 
                {
                    for (int pd = 0; pd < possibleDigits[i, j].Count; pd++) 
                    {
                        ArrayList cells = new ArrayList();
                        for (int row = (i / 3) * 3; row < (i / 3) * 3 + 3; row++)
                        {
                            for (int col = (j / 3) * 3; col < (j / 3) * 3 + 3; col++)
                            {
                                if (possibleDigits[row, col].Contains(possibleDigits[i, j][pd]))
                                {
                                    cells.Add(new int[] { row, col });
                                }
                            }
                        }

                        if (cells.Count > 3)
                        {
                            continue;
                        }

                        bool isRow = true;
                        bool isColumn = true;
                        for (int c = 0; c < cells.Count - 1; c++)
                        { 
                            int[] ordinate = (int[])cells[c];
                            int[] ordinateNext = (int[])cells[c + 1];
                            if (isRow && ordinate[0] != ordinateNext[0])
                            {
                                isRow = false;
                            }
                            if (isColumn && ordinate[1] != ordinateNext[1])
                            {
                                isColumn = false;
                            }
                            if (!isRow && !isColumn)
                            {
                                break;
                            }
                        }

                        if (isRow)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                if (c < (j / 3) * 3 || c >= (j / 3) * 3 + 3)
                                {
                                    if (possibleDigits[i, c].Contains(possibleDigits[i, j][pd]))
                                    {
                                        isSuccess = true;
                                        possibleDigits[i, c].Remove(possibleDigits[i, j][pd]);
                                    }
                                }
                            }
                        }
                        if (isColumn)
                        {
                            for (int r = 0; r < 8; r++)
                            {
                                if (r < (i / 3) * 3 || r >= (i / 3) * 3 + 3)
                                {
                                    if (possibleDigits[r, j].Contains(possibleDigits[i, j][pd]))
                                    {
                                        isSuccess = true;
                                        possibleDigits[r, j].Remove(possibleDigits[i, j][pd]);
                                    }
                                }
                            }
                        }
                    }
                }
            }


            //
            // Проходим по всей доске и выставляем определившиеся цифры
            //
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    // Если в данной ячейке может стоять только одна цифра - ставим её
                    if (possibleDigits[i, j].Count == 1)
                    {
                        isSuccess = true;
                        board[i, j] = (int)possibleDigits[i, j][0];
                        possibleDigits[i, j] = new ArrayList();
                    }
                    else
                    {
                        // Если в данной ячейке может стоять несколько цифр - то проверяем
                        // каждую цифру на возможность поставить её в другое место в строке,
                        // столбце или квадрате, которым она принадлежит
                        // Если хотя бы где-то её поставить нельзя - то ставим в эту ячейку
                        for (int val = 0; val < possibleDigits[i, j].Count; val++)
                        {
                            bool isPossible = false;

                            // Проверяем в строке
                            for (int col = 0; col < 9; col++)
                            {
                                if (col != j &&
                                   (possibleDigits[i, col].Contains(possibleDigits[i, j][val]) ||
                                    board[i, col] == (int)possibleDigits[i, j][val]))
                                {
                                    isPossible = true;
                                    break;
                                }
                            }

                            // Проверяем в столбце
                            if (isPossible)
                            {
                                isPossible = false;
                                for (int row = 0; row < 9; row++)
                                {
                                    if (row != i &&
                                       (possibleDigits[row, j].Contains(possibleDigits[i, j][val]) ||
                                        board[row, j] == (int)possibleDigits[i, j][val]))
                                    {
                                        isPossible = true;
                                        break;
                                    }
                                }
                            }

                            // Проверяем в квадрате
                            if (isPossible)
                            {
                                isPossible = false;
                                int fRow = i / 3;
                                int fCol = j / 3;
                                for (int row = fRow * 3; row < fRow * 3 + 3; row++)
                                {
                                    for (int col = fCol * 3; col < fCol * 3 + 3; col++)
                                    {
                                        if ((row != i || col != j) &&
                                            (possibleDigits[row, col].Contains(possibleDigits[i, j][val]) ||
                                             board[row, col] == (int)possibleDigits[i, j][val]))
                                        {
                                            isPossible = true;
                                            row = fRow * 3 + 3;
                                            break;
                                        }
                                    }
                                }
                            }

                            // Если где-то нельзя поставить - то ставим в эту ячейку
                            if (!isPossible)
                            {
                                isSuccess = true;
                                board[i, j] = (int)possibleDigits[i, j][val];
                                possibleDigits[i, j] = new ArrayList();
                                break;
                            }
                        }
                    }
                }
            }

            return isSuccess;
        }


        /// <summary>
        /// Имеются ли на поле дубликаты в строках, стобцах или выделенных квадратах
        /// </summary>
        /// <returns></returns>
        private bool IsLinesContainsDoublicate()
        {
            //
            // Проверяем, чтобы в каждой строке каждая цифра встречалась только один раз
            //
            for (int i = 0; i < 9; i++)
            {
                int[] temp = new int[10];
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        continue;
                    }

                    if (temp[board[i, j]] == 0)
                    {
                        temp[board[i, j]] = 1;
                    }
                    else
                    {
                        return true;
                    }
                }
            }

            //
            // Проверяем, чтобы в каждом столбце каждая цифра встречалась только один раз
            //
            for (int j = 0; j < 9; j++)
            {
                // Считаем сколько раз каждая цифра встречается в столбце
                int[] temp = new int[10];
                for (int i = 0; i < 9; i++)
                {
                    if (board[i, j] == 0)
                    {
                        continue;
                    }

                    if (temp[board[i, j]] == 0)
                    {
                        temp[board[i, j]] = 1;
                    }
                    else
                    {
                        return true;
                    }
                }
            }


            //
            // Проверяем, чтобы в каждом квадрате каждая цифра встречалась только один раз
            //
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int[] temp = new int[10];
                    for (int i = row * 3; i < row * 3 + 3; i++)
                    {
                        for (int j = col * 3; j < col * 3 + 3; j++)
                        {
                            if (board[i, j] == 0)
                            {
                                continue;
                            }

                            if (temp[board[i, j]] == 0)
                            {
                                temp[board[i, j]] = 1;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка на правильную расстановку позиции
        /// </summary>
        /// <returns></returns>
        private bool IsSudokuSolved()
        {
            //
            // Проверяем, чтобы на поле не было пустых ячеек
            //
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        return false;
                    }
                }
            }

            return !IsLinesContainsDoublicate();
        }
    }
}
