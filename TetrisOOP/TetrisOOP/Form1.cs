using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;

namespace TetrisOOP
{
    public partial class Form1 : Form
    {
        Board board;
        Shape shape;
        Shape nextShape;

        int saveSpeed;
        int saveDelay;

        bool gameRun = false;

        private const int SPI_SETKEYBOARDDELAY = 23;
        private const int SPI_GETKEYBOARDDELAY = 22;
        private const int SPI_SETKEYBOARDSPEED = 11;
        private const int SPI_GETKEYBOARDSPEED = 10;
        private const int SPIF_SENDCHANGE = 1;
        [DllImport("user32.dll")]
        public static extern int SystemParametersInfo(int uAction, int uParam, ref int lpvParam, int fuWinIni);

        public Form1()
        {
            InitializeComponent();

            SystemParametersInfo(SPI_GETKEYBOARDSPEED, saveSpeed, ref saveSpeed, SPIF_SENDCHANGE);
            SystemParametersInfo(SPI_GETKEYBOARDDELAY, saveDelay, ref saveDelay, SPIF_SENDCHANGE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Shape CreateShape()
        {
            Shape res;
            Random r = new Random();
            int i = r.Next(7);
            switch (i)
            {
                case 0:
                    res = new Line(board);
                    break;
                case 1:
                    res = new Hill(board);
                    break;
                case 2:
                    res = new LeftLadder(board);
                    break;
                case 3:
                    res = new RightLadder(board);
                    break;
                case 4:
                    res = new LeftG(board);
                    break;
                case 5:
                    res = new RightG(board);
                    break;
                default:
                    res = new Square(board);
                    break;
            }
            res.OnShapeDown += new Shape.ShapeDown(shape_OnShapeDown);
            res.OnEndGame += new Shape.ShapeDown(shape_OnEndGame);
            return res;
        }

        /// <summary>
        /// Генерирование новой будущей фигуры и инициализация текущей фигуры
        /// </summary>
        private void GenerateNewShape()
        {
            shape = nextShape;
            nextShape = CreateShape();

            shape.Initialize();
            pictureNextFigure.Image = nextShape.GetImage();
            pictureBoard.Image = board.GetImageField();

            Application.DoEvents();
        }

        /// <summary>
        /// Кнопка старта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (buttonStart.Text == "Старт")
            {                
                int newSpeed = 31;
                SystemParametersInfo(SPI_SETKEYBOARDSPEED, newSpeed, ref newSpeed, SPIF_SENDCHANGE);
                int newDelay = 0;
                SystemParametersInfo(SPI_SETKEYBOARDDELAY, newDelay, ref newDelay, SPIF_SENDCHANGE);
                labelScore.Text = "Очки:\n0";
                labelSpeed.Text = "Скорость:\n1";
                timerDown.Interval = 1000;
                if (radioTrening.Checked)
                {
                    board = new Board(20, 10);
                    labelLevel.Visible = false;
                }
                else
                {
                    board = new GameBoard(20, 10);
                    if (((GameBoard)board).Level == 0)
                    {
                        board = new Board(20, 10);
                        radioTrening.Checked = true;
                    }
                    else
                    {
                        labelLevel.Visible = true;
                        labelLevel.Text = ((GameBoard)board).LevelInfo;
                        labelScore.Text = board.ScoreInfo;
                        labelSpeed.Text = board.SpeedInfo;
                    }
                }
                blockDown = false;

                nextShape = CreateShape();

                GenerateNewShape();

                groupTypeGame.Enabled = false;
                buttonPause.Enabled = true;
                buttonStart.Text = "Стоп";
                gameRun = true;
                timerDown.Enabled = true;
            }
            else
            {
                timerDown.Enabled = false;
                gameRun = false;                
                groupTypeGame.Enabled = true;
                buttonPause.Enabled = false;
                buttonStart.Text = "Старт";
            }
        }

        /// <summary>
        /// Сдвиг фигуры вниз по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (!gameRun)
                return;

            shape.MouveDown();
            pictureBoard.Image = board.GetImageField();
        }

