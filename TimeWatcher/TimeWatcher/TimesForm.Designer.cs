namespace TimeWatcher
{
    partial class TimesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimesForm));
            this.TimesList = new System.Windows.Forms.DataGridView();
            this.DateStart = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateStop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelBorder = new System.Windows.Forms.Label();
            this.labelCaption = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timerWork = new System.Windows.Forms.Timer(this.components);
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonMoney = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TimesList)).BeginInit();
            this.SuspendLayout();
            // 
            // TimesList
            // 
            this.TimesList.AllowUserToAddRows = false;
            this.TimesList.AllowUserToDeleteRows = false;
            this.TimesList.AllowUserToResizeRows = false;
            this.TimesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TimesList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TimesList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TimesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TimesList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TimesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TimesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DateStart,
            this.DateStop,
            this.EmptyColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TimesList.DefaultCellStyle = dataGridViewCellStyle2;
            this.TimesList.Location = new System.Drawing.Point(75, 48);
            this.TimesList.MultiSelect = false;
            this.TimesList.Name = "TimesList";
            this.TimesList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TimesList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.TimesList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TimesList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.TimesList.RowTemplate.Height = 18;
            this.TimesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TimesList.ShowCellToolTips = false;
            this.TimesList.Size = new System.Drawing.Size(280, 499);
            this.TimesList.StandardTab = true;
            this.TimesList.TabIndex = 9;
            this.TimesList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.TimesList_CellDoubleClick);
            this.TimesList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TimesList_KeyDown);
            this.TimesList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.TimesList_ColumnWidthChanged);
            // 
            // DateStart
            // 
            this.DateStart.HeaderText = "Время начала";
            this.DateStart.Name = "DateStart";
            this.DateStart.ReadOnly = true;
            this.DateStart.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DateStart.Width = 130;
            // 
            // DateStop
            // 
            this.DateStop.HeaderText = "Время окончания";
            this.DateStop.Name = "DateStop";
            this.DateStop.ReadOnly = true;
            this.DateStop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DateStop.Width = 130;
            // 
            // EmptyColumn
            // 
            this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmptyColumn.HeaderText = "";
            this.EmptyColumn.MinimumWidth = 2;
            this.EmptyColumn.Name = "EmptyColumn";
            this.EmptyColumn.ReadOnly = true;
            this.EmptyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // labelBorder
            // 
            this.labelBorder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.labelBorder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelBorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelBorder.Location = new System.Drawing.Point(3, 48);
            this.labelBorder.Name = "labelBorder";
            this.labelBorder.Size = new System.Drawing.Size(66, 499);
            this.labelBorder.TabIndex = 40;
            this.labelBorder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelCaption
            // 
            this.labelCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCaption.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCaption.Location = new System.Drawing.Point(75, 9);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(280, 36);
            this.labelCaption.TabIndex = 41;
            this.labelCaption.Text = "Название проекта:";
            this.labelCaption.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelInfo.Location = new System.Drawing.Point(75, 553);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(280, 33);
            this.labelInfo.TabIndex = 45;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timerWork
            // 
            this.timerWork.Interval = 1000;
            this.timerWork.Tick += new System.EventHandler(this.timerWork_Tick);
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonEdit.FlatAppearance.BorderSize = 0;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Image = global::TimeWatcher.Properties.Resources.edit;
            this.buttonEdit.Location = new System.Drawing.Point(17, 150);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(40, 40);
            this.buttonEdit.TabIndex = 48;
            this.buttonEdit.TabStop = false;
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.MouseLeave += new System.EventHandler(this.buttonEdit_MouseLeave);
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            this.buttonEdit.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonEdit.MouseEnter += new System.EventHandler(this.buttonEdit_MouseEnter);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Image = global::TimeWatcher.Properties.Resources.delete;
            this.buttonDelete.Location = new System.Drawing.Point(17, 196);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 47;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            // 
            // buttonMoney
            // 
            this.buttonMoney.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMoney.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonMoney.FlatAppearance.BorderSize = 0;
            this.buttonMoney.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoney.Image = global::TimeWatcher.Properties.Resources.money24;
            this.buttonMoney.Location = new System.Drawing.Point(323, 557);
            this.buttonMoney.Name = "buttonMoney";
            this.buttonMoney.Size = new System.Drawing.Size(25, 25);
            this.buttonMoney.TabIndex = 46;
            this.buttonMoney.TabStop = false;
            this.buttonMoney.UseVisualStyleBackColor = true;
            this.buttonMoney.MouseLeave += new System.EventHandler(this.buttonMoney_MouseLeave);
            this.buttonMoney.Click += new System.EventHandler(this.buttonMoney_Click);
            this.buttonMoney.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonMoney.MouseEnter += new System.EventHandler(this.buttonMoney_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Image = global::TimeWatcher.Properties.Resources.close;
            this.buttonClose.Location = new System.Drawing.Point(17, 501);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 44;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonStop
            // 
            this.buttonStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStop.FlatAppearance.BorderSize = 0;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Image = global::TimeWatcher.Properties.Resources.timeStop;
            this.buttonStop.Location = new System.Drawing.Point(17, 104);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(40, 40);
            this.buttonStop.TabIndex = 43;
            this.buttonStop.TabStop = false;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.MouseLeave += new System.EventHandler(this.buttonStop_MouseLeave);
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            this.buttonStop.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonStop.MouseEnter += new System.EventHandler(this.buttonStop_MouseEnter);
            // 
            // buttonStart
            // 
            this.buttonStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStart.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlText;
            this.buttonStart.FlatAppearance.BorderSize = 0;
            this.buttonStart.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.buttonStart.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Image = global::TimeWatcher.Properties.Resources.timeStart;
            this.buttonStart.Location = new System.Drawing.Point(17, 58);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(40, 40);
            this.buttonStart.TabIndex = 42;
            this.buttonStart.TabStop = false;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.MouseLeave += new System.EventHandler(this.buttonStart_MouseLeave);
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            this.buttonStart.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonStart.MouseEnter += new System.EventHandler(this.buttonStart_MouseEnter);
            // 
            // TimesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 589);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonMoney);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.labelBorder);
            this.Controls.Add(this.TimesList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(370, 350);
            this.Name = "TimesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Распределение времени";
            this.Load += new System.EventHandler(this.TimesForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.TimesForm_InputLanguageChanged);
            this.SizeChanged += new System.EventHandler(this.TimesForm_SizeChanged);
            this.LocationChanged += new System.EventHandler(this.TimesForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.TimesList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.DataGridView TimesList;
        private System.Windows.Forms.Label labelBorder;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Timer timerWork;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateStop;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
        private System.Windows.Forms.Button buttonMoney;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
    }
}