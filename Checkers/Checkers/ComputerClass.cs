using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.Threading;
using System.Reflection;

namespace Checkers
{
    /// <summary>
    /// Класс с логикой компьютера
    /// </summary>
    public class ComputerClass
    {
        /// <summary>
        /// Дерево с информацией о возможных ходах
        /// </summary>
        Node[][] stepsThree;

        /// <summary>
        /// Количество элементов в соответствующем уровне дерева
        /// </summary>
        int[] stepsThreeLength;

        /// <summary>
        /// Информация, содержащаяся в узле дерева		
        /// </summary>
        struct Node
        {
            /// <summary>
            /// Указатель того, рабочий это узел или уже отброшен
            /// </summary>
            public bool valid;

            /// <summary>
            /// Координаты узла, где указан предыдущий ход (Номер столбца в stepsThree в предыдущей строке)
            /// </summary>
            public int numberPrev;

            /// <summary>
            /// Рассматриваемый ход (в виде XY:XY{:XY:XY:XY...})
            /// </summary>
            public string step;

            /// <summary>
            /// Получившаяся позиция
            /// </summary>
            public ObjectCheck[,] mas;

            /// <summary>
            /// Оценка получившейся позиции ( если она '?' - то надо смотреть глубже)		
            /// </summary>
            public string mark;

            /// <summary>
            /// Оценка получившейся позиции, основанная на всех разведанных ходах из этой позиции
            /// </summary>
            public string realMark;

            /// <summary>
            /// Кто ходит в данной позиции (кто делает следующий ход)
            /// </summary>
            public ColorCheck whoMove;
        }

        public struct CmpInfo
        {
            /// <summary>
            /// Текущая оценка
            /// </summary>
            public string currentMark;

            /// <summary>
            /// Оценка в ближайшей перспективе оценка
            /// </summary>
            public string currentRealMark;

            /// <summary>
            /// Общее количество рассмотренных позиций
            /// </summary>
            public int currentCountPosition;

            /// <summary>
            /// Наибольшая просчитанная глубина в данный момент
            /// </summary>
            public int currentMaxDepth;

            /// <summary>
            /// Количество просчитанных уровней
            /// </summary>
            public int currentDepth;
        }

        public CmpInfo cmpInfo;

        /// <summary>
        /// Глубина расчёта (если на данном уровне есть несъеденные шашки - то уровень может увеличиться)
        /// </summary>
        int maxDepth = 5;

        /// <summary>
        /// Переменная для ведения лога
        /// </summary>
        LogClass logClass;

        /// <summary>
        /// Поток для постоянного добавления глубины хода
        /// </summary>
        Thread threadAgent;

        /// <summary>
        /// Создание класса для получения ходов компьютера
        /// </summary>
        /// <param name="mas">Начальная позиция</param>
        /// <param name="whoMove">Кто ходит</param>
        public ComputerClass(ObjectCheck[,] mas, ColorCheck whoMove, LogClass logClass)
        {
            this.logClass = logClass;
            stepsThree = new Node[50][];
            stepsThreeLength = new int[50];
            stepsThree[0] = new Node[1];
            stepsThreeLength[0] = 1;
            Node node = new Node();
            node.numberPrev = -1;
            node.step = "";
            node.mas = mas;
            node.realMark = node.mark = Evaluation(mas, whoMove);
            node.whoMove = whoMove;
            node.valid = true;
            stepsThree[0][0] = node;

            SetCmpInfo();

            threadAgent = new Thread(new ThreadStart(AddDepthAgent));
            threadAgent.Priority = ThreadPriority.Lowest;
            threadAgent.Start();
            logClass.WriteLine("Инициализация ComputerClass");
        }

        /// <summary>
        ///  Остановка работы агента
        /// </summary>
        public void StopGame()
        {
            try
            {
                threadAgent.Abort();
                threadAgent.Join();
            }
            catch
            {
            }
        }

        /// <summary>
        /// Добавляет новую позицию в массив с позициями
        /// </summary>
        /// <param name="node">Структура, описывающая позицию</param>
        /// <param name="depth">Уровень в массиве, в который надо добавить новую позицию</param>
        private void AddNode(ref Node[][] curThree, ref int[] curThreeLenght, int depth, Node node)
        {
            if (curThreeLenght[depth] == curThree[depth].Length)
            {
                Node[] temp = new Node[2 * curThreeLenght[depth]];
                curThree[depth].CopyTo(temp, 0);
                curThree[depth] = temp;
                node.valid = true;
                curThree[depth][curThreeLenght[depth]] = node;
            }
            else
            {

                curThree[depth][curThreeLenght[depth]] = node;
            }
            curThreeLenght[depth]++;
        }

