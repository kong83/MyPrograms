namespace ApprendreLeFrançais
{
    partial class TestForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TestForm));
            this.AnswerList = new System.Windows.Forms.DataGridView();
            this.DateStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AnswerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RightAnswerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            this.labelRussionWord = new System.Windows.Forms.Label();
            this.buttonRepeat = new System.Windows.Forms.Button();
            this.frenchLetters = new ApprendreLeFrançais.FrenchLetters();
            ((System.ComponentModel.ISupportInitialize)(this.AnswerList)).BeginInit();
            this.SuspendLayout();
            // 
            // AnswerList
            // 
            this.AnswerList.AllowUserToAddRows = false;
            this.AnswerList.AllowUserToDeleteRows = false;
            this.AnswerList.AllowUserToResizeRows = false;
            this.AnswerList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AnswerList.BackgroundColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AnswerList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.AnswerList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AnswerList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateStart,
            this.AnswerColumn,
            this.RightAnswerColumn});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.AnswerList.DefaultCellStyle = dataGridViewCellStyle3;
            this.AnswerList.Location = new System.Drawing.Point(12, 93);
            this.AnswerList.MultiSelect = false;
            this.AnswerList.Name = "AnswerList";
            this.AnswerList.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.AnswerList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.AnswerList.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.AnswerList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.AnswerList.RowTemplate.Height = 18;
            this.AnswerList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.AnswerList.ShowCellToolTips = false;
            this.AnswerList.Size = new System.Drawing.Size(499, 224);
            this.AnswerList.StandardTab = true;
            this.AnswerList.TabIndex = 2;
            this.AnswerList.TabStop = false;
            this.AnswerList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.AnswerList_ColumnWidthChanged);
            // 
            // DateStart
            // 
            this.DateStart.HeaderText = "Слово";
            this.DateStart.Name = "DateStart";
            this.DateStart.ReadOnly = true;
            this.DateStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DateStart.Width = 130;
            // 
            // AnswerColumn
            // 
            this.AnswerColumn.HeaderText = "Ответ";
            this.AnswerColumn.Name = "AnswerColumn";
            this.AnswerColumn.ReadOnly = true;
            this.AnswerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AnswerColumn.Width = 130;
            // 
            // RightAnswerColumn
            // 
            this.RightAnswerColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.RightAnswerColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.RightAnswerColumn.HeaderText = "Правильный ответ";
            this.RightAnswerColumn.Name = "RightAnswerColumn";
            this.RightAnswerColumn.ReadOnly = true;
            this.RightAnswerColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxAnswer.Location = new System.Drawing.Point(12, 65);
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(418, 20);
            this.textBoxAnswer.TabIndex = 1;
            this.textBoxAnswer.Leave += new System.EventHandler(this.textBoxAnswer_Leave);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(436, 64);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 3;
            this.buttonOk.TabStop = false;
            this.buttonOk.Text = "Дальше";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(9, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(237, 16);
            this.labelInfo.TabIndex = 104;
            this.labelInfo.Text = "Переведите на французский язык:";
            // 
            // labelRussionWord
            // 
            this.labelRussionWord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelRussionWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRussionWord.Location = new System.Drawing.Point(12, 32);
            this.labelRussionWord.Name = "labelRussionWord";
            this.labelRussionWord.Size = new System.Drawing.Size(345, 27);
            this.labelRussionWord.TabIndex = 105;
            // 
            // buttonRepeat
            // 
            this.buttonRepeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRepeat.Location = new System.Drawing.Point(282, 6);
            this.buttonRepeat.Name = "buttonRepeat";
            this.buttonRepeat.Size = new System.Drawing.Size(75, 23);
            this.buttonRepeat.TabIndex = 107;
            this.buttonRepeat.TabStop = false;
            this.buttonRepeat.Text = "Повторить";
            this.buttonRepeat.UseVisualStyleBackColor = true;
            this.buttonRepeat.Visible = false;
            this.buttonRepeat.Click += new System.EventHandler(this.buttonRepeat_Click);
            // 
            // frenchLetters
            // 
            this.frenchLetters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.frenchLetters.Location = new System.Drawing.Point(363, 9);
            this.frenchLetters.Name = "frenchLetters";
            this.frenchLetters.Size = new System.Drawing.Size(148, 53);
            this.frenchLetters.TabIndex = 106;
            this.frenchLetters.PressLetterEvent += new ApprendreLeFrançais.FrenchLetters.PressLetterEventHandler(this.frenchLetters_PressLetterEvent);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 329);
            this.Controls.Add(this.buttonRepeat);
            this.Controls.Add(this.frenchLetters);
            this.Controls.Add(this.labelRussionWord);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.AnswerList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 225);
            this.Name = "TestForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тестирование";
            this.Shown += new System.EventHandler(this.TestForm_Shown);
            this.LocationChanged += new System.EventHandler(this.TestForm_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.TestForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.AnswerList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView AnswerList;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Label labelRussionWord;
        private FrenchLetters frenchLetters;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn AnswerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn RightAnswerColumn;
        private System.Windows.Forms.Button buttonRepeat;
    }
}