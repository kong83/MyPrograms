namespace Notepad
{
    partial class ProcessForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessForm));
            this.textBoxFilter3 = new System.Windows.Forms.TextBox();
            this.textBoxFilter2 = new System.Windows.Forms.TextBox();
            this.textBoxFilter1 = new System.Windows.Forms.TextBox();
            this.textBoxFilter0 = new System.Windows.Forms.TextBox();
            this.ProcessList = new System.Windows.Forms.DataGridView();
            this.labelProcessCnt = new System.Windows.Forms.Label();
            this.timerFilter = new System.Windows.Forms.Timer(this.components);
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonRemoveProcess = new System.Windows.Forms.Button();
            this.buttonViewInfo = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonGetProgram = new System.Windows.Forms.Button();
            this.buttonRemoveFilters = new System.Windows.Forms.Button();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxFilter4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessList)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxFilter3
            // 
            this.textBoxFilter3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter3.Location = new System.Drawing.Point(405, 351);
            this.textBoxFilter3.Name = "textBoxFilter3";
            this.textBoxFilter3.Size = new System.Drawing.Size(86, 20);
            this.textBoxFilter3.TabIndex = 13;
            this.textBoxFilter3.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilter2
            // 
            this.textBoxFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter2.Location = new System.Drawing.Point(254, 351);
            this.textBoxFilter2.Name = "textBoxFilter2";
            this.textBoxFilter2.Size = new System.Drawing.Size(145, 20);
            this.textBoxFilter2.TabIndex = 12;
            this.textBoxFilter2.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilter1
            // 
            this.textBoxFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter1.Location = new System.Drawing.Point(58, 351);
            this.textBoxFilter1.Name = "textBoxFilter1";
            this.textBoxFilter1.Size = new System.Drawing.Size(190, 20);
            this.textBoxFilter1.TabIndex = 11;
            this.textBoxFilter1.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilter0
            // 
            this.textBoxFilter0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter0.Location = new System.Drawing.Point(15, 351);
            this.textBoxFilter0.Name = "textBoxFilter0";
            this.textBoxFilter0.Size = new System.Drawing.Size(37, 20);
            this.textBoxFilter0.TabIndex = 10;
            this.textBoxFilter0.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // ProcessList
            // 
            this.ProcessList.AllowUserToAddRows = false;
            this.ProcessList.AllowUserToDeleteRows = false;
            this.ProcessList.AllowUserToResizeRows = false;
            this.ProcessList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ProcessList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProcessList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProcessList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.column1,
            this.column2,
            this.column3,
            this.column4,
            this.EmptyColumn});
            this.ProcessList.Location = new System.Drawing.Point(12, 12);
            this.ProcessList.MultiSelect = false;
            this.ProcessList.Name = "ProcessList";
            this.ProcessList.ReadOnly = true;
            this.ProcessList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProcessList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProcessList.RowTemplate.Height = 17;
            this.ProcessList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProcessList.Size = new System.Drawing.Size(665, 333);
            this.ProcessList.StandardTab = true;
            this.ProcessList.TabIndex = 0;
            this.ProcessList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ProcessList_CellMouseDoubleClick);
            this.ProcessList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.ProcessList_ColumnWidthChanged);
            // 
            // labelProcessCnt
            // 
            this.labelProcessCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProcessCnt.AutoSize = true;
            this.labelProcessCnt.Location = new System.Drawing.Point(12, 388);
            this.labelProcessCnt.Name = "labelProcessCnt";
            this.labelProcessCnt.Size = new System.Drawing.Size(106, 13);
            this.labelProcessCnt.TabIndex = 50;
            this.labelProcessCnt.Text = "Всего процессов: 0";
            // 
            // timerFilter
            // 
            this.timerFilter.Interval = 1000;
            this.timerFilter.Tick += new System.EventHandler(this.timerFilter_Tick);
            // 
            // buttonRemoveProcess
            // 
            this.buttonRemoveProcess.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveProcess.BackgroundImage = global::Notepad.Properties.Resources.cut16;
            this.buttonRemoveProcess.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRemoveProcess.Location = new System.Drawing.Point(442, 380);
            this.buttonRemoveProcess.Name = "buttonRemoveProcess";
            this.buttonRemoveProcess.Size = new System.Drawing.Size(35, 35);
            this.buttonRemoveProcess.TabIndex = 20;
            this.buttonRemoveProcess.UseVisualStyleBackColor = true;
            this.buttonRemoveProcess.MouseLeave += new System.EventHandler(this.buttonRemoveProcess_MouseLeave);
            this.buttonRemoveProcess.Click += new System.EventHandler(this.buttonRemoveProcess_Click);
            this.buttonRemoveProcess.MouseEnter += new System.EventHandler(this.buttonRemoveProcess_MouseEnter);
            // 
            // buttonViewInfo
            // 
            this.buttonViewInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonViewInfo.BackgroundImage = global::Notepad.Properties.Resources.about16;
            this.buttonViewInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonViewInfo.Location = new System.Drawing.Point(492, 380);
            this.buttonViewInfo.Name = "buttonViewInfo";
            this.buttonViewInfo.Size = new System.Drawing.Size(35, 35);
            this.buttonViewInfo.TabIndex = 21;
            this.buttonViewInfo.UseVisualStyleBackColor = true;
            this.buttonViewInfo.MouseLeave += new System.EventHandler(this.buttonViewInfo_MouseLeave);
            this.buttonViewInfo.Click += new System.EventHandler(this.buttonViewInfo_Click);
            this.buttonViewInfo.MouseEnter += new System.EventHandler(this.buttonViewInfo_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::Notepad.Properties.Resources.cansel24;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Location = new System.Drawing.Point(642, 380);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 24;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonGetProgram
            // 
            this.buttonGetProgram.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGetProgram.BackgroundImage = global::Notepad.Properties.Resources.refresh24;
            this.buttonGetProgram.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonGetProgram.Location = new System.Drawing.Point(592, 380);
            this.buttonGetProgram.Name = "buttonGetProgram";
            this.buttonGetProgram.Size = new System.Drawing.Size(35, 35);
            this.buttonGetProgram.TabIndex = 23;
            this.buttonGetProgram.UseVisualStyleBackColor = true;
            this.buttonGetProgram.MouseLeave += new System.EventHandler(this.buttonGetProgram_MouseLeave);
            this.buttonGetProgram.Click += new System.EventHandler(this.buttonGetProgram_Click);
            this.buttonGetProgram.MouseEnter += new System.EventHandler(this.buttonGetProgram_MouseEnter);
            // 
            // buttonRemoveFilters
            // 
            this.buttonRemoveFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveFilters.BackgroundImage = global::Notepad.Properties.Resources.filterDelete;
            this.buttonRemoveFilters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRemoveFilters.Enabled = false;
            this.buttonRemoveFilters.Location = new System.Drawing.Point(542, 380);
            this.buttonRemoveFilters.Name = "buttonRemoveFilters";
            this.buttonRemoveFilters.Size = new System.Drawing.Size(35, 35);
            this.buttonRemoveFilters.TabIndex = 22;
            this.buttonRemoveFilters.UseVisualStyleBackColor = true;
            this.buttonRemoveFilters.MouseLeave += new System.EventHandler(this.buttonRemoveFilters_MouseLeave);
            this.buttonRemoveFilters.Click += new System.EventHandler(this.buttonRemoveFilters_Click);
            this.buttonRemoveFilters.MouseEnter += new System.EventHandler(this.buttonRemoveFilters_MouseEnter);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 40;
            // 
            // column1
            // 
            this.column1.HeaderText = "Название";
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            this.column1.Width = 200;
            // 
            // column2
            // 
            this.column2.HeaderText = "Путь к файлу";
            this.column2.Name = "column2";
            this.column2.ReadOnly = true;
            this.column2.Width = 150;
            // 
            // column3
            // 
            this.column3.HeaderText = "Инфо";
            this.column3.Name = "column3";
            this.column3.ReadOnly = true;
            this.column3.Width = 70;
            // 
            // column4
            // 
            this.column4.HeaderText = "Дата запуска";
            this.column4.Name = "column4";
            this.column4.ReadOnly = true;
            this.column4.Width = 180;
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
            // textBoxFilter4
            // 
            this.textBoxFilter4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter4.Location = new System.Drawing.Point(497, 351);
            this.textBoxFilter4.Name = "textBoxFilter4";
            this.textBoxFilter4.Size = new System.Drawing.Size(154, 20);
            this.textBoxFilter4.TabIndex = 51;
            this.textBoxFilter4.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // ProcessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 422);
            this.Controls.Add(this.textBoxFilter4);
            this.Controls.Add(this.buttonRemoveFilters);
            this.Controls.Add(this.buttonRemoveProcess);
            this.Controls.Add(this.textBoxFilter3);
            this.Controls.Add(this.textBoxFilter2);
            this.Controls.Add(this.textBoxFilter1);
            this.Controls.Add(this.textBoxFilter0);
            this.Controls.Add(this.buttonViewInfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.ProcessList);
            this.Controls.Add(this.labelProcessCnt);
            this.Controls.Add(this.buttonGetProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1200, 800);
            this.MinimumSize = new System.Drawing.Size(500, 200);
            this.Name = "ProcessForm";
            this.Text = "Работа с процессами";
            this.Load += new System.EventHandler(this.ProcessForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.ProcessForm_InputLanguageChanged);
            this.SizeChanged += new System.EventHandler(this.ProcessForm_SizeChanged);
            this.Activated += new System.EventHandler(this.ProcessForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProcessForm_FormClosing);
            this.LocationChanged += new System.EventHandler(this.ProcessForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ProcessList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRemoveProcess;
        private System.Windows.Forms.TextBox textBoxFilter3;
        private System.Windows.Forms.TextBox textBoxFilter2;
        private System.Windows.Forms.TextBox textBoxFilter1;
        private System.Windows.Forms.TextBox textBoxFilter0;
        private System.Windows.Forms.Button buttonViewInfo;
        private System.Windows.Forms.Button buttonClose;
        public System.Windows.Forms.DataGridView ProcessList;
        private System.Windows.Forms.Label labelProcessCnt;
        private System.Windows.Forms.Button buttonGetProgram;
        private System.Windows.Forms.Timer timerFilter;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonRemoveFilters;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
        private System.Windows.Forms.TextBox textBoxFilter4;
    }
}