        /// <summary>
        /// Кнопка пауза/продолжить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (buttonPause.Text == "Пауза")
            {
                timerDown.Enabled = false;
                buttonPause.Text = "Продолжить";
            }
            else
            {
                timerDown.Enabled = true;
                buttonPause.Text = "Пауза";
            }
        }

        /// <summary>
        /// Отлавливание события опускания фигуры (когда ей уже некуда опускаться)
        /// </summary>
        private void shape_OnShapeDown()
        {
            timerDown.Enabled = false;
            blockDown = true;
            int cntRows = board.EraseRows();
            board.SetScore(cntRows);

            labelScore.Text = board.ScoreInfo;
            timerDown.Interval = (int)((1.1 - board.Speed * 0.1) * 1000);

            labelSpeed.Text = board.SpeedInfo;
            if (labelLevel.Visible)
            {
                labelLevel.Text = ((GameBoard)board).LevelInfo;
            }
            timerDown.Enabled = true;
            GenerateNewShape();
        }

        private void ShowMessage()
        {
            MessageBox.Show("Игра окончена", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Отлавливание окончания игры
        /// </summary>
        private void shape_OnEndGame()
        {
            timerDown.Enabled = false;
            gameRun = false;
            groupTypeGame.Enabled = true;
            buttonPause.Enabled = false;
            buttonStart.Text = "Старт";          
            Thread newThread = new Thread(new ThreadStart(ShowMessage));
            newThread.Start();
        }

        bool blockDown = false;

        /// <summary>
        /// Отлов нажатия кнопок на форме
        /// </summary>
        /// <param name="keyData">Код клавиши</param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (!gameRun)
                return base.ProcessDialogKey(keyData);

            if (keyData == Keys.Left)
            {
                shape.MouveLeft();
                pictureBoard.Image = board.GetImageField();
                blockDown = false;
                return true;
            }
            else if (keyData == Keys.Right)
            {
                shape.MouveRight();
                pictureBoard.Image = board.GetImageField();
                blockDown = false;
                return true;
            }
            else if (keyData == Keys.Down && !blockDown)
            {
                shape.MouveDown();
                pictureBoard.Image = board.GetImageField();
                timerDown.Enabled = false;
                timerDown.Enabled = true;
                return true;
            }
            else if (keyData == Keys.Up || keyData == Keys.Space)
            {
                shape.Rotate();
                pictureBoard.Image = board.GetImageField();
                blockDown = false;
                return true;
            }
            else if (keyData == Keys.P || keyData == Keys.Escape)
            {
                buttonPause_Click(null, null);
                pictureBoard.Image = board.GetImageField();
                blockDown = false;
                return true;
            }
            Application.DoEvents();
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Кнопка выхода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuFileExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Возвращаем настройки клавиатуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemParametersInfo(SPI_SETKEYBOARDSPEED, saveSpeed, ref saveSpeed, SPIF_SENDCHANGE);
            SystemParametersInfo(SPI_SETKEYBOARDDELAY, saveDelay, ref saveDelay, SPIF_SENDCHANGE);

            if (gameRun && radioGame.Checked)
            {
                board.SaveGame();
            }
        }

        private void правилаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RulesForm ruleForm = new RulesForm();
            ruleForm.ShowDialog();
        }

        private void создательУровнейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerDown.Enabled = false;
            CreatorForm createForm = new CreatorForm();
            createForm.ShowDialog();
            timerDown.Enabled = true;
        }

        private void Drop_Focus(object sender, EventArgs e)
        {
            buttonFocus.Focus();
        }


        /// <summary>
        /// Позволяем опять использовать кнопку вниз после появления новой фигуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFocus_KeyUp(object sender, KeyEventArgs e)
        {
            blockDown = false;
        }
    }
}
