namespace TetrisOOP
{
  partial class Form1
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
        this.pictureBoard = new System.Windows.Forms.PictureBox();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
        this.создательУровнейToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.правилаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
        this.buttonPause = new System.Windows.Forms.Button();
        this.pictureNextFigure = new System.Windows.Forms.PictureBox();
        this.labelScore = new System.Windows.Forms.Label();
        this.labelSpeed = new System.Windows.Forms.Label();
        this.timerDown = new System.Windows.Forms.Timer(this.components);
        this.buttonStart = new System.Windows.Forms.Button();
        this.groupTypeGame = new System.Windows.Forms.GroupBox();
        this.radioGame = new System.Windows.Forms.RadioButton();
        this.radioTrening = new System.Windows.Forms.RadioButton();
        this.labelLevel = new System.Windows.Forms.Label();
        this.panel1 = new System.Windows.Forms.Panel();
        this.buttonFocus = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoard)).BeginInit();
        this.menuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureNextFigure)).BeginInit();
        this.groupTypeGame.SuspendLayout();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // pictureBoard
        // 
        this.pictureBoard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureBoard.Location = new System.Drawing.Point(2, 27);
        this.pictureBoard.Name = "pictureBoard";
        this.pictureBoard.Size = new System.Drawing.Size(200, 400);
        this.pictureBoard.TabIndex = 0;
        this.pictureBoard.TabStop = false;
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(306, 24);
        this.menuStrip1.TabIndex = 1;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // menuFile
        // 
        this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.создательУровнейToolStripMenuItem,
            this.правилаToolStripMenuItem,
            this.menuFileExit});
        this.menuFile.Name = "menuFile";
        this.menuFile.Size = new System.Drawing.Size(45, 20);
        this.menuFile.Text = "&Файл";
        // 
        // создательУровнейToolStripMenuItem
        // 
        this.создательУровнейToolStripMenuItem.Name = "создательУровнейToolStripMenuItem";
        this.создательУровнейToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
        this.создательУровнейToolStripMenuItem.Text = "Создатель уровней";
        this.создательУровнейToolStripMenuItem.Click += new System.EventHandler(this.создательУровнейToolStripMenuItem_Click);
        // 
        // правилаToolStripMenuItem
        // 
        this.правилаToolStripMenuItem.Name = "правилаToolStripMenuItem";
        this.правилаToolStripMenuItem.Size = new System.Drawing.Size(185, 22);
        this.правилаToolStripMenuItem.Text = "&Правила";
        this.правилаToolStripMenuItem.Click += new System.EventHandler(this.правилаToolStripMenuItem_Click);
        // 
        // menuFileExit
        // 
        this.menuFileExit.Name = "menuFileExit";
        this.menuFileExit.Size = new System.Drawing.Size(185, 22);
        this.menuFileExit.Text = "&Выход";
        this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
        // 
        // buttonPause
        // 
        this.buttonPause.Enabled = false;
        this.buttonPause.Location = new System.Drawing.Point(208, 404);
        this.buttonPause.Name = "buttonPause";
        this.buttonPause.Size = new System.Drawing.Size(94, 23);
        this.buttonPause.TabIndex = 2;
        this.buttonPause.TabStop = false;
        this.buttonPause.Text = "Пауза";
        this.buttonPause.UseVisualStyleBackColor = true;
        this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);        
        this.buttonPause.Enter += new System.EventHandler(this.Drop_Focus);
        // 
        // pictureNextFigure
        // 
        this.pictureNextFigure.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        this.pictureNextFigure.Location = new System.Drawing.Point(214, 27);
        this.pictureNextFigure.Name = "pictureNextFigure";
        this.pictureNextFigure.Size = new System.Drawing.Size(80, 80);
        this.pictureNextFigure.TabIndex = 3;
        this.pictureNextFigure.TabStop = false;
        // 
        // labelScore
        // 
        this.labelScore.AutoSize = true;
        this.labelScore.Location = new System.Drawing.Point(213, 116);
        this.labelScore.Name = "labelScore";
        this.labelScore.Size = new System.Drawing.Size(35, 13);
        this.labelScore.TabIndex = 4;
        this.labelScore.Text = "Очки:";
        // 
        // labelSpeed
        // 
        this.labelSpeed.AutoSize = true;
        this.labelSpeed.Location = new System.Drawing.Point(213, 152);
        this.labelSpeed.Name = "labelSpeed";
        this.labelSpeed.Size = new System.Drawing.Size(58, 13);
        this.labelSpeed.TabIndex = 5;
        this.labelSpeed.Text = "Скорость:";
        // 
        // timerDown
        // 
        this.timerDown.Interval = 1000;
        this.timerDown.Tick += new System.EventHandler(this.timer_Tick);
        // 
        // buttonStart
        // 
        this.buttonStart.Location = new System.Drawing.Point(208, 370);
        this.buttonStart.Name = "buttonStart";
        this.buttonStart.Size = new System.Drawing.Size(94, 23);
        this.buttonStart.TabIndex = 6;
        this.buttonStart.TabStop = false;
        this.buttonStart.Text = "Старт";
        this.buttonStart.UseVisualStyleBackColor = true;
        this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);        
        this.buttonStart.Enter += new System.EventHandler(this.Drop_Focus);
        // 
        // groupTypeGame
        // 
        this.groupTypeGame.Controls.Add(this.radioGame);
        this.groupTypeGame.Controls.Add(this.radioTrening);
        this.groupTypeGame.Location = new System.Drawing.Point(209, 300);
        this.groupTypeGame.Name = "groupTypeGame";
        this.groupTypeGame.Size = new System.Drawing.Size(93, 57);
        this.groupTypeGame.TabIndex = 7;
        this.groupTypeGame.TabStop = false;
        // 
        // radioGame
        // 
        this.radioGame.AutoSize = true;
        this.radioGame.Location = new System.Drawing.Point(3, 32);
        this.radioGame.Name = "radioGame";
        this.radioGame.Size = new System.Drawing.Size(50, 17);
        this.radioGame.TabIndex = 1;
        this.radioGame.Text = "Игра";
        this.radioGame.UseVisualStyleBackColor = true;
        // 
        // radioTrening
        // 
        this.radioTrening.AutoSize = true;
        this.radioTrening.Checked = true;
        this.radioTrening.Location = new System.Drawing.Point(4, 11);
        this.radioTrening.Name = "radioTrening";
        this.radioTrening.Size = new System.Drawing.Size(86, 17);
        this.radioTrening.TabIndex = 0;
        this.radioTrening.TabStop = true;
        this.radioTrening.Text = "Тренировка";
        this.radioTrening.UseVisualStyleBackColor = true;
        // 
        // labelLevel
        // 
        this.labelLevel.AutoSize = true;
        this.labelLevel.Location = new System.Drawing.Point(213, 185);
        this.labelLevel.Name = "labelLevel";
        this.labelLevel.Size = new System.Drawing.Size(54, 13);
        this.labelLevel.TabIndex = 8;
        this.labelLevel.Text = "Уровень:";
        this.labelLevel.Visible = false;
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.buttonFocus);
        this.panel1.Location = new System.Drawing.Point(33, 176);
        this.panel1.Name = "panel1";
        this.panel1.Size = new System.Drawing.Size(94, 56);
        this.panel1.TabIndex = 9;
        // 
        // buttonFocus
        // 
        this.buttonFocus.Location = new System.Drawing.Point(29, 13);
        this.buttonFocus.Name = "buttonFocus";
        this.buttonFocus.Size = new System.Drawing.Size(34, 23);
        this.buttonFocus.TabIndex = 0;
        this.buttonFocus.UseVisualStyleBackColor = true;
        this.buttonFocus.KeyUp += new System.Windows.Forms.KeyEventHandler(this.buttonFocus_KeyUp);
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(306, 433);
        this.Controls.Add(this.labelLevel);
        this.Controls.Add(this.groupTypeGame);
        this.Controls.Add(this.buttonStart);
        this.Controls.Add(this.labelSpeed);
        this.Controls.Add(this.labelScore);
        this.Controls.Add(this.pictureNextFigure);
        this.Controls.Add(this.buttonPause);
        this.Controls.Add(this.pictureBoard);
        this.Controls.Add(this.menuStrip1);
        this.Controls.Add(this.panel1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.MainMenuStrip = this.menuStrip1;
        this.MaximizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Tetris";        
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        ((System.ComponentModel.ISupportInitialize)(this.pictureBoard)).EndInit();
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.pictureNextFigure)).EndInit();
        this.groupTypeGame.ResumeLayout(false);
        this.groupTypeGame.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.PictureBox pictureBoard;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem menuFile;
    private System.Windows.Forms.ToolStripMenuItem menuFileExit;
    private System.Windows.Forms.Button buttonPause;
    private System.Windows.Forms.PictureBox pictureNextFigure;
    private System.Windows.Forms.Label labelScore;
    private System.Windows.Forms.Label labelSpeed;
    private System.Windows.Forms.Timer timerDown;
    private System.Windows.Forms.Button buttonStart;
    private System.Windows.Forms.ToolStripMenuItem правилаToolStripMenuItem;
    private System.Windows.Forms.GroupBox groupTypeGame;
    private System.Windows.Forms.RadioButton radioGame;
    private System.Windows.Forms.RadioButton radioTrening;
    private System.Windows.Forms.Label labelLevel;
    private System.Windows.Forms.ToolStripMenuItem создательУровнейToolStripMenuItem;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button buttonFocus;
  }
}

