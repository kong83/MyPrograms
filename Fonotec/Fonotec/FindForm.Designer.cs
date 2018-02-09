namespace Fonotec
{
    partial class FindForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonFind = new System.Windows.Forms.Button();
            this.textBoxDiskNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxDiskInfo = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxFilmName = new System.Windows.Forms.TextBox();
            this.textBoxFilmInfo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxNameAll = new System.Windows.Forms.CheckBox();
            this.checkBoxInfoAll = new System.Windows.Forms.CheckBox();
            this.checkBoxNameRegistry = new System.Windows.Forms.CheckBox();
            this.checkBoxInfoRegistry = new System.Windows.Forms.CheckBox();
            this.dataGridViewFindFilms = new System.Windows.Forms.DataGridView();
            this.DiskNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiskInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilmInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelFindResult = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.buttonExport = new System.Windows.Forms.Button();
            this.comboBoxFilterGenre = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindFilms)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::Fonotec.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonClose.Location = new System.Drawing.Point(342, 161);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 42;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // buttonFind
            // 
            this.buttonFind.BackgroundImage = global::Fonotec.Properties.Resources.find;
            this.buttonFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFind.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonFind.Location = new System.Drawing.Point(342, 99);
            this.buttonFind.Name = "buttonFind";
            this.buttonFind.Size = new System.Drawing.Size(40, 40);
            this.buttonFind.TabIndex = 41;
            this.buttonFind.UseVisualStyleBackColor = true;
            this.buttonFind.MouseLeave += new System.EventHandler(this.buttonFind_MouseLeave);
            this.buttonFind.Click += new System.EventHandler(this.buttonFind_Click);
            this.buttonFind.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFind.MouseEnter += new System.EventHandler(this.buttonFind_MouseEnter);
            // 
            // textBoxDiskNumber
            // 
            this.textBoxDiskNumber.Location = new System.Drawing.Point(15, 56);
            this.textBoxDiskNumber.Name = "textBoxDiskNumber";
            this.textBoxDiskNumber.Size = new System.Drawing.Size(144, 20);
            this.textBoxDiskNumber.TabIndex = 1;
            this.textBoxDiskNumber.MouseLeave += new System.EventHandler(this.textBoxDiskNumber_MouseLeave);
            this.textBoxDiskNumber.MouseEnter += new System.EventHandler(this.textBoxDiskNumber_MouseEnter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Номера дисков";
            // 
            // comboBoxDiskInfo
            // 
            this.comboBoxDiskInfo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDiskInfo.FormattingEnabled = true;
            this.comboBoxDiskInfo.Items.AddRange(new object[] {
            "Любой",
            "Купленный",
            "Диск DVD-R",
            "Диск DVD-RW"});
            this.comboBoxDiskInfo.Location = new System.Drawing.Point(191, 56);
            this.comboBoxDiskInfo.Name = "comboBoxDiskInfo";
            this.comboBoxDiskInfo.Size = new System.Drawing.Size(136, 21);
            this.comboBoxDiskInfo.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(188, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "Тип диска";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "Название фильма";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(181, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(144, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Информация о фильме";
            // 
            // textBoxFilmName
            // 
            this.textBoxFilmName.Location = new System.Drawing.Point(12, 140);
            this.textBoxFilmName.Name = "textBoxFilmName";
            this.textBoxFilmName.Size = new System.Drawing.Size(147, 20);
            this.textBoxFilmName.TabIndex = 10;
            // 
            // textBoxFilmInfo
            // 
            this.textBoxFilmInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFilmInfo.Location = new System.Drawing.Point(180, 140);
            this.textBoxFilmInfo.Name = "textBoxFilmInfo";
            this.textBoxFilmInfo.Size = new System.Drawing.Size(147, 20);
            this.textBoxFilmInfo.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(13, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(315, 19);
            this.label5.TabIndex = 24;
            this.label5.Text = "Заполните поля для поиска";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBoxNameAll
            // 
            this.checkBoxNameAll.AutoSize = true;
            this.checkBoxNameAll.Location = new System.Drawing.Point(13, 166);
            this.checkBoxNameAll.Name = "checkBoxNameAll";
            this.checkBoxNameAll.Size = new System.Drawing.Size(127, 17);
            this.checkBoxNameAll.TabIndex = 11;
            this.checkBoxNameAll.Text = "Полное совпадение";
            this.checkBoxNameAll.UseVisualStyleBackColor = true;
            // 
            // checkBoxInfoAll
            // 
            this.checkBoxInfoAll.AutoSize = true;
            this.checkBoxInfoAll.Location = new System.Drawing.Point(182, 166);
            this.checkBoxInfoAll.Name = "checkBoxInfoAll";
            this.checkBoxInfoAll.Size = new System.Drawing.Size(127, 17);
            this.checkBoxInfoAll.TabIndex = 21;
            this.checkBoxInfoAll.Text = "Полное совпадение";
            this.checkBoxInfoAll.UseVisualStyleBackColor = true;
            // 
            // checkBoxNameRegistry
            // 
            this.checkBoxNameRegistry.AutoSize = true;
            this.checkBoxNameRegistry.Location = new System.Drawing.Point(13, 187);
            this.checkBoxNameRegistry.Name = "checkBoxNameRegistry";
            this.checkBoxNameRegistry.Size = new System.Drawing.Size(124, 17);
            this.checkBoxNameRegistry.TabIndex = 12;
            this.checkBoxNameRegistry.Text = "Учитывать регистр";
            this.checkBoxNameRegistry.UseVisualStyleBackColor = true;
            // 
            // checkBoxInfoRegistry
            // 
            this.checkBoxInfoRegistry.AutoSize = true;
            this.checkBoxInfoRegistry.Location = new System.Drawing.Point(182, 187);
            this.checkBoxInfoRegistry.Name = "checkBoxInfoRegistry";
            this.checkBoxInfoRegistry.Size = new System.Drawing.Size(124, 17);
            this.checkBoxInfoRegistry.TabIndex = 22;
            this.checkBoxInfoRegistry.Text = "Учитывать регистр";
            this.checkBoxInfoRegistry.UseVisualStyleBackColor = true;
            // 
            // dataGridViewFindFilms
            // 
            this.dataGridViewFindFilms.AllowUserToAddRows = false;
            this.dataGridViewFindFilms.AllowUserToDeleteRows = false;
            this.dataGridViewFindFilms.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewFindFilms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewFindFilms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFindFilms.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DiskNumber,
            this.DiskInfo,
            this.FilmName,
            this.FilmInfo});
            this.dataGridViewFindFilms.Location = new System.Drawing.Point(394, 37);
            this.dataGridViewFindFilms.MultiSelect = false;
            this.dataGridViewFindFilms.Name = "dataGridViewFindFilms";
            this.dataGridViewFindFilms.ReadOnly = true;
            this.dataGridViewFindFilms.RowHeadersVisible = false;
            this.dataGridViewFindFilms.RowTemplate.Height = 20;
            this.dataGridViewFindFilms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewFindFilms.Size = new System.Drawing.Size(370, 166);
            this.dataGridViewFindFilms.TabIndex = 30;
            this.dataGridViewFindFilms.TabStop = false;
            this.dataGridViewFindFilms.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewFindFilms_CellDoubleClick);
            this.dataGridViewFindFilms.MouseLeave += new System.EventHandler(this.dataGridViewFindFilms_MouseLeave);
            this.dataGridViewFindFilms.MouseEnter += new System.EventHandler(this.dataGridViewFindFilms_MouseEnter);
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
            this.FilmInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FilmInfo.HeaderText = "Информация";
            this.FilmInfo.Name = "FilmInfo";
            this.FilmInfo.ReadOnly = true;
            this.FilmInfo.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // labelFindResult
            // 
            this.labelFindResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFindResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFindResult.Location = new System.Drawing.Point(394, 8);
            this.labelFindResult.Name = "labelFindResult";
            this.labelFindResult.Size = new System.Drawing.Size(370, 19);
            this.labelFindResult.TabIndex = 30;
            this.labelFindResult.Text = "Результат поиска";
            this.labelFindResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.BackgroundImage = global::Fonotec.Properties.Resources.ExportToExcel;
            this.buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExport.Location = new System.Drawing.Point(342, 37);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(40, 40);
            this.buttonExport.TabIndex = 40;
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.MouseLeave += new System.EventHandler(this.buttonExport_MouseLeave);
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            this.buttonExport.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonExport.MouseEnter += new System.EventHandler(this.buttonExport_MouseEnter);
            // 
            // comboBoxFilterGenre
            // 
            this.comboBoxFilterGenre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterGenre.FormattingEnabled = true;
            this.comboBoxFilterGenre.Items.AddRange(new object[] {
            "Все жанры",
            "Комедии",
            "Боевики",
            "Мультики",
            "Детективы",
            "Ужасы",
            "Триллеры",
            "Мелодрамы",
            "Исторические",
            "Фэнтэзи",
            "Фантастика",
            "Другие"});
            this.comboBoxFilterGenre.Location = new System.Drawing.Point(117, 89);
            this.comboBoxFilterGenre.Name = "comboBoxFilterGenre";
            this.comboBoxFilterGenre.Size = new System.Drawing.Size(210, 21);
            this.comboBoxFilterGenre.TabIndex = 43;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(12, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 44;
            this.label6.Text = "Жанр фильма";
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 215);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBoxFilterGenre);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.labelFindResult);
            this.Controls.Add(this.dataGridViewFindFilms);
            this.Controls.Add(this.checkBoxInfoRegistry);
            this.Controls.Add(this.checkBoxNameRegistry);
            this.Controls.Add(this.checkBoxInfoAll);
            this.Controls.Add(this.checkBoxNameAll);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxFilmInfo);
            this.Controls.Add(this.textBoxFilmName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxDiskInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDiskNumber);
            this.Controls.Add(this.buttonFind);
            this.Controls.Add(this.buttonClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FindForm";
            this.Text = "Поиск фильмов";
            this.Load += new System.EventHandler(this.FindForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.FindForm_InputLanguageChanged);
            this.LocationChanged += new System.EventHandler(this.FindForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFindFilms)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonFind;
        private System.Windows.Forms.TextBox textBoxDiskNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxDiskInfo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxFilmName;
        private System.Windows.Forms.TextBox textBoxFilmInfo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox checkBoxNameAll;
        private System.Windows.Forms.CheckBox checkBoxInfoAll;
        private System.Windows.Forms.CheckBox checkBoxNameRegistry;
        private System.Windows.Forms.CheckBox checkBoxInfoRegistry;
        private System.Windows.Forms.DataGridView dataGridViewFindFilms;
        private System.Windows.Forms.Label labelFindResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiskNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiskInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilmInfo;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ComboBox comboBoxFilterGenre;
        private System.Windows.Forms.Label label6;
    }
}