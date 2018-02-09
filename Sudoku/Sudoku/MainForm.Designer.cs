namespace Sudoku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonFocus = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.labelLevel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAction = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuActionUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuActionRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuActionCheck = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuActionSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuActionSolveSudoku = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.panelHide = new System.Windows.Forms.Panel();
            this.labelTimeInfo = new System.Windows.Forms.Label();
            this.labelTime = new System.Windows.Forms.Label();
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.buttonPause = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonClear = new System.Windows.Forms.Button();
            this.buttonSolution = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(498, 218);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(87, 35);
            this.buttonOpen.TabIndex = 12;
            this.buttonOpen.TabStop = false;
            this.buttonOpen.Text = "Загрузить уровень";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Visible = false;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            this.buttonOpen.Enter += new System.EventHandler(this.Drop_Focus);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Sudoku files|*.pos";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Sudoku files|*.pos";
            // 
            // buttonFocus
            // 
            this.buttonFocus.Location = new System.Drawing.Point(178, 182);
            this.buttonFocus.Name = "buttonFocus";
            this.buttonFocus.Size = new System.Drawing.Size(75, 23);
            this.buttonFocus.TabIndex = 13;
            this.buttonFocus.Text = "buttonFocus";
            this.buttonFocus.UseVisualStyleBackColor = true;
            // 
            // labelInfo
            // 
            this.labelInfo.Location = new System.Drawing.Point(473, 58);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(134, 147);
            this.labelInfo.TabIndex = 15;
            this.labelInfo.Text = "Нажмите кнопку \"Решение\" для выбора судоку для разгадывания.\r\n\r\nНажмите кнопку \"С" +
                "оздание\" для создания нового судоку.";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // labelLevel
            // 
            this.labelLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelLevel.Location = new System.Drawing.Point(498, 260);
            this.labelLevel.Name = "labelLevel";
            this.labelLevel.Size = new System.Drawing.Size(87, 18);
            this.labelLevel.TabIndex = 20;
            this.labelLevel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelLevel.Visible = false;
            this.labelLevel.MouseLeave += new System.EventHandler(this.labelLevel_MouseLeave);
            this.labelLevel.MouseEnter += new System.EventHandler(this.labelLevel_MouseEnter);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuAction,
            this.MenuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(616, 24);
            this.menuStrip1.TabIndex = 21;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFilePrint,
            this.MenuFileExit});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(45, 20);
            this.MenuFile.Text = "&Файл";
            // 
            // MenuFilePrint
            // 
            this.MenuFilePrint.Name = "MenuFilePrint";
            this.MenuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.MenuFilePrint.Size = new System.Drawing.Size(172, 22);
            this.MenuFilePrint.Text = "Печать...";
            this.MenuFilePrint.Click += new System.EventHandler(this.MenuFilePrint_Click);
            // 
            // MenuFileExit
            // 
            this.MenuFileExit.Name = "MenuFileExit";
            this.MenuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.MenuFileExit.Size = new System.Drawing.Size(172, 22);
            this.MenuFileExit.Text = "&Выход";
            this.MenuFileExit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // MenuAction
            // 
            this.MenuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuActionUndo,
            this.MenuActionRedo,
            this.MenuActionCheck,
            this.MenuActionSettings,
            this.MenuActionSolveSudoku});
            this.MenuAction.Name = "MenuAction";
            this.MenuAction.Size = new System.Drawing.Size(68, 20);
            this.MenuAction.Text = "&Действия";
            // 
            // MenuActionUndo
            // 
            this.MenuActionUndo.Name = "MenuActionUndo";
            this.MenuActionUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuActionUndo.Size = new System.Drawing.Size(207, 22);
            this.MenuActionUndo.Text = "Отменить ход";
            this.MenuActionUndo.Click += new System.EventHandler(this.MenuActionUndo_Click);
            // 
            // MenuActionRedo
            // 
            this.MenuActionRedo.Name = "MenuActionRedo";
            this.MenuActionRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.MenuActionRedo.Size = new System.Drawing.Size(207, 22);
            this.MenuActionRedo.Text = "Вернуть ход";
            this.MenuActionRedo.Click += new System.EventHandler(this.MenuActionRedo_Click);
            // 
            // MenuActionCheck
            // 
            this.MenuActionCheck.Name = "MenuActionCheck";
            this.MenuActionCheck.Size = new System.Drawing.Size(207, 22);
            this.MenuActionCheck.Text = "&Проверить расстановку";
            this.MenuActionCheck.Click += new System.EventHandler(this.check_Click);
            // 
            // MenuActionSettings
            // 
            this.MenuActionSettings.Name = "MenuActionSettings";
            this.MenuActionSettings.Size = new System.Drawing.Size(207, 22);
            this.MenuActionSettings.Text = "&Настройка...";
            this.MenuActionSettings.Click += new System.EventHandler(this.MenuActionSettings_Click);
            // 
            // MenuActionSolveSudoku
            // 
            this.MenuActionSolveSudoku.Name = "MenuActionSolveSudoku";
            this.MenuActionSolveSudoku.Size = new System.Drawing.Size(207, 22);
            this.MenuActionSolveSudoku.Text = "&Решить";
            this.MenuActionSolveSudoku.Click += new System.EventHandler(this.MenuActionSolveSudoku_Click);
            // 
            // MenuHelp
            // 
            this.MenuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuHelpHelp,
            this.MenuHelpAbout});
            this.MenuHelp.Name = "MenuHelp";
            this.MenuHelp.Size = new System.Drawing.Size(82, 20);
            this.MenuHelp.Text = "&Информация";
            // 
            // MenuHelpHelp
            // 
            this.MenuHelpHelp.Name = "MenuHelpHelp";
            this.MenuHelpHelp.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.MenuHelpHelp.Size = new System.Drawing.Size(161, 22);
            this.MenuHelpHelp.Text = "&Помощь...";
            this.MenuHelpHelp.Click += new System.EventHandler(this.MenuHelpHelp_Click);
            // 
            // MenuHelpAbout
            // 
            this.MenuHelpAbout.Name = "MenuHelpAbout";
            this.MenuHelpAbout.Size = new System.Drawing.Size(161, 22);
            this.MenuHelpAbout.Text = "&О программе...";
            this.MenuHelpAbout.Click += new System.EventHandler(this.MenuHelpAbout_Click);
            // 
            // panelHide
            // 
            this.panelHide.Location = new System.Drawing.Point(10, 32);
            this.panelHide.Name = "panelHide";
            this.panelHide.Size = new System.Drawing.Size(457, 461);
            this.panelHide.TabIndex = 22;
            // 
            // labelTimeInfo
            // 
            this.labelTimeInfo.AutoSize = true;
            this.labelTimeInfo.Location = new System.Drawing.Point(521, 300);
            this.labelTimeInfo.Name = "labelTimeInfo";
            this.labelTimeInfo.Size = new System.Drawing.Size(40, 13);
            this.labelTimeInfo.TabIndex = 23;
            this.labelTimeInfo.Text = "Время";
            this.labelTimeInfo.Visible = false;
            // 
            // labelTime
            // 
            this.labelTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelTime.Location = new System.Drawing.Point(509, 317);
            this.labelTime.Name = "labelTime";
            this.labelTime.Size = new System.Drawing.Size(72, 18);
            this.labelTime.TabIndex = 24;
            this.labelTime.Text = "00:00:00";
            this.labelTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelTime.Visible = false;
            // 
            // timerTime
            // 
            this.timerTime.Tick += new System.EventHandler(this.timerTime_Tick);
            // 
            // buttonPause
            // 
            this.buttonPause.Location = new System.Drawing.Point(508, 341);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(72, 23);
            this.buttonPause.TabIndex = 25;
            this.buttonPause.TabStop = false;
            this.buttonPause.Text = "Пауза";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Visible = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            this.buttonPause.Enter += new System.EventHandler(this.Drop_Focus);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(498, 233);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(87, 35);
            this.buttonClear.TabIndex = 26;
            this.buttonClear.TabStop = false;
            this.buttonClear.Text = "Очистить поле";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Visible = false;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            this.buttonClear.Enter += new System.EventHandler(this.Drop_Focus);
            // 
            // buttonSolution
            // 
            this.buttonSolution.Location = new System.Drawing.Point(473, 32);
            this.buttonSolution.Name = "buttonSolution";
            this.buttonSolution.Size = new System.Drawing.Size(64, 23);
            this.buttonSolution.TabIndex = 27;
            this.buttonSolution.Text = "Решение";
            this.buttonSolution.UseVisualStyleBackColor = true;
            this.buttonSolution.Click += new System.EventHandler(this.buttonSolution_Click);
            this.buttonSolution.Enter += new System.EventHandler(this.Drop_Focus);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(543, 32);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(64, 23);
            this.buttonCreate.TabIndex = 28;
            this.buttonCreate.Text = "Создание";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            this.buttonCreate.Enter += new System.EventHandler(this.Drop_Focus);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(508, 400);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(72, 23);
            this.buttonSave.TabIndex = 29;
            this.buttonSave.TabStop = false;
            this.buttonSave.Text = "Сохранить";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Visible = false;
            this.buttonSave.Click += new System.EventHandler(this.Save_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 497);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonSolution);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.labelTime);
            this.Controls.Add(this.labelTimeInfo);
            this.Controls.Add(this.panelHide);
            this.Controls.Add(this.labelLevel);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonFocus);
            this.Controls.Add(this.buttonOpen);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Игра судоку";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonFocus;
        private System.Windows.Forms.Label labelInfo;
        public System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label labelLevel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuFileExit;
        private System.Windows.Forms.ToolStripMenuItem MenuAction;
        private System.Windows.Forms.ToolStripMenuItem MenuActionCheck;
        private System.Windows.Forms.ToolStripMenuItem MenuActionSettings;
        private System.Windows.Forms.Panel panelHide;
        private System.Windows.Forms.Label labelTimeInfo;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.Timer timerTime;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.ToolStripMenuItem MenuHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpHelp;
        private System.Windows.Forms.ToolStripMenuItem MenuHelpAbout;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Button buttonSolution;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ToolStripMenuItem MenuActionSolveSudoku;
        private System.Windows.Forms.ToolStripMenuItem MenuActionUndo;
        private System.Windows.Forms.ToolStripMenuItem MenuActionRedo;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.ToolStripMenuItem MenuFilePrint;

    }
}

