namespace ApprendreLeFrançais
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControlDictionary = new System.Windows.Forms.TabControl();
            this.tabPage0 = new System.Windows.Forms.TabPage();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.textBoxSearchRussion = new System.Windows.Forms.TextBox();
            this.textBoxSearchFrench = new System.Windows.Forms.TextBox();
            this.WordsList = new System.Windows.Forms.DataGridView();
            this.DateStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateStop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.buttonCopy = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonTest = new System.Windows.Forms.Button();
            this.radioButtonRtoF = new System.Windows.Forms.RadioButton();
            this.radioButtonFtoR = new System.Windows.Forms.RadioButton();
            this.labelVersion = new System.Windows.Forms.Label();
            this.frenchLetters1 = new ApprendreLeFrançais.FrenchLetters();
            this.tabControlDictionary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.WordsList)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlDictionary
            // 
            this.tabControlDictionary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlDictionary.Controls.Add(this.tabPage0);
            this.tabControlDictionary.Location = new System.Drawing.Point(12, 61);
            this.tabControlDictionary.Name = "tabControlDictionary";
            this.tabControlDictionary.SelectedIndex = 0;
            this.tabControlDictionary.Size = new System.Drawing.Size(270, 22);
            this.tabControlDictionary.TabIndex = 0;
            this.tabControlDictionary.TabStop = false;
            this.tabControlDictionary.SelectedIndexChanged += new System.EventHandler(this.tabControlDictionary_SelectedIndexChanged);
            // 
            // tabPage0
            // 
            this.tabPage0.Location = new System.Drawing.Point(4, 22);
            this.tabPage0.Name = "tabPage0";
            this.tabPage0.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage0.Size = new System.Drawing.Size(262, 0);
            this.tabPage0.TabIndex = 0;
            this.tabPage0.Text = "Словарь";
            this.tabPage0.UseVisualStyleBackColor = true;
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(284, 58);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(26, 23);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.Text = "+";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Location = new System.Drawing.Point(309, 58);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(26, 23);
            this.buttonRemove.TabIndex = 3;
            this.buttonRemove.TabStop = false;
            this.buttonRemove.Text = "-";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            this.buttonRemove.MouseEnter += new System.EventHandler(this.buttonRemove_MouseEnter);
            this.buttonRemove.MouseLeave += new System.EventHandler(this.buttonRemove_MouseLeave);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Location = new System.Drawing.Point(334, 58);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(26, 23);
            this.buttonEdit.TabIndex = 5;
            this.buttonEdit.TabStop = false;
            this.buttonEdit.Text = "...";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            this.buttonEdit.MouseEnter += new System.EventHandler(this.buttonEdit_MouseEnter);
            this.buttonEdit.MouseLeave += new System.EventHandler(this.buttonEdit_MouseLeave);
            // 
            // textBoxSearchRussion
            // 
            this.textBoxSearchRussion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSearchRussion.Location = new System.Drawing.Point(12, 561);
            this.textBoxSearchRussion.Name = "textBoxSearchRussion";
            this.textBoxSearchRussion.Size = new System.Drawing.Size(126, 20);
            this.textBoxSearchRussion.TabIndex = 3;
            this.textBoxSearchRussion.TextChanged += new System.EventHandler(this.textBoxSearchRussion_TextChanged);
            this.textBoxSearchRussion.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxSearchRussion.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // textBoxSearchFrench
            // 
            this.textBoxSearchFrench.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSearchFrench.Location = new System.Drawing.Point(144, 561);
            this.textBoxSearchFrench.Name = "textBoxSearchFrench";
            this.textBoxSearchFrench.Size = new System.Drawing.Size(240, 20);
            this.textBoxSearchFrench.TabIndex = 5;
            this.textBoxSearchFrench.TextChanged += new System.EventHandler(this.textBoxSearchFrench_TextChanged);
            this.textBoxSearchFrench.Enter += new System.EventHandler(this.textBox_Enter);
            this.textBoxSearchFrench.Leave += new System.EventHandler(this.textBox_Leave);
            // 
            // WordsList
            // 
            this.WordsList.AllowUserToResizeRows = false;
            this.WordsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WordsList.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WordsList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.WordsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.WordsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateStart,
            this.DateStop});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.WordsList.DefaultCellStyle = dataGridViewCellStyle2;
            this.WordsList.Location = new System.Drawing.Point(12, 81);
            this.WordsList.MultiSelect = false;
            this.WordsList.Name = "WordsList";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.WordsList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.WordsList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.WordsList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.WordsList.RowTemplate.Height = 18;
            this.WordsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.WordsList.ShowCellToolTips = false;
            this.WordsList.Size = new System.Drawing.Size(372, 474);
            this.WordsList.StandardTab = true;
            this.WordsList.TabIndex = 1;
            this.WordsList.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.WordsList_CellEndEdit);
            this.WordsList.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.WordsList_CellMouseClick);
            this.WordsList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.TimesList_ColumnWidthChanged);
            this.WordsList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.WordsList_EditingControlShowing);
            this.WordsList.Enter += new System.EventHandler(this.WordsList_Enter);
            this.WordsList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.WordsList_MouseClick);
            this.WordsList.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.WordsList_PreviewKeyDown);
            // 
            // DateStart
            // 
            this.DateStart.HeaderText = "Слово";
            this.DateStart.Name = "DateStart";
            this.DateStart.Width = 130;
            // 
            // DateStop
            // 
            this.DateStop.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DateStop.HeaderText = "Перевод";
            this.DateStop.Name = "DateStop";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(119, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.removeToolStripMenuItem.Text = "Удалить";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.Location = new System.Drawing.Point(359, 58);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(26, 23);
            this.buttonCopy.TabIndex = 101;
            this.buttonCopy.TabStop = false;
            this.buttonCopy.Text = "->";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            this.buttonCopy.MouseEnter += new System.EventHandler(this.buttonCopy_MouseEnter);
            this.buttonCopy.MouseLeave += new System.EventHandler(this.buttonCopy_MouseLeave);
            // 
            // buttonTest
            // 
            this.buttonTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTest.Location = new System.Drawing.Point(192, 10);
            this.buttonTest.Name = "buttonTest";
            this.buttonTest.Size = new System.Drawing.Size(75, 39);
            this.buttonTest.TabIndex = 102;
            this.buttonTest.Text = "Т Е С Т !";
            this.buttonTest.UseVisualStyleBackColor = true;
            this.buttonTest.Click += new System.EventHandler(this.buttonTest_Click);
            this.buttonTest.MouseEnter += new System.EventHandler(this.buttonTest_MouseEnter);
            this.buttonTest.MouseLeave += new System.EventHandler(this.buttonTest_MouseLeave);
            // 
            // radioButtonRtoF
            // 
            this.radioButtonRtoF.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonRtoF.AutoSize = true;
            this.radioButtonRtoF.Checked = true;
            this.radioButtonRtoF.Location = new System.Drawing.Point(273, 10);
            this.radioButtonRtoF.Name = "radioButtonRtoF";
            this.radioButtonRtoF.Size = new System.Drawing.Size(109, 17);
            this.radioButtonRtoF.TabIndex = 103;
            this.radioButtonRtoF.TabStop = true;
            this.radioButtonRtoF.Text = "На франзузский";
            this.radioButtonRtoF.UseVisualStyleBackColor = true;
            this.radioButtonRtoF.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonFtoR
            // 
            this.radioButtonFtoR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonFtoR.AutoSize = true;
            this.radioButtonFtoR.Location = new System.Drawing.Point(273, 32);
            this.radioButtonFtoR.Name = "radioButtonFtoR";
            this.radioButtonFtoR.Size = new System.Drawing.Size(83, 17);
            this.radioButtonFtoR.TabIndex = 104;
            this.radioButtonFtoR.Text = "На русский";
            this.radioButtonFtoR.UseVisualStyleBackColor = true;
            this.radioButtonFtoR.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(329, 580);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(59, 13);
            this.labelVersion.TabIndex = 105;
            this.labelVersion.Text = "version 2.0";
            // 
            // frenchLetters1
            // 
            this.frenchLetters1.Location = new System.Drawing.Point(12, 4);
            this.frenchLetters1.Name = "frenchLetters1";
            this.frenchLetters1.Size = new System.Drawing.Size(148, 53);
            this.frenchLetters1.TabIndex = 100;
            this.frenchLetters1.PressLetterEvent += new ApprendreLeFrançais.FrenchLetters.PressLetterEventHandler(this.frenchLetters_PressLetterEvent);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 593);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.radioButtonFtoR);
            this.Controls.Add(this.radioButtonRtoF);
            this.Controls.Add(this.buttonTest);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.WordsList);
            this.Controls.Add(this.frenchLetters1);
            this.Controls.Add(this.textBoxSearchFrench);
            this.Controls.Add(this.textBoxSearchRussion);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.tabControlDictionary);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(390, 250);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Программа для изучения французских слов";
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.tabControlDictionary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.WordsList)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlDictionary;
        private System.Windows.Forms.TabPage tabPage0;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.TextBox textBoxSearchRussion;
        private System.Windows.Forms.TextBox textBoxSearchFrench;
        private FrenchLetters frenchLetters1;
        public System.Windows.Forms.DataGridView WordsList;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateStop;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonTest;
        private System.Windows.Forms.RadioButton radioButtonRtoF;
        private System.Windows.Forms.RadioButton radioButtonFtoR;
        private System.Windows.Forms.Label labelVersion;
    }
}

