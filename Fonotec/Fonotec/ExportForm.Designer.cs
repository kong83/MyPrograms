namespace Fonotec
{
    partial class ExportForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExportForm));
            this.checkBoxAll = new System.Windows.Forms.CheckBox();
            this.checkBoxWithoutNumber = new System.Windows.Forms.CheckBox();
            this.checkBoxWithoutDiskInfo = new System.Windows.Forms.CheckBox();
            this.checkBoxWithoutFilmInfo = new System.Windows.Forms.CheckBox();
            this.dataGridViewPreview = new System.Windows.Forms.DataGridView();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.radioButtonView3 = new System.Windows.Forms.RadioButton();
            this.radioButtonView2 = new System.Windows.Forms.RadioButton();
            this.radioButtonView1 = new System.Windows.Forms.RadioButton();
            this.labelCaption = new System.Windows.Forms.Label();
            this.DiskNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiskInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilmInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).BeginInit();
            this.groupBoxSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxAll
            // 
            this.checkBoxAll.AutoSize = true;
            this.checkBoxAll.Checked = true;
            this.checkBoxAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxAll.Location = new System.Drawing.Point(6, 19);
            this.checkBoxAll.Name = "checkBoxAll";
            this.checkBoxAll.Size = new System.Drawing.Size(201, 17);
            this.checkBoxAll.TabIndex = 1;
            this.checkBoxAll.Text = "Экспортировать всю информацию";
            this.checkBoxAll.UseVisualStyleBackColor = true;
            this.checkBoxAll.CheckedChanged += new System.EventHandler(this.checkBoxAll_CheckedChanged);
            // 
            // checkBoxWithoutNumber
            // 
            this.checkBoxWithoutNumber.AutoSize = true;
            this.checkBoxWithoutNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxWithoutNumber.Location = new System.Drawing.Point(6, 42);
            this.checkBoxWithoutNumber.Name = "checkBoxWithoutNumber";
            this.checkBoxWithoutNumber.Size = new System.Drawing.Size(204, 17);
            this.checkBoxWithoutNumber.TabIndex = 2;
            this.checkBoxWithoutNumber.Text = "Экспортировать без номера диска";
            this.checkBoxWithoutNumber.UseVisualStyleBackColor = true;
            this.checkBoxWithoutNumber.CheckedChanged += new System.EventHandler(this.checkBoxWithoutNumber_CheckedChanged);
            // 
            // checkBoxWithoutDiskInfo
            // 
            this.checkBoxWithoutDiskInfo.AutoSize = true;
            this.checkBoxWithoutDiskInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxWithoutDiskInfo.Location = new System.Drawing.Point(6, 65);
            this.checkBoxWithoutDiskInfo.Name = "checkBoxWithoutDiskInfo";
            this.checkBoxWithoutDiskInfo.Size = new System.Drawing.Size(244, 17);
            this.checkBoxWithoutDiskInfo.TabIndex = 3;
            this.checkBoxWithoutDiskInfo.Text = "Экспортировать без информации о дисках";
            this.checkBoxWithoutDiskInfo.UseVisualStyleBackColor = true;
            this.checkBoxWithoutDiskInfo.CheckedChanged += new System.EventHandler(this.checkBoxWithoutDiskInfo_CheckedChanged);
            // 
            // checkBoxWithoutFilmInfo
            // 
            this.checkBoxWithoutFilmInfo.AutoSize = true;
            this.checkBoxWithoutFilmInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxWithoutFilmInfo.Location = new System.Drawing.Point(6, 88);
            this.checkBoxWithoutFilmInfo.Name = "checkBoxWithoutFilmInfo";
            this.checkBoxWithoutFilmInfo.Size = new System.Drawing.Size(254, 17);
            this.checkBoxWithoutFilmInfo.TabIndex = 4;
            this.checkBoxWithoutFilmInfo.Text = "Экспортировать без информации о фильмах";
            this.checkBoxWithoutFilmInfo.UseVisualStyleBackColor = true;
            this.checkBoxWithoutFilmInfo.CheckedChanged += new System.EventHandler(this.checkBoxWithoutFilmInfo_CheckedChanged);
            // 
            // dataGridViewPreview
            // 
            this.dataGridViewPreview.AllowUserToAddRows = false;
            this.dataGridViewPreview.AllowUserToDeleteRows = false;
            this.dataGridViewPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridViewPreview.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPreview.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewPreview.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DiskNumber,
            this.DiskInfo,
            this.FilmName,
            this.FilmInfo,
            this.EmptyColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewPreview.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewPreview.Location = new System.Drawing.Point(12, 168);
            this.dataGridViewPreview.MultiSelect = false;
            this.dataGridViewPreview.Name = "dataGridViewPreview";
            this.dataGridViewPreview.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewPreview.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewPreview.RowHeadersVisible = false;
            this.dataGridViewPreview.RowTemplate.Height = 20;
            this.dataGridViewPreview.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewPreview.Size = new System.Drawing.Size(528, 150);
            this.dataGridViewPreview.TabIndex = 30;
            this.dataGridViewPreview.TabStop = false;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCancel.BackgroundImage = global::Fonotec.Properties.Resources.close;
            this.buttonCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCancel.Location = new System.Drawing.Point(291, 331);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(40, 40);
            this.buttonCancel.TabIndex = 32;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.BackgroundImage = global::Fonotec.Properties.Resources.ok;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonOK.Location = new System.Drawing.Point(191, 331);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(40, 40);
            this.buttonOK.TabIndex = 31;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.Controls.Add(this.radioButtonView3);
            this.groupBoxSettings.Controls.Add(this.radioButtonView2);
            this.groupBoxSettings.Controls.Add(this.radioButtonView1);
            this.groupBoxSettings.Controls.Add(this.checkBoxAll);
            this.groupBoxSettings.Controls.Add(this.checkBoxWithoutNumber);
            this.groupBoxSettings.Controls.Add(this.checkBoxWithoutDiskInfo);
            this.groupBoxSettings.Controls.Add(this.checkBoxWithoutFilmInfo);
            this.groupBoxSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(528, 116);
            this.groupBoxSettings.TabIndex = 33;
            this.groupBoxSettings.TabStop = false;
            this.groupBoxSettings.Text = "Настройки экспорта";
            // 
            // radioButtonView3
            // 
            this.radioButtonView3.AutoSize = true;
            this.radioButtonView3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonView3.Location = new System.Drawing.Point(320, 65);
            this.radioButtonView3.Name = "radioButtonView3";
            this.radioButtonView3.Size = new System.Drawing.Size(175, 17);
            this.radioButtonView3.TabIndex = 7;
            this.radioButtonView3.Text = "Объединить по номеру диска";
            this.radioButtonView3.UseVisualStyleBackColor = true;
            this.radioButtonView3.CheckedChanged += new System.EventHandler(this.radioButtonView3_CheckedChanged);
            // 
            // radioButtonView2
            // 
            this.radioButtonView2.AutoSize = true;
            this.radioButtonView2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonView2.Location = new System.Drawing.Point(320, 42);
            this.radioButtonView2.Name = "radioButtonView2";
            this.radioButtonView2.Size = new System.Drawing.Size(178, 17);
            this.radioButtonView2.TabIndex = 6;
            this.radioButtonView2.Text = "Сортировать по номеру диска";
            this.radioButtonView2.UseVisualStyleBackColor = true;
            this.radioButtonView2.CheckedChanged += new System.EventHandler(this.radioButtonView2_CheckedChanged);
            // 
            // radioButtonView1
            // 
            this.radioButtonView1.AutoSize = true;
            this.radioButtonView1.Checked = true;
            this.radioButtonView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButtonView1.Location = new System.Drawing.Point(320, 18);
            this.radioButtonView1.Name = "radioButtonView1";
            this.radioButtonView1.Size = new System.Drawing.Size(201, 17);
            this.radioButtonView1.TabIndex = 5;
            this.radioButtonView1.TabStop = true;
            this.radioButtonView1.Text = "Сортировать по названию фильма";
            this.radioButtonView1.UseVisualStyleBackColor = true;
            this.radioButtonView1.CheckedChanged += new System.EventHandler(this.radioButtonView1_CheckedChanged);
            // 
            // labelCaption
            // 
            this.labelCaption.AutoSize = true;
            this.labelCaption.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCaption.Location = new System.Drawing.Point(163, 145);
            this.labelCaption.Name = "labelCaption";
            this.labelCaption.Size = new System.Drawing.Size(226, 16);
            this.labelCaption.TabIndex = 34;
            this.labelCaption.Text = "Предварительный результат";
            // 
            // DiskNumber
            // 
            this.DiskNumber.HeaderText = "Номер";
            this.DiskNumber.Name = "DiskNumber";
            this.DiskNumber.ReadOnly = true;
            this.DiskNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DiskNumber.Width = 50;
            // 
            // DiskInfo
            // 
            this.DiskInfo.HeaderText = "Диск";
            this.DiskInfo.Name = "DiskInfo";
            this.DiskInfo.ReadOnly = true;
            this.DiskInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DiskInfo.Width = 60;
            // 
            // FilmName
            // 
            this.FilmName.HeaderText = "Название фильма";
            this.FilmName.Name = "FilmName";
            this.FilmName.ReadOnly = true;
            this.FilmName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FilmName.Width = 130;
            // 
            // FilmInfo
            // 
            this.FilmInfo.HeaderText = "Информация";
            this.FilmInfo.Name = "FilmInfo";
            this.FilmInfo.ReadOnly = true;
            this.FilmInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FilmInfo.Width = 260;
            // 
            // EmptyColumn
            // 
            this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmptyColumn.HeaderText = string.Empty;
            this.EmptyColumn.Name = "EmptyColumn";
            this.EmptyColumn.ReadOnly = true;
            this.EmptyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 385);
            this.Controls.Add(this.labelCaption);
            this.Controls.Add(this.groupBoxSettings);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dataGridViewPreview);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(560, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(560, 350);
            this.Name = "ExportForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Экспорт в Excel";
            this.Load += new System.EventHandler(this.ExportForm_Load);
            this.SizeChanged += new System.EventHandler(this.ExportForm_SizeChanged);
            this.LocationChanged += new System.EventHandler(this.ExportForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewPreview)).EndInit();
            this.groupBoxSettings.ResumeLayout(false);
            this.groupBoxSettings.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxAll;
        private System.Windows.Forms.CheckBox checkBoxWithoutNumber;
        private System.Windows.Forms.CheckBox checkBoxWithoutDiskInfo;
        private System.Windows.Forms.CheckBox checkBoxWithoutFilmInfo;
        private System.Windows.Forms.DataGridView dataGridViewPreview;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.RadioButton radioButtonView3;
        private System.Windows.Forms.RadioButton radioButtonView2;
        private System.Windows.Forms.RadioButton radioButtonView1;
        private System.Windows.Forms.Label labelCaption;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiskNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiskInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilmInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
    }
}