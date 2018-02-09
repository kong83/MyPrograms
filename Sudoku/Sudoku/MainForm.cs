using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Sudoku
{
    /// <summary>
    /// Тип действий
    /// </summary>
    public enum TypeAction
    {
        /// <summary>
        /// Игра
        /// </summary>
        game,

        /// <summary>
        /// Расстановка позиции
        /// </summary>
        creating,

        /// <summary>
        /// Ничего не происходит
        /// </summary>
        none
    }

    /// <summary>
    /// Тип действий
    /// </summary>
    public struct CurrentCell
    {
        /// <summary>
        /// Игра
        /// </summary>
        public int X;

        /// <summary>
        /// Расстановка позиции
        /// </summary>
        public int Y;
    }

    public partial class MainForm : Form
    {
        /// <summary>
        /// Класс, содержащий информацию о значениях выделенной ячейки
        /// </summary>
        public Cell[,] cells;

        /// <summary>
        /// Класс для хранения истории ходов
        /// </summary>
        public HistoryClass historyClass;

        /// <summary>
        /// Координаты выделенной в данный момент ячейки
        /// </summary>
        private CurrentCell currentCell;

        /// <summary>
        /// Путь к файлу уровня
        /// </summary>
        private string pathToLevel = "";

        /// <summary>
        /// Список ячеек с ошибками
        /// </summary>
        public ArrayList errorCells = new ArrayList();

        /// <summary>
        /// Путь к файлу помощи
        /// </summary>
        public readonly string helpPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SudokuHelp.chm");

        /// <summary>
        /// Время задержки
        /// </summary>
        public int sleepTime = 2000;

        /// <summary>
        /// Время задержки
        /// </summary>
        public bool useRunning = true;

        /// <summary>
        /// Не было ли изменений после сохранения позиции
        /// </summary>
        public bool isPositionSave = true;

        /// <summary>
        /// Смещение сверху
        /// </summary>
        public readonly int top = 36;

        /// <summary>
        /// Смещение слева
        /// </summary>
        public readonly int left = 15;

        /// <summary>
        /// Время, прошедшее с начала игры
        /// </summary>
        private DateTime gameTime;

        /// <summary>
        /// Текущее действие
        /// </summary>
        public TypeAction typeAction = TypeAction.none;

        /// <summary>
        /// Список с параметрами
        /// </summary>
        public List<string> listOfParameters = new List<string>() { "sleepTime", "useRunning" };

        /// <summary>
        /// Переводит текущее время в красиво оформленную строку для пользователя
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private string DateTimeToString(DateTime time)
        {
            return gameTime.Hour.ToString("D2") + ":" +
                gameTime.Minute.ToString("D2") + ":" +
                gameTime.Second.ToString("D2");
        }


        /// <summary>
        /// Показ сообщения в тот момент, когда ничего ещё не выбрано
        /// И скрытие элементов
        /// </summary>
        private void HideAllElements()
        {
            timerTime.Enabled = false;
            gameTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            pathToLevel = "";
            labelLevel.Text = "";
            labelInfo.Text = "Нажмите кнопку \"Решение\" для выбора судоку для разгадывания.\r\nНажмите кнопку \"Создание\" для создания нового судоку.";
            ((Panel)this.Controls["leftPanel"]).Visible =
            ((Panel)this.Controls["rightPanel"]).Visible =
            ((Panel)this.Controls["upPanel"]).Visible =
            ((Panel)this.Controls["downPanel"]).Visible =
            buttonSave.Visible = buttonOpen.Visible = buttonClear.Visible = buttonPause.Visible =
            labelLevel.Visible = labelTimeInfo.Visible = labelTime.Visible = false;
        }


        /// <summary>
        /// Показ элементов для создания нового судоку
        /// и показ соответствующего информационного сообщения
        /// </summary>
        private void ShowCreateElements()
        {
            HideAllElements();
            labelInfo.Text = "Режим создания судоку:\r\nЗаполните необходимые поля и сохраните результат в папке Position. Посе этого можно будет загружать созданные уровни.";
            buttonSave.Visible = buttonClear.Visible = true;
            buttonCreate.FlatStyle = FlatStyle.Popup;
            buttonSolution.FlatStyle = FlatStyle.Standard;
        }


        /// <summary>
        /// Показ элементов для загрузки и решения судоку
        /// и показ соответствующего информационного сообщения
        /// </summary>
        private void ShowSolutionElements()
        {
            HideAllElements();
            labelInfo.Text = "Нажмите на кнопку \"Загрузить уровень\" и выберите уровень, который вы хотите начать разгадывать.";
            buttonSave.Visible = buttonOpen.Visible = labelLevel.Visible = true;
            buttonSolution.FlatStyle = FlatStyle.Popup;
            buttonCreate.FlatStyle = FlatStyle.Standard;
        }


        /// <summary>
        /// Инициализация компонентов и создание поля
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.InitialDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Position");
            openFileDialog1.FileName = "";
            saveFileDialog1.InitialDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SavedGames");
            openFileDialog1.FileName = "";

            SaveSettingsClass settingsClass = new SaveSettingsClass();
            Dictionary<string, string> settings = settingsClass.LoadParameters(listOfParameters);
            if (settings.ContainsKey("sleepTime") && !string.IsNullOrEmpty(settings["sleepTime"]))
            {
                sleepTime = Convert.ToInt32(settings["sleepTime"]);
            }
            if (settings.ContainsKey("useRunning") && !string.IsNullOrEmpty(settings["useRunning"]))
            {
                useRunning = Convert.ToBoolean(settings["useRunning"]);
            }

            //
            // Создание игровых панелей и основных меток
            //
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Panel p = new Panel();
                    p.BackColor = Color.White;
                    p.BorderStyle = BorderStyle.FixedSingle;
                    p.Location = new Point(left + j * 50, top + i * 50);
                    p.Name = "panel" + i.ToString() + j.ToString();
                    p.Size = new Size(51, 51);
                    p.TabIndex = i * 9 + j;
                    p.MouseDoubleClick += new MouseEventHandler(cell_MouseDoubleClick);
                    p.MouseDown += new MouseEventHandler(cell_MouseClick);

                    Label l = new Label();
                    l.BackColor = Color.White;
                    l.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
                    l.Location = new Point(11, 8);
                    l.Margin = new Padding(0);
                    l.Name = "label" + i.ToString() + j.ToString();
                    l.Size = new Size(25, 30);
                    l.TabIndex = 0;
                    l.MouseDoubleClick += new MouseEventHandler(cell_MouseDoubleClick);
                    l.MouseDown += new MouseEventHandler(cell_MouseClick);
                    p.Controls.Add(l);

                    this.Controls.Add(p);
                    p.BringToFront();
                }
            }

            //
            // Создание горизонтальных разделительных панелей
            //
            for (int i = 0; i < 4; i++)
            {
                Panel p = new Panel();
                p.BackColor = Color.Black;
                p.Location = new Point(left, top + i * 50 * 3 - 1);
                p.Name = "splitPanel0" + i;
                p.Size = new Size(450, 3);
                p.TabIndex = 0;
                p.TabStop = true;

                this.Controls.Add(p);
                p.BringToFront();
            }

            //
            // Создание вертикальных разделительных панелей
            //
            for (int j = 0; j < 4; j++)
            {
                Panel p = new Panel();
                p.BackColor = Color.Black;
                p.Location = new Point(left + j * 50 * 3 - 1, top);
                p.Name = "splitPanel1" + j;
                p.Size = new Size(3, 450);
                p.TabIndex = 0;
                p.TabStop = true;

                this.Controls.Add(p);
                p.BringToFront();
            }

            //
            // Создание горизонтальных выделительных панелей
            //
            for (int i = 0; i < 2; i++)
            {
                Panel p = new Panel();
                p.BackColor = Color.Red;
                p.Location = new Point(0, 0);
                p.Size = new Size(51, 3);
                p.TabIndex = 0;
                p.TabStop = true;
                p.Visible = false;

                if (i == 0)
                {
                    p.Name = "upPanel";
                }
                else
                {
                    p.Name = "downPanel";
                }
                this.Controls.Add(p);
                p.BringToFront();
            }

            //
            // Создание вертикальных выделительных панелей
            //
            for (int j = 0; j < 2; j++)
            {
                Panel p = new Panel();
                p.BackColor = Color.Red;
                p.Location = new Point(0, 0);
                p.Size = new Size(3, 51);
                p.TabIndex = 0;
                p.TabStop = true;
                p.Visible = false;
                if (j == 0)
                {
                    p.Name = "leftPanel";
                }
                else
                {
                    p.Name = "rightPanel";
                }
                this.Controls.Add(p);
                p.BringToFront();
            }

            //
            // Инициализация массива с информацией о полях
            //
            currentCell = new CurrentCell();
            cells = new Cell[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell(this, i, j);
                }
            }
            isPositionSave = true;
        }


        /// <summary>
        /// Сохранение текущей позиции
        /// </summary>
        private void SaveLevel(string saveLevelPath, int isVictory)
        {
            StringBuilder cellInfo = new StringBuilder();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cellInfo.Append("" + i + j);
                    if (cells[i, j].CellType == CellType.closed)
                    {
                        cellInfo.Append("0");
                    }
                    else
                    {
                        cellInfo.Append("1");
                    }

                    cellInfo.Append(cells[i, j].CellText);

                    for (int q = 0; q < 3; q++)
                    {
                        for (int p = 0; p < 2; p++)
                        {
                            cellInfo.Append(cells[i, j].GetSmartLabel(q, p));
                        }
                    }
                    cellInfo.Append(";");
                }
            }

            using (StreamWriter sw = new StreamWriter(saveLevelPath, false))
            {
                int time = gameTime.Millisecond + gameTime.Second * 1000 + gameTime.Minute * 1000 * 60 + gameTime.Hour * 1000 * 60 * 60;
                sw.Write(cellInfo.Remove(cellInfo.Length - 1, 1) + ":" + time + ":" + isVictory);
            }
            isPositionSave = true;
        }


        /// <summary>
        /// Загрузка выбранного уровня
        /// </summary>
        private void LoadLevel()
        {
            string fileText;
            string[] cellsInfo;
            using (StreamReader sr = new StreamReader(pathToLevel))
            {
                fileText = sr.ReadToEnd();
            }

            string[] timeInfo = fileText.Split(new char[1] { ':' });
            int millisec = Convert.ToInt32(timeInfo[1]);
            int hour = millisec / (1000 * 60 * 60);
            millisec %= (1000 * 60 * 60);
            int minute = millisec / (1000 * 60);
            millisec %= (1000 * 60);
            int second = millisec / 1000;
            millisec %= 1000;
            gameTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour, minute, second, millisec);

            cellsInfo = timeInfo[0].Split(new char[1] { ';' });
            foreach (string cell in cellsInfo)
            {
                int x = Convert.ToInt32(cell.Substring(0, 1));
                int y = Convert.ToInt32(cell.Substring(1, 1));
                int fix = Convert.ToInt32(cell.Substring(2, 1));
                int text = Convert.ToInt32(cell.Substring(3, 1));

                CellType newType = CellType.clear;
                if (fix == 0)
                {
                    newType = CellType.closed;
                }
                else if (text != 0)
                {
                    newType = CellType.value;
                }
                else
                {
                    int smartLabel = 0;
                    for (int q = 4; q < 10; q++)
                    {
                        smartLabel += Convert.ToInt32(cell.Substring(q, 1));
                    }
                    if (smartLabel > 0)
                    {
                        newType = CellType.smartLabel;
                    }
                }
                cells[x, y] = new Cell(this, x, y, newType, text);


                //
                // Получение информации о дополнительных метках для тех ячеек, 
                // которые могут изменяться и значение которых не определено
                //                
                if (newType == CellType.smartLabel)
                {
                    for (int q = 4; q < 10; q++)
                    {
                        text = Convert.ToInt32(cell.Substring(q, 1));
                        if (text != 0)
                        {
                            cells[x, y].SetSmartLabel((q - 4) / 2, (q - 4) % 2, text);
                        }
                    }
                }
            }

            panelHide.SendToBack();
            buttonClear.Visible = false;
            isPositionSave = true;

            if (timeInfo.Length < 3 || timeInfo[2] == "0")
            {
                labelInfo.Text = "Правила игры:\r\nНеобходимо расставить цифры от 1 до 9 в ячейках таким образом, чтобы в каждой строке, в каждом столбце и в каждом выделенном квадрате все цифры были различны.";
                buttonPause.Visible = labelTime.Visible = labelTimeInfo.Visible = true;
                buttonPause.Text = "Пауза";
                typeAction = TypeAction.game;
                timerTime.Enabled = true;
                currentCell.X = currentCell.Y = 0;
                cells[currentCell.X, currentCell.Y].SelectCell();
                historyClass = new HistoryClass();
            }
            else
            {
                labelInfo.Text = "Данный уровень пройден\r\n\r\nВремя прохождения уровня: " + DateTimeToString(gameTime) + ".";
                buttonPause.Visible = labelTime.Visible = labelTimeInfo.Visible = false;
                timerTime.Enabled = false;
                typeAction = TypeAction.none;
            }
        }


        /// <summary>
        /// Действия при присваивании значения ячейке
        /// </summary>
        /// <param name="value"></param>
        private void SetNewValue(int value)
        {
            Cell oldCell = cells[currentCell.X, currentCell.Y].Clone();
            cells[currentCell.X, currentCell.Y].CellText = value;
            Cell newCell = cells[currentCell.X, currentCell.Y].Clone();
            if (typeAction == TypeAction.game)
            {
                historyClass.AddStep(oldCell, newCell);
            }

            bool isAllFilled = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].CellType == CellType.clear ||
                        cells[i, j].CellType == CellType.smartLabel)
                    {
                        isAllFilled = false;
                        j = i = 10;
                    }
                }
            }

            if (isAllFilled)
            {
                check_Click(null, null);
            }
        }


        /// <summary>
        /// Отлов нажатия кнопок на форме 
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (typeAction != TypeAction.none && buttonPause.Text == "Пауза")
            {
                if (keyData == Keys.Enter && typeAction == TypeAction.game)
                {
                    if (cells[currentCell.X, currentCell.Y].CellType != CellType.closed)
                    {
                        SmartInfoForm smartInfoForm = new SmartInfoForm(cells[currentCell.X, currentCell.Y]);
                        smartInfoForm.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Значение в данной клетке изменять нельзя", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return true;
                }
                else if (keyData == Keys.Right)
                {
                    if (currentCell.Y + 1 < 9)
                    {
                        currentCell.Y++;
                    }
                    else
                    {
                        currentCell.Y = 0;
                    }
                    cells[currentCell.X, currentCell.Y].SelectCell();
                    return true;
                }
                else if (keyData == Keys.Left)
                {
                    if (currentCell.Y - 1 > -1)
                    {
                        currentCell.Y--;
                    }
                    else
                    {
                        currentCell.Y = 8;
                    }
                    cells[currentCell.X, currentCell.Y].SelectCell();
                    return true;
                }
                else if (keyData == Keys.Down)
                {
                    if (currentCell.X + 1 < 9)
                    {
                        currentCell.X++;
                    }
                    else
                    {
                        currentCell.X = 0;
                    }
                    cells[currentCell.X, currentCell.Y].SelectCell();
                    return true;
                }
                else if (keyData == Keys.Up)
                {
                    if (currentCell.X - 1 > -1)
                    {
                        currentCell.X--;
                    }
                    else
                    {
                        currentCell.X = 8;
                    }
                    cells[currentCell.X, currentCell.Y].SelectCell();
                    return true;
                }
                else if (keyData == Keys.Space || keyData == Keys.D0 || keyData == Keys.NumPad0 ||
                    keyData == Keys.Delete || keyData == Keys.Back)
                {
                    SetNewValue(0);
                    return true;
                }
                else if (keyData == Keys.D1 || keyData == Keys.D2
                     || keyData == Keys.D3 || keyData == Keys.D4
                     || keyData == Keys.D5 || keyData == Keys.D6
                     || keyData == Keys.D7 || keyData == Keys.D8
                     || keyData == Keys.D9)
                {
                    int val = Convert.ToInt32(keyData.ToString("D")) - 48;
                    SetNewValue(val);
                    return true;
                }
                else if (keyData == Keys.NumPad1 || keyData == Keys.NumPad2
                     || keyData == Keys.NumPad3 || keyData == Keys.NumPad4
                     || keyData == Keys.NumPad5 || keyData == Keys.NumPad6
                     || keyData == Keys.NumPad7 || keyData == Keys.NumPad8
                     || keyData == Keys.NumPad9)
                {
                    int val = Convert.ToInt32(keyData.ToString("D")) - 96;
                    SetNewValue(val);
                    return true;
                }
            }
            return base.ProcessDialogKey(keyData);
        }


        /// <summary>
        /// Сброс фокуса на специальную ничего не делающую кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Drop_Focus(object sender, EventArgs e)
        {
            buttonFocus.Focus();
        }


        /// <summary>
        /// Выбор файла с уровнем и начало игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (!SaveByExit())
                return;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pathToLevel = openFileDialog1.FileName;
                labelLevel.Text = Path.GetFileNameWithoutExtension(pathToLevel);

                try
                {
                    LoadLevel();
                }
                catch (Exception ex)
                {
                    currentCell = new CurrentCell();
                    MessageBox.Show("В указанном файле обнаружена ошибка:\r\n\r\n" + ex.ToString(), "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }


        /// <summary>
        /// Получение координат ячейки, по которой кликнули
        /// </summary>
        /// <param name="sender">Панель или метка</param>
        /// <param name="x">Номер его строки</param>
        /// <param name="y">Номер его столбца</param>
        private void GetOrdinate(object sender, out int x, out int y)
        {
            string name = "00";
            if (sender is Panel)
            {
                name = ((Panel)sender).Name.Replace("panel", "");
            }
            else if (sender is Label)
            {
                name = ((Label)sender).Name.Replace("label", "");
            }
            x = Convert.ToInt32(name.Substring(0, 1));
            y = Convert.ToInt32(name.Substring(1, 1));
        }


        /// <summary>
        /// Выделение ячейки кликом мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cell_MouseClick(object sender, MouseEventArgs e)
        {
            if (typeAction != TypeAction.none)
            {
                GetOrdinate(sender, out currentCell.X, out currentCell.Y);
                cells[currentCell.X, currentCell.Y].SelectCell();
            }
        }


        /// <summary>
        /// Выделение ячейки двойным кликом мыши с заходом в дополнительное окно
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void cell_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (typeAction != TypeAction.none && typeAction == TypeAction.game)
            {
                GetOrdinate(sender, out currentCell.X, out currentCell.Y);
                cells[currentCell.X, currentCell.Y].SelectCell();
                if (cells[currentCell.X, currentCell.Y].CellType != CellType.closed)
                {
                    SmartInfoForm smartInfoForm = new SmartInfoForm(cells[currentCell.X, currentCell.Y]);
                    smartInfoForm.ShowDialog();
                }
            }
        }


        /// <summary>
        /// Подсвечивание неправильных ячеек
        /// </summary>
        /// <param name="errorCells"></param>
        private void ShowErrorCells()
        {
            if (errorCells.Count == 0)
            {
                MenuActionCheck.Enabled = true;
                timerTime.Enabled = false;
                labelTime.Visible = labelTimeInfo.Visible = buttonPause.Visible = false;
                currentCell = new CurrentCell();
                ((Panel)this.Controls["upPanel"]).Visible = ((Panel)this.Controls["downPanel"]).Visible =
                ((Panel)this.Controls["leftPanel"]).Visible = ((Panel)this.Controls["rightPanel"]).Visible = false;
                labelInfo.Text = "Данный уровень пройден\r\n\r\nВремя прохождения уровня: " + DateTimeToString(gameTime) + ".";
                typeAction = TypeAction.none;
                DialogResult dres = MessageBox.Show("Расстановка выполнена верно!\r\nХотите сохранить решённое судоку?", "Поздравление", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dres == DialogResult.Yes)
                {
                    Save_Click(null, null);
                }
                return;
            }

            for (int i = 0; i < errorCells.Count; i++)
            {
                int[] ord = (int[])errorCells[i];
                cells[ord[0], ord[1]].SetCellType(Color.Red);
            }

            timer1.Enabled = true;
        }


        /// <summary>
        /// Отмена подсвечивания неправильных ячеек
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;

            for (int i = 0; i < errorCells.Count; i++)
            {
                int[] ord = (int[])errorCells[i];
                Panel p = (Panel)this.Controls["panel" + ord[0] + ord[1]];
                Label l = (Label)p.Controls["label" + ord[0] + ord[1]];
                if (l.Font.Bold)
                {
                    p.BackColor = l.BackColor = Color.LightGray;
                }
                else
                {
                    p.BackColor = l.BackColor = Color.White;
                }
            }

            MenuActionCheck.Enabled = true;
        }


        /// <summary>
        /// Проверка правильности расстановки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void check_Click(object sender, EventArgs e)
        {
            timer1.Interval = sleepTime;

            // Получаем массив с содержимым ячеек
            int[,] board = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (cells[i, j].CellText == 0)
                    {
                        if (sender != null)
                        {
                            MessageBox.Show("Ещё не все поля заполнены", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return;
                    }
                    board[i, j] = cells[i, j].CellText;
                }
            }

            MenuActionCheck.Enabled = false;

            errorCells = new ArrayList();

            //
            // Проверяем по строкам
            //
            for (int i = 0; i < 9; i++)
            {
                // Считаем сколько раз каждая цифра встречается в строке
                int[] temp = new int[10];
                for (int j = 0; j < 9; j++)
                {
                    temp[board[i, j]]++;
                }

                // Находим координаты всех цифр, которые встречаются больше одного раза
                for (int l = 1; l < 10; l++)
                {
                    if (temp[l] > 1)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (board[i, j] == l)
                            {
                                errorCells.Add(new int[2] { i, j });
                            }
                        }
                    }
                }
            }

            //
            // Проверяем по столбцам
            //
            for (int j = 0; j < 9; j++)
            {
                // Считаем сколько раз каждая цифра встречается в столбце
                int[] temp = new int[10];
                for (int i = 0; i < 9; i++)
                {
                    temp[board[i, j]]++;
                }

                // Находим координаты всех цифр, которые встречаются больше одного раза
                for (int l = 1; l < 10; l++)
                {
                    if (temp[l] > 1)
                    {
                        for (int i = 0; i < 9; i++)
                        {
                            if (board[i, j] == l)
                            {
                                errorCells.Add(new int[2] { i, j });
                            }
                        }
                    }
                }
            }


            //
            // Проверяем по квадратам
            //
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int[] temp = new int[10];
                    for (int i = row * 3; i < row * 3 + 3; i++)
                    {
                        // Считаем сколько раз каждая цифра встречается в квадрате
                        for (int j = col * 3; j < col * 3 + 3; j++)
                        {
                            temp[board[i, j]]++;
                        }
                    }

                    // Находим координаты всех цифр, которые встречаются больше одного раза
                    for (int l = 1; l < 10; l++)
                    {
                        if (temp[l] > 1)
                        {
                            for (int i = row * 3; i < row * 3 + 3; i++)
                            {
                                for (int j = col * 3; j < col * 3 + 3; j++)
                                {
                                    if (board[i, j] == l)
                                    {
                                        errorCells.Add(new int[2] { i, j });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            ShowErrorCells();
        }


        /// <summary>
        /// Сохранение текущей позиции
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, EventArgs e)
        {
            if (sender == null)
            {
                saveFileDialog1.InitialDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Answer");
                saveFileDialog1.FileName = Path.GetFileNameWithoutExtension(pathToLevel);
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if (typeAction == TypeAction.none)
                    {
                        SaveLevel(saveFileDialog1.FileName, 1);
                    }
                    else
                    {
                        SaveLevel(saveFileDialog1.FileName, 0);
                    }
                }
            }
            else
            {
                if (typeAction == TypeAction.creating)
                {
                    saveFileDialog1.InitialDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Position");
                }
                else
                {
                    saveFileDialog1.InitialDirectory = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "SavedGames");
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    SaveLevel(saveFileDialog1.FileName, 0);
                }
            }
        }


        /// <summary>
        /// Сохранение позиции при выходе
        /// </summary>
        /// <returns></returns>
        private bool SaveByExit()
        {
            if (typeAction != TypeAction.none && !isPositionSave)
            {
                DialogResult answer = MessageBox.Show("Текущая игра будет закрыта. Сохранить изменения?", "Вопрос", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    Save_Click(null, null);
                    return true;
                }
                else if (answer == DialogResult.Cancel)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// Выход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Установка времени подсветки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuActionSettings_Click(object sender, EventArgs e)
        {
            SettingsForm setForm = new SettingsForm(this);
            setForm.ShowDialog();
        }


        /// <summary>
        /// Изменение времени, затраченного на прохождение уровня
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerTime_Tick(object sender, EventArgs e)
        {
            gameTime = gameTime.AddMilliseconds(timerTime.Interval);
            if (DateTimeToString(gameTime) != labelTime.Text)
            {
                labelTime.Text = DateTimeToString(gameTime);
            }
        }


        /// <summary>
        /// Приостановка игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (buttonPause.Text == "Пауза")
            {
                buttonPause.Text = "Старт";
                panelHide.BringToFront();
                timerTime.Enabled = false;
            }
            else
            {
                buttonPause.Text = "Пауза";
                panelHide.SendToBack();
                timerTime.Enabled = true;
            }
        }


        /// <summary>
        /// Очистить поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new Cell(this, i, j);
                }
            }
        }

        /// <summary>
        /// Выход при закрытии формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!SaveByExit())
                e.Cancel = true;
        }


        /// <summary>
        /// Обработка нажатия на кнопку для начала или окончания создания нового судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (typeAction == TypeAction.game)
            {
                if (!SaveByExit())
                    return;

                HideAllElements();
                typeAction = TypeAction.none;
            }

            if (typeAction == TypeAction.none)
            {
                typeAction = TypeAction.creating;
                ShowCreateElements();
                currentCell.X = currentCell.Y = 0;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (cells[i, j].CellType != CellType.closed)
                        {
                            cells[i, j] = new Cell(this, i, j);
                        }
                    }
                }

                cells[currentCell.X, currentCell.Y].SelectCell();
                isPositionSave = true;
            }
            else
            {
                if (!SaveByExit())
                    return;

                HideAllElements();
                typeAction = TypeAction.none;
                buttonCreate.FlatStyle = FlatStyle.Standard;
            }
        }


        /// <summary>
        /// Обработка нажатия на кнопку для начала или окончания решения судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSolution_Click(object sender, EventArgs e)
        {
            if (typeAction == TypeAction.creating)
            {
                if (!SaveByExit())
                    return;

                HideAllElements();
                typeAction = TypeAction.none;
            }

            if (typeAction == TypeAction.none)
            {
                ShowSolutionElements();

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        cells[i, j] = new Cell(this, i, j);
                    }
                }
            }
            else
            {
                if (!SaveByExit())
                    return;

                HideAllElements();
                typeAction = TypeAction.none;
                buttonSolution.FlatStyle = FlatStyle.Standard;
            }
        }


        #region Подсказки
        private void labelLevel_MouseEnter(object sender, EventArgs e)
        {
            if (labelLevel.Text != "")
                toolTip1.Show(pathToLevel, labelLevel, -10, -15);
        }
        private void labelLevel_MouseLeave(object sender, EventArgs e)
        {
            if (labelLevel.Text != "")
                toolTip1.Hide(labelLevel);
        }
        #endregion

        /// <summary>
        /// Попытка решить судоку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuActionSolveSudoku_Click(object sender, EventArgs e)
        {
            int[,] board = new int[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    board[i, j] = cells[i, j].CellText;
                }
            }
            SolveClass solveClass = new SolveClass(board, this);
            board = solveClass.SolveSudoku();

            if (board != null)
            {
                ArrayList[,] passibleDigits = solveClass.PossibleDigits;
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (cells[i, j].CellType != CellType.closed)
                        {
                            if (board[i, j] != 0)
                            {
                                cells[i, j].CellText = board[i, j];
                            }
                            else
                            {
                                for (int q = 0; q < 3; q++)
                                {
                                    for (int r = 0; r < 2; r++)
                                    {
                                        if (passibleDigits[i, j].Count > q * 2 + r)
                                        {
                                            cells[i, j].SetSmartLabel(q, r, (int)passibleDigits[i, j][q * 2 + r]);
                                        }
                                        else
                                        {
                                            r = 2;
                                            q = 3;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                historyClass = new HistoryClass();
            }
        }


        /// <summary>
        /// О программе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuHelpAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }


        /// <summary>
        /// Помощь
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuHelpHelp_Click(object sender, EventArgs e)
        {
            if (typeAction == TypeAction.none)
            {
                Help.ShowHelp(new Control(), helpPath, HelpNavigator.Topic, "index.html");
            }
            else if (typeAction == TypeAction.creating)
            {
                Help.ShowHelp(new Control(), helpPath, HelpNavigator.Topic, "create.html");
            }
            else
            {
                Help.ShowHelp(new Control(), helpPath, HelpNavigator.Topic, "solve.html");
            }
        }


        /// <summary>
        /// Отменить ход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuActionUndo_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled || typeAction != TypeAction.game)
            {
                return;
            }
            try
            {
                Cell cell = historyClass.Undo();
                cells[cell.X, cell.Y] = new Cell(cell);
                cells[cell.X, cell.Y].SelectCell();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Вернуть ход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuActionRedo_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled || typeAction != TypeAction.game)
            {
                return;
            }
            try
            {
                Cell cell = historyClass.Redo();
                cells[cell.X, cell.Y] = new Cell(cell);
                cells[cell.X, cell.Y].SelectCell();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        /// <summary>
        /// Открытие формы для задания параметров печати
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuFilePrint_Click(object sender, EventArgs e)
        {
            PrintForm printForm = new PrintForm(this);
            printForm.Show();
        }
    }
}
