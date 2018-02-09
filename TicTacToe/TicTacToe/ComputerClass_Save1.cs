using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace TicTacToe
{
    public class ComputerClass
    {
        /// <summary>
        /// Структурка, которая хранит данные о том, надо ли помечать данный узел как выигрышный
        /// при просмотре узлов дерева и содержащая информацию о том, сколько ходов до выигрыша
        /// </summary>
        struct MarkAndDepth
        {
            /// <summary>
            /// True - если узел выигрышный, false - в противном случае
            /// </summary>
            public bool? IsNodeWin;

            /// <summary>
            /// Количество ходов до выигрыша
            /// </summary>
            public int Depth;
        }

        /// <summary>
        /// Структура для хранения информации о ходе
        /// </summary>
        struct StepInfo
        {
            /// <summary>
            /// x-координата ячейки
            /// </summary>
            public int X;

            /// <summary>
            /// y-координата ячейки
            /// </summary>
            public int Y;
        }


        /// <summary>
        /// Структура, содержащая список параметров одного состояния для исследования
        /// позиции вглубь 
        /// </summary>
        [DebuggerDisplay("Step={Step[0]}:{Step[1]} WhoStep={WhoStep} ParentNodeNumbers={ParentNodeNumbers[0]}")]
        struct Node
        {
            /// <summary>
            /// Текущая позиция
            /// </summary>
            public ObjectType[,] Field;

            /// <summary>
            /// Кто сделал этот ход
            /// </summary>
            public ObjectType WhoStep;

            /// <summary>
            /// Ход, который привёл к этой позиции
            /// </summary>
            public int[] Step;

            /// <summary>
            /// Номер родительского узла, откуда мы пришли сюда
            /// </summary>
            public List<int> ParentNodeNumbers;

            //public int Estimate;            // Оценка текущей позиции 

            /// <summary>
            /// Если понятно, что кто-то выиграл - то указать кто
            /// </summary>
            public ObjectType WhoWin;

            /// <summary>
            /// Количество ходов до победы
            /// </summary>
            public int StepCntToWin;
        }

        /// <summary>
        /// Поле с текущей позицией
        /// </summary>
        private static ObjectType[,] m_Field;

        /// <summary>
        /// Количество строк на поле
        /// </summary>
        private static int m_RowsCnt;

        /// <summary>
        /// Количество столбцов на поле
        /// </summary>
        private static int m_ColumnsCnt;

        /// <summary>
        /// Кто сейчас ходит
        /// </summary>
        private static ObjectType m_WhoStep;

        /// <summary>
        /// Поле для рисования
        /// </summary>
        private static PaintClass m_PaintClass;

        //private const int DefenceValue = 2;

        /// <summary>
        /// Получить объект врага
        /// </summary>
        /// <param name="whoStep"></param>
        /// <returns></returns>
        private static ObjectType WhoDoesntStep(ObjectType whoStep)
        {
            if (whoStep == ObjectType.Cross)
            {
                return ObjectType.Nil;
            }
            return ObjectType.Cross;
        }


        /// <summary>
        /// Функция для копирования массива ObjectType[,]
        /// </summary>
        /// <param name="rows">Количество строк в массиве</param>
        /// <param name="columns">Количество столбцов в массиве</param>
        /// <param name="fromArr">Массив, содержащий позицию на поле</param>
        /// <returns></returns>
        private static ObjectType[,] CopyObjectTypeArray(int rows, int columns, ObjectType[,] fromArr)
        {
            var resArr = new ObjectType[rows, columns];
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (fromArr[i, j] != ObjectType.Empty)
                    {
                        resArr[i, j] = fromArr[i, j];
                    }
                }
            }
            return resArr;
        }


        /// <summary>
        /// Добавить новый уровень в дерево возможных ходов
        /// </summary>
        /// <param name="tree"></param>
        /// <param name="newLevel"></param>
        private static void AddNewLevelToTree(ICollection<List<Node>> tree, IEnumerable<Node> newLevel)
        {
            var temp = new List<Node>();
            foreach (Node node in newLevel)
            {
                temp.Add(node);
            }
            tree.Add(temp);
        }


        /// <summary>
        /// Сравнение двух позиций
        /// </summary>
        /// <param name="field1">Первая позиция</param>
        /// <param name="field2">Вторая позиция</param>
        /// <param name="stepsChain">Список полей, по которым проводится проверка</param>
        /// <returns></returns>
        private static bool IsEqualTwoFields(ObjectType[,] field1, ObjectType[,] field2, StepInfo[] stepsChain)
        {
            for (int i = 0; i < stepsChain.Length; i++)
            {
                if (field1[stepsChain[i].X, stepsChain[i].Y] != field2[stepsChain[i].X, stepsChain[i].Y])
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Добавить только новый узел
        /// </summary>
        /// <param name="positions">Список узлов из добавляемого нового уровня дерева возможных ходов</param>
        /// <param name="newNode">Добавляемый узел</param>
        /// <param name="stepsChain">Список ходов, который был сделан для достижения добавляемого узла. В других позициях сравниваются только эти клетки</param>
        private static void AddOnlyNewNode(IList<Node> positions, Node newNode, StepInfo[] stepsChain)
        {
            bool isThisPositionExist = false;
            for (int i = 0; i < positions.Count; i++)
            {
                if (IsEqualTwoFields(positions[i].Field, newNode.Field, stepsChain))
                {
                    isThisPositionExist = true;
                    foreach (int parentNodeNumber in newNode.ParentNodeNumbers)
                    {
                        positions[i].ParentNodeNumbers.Add(parentNodeNumber);
                    }
                    break;
                }
            }

            if (!isThisPositionExist)
            {
                positions.Add(newNode);
            }
        }


        /// <summary>
        /// Получить цепочку ходов, приведщих к этому узлу
        /// </summary>
        /// <param name="tree">Всё дерево</param>
        /// <param name="node">Узел, до которого надо найти цепочку ходов</param>
        /// <returns></returns>
        private static StepInfo[] GetStepsChain(IList<List<Node>> tree, Node node)
        {
            var chain = new StepInfo[tree.Count + 1];
            chain[tree.Count].X = node.Step[0];
            chain[tree.Count].Y = node.Step[1];

            int parentNodeNumber = node.ParentNodeNumbers[0];
            int cnt = tree.Count - 1;
            do
            {
                chain[cnt].X = tree[cnt][parentNodeNumber].Step[0];
                chain[cnt].Y = tree[cnt][parentNodeNumber].Step[1];
                parentNodeNumber = tree[cnt][parentNodeNumber].ParentNodeNumbers[0];
                cnt--;
            }
            while (cnt != -1);

            return chain;
        }


        /// <summary>
        /// Сделать ход за компьютер
        /// </summary>
        /// <param name="field">Текущая позиция</param>
        /// <param name="rowsCnt">Количество строк</param>
        /// <param name="columnsCnt">Количество столбцов</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="paintClass">Ссылка на класс для рисования</param>
        /// <returns></returns>
        public static int[] DoStep(ObjectType[,] field, int rowsCnt, int columnsCnt, ObjectType whoStep, PaintClass paintClass)
        {
            m_Field = field;
            m_RowsCnt = rowsCnt;
            m_ColumnsCnt = columnsCnt;
            m_WhoStep = whoStep;
            m_PaintClass = paintClass;

            m_PaintClass.ClearCoefficients(m_RowsCnt, m_ColumnsCnt);

            #region OLD CODE
            /*
            // Проверить, можно ли текущим ходом закончить игру, поставив пятый символ
            int[] stepCoordinates = FindOneWinStep(m_WhoStep);
            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }

            // Ищем клетку, при выставлении в которую объекта врага, получается 5 подряд
            stepCoordinates = FindOneWinStep(WhoDoesntStep(m_WhoStep));

            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }

            // Ищем клетку, при выставлении которой у нас получится 4 подряд с возможностью выиграть
            // следующим ходом
            stepCoordinates = FindSomeWinStep(m_WhoStep, 10);

            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }

            // Проверить, надо ли выставлять символ для защиты
            stepCoordinates = FindSomeWinStep(WhoDoesntStep(m_WhoStep), DefenceValue);
            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }
            */
            #endregion

            // Проверить, если поле пустое - то вернуть ячейку по центру
            int[] stepCoordinates = GetFirstStep();
            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }

            // Найти оптимальное место под новый символ для атаки
            stepCoordinates = InvestigateForseAttack();
            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }

            // Если нету форсированных выигрышей или необходимости защищаться от форсированного
            // выигрыша - то поставить новый объект по какому-то алгоритму
            stepCoordinates = FindAttackStep();
            if (stepCoordinates.Length != 0)
            {
                return stepCoordinates;
            }

            // Если случился баг - то выбрать любое непустое место на поле случайным образом
            return GetRandomStep();
        }


        /// <summary>
        /// Проверить, если данный ход первый ход - то вернуть центральную клетку 
        /// в качестве первого хода
        /// </summary>
        /// <returns></returns>
        private static int[] GetFirstStep()
        {
            bool isThisFieldEmpty = true;

            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (m_Field[i, j] != ObjectType.Empty)
                    {
                        isThisFieldEmpty = false;
                        goto ex;
                    }
                }
            }
        ex:

            if (isThisFieldEmpty)
            {
                return new[] { m_RowsCnt / 2, m_ColumnsCnt / 2 };
            }

            return new int[0];
        }

        #region OLD CODE
        /*
        #region Выигрывающий ход
        /// <summary>
        /// Поиск выигравающего хода
        /// </summary>
        /// <param name="whoStep"></param>
        /// <returns></returns>
        private static int[] FindOneWinStep(ObjectType whoStep)
        {
            // Ищем, можем ли мы где нибудь поставить пятый объект
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (m_Field[i, j] != ObjectType.Empty)
                    {
                        continue;
                    }

                    // Ищем, сколько подряд по горизонтали
                    int len = 1;
                    int ci1 = i - 1;
                    int cj1 = j;
                    while (ci1 >= 0 && m_Field[ci1, cj1] == whoStep)
                    {
                        ci1--;
                        len++;
                    }

                    int ci2 = i + 1;
                    int cj2 = j;
                    while (ci2 < m_RowsCnt && m_Field[ci2, cj2] == whoStep)
                    {
                        ci2++;
                        len++;
                    }

                    if (len > 4)
                    {
                        return new[] { i, j };
                    }

                    // Ищем, сколько подряд по вертикали
                    len = 1;
                    ci1 = i;
                    cj1 = j - 1;
                    while (cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
                    {
                        cj1--;
                        len++;
                    }

                    ci2 = i;
                    cj2 = j + 1;
                    while (cj2 < m_ColumnsCnt && m_Field[ci2, cj2] == whoStep)
                    {
                        cj2++;
                        len++;
                    }

                    if (len > 4)
                    {
                        return new[] { i, j };
                    }

                    // Ищем, сколько подряд наискосок с левого верха до правого низа
                    len = 1;
                    ci1 = i - 1;
                    cj1 = j - 1;
                    while (ci1 >= 0 && cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
                    {
                        ci1--;
                        cj1--;
                        len++;
                    }

                    ci2 = i + 1;
                    cj2 = j + 1;
                    while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && m_Field[ci2, cj2] == whoStep)
                    {
                        ci2++;
                        cj2++;
                        len++;
                    }

                    if (len > 4)
                    {
                        return new[] { i, j };
                    }

                    // Ищем, сколько подряд наискосок с левого низа до правого верха
                    len = 1;
                    ci1 = i - 1;
                    cj1 = j + 1;
                    while (ci1 >= 0 && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] == whoStep)
                    {
                        ci1--;
                        cj1++;
                        len++;
                    }

                    ci2 = i + 1;
                    cj2 = j - 1;
                    while (ci2 < m_RowsCnt && cj2 >= 0 && m_Field[ci2, cj2] == whoStep)
                    {
                        ci2++;
                        cj2--;
                        len++;
                    }

                    if (len > 4)
                    {
                        return new[] { i, j };
                    }
                }
            }

            return new int[0];
        }
        #endregion


        #region Поиск выигрывающих комбинаций
        /// <summary>
        /// Проверка, является ли выигрышным данный ход
        /// </summary>
        /// <param name="x">Координата по строке</param>
        /// <param name="y">Координата по столбцу</param>
        /// <param name="whoStep">Кто ходит</param>
        /// <returns></returns>
        private static int IsThisCellWin(int x, int y, ObjectType whoStep)
        {
            // Ищем, сколько подряд по горизонтали
            int len = 1;
            int ci1 = x - 1;
            int cj1 = y;
            while (ci1 >= 0 && m_Field[ci1, cj1] == whoStep)
            {
                ci1--;
                len++;
            }

            int ci2 = x + 1;
            int cj2 = y;
            while (ci2 < m_RowsCnt && m_Field[ci2, cj2] == whoStep)
            {
                ci2++;
                len++;
            }

            if (len > 4)
            {
                return 10;
            }

            if (len > 3 && ci1 >= 0 && ci2 < m_RowsCnt &&
                m_Field[ci1, cj1] == ObjectType.Empty && m_Field[ci2, cj2] == ObjectType.Empty)
            {
                return 1;
            }

            // Ищем, сколько подряд по вертикали
            len = 1;
            ci1 = x;
            cj1 = y - 1;
            while (cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
            {
                cj1--;
                len++;
            }

            ci2 = x;
            cj2 = y + 1;
            while (cj2 < m_ColumnsCnt && m_Field[ci2, cj2] == whoStep)
            {
                cj2++;
                len++;
            }

            if (len > 4)
            {
                return 10;
            }

            if (len > 3 && cj1 >= 0 && cj2 < m_ColumnsCnt &&
                m_Field[ci1, cj1] == ObjectType.Empty && m_Field[ci2, cj2] == ObjectType.Empty)
            {
                return 1;
            }

            // Ищем, сколько подряд наискосок с левого верха до правого низа
            len = 1;
            ci1 = x - 1;
            cj1 = y - 1;
            while (ci1 >= 0 && cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1--;
                len++;
            }

            ci2 = x + 1;
            cj2 = y + 1;
            while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && m_Field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2++;
                len++;
            }

            if (len > 4)
            {
                return 10;
            }

            if (len > 3 && ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && ci1 >= 0 && cj1 >= 0 &&
                m_Field[ci1, cj1] == ObjectType.Empty && m_Field[ci2, cj2] == ObjectType.Empty)
            {
                return 1;
            }

            // Ищем, сколько подряд наискосок с левого низа до правого верха
            len = 1;
            ci1 = x - 1;
            cj1 = y + 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1++;
                len++;
            }

            ci2 = x + 1;
            cj2 = y - 1;
            while (ci2 < m_RowsCnt && cj2 >= 0 && m_Field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2--;
                len++;
            }

            if (len > 4)
            {
                return 10;
            }

            if (len > 3 && ci2 < m_RowsCnt && cj2 >= 0 && ci1 >= 0 && cj1 < m_ColumnsCnt &&
                m_Field[ci1, cj1] == ObjectType.Empty && m_Field[ci2, cj2] == ObjectType.Empty)
            {
                return 1;
            }

            return 0;
        }


        /// <summary>
        /// Смотрим, во сколько мест можно поставить выигрышный объект, если поставить
        /// объект в данную ячейку
        /// </summary>
        /// <param name="x">Координата по строке</param>
        /// <param name="y">Координата по столбцу</param>
        /// <param name="whoStep">Кто сейчас ходит</param>
        /// <returns></returns>
        private static int GetEstimateValueForCell(int x, int y, ObjectType whoStep)
        {
            ObjectType whoDoesntStep = WhoDoesntStep(whoStep);
            int result = 0;

            m_Field[x, y] = whoDoesntStep;

            // Смотрим, можно ли выиграть, поставив объект по вертикали вверху             
            int ci1 = x - 1;
            int cj1 = y;
            while (ci1 >= 0 && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                ci1--;
            }

            // Смотрим, можно ли выиграть, поставив объект по вертикали внизу
            ci1 = x + 1;
            cj1 = y;
            while (ci1 < m_RowsCnt && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                ci1++;
            }

            // Смотрим, можно ли выиграть, поставив объект по горизонтали влево
            ci1 = x;
            cj1 = y - 1;
            while (cj1 >= 0 && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                cj1--;
            }

            // Смотрим, можно ли выиграть, поставив объект по горизонтали вправо
            ci1 = x;
            cj1 = y + 1;
            while (cj1 < m_ColumnsCnt && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                cj1++;
            }

            // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок влево вверх
            ci1 = x - 1;
            cj1 = y - 1;
            while (ci1 >= 0 && cj1 >= 0 && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                ci1--;
                cj1--;
            }

            // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок вправо вниз
            ci1 = x + 1;
            cj1 = y + 1;
            while (ci1 < m_RowsCnt && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                ci1++;
                cj1++;
            }


            // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок вправо вверх
            ci1 = x - 1;
            cj1 = y + 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                ci1--;
                cj1++;
            }

            // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок влево вниз
            ci1 = x + 1;
            cj1 = y - 1;
            while (ci1 < m_RowsCnt && cj1 >= 0 && m_Field[ci1, cj1] != whoStep)
            {
                result += IsThisCellWin(ci1, cj1, whoDoesntStep);
                ci1++;
                cj1--;
            }

            m_Field[x, y] = ObjectType.Empty;

            return result;
        }


        /// <summary>
        /// Поиск выигрышной комбинации
        /// </summary>
        /// <returns></returns>
        private static int[] FindSomeWinStep(ObjectType whoStep, int winValue)
        {
            // Ищем наиболее подходящую клетку для защитного хода.
            // Если в эту клетку надо ставить объект - то ставим. 
            // Если угроза не очень большая - не ставим
            var defenceField = new int[m_RowsCnt, m_ColumnsCnt];
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (m_Field[i, j] != ObjectType.Empty)
                    {
                        defenceField[i, j] = 0;
                        continue;
                    }

                    defenceField[i, j] = GetEstimateValueForCell(i, j, WhoDoesntStep(whoStep));
                }
            }

            // Найти самый высокий коэффициент
            int x = 0;
            int y = 0;
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (defenceField[i, j] > defenceField[x, y])
                    {
                        x = i;
                        y = j;
                    }
                }
            }

            // Если нету полей, в которые надо ставить объект - то выходим
            if (defenceField[x, y] < winValue)
            {
                return new int[0];
            }

            // Получаем все ячейки с самым большим значением для защиты
            var rightFields = new List<int[]>();

            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (defenceField[i, j] == defenceField[x, y])
                    {
                        rightFields.Add(new[] { i, j });
                    }
                }
            }

            // Выбираем случайное значение из этих ячеек
            var rand = new Random();
            int val = rand.Next(rightFields.Count);

            m_PaintClass.DrawCoefficients(m_RowsCnt, m_ColumnsCnt, defenceField);
            return rightFields[val];
        }
        #endregion
        */
        #endregion

        #region Исследование форсированных выигрышей
        /// <summary>
        ///  Ищем, сколько подряд по горизонтали
        /// </summary>
        /// <param name="field">Позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>
        /// <param name="findAtack">True - если мы ищем атакующий ход, false - если защитный</param>
        /// <returns></returns>
        private static bool[] FindGorizontal(
            ObjectType[,] field, ObjectType whoStep,
            int x, int y, bool findAtack)
        {
            int len1 = 1;
            int ci1 = x - 1;
            int cj1 = y;
            while (ci1 >= 0 && field[ci1, cj1] == whoStep)
            {
                ci1--;
                len1++;
            }

            int ci2 = x + 1;
            int cj2 = y;
            while (ci2 < m_RowsCnt && field[ci2, cj2] == whoStep)
            {
                ci2++;
                len1++;
            }

            // Если есть 5 подряд или больше - то этот ход атакующий и выигрывающий
            if (len1 >= 5)
            {
                return new[] { true, true };
            }

            // Поиск новой последовательности слева через пробел от основной последовательности
            int len0 = 0;
            int ci0 = ci1;
            int cj0 = cj1;
            if (ci0 >= 0 && field[ci1, cj1] == ObjectType.Empty)
            {
                ci0 = ci1 - 1;
                while (ci0 >= 0 && field[ci0, cj0] == whoStep)
                {
                    ci0--;
                    len0++;
                }
            }

            // Поиск новой последовательности справа через пробел от основной последовательности
            int len2 = 0;
            int ci3 = ci2;
            int cj3 = cj2;
            if (ci3 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                ci3 = ci2 + 1;
                while (ci3 < m_RowsCnt && field[ci3, cj3] == whoStep)
                {
                    ci3++;
                    len2++;
                }
            }

            // Если мы ищем защитный ход - то нам подходит только 4 подряд с пуcтыми 
            // клетками по краям
            if (!findAtack)
            {
                if (len1 == 4 &&
                 ci1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                 ci2 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)
                {
                    return new[] { true, false };
                }

                // Если в сумме две последовательности дают 4 
                // (левая + средняя или средняя + правая)- значит это выигрышный ход
                if ((len0 > 0 && len1 > 1 && len0 + len1 == 4) ||
                    (len2 > 0 && len1 > 1 && len1 + len2 == 4))
                {
                    return new[] { true, false };
                }

                return new bool[0];
            }

            // Если есть 4 подряд и справа или слева есть пустая клетка - то этот ход - атакующий
            if (len1 == 4 &&
                ((ci1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            // Если есть 3 подряд, справа и слева есть пустые клетки и хотя бы с одной
            // стороны в след. клетке не стоит объект противники - то это ход - атакующий
            if (len1 == 3 && ci1 >= 0 && ci2 < m_RowsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((ci1 >= 1 && field[ci1 - 1, cj1] != WhoDoesntStep(whoStep)) ||
                (ci2 < m_RowsCnt - 1 && field[ci2 + 1, cj2] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }

            // Если в сумме две последовательности дают 4 или больше - значит пробел между 
            // последовательностями - это следующий выигрышный ход
            if ((len0 > 0 && len0 + len1 == 4) ||
                (len2 > 0 && len1 + len2 == 4))
            {
                return new[] { true, false };
            }

            // Если в сумме получается 3 объекта - то проверяем, есть ли с краёв последовательности
            // место под 5-ый объект. Тогда пустая клетка между посдедовательностями будет
            // 4-ой выигрывающей клеткой при следующем ходе. Проверяем сначала центральную и
            // левую последовательности, потом центральную и правую
            if (len0 > 0 && len0 + len1 == 3 &&
                ci0 >= 0 && field[ci0, cj0] == ObjectType.Empty &&
                ci2 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            if (len2 > 0 && len1 + len2 == 3 &&
                ci1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                ci3 < m_RowsCnt && field[ci3, cj3] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            return new bool[0];
        }


        /// <summary>
        ///  Ищем, сколько подряд по вертикали
        /// </summary>
        /// <param name="field">Позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>
        /// <param name="findAtack">True - если мы ищем атакующий ход, false - если защитный</param>
        /// <returns></returns>
        private static bool[] FindVertical(
            ObjectType[,] field, ObjectType whoStep,
            int x, int y, bool findAtack)
        {
            int len1 = 1;
            int ci1 = x;
            int cj1 = y - 1;
            while (cj1 >= 0 && field[ci1, cj1] == whoStep)
            {
                cj1--;
                len1++;
            }

            int ci2 = x;
            int cj2 = y + 1;
            while (cj2 < m_ColumnsCnt && field[ci2, cj2] == whoStep)
            {
                cj2++;
                len1++;
            }

            if (len1 >= 5)
            {
                return new[] { true, true };
            }

            int len0 = 0;
            int ci0 = ci1;
            int cj0 = cj1;
            if (cj0 >= 0 && field[ci1, cj1] == ObjectType.Empty)
            {
                cj0 = cj1 - 1;
                while (cj0 >= 0 && field[ci0, cj0] == whoStep)
                {
                    cj0--;
                    len0++;
                }
            }

            int len2 = 0;
            int ci3 = ci2;
            int cj3 = cj2;
            if (cj3 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                cj3 = cj2 + 1;
                while (cj3 < m_ColumnsCnt && field[ci3, cj3] == whoStep)
                {
                    cj3++;
                    len2++;
                }
            }

            if (!findAtack)
            {
                if (len1 == 4 &&
                 cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                 cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)
                {
                    return new[] { true, false };
                }

                if ((len0 > 0 && len1 > 1 && len0 + len1 == 4) ||
                    (len2 > 0 && len1 > 1 && len1 + len2 == 4))
                {
                    return new[] { true, false };
                }

                return new bool[0];
            }

            if (len1 == 4 &&
                ((cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            if (len1 == 3 && cj1 >= 0 && cj2 < m_ColumnsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((cj1 >= 1 && field[ci1, cj1 - 1] != WhoDoesntStep(whoStep)) ||
                (cj2 < m_ColumnsCnt - 1 && field[ci2, cj2 + 1] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }

            if ((len0 > 0 && len0 + len1 == 4) ||
                (len2 > 0 && len1 + len2 == 4))
            {
                return new[] { true, false };
            }

            if (len0 > 0 && len0 + len1 == 3 &&
                cj0 >= 0 && field[ci0, cj0] == ObjectType.Empty &&
                cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            if (len2 > 0 && len1 + len2 == 3 &&
                cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                cj3 < m_ColumnsCnt && field[ci3, cj3] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            return new bool[0];
        }


        /// <summary>
        /// Ищем, сколько подряд наискосок с левого верха до правого низа
        /// </summary>
        /// <param name="field">Позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>
        /// <param name="findAtack">True - если мы ищем атакующий ход, false - если защитный</param>
        /// <returns></returns>
        private static bool[] FindDiagonalLeftTopRightBottom(
            ObjectType[,] field, ObjectType whoStep,
            int x, int y, bool findAtack)
        {
            int len1 = 1;
            int ci1 = x - 1;
            int cj1 = y - 1;
            while (ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1--;
                len1++;
            }

            int ci2 = x + 1;
            int cj2 = y + 1;
            while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2++;
                len1++;
            }

            if (len1 >= 5)
            {
                return new[] { true, true };
            }

            int len0 = 0;
            int ci0 = ci1;
            int cj0 = cj1;
            if (ci0 >= 0 && cj0 >= 0 && field[ci1, cj1] == ObjectType.Empty)
            {
                ci0 = ci1 - 1;
                cj0 = cj1 - 1;
                while (ci0 >= 0 && cj0 >= 0 && field[ci0, cj0] == whoStep)
                {
                    ci0--;
                    cj0--;
                    len0++;
                }
            }

            int len2 = 0;
            int ci3 = ci2;
            int cj3 = cj2;
            if (ci3 < m_RowsCnt && cj3 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                ci3 = ci2 + 1;
                cj3 = cj2 + 1;
                while (ci3 < m_RowsCnt && cj3 < m_ColumnsCnt && field[ci3, cj3] == whoStep)
                {
                    ci3++;
                    cj3++;
                    len2++;
                }
            }

            if (!findAtack)
            {
                if (len1 == 4 &&
                    ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                    ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)
                {
                    return new[] { true, false };
                }

                if ((len0 > 0 && len1 > 1 && len0 + len1 == 4) ||
                    (len2 > 0 && len1 > 1 && len1 + len2 == 4))
                {
                    return new[] { true, false };
                }

                return new bool[0];
            }

            if (len1 == 4 &&
                ((ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            if (len1 == 3 && ci1 >= 0 && cj1 >= 0 && ci2 < m_RowsCnt && cj2 < m_ColumnsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((ci1 >= 1 && cj1 >= 1 && field[ci1 - 1, cj1 - 1] != WhoDoesntStep(whoStep)) ||
                (ci2 < m_RowsCnt - 1 && cj2 < m_ColumnsCnt - 1 && field[ci2 + 1, cj2 + 1] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }

            if ((len0 > 0 && len0 + len1 == 4) ||
                (len2 > 0 && len1 + len2 == 4))
            {
                return new[] { true, false };
            }

            if (len0 > 0 && len0 + len1 == 3 &&
                ci0 >= 0 && cj0 >= 0 && field[ci0, cj0] == ObjectType.Empty &&
                ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            if (len2 > 0 && len1 + len2 == 3 &&
                ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                ci3 < m_RowsCnt && cj3 < m_ColumnsCnt && field[ci3, cj3] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            return new bool[0];
        }


        /// <summary>
        /// Ищем, сколько подряд наискосок с левого низа до правого верха
        /// </summary>
        /// <param name="field">Позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>
        /// <param name="findAtack">True - если мы ищем атакующий ход, false - если защитный</param>
        /// <returns></returns>
        private static bool[] FindDiagonalLeftBottomRightTop(
            ObjectType[,] field, ObjectType whoStep,
            int x, int y, bool findAtack)
        {
            int len1 = 1;
            int ci1 = x - 1;
            int cj1 = y + 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1++;
                len1++;
            }

            int ci2 = x + 1;
            int cj2 = y - 1;
            while (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2--;
                len1++;
            }

            if (len1 >= 5)
            {
                return new[] { true, true };
            }

            int len0 = 0;
            int ci0 = ci1;
            int cj0 = cj1;
            if (ci0 >= 0 && cj0 < m_ColumnsCnt && field[ci1, cj1] == ObjectType.Empty)
            {
                ci0 = ci1 - 1;
                cj0 = cj1 + 1;
                while (ci0 >= 0 && cj0 < m_ColumnsCnt && field[ci0, cj0] == whoStep)
                {
                    ci0--;
                    cj0++;
                    len0++;
                }
            }

            int len2 = 0;
            int ci3 = ci2;
            int cj3 = cj2;
            if (ci3 < m_RowsCnt && cj3 >= 0 && field[ci2, cj2] == ObjectType.Empty)
            {
                ci3 = ci2 + 1;
                cj3 = cj2 - 1;
                while (ci3 < m_RowsCnt && cj3 >= 0 && field[ci3, cj3] == whoStep)
                {
                    ci3++;
                    cj3--;
                    len2++;
                }
            }

            if (!findAtack)
            {
                if (len1 == 4 &&
                    ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == ObjectType.Empty &&
                    ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == ObjectType.Empty)
                {
                    return new[] { true, false };
                }

                if ((len0 > 0 && len1 > 1 && len0 + len1 == 4) ||
                    (len2 > 0 && len1 > 1 && len1 + len2 == 4))
                {
                    return new[] { true, false };
                }

                return new bool[0];
            }

            if (len1 == 4 &&
                ((ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            if (len1 == 3 && ci1 >= 0 && cj1 < m_ColumnsCnt && ci2 < m_RowsCnt && cj2 >= 0 &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((ci1 >= 1 && cj1 < m_ColumnsCnt - 1 && field[ci1 - 1, cj1 + 1] != WhoDoesntStep(whoStep)) ||
                (ci2 < m_RowsCnt - 1 && cj2 >= 1 && field[ci2 + 1, cj2 - 1] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }

            if ((len0 > 0 && len0 + len1 == 4) ||
                (len2 > 0 && len1 + len2 == 4))
            {
                return new[] { true, false };
            }

            if (len0 > 0 && len0 + len1 == 3 &&
                ci0 >= 0 && cj0 < m_ColumnsCnt && field[ci0, cj0] == ObjectType.Empty &&
                ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            if (len2 > 0 && len1 + len2 == 3 &&
                ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == ObjectType.Empty &&
                ci3 < m_RowsCnt && cj3 >= 0 && field[ci3, cj3] == ObjectType.Empty)
            {
                return new[] { true, false };
            }

            return new bool[0];
        }


        /// <summary>
        /// Проверка, является ли указанный ход указанным игроком в указанной позиции атакующим
        /// (т.е. в результате появляется 3 в ряд, имеющие вправо и влево по 1 свободной клетке
        /// и хотя ыб с одной стороны не должен стоять объект противники,
        /// или 4 в ряд, имеющие слева или справа 1 свободную клетку, или 5 или более в ряд)
        /// В результате возвращается два значения: первое - является ли указанный ход атакующим,
        /// второе - приводит ли этот ход к 5 или более объектам подряд
        /// </summary>
        /// <param name="field">Исследуемая позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="step">В какое место делается ход</param>
        /// <param name="findAtack">Искать ли три в ряд (включая нашу клетку)</param>
        /// <returns></returns>
        private static bool[] IsThisStepAttackOrWin(
            ObjectType[,] field, ObjectType whoStep,
            int[] step, bool findAtack)
        {
            int x = step[0];
            int y = step[1];

            bool[] result = FindGorizontal(field, whoStep, x, y, findAtack);
            if (result.Length > 0)
                return result;

            result = FindVertical(field, whoStep, x, y, findAtack);
            if (result.Length > 0)
                return result;

            result = FindDiagonalLeftTopRightBottom(field, whoStep, x, y, findAtack);
            if (result.Length > 0)
                return result;

            result = FindDiagonalLeftBottomRightTop(field, whoStep, x, y, findAtack);
            if (result.Length > 0)
                return result;

            #region STANDART CODE
            /*
            // Ищем, сколько подряд по горизонтали
            bool[] result = FindGorizontal(field, whoStep, x, y, findThreeLines);
            if (result.Length > 0)
                return result;

            // Проверяем на наличие пустой клетки справа от (x,y), отделяющей её от нескольких 
            // подряд идущих объектов
            if (x > 0 && field[x - 1, y] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindGorizontal(field, whoStep, x - 1, y, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }

            // Проверяем на наличие пустой клетки слева от (x,y), отделяющей её от нескольких 
            // подряд идущих объектов
            if (x < m_RowsCnt - 1 && field[x + 1, y] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindGorizontal(field, whoStep, x + 1, y, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }


            // Ищем, сколько подряд по вертикали
            result = FindVertical(field, whoStep, x, y, findThreeLines);
            if (result.Length > 0)
                return result;

            // Проверяем на наличие пустой клетки вверху от (x,y), отделяющей её от нескольких 
            // подряд идущих объектов
            if (y > 0 && field[x, y - 1] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindVertical(field, whoStep, x, y - 1, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }

            // Проверяем на наличие пустой клетки внизу от (x,y), отделяющей её от нескольких 
            // подряд идущих объектов
            if (y < m_ColumnsCnt - 1 && field[x, y + 1] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindVertical(field, whoStep, x, y + 1, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }


            // Ищем, сколько подряд наискосок с левого верха до правого низа
            result = FindDiagonalLeftTopRightBottom(field, whoStep, x, y, findThreeLines);
            if (result.Length > 0)
                return result;

            // Проверяем на наличие пустой клетки слева вверху от (x,y), отделяющей её от             
            // нескольких подряд идущих объектов
            if (x > 0 && y > 0 && field[x - 1, y - 1] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindDiagonalLeftTopRightBottom(field, whoStep, x - 1, y - 1, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }

            // Проверяем на наличие пустой клетки справа внизу от (x,y), отделяющей её от 
            // нескольких подряд идущих объектов
            if (x < m_RowsCnt - 1 && y < m_ColumnsCnt - 1 && field[x + 1, y + 1] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindDiagonalLeftTopRightBottom(field, whoStep, x + 1, y + 1, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }


            // Ищем, сколько подряд наискосок с левого низа до правого верха
            result = FindDiagonalLeftBottomRightTop(field, whoStep, x, y, findThreeLines);
            if (result.Length > 0)
                return result;

            // Проверяем на наличие пустой клетки слева внизу от (x,y), отделяющей её от             
            // нескольких подряд идущих объектов
            if (x > 0 && y < m_ColumnsCnt - 1 && field[x - 1, y + 1] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindDiagonalLeftBottomRightTop(field, whoStep, x - 1, y + 1, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }

            // Проверяем на наличие пустой клетки справа ввверху от (x,y), отделяющей её от 
            // нескольких подряд идущих объектов
            if (x < m_RowsCnt - 1 && y > 0 && field[x + 1, y - 1] == ObjectType.Empty)
            {
                field[x, y] = whoStep;
                result = FindDiagonalLeftBottomRightTop(field, whoStep, x + 1, y - 1, false);
                field[x, y] = ObjectType.Empty;
                if (result.Length > 0)
                    return result;
            }*/
            #endregion

            #region OLD CODE
            /*
            int len, ci1, cj1, ci2, cj2;

            
            // Ищем, сколько подряд по горизонтали
            int len = 1;
            int ci1 = x - 1;
            int cj1 = y;
            while (ci1 >= 0 && field[ci1, cj1] == whoStep)
            {
                ci1--;
                len++;
            }

            int ci2 = x + 1;
            int cj2 = y;
            while (ci2 < m_RowsCnt && field[ci2, cj2] == whoStep)
            {
                ci2++;
                len++;
            }

            // Если есть 5 подряд или больше - то этот ход атакующий и выигрывающий
            if (len >= 5)
            {
                return new[] { true, true };
            }

            // Если есть 4 подряд и справа или слева есть пустая клетка - то этот ход - атакующий
            if (len == 4 && 
                ((ci1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            // Если есть 3 подряд, справа и слева есть пустые клетки и хотя бы с одной
            // стороны в след. клетке не стоит объект противники - то это ход - атакующий
            if (findThreeLines && len == 3 && ci1 >= 0 && ci2 < m_RowsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((ci1 >= 1 && field[ci1 - 1, cj1] != WhoDoesntStep(whoStep)) ||
                (ci2 < m_RowsCnt - 1 && field[ci2 + 1, cj2] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }

            // Ищем, сколько подряд по вертикали
            len = 1;
            ci1 = x;
            cj1 = y - 1;
            while (cj1 >= 0 && field[ci1, cj1] == whoStep)
            {
                cj1--;
                len++;
            }

            ci2 = x;
            cj2 = y + 1;
            while (cj2 < m_ColumnsCnt && field[ci2, cj2] == whoStep)
            {
                cj2++;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            if (len == 4 &&  
                ((cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            if (findThreeLines && len == 3 && cj1 >= 0 && cj2 < m_ColumnsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((cj1 >= 1 && field[ci1, cj1 - 1] != WhoDoesntStep(whoStep)) ||
                (cj2 < m_ColumnsCnt - 1 && field[ci2, cj2 + 1] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }
            

            // Ищем, сколько подряд наискосок с левого верха до правого низа
            len = 1;
            ci1 = x - 1;
            cj1 = y - 1;
            while (ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1--;
                len++;
            }

            ci2 = x + 1;
            cj2 = y + 1;
            while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2++;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            if (len == 4 && 
                ((ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            if (findThreeLines && len == 3 && ci1 >= 0 && cj1 >= 0 && ci2 < m_RowsCnt && cj2 < m_ColumnsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((ci1 >= 1 && cj1 >= 1 && field[ci1 - 1, cj1 - 1] != WhoDoesntStep(whoStep)) ||
                (ci2 < m_RowsCnt - 1 && cj2 < m_ColumnsCnt - 1 && field[ci2 + 1, cj2 + 1] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }
                
            // Ищем, сколько подряд наискосок с левого низа до правого верха
            len = 1;
            ci1 = x - 1;
            cj1 = y + 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1++;
                len++;
            }

            ci2 = x + 1;
            cj2 = y - 1;
            while (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2--;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            if (len == 4 && 
                ((ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == ObjectType.Empty)))
            {
                return new[] { true, false };
            }

            if (findThreeLines && len == 3 && ci1 >= 0 && cj1 < m_ColumnsCnt && ci2 < m_RowsCnt && cj2 >= 0 &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty &&
                ((ci1 >= 1 && cj1 < m_ColumnsCnt - 1 && field[ci1 - 1, cj1 + 1] != WhoDoesntStep(whoStep)) ||
                (ci2 < m_RowsCnt - 1 && cj2 >= 1 && field[ci2 + 1, cj2 - 1] != WhoDoesntStep(whoStep))))
            {
                return new[] { true, false };
            }*/
            #endregion

            return new[] { false, false };
        }


        /// <summary>
        /// Проверка, является ли указанный ход указанным игроком в указанной позиции защитным
        /// (т.е. в результате хода противника появляется 
        /// 4 в ряд, имеющие слева и справа по 1 свободной клетке, или 5 в ряд, или
        /// у нас появляется 5 в ряд)
        /// В результате возвращается два значения: первое - является ли указанный ход защитным,
        /// второе - приводит ли этот ход к 5 объектам подряд
        /// </summary>
        /// <param name="field">Исследуемая позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="step">В какое место делается ход</param>
        /// <returns></returns>
        private static bool[] IsThisStepDefenceOrWin(ObjectType[,] field, ObjectType whoStep, int[] step)
        {
            int x = step[0];
            int y = step[1];

            // Смотрим, является ли наша клетка пятой подряд
            // Ищем, сколько подряд по горизонтали
            int len = 1;
            int ci1 = x - 1;
            int cj1 = y;
            while (ci1 >= 0 && field[ci1, cj1] == whoStep)
            {
                ci1--;
                len++;
            }

            int ci2 = x + 1;
            int cj2 = y;
            while (ci2 < m_RowsCnt && field[ci2, cj2] == whoStep)
            {
                ci2++;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            // Ищем, сколько подряд по вертикали
            len = 1;
            ci1 = x;
            cj1 = y - 1;
            while (cj1 >= 0 && field[ci1, cj1] == whoStep)
            {
                cj1--;
                len++;
            }

            ci2 = x;
            cj2 = y + 1;
            while (cj2 < m_ColumnsCnt && field[ci2, cj2] == whoStep)
            {
                cj2++;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            // Ищем, сколько подряд наискосок с левого верха до правого низа
            len = 1;
            ci1 = x - 1;
            cj1 = y - 1;
            while (ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1--;
                len++;
            }

            ci2 = x + 1;
            cj2 = y + 1;
            while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2++;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            // Ищем, сколько подряд наискосок с левого низа до правого верха
            len = 1;
            ci1 = x - 1;
            cj1 = y + 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == whoStep)
            {
                ci1--;
                cj1++;
                len++;
            }

            ci2 = x + 1;
            cj2 = y - 1;
            while (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == whoStep)
            {
                ci2++;
                cj2--;
                len++;
            }

            if (len >= 5)
            {
                return new[] { true, true };
            }

            // Проверяем, является ли эта клетка четвёртой или пятой для противника
            // Если да - то это наша защитная клетка
            bool[] checkEnemy = IsThisStepAttackOrWin(field, WhoDoesntStep(whoStep), step, false);

            return new[] { checkEnemy[0], false };
        }


        /// <summary>
        /// Найти все ходы, которые приводят к атакующим позициям
        /// </summary>
        /// <param name="field">Исследуемая позиция</param>
        /// <param name="parentNodeNumber">Номер родительского узла</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <returns></returns>
        private static List<Node> FindAttacPositions(ObjectType[,] field, int parentNodeNumber, ObjectType whoStep)
        {
            var positions = new List<Node>();

            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (field[i, j] != ObjectType.Empty)
                    {
                        continue;
                    }

                    bool[] stepInfo = IsThisStepAttackOrWin(field, whoStep, new[] { i, j }, true);
                    if (stepInfo[0])
                    {
                        var newNode = new Node
                        {
                            Field = CopyObjectTypeArray(m_RowsCnt, m_ColumnsCnt, field),
                            Step = new[] { i, j },
                            WhoStep = whoStep,
                            WhoWin = stepInfo[1] ? whoStep : ObjectType.Empty,
                            ParentNodeNumbers = new List<int>()
                        };
                        newNode.Field[i, j] = whoStep;
                        newNode.ParentNodeNumbers.Add(parentNodeNumber);

                        positions.Add(newNode);

                        if (stepInfo[1])
                        {
                            return positions;
                        }
                    }
                }
            }
            return positions;
        }


        /// <summary>
        /// Найти все защитные хода, которые защищаются от атакующих позиций
        /// </summary>
        /// <param name="field">Исследуемая позиция</param>
        /// <param name="parentNodeNumber">Номер родительского узла</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <returns></returns>
        private static List<Node> FindDefencePositions(ObjectType[,] field, int parentNodeNumber, ObjectType whoStep)
        {
            var positions = new List<Node>();

            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (field[i, j] != ObjectType.Empty)
                    {
                        continue;
                    }

                    bool[] stepInfo = IsThisStepDefenceOrWin(field, whoStep, new[] { i, j });
                    if (stepInfo[0])
                    {
                        var newNode = new Node
                        {
                            Field = CopyObjectTypeArray(m_RowsCnt, m_ColumnsCnt, field),
                            Step = new[] { i, j },
                            WhoStep = whoStep,
                            WhoWin = stepInfo[1] ? whoStep : ObjectType.Empty,
                            ParentNodeNumbers = new List<int>()
                        };
                        newNode.Field[i, j] = whoStep;
                        newNode.ParentNodeNumbers.Add(parentNodeNumber);

                        positions.Add(newNode);

                        if (stepInfo[1])
                        {
                            return positions;
                        }
                    }
                }
            }


            return positions;
        }

        /// <summary>
        /// Просматриваем всё дерево и ищем ходы, которые ведут к выигрышу whoStep за
        /// минимальное количество ходов. Берём случайный среди всех ходов, у которого 
        /// к выигрышу ведёт минимальное количество ходов.
        /// Просматриваем дерево снизу вверх. Если ход делал whoStep и есть хотя бы один
        /// выигрывающий ход среди всех ходов из родительского узла - то помечаем его как
        /// выигрышный для whoStep. Если ход делал whoDoesntStep и есть хотя бы один не 
        /// проигрывающий ход среди всех ходов из родительского узла - то не помечаем его как
        /// выигрышный для whoStep
        /// В результате обработки узлов заполнятся все значения WhoWin и StepCntToWin для
        /// каждого узла. При помечании родительского узла как выигрышный (для whoStep) - 
        /// увеличиваем StepCntToWin
        /// </summary>
        /// <param name="tree">Дерево ходов</param>
        /// <param name="whoStep">Кто ходит первым</param>
        /// <returns></returns>
        private static void ReverseTrace(IList<List<Node>> tree, ObjectType whoStep)
        {
            for (int i = tree.Count - 1; i > 0; i--)
            {
                ObjectType whoDidThisStep = tree[i][0].WhoStep;
                var markParentWhoStep = new MarkAndDepth[tree[i - 1].Count];
                var markParentWhoDoesNotStep = new MarkAndDepth[tree[i - 1].Count];

                for (int j = 0; j < markParentWhoStep.Length; j++)
                {
                    if (whoDidThisStep == whoStep)
                    {
                        markParentWhoStep[j].Depth = 1000;
                        markParentWhoStep[j].IsNodeWin = false;
                        markParentWhoDoesNotStep[j].Depth = 0;
                        markParentWhoDoesNotStep[j].IsNodeWin = null;
                    }
                    else
                    {
                        markParentWhoStep[j].Depth = 0;
                        markParentWhoStep[j].IsNodeWin = null;
                        markParentWhoDoesNotStep[j].Depth = 1000;
                        markParentWhoDoesNotStep[j].IsNodeWin = false;
                    }
                }

                // Определение родительских узлов, которые надо помечать
                for (int j = 0; j < tree[i].Count; j++)
                {
                    // Определение выигрышных для whoStep узлов
                    if (whoDidThisStep == whoStep && tree[i][j].WhoWin == whoStep)
                    {
                        foreach (int parentNodeNumber in tree[i][j].ParentNodeNumbers)
                        {
                            markParentWhoStep[parentNodeNumber].IsNodeWin = true;
                            if (markParentWhoStep[parentNodeNumber].Depth > tree[i][j].StepCntToWin)
                            {
                                markParentWhoStep[parentNodeNumber].Depth = tree[i][j].StepCntToWin;
                            }
                        }
                    }
                    else if (whoDidThisStep != whoStep)
                    {
                        foreach (int parentNodeNumber in tree[i][j].ParentNodeNumbers)
                        {
                            if (tree[i][j].WhoWin != whoStep)
                            {
                                markParentWhoStep[parentNodeNumber].IsNodeWin = false;
                            }
                            else if (markParentWhoStep[parentNodeNumber].IsNodeWin != false)
                            {
                                markParentWhoStep[parentNodeNumber].IsNodeWin = true;
                                if (markParentWhoStep[parentNodeNumber].Depth < tree[i][j].StepCntToWin)
                                {
                                    markParentWhoStep[parentNodeNumber].Depth = tree[i][j].StepCntToWin;
                                }
                            }
                        }
                    }

                    // Определение выигрышных для whoDoesNotStep узлов
                    if (whoDidThisStep == WhoDoesntStep(whoStep) && tree[i][j].WhoWin == WhoDoesntStep(whoStep))
                    {
                        foreach (int parentNodeNumber in tree[i][j].ParentNodeNumbers)
                        {
                            markParentWhoDoesNotStep[parentNodeNumber].IsNodeWin = true;
                            if (markParentWhoDoesNotStep[parentNodeNumber].Depth > tree[i][j].StepCntToWin)
                            {
                                markParentWhoDoesNotStep[parentNodeNumber].Depth = tree[i][j].StepCntToWin;
                            }
                        }
                    }
                    else if (whoDidThisStep != WhoDoesntStep(whoStep))
                    {
                        foreach (int parentNodeNumber in tree[i][j].ParentNodeNumbers)
                        {
                            if (tree[i][j].WhoWin != whoStep)
                            {
                                markParentWhoDoesNotStep[parentNodeNumber].IsNodeWin = false;
                            }
                            else if (markParentWhoDoesNotStep[parentNodeNumber].IsNodeWin != false)
                            {
                                markParentWhoDoesNotStep[parentNodeNumber].IsNodeWin = true;
                                if (markParentWhoDoesNotStep[parentNodeNumber].Depth < tree[i][j].StepCntToWin)
                                {
                                    markParentWhoDoesNotStep[parentNodeNumber].Depth = tree[i][j].StepCntToWin;
                                }
                            }
                        }
                    }
                }

                // Помечание выигрышных родительских узлов для whoStep
                for (int j = 0; j < markParentWhoStep.Length; j++)
                {
                    if (markParentWhoStep[j].IsNodeWin == true && tree[i - 1][j].WhoWin == ObjectType.Empty)
                    {
                        Node changeNode = tree[i - 1][j];
                        changeNode.WhoWin = whoStep;
                        changeNode.StepCntToWin = markParentWhoStep[j].Depth + 1;
                        tree[i - 1][j] = changeNode;
                    }
                }

                // Помечание выигрышных родительских узлов для whoDoesNotStep
                for (int j = 0; j < markParentWhoDoesNotStep.Length; j++)
                {
                    if (markParentWhoDoesNotStep[j].IsNodeWin == true && tree[i - 1][j].WhoWin == ObjectType.Empty)
                    {
                        Node changeNode = tree[i - 1][j];
                        changeNode.WhoWin = WhoDoesntStep(whoStep);
                        changeNode.StepCntToWin = markParentWhoDoesNotStep[j].Depth + 1;
                        tree[i - 1][j] = changeNode;
                    }
                }
            }            
        }


        /// <summary>
        /// Выбрать случайный ход среди списка возможных ходов
        /// </summary>
        /// <param name="goodSteps">Список ходов</param>
        /// <returns></returns>
        private static int[] SelectRandomFromGoodSteps(IList<Node> goodSteps)
        {
            var rand = new Random();
            int val = rand.Next(goodSteps.Count);

            var result = new int[3];
            result[0] = goodSteps[val].Step[0];
            result[1] = goodSteps[val].Step[1];
            result[2] = goodSteps[val].StepCntToWin;
            return result;
        }


        /// <summary>
        /// Найти наиболее быстро выигрывающий ход, если он есть
        /// </summary>
        /// <param name="tree">Дерево с ходами</param>
        /// <param name="whoStep">Кто атакует</param>
        /// <returns></returns>
        private static int[] SelectWinStep(IList<List<Node>> tree, ObjectType whoStep)
        {
            // В 0-ой строке дерева будут лежать все возможные хода, с указанием того,
            // выигрышные они для whoStep и если да - то за сколько ходов 
            // Просматриваем их и определяем, за какое минимальное количество ходов whoStep
            // может победить
            int minStepCntToWin = -1;
            for (int j = 0; j < tree[0].Count; j++)
            {
                if (tree[0][j].WhoWin == whoStep &&
                    (minStepCntToWin == -1 || tree[0][j].StepCntToWin < minStepCntToWin))
                {
                    minStepCntToWin = tree[0][j].StepCntToWin;
                }
            }

            if (minStepCntToWin == -1)
            {
                return new int[0];
            }

            // Выбираем все хода с минимальным временем победы среди ходов 0-й строки дерева
            var goodSteps = new List<Node>();
            for (int j = 0; j < tree[0].Count; j++)
            {
                if (tree[0][j].WhoWin == whoStep && tree[0][j].StepCntToWin == minStepCntToWin)
                {
                    goodSteps.Add(tree[0][j]);
                }
            }

            // Выбираем среди них случайный ход
            return SelectRandomFromGoodSteps(goodSteps);
        }


        /// <summary>
        /// Найти защищающийся ход, если он есть. Если нету - то наименее быстро проигрывающий
        /// </summary>
        /// <param name="tree">Дерево с ходами</param>
        /// <param name="whoStep">Кто атакует</param>
        /// <returns></returns>
        private static int[] SelectDefenceStep(IList<List<Node>> tree, ObjectType whoStep)
        {
            // Выбираем все хода, в которых не побеждает whoStep
            var goodSteps = new List<Node>();
            for (int j = 0; j < tree[0].Count; j++)
            {
                if (tree[0][j].WhoWin != whoStep)
                {
                    goodSteps.Add(tree[0][j]);
                }
            }

            if (goodSteps.Count > 0)
            {
                // Выбираем среди них случайный ход
                return SelectRandomFromGoodSteps(goodSteps);
            }
            
            // Если защитных ходов нет - выбираем тот, который проигрывает дольше всего
            int minStepCntToWin = 1000;
            for (int j = 0; j < tree[0].Count; j++)
            {
                if (tree[0][j].StepCntToWin < minStepCntToWin)
                {
                    minStepCntToWin = tree[0][j].StepCntToWin;
                }
            }                

            // Выбираем все хода с максимальным временем победы среди ходов 0-й строки дерева
            goodSteps = new List<Node>();
            for (int j = 0; j < tree[0].Count; j++)
            {
                if (tree[0][j].StepCntToWin == minStepCntToWin)
                {
                    goodSteps.Add(tree[0][j]);
                }
            }

            // Выбираем среди них случайный ход
            return SelectRandomFromGoodSteps(goodSteps);
        }


        /// <summary>
        /// Поиск форсированно выигрывающего хода за указанного игрока
        /// </summary>
        /// <param name="whoStep">Кто атакует</param>
        /// <param name="isFindDefence">true - если мы ищем защитный ход</param>
        /// <returns></returns>
        private static int[] FindBestForceStep(ObjectType whoStep, bool isFindDefence)
        {
            var tree = new List<List<Node>>();    // Дерево атакующих ходов с исследуемыми позициями

            // Получаем все позиции из исходной для исследования. Ход делается указанным объектом
            List<Node> newLevel = isFindDefence 
                ? FindDefencePositions(m_Field, -1, WhoDoesntStep(whoStep)) 
                : FindAttacPositions(m_Field, -1, whoStep);

            while (newLevel.Count > 0)
            {
                /*int cnt123 = 0;
                foreach (Node node in newLevel)
                    if (node.WhoWin != ObjectType.Empty)
                        cnt123++;
                Console.WriteLine(cnt123);*/

                AddNewLevelToTree(tree, newLevel);

                if (tree[tree.Count - 1].Count > 2000 || tree.Count >= 10)
                {
                    break;
                }

                newLevel.Clear();
                int cnt = tree.Count - 1;
                var grandParentNumbersForDelete = new List<int>();
                for (int i = 0; i < tree[cnt].Count; i++)
                {
                    if (tree[cnt][i].WhoWin == ObjectType.Empty)
                    {
                        List<Node> newPositions = WhoDoesntStep(tree[cnt][i].WhoStep) == whoStep
                            ? FindAttacPositions(tree[cnt][i].Field, i, WhoDoesntStep(tree[cnt][i].WhoStep))
                            : FindDefencePositions(tree[cnt][i].Field, i, WhoDoesntStep(tree[cnt][i].WhoStep));
                        
                        // Если мы искали атакующие хода, а их нет - значит нет смысла исследовать
                        // хода из братьев данного узла. Запоминаем номер родителя и после нахождения
                        // всех доступных ходов - удлим все хода, у которых номер дедушки равен 
                        // запомненному нами номеру
                        if (WhoDoesntStep(tree[cnt][i].WhoStep) == whoStep && newPositions.Count == 0)
                        {
                            foreach (int parentNumber in tree[cnt][i].ParentNodeNumbers)
                            {
                                if (!grandParentNumbersForDelete.Contains(parentNumber))
                                {
                                    grandParentNumbersForDelete.Add(parentNumber);
                                }
                            }
                            Node changeNode = tree[cnt][i];
                            changeNode.WhoWin = WhoDoesntStep(whoStep);                            
                            tree[cnt][i] = changeNode;                            
                        }

                        // Если есть выигрышные узлы - то добавляем только их
                        // Если выигрышных узлов нет - добавляем все
                        bool isWinExist = false;
                        foreach (Node node in newPositions)
                        {
                            if (node.WhoWin != ObjectType.Empty)
                            {
                                isWinExist = true;
                                break;
                            }
                        }

                        foreach (Node node in newPositions)
                        {
                            if (!isWinExist || node.WhoWin != ObjectType.Empty)
                            {
                                AddOnlyNewNode(newLevel, node, GetStepsChain(tree, node));
                            }
                        }
                    }
                }

                // Удаление тех ходов, исследование которых не нужно
                if (grandParentNumbersForDelete.Count == 0)
                {
                    continue;
                }

                for (int i = 0; i < newLevel.Count; i++)
                {
                    // Получить список всех номеров дедушкиных узлов
                    var grandParents = new List<int>();
                    foreach (int parentNumber in newLevel[i].ParentNodeNumbers)
                    {
                        grandParents.AddRange(tree[cnt][parentNumber].ParentNodeNumbers);
                    }

                    // Посмореть, если хотя бы один из дедушкиных узлов не содержится в списке
                    // родительских узлов для узла, у которого нет атакующих ходов - то такой узел
                    // надо оставить.
                    bool isNeedDeleteNode = true;
                    foreach (int grandParentNumber in grandParents)
                    {
                        if (!grandParentNumbersForDelete.Contains(grandParentNumber))
                        {
                            isNeedDeleteNode = false;
                            break;
                        }
                    }

                    if (isNeedDeleteNode)
                    {
                        newLevel.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (tree.Count == 0)
            {
                return new int[0];
            }

            
            ReverseTrace(tree, whoStep);

            if (isFindDefence)
            {
                return SelectDefenceStep(tree, whoStep);
            }
            return SelectWinStep(tree, whoStep);
        }



        /// <summary>
        /// Исследование форсированных выигрышей за крестики и за нолики
        /// </summary>
        /// <returns></returns>
        private static int[] InvestigateForseAttack()
        {
            // Исследуем атаку компа. Исследуем все ходы, которые приводят к появлению
            // трёх и более объектов, через которые можно в итоге построить 5 в ряд
            // В результате получаем ход, который показывает, куда надо сходить и через сколько
            // ходов мы победим
            int[] attackStepInfo = FindBestForceStep(m_WhoStep, false);

            int[] defenceStepInfo = FindBestForceStep(WhoDoesntStep(m_WhoStep), true);

            if (attackStepInfo.Length == 0 && defenceStepInfo.Length == 0)
            {
                return new int[0];
            }

            if (attackStepInfo.Length != 0 && defenceStepInfo.Length != 0)
            {
                if (attackStepInfo[2] <= defenceStepInfo[2])
                {
                    return new[] { attackStepInfo[0], attackStepInfo[1] };
                }
                return new[] { defenceStepInfo[0], defenceStepInfo[1] };
            }

            if (attackStepInfo.Length != 0)
            {
                return new[] { attackStepInfo[0], attackStepInfo[1] };
            }

            return new[] { defenceStepInfo[0], defenceStepInfo[1] };
        }
        #endregion


        #region Поиск выигрывающей стратегии
        /// <summary>
        /// Получение оценки хода как сумма восьми длинн ряда от данной ячейки, умноженное на 3
        /// (только для тех рядов, в которых могут выстроиться 5 в ряд - ПОПРАВИТЬ!!!)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="whoStep"></param>
        /// <returns></returns>
        private static double GetCellCoeff(int x, int y, ObjectType whoStep)
        {
            const double val = 3;
            double coeff = 0;

            // Проверяем, можно ли поставить 5 в ряд по вертикали через x, y
            // Если да - то считаем коэффициенты для данных двух линий
            int ci1 = x - 1;
            int cj1 = y;
            int cnt = 1;
            while (ci1 >= 0 && m_Field[ci1, cj1] != WhoDoesntStep(whoStep))
            {
                cnt++;
                ci1--;
            }

            int ci2 = x + 1;
            int cj2 = y;
            while (ci2 < m_RowsCnt && m_Field[ci2, cj2] != WhoDoesntStep(whoStep))
            {
                cnt++;
                ci2++;
            }

            if (cnt >= 5)
            {
                // Смотрим, можно ли выиграть, поставив объект по вертикали вверху             
                cnt = 0;
                ci1 = x - 1;
                cj1 = y;
                while (ci1 >= 0 && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    ci1--;
                }
                coeff += val * cnt;

                // Смотрим, можно ли выиграть, поставив объект по вертикали внизу
                cnt = 0;
                ci1 = x + 1;
                cj1 = y;
                while (ci1 < m_RowsCnt && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    ci1++;
                }
                coeff += val * cnt;
            }

            // Проверяем, можно ли поставить 5 в ряд по горизонтали через x, y
            // Если да - то считаем коэффициенты для данных двух линий
            ci1 = x;
            cj1 = y - 1;
            cnt = 1;
            while (cj1 >= 0 && m_Field[ci1, cj1] != WhoDoesntStep(whoStep))
            {
                cnt++;
                cj1--;
            }

            ci2 = x;
            cj2 = y + 1;
            while (cj2 < m_ColumnsCnt && m_Field[ci2, cj2] != WhoDoesntStep(whoStep))
            {
                cnt++;
                cj2++;
            }

            if (cnt >= 5)
            {
                // Смотрим, можно ли выиграть, поставив объект по горизонтали влево
                cnt = 0;
                ci1 = x;
                cj1 = y - 1;
                while (cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    cj1--;
                }
                coeff += val * cnt;

                // Смотрим, можно ли выиграть, поставив объект по горизонтали вправо
                cnt = 0;
                ci1 = x;
                cj1 = y + 1;
                while (cj1 < m_ColumnsCnt && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    cj1++;
                }
                coeff += val * cnt;
            }

            // Проверяем, можно ли поставить 5 в ряд по диагонали через x, y слева сверху направо вниз
            // Если да - то считаем коэффициенты для данных двух линий
            ci1 = x - 1;
            cj1 = y - 1;
            cnt = 1;
            while (ci1 >= 0 && cj1 >= 0 && m_Field[ci1, cj1] != WhoDoesntStep(whoStep))
            {
                cnt++;
                ci1--;
                cj1--;
            }

            ci2 = x + 1;
            cj2 = y + 1;
            while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && m_Field[ci2, cj2] != WhoDoesntStep(whoStep))
            {
                cnt++;
                ci2++;
                cj2++;
            }

            if (cnt >= 5)
            {
                // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок влево вверх
                cnt = 0;
                ci1 = x - 1;
                cj1 = y - 1;
                while (ci1 >= 0 && cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    ci1--;
                    cj1--;
                }
                coeff += val * cnt;

                // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок вправо вниз
                cnt = 0;
                ci1 = x + 1;
                cj1 = y + 1;
                while (ci1 < m_RowsCnt && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    ci1++;
                    cj1++;
                }
                coeff += val * cnt;
            }

            // Проверяем, можно ли поставить 5 в ряд по диагонали через x, y слева снизу направо вверх
            // Если да - то считаем коэффициенты для данных двух линий
            ci1 = x - 1;
            cj1 = y + 1;
            cnt = 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] != WhoDoesntStep(whoStep))
            {
                cnt++;
                ci1--;
                cj1++;
            }

            ci2 = x + 1;
            cj2 = y - 1;
            while (ci2 < m_RowsCnt && cj2 >= 0 && m_Field[ci2, cj2] != WhoDoesntStep(whoStep))
            {
                cnt++;
                ci2++;
                cj2--;
            }

            if (cnt >= 5)
            {
                // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок вправо вверх
                cnt = 0;
                ci1 = x - 1;
                cj1 = y + 1;
                while (ci1 >= 0 && cj1 < m_ColumnsCnt && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    ci1--;
                    cj1++;
                }
                coeff += val * cnt;


                // Смотрим, можно ли выиграть, поставив объект по диагонали наискосок влево вниз
                cnt = 0;
                ci1 = x + 1;
                cj1 = y - 1;
                while (ci1 < m_RowsCnt && cj1 >= 0 && m_Field[ci1, cj1] == whoStep)
                {
                    cnt++;
                    ci1++;
                    cj1--;
                }
                coeff += val * cnt;
            }

            return coeff;
        }


        /// <summary>
        /// Поиск наилучшего хода, если не надо защищаться или атаковать наверняка
        /// </summary>
        /// <returns></returns>
        private static int[] FindAttackStep()
        {
            const double e = 0.2;
            const double q = 0.5;

            var defenceField = new double[m_RowsCnt, m_ColumnsCnt];
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (m_Field[i, j] != ObjectType.Empty)
                    {
                        defenceField[i, j] = 0;
                        continue;
                    }

                    double whoStepCoeff = GetCellCoeff(i, j, m_WhoStep);
                    double whoDoesntStepCoeff = GetCellCoeff(i, j, WhoDoesntStep(m_WhoStep));
                    defenceField[i, j] = whoStepCoeff + q * whoDoesntStepCoeff;
                }
            }

            // Найти самый высокий коэффициент
            int x = 0;
            int y = 0;
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (defenceField[i, j] > defenceField[x, y])
                    {
                        x = i;
                        y = j;
                    }
                }
            }

            // Если нету полей, в которые надо ставить объект - то выходим
            if (defenceField[x, y] == 0)
            {
                return new int[0];
            }

            // Получаем все ячейки с самым большим значением для защиты
            var rightFields = new List<int[]>();

            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (Math.Abs(defenceField[i, j] - defenceField[x, y]) < e)
                    {
                        rightFields.Add(new[] { i, j });
                    }
                }
            }

            // Выбираем случайное значение из этих ячеек
            var rand = new Random();
            int val = rand.Next(rightFields.Count);

            //m_PaintClass.DrawCoefficients(m_RowsCnt, m_ColumnsCnt, defenceField);
            return rightFields[val];
        }
        #endregion


        /// <summary>
        /// Поиск случайного хода на поле
        /// </summary>
        /// <returns></returns>
        private static int[] GetRandomStep()
        {
            var rand = new Random();
            int x;
            int y;
            do
            {
                x = rand.Next(m_RowsCnt);
                y = rand.Next(m_ColumnsCnt);
            }
            while (m_Field[x, y] != ObjectType.Empty);

            return new[] { x, y };
        }
    }
}
