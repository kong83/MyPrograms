namespace Checkers
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.boxTimeWhite = new System.Windows.Forms.TextBox();
            this.boxTimeBlack = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelBlack = new System.Windows.Forms.Label();
            this.labelWhite = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBoxWhite = new System.Windows.Forms.PictureBox();
            this.pictureBoxBlack = new System.Windows.Forms.PictureBox();
            this.HistoryList = new System.Windows.Forms.DataGridView();
            this.nameUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxAddSec = new System.Windows.Forms.ComboBox();
            this.comboBoxAllTime = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.buttonFirst = new System.Windows.Forms.Button();
            this.buttonPrevision = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonEnd = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBoxNetInfo = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.beginButton = new System.Windows.Forms.Button();
            this.startButton = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioFirstBlack = new System.Windows.Forms.RadioButton();
            this.radioFirstWhite = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioBlackDam = new System.Windows.Forms.RadioButton();
            this.radioBlackCheck = new System.Windows.Forms.RadioButton();
            this.radioFull = new System.Windows.Forms.RadioButton();
            this.radioWhiteDam = new System.Windows.Forms.RadioButton();
            this.radioWhiteCheck = new System.Windows.Forms.RadioButton();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сохранитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.загрузитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.играToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StartGameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.RemiMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DeliverMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.BackStepMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TurnMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.StatePositionMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.режимToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UserUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.UserCompMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CompUserMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.CompCompMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.сетьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ConnectToServerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.DisconnectMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.AutorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.timerCmpStep = new System.Windows.Forms.Timer(this.components);
            this.labelMark = new System.Windows.Forms.Label();
            this.deliverButton = new System.Windows.Forms.Button();
            this.backStepButton = new System.Windows.Forms.Button();
            this.disconnectButton = new System.Windows.Forms.Button();
            this.remiButton = new System.Windows.Forms.Button();
            this.turnButton = new System.Windows.Forms.Button();
            this.connectButton = new System.Windows.Forms.Button();
            this.serverButton = new System.Windows.Forms.Button();
            this.startGameButton = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boxTimeWhite
            // 
            this.boxTimeWhite.BackColor = System.Drawing.SystemColors.Window;
            this.boxTimeWhite.CausesValidation = false;
            this.boxTimeWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boxTimeWhite.Location = new System.Drawing.Point(38, 16);
            this.boxTimeWhite.Name = "boxTimeWhite";
            this.boxTimeWhite.ReadOnly = true;
            this.boxTimeWhite.Size = new System.Drawing.Size(66, 20);
            this.boxTimeWhite.TabIndex = 0;
            this.boxTimeWhite.TabStop = false;
            // 
            // boxTimeBlack
            // 
            this.boxTimeBlack.BackColor = System.Drawing.SystemColors.Window;
            this.boxTimeBlack.CausesValidation = false;
            this.boxTimeBlack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.boxTimeBlack.Location = new System.Drawing.Point(38, 42);
            this.boxTimeBlack.Name = "boxTimeBlack";
            this.boxTimeBlack.ReadOnly = true;
            this.boxTimeBlack.Size = new System.Drawing.Size(66, 20);
            this.boxTimeBlack.TabIndex = 1;
            this.boxTimeBlack.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(20, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 400);
            this.panel1.TabIndex = 5;
            this.panel1.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelBlack);
            this.groupBox1.Controls.Add(this.labelWhite);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(547, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(55, 72);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Счёт";
            // 
            // labelBlack
            // 
            this.labelBlack.AutoSize = true;
            this.labelBlack.Location = new System.Drawing.Point(19, 45);
            this.labelBlack.Name = "labelBlack";
            this.labelBlack.Size = new System.Drawing.Size(14, 13);
            this.labelBlack.TabIndex = 13;
            this.labelBlack.Text = "0";
            // 
            // labelWhite
            // 
            this.labelWhite.AutoSize = true;
            this.labelWhite.Location = new System.Drawing.Point(19, 19);
            this.labelWhite.Name = "labelWhite";
            this.labelWhite.Size = new System.Drawing.Size(14, 13);
            this.labelWhite.TabIndex = 14;
            this.labelWhite.Text = "0";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pictureBoxWhite);
            this.groupBox2.Controls.Add(this.pictureBoxBlack);
            this.groupBox2.Controls.Add(this.boxTimeWhite);
            this.groupBox2.Controls.Add(this.boxTimeBlack);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(426, 30);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(115, 72);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Время";
            // 
            // pictureBoxWhite
            // 
            this.pictureBoxWhite.Image = global::Checkers.Properties.Resources.smallWhite;
            this.pictureBoxWhite.Location = new System.Drawing.Point(6, 16);
            this.pictureBoxWhite.Name = "pictureBoxWhite";
            this.pictureBoxWhite.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxWhite.TabIndex = 35;
            this.pictureBoxWhite.TabStop = false;
            // 
            // pictureBoxBlack
            // 
            this.pictureBoxBlack.Image = global::Checkers.Properties.Resources.smallBlack;
            this.pictureBoxBlack.Location = new System.Drawing.Point(6, 42);
            this.pictureBoxBlack.Name = "pictureBoxBlack";
            this.pictureBoxBlack.Size = new System.Drawing.Size(20, 20);
            this.pictureBoxBlack.TabIndex = 36;
            this.pictureBoxBlack.TabStop = false;
            // 
            // HistoryList
            // 
            this.HistoryList.AllowUserToAddRows = false;
            this.HistoryList.AllowUserToDeleteRows = false;
            this.HistoryList.AllowUserToResizeColumns = false;
            this.HistoryList.AllowUserToResizeRows = false;
            this.HistoryList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryList.BackgroundColor = System.Drawing.Color.White;
            this.HistoryList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.HistoryList.CausesValidation = false;
            this.HistoryList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HistoryList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.HistoryList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HistoryList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameUser,
            this.Balance,
            this.Empty});
            this.HistoryList.Location = new System.Drawing.Point(5, 5);
            this.HistoryList.MultiSelect = false;
            this.HistoryList.Name = "HistoryList";
            this.HistoryList.ReadOnly = true;
            this.HistoryList.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.HistoryList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.HistoryList.RowTemplate.Height = 16;
            this.HistoryList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.HistoryList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.HistoryList.Size = new System.Drawing.Size(156, 226);
            this.HistoryList.StandardTab = true;
            this.HistoryList.TabIndex = 13;
            this.HistoryList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.HistoryList_CellMouseClick);
            // 
            // nameUser
            // 
            this.nameUser.HeaderText = "С";
            this.nameUser.Name = "nameUser";
            this.nameUser.ReadOnly = true;
            this.nameUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameUser.Width = 65;
            // 
            // Balance
            // 
            this.Balance.HeaderText = "К";
            this.Balance.Name = "Balance";
            this.Balance.ReadOnly = true;
            this.Balance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Balance.Width = 65;
            // 
            // Empty
            // 
            this.Empty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Empty.HeaderText = "";
            this.Empty.Name = "Empty";
            this.Empty.ReadOnly = true;
            this.Empty.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkRate = 500;
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(20, 439);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(50, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "A";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(70, 439);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "B";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(120, 439);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "C";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(170, 439);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 13);
            this.label9.TabIndex = 22;
            this.label9.Text = "D";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(220, 439);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 13);
            this.label10.TabIndex = 23;
            this.label10.Text = "E";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(270, 439);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(50, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "F";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(320, 439);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(50, 13);
            this.label12.TabIndex = 25;
            this.label12.Text = "G";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(370, 439);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 26;
            this.label13.Text = "H";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(0, 30);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(19, 50);
            this.label14.TabIndex = 27;
            this.label14.Text = "8";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(0, 80);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(19, 50);
            this.label15.TabIndex = 28;
            this.label15.Text = "7";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(0, 130);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(19, 50);
            this.label16.TabIndex = 29;
            this.label16.Text = "6";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(0, 180);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(19, 50);
            this.label17.TabIndex = 30;
            this.label17.Text = "5";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(0, 230);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(19, 50);
            this.label18.TabIndex = 31;
            this.label18.Text = "4";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(0, 280);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(19, 50);
            this.label19.TabIndex = 32;
            this.label19.Text = "3";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(0, 330);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 50);
            this.label20.TabIndex = 33;
            this.label20.Text = "2";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(0, 380);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(19, 50);
            this.label21.TabIndex = 34;
            this.label21.Text = "1";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(143, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "сек.";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(48, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "мин.   +";
            // 
            // comboBoxAddSec
            // 
            this.comboBoxAddSec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAddSec.FormattingEnabled = true;
            this.comboBoxAddSec.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9"});
            this.comboBoxAddSec.Location = new System.Drawing.Point(102, 22);
            this.comboBoxAddSec.Name = "comboBoxAddSec";
            this.comboBoxAddSec.Size = new System.Drawing.Size(40, 21);
            this.comboBoxAddSec.TabIndex = 4;
            this.comboBoxAddSec.Text = "0";
            this.comboBoxAddSec.TextChanged += new System.EventHandler(this.comboBoxAddSec_TextChanged);
            // 
            // comboBoxAllTime
            // 
            this.comboBoxAllTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAllTime.FormattingEnabled = true;
            this.comboBoxAllTime.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "10",
            "20",
            "30",
            "60"});
            this.comboBoxAllTime.Location = new System.Drawing.Point(6, 22);
            this.comboBoxAllTime.MaxDropDownItems = 10;
            this.comboBoxAllTime.Name = "comboBoxAllTime";
            this.comboBoxAllTime.Size = new System.Drawing.Size(40, 21);
            this.comboBoxAllTime.TabIndex = 3;
            this.comboBoxAllTime.Text = "5";
            this.comboBoxAllTime.TextChanged += new System.EventHandler(this.comboBoxAllTime_TextChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.comboBoxAllTime);
            this.groupBox3.Controls.Add(this.comboBoxAddSec);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(426, 412);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(176, 53);
            this.groupBox3.TabIndex = 37;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Контроль времени";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(426, 108);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(176, 299);
            this.tabControl1.TabIndex = 41;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.HistoryList);
            this.tabPage1.Controls.Add(this.buttonFirst);
            this.tabPage1.Controls.Add(this.buttonPrevision);
            this.tabPage1.Controls.Add(this.buttonNext);
            this.tabPage1.Controls.Add(this.buttonEnd);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(168, 273);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "История";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // buttonFirst
            // 
            this.buttonFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFirst.Image = global::Checkers.Properties.Resources.first;
            this.buttonFirst.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonFirst.Location = new System.Drawing.Point(5, 237);
            this.buttonFirst.Name = "buttonFirst";
            this.buttonFirst.Size = new System.Drawing.Size(30, 30);
            this.buttonFirst.TabIndex = 15;
            this.buttonFirst.UseVisualStyleBackColor = true;
            this.buttonFirst.Click += new System.EventHandler(this.buttonFirst_Click);
            // 
            // buttonPrevision
            // 
            this.buttonPrevision.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonPrevision.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPrevision.Image = global::Checkers.Properties.Resources.prevision;
            this.buttonPrevision.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonPrevision.Location = new System.Drawing.Point(46, 237);
            this.buttonPrevision.Name = "buttonPrevision";
            this.buttonPrevision.Size = new System.Drawing.Size(30, 30);
            this.buttonPrevision.TabIndex = 16;
            this.buttonPrevision.UseVisualStyleBackColor = true;
            this.buttonPrevision.Click += new System.EventHandler(this.buttonPrevision_Click);
            // 
            // buttonNext
            // 
            this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNext.Image = global::Checkers.Properties.Resources.next;
            this.buttonNext.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonNext.Location = new System.Drawing.Point(89, 237);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(30, 30);
            this.buttonNext.TabIndex = 17;
            this.buttonNext.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonEnd
            // 
            this.buttonEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonEnd.Image = global::Checkers.Properties.Resources.end;
            this.buttonEnd.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonEnd.Location = new System.Drawing.Point(131, 237);
            this.buttonEnd.Name = "buttonEnd";
            this.buttonEnd.Size = new System.Drawing.Size(30, 30);
            this.buttonEnd.TabIndex = 18;
            this.buttonEnd.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.buttonEnd.UseVisualStyleBackColor = true;
            this.buttonEnd.Click += new System.EventHandler(this.buttonEnd_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textBoxNetInfo);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(168, 273);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Сеть";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBoxNetInfo
            // 
            this.textBoxNetInfo.BackColor = System.Drawing.Color.White;
            this.textBoxNetInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxNetInfo.Location = new System.Drawing.Point(3, 3);
            this.textBoxNetInfo.Multiline = true;
            this.textBoxNetInfo.Name = "textBoxNetInfo";
            this.textBoxNetInfo.ReadOnly = true;
            this.textBoxNetInfo.Size = new System.Drawing.Size(162, 267);
            this.textBoxNetInfo.TabIndex = 0;
            this.textBoxNetInfo.TabStop = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.beginButton);
            this.tabPage3.Controls.Add(this.startButton);
            this.tabPage3.Controls.Add(this.clearButton);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(168, 273);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Расстановка";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // beginButton
            // 
            this.beginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.beginButton.Location = new System.Drawing.Point(85, 214);
            this.beginButton.Name = "beginButton";
            this.beginButton.Size = new System.Drawing.Size(80, 23);
            this.beginButton.TabIndex = 4;
            this.beginButton.Text = "Начальная";
            this.beginButton.UseVisualStyleBackColor = true;
            this.beginButton.Click += new System.EventHandler(this.beginButton_Click);
            // 
            // startButton
            // 
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startButton.Location = new System.Drawing.Point(47, 243);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 5;
            this.startButton.Text = "Старт";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.clearButton.Location = new System.Drawing.Point(3, 214);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(76, 23);
            this.clearButton.TabIndex = 3;
            this.clearButton.Text = "Очистить";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioFirstBlack);
            this.groupBox5.Controls.Add(this.radioFirstWhite);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.Location = new System.Drawing.Point(3, 141);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(162, 68);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Ходят первыми";
            // 
            // radioFirstBlack
            // 
            this.radioFirstBlack.AutoSize = true;
            this.radioFirstBlack.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioFirstBlack.Location = new System.Drawing.Point(6, 42);
            this.radioFirstBlack.Name = "radioFirstBlack";
            this.radioFirstBlack.Size = new System.Drawing.Size(69, 17);
            this.radioFirstBlack.TabIndex = 2;
            this.radioFirstBlack.Text = "красные";
            this.radioFirstBlack.UseVisualStyleBackColor = true;
            // 
            // radioFirstWhite
            // 
            this.radioFirstWhite.AutoSize = true;
            this.radioFirstWhite.Checked = true;
            this.radioFirstWhite.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioFirstWhite.Location = new System.Drawing.Point(6, 19);
            this.radioFirstWhite.Name = "radioFirstWhite";
            this.radioFirstWhite.Size = new System.Drawing.Size(55, 17);
            this.radioFirstWhite.TabIndex = 1;
            this.radioFirstWhite.TabStop = true;
            this.radioFirstWhite.Text = "синие";
            this.radioFirstWhite.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioBlackDam);
            this.groupBox4.Controls.Add(this.radioBlackCheck);
            this.groupBox4.Controls.Add(this.radioFull);
            this.groupBox4.Controls.Add(this.radioWhiteDam);
            this.groupBox4.Controls.Add(this.radioWhiteCheck);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(162, 135);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "На поле";
            // 
            // radioBlackDam
            // 
            this.radioBlackDam.AutoSize = true;
            this.radioBlackDam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioBlackDam.Location = new System.Drawing.Point(6, 87);
            this.radioBlackDam.Name = "radioBlackDam";
            this.radioBlackDam.Size = new System.Drawing.Size(102, 17);
            this.radioBlackDam.TabIndex = 4;
            this.radioBlackDam.Text = "красная дамка";
            this.radioBlackDam.UseVisualStyleBackColor = true;
            // 
            // radioBlackCheck
            // 
            this.radioBlackCheck.AutoSize = true;
            this.radioBlackCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioBlackCheck.Location = new System.Drawing.Point(6, 110);
            this.radioBlackCheck.Name = "radioBlackCheck";
            this.radioBlackCheck.Size = new System.Drawing.Size(104, 17);
            this.radioBlackCheck.TabIndex = 3;
            this.radioBlackCheck.Text = "красная шашка";
            this.radioBlackCheck.UseVisualStyleBackColor = true;
            // 
            // radioFull
            // 
            this.radioFull.AutoSize = true;
            this.radioFull.Checked = true;
            this.radioFull.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioFull.Location = new System.Drawing.Point(6, 64);
            this.radioFull.Name = "radioFull";
            this.radioFull.Size = new System.Drawing.Size(53, 17);
            this.radioFull.TabIndex = 2;
            this.radioFull.TabStop = true;
            this.radioFull.Text = "пусто";
            this.radioFull.UseVisualStyleBackColor = true;
            // 
            // radioWhiteDam
            // 
            this.radioWhiteDam.AutoSize = true;
            this.radioWhiteDam.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioWhiteDam.Location = new System.Drawing.Point(6, 41);
            this.radioWhiteDam.Name = "radioWhiteDam";
            this.radioWhiteDam.Size = new System.Drawing.Size(90, 17);
            this.radioWhiteDam.TabIndex = 1;
            this.radioWhiteDam.Text = "синяя дамка";
            this.radioWhiteDam.UseVisualStyleBackColor = true;
            // 
            // radioWhiteCheck
            // 
            this.radioWhiteCheck.AutoSize = true;
            this.radioWhiteCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioWhiteCheck.Location = new System.Drawing.Point(6, 19);
            this.radioWhiteCheck.Name = "radioWhiteCheck";
            this.radioWhiteCheck.Size = new System.Drawing.Size(92, 17);
            this.radioWhiteCheck.TabIndex = 0;
            this.radioWhiteCheck.Text = "синяя шашка";
            this.radioWhiteCheck.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.играToolStripMenuItem,
            this.режимToolStripMenuItem,
            this.сетьToolStripMenuItem,
            this.информацияToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(608, 24);
            this.menuStrip1.TabIndex = 43;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem,
            this.загрузитьToolStripMenuItem,
            this.ExitMenu});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "&Файл";
            // 
            // сохранитьToolStripMenuItem
            // 
            this.сохранитьToolStripMenuItem.Name = "сохранитьToolStripMenuItem";
            this.сохранитьToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.сохранитьToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.сохранитьToolStripMenuItem.Text = "&Сохранить...";
            this.сохранитьToolStripMenuItem.Click += new System.EventHandler(this.сохранитьToolStripMenuItem_Click);
            // 
            // загрузитьToolStripMenuItem
            // 
            this.загрузитьToolStripMenuItem.Name = "загрузитьToolStripMenuItem";
            this.загрузитьToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.загрузитьToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.загрузитьToolStripMenuItem.Text = "&Загрузить...";
            this.загрузитьToolStripMenuItem.Click += new System.EventHandler(this.загрузитьToolStripMenuItem_Click);
            // 
            // ExitMenu
            // 
            this.ExitMenu.Name = "ExitMenu";
            this.ExitMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.ExitMenu.Size = new System.Drawing.Size(160, 22);
            this.ExitMenu.Text = "&Выход";
            this.ExitMenu.Click += new System.EventHandler(this.ExitMenu_Click);
            // 
            // играToolStripMenuItem
            // 
            this.играToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartGameMenu,
            this.RemiMenu,
            this.DeliverMenu,
            this.BackStepMenu,
            this.TurnMenu,
            this.StatePositionMenu});
            this.играToolStripMenuItem.Name = "играToolStripMenuItem";
            this.играToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.играToolStripMenuItem.Text = "&Игра";
            // 
            // StartGameMenu
            // 
            this.StartGameMenu.Name = "StartGameMenu";
            this.StartGameMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.StartGameMenu.Size = new System.Drawing.Size(213, 22);
            this.StartGameMenu.Text = "&Начать игру";
            this.StartGameMenu.Click += new System.EventHandler(this.button1_Click);
            // 
            // RemiMenu
            // 
            this.RemiMenu.Enabled = false;
            this.RemiMenu.Name = "RemiMenu";
            this.RemiMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.RemiMenu.Size = new System.Drawing.Size(213, 22);
            this.RemiMenu.Text = "&Предложить ничью";
            this.RemiMenu.Click += new System.EventHandler(this.button6_Click);
            // 
            // DeliverMenu
            // 
            this.DeliverMenu.Enabled = false;
            this.DeliverMenu.Name = "DeliverMenu";
            this.DeliverMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.DeliverMenu.Size = new System.Drawing.Size(213, 22);
            this.DeliverMenu.Text = "&Сдаться";
            this.DeliverMenu.Click += new System.EventHandler(this.button5_Click);
            // 
            // BackStepMenu
            // 
            this.BackStepMenu.Enabled = false;
            this.BackStepMenu.Name = "BackStepMenu";
            this.BackStepMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.BackStepMenu.Size = new System.Drawing.Size(213, 22);
            this.BackStepMenu.Text = "Отменить ход";
            this.BackStepMenu.Click += new System.EventHandler(this.BackStepMenu_Click);
            // 
            // TurnMenu
            // 
            this.TurnMenu.Name = "TurnMenu";
            this.TurnMenu.Size = new System.Drawing.Size(213, 22);
            this.TurnMenu.Text = "&Перевернуть доску";
            this.TurnMenu.Click += new System.EventHandler(this.button4_Click);
            // 
            // StatePositionMenu
            // 
            this.StatePositionMenu.Name = "StatePositionMenu";
            this.StatePositionMenu.Size = new System.Drawing.Size(213, 22);
            this.StatePositionMenu.Text = "Расстановка позиции";
            this.StatePositionMenu.Click += new System.EventHandler(this.StatePositionMenu_Click);
            // 
            // режимToolStripMenuItem
            // 
            this.режимToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserUserMenu,
            this.UserCompMenu,
            this.CompUserMenu,
            this.CompCompMenu});
            this.режимToolStripMenuItem.Name = "режимToolStripMenuItem";
            this.режимToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.режимToolStripMenuItem.Text = "&Режим";
            // 
            // UserUserMenu
            // 
            this.UserUserMenu.Checked = true;
            this.UserUserMenu.CheckOnClick = true;
            this.UserUserMenu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.UserUserMenu.Name = "UserUserMenu";
            this.UserUserMenu.Size = new System.Drawing.Size(194, 22);
            this.UserUserMenu.Text = "Ч&еловек-Человек";
            this.UserUserMenu.CheckedChanged += new System.EventHandler(this.UserUserMenu_CheckedChanged);
            // 
            // UserCompMenu
            // 
            this.UserCompMenu.CheckOnClick = true;
            this.UserCompMenu.Name = "UserCompMenu";
            this.UserCompMenu.Size = new System.Drawing.Size(194, 22);
            this.UserCompMenu.Text = "&Человек-Компьютер";
            this.UserCompMenu.CheckedChanged += new System.EventHandler(this.UserCompMenu_CheckedChanged);
            // 
            // CompUserMenu
            // 
            this.CompUserMenu.CheckOnClick = true;
            this.CompUserMenu.Name = "CompUserMenu";
            this.CompUserMenu.Size = new System.Drawing.Size(194, 22);
            this.CompUserMenu.Text = "&Компьютер-Человек";
            this.CompUserMenu.CheckedChanged += new System.EventHandler(this.CompUserMenu_CheckedChanged);
            // 
            // CompCompMenu
            // 
            this.CompCompMenu.CheckOnClick = true;
            this.CompCompMenu.Name = "CompCompMenu";
            this.CompCompMenu.Size = new System.Drawing.Size(194, 22);
            this.CompCompMenu.Text = "К&омпьютер-Компьютер";
            this.CompCompMenu.CheckedChanged += new System.EventHandler(this.CompCompMenu_CheckedChanged);
            // 
            // сетьToolStripMenuItem
            // 
            this.сетьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ServerMenu,
            this.ConnectToServerMenu,
            this.DisconnectMenu});
            this.сетьToolStripMenuItem.Name = "сетьToolStripMenuItem";
            this.сетьToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.сетьToolStripMenuItem.Text = "&Сеть";
            // 
            // ServerMenu
            // 
            this.ServerMenu.Name = "ServerMenu";
            this.ServerMenu.Size = new System.Drawing.Size(215, 22);
            this.ServerMenu.Text = "&Установить сервер";
            this.ServerMenu.Click += new System.EventHandler(this.button2_Click);
            // 
            // ConnectToServerMenu
            // 
            this.ConnectToServerMenu.Name = "ConnectToServerMenu";
            this.ConnectToServerMenu.Size = new System.Drawing.Size(215, 22);
            this.ConnectToServerMenu.Text = "&Подключиться к серверу...";
            this.ConnectToServerMenu.Click += new System.EventHandler(this.button3_Click);
            // 
            // DisconnectMenu
            // 
            this.DisconnectMenu.Enabled = false;
            this.DisconnectMenu.Name = "DisconnectMenu";
            this.DisconnectMenu.Size = new System.Drawing.Size(215, 22);
            this.DisconnectMenu.Text = "&Разорвать соединение";
            this.DisconnectMenu.Click += new System.EventHandler(this.button7_Click);
            // 
            // информацияToolStripMenuItem
            // 
            this.информацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenu,
            this.AutorMenu});
            this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
            this.информацияToolStripMenuItem.Size = new System.Drawing.Size(82, 20);
            this.информацияToolStripMenuItem.Text = "&Информация";
            // 
            // AboutMenu
            // 
            this.AboutMenu.Name = "AboutMenu";
            this.AboutMenu.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.AboutMenu.Size = new System.Drawing.Size(169, 22);
            this.AboutMenu.Text = "&О программе...";
            this.AboutMenu.Click += new System.EventHandler(this.AboutMenu_Click);
            // 
            // AutorMenu
            // 
            this.AutorMenu.Name = "AutorMenu";
            this.AutorMenu.Size = new System.Drawing.Size(169, 22);
            this.AutorMenu.Text = "Об &авторе";
            this.AutorMenu.Click += new System.EventHandler(this.AutorMenu_Click);
            // 
            // timerCmpStep
            // 
            this.timerCmpStep.Interval = 500;
            this.timerCmpStep.Tick += new System.EventHandler(this.timerCmpStep_Tick);
            // 
            // labelMark
            // 
            this.labelMark.AutoSize = true;
            this.labelMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelMark.Location = new System.Drawing.Point(423, 468);
            this.labelMark.Name = "labelMark";
            this.labelMark.Size = new System.Drawing.Size(0, 13);
            this.labelMark.TabIndex = 49;
            // 
            // deliverButton
            // 
            this.deliverButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.deliverButton.Image = global::Checkers.Properties.Resources.stop_2;
            this.deliverButton.Location = new System.Drawing.Point(373, 461);
            this.deliverButton.Name = "deliverButton";
            this.deliverButton.Size = new System.Drawing.Size(40, 40);
            this.deliverButton.TabIndex = 39;
            this.deliverButton.UseVisualStyleBackColor = true;
            this.deliverButton.Visible = false;
            this.deliverButton.MouseLeave += new System.EventHandler(this.button5_MouseLeave);
            this.deliverButton.Click += new System.EventHandler(this.button5_Click);
            this.deliverButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.deliverButton.MouseEnter += new System.EventHandler(this.button5_MouseEnter);
            // 
            // backStepButton
            // 
            this.backStepButton.Enabled = false;
            this.backStepButton.Image = global::Checkers.Properties.Resources.undo;
            this.backStepButton.Location = new System.Drawing.Point(226, 460);
            this.backStepButton.Name = "backStepButton";
            this.backStepButton.Size = new System.Drawing.Size(40, 40);
            this.backStepButton.TabIndex = 44;
            this.backStepButton.UseVisualStyleBackColor = true;
            this.backStepButton.MouseLeave += new System.EventHandler(this.backStepButton_MouseLeave);
            this.backStepButton.Click += new System.EventHandler(this.BackStepMenu_Click);
            this.backStepButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.backStepButton.MouseEnter += new System.EventHandler(this.backStepButton_MouseEnter);
            // 
            // disconnectButton
            // 
            this.disconnectButton.Image = global::Checkers.Properties.Resources.connectDel;
            this.disconnectButton.Location = new System.Drawing.Point(20, 461);
            this.disconnectButton.Name = "disconnectButton";
            this.disconnectButton.Size = new System.Drawing.Size(40, 40);
            this.disconnectButton.TabIndex = 42;
            this.disconnectButton.UseVisualStyleBackColor = true;
            this.disconnectButton.Visible = false;
            this.disconnectButton.MouseLeave += new System.EventHandler(this.button7_MouseLeave);
            this.disconnectButton.Click += new System.EventHandler(this.button7_Click);
            this.disconnectButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.disconnectButton.MouseEnter += new System.EventHandler(this.button7_MouseEnter);
            // 
            // remiButton
            // 
            this.remiButton.Image = global::Checkers.Properties.Resources.remi;
            this.remiButton.Location = new System.Drawing.Point(322, 461);
            this.remiButton.Name = "remiButton";
            this.remiButton.Size = new System.Drawing.Size(40, 40);
            this.remiButton.TabIndex = 40;
            this.remiButton.UseVisualStyleBackColor = true;
            this.remiButton.Visible = false;
            this.remiButton.MouseLeave += new System.EventHandler(this.button6_MouseLeave);
            this.remiButton.Click += new System.EventHandler(this.button6_Click);
            this.remiButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.remiButton.MouseEnter += new System.EventHandler(this.button6_MouseEnter);
            // 
            // turnButton
            // 
            this.turnButton.Image = global::Checkers.Properties.Resources.changeSide;
            this.turnButton.Location = new System.Drawing.Point(175, 460);
            this.turnButton.Name = "turnButton";
            this.turnButton.Size = new System.Drawing.Size(40, 40);
            this.turnButton.TabIndex = 38;
            this.turnButton.UseVisualStyleBackColor = true;
            this.turnButton.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            this.turnButton.Click += new System.EventHandler(this.button4_Click);
            this.turnButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.turnButton.MouseEnter += new System.EventHandler(this.button4_MouseEnter);
            // 
            // connectButton
            // 
            this.connectButton.Image = global::Checkers.Properties.Resources.connect;
            this.connectButton.Location = new System.Drawing.Point(71, 461);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(40, 40);
            this.connectButton.TabIndex = 36;
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
            this.connectButton.Click += new System.EventHandler(this.button3_Click);
            this.connectButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.connectButton.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
            // 
            // serverButton
            // 
            this.serverButton.Image = global::Checkers.Properties.Resources.setServer;
            this.serverButton.Location = new System.Drawing.Point(20, 461);
            this.serverButton.Name = "serverButton";
            this.serverButton.Size = new System.Drawing.Size(40, 40);
            this.serverButton.TabIndex = 35;
            this.serverButton.UseVisualStyleBackColor = true;
            this.serverButton.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
            this.serverButton.Click += new System.EventHandler(this.button2_Click);
            this.serverButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.serverButton.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
            // 
            // startGameButton
            // 
            this.startGameButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startGameButton.Image = global::Checkers.Properties.Resources.start_2;
            this.startGameButton.Location = new System.Drawing.Point(373, 461);
            this.startGameButton.Name = "startGameButton";
            this.startGameButton.Size = new System.Drawing.Size(40, 40);
            this.startGameButton.TabIndex = 2;
            this.startGameButton.UseVisualStyleBackColor = true;
            this.startGameButton.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            this.startGameButton.Click += new System.EventHandler(this.button1_Click);
            this.startGameButton.Enter += new System.EventHandler(this.Drop_Focus);
            this.startGameButton.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 508);
            this.Controls.Add(this.labelMark);
            this.Controls.Add(this.deliverButton);
            this.Controls.Add(this.backStepButton);
            this.Controls.Add(this.disconnectButton);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.remiButton);
            this.Controls.Add(this.turnButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.connectButton);
            this.Controls.Add(this.serverButton);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.startGameButton);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Шашки";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWhite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxBlack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox boxTimeWhite;
		private System.Windows.Forms.TextBox boxTimeBlack;
		private System.Windows.Forms.Button startGameButton;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label labelBlack;
		private System.Windows.Forms.Label labelWhite;
		private System.Windows.Forms.GroupBox groupBox2;		
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Button buttonEnd;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Button buttonPrevision;
		private System.Windows.Forms.Button buttonFirst;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button serverButton;		
		private System.Windows.Forms.Button deliverButton;
		private System.Windows.Forms.Button turnButton;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.ComboBox comboBoxAddSec;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolTip toolTip1;
		public System.Windows.Forms.PictureBox pictureBoxWhite;
		public System.Windows.Forms.PictureBox pictureBoxBlack;
		private System.Windows.Forms.Button remiButton;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button disconnectButton;
		public System.Windows.Forms.ComboBox comboBoxAllTime;
		public System.Windows.Forms.TextBox textBoxNetInfo;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitMenu;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameUser;
		private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
		private System.Windows.Forms.DataGridViewTextBoxColumn Empty;
		private System.Windows.Forms.ToolStripMenuItem играToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem сетьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem StartGameMenu;
		private System.Windows.Forms.ToolStripMenuItem RemiMenu;
		private System.Windows.Forms.ToolStripMenuItem DeliverMenu;
		private System.Windows.Forms.ToolStripMenuItem TurnMenu;
		private System.Windows.Forms.ToolStripMenuItem ServerMenu;
		private System.Windows.Forms.ToolStripMenuItem ConnectToServerMenu;
		private System.Windows.Forms.ToolStripMenuItem DisconnectMenu;
		private System.Windows.Forms.ToolStripMenuItem AboutMenu;
		private System.Windows.Forms.ToolStripMenuItem AutorMenu;
		private System.Windows.Forms.DataGridView HistoryList;
		private System.Windows.Forms.ToolStripMenuItem BackStepMenu;
		private System.Windows.Forms.RadioButton radioWhiteCheck;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton radioFirstBlack;
		private System.Windows.Forms.RadioButton radioFirstWhite;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.RadioButton radioBlackDam;
		private System.Windows.Forms.RadioButton radioBlackCheck;
		private System.Windows.Forms.RadioButton radioFull;
		private System.Windows.Forms.RadioButton radioWhiteDam;
		private System.Windows.Forms.Button beginButton;
		private System.Windows.Forms.Button startButton;
		private System.Windows.Forms.Button clearButton;
		public System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.ToolStripMenuItem StatePositionMenu;
		private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem загрузитьToolStripMenuItem;
    private System.Windows.Forms.Button backStepButton;
    private System.Windows.Forms.ToolStripMenuItem режимToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem UserUserMenu;
    private System.Windows.Forms.ToolStripMenuItem UserCompMenu;
    private System.Windows.Forms.ToolStripMenuItem CompUserMenu;
    private System.Windows.Forms.ToolStripMenuItem CompCompMenu;
    private System.Windows.Forms.Timer timerCmpStep;
    private System.Windows.Forms.Label labelMark;


	}
}

