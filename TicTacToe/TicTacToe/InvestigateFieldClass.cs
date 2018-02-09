using System.Collections.Generic;
using System.Diagnostics;

namespace TicTacToe
{
    /// <summary>
    /// Класс для поиска всех атакующих позиций для одной линии
    /// </summary>
    public static class InvestigateFieldClass
    {
        [DebuggerDisplay("X={X}  Y={Y}  Figure={Figure}")]
        struct Line
        {
            public int X;
            public int Y;
            public ObjectType Figure;
        }

        /// <summary>
        /// Поле с начальной позицией, для которой ищется ход за компьютер
        /// </summary>
        private static ObjectType[,] m_Field;

        /// <summary>
        /// Атакующий объект
        /// </summary>
        private static ObjectType m_WhoStep;

        /// <summary>
        /// Защищающийся объект
        /// </summary>
        private static ObjectType m_WhoDoesntStep;

        /// <summary>
        /// Количество строк на поле
        /// </summary>
        private static int m_RowsCnt;

        /// <summary>
        /// Количество столбцов на поле
        /// </summary>
        private static int m_ColumnsCnt;

        /// <summary>
        /// Линия из объектов для исследования
        /// </summary>
        private static List<Line> m_Line;

        /// <summary>
        /// Исследовать позицию для данного узла и найти все возможные атаки и защиты для
        /// одной из атакующих линий (если они есть)
        /// </summary>
        /// <param name="currentNode">Текущий узел</param>
        /// <param name="parentNodeNumber">Номер этого узла в списке ходов</param>
        /// <param name="field">Поле для которого мы ищем ход за компьютер</param>
        /// <param name="rowsCnt">Количество строк в этом поле</param>
        /// <param name="columnsCnt">Количество столбцов в этом поле</param>
        /// <returns></returns>
        public static List<List<Node>> FindPositionsForLine(Node currentNode, int parentNodeNumber, ObjectType[,] field, int rowsCnt, int columnsCnt)
        {
            var tree = new List<List<Node>>();

            m_WhoDoesntStep = currentNode.WhoStep;
            m_WhoStep = currentNode.WhoStep == ObjectType.Cross
                ? ObjectType.Nil
                : ObjectType.Cross;
            m_Field = field;
            m_RowsCnt = rowsCnt;
            m_ColumnsCnt = columnsCnt;

            m_Line = FindNewLine(GetFieldFromStepsChain(currentNode.StepsInfo));

            List<Node> newLevel = FindAttacPositions(new StepInfo[0], parentNodeNumber);

            while (newLevel.Count > 0)
            {
                AddNewLevelToTree(tree, newLevel);

                newLevel.Clear();
                int cnt = tree.Count - 1;
                for (int i = 0; i < tree[cnt].Count; i++)
                {
                    if (tree[cnt][i].WhoWin == ObjectType.Empty)
                    {
                        List<Node> newPositions = tree[cnt][i].WhoStep == m_WhoDoesntStep
                            ? FindAttacPositions(tree[cnt][i].StepsInfo, i)
                            : FindDefencePositions(tree[cnt][i].StepsInfo, i);

                        if (newPositions.Count > 0)
                        {
                            tree[cnt][i].HasChild = true;
                            newLevel.AddRange(newPositions);
                        }
                        
                    }
                }

            }

            if (tree.Count > 0)
            {
                currentNode.HasChild = true;
            }
            return tree;
        }


        /// <summary>
        /// Найти атакующие хода в исследуемой линии m_Line. Ищем хода за m_WhoStep.
        /// Возвращаем только правильные хода, которые не надо фильтровать
        /// </summary>
        /// <param name="stepInfos"></param>
        /// <param name="parentNodeNumber"></param>
        /// <returns></returns>
        private static List<Node> FindAttacPositions(IEnumerable<StepInfo> stepInfos, int parentNodeNumber)
        {
            var newPositions = new List<Node>();
            List<Line> line = GetLineFromStepsChain(stepInfos);

            var isWinNumbers = new List<int>();
            var isOneStepWinNumbers = new List<int>();
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].Figure != ObjectType.Empty)
                {
                    continue;
                }

                int len0 = 0, len1 = 1, len2 = 0;
                int i1 = i - 1;
                while (i1 >= 0 && line[i1].Figure == m_WhoStep)
                {
                    i1--;
                    len1++;
                }

