using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TicTacToe
{
    public class ComputerClass
    {
        /// <summary>
        /// Структурка, которая хранит данные о том, надо ли помечать данный узел как выигрышный
        /// при просмотре узлов дерева и содержащая информацию о том, сколько ходов до выигрыша
        /// </summary>
        [DebuggerDisplay("IsNodeWin={IsNodeWin}  Depth={Depth}")]
        struct MarkAndDepth
        {
            /// <summary>
            /// True - если узел выигрышный, false - в противном случае
            /// </summary>
            public bool? IsNodeWin;

            /// <summary>
            /// Количество ходов до выигрыша
            /// </summary>
            public byte Depth;
        }

        /// <summary>
        /// Структура для хранения информации о ходе
        /// </summary>
        [DebuggerDisplay("X={X}  Y={Y}  WhoStep={WhoStep}")]
        class StepInfo
        {
            /// <summary>
            /// x-координата ячейки
            /// </summary>
            public byte X;

            /// <summary>
            /// y-координата ячейки
            /// </summary>
            public byte Y;

            /// <summary>
            /// Какой объект стоит в этой ячейке
            /// </summary>
            public ObjectType WhoStep;
        }


        /// <summary>
        /// Структура, содержащая список параметров одного состояния для исследования
        /// позиции вглубь 
        /// </summary>
        [DebuggerDisplay("Step={Step[0]}:{Step[1]}  WhoStep={WhoStep}  ParentNode={ParentNodeNumber}  WhoWin={WhoWin}")]
        class Node
        {
            /*/// <summary>
            /// Текущая позиция
            /// </summary>
            public ObjectType[,] Field;
            */
            /// <summary>
            /// Список ходов до данной позиции
            /// </summary>
            public List<StepInfo> StepsInfo;

            /// <summary>
            /// Кто сделал этот ход
            /// </summary>
            public ObjectType WhoStep;

            /// <summary>
            /// Ход, который привёл к этой позиции
            /// </summary>
            public byte[] Step;

            /// <summary>
            /// Номер родительского узла, откуда мы пришли сюда
            /// </summary>
            public int ParentNodeNumber;

            //public int Estimate;            // Оценка текущей позиции 

            /// <summary>
            /// Если понятно, что кто-то выиграл - то указать кто
            /// </summary>
            public ObjectType WhoWin;

            /// <summary>
            /// Количество ходов до победы
            /// </summary>
            public byte StepCntToWin;
        }

        /// <summary>
        /// Поле с текущей позицией
        /// </summary>
        private ObjectType[,] m_Field;

        /// <summary>
        /// Количество строк на поле
        /// </summary>
        private byte m_RowsCnt;

        /// <summary>
        /// Количество столбцов на поле
        /// </summary>
        private byte m_ColumnsCnt;

        /// <summary>
        /// Кто сейчас ходит
        /// </summary>
        private ObjectType m_WhoStep;

        /// <summary>
        /// Поле для рисования
        /// </summary>
        private PaintClass m_PaintClass;

        
        #region Разные вспомогательные функции
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
        /// <param name="fromArr">Массив, содержащий позицию на поле</param>
        /// <returns></returns>
        private ObjectType[,] CopyObjectTypeArray(ObjectType[,] fromArr)
        {
            var resArr = new ObjectType[m_RowsCnt, m_ColumnsCnt];
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
        /// Получить позицию из цепочки ходов, приводящих к ней
        /// </summary>
        /// <param name="stepsChain">Список ходов до позиции</param>        
        /// <returns></returns>
        private ObjectType[,] GetFieldFromStepsChain(IEnumerable<StepInfo> stepsChain)
        {
            var field = CopyObjectTypeArray(m_Field);

            foreach (StepInfo stepInfo in stepsChain)
            {
                field[stepInfo.X, stepInfo.Y] = stepInfo.WhoStep;
            }

            return field;
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
        #endregion

        /// <summary>
        /// Сделать ход за компьютер
        /// </summary>
        /// <param name="field">Текущая позиция</param>
        /// <param name="rowsCnt">Количество строк</param>
        /// <param name="columnsCnt">Количество столбцов</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="paintClass">Ссылка на класс для рисования</param>
        /// <returns></returns>
        private int[] DoStepNotStatic(ObjectType[,] field, byte rowsCnt, byte columnsCnt, ObjectType whoStep, PaintClass paintClass)
        {
            m_Field = field;
            m_RowsCnt = rowsCnt;
            m_ColumnsCnt = columnsCnt;
            m_WhoStep = whoStep;
            m_PaintClass = paintClass;

            //m_PaintClass.ClearCoefficients(m_RowsCnt, m_ColumnsCnt);            

            // Проверить, если поле пустое - то вернуть ячейку по центру
            int[] stepCoordinates = GetFirstStep();
            if (stepCoordinates.Length != 0)
            {
                MessageBox.Show("Первый ход");
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
                MessageBox.Show("Просто атакующий ход");
                return stepCoordinates;
            }

            // Если случился баг - то выбрать любое непустое место на поле случайным образом
            return GetRandomStep();
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
        public static int[] DoStep(ObjectType[,] field, byte rowsCnt, byte columnsCnt, ObjectType whoStep, PaintClass paintClass)
        {
            return new ComputerClass().DoStepNotStatic(field, rowsCnt, columnsCnt, whoStep, paintClass);
        }


        /// <summary>
        /// Проверить, если данный ход первый ход - то вернуть центральную клетку 
        /// в качестве первого хода
        /// </summary>
        /// <returns></returns>
        private int[] GetFirstStep()
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
                     

        #region Исследование форсированных выигрышей

        #region Проверка клетки, подходит ли она для атаки или защиты
        /// <summary>
        /// Проверить, создаёт ли атакующую (или защитную) цепочку проверяемая клетка
        /// </summary>        
        /// <returns></returns>
        private bool[] VerifyChain(bool findAtack, ObjectType[,] field, ObjectType whoStep,
            int len0, int len1, int len2, 
            int ci0, int ci1, int ci2, int ci3,
            int cj0, int cj1, int cj2, int cj3)
        {
            // Если есть 5 подряд или больше - то этот ход атакующий и выигрывающий
            if (len1 >= 5)
            {
                return new[] { true, true };
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
        ///  Ищем, сколько подряд по горизонтали
        /// </summary>
        /// <param name="field">Позиция</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>
        /// <param name="findAtack">True - если мы ищем атакующий ход, false - если защитный</param>
        /// <returns></returns>
        private bool[] FindGorizontal(
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

            return VerifyChain(findAtack, field, whoStep, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3);
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
        private bool[] FindVertical(
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

            return VerifyChain(findAtack, field, whoStep, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3);
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
        private bool[] FindDiagonalLeftTopRightBottom(
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

            return VerifyChain(findAtack, field, whoStep, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3);
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
        private bool[] FindDiagonalLeftBottomRightTop(
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

            return VerifyChain(findAtack, field, whoStep, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3);
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
        private bool[] IsThisStepAttackOrWin(
            ObjectType[,] field, ObjectType whoStep,
            byte[] step, bool findAtack)
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

            return new[] { false, false };
        }
        #endregion

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
        private bool[] IsThisStepDefenceOrWin(ObjectType[,] field, ObjectType whoStep, byte[] step)
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
        /// <param name="stepsInfo">Список ходов до исследуемой позиции</param>
        /// <param name="parentNodeNumber">Номер родительского узла</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <returns></returns>
        private List<Node> FindAttacPositions(IEnumerable<StepInfo> stepsInfo, int parentNodeNumber, ObjectType whoStep)
        {
            var positions = new List<Node>();
            ObjectType[,] field = GetFieldFromStepsChain(stepsInfo);

            for (byte i = 0; i < m_RowsCnt; i++)
            {
                for (byte j = 0; j < m_ColumnsCnt; j++)
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
                            StepsInfo = new List<StepInfo>(stepsInfo),
                            Step = new[] { i, j },
                            WhoStep = whoStep,
                            WhoWin = stepInfo[1] ? whoStep : ObjectType.Empty,
                            ParentNodeNumber = parentNodeNumber
                        };
                        var newStepInfo = new StepInfo { X = i, Y = j, WhoStep = whoStep };
                        newNode.StepsInfo.Add(newStepInfo);                        

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
        /// <param name="stepsInfo">Список ходов до исследуемой позиции</param>
        /// <param name="parentNodeNumber">Номер родительского узла</param>
        /// <param name="whoStep">Кто делает ход</param>
        /// <returns></returns>
        private List<Node> FindDefencePositions(IEnumerable<StepInfo> stepsInfo, int parentNodeNumber, ObjectType whoStep)
        {
            var positions = new List<Node>();

            ObjectType[,] field = GetFieldFromStepsChain(stepsInfo);

            for (byte i = 0; i < m_RowsCnt; i++)
            {
                for (byte j = 0; j < m_ColumnsCnt; j++)
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
                            StepsInfo = new List<StepInfo>(stepsInfo),
                            Step = new[] { i, j },
                            WhoStep = whoStep,
                            WhoWin = stepInfo[1] ? whoStep : ObjectType.Empty,
                            ParentNodeNumber = parentNodeNumber
                        };
                        var newStepInfo = new StepInfo {X = i, Y = j, WhoStep = whoStep};
                        newNode.StepsInfo.Add(newStepInfo);

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
        /// <param name="whoStep">Кто атакует</param>
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
                        markParentWhoStep[j].Depth = 255;
                        markParentWhoStep[j].IsNodeWin = false;
                        markParentWhoDoesNotStep[j].Depth = 0;
                        markParentWhoDoesNotStep[j].IsNodeWin = null;
                    }
                    else
                    {
                        markParentWhoStep[j].Depth = 0;
                        markParentWhoStep[j].IsNodeWin = null;
                        markParentWhoDoesNotStep[j].Depth = 255;
                        markParentWhoDoesNotStep[j].IsNodeWin = false;
                    }
                }

                // Определение родительских узлов, которые надо помечать как выигранные
                for (int j = 0; j < tree[i].Count; j++)
                {
                    // Определение выигрышных для whoStep узлов
                    if (whoDidThisStep == whoStep)                        
                    {
                        if (tree[i][j].WhoWin == whoStep)
                        {
                            markParentWhoStep[tree[i][j].ParentNodeNumber].IsNodeWin = true;
                            if (markParentWhoStep[tree[i][j].ParentNodeNumber].Depth > tree[i][j].StepCntToWin)
                            {
                                markParentWhoStep[tree[i][j].ParentNodeNumber].Depth = tree[i][j].StepCntToWin;
                            }
                        }
                    }
                    else
                    {
                        if (tree[i][j].WhoWin != whoStep)
                        {
                            markParentWhoStep[tree[i][j].ParentNodeNumber].IsNodeWin = false;
                        }
                        else if (markParentWhoStep[tree[i][j].ParentNodeNumber].IsNodeWin != false)
                        {
                            markParentWhoStep[tree[i][j].ParentNodeNumber].IsNodeWin = true;
                            if (markParentWhoStep[tree[i][j].ParentNodeNumber].Depth < tree[i][j].StepCntToWin)
                            {
                                markParentWhoStep[tree[i][j].ParentNodeNumber].Depth = tree[i][j].StepCntToWin;
                            }
                        }
                    }
                    
                    // Определение выигрышных для whoDoesNotStep узлов
                    if (whoDidThisStep == WhoDoesntStep(whoStep))
                    {
                        if (tree[i][j].WhoWin == WhoDoesntStep(whoStep))
                        {
                            markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].IsNodeWin = true;
                            if (markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].Depth > tree[i][j].StepCntToWin)
                            {
                                markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].Depth = tree[i][j].StepCntToWin;
                            }
                        }
                    }
                    else
                    {
                        if (tree[i][j].WhoWin != WhoDoesntStep(whoStep))
                        {
                            markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].IsNodeWin = false;
                        }
                        else if (markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].IsNodeWin != false)
                        {
                            markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].IsNodeWin = true;
                            if (markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].Depth < tree[i][j].StepCntToWin)
                            {
                                markParentWhoDoesNotStep[tree[i][j].ParentNodeNumber].Depth = tree[i][j].StepCntToWin;
                            }
                        }
                    }
                }

                // Помечание выигрышных родительских узлов для whoStep
                for (int j = 0; j < markParentWhoStep.Length; j++)
                {
                    if (markParentWhoStep[j].IsNodeWin == true && tree[i - 1][j].WhoWin == ObjectType.Empty)
                    {
                        tree[i - 1][j].WhoWin = whoStep;
                        tree[i - 1][j].StepCntToWin = (byte)(markParentWhoStep[j].Depth + 1);                        
                    }
                }
                
                // Помечание выигрышных родительских узлов для whoDoesNotStep
                for (int j = 0; j < markParentWhoDoesNotStep.Length; j++)
                {
                    if (markParentWhoDoesNotStep[j].IsNodeWin == true && tree[i - 1][j].WhoWin == ObjectType.Empty)
                    {
                        tree[i - 1][j].WhoWin = WhoDoesntStep(whoStep);
                        tree[i - 1][j].StepCntToWin = (byte)(markParentWhoDoesNotStep[j].Depth + 1);                        
                    }
                }
            }
        }


        /// <summary>
        /// Проход по всем узлам дерева. Если родители данного узла помечены как выигрышные - 
        /// то пометить и этот узел.
        /// </summary>
        /// <param name="tree">Дерево ходов</param>
        private static void MarkNodeTrace(IList<List<Node>> tree)
        {
            for (int i = 2; i < tree.Count; i++)
            {
                // Просмотр родительских узлов. Если хотя бы один из них помечен - то пометить
                // и наш узел
                for (int j = 0; j < tree[i].Count; j++)
                {
                    if (tree[i][j].WhoWin != ObjectType.Empty)
                    {
                        continue;
                    }
                    
                    if (tree[i - 1][tree[i][j].ParentNodeNumber].WhoWin != ObjectType.Empty)
                    {
                        tree[i][j].WhoWin = tree[i - 1][tree[i][j].ParentNodeNumber].WhoWin;
                    }
                }
            }
        }        


        /// <summary>
        /// Найти наиболее быстро выигрывающий ход, если он есть
        /// </summary>
        /// <param name="tree">Дерево с ходами</param>
        /// <param name="whoStep">Кто атакует</param>
        /// <returns></returns>
        private static List<Node> SelectWinSteps(IList<List<Node>> tree, ObjectType whoStep)
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
                return new List<Node>();
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
            return goodSteps;
        }


        /// <summary>
        /// Найти защищающийся ход, если он есть. Если нету - то наименее быстро проигрывающий
        /// </summary>
        /// <param name="tree">Дерево с ходами</param>
        /// <param name="whoStep">Кто атакует</param>
        /// <returns></returns>
        private static List<Node> SelectDefenceSteps(IList<List<Node>> tree, ObjectType whoStep)
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
                return goodSteps;
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
            return goodSteps;
        }


        /// <summary>
        /// Поиск форсированно выигрывающего хода за указанного игрока
        /// </summary>
        /// <param name="whoStep">Кто атакует</param>
        /// <param name="isFindDefence">Ищем мы защитные хода или атакующие</param>        
        /// <returns></returns>
        private List<Node> FindForceSteps(ObjectType whoStep, bool isFindDefence)
        {
            // Получаем все позиции из исходной для исследования. Ход делается указанным объектом
            List<Node> newLevel = FindAttacPositions(new StepInfo[0], -1, whoStep);

            return FindForceSteps(whoStep, isFindDefence, newLevel);
        }


        /// <summary>
        /// Поиск форсированно выигрывающего хода за указанного игрока
        /// </summary>
        /// <param name="whoStep">Кто атакует</param>
        /// <param name="isFindDefence">Ищем мы защитные хода или атакующие</param>
        /// <param name="newLevel">Список ходов первого уровня дерева</param>
        /// <returns></returns>
        private List<Node> FindForceSteps(ObjectType whoStep, bool isFindDefence, ICollection<Node> newLevel)
        {
            var tree = new List<List<Node>>();    // Дерево атакующих ходов с исследуемыми позициями            

            while (newLevel.Count > 0)
            {
                AddNewLevelToTree(tree, newLevel);                

                ReverseTrace(tree, whoStep);

                if (!isFindDefence)
                {
                    List<Node> winStep = SelectWinSteps(tree, whoStep);
                    if (winStep.Count > 0)
                    {
                        return winStep;
                    }
                }

                if (tree[tree.Count - 1].Count > 100000)
                {                    
                    break;
                }
                
                MarkNodeTrace(tree);                
                
                newLevel.Clear();
                int cnt = tree.Count - 1;
                for (int i = 0; i < tree[cnt].Count; i++)
                {
                    if (tree[cnt][i].WhoWin == ObjectType.Empty)
                    {
                        List<Node> newPositions = WhoDoesntStep(tree[cnt][i].WhoStep) == whoStep
                            ? FindAttacPositions(tree[cnt][i].StepsInfo, i, WhoDoesntStep(tree[cnt][i].WhoStep))
                            : FindDefencePositions(tree[cnt][i].StepsInfo, i, WhoDoesntStep(tree[cnt][i].WhoStep));

                        // Если мы искали атакующие хода, а их нет - значит нет смысла исследовать
                        // хода из братьев данного узла. Помечаем узел как выигрышный за противника
                        // и после обратного хода все его братья пометятся как не требующие исследования
                        if (WhoDoesntStep(tree[cnt][i].WhoStep) == whoStep && newPositions.Count == 0)
                        {
                            tree[cnt][i].WhoWin = WhoDoesntStep(whoStep);                            
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
                                newLevel.Add(node);                                
                            }
                        }
                    }
                }

            }

            if (tree.Count == 0)
            {
                return new List<Node>();
            }

            ReverseTrace(tree, whoStep);

            if (isFindDefence)
            {
                return SelectDefenceSteps(tree, whoStep);
            }
            return SelectWinSteps(tree, whoStep);
        }



        /// <summary>
        /// Исследование форсированных выигрышей за крестики и за нолики
        /// </summary>
        /// <returns></returns>
        private int[] InvestigateForseAttack()
        {
            // Исследуем атаку компа. Исследуем все ходы, которые приводят к появлению
            // трёх и более объектов, через которые можно в итоге построить 5 в ряд
            // В результате получаем ход, который показывает, куда надо сходить и через сколько
            // ходов мы победим
            List<Node> attackStepInfo = FindForceSteps(m_WhoStep, false);

            List<Node> defenceStepInfo = FindForceSteps(WhoDoesntStep(m_WhoStep), false);

            if (attackStepInfo.Count == 0 && defenceStepInfo.Count == 0)
            {
                return new int[0];
            }

            int[] randStep;

            if (attackStepInfo.Count != 0 && defenceStepInfo.Count != 0)
            {
                if (attackStepInfo[0].StepCntToWin <= defenceStepInfo[0].StepCntToWin)
                {
                    randStep = SelectRandomFromGoodSteps(attackStepInfo);
                    MessageBox.Show("Форсированный атакующий ход");
                    return new[] { randStep[0], randStep[1] };
                }
                if (defenceStepInfo.Count > 1)
                {
                    defenceStepInfo = FindForceSteps(WhoDoesntStep(m_WhoStep), true, defenceStepInfo);
                }
                randStep = SelectRandomFromGoodSteps(defenceStepInfo);
                MessageBox.Show("Форсированный защитный ход");
                return new[] { randStep[0], randStep[1] };
            }

            if (attackStepInfo.Count != 0)
            {
                randStep = SelectRandomFromGoodSteps(attackStepInfo);
                MessageBox.Show("Форсированный атакующий ход");
                return new[] { randStep[0], randStep[1] };
            }

            if (defenceStepInfo.Count > 1)
            {
                defenceStepInfo = FindForceSteps(WhoDoesntStep(m_WhoStep), true, InvertSteps(defenceStepInfo));
            }
            randStep = SelectRandomFromGoodSteps(defenceStepInfo);
            MessageBox.Show("Форсированный защитный ход");
            return new[] { randStep[0], randStep[1] };
        }


        /// <summary>
        /// Сделать так, чтобы узлы, допустим, за крестики, стали узлами за нолики и наоборот
        /// </summary>
        /// <param name="stepInfo">Список узлов</param>
        /// <returns></returns>
        private static List<Node> InvertSteps(List<Node> stepInfo)
        {
            for (int i = 0; i < stepInfo.Count; i++)
            {
                stepInfo[i].WhoWin = ObjectType.Empty;
                stepInfo[i].WhoStep = WhoDoesntStep(stepInfo[i].WhoStep);
                stepInfo[i].StepsInfo[stepInfo[i].StepsInfo.Count - 1].WhoStep = WhoDoesntStep(stepInfo[i].WhoStep);                
                /*Node changeNode = stepInfo[i];                
                changeNode.WhoWin = ObjectType.Empty;
                changeNode.WhoStep = WhoDoesntStep(changeNode.WhoStep);
                changeNode.Field[changeNode.Step[0], changeNode.Step[1]] = changeNode.WhoStep;
                stepInfo[i] = changeNode;*/
            }
            return stepInfo;
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
        private double GetCellCoeff(int x, int y, ObjectType whoStep)
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
        private int[] FindAttackStep()
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
        private int[] GetRandomStep()
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