        /// <summary>
        /// Проверка на возможность съесть чего-либо фигурой на данном поле
        /// </summary>
        /// <param name="mas">Массив с текущей расстановкой</param>
        /// <param name="x">Х-координата ходящей фигуры</param>
        /// <param name="y">Y-координата ходящей фигуры</param>		
        /// <returns></returns>
        private bool CheckEat(ObjectCheck[,] mas, int x, int y)
        {
            int saveX,
                saveY;

            // Получение цвета ходящей шашки
            ColorCheck colCheck = ColorCheck.white;
            if (mas[x, y] == ObjectCheck.check_black || mas[x, y] == ObjectCheck.check_black_dam)
                colCheck = ColorCheck.black;

            // Получение типа ходящей фигуры
            TypeCheck typeCheck = TypeCheck.check;
            if (mas[x, y] == ObjectCheck.check_white_dam || mas[x, y] == ObjectCheck.check_black_dam)
                typeCheck = TypeCheck.king;

            // Проверка вправо вниз
            ColorCheck thisColCheck = ColorCheck.disable;
            saveX = x + 1;
            saveY = y + 1;
            if (typeCheck == TypeCheck.king)
                while (saveX < 6 && saveY < 6 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    saveX++;
                    saveY++;
                }
            if (saveX < 7 && saveY < 7)
            {
                if (mas[saveX, saveY] == ObjectCheck.check_black || mas[saveX, saveY] == ObjectCheck.check_black_dam)
                    thisColCheck = ColorCheck.black;
                else if (mas[saveX, saveY] == ObjectCheck.check_white || mas[saveX, saveY] == ObjectCheck.check_white_dam)
                    thisColCheck = ColorCheck.white;
                if (thisColCheck != ColorCheck.disable && colCheck != thisColCheck && mas[saveX + 1, saveY + 1] == ObjectCheck.full)
                    return true;
            }


            // Проверка влево вниз
            thisColCheck = ColorCheck.disable;
            saveX = x + 1;
            saveY = y - 1;
            if (typeCheck == TypeCheck.king)
                while (saveX < 8 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    saveX++;
                    saveY--;
                }
            if (saveX < 7 && saveY > 0)
            {
                if (mas[saveX, saveY] == ObjectCheck.check_black || mas[saveX, saveY] == ObjectCheck.check_black_dam)
                    thisColCheck = ColorCheck.black;
                else if (mas[saveX, saveY] == ObjectCheck.check_white || mas[saveX, saveY] == ObjectCheck.check_white_dam)
                    thisColCheck = ColorCheck.white;
                if (thisColCheck != ColorCheck.disable && colCheck != thisColCheck && mas[saveX + 1, saveY - 1] == ObjectCheck.full)
                    return true;
            }


            // Проверка вправо вверх
            thisColCheck = ColorCheck.disable;
            saveX = x - 1;
            saveY = y + 1;
            if (typeCheck == TypeCheck.king)
                while (saveX > -1 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    saveX--;
                    saveY++;
                }
            if (saveX > 0 && saveY < 7)
            {
                if (mas[saveX, saveY] == ObjectCheck.check_black || mas[saveX, saveY] == ObjectCheck.check_black_dam)
                    thisColCheck = ColorCheck.black;
                else if (mas[saveX, saveY] == ObjectCheck.check_white || mas[saveX, saveY] == ObjectCheck.check_white_dam)
                    thisColCheck = ColorCheck.white;
                if (thisColCheck != ColorCheck.disable && colCheck != thisColCheck && mas[saveX - 1, saveY + 1] == ObjectCheck.full)
                    return true;
            }


            // Проверка влево вверх	
            thisColCheck = ColorCheck.disable;
            saveX = x - 1;
            saveY = y - 1;
            if (typeCheck == TypeCheck.king)
                while (saveX > -1 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    saveX--;
                    saveY--;
                }
            if (saveX > 0 && saveY > 0)
            {
                if (mas[saveX, saveY] == ObjectCheck.check_black || mas[saveX, saveY] == ObjectCheck.check_black_dam)
                    thisColCheck = ColorCheck.black;
                else if (mas[saveX, saveY] == ObjectCheck.check_white || mas[saveX, saveY] == ObjectCheck.check_white_dam)
                    thisColCheck = ColorCheck.white;
                if (thisColCheck != ColorCheck.disable && colCheck != thisColCheck && mas[saveX - 1, saveY - 1] == ObjectCheck.full)
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Поиск всех фигур, которые могут съесть в данной расстановке
        /// </summary>
        /// <param name="mas">Массив с текущей расстановкой</param>
        /// <param name="whoMove">Кто сейчас ходит</param>
        /// <returns></returns>
        private ArrayList CheckEatAll(ObjectCheck[,] mas, ColorCheck whoMove)
        {
            ArrayList figure = new ArrayList();
            int[] xy;

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    switch (mas[i, j])
                    {
                        case ObjectCheck.check_black:
                        case ObjectCheck.check_black_dam:
                            if (whoMove == ColorCheck.black)
                            {
                                if (CheckEat(mas, i, j))
                                {
                                    xy = new int[2];
                                    xy[0] = i;
                                    xy[1] = j;
                                    figure.Add(xy);
                                }
                            }
                            break;
                        case ObjectCheck.check_white:
                        case ObjectCheck.check_white_dam:
                            if (whoMove == ColorCheck.white)
                            {
                                if (CheckEat(mas, i, j))
                                {
                                    xy = new int[2];
                                    xy[0] = i;
                                    xy[1] = j;
                                    figure.Add(xy);
                                }
                            }
                            break;
                    }
                }
            return figure;
        }

        /// <summary>
        ///  Подсчёт количества полей, куда доходит дамка
        /// </summary>
        /// <param name="mas">Массив с текущей расстановкой</param>
        /// <param name="x">Х-координата ходящей фигуры</param>
        /// <param name="y">Y-координата ходящей фигуры</param>
        /// <returns></returns>
        private int ViewKingStep(ObjectCheck[,] mas, int x, int y)
        {
            int cnt = 0,
              saveX,
              saveY;
            saveX = x + 1;
            saveY = y + 1;
            while (saveX < 8 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
            {
                saveX++;
                saveY++;
                cnt++;
            }

            saveX = x + 1;
            saveY = y - 1;
            while (saveX < 8 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
            {
                saveX++;
                saveY--;
                cnt++;
            }

            saveX = x - 1;
            saveY = y + 1;
            while (saveX > -1 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
            {
                saveX--;
                saveY++;
                cnt++;
            }

            saveX = x - 1;
            saveY = y - 1;
            while (saveX > -1 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
            {
                saveX--;
                saveY--;
                cnt++;
            }

            return cnt;
        }

        /// <summary>
        /// Оценка текущей позиции
        /// </summary>
        /// <param name="mas">Текущая позиция</param>
        /// <param name="whoMove">Кто ходит</param>
        /// <returns>Возвращает оценку позиции с точки зрения того, кто ходит или "?", в случае невозможность определить оценку</returns>
        private string Evaluation(ObjectCheck[,] mas, ColorCheck whoMove)
        {
            int sumWhite = 0,
                sumBlack = 0,
                cntW = 0,
                cntB = 0,
                cntW_D = 0,
                cntB_D = 0;

            // Проверка, на то, можно ли чем нибудь кого-нибудь съесть
            // Если да - возвращаем "?"
            ArrayList figure = CheckEatAll(mas, whoMove);
            if (figure.Count > 0)
                return "?";

            // Если нельзя съесть - то оцениваем расположение и качество фигур
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                {
                    switch (mas[i, j])
                    {
                        case ObjectCheck.check_black:
                            cntB++;
                            if (i < 7)
                            {
                                sumBlack += i;
                                if (j > 0 && mas[i + 1, j - 1] == ObjectCheck.full)
                                    sumBlack += (i + 1) * 2;
                                if (j < 7 && mas[i + 1, j + 1] == ObjectCheck.full)
                                    sumBlack += (i + 1) * 2;
                            }
                            break;
                        case ObjectCheck.check_black_dam:
                            cntB_D++;
                            sumBlack += i * 2;
                            sumBlack += ViewKingStep(mas, i, j) * 3;
                            break;
                        case ObjectCheck.check_white:
                            cntW++;
                            if (i > 0)
                            {
                                sumWhite += 7 - i;
                                if (j > 0 && mas[i - 1, j - 1] == ObjectCheck.full)
                                    sumWhite += (7 - i + 1) * 2;
                                if (j < 7 && mas[i - 1, j + 1] == ObjectCheck.full)
                                    sumWhite += (7 - i + 1) * 2;
                            }
                            break;
                        case ObjectCheck.check_white_dam:
                            cntW_D++;
                            sumWhite += i * 2;
                            sumWhite += ViewKingStep(mas, i, j) * 2;
                            break;
                    }
                }

            // Бонус за лишние шашки
            if (cntW > cntB)
                sumWhite += (cntW - cntB) * 10;
            else
                sumBlack += (cntB - cntW) * 10;

            // Бонус за лишние дамки
            if (cntW_D > cntB_D)
                sumWhite += (cntW_D - cntB_D) * 50;
            else
                sumBlack += (cntB_D - cntW_D) * 50;

            if (whoMove == ColorCheck.white)
                return Convert.ToString(sumWhite - sumBlack);
            else
                return Convert.ToString(sumBlack - sumWhite);
        }

        /// <summary>
        /// Сделать шаг и вернуть полученную позицию
        /// </summary>
        /// <param name="mas">Текущая позиция</param>
        /// <param name="fromX">X-координата поля, откуда ходим</param>
        /// <param name="fromY">Y-координата поля, откуда ходим</param>
        /// <param name="toX">X-координата поля, куда ходим</param>
        /// <param name="toY">Y-координата поля, куда ходим</param>
        /// <returns></returns>
        private ObjectCheck[,] SetOneStep(ObjectCheck[,] mas1, int fromX, int fromY, int toX, int toY)
        {
            ObjectCheck[,] mas = new ObjectCheck[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    mas[i, j] = mas1[i, j];

            ObjectCheck asdf = mas[fromX, fromY];
            mas[fromX, fromY] = mas[toX, toY];
            mas[toX, toY] = asdf;

            if (mas[toX, toY] == ObjectCheck.check_black && toX == 7)
                mas[toX, toY] = ObjectCheck.check_black_dam;

            if (mas[toX, toY] == ObjectCheck.check_white && toX == 0)
                mas[toX, toY] = ObjectCheck.check_white_dam;

            int x1, y1, x2, y2;				// Удаляем шашку, которую съели, если она есть

            if (fromX > toX)
            {
                x1 = toX;
                y1 = toY;
                x2 = fromX;
                y2 = fromY;
            }
            else
            {
                x2 = toX;
                y2 = toY;
                x1 = fromX;
                y1 = fromY;
            }


            int w = y1;
            for (int q = x1 + 1; q < x2; q++)
            {
                if (y1 > y2)
                    w--;
                else
                    w++;
                if (mas[q, w] != ObjectCheck.full)
                {
                    mas[q, w] = ObjectCheck.full;
                    break;
                }
            }

            return mas;
        }

        /// <summary>
        /// Добавляем к списку возможных ходов в данной позиции все хода указанной дамки (когда есть нечего)
        /// </summary>
        /// <param name="mas">Текущая позиция</param>
        /// <param name="x">X-координата дамки</param>
        /// <param name="y">Y-координата дамки</param>
        /// <param name="stepsList">Массив с возможными ходами</param>
        /// <returns></returns>
        private void AddKingMove(ObjectCheck[,] mas, int x, int y, ref ArrayList stepsList)
        {
            string[] node;
            int saveX,
                saveY;
            saveX = x + 1;
            saveY = y + 1;
            while (saveX < 8 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
            {
                node = new string[2];
                node[0] = x.ToString() + y.ToString();
                node[1] = saveX.ToString() + saveY.ToString();
                stepsList.Add(node[0] + ":" + node[1]);
                saveX++;
                saveY++;
            }

            saveX = x + 1;
            saveY = y - 1;
            while (saveX < 8 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
            {
                node = new string[2];
                node[0] = x.ToString() + y.ToString();
                node[1] = saveX.ToString() + saveY.ToString();
                stepsList.Add(node[0] + ":" + node[1]);
                saveX++;
                saveY--;
            }

            saveX = x - 1;
            saveY = y + 1;
            while (saveX > -1 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
            {
                node = new string[2];
                node[0] = x.ToString() + y.ToString();
                node[1] = saveX.ToString() + saveY.ToString();
                stepsList.Add(node[0] + ":" + node[1]);
                saveX--;
                saveY++;
            }

            saveX = x - 1;
            saveY = y - 1;
            while (saveX > -1 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
            {
                node = new string[2];
                node[0] = x.ToString() + y.ToString();
                node[1] = saveX.ToString() + saveY.ToString();
                stepsList.Add(node[0] + ":" + node[1]);
                saveX--;
                saveY--;
            }
        }

        /// <summary>
        /// Функция для нахождения всех возможных ходов данной шашки
        /// </summary>
        /// <param name="mas">Массив с позицией</param>
        /// <param name="x">X-координата</param>
        /// <param name="y">Y-координата</param>
        /// <param name="check">Тип шашек, которые съедаем</param>
        /// <param name="check_dam">Тип дамок, которые съедаем</param>
        /// <param name="coorse">В какую сторону не проверяем. 1 - вниз влево, 2 - вниз вправо, 3 - вверх вправо, 4 - вверх влево</param>
        /// <returns></returns>
        private ArrayList CheckKing(ObjectCheck[,] mas, int x, int y, ObjectCheck check, ObjectCheck check_dam, int coorse)
        {
            ArrayList destArray = new ArrayList();
            int flag = 0;
            // Массив для хранения информации о направлении съедания, об координатах поля и о весе 
            // (количестве съеденных фигур). До съедания фигуры - 0, после - 1, если можно съесть ещё - 2
            int[] xy;
            int saveX,
                saveY,
                x1,
                y1,
                saveFlag;
            // Проверка вниз влево
            if (coorse != 1)
            {
                saveX = x + 1;
                saveY = y - 1;
                while (saveX < 6 && saveY > 1 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    xy = new int[4] { 1, saveX, saveY, 0 };
                    destArray.Add(xy);
                    saveX++;
                    saveY--;
                }
                if (saveX < 7 && saveY > 0 && (mas[saveX, saveY] == check || mas[saveX, saveY] == check_dam) &&
                  mas[saveX + 1, saveY - 1] == ObjectCheck.full)
                {
                    saveX++;
                    saveY--;

                    // Проверка на то, можно ли съесть ещё одну шашку на одной прямой со съеденной
                    x1 = saveX;
                    y1 = saveY;
                    saveFlag = 1;
                    while (x1 < 7 && y1 > 0 && mas[x1, y1] == ObjectCheck.full)
                    {
                        x1++;
                        y1--;
                    }
                    if (x1 < 7 && y1 > 0 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                    mas[x1 + 1, y1 - 1] == ObjectCheck.full)
                        saveFlag = 2;

                    while (saveX < 8 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
                    {
                        flag = saveFlag;

                        if (flag < 2)
                        {
                            // Поиск вверх влево и вниз вправо шашки, которую можно съесть
                            x1 = saveX - 1;
                            y1 = saveY - 1;
                            while (x1 > 1 && y1 > 1 && mas[x1, y1] == ObjectCheck.full)
                            {
                                x1--;
                                y1--;
                            }
                            if (x1 > 0 && y1 > 0 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                mas[x1 - 1, y1 - 1] == ObjectCheck.full)
                                flag = 2;
                            else
                            {
                                // вниз вправо, если вверх влево ничего не нашли
                                x1 = saveX + 1;
                                y1 = saveY + 1;
                                while (x1 < 6 && y1 < 6 && mas[x1, y1] == ObjectCheck.full)
                                {
                                    x1++;
                                    y1++;
                                }
                                if (x1 < 7 && y1 < 7 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                  mas[x1 + 1, y1 + 1] == ObjectCheck.full)
                                    flag = 2;
                            }
                        }
                        xy = new int[4] { 1, saveX, saveY, flag };
                        destArray.Add(xy);
                        saveX++;
                        saveY--;
                    }
                }
            }

            // Проверка вниз вправо
            if (coorse != 2)
            {
                saveX = x + 1;
                saveY = y + 1;
                while (saveX < 6 && saveY < 6 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    xy = new int[4] { 2, saveX, saveY, 0 };
                    destArray.Add(xy);
                    saveX++;
                    saveY++;
                }
                if (saveX < 7 && saveY < 7 && (mas[saveX, saveY] == check || mas[saveX, saveY] == check_dam) &&
                  mas[saveX + 1, saveY + 1] == ObjectCheck.full)
                {
                    saveX++;
                    saveY++;

                    // Проверка на то, можно ли съесть ещё одну шашку на одной прямой со съеденной
                    x1 = saveX;
                    y1 = saveY;
                    saveFlag = 1;
                    while (x1 < 7 && y1 < 7 && mas[x1, y1] == ObjectCheck.full)
                    {
                        x1++;
                        y1++;
                    }
                    if (x1 < 7 && y1 < 7 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                    mas[x1 + 1, y1 + 1] == ObjectCheck.full)
                        saveFlag = 2;

                    while (saveX < 8 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
                    {
                        flag = saveFlag;

                        if (flag < 2)
                        {
                            // Поиск вверх вправо и вниз влево шашки, которую можно съесть
                            x1 = saveX - 1;
                            y1 = saveY + 1;
                            while (x1 > 1 && y1 < 6 && mas[x1, y1] == ObjectCheck.full)
                            {
                                x1--;
                                y1++;
                            }
                            if (x1 > 0 && y1 < 7 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                mas[x1 - 1, y1 + 1] == ObjectCheck.full)
                                flag = 2;
                            else
                            {
                                // вниз влево, если вверх вправо ничего не нашли
                                x1 = saveX + 1;
                                y1 = saveY - 1;
                                while (x1 < 6 && y1 > 1 && mas[x1, y1] == ObjectCheck.full)
                                {
                                    x1++;
                                    y1--;
                                }
                                if (x1 < 7 && y1 > 0 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                  mas[x1 + 1, y1 - 1] == ObjectCheck.full)
                                    flag = 2;
                            }
                        }

                        xy = new int[4] { 2, saveX, saveY, flag };
                        destArray.Add(xy);
                        saveX++;
                        saveY++;
                    }
                }
            }

            // Проверка вверх вправо
            if (coorse != 3)
            {
                saveX = x - 1;
                saveY = y + 1;
                while (saveX > 1 && saveY < 6 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    xy = new int[4] { 3, saveX, saveY, 0 };
                    destArray.Add(xy);
                    saveX--;
                    saveY++;
                }
                if (saveX > 0 && saveY < 7 && (mas[saveX, saveY] == check || mas[saveX, saveY] == check_dam) &&
                  mas[saveX - 1, saveY + 1] == ObjectCheck.full)
                {
                    saveX--;
                    saveY++;

                    // Проверка на то, можно ли съесть ещё одну шашку на одной прямой со съеденной
                    x1 = saveX;
                    y1 = saveY;
                    saveFlag = 1;
                    while (x1 > 0 && y1 < 7 && mas[x1, y1] == ObjectCheck.full)
                    {
                        x1--;
                        y1++;
                    }
                    if (x1 > 0 && y1 < 7 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                    mas[x1 - 1, y1 + 1] == ObjectCheck.full)
                        saveFlag = 2;

                    while (saveX > -1 && saveY < 8 && mas[saveX, saveY] == ObjectCheck.full)
                    {
                        flag = saveFlag;

                        if (flag < 2)
                        {
                            // Поиск вверх влево и вниз вправо шашки, которую можно съесть
                            x1 = saveX - 1;
                            y1 = saveY - 1;
                            while (x1 > 1 && y1 > 1 && mas[x1, y1] == ObjectCheck.full)
                            {
                                x1--;
                                y1--;
                            }
                            if (x1 > 0 && y1 > 0 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                mas[x1 - 1, y1 - 1] == ObjectCheck.full)
                                flag = 2;
                            else
                            {
                                // вниз вправо, если вверх влево ничего не нашли
                                x1 = saveX + 1;
                                y1 = saveY + 1;
                                while (x1 < 6 && y1 < 6 && mas[x1, y1] == ObjectCheck.full)
                                {
                                    x1++;
                                    y1++;
                                }
                                if (x1 < 7 && y1 < 7 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                  mas[x1 + 1, y1 + 1] == ObjectCheck.full)
                                    flag = 2;
                            }
                        }

                        xy = new int[4] { 3, saveX, saveY, flag };
                        destArray.Add(xy);
                        saveX--;
                        saveY++;
                    }
                }
            }

            // Проверка вверх влево
            if (coorse != 4)
            {
                saveX = x - 1;
                saveY = y - 1;
                while (saveX > 1 && saveY > 1 && mas[saveX, saveY] == ObjectCheck.full)
                {
                    xy = new int[4] { 4, saveX, saveY, 0 };
                    destArray.Add(xy);
                    saveX--;
                    saveY--;
                }
                if (saveX > 0 && saveY > 0 && (mas[saveX, saveY] == check || mas[saveX, saveY] == check_dam) &&
                  mas[saveX - 1, saveY - 1] == ObjectCheck.full)
                {
                    saveX--;
                    saveY--;

                    // Проверка на то, можно ли съесть ещё одну шашку на одной прямой со съеденной
                    x1 = saveX;
                    y1 = saveY;
                    saveFlag = 1;
                    while (x1 > 0 && y1 > 0 && mas[x1, y1] == ObjectCheck.full)
                    {
                        x1--;
                        y1--;
                    }
                    if (x1 > 0 && y1 > 0 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                    mas[x1 - 1, y1 - 1] == ObjectCheck.full)
                        saveFlag = 2;

                    while (saveX > -1 && saveY > -1 && mas[saveX, saveY] == ObjectCheck.full)
                    {
                        flag = saveFlag;

                        if (flag < 2)
                        {
                            // Поиск вверх вправо и вниз влево шашки, которую можно съесть
                            x1 = saveX - 1;
                            y1 = saveY + 1;
                            while (x1 > 1 && y1 < 6 && mas[x1, y1] == ObjectCheck.full)
                            {
                                x1--;
                                y1++;
                            }
                            if (x1 > 0 && y1 < 7 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                mas[x1 - 1, y1 + 1] == ObjectCheck.full)
                                flag = 2;
                            else
                            {
                                // вниз влево, если вверх вправо ничего не нашли
                                x1 = saveX + 1;
                                y1 = saveY - 1;
                                while (x1 < 6 && y1 > 1 && mas[x1, y1] == ObjectCheck.full)
                                {
                                    x1++;
                                    y1--;
                                }
                                if (x1 < 7 && y1 > 0 && (mas[x1, y1] == check || mas[x1, y1] == check_dam) &&
                                  mas[x1 + 1, y1 - 1] == ObjectCheck.full)
                                    flag = 2;
                            }
                        }

                        xy = new int[4] { 4, saveX, saveY, flag };
                        destArray.Add(xy);
                        saveX--;
                        saveY--;
                    }
                }
            }
            return destArray;
        }

        /// <summary>
        /// Рекурсивная функция для поиска всех возможных взятий выделенной шашкой
        /// </summary>
        /// <param name="mas">Текущая позиция</param>
        /// <param name="x">X-координата</param>
        /// <param name="y">Y-координата</param>
        /// <param name="coorse">В какую сторону не проверяем. 1 - вниз влево, 2 - вниз вправо, 3 - вверх вправо, 4 - вверх влево</param>
        /// <returns>Возвращает ход в виде XY:XY{:XY:XY...}</returns>
        private string[] AddTakeMove(ObjectCheck[,] mas, int x, int y, int coorse)
        {
            string[] rez;
            ObjectCheck[,] masCopy;
            ArrayList stepsList = new ArrayList();
            bool flag = false;
            ArrayList destArray = new ArrayList();
            int[] xy;

            switch (mas[x, y])
            {
                case ObjectCheck.check_black:
                    if (x < 6 && y > 1 && (mas[x + 1, y - 1] == ObjectCheck.check_white ||
                        mas[x + 1, y - 1] == ObjectCheck.check_white_dam) && mas[x + 2, y - 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x + 2, y - 2);
                        rez = AddTakeMove(masCopy, x + 2, y - 2, 3);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    if (x < 6 && y < 6 && (mas[x + 1, y + 1] == ObjectCheck.check_white ||
                        mas[x + 1, y + 1] == ObjectCheck.check_white_dam) && mas[x + 2, y + 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x + 2, y + 2);
                        rez = AddTakeMove(masCopy, x + 2, y + 2, 4);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    if (x > 1 && y < 6 && (mas[x - 1, y + 1] == ObjectCheck.check_white ||
                        mas[x - 1, y + 1] == ObjectCheck.check_white_dam) && mas[x - 2, y + 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x - 2, y + 2);
                        rez = AddTakeMove(masCopy, x - 2, y + 2, 1);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    if (x > 1 && y > 1 && (mas[x - 1, y - 1] == ObjectCheck.check_white ||
                        mas[x - 1, y - 1] == ObjectCheck.check_white_dam) && mas[x - 2, y - 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x - 2, y - 2);
                        rez = AddTakeMove(masCopy, x - 2, y - 2, 2);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    break;
                case ObjectCheck.check_white:
                    if (x < 6 && y > 1 && (mas[x + 1, y - 1] == ObjectCheck.check_black ||
                        mas[x + 1, y - 1] == ObjectCheck.check_black_dam) && mas[x + 2, y - 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x + 2, y - 2);
                        rez = AddTakeMove(masCopy, x + 2, y - 2, 3);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    if (x < 6 && y < 6 && (mas[x + 1, y + 1] == ObjectCheck.check_black ||
                        mas[x + 1, y + 1] == ObjectCheck.check_black_dam) && mas[x + 2, y + 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x + 2, y + 2);
                        rez = AddTakeMove(masCopy, x + 2, y + 2, 4);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    if (x > 1 && y < 6 && (mas[x - 1, y + 1] == ObjectCheck.check_black ||
                        mas[x - 1, y + 1] == ObjectCheck.check_black_dam) && mas[x - 2, y + 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x - 2, y + 2);
                        rez = AddTakeMove(masCopy, x - 2, y + 2, 1);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    if (x > 1 && y > 1 && (mas[x - 1, y - 1] == ObjectCheck.check_black ||
                        mas[x - 1, y - 1] == ObjectCheck.check_black_dam) && mas[x - 2, y - 2] == ObjectCheck.full)
                    {
                        masCopy = SetOneStep(mas, x, y, x - 2, y - 2);
                        rez = AddTakeMove(masCopy, x - 2, y - 2, 2);
                        for (int i = 0; i < rez.Length; i++)
                            stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        flag = true;
                    }
                    break;
                case ObjectCheck.check_black_dam:
                case ObjectCheck.check_white_dam:
                    // Получаем вес всех ходов указанной дамки 
                    if (mas[x, y] == ObjectCheck.check_black_dam)
                        destArray = CheckKing(mas, x, y, ObjectCheck.check_white, ObjectCheck.check_white_dam, coorse);
                    else
                        destArray = CheckKing(mas, x, y, ObjectCheck.check_black, ObjectCheck.check_black_dam, coorse);

                    int[,] masStep = new int[destArray.Count, 2];
                    int cnt = 0;

                    // Создание массива соответствий для направлений и флагов
                    // В итоге получим максимальное значение флага по каждому из направлений движения дамки
                    int[] flags = new int[5];
                    for (int l = 0; l < destArray.Count; l++)
                    {
                        xy = (int[])destArray[l];
                        if (xy[3] > flags[xy[0]])
                            flags[xy[0]] = xy[3];
                    }

                    // Проверяем, действительно происходит взятие на каком-то из направлений
                    for (int l = 0; l < flags.Length; l++)
                        if (flags[l] > 0)
                        {
                            flag = true;
                            break;
                        }

                    if (flag)
                    {
                        for (int l = 0; l < destArray.Count; l++)
                        {

                            xy = (int[])destArray[l];
                            if (flags[xy[0]] == xy[3] && xy[3] > 0)
                            {
                                masStep[cnt, 0] = xy[1];
                                masStep[cnt, 1] = xy[2];
                                cnt++;
                            }
                        }
                        for (int l = 0; l < cnt; l++)
                        {
                            masCopy = SetOneStep(mas, x, y, masStep[l, 0], masStep[l, 1]);
                            int coor;
                            if (x > masStep[l, 0] && y > masStep[l, 1])
                                coor = 2;
                            else if (x > masStep[l, 0] && y < masStep[l, 1])
                                coor = 1;
                            else if (x < masStep[l, 0] && y > masStep[l, 1])
                                coor = 3;
                            else
                                coor = 4;
                            rez = AddTakeMove(masCopy, masStep[l, 0], masStep[l, 1], coor);
                            for (int i = 0; i < rez.Length; i++)
                                stepsList.Add(x.ToString() + y.ToString() + ":" + rez[i]);
                        }
                    }
                    break;
            }
            if (!flag)
                stepsList.Add(x.ToString() + y.ToString());

            rez = new string[stepsList.Count];
            for (int i = 0; i < stepsList.Count; i++)
                rez[i] = (string)stepsList[i];
            return rez;
        }

        /// <summary>
        /// Возвращаем массив из возможных ходов в указанной позиции для указанного игрока
        /// </summary>
        /// <param name="mas">Текущая позиция</param>
        /// <param name="whoMove">Кто ходит</param>
        /// <returns>Возвращаем массив с ходами в виде XY:XY{:XY:XY:XY...}</returns>
        private string[] GetAllMustStep(ObjectCheck[,] mas, ColorCheck whoMove)
        {
            ArrayList figure;
            int[] xy = new int[2];
            ArrayList stepsList = new ArrayList();
            string[] node = new string[2];

            // Проверка, на то, можно ли чем нибудь кого-нибудь съесть
            // Если да - то запоминаем эти фигуры
            figure = CheckEatAll(mas, whoMove);

            // Если мы можем съесть - то будем искать все возможные съедения
            if (figure.Count > 0)
            {
                for (int i = 0; i < figure.Count; i++)
                {
                    xy = (int[])figure[i];
                    string[] steps = AddTakeMove(mas, xy[0], xy[1], 0);
                    for (int j = 0; j < steps.Length; j++)
                        stepsList.Add(steps[j]);
                }
            }
            else			// Иначе смотрим, кто куда может сходить
            {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                    {
                        node[0] = i.ToString() + j.ToString();
                        if (whoMove == ColorCheck.white)
                            switch (mas[i, j])
                            {
                                case ObjectCheck.check_white:
                                    if (i > 0)
                                    {
                                        if (j > 0 && mas[i - 1, j - 1] == ObjectCheck.full)
                                        {
                                            node[1] = (i - 1).ToString() + (j - 1).ToString();
                                            stepsList.Add(node[0] + ":" + node[1]);
                                        }
                                        if (j < 7 && mas[i - 1, j + 1] == ObjectCheck.full)
                                        {
                                            node[1] = (i - 1).ToString() + (j + 1).ToString();
                                            stepsList.Add(node[0] + ":" + node[1]);
                                        }
                                    }
                                    break;
                                case ObjectCheck.check_white_dam:
                                    AddKingMove(mas, i, j, ref stepsList);
                                    break;
                            }
                        else
                            switch (mas[i, j])
                            {
                                case ObjectCheck.check_black:
                                    if (i < 7)
                                    {
                                        if (j > 0 && mas[i + 1, j - 1] == ObjectCheck.full)
                                        {
                                            node[1] = (i + 1).ToString() + (j - 1).ToString();
                                            stepsList.Add(node[0] + ":" + node[1]);
                                        }
                                        if (j < 7 && mas[i + 1, j + 1] == ObjectCheck.full)
                                        {
                                            node[1] = (i + 1).ToString() + (j + 1).ToString();
                                            stepsList.Add(node[0] + ":" + node[1]);
                                        }
                                    }
                                    break;
                                case ObjectCheck.check_black_dam:
                                    AddKingMove(mas, i, j, ref stepsList);
                                    break;
                            }
                    }
            }
            return (string[])stepsList.ToArray(typeof(string));
        }

        /// <summary>
        /// Сделать одну итерацию хода (если он состоит из нескольких)
        /// </summary>
        /// <param name="mas">Текущая позиция</param>
        /// <param name="st1">Координаты фигуры, которая ходит</param>
        /// <param name="st2">Координаты поля, куда ходит фигура</param>
        /// <returns>Возвращается получившееся после хода поле</returns>
        private void DoStep(ref ObjectCheck[,] mas, int[] from, int[] to)
        {
            mas[to[0], to[1]] = mas[from[0], from[1]];
            mas[from[0], from[1]] = ObjectCheck.full;

            if (mas[to[0], to[1]] == ObjectCheck.check_black && to[0] == 7)
            {
                mas[to[0], to[1]] = ObjectCheck.check_black_dam;
            }
            else if (mas[to[0], to[1]] == ObjectCheck.check_white && to[0] == 0)
            {
                mas[to[0], to[1]] = ObjectCheck.check_white_dam;
            }

            int x1, y1, x2, y2;				// Удаляем шашку, которую съели, если она есть

            if (from[0] > to[0])
            {
                x1 = to[0];
                y1 = to[1];
                x2 = from[0];
                y2 = from[1];
            }
            else
            {
                x2 = to[0];
                y2 = to[1];
                x1 = from[0];
                y1 = from[1];
            }

            int w = y1;
            for (int q = x1 + 1; q < x2; q++)
            {
                if (y1 > y2)
                    w--;
                else
                    w++;
                if (mas[q, w] != ObjectCheck.full)
                {
                    mas[q, w] = ObjectCheck.full;
                    break;
                }
            }
        }

        /// <summary>
        /// Сделать указанный ход в указанной позиции
        /// </summary>
        /// <param name="mas">Позиция на доске</param>
        /// <param name="step">Ход в виде XY:XY{:XY:XY:XY...}</param>
        /// <returns>Возвратит получившуюся позицию</returns>
        private ObjectCheck[,] DoAllStep(ObjectCheck[,] mas, string step)
        {
            ObjectCheck[,] newMas = new ObjectCheck[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    newMas[i, j] = mas[i, j];

            ArrayList steps = new ArrayList();
            int n = 0;
            while (n < step.Length)
            {
                int[] st = new int[2];
                st[0] = Convert.ToInt32(step.Substring(n, 1));
                st[1] = Convert.ToInt32(step.Substring(n + 1, 1));
                n += 3;
                steps.Add(st);
            }

            for (int i = 1; i < steps.Count; i++)
                DoStep(ref newMas, (int[])steps[i - 1], (int[])steps[i]);

            return newMas;
        }

        /// <summary>
        /// Текущая глубина расчёта
        /// </summary>
        private int curDepth = 0;

        /// <summary>
        /// Переменная, останавливающая или возобновляющая работу агента о добавлению глубина расчёта
        /// </summary>
        private bool stopAdd = false;

        /// <summary>
        /// Переменная для залочивания кусков кода
        /// </summary>
        private Object thisLock = new Object();

        /// <summary>
        /// Устанавливает информацию о текущем состоянии просчитанныx позиций
        /// </summary>
        private void SetCmpInfo()
        {
            if (stepsThree[0] == null || stepsThreeLength[0] == 0)
            {
                cmpInfo.currentCountPosition = 0;
                cmpInfo.currentMaxDepth = 0;
                cmpInfo.currentMark = "?";
                cmpInfo.currentRealMark = "?";
                cmpInfo.currentDepth = 0;
                return;
            }

            cmpInfo.currentMark = stepsThree[0][0].mark;
            cmpInfo.currentRealMark = stepsThree[0][0].realMark;
            cmpInfo.currentDepth = curDepth;

            int n = 0;
            cmpInfo.currentCountPosition = 0;
            while (stepsThree[n] != null)
            {
                cmpInfo.currentCountPosition += stepsThreeLength[n];
                n++;
            }
            cmpInfo.currentMaxDepth = n;
        }

        /// <summary>
        /// Рекурсивное удаление всех ветвей дерева, которые выходят из указанного узла
        /// </summary>
        /// <param name="x">строка в дереве</param>
        /// <param name="y">столбец в дереве</param>
        private void DeleteAllStep()
        {
            int x = 2;
            while (stepsThree[x] != null)
            {
                for (int i = 0; i < stepsThreeLength[x]; i++)
                {
                    if (!stepsThree[x - 1][stepsThree[x][i].numberPrev].valid)
                    {
                        stepsThree[x][i].valid = false;
                    }
                }
                x++;
            }
        }


        /// <summary>
        /// Удаляет несостоявшиеся узлы из нашего дерева
        /// </summary>
        /// <param name="step">Сделанный ход</param>
        private void ThinOutStepsThree(string step)
        {
            if (stepsThree[1] == null)
                return;

            logClass.WriteLine("ThinOutStepsThree step=" + step);
            for (int i = 0; i < stepsThree[1].Length; i++)
            {
                if (stepsThree[1][i].step != step)
                {
                    stepsThree[1][i].valid = false;
                }
            }
            DeleteAllStep();
            logClass.WriteLine("DeleteAllStep complete");

            int[] convertTable;
            Node[][] stepsThreeCopy = new Node[stepsThree.Length][];
            int[] stepsThreeLengthCopy = new int[stepsThree.Length];
            int n = 1;
            while (stepsThree[n] != null)
            {

                stepsThreeCopy[n - 1] = new Node[10];
                convertTable = new int[stepsThreeLength[n]];

                for (int i = 0; i < stepsThreeLength[n]; i++)
                {
                    if (stepsThree[n][i].valid)
                    {
                        AddNode(ref stepsThreeCopy, ref stepsThreeLengthCopy, n - 1, stepsThree[n][i]);

                        convertTable[i] = stepsThreeLengthCopy[n - 1] - 1;
                    }
                    else
                    {
                        convertTable[i] = -1;
                    }
                }
                for (int j = 0; j < stepsThreeLength[n + 1]; j++)
                {
                    if (stepsThree[n + 1][j].valid/* && convertTable[stepsThree[n + 1][j].numberPrev] != -1*/)
                    {
                        stepsThree[n + 1][j].numberPrev = convertTable[stepsThree[n + 1][j].numberPrev];
                    }
                }
                n++;
            }
            logClass.WriteLine("stepsThreeCopy create complete");

            stepsThreeCopy[0][0].numberPrev = -1;
            stepsThree = new Node[50][];
            stepsThreeLength = new int[50];

            for (int i = 0; i < stepsThreeCopy.Length && stepsThreeCopy[i] != null && stepsThreeLengthCopy[i] > 0; i++)
            {
                stepsThree[i] = stepsThreeCopy[i];
                stepsThreeLength[i] = stepsThreeLengthCopy[i];
            }

            curDepth--;
            SetCmpInfo();
            logClass.WriteLine("ThinOutStepsThree complete");
        }

        /// <summary>
        /// Добавляет уровень глубины расчёта после указанного уровня
        /// </summary>
        /// <param name="x">Уровень, после которого надо добавлять новые возможные ходы</param>
        private void AddDepth(int x)
        {
            logClass.WriteLine("AddDepth x=" + x.ToString());
            if (stepsThree[x] == null)
            {
                return;
            }
            for (int i = 0; i < stepsThreeLength[x]; i++)
            {
                if (stopAdd)
                {
                    logClass.WriteLine("AddDepth hand return");
                    return;
                }

                //Проверяем, что если оценка позиции очевидна - и глубина достаточна - то не смотрим глубже
                /*if(x >= maxDepth && stepsThree[x][i].mark != "?")
                  if(Math.Abs(Convert.ToDouble(stepsThree[x][i].mark)) > 30)
                    continue;*/

                // Проверяем, есть ли на следующем уровне дерева узлы, полученные из текущего ( №i )
                bool flag = false;
                if (stepsThree[x + 1] != null)
                {
                    for (int j = 0; j < stepsThreeLength[x + 1]; j++)
                    {
                        if (stepsThree[x + 1][j].numberPrev == i)
                        {
                            flag = true;
                            break;
                        }
                    }

                    // Если есть - то ничего не делаем
                    if (flag)
                        continue;
                }
                else
                    stepsThree[x + 1] = new Node[10];

                Node node = stepsThree[x][i];
                ObjectCheck[,] masSave = node.mas;
                ColorCheck whoMove = node.whoMove;
                ColorCheck whoMoveNext = ColorCheck.black;
                if (whoMove == ColorCheck.black)
                    whoMoveNext = ColorCheck.white;
                string[] mustSteps = GetAllMustStep(masSave, whoMove);
                for (int j = 0; j < mustSteps.Length; j++)
                {
                    ObjectCheck[,] mas = DoAllStep(masSave, mustSteps[j]);
                    node.numberPrev = i;
                    node.step = mustSteps[j];
                    node.mas = mas;
                    node.realMark = node.mark = Evaluation(mas, whoMoveNext);
                    node.whoMove = whoMoveNext;
                    AddNode(ref stepsThree, ref stepsThreeLength, x + 1, node);
                    cmpInfo.currentCountPosition++;
                }
            }
            logClass.WriteLine("AddDepth complete");
        }

        /// <summary>
        /// Возвращает список узлов на последнем уровне, для которых не определена оценка
        /// </summary>
        /// <param name="mas"></param>
        /// <returns></returns>
        private int[] GetUnknownNodes(int x)
        {
            int[] nodesList = new int[stepsThreeLength[x]];
            int nlCnt = 0;

            for (int i = 0; i < stepsThreeLength[x]; i++)
            {
                Node node = stepsThree[x][i];
                if (node.mark == "?")
                    nodesList[nlCnt++] = i;
            }

            int[] res = new int[nlCnt];
            for (int i = 0; i < nlCnt; i++)
                res[i] = nodesList[i];
            return res;
        }

        /// <summary>
        /// Просматривает дерево и находит в нём узлы с неопределённой оценкой и для них просчитывает ходы 
        /// на большую глубину
        /// </summary>
        private void AddDepthToUnknown()
        {
            logClass.WriteLine("AddDepthToUnknown curDepth=" + curDepth.ToString());

            if (stepsThree[curDepth] == null)
            {
                return;
            }

            for (int i = curDepth + 1; i < stepsThree.Length; i++)
            {
                stepsThree[i] = null;
                stepsThreeLength[i] = 0;
            }

            int x = curDepth;
            int[] nodesList = GetUnknownNodes(x);

            while (nodesList.Length > 0)
            {
                stepsThree[x + 1] = new Node[10];

                for (int i = 0; i < nodesList.Length; i++)
                {
                    Node node = stepsThree[x][nodesList[i]];
                    ObjectCheck[,] masSave = node.mas;
                    ColorCheck whoMove = node.whoMove;
                    ColorCheck whoMoveNext = ColorCheck.black;
                    if (whoMove == ColorCheck.black)
                        whoMoveNext = ColorCheck.white;
                    string[] mustSteps = GetAllMustStep(masSave, whoMove);
                    for (int j = 0; j < mustSteps.Length; j++)
                    {
                        ObjectCheck[,] mas = DoAllStep(masSave, mustSteps[j]);
                        node.numberPrev = nodesList[i];
                        node.step = mustSteps[j];
                        node.mas = mas;
                        node.realMark = node.mark = Evaluation(mas, whoMoveNext);
                        node.whoMove = whoMoveNext;
                        AddNode(ref stepsThree, ref stepsThreeLength, x + 1, node);
                        cmpInfo.currentCountPosition++;
                    }
                }

                x++;
                nodesList = GetUnknownNodes(x);
            }
            logClass.WriteLine("AddDepthToUnknown complete");
        }

        /// <summary>
        /// Установить итоговые оценки для всех узлов
        /// Устанавливает оценку для данного узла как наименьшую из всех 
        /// оценок дочерних ходов, взятую с противоположным знаком
        /// </summary>
        /// <param name="depth">Текущая глубина</param>
        /// <param name="cnt">Номер хода</param>
        /// <param name="?"></param>
        private void SetMark()
        {
            logClass.WriteLine("SetMark");
            int stepsThreeLen = 0;
            while (stepsThree[stepsThreeLen] != null)
                stepsThreeLen++;

            for (int i = stepsThreeLen - 1; i > 0; i--)
            {
                for (int j = 0; j < stepsThreeLength[i]; j++)
                {
                    stepsThree[i - 1][stepsThree[i][j].numberPrev].realMark = "?";
                }
                for (int j = 0; j < stepsThreeLength[i]; j++)
                {
                    if (stepsThree[i - 1][stepsThree[i][j].numberPrev].realMark == "?")
                    {
                        stepsThree[i - 1][stepsThree[i][j].numberPrev].realMark = (-Convert.ToInt32(stepsThree[i][j].realMark)).ToString();
                    }
                    else if (Convert.ToInt32(stepsThree[i - 1][stepsThree[i][j].numberPrev].realMark) < -Convert.ToInt32(stepsThree[i][j].realMark))
                    {
                        stepsThree[i - 1][stepsThree[i][j].numberPrev].realMark = (-Convert.ToInt32(stepsThree[i][j].realMark)).ToString();
                    }
                }
            }
            logClass.WriteLine("Agent SetMark complete");
        }

        /// <summary>
        /// Агент, который запускается с началом игры и постоянно добавляет уровень глубины расчёта
        /// текущий уровень помещается в переменную curDepth
        /// </summary>
        private void AddDepthAgent()
        {
        repeat:
            if (stopAdd)
            {
                Thread.Sleep(100);
                goto repeat;
            }

            lock (thisLock)
            {
                /*for(int i = 0; i < curDepth - 1; i++)
                  AddDepth(i);*/

                while (curDepth > 1 && stepsThree[curDepth] == null)
                {
                    curDepth--;
                }

                if ((stepsThree[curDepth] == null || stepsThreeLength[curDepth] == 0) && curDepth < maxDepth)
                {
                    logClass.WriteLine("AgentAddDepth EXIT");
                    return;
                }

                AddDepth(curDepth);
                curDepth++;
                AddDepthToUnknown();
                SetMark();

                SetCmpInfo();

                if (stopAdd)
                    curDepth--;
            }
            goto repeat;
        }

        /// <summary>
        /// Обработка очередного сделанного хода
        /// </summary>
        /// <param name="step">Ход в формате XY:XY{:XY:XY:XY...}</param>    
        /// <returns>Текущая оценка позиции</returns>
        public void SetStep(string step)
        {
            logClass.WriteLine("SetStep step=" + step);
            while (curDepth < maxDepth && stepsThreeLength[curDepth] != 0)
            {
                Thread.Sleep(1000);
            }

            stopAdd = true;
            lock (thisLock)
            {
                ThinOutStepsThree(step);
                if (stepsThree[0] == null)
                {
                    logClass.WriteLine("SetStep stepsThree[0]=null !!!!!");
                    return;
                }
                stopAdd = false;
            }
            logClass.WriteLine("SetStep complete");
        }

        /// <summary>
        /// Получить новый ход компьютера в формате XY:XY{:XY:XY:XY...}
        /// </summary>
        /// <returns></returns>
        public string GetComputerStep()
        {
            logClass.WriteLine("GetComputerStep");
            while (curDepth < maxDepth && stepsThree[curDepth] != null && stepsThreeLength[curDepth] != 0)
            {
                Thread.Sleep(1000);
            }
            stopAdd = true;

            lock (thisLock)
            {
                try
                {
                    // Лучший ход в формате XY:XY{:XY:XY:XY...}
                    string step = "";

                    // Выбрать список из лучших ходов среди ходов первого уровня
                    string bestMark = stepsThree[0][0].realMark;
                    if (bestMark != "?")
                    {
                        bestMark = Convert.ToString(Convert.ToDouble(bestMark) * -1);
                    }

                    if (stepsThree[1] == null || stepsThreeLength[1] == 0)
                        return "";

                    ArrayList bestSteps = new ArrayList();
                    for (int i = 0; i < stepsThreeLength[1]; i++)
                    {

                        string mark = stepsThree[1][i].realMark;
                        if (mark == bestMark)
                        {
                            bestSteps.Add(i);
                        }
                        else
                        {
                            try
                            {
                                double iMark = Convert.ToDouble(mark);
                                double iBestMark = Convert.ToDouble(bestMark);
                                if (Math.Abs(iMark - iBestMark) <= 5.0)
                                {
                                    bestSteps.Add(i);
                                }
                            }
                            catch
                            {
                            }
                        }
                    }

                    Random rand = new Random();
                    int bestNum = rand.Next(bestSteps.Count);

                    step = stepsThree[1][(int)bestSteps[bestNum]].step;

                    logClass.WriteLine("GetComputerStep return " + step);
                    return step;

                }
                finally
                {

                    stopAdd = false;
                }
            }
        }
    }
}