                int i0 = i1;
                if (i0 > 0 && line[i0].Figure == ObjectType.Empty)
                {
                    i0--;
                    while (i0 >= 0 && line[i0].Figure == m_WhoStep)
                    {
                        i0--;
                        len0++;
                    }
                }
                int i2 = i + 1;
                while (i2 < line.Count && line[i2].Figure == m_WhoStep)
                {
                    i2++;
                    len1++;
                }

                int i3 = i2;
                if (i3 < line.Count - 1 && line[i3].Figure == ObjectType.Empty)
                {
                    i3++;
                    while (i3 < line.Count && line[i3].Figure == m_WhoStep)
                    {
                        i3++;
                        len2++;
                    }
                }

                bool[] res = VerifyChain(line, len0, len1, len2, i0, i1, i2, i3);
                if (res[0])
                {
                    var temp = new Node
                    {
                        ParentNodeNumber = parentNodeNumber,
                        WhoWin = ObjectType.Empty,
                        WhoStep = m_WhoStep,
                        StepsInfo = new List<StepInfo>(stepInfos),
                        Step = new[] { line[i].X, line[i].Y }
                    };                    
                    temp.StepsInfo.Add(new StepInfo { WhoStep = m_WhoStep, X = line[i].X, Y = line[i].Y });
                    if (res[1])
                    {
                        temp.WhoWin = m_WhoStep;
                    }
                    newPositions.Add(temp);
                    if (res[1])
                    {
                        isWinNumbers.Add(newPositions.Count - 1);
                    }
                    else if (res[2])
                    {
                        isOneStepWinNumbers.Add(newPositions.Count - 1);
                    }
                }
            }

            // Выкинуть ненужные хода
            if (isWinNumbers.Count > 0 && newPositions.Count != isWinNumbers.Count)
            {
                RemoveAllExceptSelected(newPositions, isWinNumbers);
            }
            else if (isOneStepWinNumbers.Count > 0 && newPositions.Count != isOneStepWinNumbers.Count)
            {
                RemoveAllExceptSelected(newPositions, isOneStepWinNumbers);
            }

            return newPositions;
        }


        /// <summary>
        /// Проверить, создаёт ли атакующую (или защитную) цепочку проверяемая клетка
        /// </summary>        
        /// <returns></returns>
        private static bool[] VerifyChain(IList<Line> line,
            int len0, int len1, int len2,
            int i0, int i1, int i2, int i3)
        {
            // Если есть 5 подряд или больше - то этот ход атакующий и выигрывающий
            if (len1 >= 5)
            {
                return new[] { true, true, false };
            }

            // Если есть 4 подряд и справа и слева есть пустая клетка - то этот ход почти выигрывающий
            if (len1 == 4 &&
                i1 >= 0 && line[i1].Figure == ObjectType.Empty &&
                i2 < line.Count && line[i2].Figure == ObjectType.Empty)
            {
                return new[] { true, false, true };
            }

            // Если есть 4 подряд и справа или слева есть пустая клетка - то этот ход - атакующий
            if (len1 == 4 &&
                ((i1 >= 0 && line[i1].Figure == ObjectType.Empty) ||
                (i2 < line.Count && line[i2].Figure == ObjectType.Empty)))
            {
                return new[] { true, false, false };
            }

            // Если есть 3 подряд, справа и слева есть пустые клетки и хотя бы с одной
            // стороны в след. клетке не стоит объект противники - то это ход - атакующий
            if (len1 == 3 && i1 >= 0 && i2 < line.Count &&
                line[i1].Figure == ObjectType.Empty && line[i2].Figure == ObjectType.Empty &&
                ((i1 >= 1 && line[i1 - 1].Figure != m_WhoDoesntStep) ||
                (i2 < line.Count - 1 && line[i2 + 1].Figure != m_WhoDoesntStep)))
            {
                return new[] { true, false, false };
            }

            // Если в сумме две последовательности дают 4 или больше - значит пробел между 
            // последовательностями - это следующий выигрышный ход
            if ((len0 > 0 && len0 + len1 == 4) ||
                (len2 > 0 && len1 + len2 == 4))
            {
                return new[] { true, false, false };
            }

            // Если в сумме получается 3 объекта - то проверяем, есть ли с краёв последовательности
            // место под 5-ый объект. Тогда пустая клетка между посдедовательностями будет
            // 4-ой выигрывающей клеткой при следующем ходе. Проверяем сначала центральную и
            // левую последовательности, потом центральную и правую
            if (len0 > 0 && len0 + len1 == 3 &&
                i0 >= 0 && line[i0].Figure == ObjectType.Empty &&
                i2 < line.Count && line[i2].Figure == ObjectType.Empty)
            {
                return new[] { true, false, false };
            }

            if (len2 > 0 && len1 + len2 == 3 &&
                i1 >= 0 && line[i1].Figure == ObjectType.Empty &&
                i3 < line.Count && line[i3].Figure == ObjectType.Empty)
            {
                return new[] { true, false, false };
            }

            return new[] { false, false, false };
        }


        /// <summary>
        /// Найти защитные хода в исследуемой линии m_Line. Ищем хода за m_WhoDoesntStep.
        /// Возвращаем только правильные хода, которые не надо фильтровать
        /// </summary>
        /// <param name="stepInfos"></param>
        /// <param name="parentNodeNumber"></param>
        /// <returns></returns>
        private static List<Node> FindDefencePositions(IEnumerable<StepInfo> stepInfos, int parentNodeNumber)
        {
            var newPositions = new List<Node>();

            // Проверить, может быть можно выиграть за защищающегося
            int[] winStep = FindOneWinStepForEnemy(stepInfos);
            if (winStep.Length > 0)
            {
                var temp = new Node
                {
                    ParentNodeNumber = parentNodeNumber,
                    WhoWin = m_WhoDoesntStep,
                    WhoStep = m_WhoDoesntStep,
                    StepsInfo = new List<StepInfo>(stepInfos),
                    Step = new[] { winStep[0], winStep[1] }
                };
                temp.StepsInfo.Add(new StepInfo { WhoStep = m_WhoDoesntStep, X = winStep[0], Y = winStep[1] });
                newPositions.Add(temp);

                return newPositions;
            }

            List<Line> line = GetLineFromStepsChain(stepInfos);
            var isLoseNumbers = new List<int>();
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].Figure != ObjectType.Empty)
                {
                    continue;
                }                

                int len1 = 1;

                int i1 = i - 1;
                while (i1 >= 0 && line[i1].Figure == m_WhoStep)
                {
                    i1--;
                    len1++;
                }

                int i2 = i + 1;
                while (i2 < line.Count && line[i2].Figure == m_WhoStep)
                {
                    i2++;
                    len1++;
                }

                // Если есть 5 подряд или больше - то этот ход атакующий и выигрывающий
                if (len1 >= 5 || (len1 == 4 &&
                    i1 >= 0 && line[i1].Figure == ObjectType.Empty &&
                    i2 < line.Count && line[i2].Figure == ObjectType.Empty))
                {
                    var temp = new Node
                    {
                        ParentNodeNumber = parentNodeNumber,
                        WhoWin = ObjectType.Empty,
                        WhoStep = m_WhoDoesntStep,
                        StepsInfo = new List<StepInfo>(stepInfos),
                        Step = new[] { line[i].X, line[i].Y }
                    };
                    temp.StepsInfo.Add(new StepInfo { WhoStep = m_WhoDoesntStep, X = line[i].X, Y = line[i].Y });

                    newPositions.Add(temp);

                    if (len1 >= 5)
                    {
                        isLoseNumbers.Add(newPositions.Count - 1);
                    }
                }
            }

            // Выкинуть ненужные хода            
            if (isLoseNumbers.Count > 0 && newPositions.Count != isLoseNumbers.Count)
            {
                RemoveAllExceptSelected(newPositions, isLoseNumbers);
            }

            return newPositions;
        }


        /// <summary>
        /// Проверить позицию. Нет ли в ней выигрыша за противника
        /// </summary>
        /// <param name="stepsInfo"></param>
        /// <returns></returns>
        private static int[] FindOneWinStepForEnemy(IEnumerable<StepInfo> stepsInfo)
        {
            ObjectType[,] field = GetFieldFromStepsChain(stepsInfo);

            for (int x = 0; x < m_RowsCnt; x++)
            {
                for (int y = 0; y < m_ColumnsCnt; y++)
                {
                    // Смотрим, является ли наша клетка пятой подряд
                    // Ищем, сколько подряд по горизонтали
                    int len = 1;
                    int ci1 = x - 1;
                    int cj1 = y;
                    while (ci1 >= 0 && field[ci1, cj1] == m_WhoDoesntStep)
                    {
                        ci1--;
                        len++;
                    }

                    int ci2 = x + 1;
                    int cj2 = y;
                    while (ci2 < m_RowsCnt && field[ci2, cj2] == m_WhoDoesntStep)
                    {
                        ci2++;
                        len++;
                    }

                    if (len >= 5)
                    {
                        return new[] { x, y };
                    }

                    // Ищем, сколько подряд по вертикали
                    len = 1;
                    ci1 = x;
                    cj1 = y - 1;
                    while (cj1 >= 0 && field[ci1, cj1] == m_WhoDoesntStep)
                    {
                        cj1--;
                        len++;
                    }

                    ci2 = x;
                    cj2 = y + 1;
                    while (cj2 < m_ColumnsCnt && field[ci2, cj2] == m_WhoDoesntStep)
                    {
                        cj2++;
                        len++;
                    }

                    if (len >= 5)
                    {
                        return new[] { x, y };
                    }

                    // Ищем, сколько подряд наискосок с левого верха до правого низа
                    len = 1;
                    ci1 = x - 1;
                    cj1 = y - 1;
                    while (ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == m_WhoDoesntStep)
                    {
                        ci1--;
                        cj1--;
                        len++;
                    }

                    ci2 = x + 1;
                    cj2 = y + 1;
                    while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == m_WhoDoesntStep)
                    {
                        ci2++;
                        cj2++;
                        len++;
                    }

                    if (len >= 5)
                    {
                        return new[] { x, y };
                    }

                    // Ищем, сколько подряд наискосок с левого низа до правого верха
                    len = 1;
                    ci1 = x - 1;
                    cj1 = y + 1;
                    while (ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == m_WhoDoesntStep)
                    {
                        ci1--;
                        cj1++;
                        len++;
                    }

                    ci2 = x + 1;
                    cj2 = y - 1;
                    while (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == m_WhoDoesntStep)
                    {
                        ci2++;
                        cj2--;
                        len++;
                    }

                    if (len >= 5)
                    {
                        return new[] { x, y };
                    }
                }
            }
            return new int[0];
        }


        #region Поиск атакующей линии
        /// <summary>
        /// Проверить, является ли переданная клетка атакующей и возвратить направление линии и все объекты линии
        /// </summary>        
        /// <returns></returns>
        private static List<Line> FindNewLine(ObjectType[,] field)
        {
            var line = new List<Line>();
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    if (field[i, j] != ObjectType.Empty)
                    {
                        continue;
                    }

                    line = FindHorizontal(i, j, field);
                    if (line.Count > 0)
                        return line;

                    line = FindVertical(i, j, field);
                    if (line.Count > 0)
                        return line;                    
                    
                    line = FindDiagonalLeftBottomRightTop(i, j, field);
                    if (line.Count > 0)
                        return line;

                    line = FindDiagonalLeftTopRightBottom(i, j, field);
                    if (line.Count > 0)
                        return line;

                }
            }
            return line;
        }


        /// <summary>
        /// Проверить, создаёт ли атакующую (или защитную) цепочку проверяемая клетка
        /// </summary>        
        /// <returns></returns>
        private static bool VerifyPosition(ObjectType[,] field,
            int len0, int len1, int len2,
            int ci0, int ci1, int ci2, int ci3,
            int cj0, int cj1, int cj2, int cj3,
            string direction)
        {
            // Если есть 5 подряд или больше - то этот ход атакующий и выигрывающий
            if (len1 >= 5)
            {
                return true;
            }

            // Если есть 4 подряд и справа или слева есть пустая клетка - то этот ход - атакующий
            if (len1 == 4 &&
                ((ci1 >= 0 && field[ci1, cj1] == ObjectType.Empty) ||
                (ci2 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)))
            {
                return true;
            }

            // Если есть 3 подряд, справа и слева есть пустые клетки и хотя бы с одной
            // стороны в след. клетке не стоит объект противники - то это ход - атакующий
            if (len1 == 3 && ci1 >= 0 && ci2 < m_RowsCnt &&
                field[ci1, cj1] == ObjectType.Empty && field[ci2, cj2] == ObjectType.Empty)
            {
                if (direction == "Horizontal" &&                
                  ((ci1 >= 1 && field[ci1 - 1, cj1] != m_WhoDoesntStep) ||
                  (ci2 < m_RowsCnt - 1 && field[ci2 + 1, cj2] != m_WhoDoesntStep)))
                    return true;
                if (direction == "Vertical" &&
                  ((cj1 >= 1 && field[ci1, cj1 - 1] != m_WhoDoesntStep) ||
                  (cj2 < m_ColumnsCnt - 1 && field[ci2, cj2 + 1] != m_WhoDoesntStep)))
                    return true;
                if (direction == "DiagonalLeftTopRightBottom" &&
                  ((ci1 >= 1 && cj1 >= 1 && field[ci1 - 1, cj1 - 1] != m_WhoDoesntStep) ||
                  (ci2 < m_RowsCnt - 1 && cj2 < m_ColumnsCnt - 1 && field[ci2 + 1, cj2 + 1] != m_WhoDoesntStep)))
                    return true;
                if (direction == "DiagonalLeftBottomRightTop" &&
                  ((ci1 >= 1 && cj1 < m_ColumnsCnt - 1 && field[ci1 - 1, cj1 + 1] != m_WhoDoesntStep) ||
                  (ci2 < m_RowsCnt - 1 && cj2 >= 1 && field[ci2 + 1, cj2 - 1] != m_WhoDoesntStep)))
                    return true;
            }

            // Если в сумме две последовательности дают 4 или больше - значит пробел между 
            // последовательностями - это следующий выигрышный ход
            if ((len0 > 0 && len0 + len1 == 4) ||
                (len2 > 0 && len1 + len2 == 4))
            {
                return true;
            }

            // Если в сумме получается 3 объекта - то проверяем, есть ли с краёв последовательности
            // место под 5-ый объект. Тогда пустая клетка между посдедовательностями будет
            // 4-ой выигрывающей клеткой при следующем ходе. Проверяем сначала центральную и
            // левую последовательности, потом центральную и правую
            if (len0 > 0 && len0 + len1 == 3 &&
                ci0 >= 0 && field[ci0, cj0] == ObjectType.Empty &&
                ci2 < m_RowsCnt && field[ci2, cj2] == ObjectType.Empty)
            {
                return true;
            }

            if (len2 > 0 && len1 + len2 == 3 &&
                ci1 >= 0 && field[ci1, cj1] == ObjectType.Empty &&
                ci3 < m_RowsCnt && field[ci3, cj3] == ObjectType.Empty)
            {
                return true;
            }

            return false;
        }


        /// <summary>
        ///  Ищем, сколько подряд по горизонтали
        /// </summary>        
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>  
        /// <param name="field"></param>
        /// <returns></returns>
        private static List<Line> FindHorizontal(int x, int y, ObjectType[,] field)
        {
            int len1 = 1;
            int ci1 = x - 1;
            int cj1 = y;
            while (ci1 >= 0 && field[ci1, cj1] == m_WhoStep)
            {
                ci1--;
                len1++;
            }

            int ci2 = x + 1;
            int cj2 = y;
            while (ci2 < m_RowsCnt && field[ci2, cj2] == m_WhoStep)
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
                while (ci0 >= 0 && field[ci0, cj0] == m_WhoStep)
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
                while (ci3 < m_RowsCnt && field[ci3, cj3] == m_WhoStep)
                {
                    ci3++;
                    len2++;
                }
            }

            var line = new List<Line>();
            if (VerifyPosition(field, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3, "Horizontal"))
            {
                while (field[ci0 + 1, cj0] != m_WhoStep) ci0++;
                while (field[ci3 - 1, cj3] != m_WhoStep) cj3--;

                int cnt = 0;
                while (cnt < 4 && ci0 >= 0 && field[ci0, cj0] == ObjectType.Empty)
                {
                    cnt++;
                    ci0--;
                }

                cnt = 0;
                while (cnt < 4 && ci3 < m_RowsCnt && field[ci3, cj3] == ObjectType.Empty)
                {
                    cnt++;
                    ci3++;
                }

                for (int i = ci0 + 1; i < ci3; i++)
                {
                    var temp = new Line
                    {
                        X = i,
                        Y = y,
                        Figure = field[i, y]
                    };
                    line.Add(temp);
                }
                return line;
            }

            return line;
        }


        /// <summary>
        ///  Ищем, сколько подряд по вертикали
        /// </summary>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param> 
        /// <param name="field"></param>
        /// <returns></returns>
        private static List<Line> FindVertical(int x, int y, ObjectType[,] field)
        {
            int len1 = 1;
            int ci1 = x;
            int cj1 = y - 1;
            while (cj1 >= 0 && field[ci1, cj1] == m_WhoStep)
            {
                cj1--;
                len1++;
            }

            int ci2 = x;
            int cj2 = y + 1;
            while (cj2 < m_ColumnsCnt && field[ci2, cj2] == m_WhoStep)
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
                while (cj0 >= 0 && field[ci0, cj0] == m_WhoStep)
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
                while (cj3 < m_ColumnsCnt && field[ci3, cj3] == m_WhoStep)
                {
                    cj3++;
                    len2++;
                }
            }

            var line = new List<Line>();
            if (VerifyPosition(field, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3, "Vertical"))
            {
                while (field[ci0, cj0 + 1] != m_WhoStep) cj0++;
                while (field[ci3, cj3 - 1] != m_WhoStep) cj3--;

                int cnt = 0;
                while (cnt < 4 && cj0 >= 0 && field[ci0, cj0] == ObjectType.Empty)
                {
                    cnt++;
                    cj0--;
                }

                cnt = 0;
                while (cnt < 4 && cj3 < m_RowsCnt && field[ci3, cj3] == ObjectType.Empty)
                {
                    cnt++;
                    cj3++;
                }

                for (int j = cj0 + 1; j < cj3; j++)
                {
                    var temp = new Line
                    {
                        X = x,
                        Y = j,
                        Figure = field[x, j]
                    };
                    line.Add(temp);
                }
                return line;
            }

            return line;
        }


        /// <summary>
        /// Ищем, сколько подряд наискосок с левого верха до правого низа
        /// </summary>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>     
        /// <param name="field"></param>
        /// <returns></returns>
        private static List<Line> FindDiagonalLeftTopRightBottom(int x, int y, ObjectType[,] field)
        {
            int len1 = 1;
            int ci1 = x - 1;
            int cj1 = y - 1;
            while (ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == m_WhoStep)
            {
                ci1--;
                cj1--;
                len1++;
            }

            int ci2 = x + 1;
            int cj2 = y + 1;
            while (ci2 < m_RowsCnt && cj2 < m_ColumnsCnt && field[ci2, cj2] == m_WhoStep)
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
                while (ci0 >= 0 && cj0 >= 0 && field[ci0, cj0] == m_WhoStep)
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
                while (ci3 < m_RowsCnt && cj3 < m_ColumnsCnt && field[ci3, cj3] == m_WhoStep)
                {
                    ci3++;
                    cj3++;
                    len2++;
                }
            }

            var line = new List<Line>();
            if (VerifyPosition(field, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3, "DiagonalLeftTopRightBottom"))
            {
                while (field[ci0 + 1, cj0 + 1] != m_WhoStep)
                {
                    ci0++;
                    cj0++;

                }
                while (field[ci3 - 1, cj3 - 1] != m_WhoStep)
                {
                    ci3--;
                    cj3--;
                }

                int cnt = 0;
                while (cnt < 4 && ci0 >= 0 && cj0 >= 0 && field[ci0, cj0] == ObjectType.Empty)
                {
                    cnt++;
                    ci0--;
                    cj0--;
                }

                cnt = 0;
                while (cnt < 4 && ci3 < m_RowsCnt && cj3 < m_ColumnsCnt && field[ci3, cj3] == ObjectType.Empty)
                {
                    cnt++;
                    ci3++;
                    cj3++;
                }

                int i;
                int j;
                for (i = ci0 + 1, j = cj0 + 1; i < ci3 && j < cj3; i++, j++)
                {
                    var temp = new Line
                    {
                        X = i,
                        Y = j,
                        Figure = field[i, j]
                    };
                    line.Add(temp);
                }
                return line;
            }

            return line;
        }


        /// <summary>
        /// Ищем, сколько подряд наискосок с левого низа до правого верха
        /// </summary>
        /// <param name="x">x - координата хода</param>
        /// <param name="y">y - координата хода</param>        
        /// <param name="field"></param>
        /// <returns></returns>
        private static List<Line> FindDiagonalLeftBottomRightTop(int x, int y, ObjectType[,] field)
        {
            int len1 = 1;
            int ci1 = x - 1;
            int cj1 = y + 1;
            while (ci1 >= 0 && cj1 < m_ColumnsCnt && field[ci1, cj1] == m_WhoStep)
            {
                ci1--;
                cj1++;
                len1++;
            }

            int ci2 = x + 1;
            int cj2 = y - 1;
            while (ci2 < m_RowsCnt && cj2 >= 0 && field[ci2, cj2] == m_WhoStep)
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
                while (ci0 >= 0 && cj0 < m_ColumnsCnt && field[ci0, cj0] == m_WhoStep)
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
                while (ci3 < m_RowsCnt && cj3 >= 0 && field[ci3, cj3] == m_WhoStep)
                {
                    ci3++;
                    cj3--;
                    len2++;
                }
            }

            var line = new List<Line>();
            if (VerifyPosition(field, len0, len1, len2, ci0, ci1, ci2, ci3, cj0, cj1, cj2, cj3, "DiagonalLeftBottomRightTop"))
            {
                while (field[ci0 + 1, cj0 - 1] != m_WhoStep)
                {
                    ci0++;
                    cj0--;

                }
                while (field[ci3 - 1, cj3 + 1] != m_WhoStep)
                {
                    ci3--;
                    cj3++;
                }

                int cnt = 0;
                while (cnt < 4 && ci0 >= 0 && cj0 < m_ColumnsCnt && field[ci0, cj0] == ObjectType.Empty)
                {
                    cnt++;
                    ci0--;
                    cj0++;
                }

                cnt = 0;
                while (cnt < 4 && ci3 < m_RowsCnt && cj3 >= 0 && field[ci3, cj3] == ObjectType.Empty)
                {
                    cnt++;
                    ci3++;
                    cj3--;
                }

                int i;
                int j;
                for (i = ci0 + 1, j = cj0 - 1; i < ci3 && j > cj3; i++, j--)
                {
                    var temp = new Line
                    {
                        X = i,
                        Y = j,
                        Figure = field[i, j]
                    };
                    line.Add(temp);
                }
                return line;
            }

            return line;
        }

        #endregion

        #region Разные функции
        /// <summary>
        /// Функция для копирования массива ObjectType[,]
        /// </summary>        
        /// <param name="fromArr">Массив, содержащий позицию на поле</param>
        /// <returns></returns>
        private static ObjectType[,] CopyObjectTypeArray(ObjectType[,] fromArr)
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
        /// Получить позицию из цепочки ходов, приводящих к ней
        /// </summary>
        /// <param name="stepsChain">Список ходов до позиции</param>        
        /// <returns></returns>
        private static ObjectType[,] GetFieldFromStepsChain(IEnumerable<StepInfo> stepsChain)
        {
            var field = CopyObjectTypeArray(m_Field);

            foreach (StepInfo stepInfo in stepsChain)
            {
                field[stepInfo.X, stepInfo.Y] = stepInfo.WhoStep;
            }

            return field;
        }


        /// <summary>
        /// Пометить клетку в линии, которая соответствует сделанному ходу
        /// </summary>
        /// <param name="line"></param>
        /// <param name="stepInfo"></param>
        private static void SetFigureToLine(IList<Line> line, StepInfo stepInfo)
        {
            for (int i = 0; i < line.Count; i++)
            {
                if (line[i].X == stepInfo.X && line[i].Y == stepInfo.Y)
                {
                    Line temp = line[i];
                    temp.Figure = stepInfo.WhoStep;
                    line[i] = temp;
                    return;
                }
            }
        }


        /// <summary>
        /// Получить позицию из цепочки ходов, приводящих к ней
        /// </summary>
        /// <param name="stepsChain">Список ходов до позиции</param>        
        /// <returns></returns>
        private static List<Line> GetLineFromStepsChain(IEnumerable<StepInfo> stepsChain)
        {
            var line = new List<Line>(m_Line);            

            foreach (StepInfo stepInfo in stepsChain)
            {
                SetFigureToLine(line, stepInfo);
            }

            return line;
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
        /// Выкинуть все узлы, которые не в списке выбранных узлов
        /// </summary>
        /// <param name="newPosition"></param>
        /// <param name="selectedNumbers"></param>
        private static void RemoveAllExceptSelected(IList<Node> newPosition, ICollection<int> selectedNumbers)
        {
            for (int i = newPosition.Count - 1; i >= 0; i--)
            {
                if (!selectedNumbers.Contains(i))
                {
                    newPosition.RemoveAt(i);
                }
            }
        }
        #endregion
    }
}
