using System;

namespace TicTacToe
{
    class CheckFieldClass
    {
        private static ObjectType GetLoser(ObjectType winner)
        {
            if (winner == ObjectType.Cross)
            {
                return ObjectType.Nil;
            }

            return ObjectType.Cross;
        }

        public ObjectType FindLoser(ObjectType[,] field, int rowsCnt, int columnsCnt, PaintClass paintClass)
        {
            int emptyCnt = 0;
            // Поиск линии из 5 одинаковых объектов            
            for (int i = 0; i < rowsCnt; i++)
            {
                for (int j = 0; j < columnsCnt; j++)
                {
                    if (field[i, j] == ObjectType.Empty)
                    {
                        emptyCnt++;
                        continue;
                    }

                    ObjectType curObj = field[i, j];

                    // Ищем, сколько подряд по горизонтали
                    int len = 1;
                    int ci1 = i - 1;
                    int cj1 = j;
                    while (ci1 >= 0 && field[ci1, cj1] == curObj)
                    {
                        ci1--;
                        len++;
                    }
                    ci1++;

                    int ci2 = i + 1;
                    int cj2 = j;
                    while (ci2 < rowsCnt && field[ci2, cj2] == curObj)
                    {
                        ci2++;
                        len++;
                    }
                    ci2--;

                    if (len > 4)
                    {
                        paintClass.DrawWinLine(curObj, ci1, cj1, ci2, cj2);
                        return GetLoser(curObj);
                    }

                    // Ищем, сколько подряд по вертикали
                    len = 1;
                    ci1 = i;
                    cj1 = j - 1;
                    while (cj1 >= 0 && field[ci1, cj1] == curObj)
                    {
                        cj1--;
                        len++;
                    }
                    cj1++;

                    ci2 = i;
                    cj2 = j + 1;
                    while (cj2 < columnsCnt && field[ci2, cj2] == curObj)
                    {
                        cj2++;
                        len++;
                    }
                    cj2--;

                    if (len > 4)
                    {
                        paintClass.DrawWinLine(curObj, ci1, cj1, ci2, cj2);
                        return GetLoser(curObj);
                    }

                    // Ищем, сколько подряд наискосок с левого верха до правого низа
                    len = 1;
                    ci1 = i - 1;
                    cj1 = j - 1;
                    while (ci1 >= 0 && cj1 >= 0 && field[ci1, cj1] == curObj)
                    {
                        ci1--; 
                        cj1--;
                        len++;
                    }
                    ci1++;
                    cj1++;

                    ci2 = i + 1;
                    cj2 = j + 1;
                    while (ci2 < rowsCnt && cj2 < columnsCnt && field[ci2, cj2] == curObj)
                    {
                        ci2++;
                        cj2++;
                        len++;
                    }
                    ci2--;
                    cj2--;

                    if (len > 4)
                    {
                        paintClass.DrawWinLine(curObj, ci1, cj1, ci2, cj2);
                        return GetLoser(curObj);
                    }

                    // Ищем, сколько подряд наискосок с левого низа до правого верха
                    len = 1;
                    ci1 = i - 1;
                    cj1 = j + 1;
                    while (ci1 >= 0 && cj1 < columnsCnt && field[ci1, cj1] == curObj)
                    {
                        ci1--;
                        cj1++;
                        len++;
                    }
                    ci1++;
                    cj1--;

                    ci2 = i + 1;
                    cj2 = j - 1;
                    while (ci2 < rowsCnt && cj2 >= 0 && field[ci2, cj2] == curObj)
                    {
                        ci2++;
                        cj2--;
                        len++;
                    }
                    ci2--;
                    cj2++;

                    if (len > 4)
                    {
                        paintClass.DrawWinLine(curObj, ci1, cj1, ci2, cj2);
                        return GetLoser(curObj);
                    }
                }
            }

            if (emptyCnt == 0)
            {
                throw new Exception("Клетки закончились.");
            }

            return ObjectType.Empty;
        }
    }
}
