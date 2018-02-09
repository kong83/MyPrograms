using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Sudoku
{
    /// <summary>
    /// Типы ячеек на поле
    /// </summary>
    public enum CellType
    {
        /// <summary>
        /// Пустая ячейка
        /// </summary>
        clear,

        /// <summary>
        /// Заполненная
        /// </summary>
        value,

        /// <summary>
        /// Есть дополнительные метки
        /// </summary>
        smartLabel,

        /// <summary>
        /// Неизменяемая
        /// </summary>
        closed
    }

    /// <summary>
    /// Класс, содержащий информацию о ячейках
    /// </summary>
    public class Cell
    {
        /// <summary>
        /// Ссылка на mainForm
        /// </summary>
        public MainForm mainForm;

        /// <summary>
        /// Строка текущей ячейки
        /// </summary>
        int x;
        /// <summary>
        /// Строка текущей ячейки
        /// </summary>
        public int X
        {
            get
            {
                return x;
            }
        }

        /// <summary>
        /// Столбец текущей ячейки
        /// </summary>
        int y;
        /// <summary>
        /// Столбец текущей ячейки
        /// </summary>
        public int Y
        {
            get
            {
                return y;
            }
        }

        /// <summary>
        /// Текст в текущей ячейке
        /// </summary>
        int cellText;
        /// <summary>
        /// Текст в текущей ячейке
        /// </summary>
        public int CellText
        {
            get
            {
                return cellText;
            }

            set
            {
                if (mainForm.typeAction == TypeAction.none)
                {
                    MessageBox.Show("Игра ещё на началась. Ошибка в приложении. Сообщите автору.", "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (mainForm.timer1.Enabled)
                {
                    return;
                }
                else if (value < 0 || value > 9)
                {
                    MessageBox.Show("Недопустимое значение", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (cellType == CellType.closed && mainForm.typeAction == TypeAction.game)
                {
                    MessageBox.Show("Значение в данной клетке изменять нельзя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    cellText = value;
                    DeleteAllSmartLabels();
                    SetTextToCell();
                    if (value != 0)
                    {
                        if (mainForm.typeAction == TypeAction.game)
                        {
                            cellType = CellType.value;
                            SetCellType(Color.White);
                        }
                        else
                        {
                            cellType = CellType.closed;
                            SetCellType(Color.LightGray);
                        }

                        bool isAllFilled = true;
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (mainForm.cells[i, j].CellType == CellType.clear ||
                                    mainForm.cells[i, j].CellType == CellType.smartLabel)
                                {
                                    isAllFilled = false;
                                    j = i = 10;
                                }
                            }
                        }
                        if (!isAllFilled)
                        {
                            CheckValue();
                        }
                    }
                    else
                    {
                        cellType = CellType.clear;
                        SetCellType(Color.White);
                    }
                }
            }
        }

        /// <summary>
        /// Текст в дополнительных ячейках
        /// </summary>
        private int[,] labels;

        public int GetSmartLabel(int i, int j)
        {
            return labels[i, j];
        }

        public string GetSmartLabelText(int i, int j)
        {
            if (labels[i, j] == 0)
            {
                return "";
            }
            else
            {
                return labels[i, j].ToString();
            }
        }

        /// <summary>
        /// Тип текущей ячейки
        /// </summary>
        private CellType cellType;
        /// <summary>
        /// Тип текущей ячейки
        /// </summary>
        public CellType CellType
        {
            get 
            {
                return cellType;
            }
        }

        public Cell()            
        {
        }

        /// <summary>
        /// Конструктор для переменной типа Cell
        /// </summary>
        /// <param name="newX">Строка новой текущей ячейки</param>
        /// <param name="newY">Столбец новой текущей ячейки</param>        
        public Cell(MainForm mf, int newX, int newY):
            this(mf, newX, newY, CellType.clear, 0)
        {            
        }

        
        /// <summary>
        /// Конструктор для переменной типа Cell
        /// </summary>
        /// <param name="newX">Строка новой текущей ячейки</param>
        /// <param name="newY">Столбец новой текущей ячейки</param>
        /// <param name="newType">Тип ячейки</param>
        /// <param name="newText">Значение</param>
        public Cell(MainForm mf, int newX, int newY, CellType newType, int newText)
        {
            x = newX;
            y = newY;
            cellText = newText;            
            cellType = newType;
            mainForm = mf;

            Panel p = ((Panel)(mainForm.Controls["panel" + x + y]));
            Label l = (Label)p.Controls["label" + x + y];
            Font f;
            if (cellType == CellType.closed)
            {
                f = new Font(l.Font.FontFamily, l.Font.Size, FontStyle.Bold);
                p.BackColor = l.BackColor = Color.LightGray;
            }
            else
            {
                f = new Font(l.Font.FontFamily, l.Font.Size, FontStyle.Regular);
                p.BackColor = l.BackColor = Color.White;
            }
            l.Font = f;

            cellText = newText;
            
            labels = new int[3, 2];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    labels[i, j] = 0;
                }
            }

            SetTextToCell();
            DeleteAllSmartLabels();
        }


        /// <summary>
        /// Конструктор для переменной типа Cell
        /// </summary>
        /// <param name="newX">Строка новой текущей ячейки</param>      
        public Cell(Cell newCell)
        {
            mainForm = newCell.mainForm;
            x = newCell.X;
            y = newCell.Y;

            labels = new int[3, 2];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (newCell.CellType == CellType.smartLabel)
                    {
                        labels[i, j] = newCell.GetSmartLabel(i, j);
                        SetSmartLabel(i, j, labels[i, j]);
                    }
                    else
                    {
                        labels[i, j] = 0;
                        DeleteSmartLabel(i, j);
                    }
                }
            }
            cellType = newCell.CellType;
            cellText = newCell.CellText;
            SetTextToCell();
            if (cellText != 0)
            {
                if (mainForm.typeAction == TypeAction.game)
                {
                    cellType = CellType.value;
                    SetCellType(Color.White);
                }
                else
                {
                    cellType = CellType.closed;
                    SetCellType(Color.LightGray);
                }
                CheckValue();
            }
            else
            {
                if (cellType != CellType.smartLabel)
                {
                    cellType = CellType.clear;
                }
                SetCellType(Color.White);
            }
        }


        /// <summary>
        /// Установка цвета фона для ячейки
        /// </summary>
        /// <param name="color">Цвет</param>
        public void SetCellType(Color color)
        {
            Panel p = (Panel)mainForm.Controls["panel" + x + y];
            Label l = (Label)p.Controls["label" + x + y];
            p.BackColor = l.BackColor = color;
            if (color != Color.Red)
            {
                Font f;
                if (color == Color.LightGray)
                {
                    f = new Font(l.Font.FontFamily, l.Font.Size, FontStyle.Bold);
                }
                else
                {
                    f = new Font(l.Font.FontFamily, l.Font.Size, FontStyle.Regular);
                }
                l.Font = f;
            }
        }
        

        /// <summary>
        /// Помещение текста в метку
        /// </summary>
        private void SetTextToCell()
        {
            mainForm.isPositionSave = false;
            Panel p = ((Panel)(mainForm.Controls["panel" + x + y]));

            if (cellText != 0)
            {
                ((Label)p.Controls["label" + x + y]).Text = cellText.ToString();
            }
            else
            {
                ((Label)p.Controls["label" + x + y]).Text = "";
            }
        }


        /// <summary>
        /// Выделение ячейки
        /// </summary>        
        public void SelectCell()
        {
            Panel p = (Panel)mainForm.Controls["upPanel"];
            p.Visible = false;
            p.Location = new Point(mainForm.left + y * 50, mainForm.top + x * 50);
            p.Visible = true;

            p = (Panel)mainForm.Controls["leftPanel"];
            p.Visible = false;
            p.Location = new Point(mainForm.left + y * 50, mainForm.top + x * 50);
            p.Visible = true;

            p = (Panel)mainForm.Controls["downPanel"];
            p.Visible = false;
            p.Location = new Point(mainForm.left + y * 50, mainForm.top + (x + 1) * 50 - 2);
            p.Visible = true;

            p = (Panel)mainForm.Controls["rightPanel"];
            p.Visible = false;
            p.Location = new Point(mainForm.left + (y + 1) * 50 - 2, mainForm.top + x * 50);
            p.Visible = true;
        }


        /// <summary>
        /// Проверка введённого значения на правильность
        /// </summary>
        private void CheckValue()
        {
            bool isError = false;

            mainForm.timer1.Interval = mainForm.sleepTime;

            // Получаем массив с содержимым ячеек
            int[,] board = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = mainForm.cells[i, j].CellText;                    
                }
            }

            //
            // Ищем такую же цифру в строке
            //
            for (int j = 0; j < 9; j++)
            {
                if (board[x, j] == cellText && j != y)
                {
                    isError = true;
                    break;
                }
            }

            //
            // Ищем такую же цифру в столбце
            //
            if (!isError)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (board[i, y] == cellText && i != x)
                    {
                        isError = true;
                        break;
                    }
                }
            }


            //
            // Ищем такую же цифру в квадрате
            //
            if (!isError)
            {
                int rowSquare = x / 3;
                int colSquare = y / 3;
                for (int i = rowSquare * 3; i < rowSquare * 3 + 3; i++)
                {
                    for (int j = colSquare * 3; j < colSquare * 3 + 3; j++)
                    {
                        if (board[i, j] == cellText && (i != x || j != y))
                        {
                            isError = true;
                            break;
                        }
                    }
                }
            }

            if (isError)
            {
                SetCellType(Color.Red);

                mainForm.errorCells = new ArrayList();
                mainForm.errorCells.Add(new int[2] { x, y });
                mainForm.timer1.Enabled = true;
            }
        }
        

        /// <summary>
        /// Создание дополнительных меток в ячейке на основе заполненных меток в текущей
        /// выделенной ячейке
        /// </summary>
        /// <param name="cell"></param>
        public void SetSmartLabel(int i, int j, int value)
        {
            if (value < 0 || value > 9)
            {
                MessageBox.Show("Недопустимое значение дополнительной метки", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }                        
            
            if (cellType == CellType.value)
            {
                CellText = 0;
            }

            mainForm.isPositionSave = false;            
            if (value != 0)
            {
                labels[i, j] = value;
                Panel p = (Panel)mainForm.Controls["panel" + x + y];
                cellType = CellType.smartLabel;
                Label l;
                if (!p.Controls.ContainsKey("label" + x + y + (i * 2 + j).ToString()))
                {
                    l = new Label();
                    l.BackColor = Color.White;
                    l.BorderStyle = BorderStyle.FixedSingle;
                    l.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    l.Location = new Point(2 + j * mainForm.left * 2, 2 + i * mainForm.left);
                    l.Name = "label" + x + y + (i * 2 + j).ToString();
                    l.Margin = new Padding(0);
                    l.Size = new Size(16, 16);
                    l.TabIndex = i + j + 1;
                    l.MouseDoubleClick += new MouseEventHandler(mainForm.cell_MouseDoubleClick);
                    l.MouseClick += new MouseEventHandler(mainForm.cell_MouseClick);
                    p.Controls.Add(l);
                    l.BringToFront();
                }
                else
                {
                    l = (Label)p.Controls["label" + x + y + (i * 2 + j).ToString()];
                }
                l.Text = labels[i, j].ToString();
            }
            else
            {
                DeleteSmartLabel(i, j);                
            }            
        }


        /// <summary>
        /// Проверка после удаления дополнительной метки, остались ли в данной клетке ещё метки
        /// </summary>
        private void CheckCellType()
        {
            if (cellType != CellType.smartLabel)
            {
                return;
            }

            bool isEmpty = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (labels[i, j] != 0)
                    {
                        isEmpty = false;
                        i = j = 3;
                    }
                }
            }

            if (isEmpty)
            {
                cellType = CellType.clear;
            }
        }


        /// <summary>
        /// Удаление дополнительной метки
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void DeleteSmartLabel(int i, int j)
        {
            Panel p = (Panel)mainForm.Controls["panel" + x + y];
            labels[i, j] = 0;
            if (p.Controls.ContainsKey("label" + x + y + (i * 2 + j).ToString()))
            {
                p.Controls.RemoveByKey("label" + x + y + (i * 2 + j).ToString());
            }
            CheckCellType();
        }


        /// <summary>
        /// Удаление всех дополнительных меток
        /// </summary>
        private void DeleteAllSmartLabels()
        {
            Panel p = ((Panel)(mainForm.Controls["panel" + x + y]));
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    DeleteSmartLabel(i, j);
                }
            }
        }

        /// <summary>
        /// Создание копии данной ячейки
        /// </summary>
        /// <returns></returns>
        public Cell Clone()
        {
            Cell result = new Cell();
            result.mainForm = mainForm;
            result.x = x;
            result.y = y;
            result.cellType = cellType;
            result.cellText = cellText;
            
            result.labels = new int[3, 2];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    result.labels[i, j] = labels[i, j];
                }
            }
            return result;
        }
    }
}
