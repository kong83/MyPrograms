using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting;

using FromServerClass;
using FromClientClass;
using System.Runtime.Remoting.Messaging;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace Checkers
{
    #region Enum's
    /// <summary>
    /// Тип клиента
    /// </summary>
    public enum TypePlayer
    {
        server,
        client,
        local
    }

    /// <summary>
    /// Тип локальной игры
    /// </summary>
    public enum OrderGame
    {
        UserUser,
        UserComp,
        CompUser,
        CompComp
    }

    /// <summary>
    /// Объекты нашей доски
    /// </summary>
    public enum ObjectCheck
    {
        empty,
        full,
        check_black,
        check_black_dam,
        check_white,
        check_white_dam
    }

    /// <summary>
    /// Типы шашки (пустая, обычная и дамка)
    /// </summary>
    public enum TypeCheck
    {
        check,
        king,
        empty
    }

    /// <summary>
    /// Цвет шашки на поле(черная, белая и нет шашки)
    /// </summary>
    public enum ColorCheck
    {
        black,
        white,
        unknown,
        disable
    }
    #endregion

    public partial class MainForm : Form
    {
        public int nSize = 50;										// Размер клетки
        public int leftX, leftY;									// Левый верхний угол, откуда начинается поле
        public bool changeSide = false;						// Повёрнута ли доска
        public TypePlayer typePlayer = TypePlayer.local;		// Тип клиента
        public OrderGame typeOrder = OrderGame.UserUser;    // Тип игры
        public ColorCheck whoFirst = ColorCheck.white;			// Кто ходит первым
        public bool serverReady = false,									// Готовность сервера играть
                                clientReady = false;									// Готовность клиента играть
        public bool placing = false;							// true - если происходит расстановка шашек по полю

        public LogClass logClass;                 // Класс для ведения лога
        public ComputerClass cmpClass;            // Класс с компьютерной логикой
        public ArrayList historyMas;							// Массив с историей ходов
        public ActionClass action;								// Общий класс для соверешния действий над шашками
        public bool isEnd;												// Указывает на то, идёт ли игра, или уже остановлена
        public ObjectCheck[,] masSave = new ObjectCheck[8, 8];		// Сохранённая начальная расстановка, с которой началась данная партия
        public int timeWhite,											// Время для белых в миллисекундах
                timeBlack;														// Время для черных в миллисекундах		
        int addSec,																// Количество добавляемых миллисекунд на ход	
                cntHist;															// Номер строки с HistoryList, куда писать новую запись
        RegistryClass regClass;                   // Класс для сохранения параметров

        public MainForm()
        {
            InitializeComponent();
        }

        Thread cmpAgent;

        // Действия при начале игры
        private bool SetStart()
        {
            try
            {
                timeWhite = timeBlack = Convert.ToInt32(comboBoxAllTime.Text) * 600;
            }
            catch
            {
                MessageBox.Show("Ошибка в записи количества минут");
                return false;
            }

            try
            {
                addSec = Convert.ToInt32(comboBoxAddSec.Text) * 10;
            }
            catch
            {
                MessageBox.Show("Ошибка в записи количества секунд");
                return false;
            }
            logClass.WriteLine("SetStart");
            UserCompMenu.Enabled = UserUserMenu.Enabled = CompUserMenu.Enabled = CompCompMenu.Enabled = false;
            comboBoxAddSec.Enabled = comboBoxAllTime.Enabled = false;
            cntHist = 0;
            HistoryList.Rows.Clear();
            historyMas = new ArrayList();
            HistoryList.Enabled = false;
            buttonFirst.Enabled = buttonPrevision.Enabled =
            buttonNext.Enabled = buttonEnd.Enabled = false;

            action.whoMove = whoFirst;
            SaveBeginBoard();
            startGameButton.Visible = StartGameMenu.Enabled = false;
            deliverButton.Visible = DeliverMenu.Enabled =
            remiButton.Visible = RemiMenu.Enabled = true;

            if (whoFirst == ColorCheck.white)
            {
                pictureBoxBlack.Visible = false;
                HistoryList.Columns[0].HeaderText = "C";
                HistoryList.Columns[1].HeaderText = "К";
            }
            else
            {
                pictureBoxWhite.Visible = false;
                HistoryList.Columns[0].HeaderText = "К";
                HistoryList.Columns[1].HeaderText = "C";
            }

            if (typePlayer == TypePlayer.local)
            {
                tabControl1.SelectedIndex = 0;
                serverReady = clientReady = true;
                if (typeOrder != OrderGame.UserUser)
                {

                    cmpClass = new ComputerClass(masSave, whoFirst, logClass);
                    cmpAgent = new Thread(new ThreadStart(CmpMonitorAgent));
                    cmpAgent.Priority = ThreadPriority.Lowest;
                    cmpAgent.Start();
                    timerCmpStep.Enabled = true;
                }
            }
            else
            {
                UserUserMenu.Checked = true;
                tabControl1.SelectedIndex = 1;
                ArrayList boardMas = new ArrayList();
                boardMas.Add(masSave);
                boardMas.Add(whoFirst);
                if (typePlayer == TypePlayer.client)
                {
                    clientReady = true;
                    fromClient.ToServerReady(comboBoxAllTime.Text, comboBoxAddSec.Text, boardMas);
                }
                else
                {
                    serverReady = true;
                    fromServer.ToClientReady(comboBoxAllTime.Text, comboBoxAddSec.Text, boardMas);
                }
                if (serverReady == true && clientReady == true)
                {
                    if ((typePlayer == TypePlayer.server && whoFirst == ColorCheck.white) ||
                         (typePlayer == TypePlayer.client && whoFirst == ColorCheck.black))
                        textBoxNetInfo.Text += "Игра началась\r\nВаш ход";
                    else
                        textBoxNetInfo.Text += "Игра началась\r\nОжидание хода противника";
                }
            }
            isEnd = false;
            return true;
        }

        /// <summary>
        /// Мониторинг за текущей оценкой и количеством обработанных позиций
        /// </summary>
        private void CmpMonitorAgent()
        {

        repeat:
            Thread.Sleep(500);
            if (isEnd)
                return;
            try
            {
                IAsyncResult iar1 = this.BeginInvoke(new ViewEvaluetionDelegate(ViewEvaluetion), new object[] { cmpClass.cmpInfo.currentMark, cmpClass.cmpInfo.currentRealMark, cmpClass.cmpInfo.currentCountPosition, cmpClass.cmpInfo.currentMaxDepth, cmpClass.cmpInfo.currentDepth });
                this.EndInvoke(iar1);
            }
            catch
            {
            }
            goto repeat;
        }

        private delegate void ViewEvaluetionDelegate(string mark, string realMark, int cnt, int maxDepth, int curDepth);
        /// <summary>
        /// Изменение оценки для позиции
        /// </summary>
        /// <param name="eval"></param>
        private void ViewEvaluetion(string mark, string realMark, int cnt, int maxDepth, int curDepth)
        {

            labelMark.Text = "Оценка: " + mark + ". Прогноз: " + realMark + "\r\nОбработано: " + cnt.ToString() + " Глубина: " + maxDepth.ToString() + " / " + curDepth.ToString();
            Application.DoEvents();
        }

        // Нажатие на кнопку начала игры
        private void button1_Click(object sender, EventArgs e)
        {
            if (typePlayer == TypePlayer.local ||
                 (typePlayer == TypePlayer.client && !serverReady) ||
                 (typePlayer == TypePlayer.server && !clientReady))
                beginButton_Click(null, null);

            if (SetStart())
            {
                action.ReDestination(-1, -1);
                timer1.Enabled = true;
            }
        }

        // Действия при завершении игры
        public void SetEnd(ColorCheck whoLoose)
        {
            logClass.WriteLine("SetEnd");
            timer1.Enabled = false;
            timerCmpStep.Enabled = false;
            isEnd = true;
            UserCompMenu.Enabled = UserUserMenu.Enabled = CompUserMenu.Enabled = CompCompMenu.Enabled = true;
            if (cmpClass != null)
            {

                try
                {
                    cmpAgent.Abort();
                    cmpAgent.Join();
                }
                catch
                {
                }
                cmpClass.StopGame();
                cmpClass = null;
            }
            action.SetEnd();
            comboBoxAddSec.Enabled = comboBoxAllTime.Enabled = true;

            startGameButton.Visible = StartGameMenu.Enabled = true;
            deliverButton.Visible = DeliverMenu.Enabled =
            remiButton.Visible = RemiMenu.Enabled = false;
            pictureBoxWhite.Visible =
            pictureBoxBlack.Visible = true;
            tabControl1.SelectedIndex = 0;
            HistoryList.Enabled = true;
            buttonFirst.Enabled = buttonPrevision.Enabled =
            buttonNext.Enabled = buttonEnd.Enabled = true;
            textBoxNetInfo.Text = "";
            if (whoFirst == ColorCheck.white)
                whoFirst = ColorCheck.black;
            else
                whoFirst = ColorCheck.white;
            clientReady = serverReady = false;
            backStepButton.Enabled = BackStepMenu.Enabled = false;
            if (whoLoose != ColorCheck.disable)
            {
                if (whoLoose == ColorCheck.black)
                {
                    MessageBox.Show("Синии выиграли");
                    labelWhite.Text = Convert.ToString(Convert.ToInt32(labelWhite.Text) + 1);
                }
                else if (whoLoose == ColorCheck.white)
                {
                    MessageBox.Show("Красные выиграли");
                    labelBlack.Text = Convert.ToString(Convert.ToInt32(labelBlack.Text) + 1);
                }
                else
                {
                    MessageBox.Show("Партия завершилась вничью");
                }
            }
        }

        // Отображение времени (с точность до сек)			
        private void ShowTime(TextBox textBoxTime, int time)
        {
            string min = Convert.ToString((int)(time / 10 / 60));
            string sec = Convert.ToString((int)(time / 10 % 60));
            if (sec.Length == 1)
                sec = "0" + sec;
            if (min + ":" + sec != textBoxTime.Text)
                textBoxTime.Text = min + ":" + sec;
        }

        // Изменение времени
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!serverReady || !clientReady)
                return;

            if (action.whoMove == ColorCheck.black)
                timeBlack--;
            else
                timeWhite--;

            if (timeBlack < 0)
                SetEnd(ColorCheck.black);
            if (timeWhite < 0)
                SetEnd(ColorCheck.white);

            ShowTime(boxTimeWhite, timeWhite);
            ShowTime(boxTimeBlack, timeBlack);
            Application.DoEvents();
        }

        #region Перевод программных координат в красивые человеческие
        private string XToDigit(int x)
        {
            switch (x)
            {
                case 0: return "8";
                case 1: return "7";
                case 2: return "6";
                case 3: return "5";
                case 4: return "4";
                case 5: return "3";
                case 6: return "2";
                case 7: return "1";
                default: return "0";
            }
        }
        private string YToAlpha(int y)
        {
            switch (y)
            {
                case 0: return "A";
                case 1: return "B";
                case 2: return "C";
                case 3: return "D";
                case 4: return "E";
                case 5: return "F";
                case 6: return "G";
                case 7: return "H";
                default: return "X";
            }
        }
        private string ObjectCheckToDigit(ObjectCheck check)
        {
            switch (check)
            {
                case ObjectCheck.check_black: return "1";
                case ObjectCheck.check_black_dam: return "2";
                case ObjectCheck.check_white: return "3";
                case ObjectCheck.check_white_dam: return "4";
                default: return "0";
            }
        }
        #endregion

        /// <summary>
        /// Добавление указанного количества секунд к времени только что сходившего,
        /// добавление очередного хода к истории и смена картинки	рядом с часами
        /// </summary>
        /// <param name="whoStep">Кто сходил</param>
        /// <param name="x1">X-координата клетки, откуда ходили</param>
        /// <param name="y1">Y-координата клетки, откуда ходили</param>
        /// <param name="x2">X-координата клетки, куда сходили</param>
        /// <param name="y2">Y-координата клетки, куда сходили</param>
        /// <param name="moveCheck">Какая шашка ходила (начинала ходить)</param>
        /// <param name="x3">X-координата клетки, которую съели (если её не было - то "-1")</param>
        /// <param name="y3">Y-координата клетки, которую съели(если её не было - то "-1")</param>
        /// <param name="delCheck">Какая шашка там стояла ((если её не было - то "empty"))</param>
        /// <param name="forbidSelect">Будет ли продолжено поедание </param>
        public void AfterStep(ColorCheck whoStep, int x1, int y1, int x2, int y2, ObjectCheck moveCheck, int x3, int y3, ObjectCheck delCheck, bool forbidSelect)
        {
            logClass.WriteLine("AfterStep whoStep=" + whoStep.ToString() + " x1=" + x1.ToString() + " y1=" + y1.ToString() +
              " x2=" + x2.ToString() + " y2=" + y2.ToString() + " moveCheck=" + moveCheck.ToString() + " x3=" + x2.ToString() +
              " y3=" + y3.ToString() + " delCheck=" + delCheck.ToString() + " forbidSelect=" + forbidSelect.ToString());
            string[] str = new string[6];			// Массив под ход белых, время белых, чего съели, 
            // ход черных, время чёрных и чего съели
            if (BackStepMenu.Enabled == false)
                backStepButton.Enabled = BackStepMenu.Enabled = true;

            if (!forbidSelect)
                if (whoStep == ColorCheck.white)
                    timeWhite += addSec;
                else
                    timeBlack += addSec;

            if (whoStep == whoFirst)
            {
                if (HistoryList.Rows.Count - 1 == cntHist)			// Если добавлять строку не надо - то добавляем в ячейку
                {
                    str = (string[])historyMas[cntHist];
                    str[0] = str[0] + ":" + x2.ToString() + "," + y2.ToString();
                    str[1] = timeWhite.ToString();
                    if (x3 != -1)
                        str[2] = str[2] + ":" + x3.ToString() + "," + y3.ToString() + "=" + ObjectCheckToDigit(delCheck);
                    historyMas[cntHist] = str;

                    HistoryList.Rows[cntHist].Cells[0].Value = HistoryList.Rows[cntHist].Cells[0].Value.ToString() + ": " + YToAlpha(y2) + XToDigit(x2);
                }
                else
                {
                    str[0] = ObjectCheckToDigit(moveCheck) + "=" + x1.ToString() + "," + y1.ToString() + ":" + x2.ToString() + "," + y2.ToString();
                    str[1] = timeWhite.ToString();
                    if (x3 != -1)
                        str[2] = x3.ToString() + "," + y3.ToString() + "=" + ObjectCheckToDigit(delCheck);
                    else
                        str[2] = "";
                    str[3] = str[4] = str[5] = "";
                    historyMas.Add(str);

                    string[] param = new string[2];
                    string raz = " - ";
                    if (x3 != -1)
                        raz = " : ";
                    param[0] = YToAlpha(y1) + XToDigit(x1) + raz + YToAlpha(y2) + XToDigit(x2);
                    param[1] = "";
                    HistoryList.Rows.Add(param);
                }
                HistoryList.CurrentCell = HistoryList.Rows[cntHist].Cells[0];
            }
            else
            {
                if (HistoryList.Rows[cntHist].Cells[1].Value.ToString() != "")			// Если добавлять строку не надо - то добавляем в ячейку
                {
                    str = (string[])historyMas[cntHist];
                    str[3] = str[3] + ":" + x2.ToString() + "," + y2.ToString();
                    str[4] = timeBlack.ToString();
                    if (x3 != -1)
                        str[5] = str[5] + ":" + x3.ToString() + "," + y3.ToString() + "=" + ObjectCheckToDigit(delCheck);

                    historyMas[cntHist] = str;

                    HistoryList.Rows[cntHist].Cells[1].Value = HistoryList.Rows[cntHist].Cells[1].Value.ToString() + " : " + YToAlpha(y2) + XToDigit(x2);
                }
                else
                {
                    str = (string[])historyMas[cntHist];
                    str[3] = ObjectCheckToDigit(moveCheck) + "=" + x1.ToString() + "," + y1.ToString() + ":" + x2.ToString() + "," + y2.ToString();
                    str[4] = timeBlack.ToString();
                    if (x3 != -1)
                        str[5] = x3.ToString() + "," + y3.ToString() + "=" + ObjectCheckToDigit(delCheck);
                    else
                        str[5] = "";
                    historyMas[cntHist] = str;

                    string raz = " - ";
                    if (x3 != -1)
                        raz = " : ";
                    HistoryList.Rows[cntHist].Cells[1].Value = YToAlpha(y1) + XToDigit(x1) + raz + YToAlpha(y2) + XToDigit(x2);
                }
                HistoryList.CurrentCell = HistoryList.Rows[cntHist].Cells[1];
                if (!forbidSelect)
                    cntHist++;
            }

            Application.DoEvents();
            if (!forbidSelect)
            {
                string step;
                if (str[3] != "")
                    step = str[3].Substring(2).Replace(",", "");
                else
                    step = str[0].Substring(2).Replace(",", "");

                // Отправка совершённого хода компьютеру
                if (typePlayer == TypePlayer.local && typeOrder != OrderGame.UserUser)
                {

                    logClass.WriteLine("Отправка совершённого хода компьютеру");
                    cmpClass.SetStep(step);
                    /*action.freezeGame = true;
                    SetStepDelegate setStep = new SetStepDelegate(cmpClass.SetStep);
                    AsyncCallback callBack = new AsyncCallback(SetStep_CallBack);
                    setStep.BeginInvoke(step, callBack, null);     */
                }
                timerCmpStep.Enabled = true;
            }
        }

        /* private delegate void SetStepDelegate(string step);
         /// <summary>
         /// Обработка события, после отправки совершённого хода компьютеру и получения оценки новой позиции
         /// </summary>
         /// <param name="iar"></param>
         private void SetStep_CallBack(IAsyncResult iar)
         {
           try {
      
             SetStepDelegate setStep = (SetStepDelegate)((AsyncResult)iar).AsyncDelegate;
             setStep.EndInvoke(iar);
           } catch {
           }

           //IAsyncResult iar1 = this.BeginInvoke(new ViewEvaluetionDelegate(ViewEvaluetion), new object[] { eval });
           //this.EndInvoke(iar1);
           //timerCmpStep.Enabled = true;
           action.freezeGame = false;
           logClass.WriteLine("SetStep_CallBack complete whoMove=" + action.whoMove.ToString() +
             " action.freezeGame=" + action.freezeGame.ToString());
 
           //iar1 = this.BeginInvoke(new SetTimerCmpStepDelegate(SetTimerCmpStep), new object[] { true });
           //this.EndInvoke(iar1);
         }  */

        // Действия при загрузке программы
        private void MainForm_Load(object sender, EventArgs e)
        {
            logClass = new LogClass();
            leftX = panel1.Location.X;
            leftY = panel1.Location.Y;
            isEnd = true;
            action = new ActionClass(this);
            stopChecked = true;
            regClass = new RegistryClass(this);
            regClass.LoadParameter();
            if (typeOrder == OrderGame.UserUser)
            {
                UserUserMenu.Checked = true;
                UserCompMenu.Checked = CompUserMenu.Checked = CompCompMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallBlack;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallWhite;
            }
            else if (typeOrder == OrderGame.CompUser)
            {
                CompUserMenu.Checked = true;
                UserCompMenu.Checked = UserUserMenu.Checked = CompCompMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallBlack;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallComp;
            }
            else if (typeOrder == OrderGame.UserComp)
            {
                UserCompMenu.Checked = true;
                UserUserMenu.Checked = CompUserMenu.Checked = CompCompMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallComp;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallWhite;
            }
            else
            {
                CompCompMenu.Checked = true;
                UserCompMenu.Checked = CompUserMenu.Checked = UserUserMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallComp;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallComp;
            }
            stopChecked = false;
        }

        // Смена местами двух label-ов
        private void ChangeLabel(Label lab1, Label lab2)
        {
            Point p = lab1.Location;
            lab1.Location = lab2.Location;
            lab2.Location = p;
        }

        // Поворот доски на 180 градусов
        private void button4_Click(object sender, EventArgs e)
        {
            if (changeSide)
                changeSide = false;
            else
                changeSide = true;

            ChangeLabel(label6, label13);
            ChangeLabel(label7, label12);
            ChangeLabel(label8, label11);
            ChangeLabel(label9, label10);

            ChangeLabel(label14, label21);
            ChangeLabel(label15, label20);
            ChangeLabel(label16, label19);
            ChangeLabel(label17, label18);
            action.ReWriteCells();
        }

        // Попытка снятия выделения с кнопок
        private void Drop_Focus(object sender, EventArgs e)
        {
            turnButton.Focus();
        }

        // Замена последней строки с поле с сетевой информацией на newStr
        public void UpdateLastLinesBoxNetInfo(string newStr)
        {
            string str = "";
            for (int i = 0; i < textBoxNetInfo.Lines.Length - 1; i++)
                str += textBoxNetInfo.Lines[i] + "\r\n";
            str += newStr;
            textBoxNetInfo.Text = str;
        }

        // Сдаться
        private void button5_Click(object sender, EventArgs e)
        {
            if (isEnd)
                return;
            if (MessageBox.Show("Вы уверены, что хотите сдаться?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (typePlayer == TypePlayer.local)
                    SetEnd(action.whoMove);
                if (typePlayer == TypePlayer.client)
                {
                    SetEnd(ColorCheck.black);
                    try
                    {
                        fromClient.ToServerDeliver();
                    }
                    catch { }
                }
                else if (typePlayer == TypePlayer.server)
                {
                    SetEnd(ColorCheck.white);
                    try
                    {
                        fromServer.ToClientDeliver();
                    }
                    catch { }
                }
            }
        }

        #region Проход по истории партии
        // Сохранение позиции, с которой начинается партия
        private void SaveBeginBoard()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 == 0)
                        masSave[i, j] = ObjectCheck.empty;
                    else
                    {
                        if (action.board[i, j].colorCheck == ColorCheck.black && action.board[i, j].typeCheck == TypeCheck.check)
                            masSave[i, j] = ObjectCheck.check_black;
                        else if (action.board[i, j].colorCheck == ColorCheck.black && action.board[i, j].typeCheck == TypeCheck.king)
                            masSave[i, j] = ObjectCheck.check_black_dam;
                        else if (action.board[i, j].colorCheck == ColorCheck.white && action.board[i, j].typeCheck == TypeCheck.check)
                            masSave[i, j] = ObjectCheck.check_white;
                        else if (action.board[i, j].colorCheck == ColorCheck.white && action.board[i, j].typeCheck == TypeCheck.king)
                            masSave[i, j] = ObjectCheck.check_white_dam;
                        else
                            masSave[i, j] = ObjectCheck.full;
                    }
        }

        // Прокрутка партии до указанного места в списке ходов
        private void ShowGame(int cellX, int cellY)
        {
            // Начальная расстановка
            ObjectCheck[,] mas = new ObjectCheck[8, 8];

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    mas[i, j] = masSave[i, j];

            string[] str, strSave;
            int X1, Y1, X2, Y2;		// Для работы с координатами на поле
            int x1, y1, x2, y2;	  // Для удаления шашки, которую съели
            // Проведение ходов
            for (int i = 0; i <= cellX; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    if (i == cellX && j == 1 && cellY == 0)
                        break;

                    strSave = (string[])historyMas[i];
                    str = new string[4] { strSave[0], strSave[1], strSave[3], strSave[4] };
                    if (j == 0)												// Выставление времени
                        ShowTime(boxTimeWhite, Convert.ToInt32(str[1]));
                    else
                        ShowTime(boxTimeBlack, Convert.ToInt32(str[3]));
                    X1 = Convert.ToInt32(str[j * 2].Substring(2, 1));
                    Y1 = Convert.ToInt32(str[j * 2].Substring(4, 1));
                    str[j * 2] = str[j * 2].Substring(6);
                    while (str[j * 2].Length > 0)
                    {
                        X2 = Convert.ToInt32(str[j * 2].Substring(0, 1));
                        Y2 = Convert.ToInt32(str[j * 2].Substring(2, 1));
                        str[j * 2] = str[j * 2].Substring(Math.Min(4, str[j * 2].Length));

                        mas[X2, Y2] = mas[X1, Y1];
                        mas[X1, Y1] = ObjectCheck.full;
                        if (X2 == 0 && mas[X2, Y2] == ObjectCheck.check_white)
                            mas[X2, Y2] = ObjectCheck.check_white_dam;
                        else if (X2 == 7 && mas[X2, Y2] == ObjectCheck.check_black)
                            mas[X2, Y2] = ObjectCheck.check_black_dam;

                        if (X2 > X1)	// Удаляем шашку, которую съели, если она есть
                        {
                            x1 = X1;
                            y1 = Y1;
                            x2 = X2;
                            y2 = Y2;
                        }
                        else
                        {
                            x2 = X1;
                            y2 = Y1;
                            x1 = X2;
                            y1 = Y2;
                        }
                        int w = y1;
                        for (int q = x1 + 1; q < x2; q++)
                        {
                            if (y1 > y2)
                                w--;
                            else
                                w++;
                            mas[q, w] = ObjectCheck.full;
                        }

                        X1 = X2;
                        Y1 = Y2;
                    }
                }
            }

            // Изменение поля
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    action.SetDataToBoard(mas[i, j], i, j);
        }

        // Отлов кликанья мышью по ячейкам для отображения хода партии
        private void HistoryList_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1 || HistoryList.Enabled == false || e.ColumnIndex == 2)
                return;

            if (HistoryList.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                ShowGame(e.RowIndex, e.ColumnIndex);
            else
            {
                HistoryList.CurrentCell = HistoryList.Rows[e.RowIndex].Cells[0];
                ShowGame(e.RowIndex, 0);
            }
        }

        // Вызов начала партии
        private void buttonFirst_Click(object sender, EventArgs e)
        {
            if (HistoryList.CurrentCell == null)
                return;

            HistoryList.CurrentCell = HistoryList.Rows[0].Cells[0];
            ShowGame(HistoryList.CurrentCell.RowIndex, HistoryList.CurrentCell.ColumnIndex);
        }

        private void buttonPrevision_Click(object sender, EventArgs e)
        {
            if (HistoryList.CurrentCell == null)
                return;

            int rowInd = HistoryList.CurrentCell.RowIndex,
                    colInd = HistoryList.CurrentCell.ColumnIndex;

            if (colInd == 1)
            {
                HistoryList.CurrentCell = HistoryList.Rows[rowInd].Cells[colInd - 1];
            }
            else
            {
                if (rowInd > 0)
                    HistoryList.CurrentCell = HistoryList.Rows[rowInd - 1].Cells[1];
            }

            ShowGame(HistoryList.CurrentCell.RowIndex, HistoryList.CurrentCell.ColumnIndex);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (HistoryList.CurrentCell == null)
                return;

            int rowInd = HistoryList.CurrentCell.RowIndex,
                    colInd = HistoryList.CurrentCell.ColumnIndex;

            if (colInd == 0)
            {
                if (HistoryList.Rows[rowInd].Cells[1].Value.ToString() != "")
                    HistoryList.CurrentCell = HistoryList.Rows[rowInd].Cells[colInd + 1];
            }
            else
            {
                if (rowInd < HistoryList.Rows.Count - 1)
                    HistoryList.CurrentCell = HistoryList.Rows[rowInd + 1].Cells[0];
            }
            ShowGame(HistoryList.CurrentCell.RowIndex, HistoryList.CurrentCell.ColumnIndex);
        }

        private void buttonEnd_Click(object sender, EventArgs e)
        {
            if (HistoryList.CurrentCell == null)
                return;

            int maxRow = HistoryList.Rows.Count - 1;

            if (HistoryList.Rows[maxRow].Cells[1].Value.ToString() == "")
                HistoryList.CurrentCell = HistoryList.Rows[maxRow].Cells[0];
            else
                HistoryList.CurrentCell = HistoryList.Rows[maxRow].Cells[1];

            ShowGame(HistoryList.CurrentCell.RowIndex, HistoryList.CurrentCell.ColumnIndex);
        }
        #endregion

        #region Отсылка предложения о ничьей и обработка ответа
        private delegate void SetRemiDelegate(bool rez);
        private delegate bool ToServerRemiDelegate();
        private delegate bool ToClientRemiDelegate();

        // Остановка игры c результатом "Ничья"
        private void SetRemi(bool rez)
        {
            if (rez)
                SetEnd(ColorCheck.unknown);
            else
                MessageBox.Show("Противник отказался от ничьей");
        }

        private void ToServerRemiQuery(IAsyncResult ar)
        {
            ToServerRemiDelegate d = (ToServerRemiDelegate)((AsyncResult)ar).AsyncDelegate;

            try
            {
                bool str = d.EndInvoke(ar);
                this.BeginInvoke(new SetRemiDelegate(SetRemi), new object[] { str });
            }
            catch (Exception ex)
            {
                ShowError("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message);
            }
        }

        private void ToClientRemiQuery(IAsyncResult ar)
        {
            ToClientRemiDelegate d = (ToClientRemiDelegate)((AsyncResult)ar).AsyncDelegate;

            try
            {
                bool str = d.EndInvoke(ar);
                this.BeginInvoke(new SetRemiDelegate(SetRemi), new object[] { str });
            }
            catch (Exception ex)
            {
                ShowError("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message);
            }
        }

        // Предложение ничьи
        private void button6_Click(object sender, EventArgs e)
        {
            if (typePlayer == TypePlayer.local)
            {
                if (MessageBox.Show("Вы уверены, что хотите завершить партию вничью?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    SetEnd(ColorCheck.unknown);
            }
            else if (typePlayer == TypePlayer.client)
            {
                AsyncCallback cb = new AsyncCallback(this.ToServerRemiQuery);
                ToServerRemiDelegate d = new ToServerRemiDelegate(fromClient.ToServerRemi);
                IAsyncResult ar = d.BeginInvoke(cb, null);
            }
            else
            {
                AsyncCallback cb = new AsyncCallback(this.ToClientRemiQuery);
                ToClientRemiDelegate d = new ToClientRemiDelegate(fromServer.ToClientRemi);
                IAsyncResult ar = d.BeginInvoke(cb, null);
            }
        }
        #endregion

        #region Проверка вводимых значений
        private void comboBoxAllTime_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(comboBoxAllTime.Text);
                errorProvider1.SetError(comboBoxAllTime, "");
            }
            catch
            {
                errorProvider1.SetError(comboBoxAllTime, "Недопустимое значение");
            }
        }
        private void comboBoxAddSec_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Convert.ToInt32(comboBoxAddSec.Text);
                errorProvider1.SetError(comboBoxAddSec, "");
            }
            catch
            {
                errorProvider1.SetError(comboBoxAddSec, "Недопустимое значение");
            }
        }

        // Вывод сообщения об ошибке
        private void ShowError(string msg)
        {
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #endregion

        #region Функции изменения формы для серверной и клиентской частей
        public FromClientTransmittor fromClient;
        public FromServerTransmittor fromServer;
        public string ipConnect;
        ObjRef refFromClient;
        ObjRef refFromServer;

        private delegate void SetEndRemiDelegate();
        private void SetEndRemi()
        {
            SetEnd(ColorCheck.unknown);
        }

        private delegate void SetEndBackStepDelegate(bool rez);
        private void SetEndBackStep(bool rez)
        {
            if (rez)
                BackStep();
            timer1.Enabled = true;
        }

        private delegate void SetEndReadyDelegate(ColorCheck who, string timeAll, string addSec, ArrayList boardMas);
        private void SetEndReady(ColorCheck who, string timeAll, string addSec, ArrayList boardMas)
        {
            if (who == ColorCheck.black)
                textBoxNetInfo.Text += "Клиент готов играть\r\n";
            else
                textBoxNetInfo.Text += "Сервер готов играть\r\n";
            if (serverReady == true && clientReady == true)
            {
                if ((typePlayer == TypePlayer.server && whoFirst == ColorCheck.white) ||
                     (typePlayer == TypePlayer.client && whoFirst == ColorCheck.black))
                    textBoxNetInfo.Text += "Игра началась\r\nВаш ход";
                else
                    textBoxNetInfo.Text += "Игра началась\r\nОжидание хода противника";
            }
            else
            {
                masSave = (ObjectCheck[,])boardMas[0];
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        action.SetDataToBoard(masSave[i, j], i, j);
                whoFirst = (ColorCheck)boardMas[1];

                comboBoxAllTime.Text = timeAll;
                comboBoxAddSec.Text = addSec;
                comboBoxAllTime.Enabled =
                comboBoxAddSec.Enabled = false;
                if (tabControl1.SelectedIndex == 2)
                    tabControl1.SelectedIndex = 1;
            }
        }

        private delegate void SetEndDisableDelegate(ColorCheck looser);
        private void SetEndDisable(ColorCheck looser)
        {
            MessageBox.Show("Противник разорвал связь");
            DisableConnect();
            SetEnd(looser);
        }

        private delegate void SetEndDeliverDelegate(ColorCheck looser);
        private void SetEndDeliver(ColorCheck looser)
        {
            SetEnd(looser);
        }

        private delegate void SetStartGameDelegate();
        private void SetStartGame()
        {
            whoFirst = ColorCheck.white;
            textBoxNetInfo.Text += "Клиент присоединился\r\n";
            startGameButton.Enabled = StartGameMenu.Enabled =
            startButton.Enabled = true;
        }
        #endregion

        #region Серверная часть
        // Установить сервер
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isEnd)
                {
                    if (MessageBox.Show("Текущая игра будет остановлена. Вы уверены?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        SetEnd(ColorCheck.disable);
                    else
                        return;
                }
                tabControl1.SelectedIndex = 1;
                textBoxNetInfo.Text = "";
                clientReady = serverReady = false;
                ChannelServices.RegisterChannel(new TcpChannel(8080), false);

                fromClient = new FromClientTransmittor();
                refFromClient = RemotingServices.Marshal(fromClient, "FromClientTransmittor");

                // Обработка события на запрос о ничьей
                fromClient.ToServerRemiEvent += new FromClientTransmittor.ToServerRemiEventHandler(ToServerRemiEvent);

                // Обработка события на сообщение о начале игры
                fromClient.StartGameEvent += new FromClientTransmittor.StartGameEventHandler(StartGameEvent);

                // Обработка нового хода клиента
                fromClient.ToServerStepEvent += new FromClientTransmittor.ToServerStepEventHandler(ToServerStepEvent);

                // Обработка события на сообщение о ничьей
                fromClient.ToServerDeliverEvent += new FromClientTransmittor.ToServerDeliverEventHandler(ToServerDeliverEvent);

                // Обработка события на готовность клиента играть
                fromClient.ToServerReadyEvent += new FromClientTransmittor.ToServerReadyEventHandler(ToServerReadyEvent);

                // Обработка события на отключение клиента 
                fromClient.ToServerDisableEvent += new FromClientTransmittor.ToServerDisableEventHandler(ToServerDisableEvent);

                // Обработка события на отключение клиента 
                fromClient.ToServerBackStepEvent += new FromClientTransmittor.ToServerBackStepEventHandler(ToServerBackStepEvent);

                typePlayer = TypePlayer.server;
                IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1];
                textBoxNetInfo.Text += "Сервер установлен\r\nIP-адрес: " + ip.ToString() + "\r\n";
                textBoxNetInfo.Text += "Ожидаем подсоединения клиента\r\n";

                serverButton.Visible = ServerMenu.Enabled =
                connectButton.Visible = ConnectToServerMenu.Enabled =
                startGameButton.Enabled = StartGameMenu.Enabled =
                startButton.Enabled = false;
                disconnectButton.Visible = DisconnectMenu.Enabled = true;
            }
            catch
            {
                ShowError("Сервер не установлен.");
                try
                {
                    ChannelServices.UnregisterChannel(ChannelServices.RegisteredChannels[0]);
                }
                catch { }

                try
                {
                    if (refFromClient != null)
                    {
                        RemotingServices.Unmarshal(refFromClient);
                        RemotingServices.Disconnect(fromClient);
                    }
                }
                catch { }

                try
                {
                    if (refFromServer != null)
                    {
                        RemotingServices.Unmarshal(refFromServer);
                        RemotingServices.Disconnect(fromServer);
                    }
                }
                catch { }
            }
        }

        // Обработка события на сообщение клиента о готовности играть
        private void ToServerReadyEvent(string timeAll, string addSec, ArrayList boardMas)
        {
            clientReady = true;
            this.BeginInvoke(new SetEndReadyDelegate(SetEndReady), new object[] { ColorCheck.black, timeAll, addSec, boardMas });
        }

        // Обработка события на отключение клиента
        private void ToServerDisableEvent()
        {
            ColorCheck looser = ColorCheck.disable;
            if (!isEnd)
                looser = ColorCheck.black;
            this.BeginInvoke(new SetEndDisableDelegate(SetEndDisable), new object[] { looser });
        }

        // Обработка события на запрос о ничьей
        private bool ToServerRemiEvent()
        {
            //timer1.Enabled = false;
            if (MessageBox.Show("Противник предлагает ничью. Вы согласны?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.BeginInvoke(new SetEndRemiDelegate(SetEndRemi));
                //timer1.Enabled = true;
                return true;
            }
            else
            {
                //timer1.Enabled = true;
                return false;
            }
        }

        // Обработка события на запрос об отмене хода
        private bool ToServerBackStepEvent()
        {
            timer1.Enabled = false;
            if (MessageBox.Show("Противник просит отменить последний ход. Вы согласны?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.BeginInvoke(new SetEndBackStepDelegate(SetEndBackStep), new object[] { true });
                return true;
            }
            else
            {
                this.BeginInvoke(new SetEndBackStepDelegate(SetEndBackStep), new object[] { false });
                return false;
            }
        }

        // Обработка события при сдаче клиента
        private void ToServerDeliverEvent()
        {
            this.BeginInvoke(new SetEndDeliverDelegate(SetEndDeliver), new object[] { ColorCheck.black });
        }

        // Обработка сообщения от клиента о начале игры
        private void StartGameEvent(string ipClient)
        {
            try
            {
                // Установка клиента к ipClient			
                fromServer = (FromServerTransmittor)Activator.GetObject(typeof(FromServerTransmittor), ipClient + "/FromServerTransmittor");
                this.BeginInvoke(new SetStartGameDelegate(SetStartGame));
            }
            catch
            {
                ShowError("Ошибка работы по сети.");
            }
        }

        // Действия после хода клиента
        private delegate void ServerStepDelegate(int[] selectedCell, int x, int y, int timeClient);
        private void ServerStep(int[] selectedCell, int x, int y, int timeClient)
        {
            // Действия:
            // 1. Изменение клеток на доске
            // 2. Изменение истории
            // 3. Смена хода, если он закончен (whoMove, в информации о сети)
            // 4. Проверка на проигрыш
            action.MoveCheck(selectedCell, x, y);
            UpdateLastLinesBoxNetInfo("Ваш ход");
            timeBlack = timeClient;
        }

        // Изменённые клетки после очередного хода клиента
        // В первых двух - ход шашки(дамки), в третьей (если она есть) - съеденная шашка
        private void ToServerStepEvent(int[] selectedCell, int x, int y, int timeClient)
        {
            this.BeginInvoke(new ServerStepDelegate(ServerStep), new object[] { selectedCell, x, y, timeClient });
        }
        #endregion

        #region Клиентская часть
        private delegate void StartGameDelegate(string ipConnect);
        private void StartGameQuery(IAsyncResult ar)
        {
            StartGameDelegate d = (StartGameDelegate)((AsyncResult)ar).AsyncDelegate;
            d.EndInvoke(ar);
        }

        // Подключиться к серверу
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isEnd)
                {
                    if (MessageBox.Show("Текущая игра будет остановлена. Вы уверены?", "Подтверждение", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        SetEnd(ColorCheck.disable);
                    else
                        return;
                }
                whoFirst = ColorCheck.white;
                clientReady = serverReady = false;
                ServerConnectForm serverConnectForm = new ServerConnectForm(this);
                serverConnectForm.ShowDialog();
                if (ipConnect != "")
                {
                    ChannelServices.RegisterChannel(new TcpChannel(8081), false);

                    fromClient = (FromClientTransmittor)Activator.GetObject(typeof(FromClientTransmittor), "tcp://" + ipConnect + ":8080/FromClientTransmittor");

                    // Установить свой сервер для получения ответа от сервера					
                    fromServer = new FromServerTransmittor();
                    refFromServer = RemotingServices.Marshal(fromServer, "FromServerTransmittor");

                    // Обработка события на запрос о ничьей
                    fromServer.ToClientRemiEvent += new FromServerTransmittor.ToClientRemiEventHandler(ToClientRemiEvent);

                    // Обработка события на сообщение о сдаче сервера
                    fromServer.ToClientDeliverEvent += new FromServerTransmittor.ToClientDeliverEventHandler(ToClientDeliverEvent);

                    // Обработка очередного хода сервера
                    fromServer.ToClientStepEvent += new FromServerTransmittor.ToClientStepEventHandler(ToClientStepEvent);

                    // Обработка события на готовность клиента играть
                    fromServer.ToClientReadyEvent += new FromServerTransmittor.ToClientReadyEventHandler(ToClientReadyEvent);

                    // Обработка события на отключение клиента 
                    fromServer.ToClientDisableEvent += new FromServerTransmittor.ToClientDisableEventHandler(ToClientDisableEvent);

                    // Просьба отменить последний ход
                    fromServer.ToClientBackStepEvent += new FromServerTransmittor.ToClientBackStepEventHandler(ToClientBackStepEvent);

                    // Получение сетевого адреса клиента (вида tcp://xxx.xxx.xxx.xxx:8081)
                    IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1];
                    string addr = "tcp://" + ip.ToString() + ":8081";

                    /*ChannelDataStore cds = (ChannelDataStore)refFromServer.ChannelInfo.ChannelData.GetValue(1);
                    string addr = cds.ChannelUris[0];*/

                    AsyncCallback cb = new AsyncCallback(this.StartGameQuery);
                    StartGameDelegate d = new StartGameDelegate(fromClient.StartGame);
                    IAsyncResult ar = d.BeginInvoke(addr, cb, null);

                    typePlayer = TypePlayer.client;
                    serverButton.Visible = ServerMenu.Enabled =
                    connectButton.Visible = ConnectToServerMenu.Enabled = false;
                    disconnectButton.Visible = DisconnectMenu.Enabled = true;
                    tabControl1.SelectedIndex = 1;
                    textBoxNetInfo.Text = "";
                    textBoxNetInfo.Text += "Соединение с сервером установлено\r\n";
                }
            }
            catch
            {
                ShowError("Подключение к серверу не установлено.");
                ChannelServices.UnregisterChannel(ChannelServices.RegisteredChannels[0]);
                try
                {
                    if (refFromClient != null)
                    {
                        RemotingServices.Unmarshal(refFromClient);
                        RemotingServices.Disconnect(fromClient);
                    }
                }
                catch { }

                try
                {
                    if (refFromServer != null)
                    {
                        RemotingServices.Unmarshal(refFromServer);
                        RemotingServices.Disconnect(fromServer);
                    }
                }
                catch { }
            }
        }

        // Обработка события на сообщение сервера о готовности играть
        private void ToClientReadyEvent(string timeAll, string addSec, ArrayList boardMas)
        {
            serverReady = true;
            this.BeginInvoke(new SetEndReadyDelegate(SetEndReady), new object[] { ColorCheck.white, timeAll, addSec, boardMas });
        }

        // Обработка события на отключение сервера
        private void ToClientDisableEvent()
        {
            ColorCheck looser = ColorCheck.disable;
            if (!isEnd)
                looser = ColorCheck.white;
            this.BeginInvoke(new SetEndDisableDelegate(SetEndDisable), new object[] { looser });
        }

        // Обработка события на запрос о ничьей
        private bool ToClientRemiEvent()
        {
            //timer1.Enabled = false;
            if (MessageBox.Show("Противник предлагает ничью. Вы согласны?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.BeginInvoke(new SetEndRemiDelegate(SetEndRemi));
                //timer1.Enabled = true;
                return true;
            }
            else
            {
                //timer1.Enabled = true;
                return false;
            }
        }

        // Обработка события на запрос об отмене хода
        private bool ToClientBackStepEvent()
        {
            timer1.Enabled = false;
            if (MessageBox.Show("Противник просит отменить последний ход. Вы согласны?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.BeginInvoke(new SetEndBackStepDelegate(SetEndBackStep), new object[] { true });
                return true;
            }
            else
            {
                this.BeginInvoke(new SetEndBackStepDelegate(SetEndBackStep), new object[] { false });
                return false;
            }
        }

        // Обработка события на сообщение о сдаче
        private void ToClientDeliverEvent()
        {
            this.BeginInvoke(new SetEndDeliverDelegate(SetEndDeliver), new object[] { ColorCheck.white });
        }

        // Действия после хода сервера
        private delegate void ClientStepDelegate(int[] selectedCell, int x, int y, int timeServer);
        private void ClientStep(int[] selectedCell, int x, int y, int timeServer)
        {
            action.MoveCheck(selectedCell, x, y);
            UpdateLastLinesBoxNetInfo("Ваш ход");
            timeWhite = timeServer;
        }

        // Изменённые клетки после очередного хода сервера
        // В первых двух - ход шашки(дамки), в третьей (если она есть) - съеденная шашка
        private void ToClientStepEvent(int[] selectedCell, int x, int y, int timeServer)
        {
            this.BeginInvoke(new ClientStepDelegate(ClientStep), new object[] { selectedCell, x, y, timeServer });
        }
        #endregion

        #region Разрыв соединения
        // Непосредственно разрыв
        private void DisableConnect()
        {
            ChannelServices.UnregisterChannel(ChannelServices.RegisteredChannels[0]);
            try
            {
                if (refFromClient != null)
                {
                    RemotingServices.Unmarshal(refFromClient);
                    RemotingServices.Disconnect(fromClient);
                }
            }
            catch { }

            try
            {
                if (refFromServer != null)
                {
                    RemotingServices.Unmarshal(refFromServer);
                    RemotingServices.Disconnect(fromServer);
                }
            }
            catch { }

            typePlayer = TypePlayer.local;
            serverButton.Visible = ServerMenu.Enabled =
            connectButton.Visible = ConnectToServerMenu.Enabled = true;
            disconnectButton.Visible = DisconnectMenu.Enabled = false;
            fromClient = null;
            fromServer = null;
            startGameButton.Enabled = StartGameMenu.Enabled =
            startButton.Enabled = true;
            whoFirst = ColorCheck.white;
            clientReady = serverReady = false;
            textBoxNetInfo.Text = "Соединение разорвано.";
        }

        // Разорвать соединение
        public void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите разорвать соединение?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (fromClient != null && fromServer != null)
                {
                    if (typePlayer == TypePlayer.local)
                        SetEnd(action.whoMove);
                    else if (typePlayer == TypePlayer.client)
                    {
                        if (!isEnd)
                            SetEnd(ColorCheck.black);
                        else
                            SetEnd(ColorCheck.disable);
                        try
                        {
                            fromClient.ToServerDisable();
                        }
                        catch { }
                    }
                    else
                    {
                        if (!isEnd)
                            SetEnd(ColorCheck.white);
                        else
                            SetEnd(ColorCheck.disable);
                        try
                        {
                            fromServer.ToClientDisable();
                        }
                        catch { }
                    }
                }
                DisableConnect();
            }
        }
        #endregion

        #region Пункты меню
        // Выход
        private void ExitMenu_Click(object sender, EventArgs e)
        {
            if (!isEnd && typePlayer != TypePlayer.local)
            {
                MessageBox.Show("Прежде чем закрыть программу - завершите игру.");
            }
            else
                Environment.Exit(0);
        }

        // Отмена хода
        private void BackStep()
        {
            if (!action.BackStep())
            {
                MessageBox.Show("Для отмены хода его необходимо закончить.");
                return;
            }
            string[] str = (string[])historyMas[historyMas.Count - 1];
            string[] str1 = new string[5];
            str1[1] = str1[4] = Convert.ToString(Convert.ToInt32(comboBoxAllTime.Text) * 600);
            if (historyMas.Count - 2 > 0)
                str1 = (string[])historyMas[historyMas.Count - 2];

            if (str[3] != "")
            {
                if (whoFirst == ColorCheck.white)
                {
                    timeWhite = Convert.ToInt32(str[1]);
                    timeBlack = Convert.ToInt32(str1[4]);
                }
                else
                {
                    timeWhite = Convert.ToInt32(str1[4]);
                    timeBlack = Convert.ToInt32(str[1]);
                }
                str[3] = str[4] = str[5] = "";
                HistoryList.Rows[HistoryList.Rows.Count - 1].Cells[1].Value = "";
                HistoryList.CurrentCell = HistoryList.Rows[HistoryList.Rows.Count - 1].Cells[0];
                cntHist--;
            }
            else
            {
                if (whoFirst == ColorCheck.white)
                {
                    timeWhite = Convert.ToInt32(str1[1]);
                    timeBlack = Convert.ToInt32(str1[4]);
                }
                else
                {
                    timeBlack = Convert.ToInt32(str1[1]);
                    timeWhite = Convert.ToInt32(str1[4]);
                }

                historyMas.RemoveAt(historyMas.Count - 1);
                HistoryList.Rows.RemoveAt(HistoryList.Rows.Count - 1);
                if (HistoryList.Rows.Count > 0)
                    HistoryList.CurrentCell = HistoryList.Rows[HistoryList.Rows.Count - 1].Cells[1];
            }
            if (action.whoMove == ColorCheck.white)
            {
                pictureBoxWhite.Visible = true;
                pictureBoxBlack.Visible = false;
            }
            else
            {
                pictureBoxWhite.Visible = false;
                pictureBoxBlack.Visible = true;
            }
        }

        // Отмена хода
        private void BackStepMenu_Click(object sender, EventArgs e)
        {
            if (historyMas.Count == 0 || isEnd)
                return;
            if (typePlayer == TypePlayer.local)
            {
                BackStep();
            }
            else if (typePlayer == TypePlayer.server)
            {
                timer1.Enabled = false;
                bool rez = fromServer.ToClientBackStep();
                timer1.Enabled = true;
                if (rez)
                    BackStep();
                else
                    MessageBox.Show("Противник отказался отменять ход.");
            }
            else
            {
                timer1.Enabled = false;
                bool rez = fromClient.ToServerBackStep();
                timer1.Enabled = true;
                if (rez)
                    BackStep();
                else
                    MessageBox.Show("Противник отказался отменять ход.");
            }
        }

        // Проверка на то, чтобы игра по сети была завершена в момент закрытия программы
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cmpClass != null)
            {

                try
                {
                    cmpAgent.Abort();
                    cmpAgent.Join();
                }
                catch
                {
                }
                cmpClass.StopGame();
                cmpClass = null;
            }

            if (!isEnd && typePlayer != TypePlayer.local)
            {
                MessageBox.Show("Прежде чем закрыть программу - завершите игру.");
                e.Cancel = true;
            }
            else if (typePlayer == TypePlayer.client && fromClient != null)
            {
                try
                {
                    fromClient.ToServerDisable();
                }
                catch { }
            }
            else if (typePlayer == TypePlayer.server && fromServer != null)
            {
                try
                {
                    fromServer.ToClientDisable();
                }
                catch { }
            }
        }

        // Вызов раздела с расстановкой позиции
        private void StatePositionMenu_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
        }

        // Показ информации о программе
        private void AboutMenu_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm();
            aboutForm.ShowDialog();
        }

        // Показ информации об авторе
        private void AutorMenu_Click(object sender, EventArgs e)
        {
            AutorForm autorForm = new AutorForm();
            autorForm.ShowDialog();
        }
        #endregion

        #region Расстановка шашек
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 2 && !isEnd)
            {
                MessageBox.Show("Для начала расстановки надо закончить игру.");
                tabControl1.SelectedIndex = 0;
            }

            if (((typePlayer == TypePlayer.client && serverReady) ||
                 (typePlayer == TypePlayer.server && clientReady)) && tabControl1.SelectedIndex == 2)
            {
                MessageBox.Show("Начальная расстановка уже утверждена.");
                tabControl1.SelectedIndex = 1;
            }

            if (tabControl1.SelectedIndex == 2)
                placing = true;
            else
                placing = false;
        }

        // Очистить все поля
        private void clearButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 == 0)
                        action.SetDataToBoard(ObjectCheck.empty, i, j);
                    else
                        action.SetDataToBoard(ObjectCheck.full, i, j);
        }

        // Установить начальную расстановку
        private void beginButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 != 0)
                        action.SetDataToBoard(ObjectCheck.check_black, i, j);
                    else
                        action.SetDataToBoard(ObjectCheck.empty, i, j);

            for (int i = 3; i < 5; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 != 0)
                        action.SetDataToBoard(ObjectCheck.full, i, j);
                    else
                        action.SetDataToBoard(ObjectCheck.empty, i, j);

            for (int i = 5; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if ((i + j) % 2 != 0)
                        action.SetDataToBoard(ObjectCheck.check_white, i, j);
                    else
                        action.SetDataToBoard(ObjectCheck.empty, i, j);
        }

        // Добавление нового поля
        public void AddCheck(int x, int y)
        {
            if ((x + y) % 2 != 0)
            {
                if (radioBlackCheck.Checked)
                    action.SetDataToBoard(ObjectCheck.check_black, x, y);
                else if (radioBlackDam.Checked)
                    action.SetDataToBoard(ObjectCheck.check_black_dam, x, y);
                else if (radioWhiteCheck.Checked)
                    action.SetDataToBoard(ObjectCheck.check_white, x, y);
                else if (radioWhiteDam.Checked)
                    action.SetDataToBoard(ObjectCheck.check_white_dam, x, y);
                else
                    action.SetDataToBoard(ObjectCheck.full, x, y);
            }
        }

        // Начало игры с данной расстановкой
        private void startButton_Click(object sender, EventArgs e)
        {
            if (radioFirstWhite.Checked)
                whoFirst = ColorCheck.white;
            else
                whoFirst = ColorCheck.black;
            if (SetStart())
            {
                if (typePlayer == TypePlayer.local)
                    tabControl1.SelectedIndex = 0;
                else
                    tabControl1.SelectedIndex = 1;
                action.ReDestination(-1, -1);
                timer1.Enabled = true;
                timerCmpStep.Enabled = true;
                if (action.IsEnd())
                    SetEnd(action.whoMove);
            }
        }
        #endregion

        #region Сохранение и загрузка игр
        // Сохранить текущую историю игры в файл
        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isEnd)
                MessageBox.Show("Сначала необходимо закончить игру.");
            else
            {
                ColorCheck cc = ColorCheck.black;
                if (HistoryList.Columns[0].HeaderText == "C")
                    cc = ColorCheck.white;
                SaveGameForm saveForm = new SaveGameForm(historyMas, cc, masSave);
                saveForm.ShowDialog();
            }
        }

        // Загрузить из файла историю игры
        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!isEnd)
                MessageBox.Show("Сначала необходимо закончить игру.");
            else
            {
                ColorCheck savewhoFirst = whoFirst;
                whoFirst = ColorCheck.disable;
                LoadGameForm loadForm = new LoadGameForm(this);
                loadForm.ShowDialog();
                if (whoFirst != ColorCheck.disable)
                {
                    if (whoFirst == ColorCheck.white)
                    {
                        HistoryList.Columns[0].HeaderText = "C";
                        HistoryList.Columns[1].HeaderText = "К";
                    }
                    else
                    {
                        HistoryList.Columns[0].HeaderText = "К";
                        HistoryList.Columns[1].HeaderText = "C";
                    }
                    HistoryList.Rows.Clear();

                    for (int i = 0; i < historyMas.Count; i++)
                    {
                        string[] str = (string[])historyMas[i];
                        string[] param = new string[2];

                        string znak = " - ";
                        if (str[2] != "")
                            znak = " : ";
                        int n = 2;
                        param[0] = YToAlpha(Convert.ToInt32(str[0].Substring(n + 2, 1))) +
                                             XToDigit(Convert.ToInt32(str[0].Substring(n, 1))) + znak +
                                             YToAlpha(Convert.ToInt32(str[0].Substring(n + 6, 1))) +
                                             XToDigit(Convert.ToInt32(str[0].Substring(n + 4, 1)));
                        n += 8;
                        while (n < str[0].Length)
                        {
                            param[0] += " : " + YToAlpha(Convert.ToInt32(str[0].Substring(n + 2, 1))) +
                                                    XToDigit(Convert.ToInt32(str[0].Substring(n, 1)));
                            n += 4;
                        }

                        if (str[3].Length > 8)
                        {
                            znak = " - ";
                            if (str[5] != "")
                                znak = " : ";
                            n = 2;
                            param[1] = YToAlpha(Convert.ToInt32(str[3].Substring(n + 2, 1))) +
                                                 XToDigit(Convert.ToInt32(str[3].Substring(n, 1))) + znak +
                                                 YToAlpha(Convert.ToInt32(str[3].Substring(n + 6, 1))) +
                                                 XToDigit(Convert.ToInt32(str[3].Substring(n + 4, 1)));
                            n += 8;
                            while (n < str[3].Length)
                            {
                                param[1] += " : " + YToAlpha(Convert.ToInt32(str[3].Substring(n + 2, 1))) +
                                                        XToDigit(Convert.ToInt32(str[3].Substring(n, 1)));
                                n += 4;
                            }
                        }
                        else
                            param[1] = "";
                        HistoryList.Rows.Add(param);
                    }
                }
                else
                    whoFirst = savewhoFirst;
            }
        }
        #endregion

        #region Подсказки
        private void button5_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Сдаться", deliverButton, 15, -17);
        }
        private void button5_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(deliverButton);
        }
        private void button1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Начать игру с начала", startGameButton, 15, -17);
        }
        private void button1_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(startGameButton);
        }
        private void button4_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Перевернуть доску", turnButton, 15, -17);
        }
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(turnButton);
        }
        private void button6_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Предложить ничью", remiButton, 15, -17);
        }
        private void button6_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(remiButton);
        }
        private void button7_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Разорвать соединение", disconnectButton, 15, -17);
        }
        private void button7_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(disconnectButton);
        }
        private void button3_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Подключиться к серверу", connectButton, 15, -17);
        }
        private void button3_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(connectButton);
        }
        private void button2_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Установить сервер", serverButton, 15, -17);
        }
        private void button2_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(serverButton);
        }
        private void backStepButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Отменить ход", backStepButton, 15, -17);
        }
        private void backStepButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip1.Hide(backStepButton);
        }
        #endregion	}

        #region Изменение режима игры
        bool stopChecked = false;
        // Изменение режима игры
        private void UserUserMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (stopChecked)
                return;
            stopChecked = true;
            if (UserUserMenu.Checked)
            {
                typeOrder = OrderGame.UserUser;
                regClass.SaveParameters();
                CompCompMenu.Checked = CompUserMenu.Checked = UserCompMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallBlack;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallWhite;
            }
            else
            {
                UserUserMenu.Checked = true;
            }
            stopChecked = false;
        }
        private void UserCompMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (stopChecked)
                return;
            stopChecked = true;
            if (UserCompMenu.Checked)
            {
                typeOrder = OrderGame.UserComp;
                regClass.SaveParameters();
                CompCompMenu.Checked = CompUserMenu.Checked = UserUserMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallComp;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallWhite;
            }
            else
            {
                UserCompMenu.Checked = true;
            }
            stopChecked = false;
        }
        private void CompUserMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (stopChecked)
                return;
            stopChecked = true;
            if (CompUserMenu.Checked)
            {
                typeOrder = OrderGame.CompUser;
                regClass.SaveParameters();
                CompCompMenu.Checked = UserUserMenu.Checked = UserCompMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallBlack;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallComp;
            }
            else
            {
                CompUserMenu.Checked = true;
            }
            stopChecked = false;
        }
        private void CompCompMenu_CheckedChanged(object sender, EventArgs e)
        {
            if (stopChecked)
                return;
            stopChecked = true;
            if (CompCompMenu.Checked)
            {
                typeOrder = OrderGame.CompComp;
                regClass.SaveParameters();
                UserUserMenu.Checked = CompUserMenu.Checked = UserCompMenu.Checked = false;
                pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallComp;
                pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallComp;
            }
            else
            {
                CompCompMenu.Checked = true;
            }
            stopChecked = false;
        }
        #endregion

        #region Отлавливание момента, когда компьютеру надо ходить, получение хода и совершение его
        private delegate void SetTimerCmpStepDelegate(bool state);
        private void SetTimerCmpStep(bool state)
        {
            if (isEnd)
            {
                timerCmpStep.Enabled = false;
                return;
            }
            timerCmpStep.Enabled = state;
        }

        /// <summary>
        /// Делегат для организации получения хода компьютера
        /// </summary>
        /// <returns></returns>
        private delegate string GetComputerStepDelegate();
        /// <summary>
        /// Получение и совершение хода компьютера
        /// </summary>
        /// <param name="iar"></param>
        private void GetComputerStep_CallBack(IAsyncResult iar)
        {
            GetComputerStepDelegate getCompStep = (GetComputerStepDelegate)((AsyncResult)iar).AsyncDelegate;

            string step = getCompStep.EndInvoke(iar);
            //string step = cmpClass.GetComputerStep();
            string[] steps = step.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);

            IAsyncResult iar1;
            try
            {
                for (int i = 0; i < steps.Length; i++)
                {
                    string s = steps[i][0].ToString();
                    string s1 = steps[i][1].ToString();
                    iar1 = this.BeginInvoke(new ActionClass.PressDelegate(action.Press), new object[] { Convert.ToInt32(s), Convert.ToInt32(s1), true });
                    //action.Press(Convert.ToInt32(s), Convert.ToInt32(s1), true);
                    if (i < steps.Length - 1)
                    {
                        Thread.Sleep(300);
                    }
                    this.EndInvoke(iar1);
                }
            }
            catch
            {
                MessageBox.Show("Ход компьютера непонятен: " + step);
            }
        }

        // Проверка на необходимость компьютеру ходить
        private void timerCmpStep_Tick(object sender, EventArgs e)
        {
            logClass.WriteLine("timerCmpStep_Tick whoMove=" + action.whoMove.ToString() +
              " action.freezeGame=" + action.freezeGame.ToString());

            if (typePlayer != TypePlayer.local || typeOrder == OrderGame.UserUser || action.freezeGame)
                return;

            // Отправка запроса на получение хода компьютера
            if (typeOrder == OrderGame.CompComp ||
              (typeOrder == OrderGame.UserComp && action.whoMove == ColorCheck.black) ||
              (typeOrder == OrderGame.CompUser && action.whoMove == ColorCheck.white))
            {
                timerCmpStep.Enabled = false;
                logClass.WriteLine("Получение хода компьютера");

                GetComputerStepDelegate getCompStep = new GetComputerStepDelegate(cmpClass.GetComputerStep);
                AsyncCallback callBack = new AsyncCallback(GetComputerStep_CallBack);
                getCompStep.BeginInvoke(callBack, null);
                /*string step = cmpClass.GetComputerStep();
                string[] steps = step.Split(new string[1] { ":" }, StringSplitOptions.RemoveEmptyEntries);
       
                try
                {
                  for (int i = 0; i < steps.Length; i++)
                  {
                    string s = steps[i][0].ToString();
                    string s1 = steps[i][1].ToString();            
                    action.Press(Convert.ToInt32(s), Convert.ToInt32(s1), true);
                    if (i < steps.Length - 1)
                    {
                      Thread.Sleep(300);
                    }            
                  }
                }
                catch
                {
                  MessageBox.Show("Ход компьютера непонятен: " + step);
                }
                timerCmpStep.Enabled = true;     */
                /*
                 string state = "Hello, background world!";
                 Thread t = new Thread(new ParameterizedThreadStart(MyMethod));
                 t.Start(state);

                 */
            }
        }
        #endregion
    }
}