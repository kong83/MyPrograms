using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TicTacToe
{
    /// <summary>
    /// Структура для хранения информации о ходе
    /// </summary>
    [DebuggerDisplay("X={X}  Y={Y}  WhoStep={WhoStep}")]
    public struct StepInfo
    {
        /// <summary>
        /// x-координата ячейки
        /// </summary>
        public int X;

        /// <summary>
        /// y-координата ячейки
        /// </summary>
        public int Y;

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
    public class Node
    {
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
        public int[] Step;

        /// <summary>
        /// Номер родительского узла, откуда мы пришли сюда
        /// </summary>
        public int ParentNodeNumber;

        /// <summary>
        /// Есть ли дети у данного узла
        /// </summary>
        public bool HasChild;

        /// <summary>
        /// Если понятно, что кто-то выиграл - то указать кто
        /// </summary>
        public ObjectType WhoWin;

        /// <summary>
        /// Количество ходов до победы
        /// </summary>
        public int StepCntToWin;
    }

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
            public int Depth;
        }


        /// <summary>
        /// Поле с текущей позицией
        /// </summary>
        private ObjectType[,] m_Field;

        /// <summary>
        /// Количество строк на поле
        /// </summary>
        private int m_RowsCnt;

        /// <summary>
        /// Количество столбцов на поле
        /// </summary>
        private int m_ColumnsCnt;

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
        /// Добавить новый уровень в дерево возможных ходов
        /// </summary>
        /// <param name="tree">Общее дерево с ходами</param>
        /// <param name="newLevel">Новые узлы</param>
        /// <param name="treeNumber">Номер строки в общем дереве, куда надо добавить узлы</param>
        private static void AddNewLevelToTree(IList<List<Node>> tree, IEnumerable<Node> newLevel, int treeNumber)
        {
            if (tree.Count > treeNumber)
            {
                tree[treeNumber].AddRange(newLevel);
            }
            else
            {
                var temp = new List<Node>(newLevel);
                tree.Add(temp);
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

            var result = new int[2];
            result[0] = goodSteps[val].Step[0];
            result[1] = goodSteps[val].Step[1];            
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
        private int[] DoStepNotStatic(ObjectType[,] field, int rowsCnt, int columnsCnt, ObjectType whoStep, PaintClass paintClass)
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
        public static int[] DoStep(ObjectType[,] field, int rowsCnt, int columnsCnt, ObjectType whoStep, PaintClass paintClass)
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

            /*
             Когда ищем обратный ход, надо:
1. Определить 5 клеток победной линии, а потом все пятерные линии, которые проходят через неё. И так далее, для всех 
пятерных линий, проходящих через уже найденные, пока мы не найдём все пятерные линии (линии из атакующих объектов +
те клетки, в которых стоят защищающиеся объекты).
2. Создать массив необходимых клеток и выбросить все хода, не относящиеся к этому массиву.
3. Поменять информацию об узлах так, чтобы у отца был указатель на сына, который надо брать в победной 
последовательности ходов.
             */
            for (int i = tree.Count - 1; i > 1; i--)
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
                        tree[i - 1][j].StepCntToWin = (markParentWhoStep[j].Depth + 1);
                    }
                }

                // Помечание выигрышных родительских узлов для whoDoesNotStep
                for (int j = 0; j < markParentWhoDoesNotStep.Length; j++)
                {
                    if (markParentWhoDoesNotStep[j].IsNodeWin == true && tree[i - 1][j].WhoWin == ObjectType.Empty)
                    {
                        tree[i - 1][j].WhoWin = WhoDoesntStep(whoStep);
                        tree[i - 1][j].StepCntToWin = markParentWhoDoesNotStep[j].Depth + 1;
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
            if (tree.Count == 1)
            {
                return new List<Node>();
            }
            int minStepCntToWin = -1;
            for (int j = 0; j < tree[1].Count; j++)
            {
                if (tree[1][j].WhoWin == whoStep &&
                    (minStepCntToWin == -1 || tree[1][j].StepCntToWin < minStepCntToWin))
                {
                    minStepCntToWin = tree[1][j].StepCntToWin;
                }
            }

            if (minStepCntToWin == -1)
            {
                return new List<Node>();
            }

            // Выбираем все хода с минимальным временем победы среди ходов 0-й строки дерева
            var goodSteps = new List<Node>();
            for (int j = 0; j < tree[1].Count; j++)
            {
                if (tree[1][j].WhoWin == whoStep && tree[1][j].StepCntToWin == minStepCntToWin)
                {
                    goodSteps.Add(tree[1][j]);
                }
            }

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
            for (int j = 0; j < tree[1].Count; j++)
            {
                if (tree[1][j].WhoWin != whoStep)
                {
                    goodSteps.Add(tree[1][j]);
                }
            }

            if (goodSteps.Count > 0)
            {
                // Выбираем среди них случайный ход
                return goodSteps;
            }

            // Если защитных ходов нет - выбираем тот, который проигрывает дольше всего
            int minStepCntToWin = 1000;
            for (int j = 0; j < tree[1].Count; j++)
            {
                if (tree[1][j].StepCntToWin < minStepCntToWin)
                {
                    minStepCntToWin = tree[1][j].StepCntToWin;
                }
            }

            // Выбираем все хода с максимальным временем победы среди ходов 0-й строки дерева
            goodSteps = new List<Node>();
            for (int j = 0; j < tree[1].Count; j++)
            {
                if (tree[1][j].StepCntToWin == minStepCntToWin)
                {
                    goodSteps.Add(tree[1][j]);
                }
            }

            // Выбираем среди них случайный ход
            return goodSteps;
        }


        /// <summary>
        /// Поиск форсированно выигрывающего хода за указанного игрока
        /// </summary>        
        /// <param name="firstNode">Узел, позицию которого надо исследовать</param>
        /// <param name="isFindDefence">Ищем мы атакующие хода, или защитные</param>
        /// <returns></returns>
        private List<Node> FindForceSteps(Node firstNode, bool isFindDefence)
        {
            // Дерево атакующих ходов с исследуемыми позициями
            var tree = new List<List<Node>> { new List<Node>() };
            tree[0].Add(firstNode);

            int treeNumber = 0;            
            while (treeNumber < tree.Count)
            {                
                for (int i = 0; i < tree[treeNumber].Count; i++)
                {
                    if (tree[treeNumber][i].WhoWin == ObjectType.Empty && !tree[treeNumber][i].HasChild)
                    {
                        List<List<Node>> newPositionsTree = InvestigateFieldClass.FindPositionsForLine(tree[treeNumber][i], i, m_Field, m_RowsCnt, m_ColumnsCnt);

                        if (newPositionsTree.Count == 0)
                        {
                            tree[treeNumber][i].WhoWin = firstNode.WhoStep;
                        }
                        else
                        {                            
                            int num = treeNumber + 1;
                            // Добавляем в общее дерево дерево с исследованием одной линии
                            for (int n = 0; n < newPositionsTree.Count; n++)
                            {
                                if (n > 0)
                                {
                                    int cnt = tree[num - 1].Count - newPositionsTree[n - 1].Count;
                                    for (int q = 0; cnt > 0 && q < newPositionsTree[n].Count; q++)
                                    {
                                        newPositionsTree[n][q].ParentNodeNumber += cnt;
                                    }
                                }
                                AddNewLevelToTree(tree, newPositionsTree[n], num);
                                num++;
                            }
                        }
                    }
                }
                treeNumber++;
            }

            firstNode.HasChild = false;

            if (tree.Count == 0)
            {
                return new List<Node>();
            }

            ReverseTrace(tree, WhoDoesntStep(firstNode.WhoStep));

            if (isFindDefence)
            {
                return SelectDefenceSteps(tree, firstNode.WhoStep);
            }
            return SelectWinSteps(tree, WhoDoesntStep(firstNode.WhoStep));
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
            var currentNode = new Node
            {
                WhoStep = WhoDoesntStep(m_WhoStep),
                WhoWin = ObjectType.Empty,
                ParentNodeNumber = -1,
                StepsInfo = new List<StepInfo>()
            };

            List<Node> attackStepInfo = FindForceSteps(currentNode, false);                        
            
            currentNode.WhoStep = m_WhoStep;
            List<Node> defenceStepInfo = FindForceSteps(currentNode, false);

            if (attackStepInfo.Count == 0 && defenceStepInfo.Count == 0)
            {
                return new int[0];
            }

            if (attackStepInfo.Count != 0 && defenceStepInfo.Count != 0)
            {
                if (attackStepInfo[0].StepCntToWin <= defenceStepInfo[0].StepCntToWin)
                {
                    MessageBox.Show("Форсированный атакующий ход");
                    return SelectRandomFromGoodSteps(attackStepInfo);
                }
                if (defenceStepInfo.Count > 1)
                {
                    defenceStepInfo = FindForceSteps(currentNode, true);
                }
                MessageBox.Show("Форсированный защитный ход");
                return SelectRandomFromGoodSteps(defenceStepInfo);
            }

            if (attackStepInfo.Count != 0)
            {
                MessageBox.Show("Форсированный атакующий ход");
                return SelectRandomFromGoodSteps(attackStepInfo);
            }

            if (defenceStepInfo.Count > 1)
            {
                defenceStepInfo = FindForceSteps(currentNode, true);
            }
            MessageBox.Show("Форсированный защитный ход");
            return SelectRandomFromGoodSteps(defenceStepInfo);
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

            MessageBox.Show("Случайный ход");

            return new[] { x, y };
        }
    }
}
