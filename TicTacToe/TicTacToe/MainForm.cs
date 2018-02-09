using System;
using System.Net;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Channels.Tcp;

using FromClientClass;
using FromServerClass;

namespace TicTacToe
{
    /// <summary>
    /// Тип клиента
    /// </summary>
    public enum TypePlayer : byte
    {
        Server,
        Client,
        Local
    }

    /// <summary>
    /// Объект на поле: крестик, нолик или путо
    /// </summary>
    public enum ObjectType : byte
    {
        Empty,
        Cross,
        Nil
    }

    public partial class MainForm : Form
    {
        private ObjectType m_WhoStep = ObjectType.Empty;			// Чей сейчас ход
        private bool m_ServerReady;		            				// Готовность сервера играть
        private bool m_ClientReady;     			    			// Готовность клиента играть
        private TypePlayer m_TypePlayer = TypePlayer.Local;		    // Тип текущей игры
        private PaintClass m_PaintClass;							// Общий класс для рисования
        private int m_TimeCross;									// Время для крестиков в миллисекундах
        private int m_TimeNil;									    // Время для ноликов в миллисекундах		
        private int m_AddSec;										// Количество добавляемых миллисекунд на ход	
        private bool m_IsPanelBlock;                                // Блокирование панели на время мигания нового объекта

        private ObjectType[,] m_Field;		                        // Поле с текущей позицией
        private int m_RowsCnt;                                      // Количество строк на поле
        private int m_ColumnsCnt;                                   // Количество столбцов на поле

        public MainForm()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            m_PaintClass = new PaintClass();

            m_RowsCnt = PaintClass.BitmapWidth / PaintClass.CellSize;
            m_ColumnsCnt = PaintClass.BitmapHeight / PaintClass.CellSize;
            m_Field = new ObjectType[m_RowsCnt, m_ColumnsCnt];

            PaintBitmap();
        }



        public void PaintBitmap()
        {
            using (Graphics g = panelField.CreateGraphics())
            {
                g.DrawImage(m_PaintClass.FieldBitmap, 0, 0);
            }
        }


        /// <summary>
        /// Перерисовка изображения на панели
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelField_Paint(object sender, PaintEventArgs e)
        {
            PaintBitmap();
        }


        /// <summary>
        /// Кнопка сдачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deliverButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите сдаться?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SetEnd(m_WhoStep);

