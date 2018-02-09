namespace Notepad
{
    partial class UninstallForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UninstallForm));
            this.labelProgramCnt = new System.Windows.Forms.Label();
            this.ProgramList = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonCopyData = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonGetProgram = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxFilter0 = new System.Windows.Forms.TextBox();
            this.textBoxFilter1 = new System.Windows.Forms.TextBox();
            this.textBoxFilter2 = new System.Windows.Forms.TextBox();
            this.textBoxFilter3 = new System.Windows.Forms.TextBox();
            this.timerFilter = new System.Windows.Forms.Timer(this.components);
            this.buttonRemoveFilters = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProgramList)).BeginInit();
            this.SuspendLayout();
            // 
            // labelProgramCnt
            // 
            this.labelProgramCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelProgramCnt.AutoSize = true;
            this.labelProgramCnt.Location = new System.Drawing.Point(12, 343);
            this.labelProgramCnt.Name = "labelProgramCnt";
            this.labelProgramCnt.Size = new System.Drawing.Size(103, 13);
            this.labelProgramCnt.TabIndex = 19;
            this.labelProgramCnt.Text = "Всего программ: 0";
            // 
            // ProgramList
            // 
            this.ProgramList.AllowUserToAddRows = false;
            this.ProgramList.AllowUserToDeleteRows = false;
            this.ProgramList.AllowUserToResizeRows = false;
            this.ProgramList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProgramList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ProgramList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProgramList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProgramList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProgramList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.column2,
            this.column3,
            this.column4,
            this.EmptyColumn});
            this.ProgramList.Location = new System.Drawing.Point(12, 19);
            this.ProgramList.MultiSelect = false;
            this.ProgramList.Name = "ProgramList";
            this.ProgramList.ReadOnly = true;
            this.ProgramList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProgramList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ProgramList.RowTemplate.Height = 17;
            this.ProgramList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProgramList.Size = new System.Drawing.Size(613, 281);
            this.ProgramList.StandardTab = true;
            this.ProgramList.TabIndex = 0;
            this.ProgramList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PassList_CellDoubleClick);
            this.ProgramList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.ProgramList_ColumnWidthChanged);
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
            this.column2.HeaderText = "Ключ реестра";
            this.column2.Name = "column2";
            this.column2.ReadOnly = true;
            this.column2.Width = 150;
            // 
            // column3
            // 
            this.column3.HeaderText = "Версия";
            this.column3.Name = "column3";
            this.column3.ReadOnly = true;
            this.column3.Width = 70;
            // 
            // column4
            // 
            this.column4.HeaderText = "Путь установки";
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
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(12, 3);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(403, 13);
            this.labelInfo.TabIndex = 40;
            this.labelInfo.Text = "Кликните дважды на ячейке для копирования хранящейся в ней информации";
            // 
            // buttonCopyData
            // 
            this.buttonCopyData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopyData.BackgroundImage = global::Notepad.Properties.Resources.copyConvert;
            this.buttonCopyData.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopyData.Location = new System.Drawing.Point(440, 333);
            this.buttonCopyData.Name = "buttonCopyData";
            this.buttonCopyData.Size = new System.Drawing.Size(35, 35);
            this.buttonCopyData.TabIndex = 20;
            this.buttonCopyData.UseVisualStyleBackColor = true;
            this.buttonCopyData.MouseLeave += new System.EventHandler(this.buttonCopyData_MouseLeave);
            this.buttonCopyData.Click += new System.EventHandler(this.buttonCopyData_Click);
            this.buttonCopyData.MouseEnter += new System.EventHandler(this.buttonCopyData_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::Notepad.Properties.Resources.cansel24;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Location = new System.Drawing.Point(590, 333);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 23;
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
            this.buttonGetProgram.Location = new System.Drawing.Point(540, 333);
            this.buttonGetProgram.Name = "buttonGetProgram";
            this.buttonGetProgram.Size = new System.Drawing.Size(35, 35);
            this.buttonGetProgram.TabIndex = 22;
            this.buttonGetProgram.UseVisualStyleBackColor = true;
            this.buttonGetProgram.MouseLeave += new System.EventHandler(this.buttonGetProgram_MouseLeave);
            this.buttonGetProgram.Click += new System.EventHandler(this.buttonGetProgram_Click);
            this.buttonGetProgram.MouseEnter += new System.EventHandler(this.buttonGetProgram_MouseEnter);
            // 
            // textBoxFilter0
            // 
            this.textBoxFilter0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter0.Location = new System.Drawing.Point(12, 306);
            this.textBoxFilter0.Name = "textBoxFilter0";
            this.textBoxFilter0.Size = new System.Drawing.Size(196, 20);
            this.textBoxFilter0.TabIndex = 10;
            this.textBoxFilter0.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilter1
            // 
            this.textBoxFilter1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter1.Location = new System.Drawing.Point(214, 306);
            this.textBoxFilter1.Name = "textBoxFilter1";
            this.textBoxFilter1.Size = new System.Drawing.Size(148, 20);
            this.textBoxFilter1.TabIndex = 11;
            this.textBoxFilter1.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilter2
            // 
            this.textBoxFilter2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter2.Location = new System.Drawing.Point(368, 307);
            this.textBoxFilter2.Name = "textBoxFilter2";
            this.textBoxFilter2.Size = new System.Drawing.Size(70, 20);
            this.textBoxFilter2.TabIndex = 12;
            this.textBoxFilter2.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilter3
            // 
            this.textBoxFilter3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilter3.Location = new System.Drawing.Point(444, 307);
            this.textBoxFilter3.Name = "textBoxFilter3";
            this.textBoxFilter3.Size = new System.Drawing.Size(181, 20);
            this.textBoxFilter3.TabIndex = 13;
            this.textBoxFilter3.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // timerFilter
            // 
            this.timerFilter.Interval = 1000;
            this.timerFilter.Tick += new System.EventHandler(this.timerFilter_Tick);
            // 
            // buttonRemoveFilters
            // 
            this.buttonRemoveFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemoveFilters.BackgroundImage = global::Notepad.Properties.Resources.filterDelete;
            this.buttonRemoveFilters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRemoveFilters.Location = new System.Drawing.Point(490, 333);
            this.buttonRemoveFilters.Name = "buttonRemoveFilters";
            this.buttonRemoveFilters.Size = new System.Drawing.Size(35, 35);
            this.buttonRemoveFilters.TabIndex = 21;
            this.buttonRemoveFilters.UseVisualStyleBackColor = true;
            this.buttonRemoveFilters.MouseLeave += new System.EventHandler(this.buttonRemoveFilters_MouseLeave);
            this.buttonRemoveFilters.Click += new System.EventHandler(this.buttonRemoveFilters_Click);
            this.buttonRemoveFilters.MouseEnter += new System.EventHandler(this.buttonRemoveFilters_MouseEnter);
            // 
            // UninstallForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(637, 372);
            this.Controls.Add(this.buttonRemoveFilters);
            this.Controls.Add(this.textBoxFilter3);
            this.Controls.Add(this.textBoxFilter2);
            this.Controls.Add(this.textBoxFilter1);
            this.Controls.Add(this.textBoxFilter0);
            this.Controls.Add(this.buttonCopyData);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.ProgramList);
            this.Controls.Add(this.labelProgramCnt);
            this.Controls.Add(this.buttonGetProgram);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 200);
            this.Name = "UninstallForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Установленные программы";
            this.Load += new System.EventHandler(this.UninstallForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.UninstallForm_InputLanguageChanged);
            this.SizeChanged += new System.EventHandler(this.UninstallForm_SizeChanged);
            this.Activated += new System.EventHandler(this.UninstallForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UninstallForm_FormClosing);
            this.LocationChanged += new System.EventHandler(this.UninstallForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ProgramList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonGetProgram;
        private System.Windows.Forms.Label labelProgramCnt;
        public System.Windows.Forms.DataGridView ProgramList;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonCopyData;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
        private System.Windows.Forms.TextBox textBoxFilter0;
        private System.Windows.Forms.TextBox textBoxFilter1;
        private System.Windows.Forms.TextBox textBoxFilter2;
        private System.Windows.Forms.TextBox textBoxFilter3;
        private System.Windows.Forms.Timer timerFilter;
        private System.Windows.Forms.Button buttonRemoveFilters;
    }
}