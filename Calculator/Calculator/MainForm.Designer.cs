namespace Calculator
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.textOperation = new System.Windows.Forms.TextBox();
        this.textResult = new System.Windows.Forms.TextBox();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.radioButton_16 = new System.Windows.Forms.RadioButton();
        this.radioButton_10 = new System.Windows.Forms.RadioButton();
        this.radioButton_8 = new System.Windows.Forms.RadioButton();
        this.radioButton_2 = new System.Windows.Forms.RadioButton();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.List = new System.Windows.Forms.DataGridView();
        this.nameUser = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Balance = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.очиститьСписокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.взятьРезультатToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.взятьОперациюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenu_FileAllNotationResults = new System.Windows.Forms.ToolStripMenuItem();
        this.oolStripMenu_FileExit = new System.Windows.Forms.ToolStripMenuItem();
        this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripMenu_InformationAbout = new System.Windows.Forms.ToolStripMenuItem();
        this.buttonShowHistory = new System.Windows.Forms.Button();
        this.buttonExit = new System.Windows.Forms.Button();
        this.buttonRun = new System.Windows.Forms.Button();
        this.timer1 = new System.Windows.Forms.Timer(this.components);
        this.groupBox1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.List)).BeginInit();
        this.contextMenuStrip1.SuspendLayout();
        this.menuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // textOperation
        // 
        this.textOperation.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.textOperation.Location = new System.Drawing.Point(2, 33);
        this.textOperation.Name = "textOperation";
        this.textOperation.Size = new System.Drawing.Size(345, 22);
        this.textOperation.TabIndex = 0;
        // 
        // textResult
        // 
        this.textResult.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        this.textResult.Location = new System.Drawing.Point(2, 79);
        this.textResult.Name = "textResult";
        this.textResult.ReadOnly = true;
        this.textResult.Size = new System.Drawing.Size(345, 22);
        this.textResult.TabIndex = 2;
        this.textResult.TabStop = false;
        this.textResult.MouseLeave += new System.EventHandler(this.textBox2_MouseLeave);
        this.textResult.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.textBox2_MouseDoubleClick);
        this.textResult.MouseEnter += new System.EventHandler(this.textBox2_MouseEnter);
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.radioButton_16);
        this.groupBox1.Controls.Add(this.radioButton_10);
        this.groupBox1.Controls.Add(this.radioButton_8);
        this.groupBox1.Controls.Add(this.radioButton_2);
        this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        this.groupBox1.Location = new System.Drawing.Point(353, 26);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(87, 120);
        this.groupBox1.TabIndex = 5;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Система счисления";
        // 
        // radioButton_16
        // 
        this.radioButton_16.AutoSize = true;
        this.radioButton_16.Location = new System.Drawing.Point(6, 100);
        this.radioButton_16.Name = "radioButton_16";
        this.radioButton_16.Size = new System.Drawing.Size(57, 17);
        this.radioButton_16.TabIndex = 3;
        this.radioButton_16.Text = "16-ая";
        this.radioButton_16.UseVisualStyleBackColor = true;
        this.radioButton_16.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
        // 
        // radioButton_10
        // 
        this.radioButton_10.AutoSize = true;
        this.radioButton_10.Checked = true;
        this.radioButton_10.Location = new System.Drawing.Point(6, 76);
        this.radioButton_10.Name = "radioButton_10";
        this.radioButton_10.Size = new System.Drawing.Size(57, 17);
        this.radioButton_10.TabIndex = 2;
        this.radioButton_10.TabStop = true;
        this.radioButton_10.Text = "10-ая";
        this.radioButton_10.UseVisualStyleBackColor = true;
        this.radioButton_10.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
        // 
        // radioButton_8
        // 
        this.radioButton_8.AutoSize = true;
        this.radioButton_8.Location = new System.Drawing.Point(6, 53);
        this.radioButton_8.Name = "radioButton_8";
        this.radioButton_8.Size = new System.Drawing.Size(50, 17);
        this.radioButton_8.TabIndex = 1;
        this.radioButton_8.Text = "8-ая";
        this.radioButton_8.UseVisualStyleBackColor = true;
        this.radioButton_8.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
        // 
        // radioButton_2
        // 
        this.radioButton_2.AutoSize = true;
        this.radioButton_2.Location = new System.Drawing.Point(6, 30);
        this.radioButton_2.Name = "radioButton_2";
        this.radioButton_2.Size = new System.Drawing.Size(50, 17);
        this.radioButton_2.TabIndex = 0;
        this.radioButton_2.Text = "2-ая";
        this.radioButton_2.UseVisualStyleBackColor = true;
        this.radioButton_2.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
        // 
        // toolTip1
        // 
        this.toolTip1.AutoPopDelay = 10000;
        this.toolTip1.InitialDelay = 500;
        this.toolTip1.ReshowDelay = 100;
        // 
        // List
        // 
        this.List.AllowUserToAddRows = false;
        this.List.AllowUserToDeleteRows = false;
        this.List.AllowUserToResizeColumns = false;
        this.List.AllowUserToResizeRows = false;
        this.List.BackgroundColor = System.Drawing.SystemColors.Window;
        this.List.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.List.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
        this.List.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.List.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameUser,
            this.Balance});
        this.List.Location = new System.Drawing.Point(2, 153);
        this.List.MultiSelect = false;
        this.List.Name = "List";
        this.List.ReadOnly = true;
        this.List.RowHeadersVisible = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        this.List.RowsDefaultCellStyle = dataGridViewCellStyle1;
        this.List.RowTemplate.Height = 16;
        this.List.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.List.Size = new System.Drawing.Size(437, 140);
        this.List.TabIndex = 107;
        this.List.TabStop = false;
        this.List.MouseClick += new System.Windows.Forms.MouseEventHandler(this.List_MouseClick);
        this.List.DoubleClick += new System.EventHandler(this.List_DoubleClick);
        this.List.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.List_CellMouseDown);
        this.List.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.List_CellMouseDoubleClick);
        // 
        // nameUser
        // 
        this.nameUser.HeaderText = "Операция";
        this.nameUser.Name = "nameUser";
        this.nameUser.ReadOnly = true;
        this.nameUser.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        this.nameUser.Width = 216;
        // 
        // Balance
        // 
        this.Balance.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.Balance.HeaderText = "Результат";
        this.Balance.Name = "Balance";
        this.Balance.ReadOnly = true;
        this.Balance.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьСписокToolStripMenuItem,
            this.взятьРезультатToolStripMenuItem,
            this.взятьОперациюToolStripMenuItem});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(169, 70);
        // 
        // очиститьСписокToolStripMenuItem
        // 
        this.очиститьСписокToolStripMenuItem.Name = "очиститьСписокToolStripMenuItem";
        this.очиститьСписокToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
        this.очиститьСписокToolStripMenuItem.Text = "Очистить список";
        this.очиститьСписокToolStripMenuItem.Click += new System.EventHandler(this.очиститьСписокToolStripMenuItem_Click);
        // 
        // взятьРезультатToolStripMenuItem
        // 
        this.взятьРезультатToolStripMenuItem.Name = "взятьРезультатToolStripMenuItem";
        this.взятьРезультатToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
        this.взятьРезультатToolStripMenuItem.Text = "Взять результат";
        this.взятьРезультатToolStripMenuItem.Click += new System.EventHandler(this.взятьРезультатToolStripMenuItem_Click);
        // 
        // взятьОперациюToolStripMenuItem
        // 
        this.взятьОперациюToolStripMenuItem.Name = "взятьОперациюToolStripMenuItem";
        this.взятьОперациюToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
        this.взятьОперациюToolStripMenuItem.Text = "Взять операцию";
        this.взятьОперациюToolStripMenuItem.Click += new System.EventHandler(this.взятьОперациюToolStripMenuItem_Click);
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.информацияToolStripMenuItem});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(444, 24);
        this.menuStrip1.TabIndex = 108;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // файлToolStripMenuItem
        // 
        this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_FileAllNotationResults,
            this.oolStripMenu_FileExit});
        this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
        this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
        this.файлToolStripMenuItem.Text = "&Файл";
        // 
        // toolStripMenu_FileAllNotationResults
        // 
        this.toolStripMenu_FileAllNotationResults.Name = "toolStripMenu_FileAllNotationResults";
        this.toolStripMenu_FileAllNotationResults.Size = new System.Drawing.Size(217, 22);
        this.toolStripMenu_FileAllNotationResults.Text = "Все &счисления результата";
        this.toolStripMenu_FileAllNotationResults.Click += new System.EventHandler(this.toolStripMenu_FileAllNotationResults_Click);
        // 
        // oolStripMenu_FileExit
        // 
        this.oolStripMenu_FileExit.Name = "oolStripMenu_FileExit";
        this.oolStripMenu_FileExit.Size = new System.Drawing.Size(217, 22);
        this.oolStripMenu_FileExit.Text = "&Выход";
        this.oolStripMenu_FileExit.Click += new System.EventHandler(this.oolStripMenu_FileExit_Click);
        // 
        // информацияToolStripMenuItem
        // 
        this.информацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenu_InformationAbout});
        this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
        this.информацияToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
        this.информацияToolStripMenuItem.Text = "&Информация";
        // 
        // toolStripMenu_InformationAbout
        // 
        this.toolStripMenu_InformationAbout.Name = "toolStripMenu_InformationAbout";
        this.toolStripMenu_InformationAbout.Size = new System.Drawing.Size(158, 22);
        this.toolStripMenu_InformationAbout.Text = "&О программе...";
        this.toolStripMenu_InformationAbout.Click += new System.EventHandler(this.toolStripMenu_InformationAbout_Click);
        // 
        // buttonShowHistory
        // 
        this.buttonShowHistory.Image = global::Calculator.Properties.Resources.history;
        this.buttonShowHistory.Location = new System.Drawing.Point(184, 107);
        this.buttonShowHistory.Name = "buttonShowHistory";
        this.buttonShowHistory.Size = new System.Drawing.Size(40, 40);
        this.buttonShowHistory.TabIndex = 3;
        this.buttonShowHistory.TabStop = false;
        this.buttonShowHistory.UseVisualStyleBackColor = true;
        this.buttonShowHistory.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
        this.buttonShowHistory.Click += new System.EventHandler(this.buttonShowHistory_Click);
        this.buttonShowHistory.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
        // 
        // buttonExit
        // 
        this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.buttonExit.Image = global::Calculator.Properties.Resources.exit;
        this.buttonExit.Location = new System.Drawing.Point(307, 107);
        this.buttonExit.Name = "buttonExit";
        this.buttonExit.Size = new System.Drawing.Size(40, 40);
        this.buttonExit.TabIndex = 4;
        this.buttonExit.TabStop = false;
        this.buttonExit.UseVisualStyleBackColor = true;
        this.buttonExit.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
        this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
        this.buttonExit.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
        // 
        // buttonRun
        // 
        this.buttonRun.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.buttonRun.Image = global::Calculator.Properties.Resources.run;
        this.buttonRun.Location = new System.Drawing.Point(12, 107);
        this.buttonRun.Name = "buttonRun";
        this.buttonRun.Size = new System.Drawing.Size(60, 40);
        this.buttonRun.TabIndex = 1;
        this.buttonRun.TabStop = false;
        this.buttonRun.UseVisualStyleBackColor = true;
        this.buttonRun.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
        this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
        this.buttonRun.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
        // 
        // timer1
        // 
        this.timer1.Interval = 1500;
        this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(444, 151);
        this.Controls.Add(this.menuStrip1);
        this.Controls.Add(this.List);
        this.Controls.Add(this.buttonShowHistory);
        this.Controls.Add(this.buttonExit);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.textResult);
        this.Controls.Add(this.textOperation);
        this.Controls.Add(this.buttonRun);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Location = new System.Drawing.Point(100, 100);
        this.MainMenuStrip = this.menuStrip1;
        this.MaximizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Калькулятор";
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.List)).EndInit();
        this.contextMenuStrip1.ResumeLayout(false);
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button buttonRun;
    private System.Windows.Forms.TextBox textOperation;
    private System.Windows.Forms.TextBox textResult;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioButton_16;
    private System.Windows.Forms.RadioButton radioButton_10;
    private System.Windows.Forms.RadioButton radioButton_8;
    private System.Windows.Forms.RadioButton radioButton_2;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Button buttonShowHistory;
		public System.Windows.Forms.DataGridView List;
		private System.Windows.Forms.DataGridViewTextBoxColumn nameUser;
		private System.Windows.Forms.DataGridViewTextBoxColumn Balance;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem очиститьСписокToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem взятьРезультатToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem взятьОперациюToolStripMenuItem;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem oolStripMenu_FileExit;
    private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenu_InformationAbout;
		private System.Windows.Forms.Timer timer1;
    private System.Windows.Forms.ToolStripMenuItem toolStripMenu_FileAllNotationResults;
  }
}