                if (m_TypePlayer == TypePlayer.Client)
                {
                    try
                    {
                        m_FromClient.ToServerDeliver();
                    }
// ReSharper disable EmptyGeneralCatchClause
                    catch { }
// ReSharper restore EmptyGeneralCatchClause
                }
                else
                {
                    try
                    {
                        m_FromServer.ToClientDeliver();
                    }
// ReSharper disable EmptyGeneralCatchClause
                    catch { }
// ReSharper restore EmptyGeneralCatchClause
                }
            }
        }


        /// <summary>
        /// Кнопка запуска новой игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startGameButton_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_RowsCnt; i++)
            {
                for (int j = 0; j < m_ColumnsCnt; j++)
                {
                    m_Field[i, j] = ObjectType.Empty;
                }
            }

            m_PaintClass.ClearField();
            PaintBitmap();

            if (SetStart())
            {
                timer1.Enabled = true;
            }
        }

        private delegate void DoStepDelegate(int x, int y);

        /// <summary>
        /// Сделать ход для крестика или для нолика
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void DoStep(int x, int y)
        {
            buttonGetComputerStep.Enabled = true;
            try
            {
                m_IsPanelBlock = true;
                m_Field[x, y] = m_WhoStep;
                
                m_PaintClass.DrawObject(m_WhoStep, x, y);
                PaintBitmap();
                m_PaintClass.ShowLastStep(this, 1);

                m_IsPanelBlock = false;

                ObjectType loser = new CheckFieldClass().FindLoser(m_Field, m_RowsCnt, m_ColumnsCnt, m_PaintClass);
                PaintBitmap();

                m_WhoStep = m_WhoStep == ObjectType.Cross ? ObjectType.Nil : ObjectType.Cross;

                if (loser != ObjectType.Empty)
                {
                    SetEnd(loser);
                }
                else if (m_WhoStep == ObjectType.Cross)
                {
                    pictureBoxCross.Visible = true;
                    pictureBoxNil.Visible = false;
                    m_TimeNil += m_AddSec;
                }
                else
                {
                    pictureBoxCross.Visible = false;
                    pictureBoxNil.Visible = true;
                    m_TimeCross += m_AddSec;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information );
                SetEnd(ObjectType.Empty);
            }
        }

        /// <summary>
        /// Действия при очередном ходе
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panelField_MouseDown(object sender, MouseEventArgs e)
        {
            if (!m_ServerReady || !m_ClientReady ||
                (m_TypePlayer == TypePlayer.Client && m_WhoStep == ObjectType.Cross) ||
                (m_TypePlayer == TypePlayer.Server && m_WhoStep == ObjectType.Nil) ||
                m_IsPanelBlock)
            {
                return;
            }

            int x = e.X / PaintClass.CellSize;
            int y = e.Y / PaintClass.CellSize;

            if (m_Field[x, y] != ObjectType.Empty)
            {
                return;
            }

            if (m_TypePlayer != TypePlayer.Local)
            {
                if (m_WhoStep == ObjectType.Cross)
                {
                    m_FromServer.ToClientStep(x, y, m_TimeCross);
                }
                else
                {
                    m_FromClient.ToServerStep(x, y, m_TimeNil);
                }
                UpdateLastLinesBoxNetInfo("Ожидание хода противника.");
            }

            DoStep(x, y);
        }


        /// <summary>
        /// Действия при начале игры
        /// </summary>
        /// <returns></returns>
        private bool SetStart()
        {
            try
            {
                m_TimeCross = m_TimeNil = Convert.ToInt32(comboBoxAllTime.Text) * 600;
            }
            catch
            {
                MessageBox.Show("Ошибка в записи количества минут");
                return false;
            }

            try
            {
                m_AddSec = Convert.ToInt32(comboBoxAddSec.Text) * 10;
            }
            catch
            {
                MessageBox.Show("Ошибка в записи количества секунд");
                return false;
            }

            comboBoxAddSec.Enabled = comboBoxAllTime.Enabled = false;

            startGameButton.Visible = serverButton.Visible = connectButton.Visible = false;
            deliverButton.Visible = remiButton.Visible = buttonViewLastStep.Visible = true;

            if (m_TypePlayer == TypePlayer.Local)
            {
                if (m_WhoStep == ObjectType.Empty)
                {
                    m_WhoStep = ObjectType.Cross;
                }
                m_ServerReady = m_ClientReady = true;
            }
            else
            {
                if (m_TypePlayer == TypePlayer.Client)
                {
                    if (m_WhoStep == ObjectType.Empty)
                    {
                        m_WhoStep = ObjectType.Cross;
                    }
                    m_ClientReady = true;
                    m_FromClient.ToServerReady(comboBoxAllTime.Text, comboBoxAddSec.Text);
                }
                else
                {
                    if (m_WhoStep == ObjectType.Empty)
                    {
                        m_WhoStep = ObjectType.Cross;
                    }
                    m_ServerReady = true;
                    m_FromServer.ToClientReady(comboBoxAllTime.Text, comboBoxAddSec.Text);
                }
                if (m_ServerReady && m_ClientReady)
                {
                    if ((m_TypePlayer == TypePlayer.Server && m_WhoStep == ObjectType.Cross) ||
                         (m_TypePlayer == TypePlayer.Client && m_WhoStep == ObjectType.Nil))
                    {
                        textBoxNetInfo.Text += "Игра началась\r\nВаш ход";
                    }
                    else
                    {
                        textBoxNetInfo.Text += "Игра началась\r\nОжидание хода противника";
                    }
                }
            }

            if (m_WhoStep == ObjectType.Cross)
            {
                pictureBoxNil.Visible = false;
            }
            else
            {
                pictureBoxCross.Visible = false;
            }

            return true;
        }


        /// <summary>
        /// Действия при завершении игры
        /// </summary>
        /// <param name="whoLose">Кто проиграл</param>
        private void SetEnd(ObjectType whoLose)
        {
            timer1.Enabled = false;

            comboBoxAddSec.Enabled = comboBoxAllTime.Enabled = true;

            startGameButton.Visible = true;
            m_ClientReady = m_ServerReady = false;
            if (m_TypePlayer == TypePlayer.Local)
            {
                serverButton.Visible = connectButton.Visible = true;                
            }
            deliverButton.Visible = remiButton.Visible = buttonViewLastStep.Visible = false;
            pictureBoxCross.Visible = pictureBoxNil.Visible = true;
            textBoxNetInfo.Text = "";

            if (whoLose == ObjectType.Empty)
            {
                MessageBox.Show("Партия завершилась вничью");
            }
            else if (whoLose == ObjectType.Nil)
            {
                labelCross.Text = Convert.ToString(Convert.ToInt32(labelCross.Text) + 1);
                MessageBox.Show("Крестики выиграли");
            }
            else
            {
                labelNil.Text = Convert.ToString(Convert.ToInt32(labelNil.Text) + 1);
                MessageBox.Show("Нолики выиграли");
            }
        }
        
        /// <summary>
        /// Замена последней строки с поле с сетевой информацией на newStr
        /// </summary>
        /// <param name="newStr"></param>
        private void UpdateLastLinesBoxNetInfo(string newStr)
        {
            string str = "";
            for (int i = 0; i < textBoxNetInfo.Lines.Length - 1; i++)
                str += textBoxNetInfo.Lines[i] + "\r\n";
            str += newStr;
            textBoxNetInfo.Text = str;
        }

        /// <summary>
        /// Отображение времени (с точность до сек)			
        /// </summary>
        /// <param name="textBoxTime">TextBox в котором надо отобразить время</param>
        /// <param name="time">Время в миллисекундах</param>
        private static void ShowTime(Control textBoxTime, int time)
        {
            string min = Convert.ToString(time / 10 / 60);
            string sec = Convert.ToString(time / 10 % 60);
            if (sec.Length == 1)
                sec = "0" + sec;
            if (min + ":" + sec != textBoxTime.Text)
                textBoxTime.Text = min + ":" + sec;
        }


        /// <summary>
        /// Изменение времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!m_ServerReady || !m_ClientReady)
            {
                return;
            }

            if (m_WhoStep == ObjectType.Nil)
                m_TimeNil--;
            else
                m_TimeCross--;

            if (m_TimeNil < 0)
                SetEnd(ObjectType.Nil);
            if (m_TimeCross < 0)
                SetEnd(ObjectType.Cross);

            ShowTime(boxTimeCross, m_TimeCross);
            ShowTime(boxTimeNil, m_TimeNil);
            Application.DoEvents();
        }

        #region Отсылка предложения о ничьей и обработка ответа
        private delegate void SetRemiDelegate(bool rez);
        private delegate bool ToServerRemiDelegate();
        private delegate bool ToClientRemiDelegate();

        // Остановка игры c результатом "Ничья"
        private void SetRemi(bool rez)
        {
            if (rez)
                SetEnd(ObjectType.Empty);
            else
                MessageBox.Show("Противник отказался от ничьей");
        }

        private void ToServerRemiQuery(IAsyncResult ar)
        {
            var d = (ToServerRemiDelegate)((AsyncResult)ar).AsyncDelegate;

            try
            {
                bool str = d.EndInvoke(ar);
                BeginInvoke(new SetRemiDelegate(SetRemi), new object[] { str });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToClientRemiQuery(IAsyncResult ar)
        {
            var d = (ToClientRemiDelegate)((AsyncResult)ar).AsyncDelegate;

            try
            {
                bool str = d.EndInvoke(ar);
                BeginInvoke(new SetRemiDelegate(SetRemi), new object[] { str });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Вызов не прошёл. Возможно, не установлен сервер.\n" + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Кнопка для предложения ничьи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void remiButton_Click(object sender, EventArgs e)
        {
            if (m_TypePlayer == TypePlayer.Local)
            {
                if (MessageBox.Show("Вы уверены, что хотите завершить партию вничью?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    SetEnd(ObjectType.Empty);
            }
            else if (m_TypePlayer == TypePlayer.Client)
            {
                var cb = new AsyncCallback(ToServerRemiQuery);
                var d = new ToServerRemiDelegate(m_FromClient.ToServerRemi);
                d.BeginInvoke(cb, null);
            }
            else
            {
                var cb = new AsyncCallback(ToClientRemiQuery);
                var d = new ToClientRemiDelegate(m_FromServer.ToClientRemi);
                d.BeginInvoke(cb, null);
            }
        }
        #endregion

        #region Функции изменения формы для серверной и клиентской частей
        private FromClientTransmittor m_FromClient;
        private FromServerTransmittor m_FromServer;
        public string IpConnect;
        private ObjRef m_RefFromClient;
        private ObjRef m_RefFromServer;

        private delegate void SetEndRemiDelegate();
        private void SetEndRemi()
        {
            SetEnd(ObjectType.Empty);
        }

        private delegate void SetEndReadyDelegate(ObjectType who, string timeAll, string addSec);
        private void SetEndReady(ObjectType who, string timeAll, string addSec)
        {
            if (who == ObjectType.Nil)
                textBoxNetInfo.Text += "Клиент готов играть\r\n";
            else
                textBoxNetInfo.Text += "Сервер готов играть\r\n";
            if (m_ServerReady && m_ClientReady)
            {
                if ((m_TypePlayer == TypePlayer.Server && m_WhoStep == ObjectType.Cross) ||
                     (m_TypePlayer == TypePlayer.Client && m_WhoStep == ObjectType.Nil))
                    textBoxNetInfo.Text += "Игра началась\r\nВаш ход";
                else
                    textBoxNetInfo.Text += "Игра началась\r\nОжидание хода противника";
            }
            else
            {
                comboBoxAllTime.Text = timeAll;
                comboBoxAddSec.Text = addSec;
                comboBoxAllTime.Enabled =
                comboBoxAddSec.Enabled = false;
            }
        }

        private delegate void SetEndDisableDelegate(ObjectType loser);
        private void SetEndDisable(ObjectType loser)
        {
            MessageBox.Show("Противник разорвал связь");
            DisableConnect();
            SetEnd(loser);
        }

        private delegate void SetEndDeliverDelegate(ObjectType looser);
        private void SetEndDeliver(ObjectType loser)
        {
            SetEnd(loser);
        }

        private delegate void SetStartGameDelegate();
        private void SetStartGame()
        {
            textBoxNetInfo.Text += "Клиент присоединился\r\n";
            startGameButton.Enabled = true;
        }
        #endregion

        #region Серверная часть
        // Установить сервер
        private void serverButton_Click(object sender, EventArgs e)
        {
            try
            {
                textBoxNetInfo.Text = "";
                m_ClientReady = m_ServerReady = false;
                ChannelServices.RegisterChannel(new TcpChannel(8080), false);

                m_FromClient = new FromClientTransmittor();
                m_RefFromClient = RemotingServices.Marshal(m_FromClient, "FromClientTransmittor");

                // Обработка события на запрос о ничьей
                m_FromClient.ToServerRemiEvent += ToServerRemiEvent;

                // Обработка события на сообщение о начале игры
                m_FromClient.StartGameEvent += StartGameEvent;

                // Обработка нового хода клиента
                m_FromClient.ToServerStepEvent += ToServerStepEvent;

                // Обработка события на сообщение о ничьей
                m_FromClient.ToServerDeliverEvent += ToServerDeliverEvent;

                // Обработка события на готовность клиента играть
                m_FromClient.ToServerReadyEvent += ToServerReadyEvent;

                // Обработка события на отключение клиента 
                m_FromClient.ToServerDisableEvent += ToServerDisableEvent;

                m_WhoStep = ObjectType.Cross;
                m_TypePlayer = TypePlayer.Server;
                IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1];
                textBoxNetInfo.Text += "Сервер установлен\r\nIP-адрес: " + ip + "\r\n";
                textBoxNetInfo.Text += "Ожидаем подсоединения клиента\r\n";
                
                serverButton.Visible = connectButton.Visible = startGameButton.Enabled = false;
                disconnectButton.Visible = true;
            }
            catch
            {
                MessageBox.Show("Сервер не установлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                try
                {
                    ChannelServices.UnregisterChannel(ChannelServices.RegisteredChannels[0]);
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause

                try
                {
                    if (m_RefFromClient != null)
                    {
                        RemotingServices.Unmarshal(m_RefFromClient);
                        RemotingServices.Disconnect(m_FromClient);
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause

                try
                {
                    if (m_RefFromServer != null)
                    {
                        RemotingServices.Unmarshal(m_RefFromServer);
                        RemotingServices.Disconnect(m_FromServer);
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }
        }

        // Обработка события на сообщение клиента о готовности играть
        private void ToServerReadyEvent(string timeAll, string addSec)
        {
            m_ClientReady = true;
            BeginInvoke(new SetEndReadyDelegate(SetEndReady), new object[] { ObjectType.Nil, timeAll, addSec });
        }

        // Обработка события на отключение клиента
        private void ToServerDisableEvent()
        {
            ObjectType loser = ObjectType.Empty;
            if (m_ClientReady && m_ServerReady)
            {
                loser = ObjectType.Nil;
            }
            BeginInvoke(new SetEndDisableDelegate(SetEndDisable), new object[] { loser });
        }

        // Обработка события на запрос о ничьей
        private bool ToServerRemiEvent()
        {
            if (MessageBox.Show("Противник предлагает ничью. Вы согласны?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BeginInvoke(new SetEndRemiDelegate(SetEndRemi));
                return true;
            }

            return false;
        }

        // Обработка события при сдаче клиента
        private void ToServerDeliverEvent()
        {
            BeginInvoke(new SetEndDeliverDelegate(SetEndDeliver), new object[] { ObjectType.Nil });
        }

        // Обработка сообщения от клиента о начале игры
        private void StartGameEvent(string ipClient)
        {
            try
            {
                // Установка клиента к ipClient			
                m_FromServer = (FromServerTransmittor)Activator.GetObject(typeof(FromServerTransmittor), ipClient + "/FromServerTransmittor");
                BeginInvoke(new SetStartGameDelegate(SetStartGame));
            }
            catch
            {
                MessageBox.Show("Ошибка работы по сети.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Действия после хода клиента
        private delegate void ServerStepDelegate(int x, int y, int timeClient);
        private void ServerStep(int x, int y, int timeClient)
        {
            m_TimeNil = timeClient;
            UpdateLastLinesBoxNetInfo("Ваш ход");
            DoStep(x, y);            
        }

        // Координаты ячейки, куда поставили нолик
        private void ToServerStepEvent(int x, int y, int timeClient)
        {
            BeginInvoke(new ServerStepDelegate(ServerStep), new object[] { x, y, timeClient });
        }
        #endregion

        #region Клиентская часть
        private delegate void StartGameDelegate(string ipConnect);
        private static void StartGameQuery(IAsyncResult ar)
        {
            var d = (StartGameDelegate)((AsyncResult)ar).AsyncDelegate;            
            d.EndInvoke(ar);
        }

        // Подключиться к серверу
        private void connectButton_Click(object sender, EventArgs e)
        {
            try
            {
                m_ClientReady = m_ServerReady = false;
                new ServerConnectForm(this).ShowDialog();
                if (IpConnect != "")
                {
                    ChannelServices.RegisterChannel(new TcpChannel(8081), false);

                    m_FromClient = (FromClientTransmittor)Activator.GetObject(typeof(FromClientTransmittor), "tcp://" + IpConnect + ":8080/FromClientTransmittor");

                    // Установить свой сервер для получения ответа от сервера					
                    m_FromServer = new FromServerTransmittor();
                    m_RefFromServer = RemotingServices.Marshal(m_FromServer, "FromServerTransmittor");

                    // Обработка события на запрос о ничьей
                    m_FromServer.ToClientRemiEvent += ToClientRemiEvent;

                    // Обработка события на сообщение о сдаче сервера
                    m_FromServer.ToClientDeliverEvent += ToClientDeliverEvent;

                    // Обработка очередного хода сервера
                    m_FromServer.ToClientStepEvent += ToClientStepEvent;

                    // Обработка события на готовность клиента играть
                    m_FromServer.ToClientReadyEvent += ToClientReadyEvent;

                    // Обработка события на отключение клиента 
                    m_FromServer.ToClientDisableEvent += ToClientDisableEvent;

                    // Получение сетевого адреса клиента (вида tcp://xxx.xxx.xxx.xxx:8081)
                    IPAddress ip = Dns.GetHostEntry(Dns.GetHostName()).AddressList[Dns.GetHostEntry(Dns.GetHostName()).AddressList.Length - 1];
                    string addr = "tcp://" + ip + ":8081";

                    var cb = new AsyncCallback(StartGameQuery);
                    var d = new StartGameDelegate(m_FromClient.StartGame);
                    d.BeginInvoke(addr, cb, null);

                    m_WhoStep = ObjectType.Cross;
                    m_TypePlayer = TypePlayer.Client;
                    serverButton.Visible = connectButton.Visible = false;
                    disconnectButton.Visible = true;
                    textBoxNetInfo.Text = "Соединение с сервером установлено\r\n";
                }
            }
            catch
            {
                MessageBox.Show("Подключение к серверу не установлено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ChannelServices.UnregisterChannel(ChannelServices.RegisteredChannels[0]);
                try
                {
                    if (m_RefFromClient != null)
                    {
                        RemotingServices.Unmarshal(m_RefFromClient);
                        RemotingServices.Disconnect(m_FromClient);
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause

                try
                {
                    if (m_RefFromServer != null)
                    {
                        RemotingServices.Unmarshal(m_RefFromServer);
                        RemotingServices.Disconnect(m_FromServer);
                    }
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }
        }

        // Обработка события на сообщение сервера о готовности играть
        private void ToClientReadyEvent(string timeAll, string addSec)
        {
            m_ServerReady = true;
            BeginInvoke(new SetEndReadyDelegate(SetEndReady), new object[] { ObjectType.Cross, timeAll, addSec });
        }

        // Обработка события на отключение сервера
        private void ToClientDisableEvent()
        {
            ObjectType loser = ObjectType.Empty;
            if (m_ClientReady && m_ServerReady)
            {
                loser = ObjectType.Cross;
            }
            BeginInvoke(new SetEndDisableDelegate(SetEndDisable), new object[] { loser });
        }

        // Обработка события на запрос о ничьей
        private bool ToClientRemiEvent()
        {
            if (MessageBox.Show("Противник предлагает ничью. Вы согласны?", "Запрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                BeginInvoke(new SetEndRemiDelegate(SetEndRemi));
                return true;
            }

            return false;
        }

        // Обработка события на сообщение о сдаче
        private void ToClientDeliverEvent()
        {
            BeginInvoke(new SetEndDeliverDelegate(SetEndDeliver), new object[] { ObjectType.Cross });
        }

        // Действия после хода сервера
        private delegate void ClientStepDelegate(int x, int y, int timeServer);
        private void ClientStep(int x, int y, int timeServer)
        {
            m_TimeCross = timeServer;
            UpdateLastLinesBoxNetInfo("Ваш ход");
            DoStep(x, y);            
        }

        // Изменённые клетки после очередного хода сервера
        // В первых двух - ход шашки(дамки), в третьей (если она есть) - съеденная шашка
        private void ToClientStepEvent(int x, int y, int timeServer)
        {
            BeginInvoke(new ClientStepDelegate(ClientStep), new object[] { x, y, timeServer });
        }
        #endregion

        #region Разрыв соединения
        // Непосредственно разрыв
        private void DisableConnect()
        {
            ChannelServices.UnregisterChannel(ChannelServices.RegisteredChannels[0]);
            try
            {
                if (m_RefFromClient != null)
                {
                    RemotingServices.Unmarshal(m_RefFromClient);
                    RemotingServices.Disconnect(m_FromClient);
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { }
// ReSharper restore EmptyGeneralCatchClause

            try
            {
                if (m_RefFromServer != null)
                {
                    RemotingServices.Unmarshal(m_RefFromServer);
                    RemotingServices.Disconnect(m_FromServer);
                }
            }
// ReSharper disable EmptyGeneralCatchClause
            catch { }
// ReSharper restore EmptyGeneralCatchClause

            m_TypePlayer = TypePlayer.Local;
            serverButton.Visible = connectButton.Visible = true;
            disconnectButton.Visible = false;
            m_FromClient = null;
            m_FromServer = null;
            startGameButton.Enabled = true;
            m_WhoStep = ObjectType.Empty;
            m_ClientReady = m_ServerReady = false;
            textBoxNetInfo.Text += "Соединение разорвано.";
        }

        // Разорвать соединение
        public void disconnectButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите разорвать соединение?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (m_FromClient != null && m_FromServer != null)
                {
                    if (m_TypePlayer == TypePlayer.Client)
                    {
                        if (m_ServerReady && m_ClientReady)
                            SetEnd(ObjectType.Nil);
                        else
                            SetEnd(ObjectType.Empty);
                        try
                        {
                            m_FromClient.ToServerDisable();
                        }
// ReSharper disable EmptyGeneralCatchClause
                        catch { }
// ReSharper restore EmptyGeneralCatchClause
                    }
                    else
                    {
                        if (m_ServerReady && m_ClientReady)
                            SetEnd(ObjectType.Cross);
                        else
                            SetEnd(ObjectType.Empty);
                        try
                        {
                            m_FromServer.ToClientDisable();
                        }
// ReSharper disable EmptyGeneralCatchClause
                        catch { }
// ReSharper restore EmptyGeneralCatchClause
                    }
                }
                DisableConnect();
            }
        }
        #endregion

        #region Подсказки
        private void startGameButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Начать новую игру", startGameButton, 15, -17);
        }

        private void startGameButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(startGameButton);
        }

        private void remiButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Предложить ничью", remiButton, 15, -17);
        }

        private void remiButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(remiButton);
        }

        private void deliverButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Сдаться", deliverButton, 15, -17);
        }

        private void deliverButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(deliverButton);
        }

        private void connectButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Подключиться к серверу", connectButton, 15, -17);
        }

        private void connectButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(connectButton);
        }

        private void disconnectButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Разорвать соединение", disconnectButton, 15, -17);
        }

        private void disconnectButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(disconnectButton);
        }

        private void serverButton_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Установить сервер", serverButton, 15, -17);
        }

        private void serverButton_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(serverButton);
        }

        private void buttonViewLastStep_MouseEnter(object sender, EventArgs e)
        {
            toolTip.Show("Показать последний ход", buttonViewLastStep, 15, -17);
        }

        private void buttonViewLastStep_MouseLeave(object sender, EventArgs e)
        {
            toolTip.Hide(buttonViewLastStep);
        }
        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_ServerReady && m_ClientReady && m_TypePlayer != TypePlayer.Local)
            {
                MessageBox.Show("Прежде чем закрыть программу - завершите игру.");
                e.Cancel = true;
            }
            else if (m_TypePlayer == TypePlayer.Client && m_FromClient != null)
            {
                try
                {
                    m_FromClient.ToServerDisable();
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }
            else if (m_TypePlayer == TypePlayer.Server && m_FromServer != null)
            {
                try
                {
                    m_FromServer.ToClientDisable();
                }
// ReSharper disable EmptyGeneralCatchClause
                catch { }
// ReSharper restore EmptyGeneralCatchClause
            }
        }
        /*
        [ DllImport( "kernel32.dll" ) ]
        private static extern bool SetProcessWorkingSetSize ( IntPtr handle, int minimumWorkingSetSize, int maximumWorkingSetSize );
        */
        /// <summary>
        /// Временная кнопка для получения хода компьютера
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonGetComputerStep_Click(object sender, EventArgs e)
        {
            if (!m_ServerReady || !m_ClientReady || m_IsPanelBlock)
            {
                return;
            }

            buttonGetComputerStep.Enabled = false;

            //SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, 204800, 104857600);

            var getCompStep = new GetComputerStepDelegate(ComputerClass.DoStep);
            var callBack = new AsyncCallback(GetComputerStepCallBack);
            getCompStep.BeginInvoke(m_Field, m_RowsCnt, m_ColumnsCnt, m_WhoStep, m_PaintClass, callBack, null);
        }


        /// <summary>
        /// Делегат для организации получения хода компьютера (чтобы время компьютера изменялось)
        /// </summary>
        /// <returns></returns>
        private delegate int[] GetComputerStepDelegate(ObjectType[,] field, int rowsCnt, int columnsCnt, ObjectType whoStep, PaintClass paintClass);
        /// <summary>
        /// Получение и совершение хода компьютера
        /// </summary>
        /// <param name="iar"></param>
        private void GetComputerStepCallBack(IAsyncResult iar)
        {
            var getCompStep = (GetComputerStepDelegate)((AsyncResult)iar).AsyncDelegate;

            int[] compStep = getCompStep.EndInvoke(iar);            
            
            try
            {
                var doStepDelegate = new DoStepDelegate(DoStep);
                BeginInvoke(doStepDelegate, compStep[0], compStep[1]);
                //DoStep(compStep[0], compStep[1]);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       


        /// <summary>
        /// Показать последний сделанный ход
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonViewLastStep_Click(object sender, EventArgs e)
        {
            m_PaintClass.ShowLastStep(this, 3);
        }
    }
}
