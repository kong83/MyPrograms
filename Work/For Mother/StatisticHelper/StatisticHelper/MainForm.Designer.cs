namespace StatisticHelper
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
            this.labelSvodFileInfo = new System.Windows.Forms.Label();
            this.textBoxSvodFilePath = new System.Windows.Forms.TextBox();
            this.textBoxHospitalFilesFolder = new System.Windows.Forms.TextBox();
            this.comboBoxFirstMonth = new System.Windows.Forms.ComboBox();
            this.comboBoxSecondMonth = new System.Windows.Forms.ComboBox();
            this.labelHospitalFilesInfo = new System.Windows.Forms.Label();
            this.labelPeriodInfo = new System.Windows.Forms.Label();
            this.textBoxSvod100FilePath = new System.Windows.Forms.TextBox();
            this.labelSvod100FileInfo = new System.Windows.Forms.Label();
            this.labelFirstMonth = new System.Windows.Forms.Label();
            this.labelSecondMonth = new System.Windows.Forms.Label();
            this.progressBarWorkInfo = new System.Windows.Forms.ProgressBar();
            this.labelCurrentAction = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.labelSvodVUTFileInfo = new System.Windows.Forms.Label();
            this.textBoxSvodVUTFilePath = new System.Windows.Forms.TextBox();
            this.buttonSelectSvodVUTFilePath = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonAbout = new System.Windows.Forms.Button();
            this.buttonSelectSvod100FilePath = new System.Windows.Forms.Button();
            this.buttonStartWork = new System.Windows.Forms.Button();
            this.buttonSelectHospitalFileFolder = new System.Windows.Forms.Button();
            this.buttonSelectSvodFilePath = new System.Windows.Forms.Button();
            this.buttonStopWork = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxCulture = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // labelSvodFileInfo
            // 
            this.labelSvodFileInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSvodFileInfo.Location = new System.Drawing.Point(12, 14);
            this.labelSvodFileInfo.Name = "labelSvodFileInfo";
            this.labelSvodFileInfo.Size = new System.Drawing.Size(737, 13);
            this.labelSvodFileInfo.TabIndex = 1;
            this.labelSvodFileInfo.Text = "Введите путь до файла с общим сводом";
            this.labelSvodFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSvodFilePath
            // 
            this.textBoxSvodFilePath.Location = new System.Drawing.Point(15, 30);
            this.textBoxSvodFilePath.Name = "textBoxSvodFilePath";
            this.textBoxSvodFilePath.Size = new System.Drawing.Size(734, 20);
            this.textBoxSvodFilePath.TabIndex = 1;
            this.textBoxSvodFilePath.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxHospitalFilesFolder
            // 
            this.textBoxHospitalFilesFolder.Location = new System.Drawing.Point(15, 210);
            this.textBoxHospitalFilesFolder.Name = "textBoxHospitalFilesFolder";
            this.textBoxHospitalFilesFolder.Size = new System.Drawing.Size(734, 20);
            this.textBoxHospitalFilesFolder.TabIndex = 10;
            this.textBoxHospitalFilesFolder.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // comboBoxFirstMonth
            // 
            this.comboBoxFirstMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFirstMonth.FormattingEnabled = true;
            this.comboBoxFirstMonth.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.comboBoxFirstMonth.Location = new System.Drawing.Point(223, 288);
            this.comboBoxFirstMonth.MaxDropDownItems = 12;
            this.comboBoxFirstMonth.Name = "comboBoxFirstMonth";
            this.comboBoxFirstMonth.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFirstMonth.TabIndex = 15;
            this.comboBoxFirstMonth.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // comboBoxSecondMonth
            // 
            this.comboBoxSecondMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSecondMonth.FormattingEnabled = true;
            this.comboBoxSecondMonth.Items.AddRange(new object[] {
            "Январь",
            "Февраль",
            "Март",
            "Апрель",
            "Май",
            "Июнь",
            "Июль",
            "Август",
            "Сентябрь",
            "Октябрь",
            "Ноябрь",
            "Декабрь"});
            this.comboBoxSecondMonth.Location = new System.Drawing.Point(369, 288);
            this.comboBoxSecondMonth.MaxDropDownItems = 12;
            this.comboBoxSecondMonth.Name = "comboBoxSecondMonth";
            this.comboBoxSecondMonth.Size = new System.Drawing.Size(121, 21);
            this.comboBoxSecondMonth.TabIndex = 16;
            this.comboBoxSecondMonth.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // labelHospitalFilesInfo
            // 
            this.labelHospitalFilesInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelHospitalFilesInfo.Location = new System.Drawing.Point(12, 194);
            this.labelHospitalFilesInfo.Name = "labelHospitalFilesInfo";
            this.labelHospitalFilesInfo.Size = new System.Drawing.Size(737, 13);
            this.labelHospitalFilesInfo.TabIndex = 6;
            this.labelHospitalFilesInfo.Text = "Введите путь до папки, содержащей информацию по больницам";
            this.labelHospitalFilesInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelPeriodInfo
            // 
            this.labelPeriodInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPeriodInfo.Location = new System.Drawing.Point(223, 252);
            this.labelPeriodInfo.Name = "labelPeriodInfo";
            this.labelPeriodInfo.Size = new System.Drawing.Size(267, 13);
            this.labelPeriodInfo.TabIndex = 7;
            this.labelPeriodInfo.Text = "Введите обрабатываемый период";
            this.labelPeriodInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSvod100FilePath
            // 
            this.textBoxSvod100FilePath.Location = new System.Drawing.Point(15, 90);
            this.textBoxSvod100FilePath.Name = "textBoxSvod100FilePath";
            this.textBoxSvod100FilePath.Size = new System.Drawing.Size(734, 20);
            this.textBoxSvod100FilePath.TabIndex = 5;
            this.textBoxSvod100FilePath.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // labelSvod100FileInfo
            // 
            this.labelSvod100FileInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSvod100FileInfo.Location = new System.Drawing.Point(12, 74);
            this.labelSvod100FileInfo.Name = "labelSvod100FileInfo";
            this.labelSvod100FileInfo.Size = new System.Drawing.Size(737, 13);
            this.labelSvod100FileInfo.TabIndex = 11;
            this.labelSvod100FileInfo.Text = "Введите путь до файла с общим со сводом на 100 работающих";
            this.labelSvod100FileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFirstMonth
            // 
            this.labelFirstMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelFirstMonth.Location = new System.Drawing.Point(220, 272);
            this.labelFirstMonth.Name = "labelFirstMonth";
            this.labelFirstMonth.Size = new System.Drawing.Size(124, 13);
            this.labelFirstMonth.TabIndex = 13;
            this.labelFirstMonth.Text = "первый месяц";
            this.labelFirstMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelSecondMonth
            // 
            this.labelSecondMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSecondMonth.Location = new System.Drawing.Point(366, 272);
            this.labelSecondMonth.Name = "labelSecondMonth";
            this.labelSecondMonth.Size = new System.Drawing.Size(124, 13);
            this.labelSecondMonth.TabIndex = 14;
            this.labelSecondMonth.Text = "последний месяц";
            this.labelSecondMonth.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // progressBarWorkInfo
            // 
            this.progressBarWorkInfo.Location = new System.Drawing.Point(12, 359);
            this.progressBarWorkInfo.Name = "progressBarWorkInfo";
            this.progressBarWorkInfo.Size = new System.Drawing.Size(737, 23);
            this.progressBarWorkInfo.TabIndex = 15;
            // 
            // labelCurrentAction
            // 
            this.labelCurrentAction.AutoSize = true;
            this.labelCurrentAction.Location = new System.Drawing.Point(12, 342);
            this.labelCurrentAction.Name = "labelCurrentAction";
            this.labelCurrentAction.Size = new System.Drawing.Size(235, 13);
            this.labelCurrentAction.TabIndex = 16;
            this.labelCurrentAction.Text = "Информация о ходе выполнения программы";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelSvodVUTFileInfo
            // 
            this.labelSvodVUTFileInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSvodVUTFileInfo.Location = new System.Drawing.Point(12, 134);
            this.labelSvodVUTFileInfo.Name = "labelSvodVUTFileInfo";
            this.labelSvodVUTFileInfo.Size = new System.Drawing.Size(737, 13);
            this.labelSvodVUTFileInfo.TabIndex = 24;
            this.labelSvodVUTFileInfo.Text = "Введите путь до файла со сводом ВУТ";
            this.labelSvodVUTFileInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBoxSvodVUTFilePath
            // 
            this.textBoxSvodVUTFilePath.Location = new System.Drawing.Point(15, 150);
            this.textBoxSvodVUTFilePath.Name = "textBoxSvodVUTFilePath";
            this.textBoxSvodVUTFilePath.Size = new System.Drawing.Size(734, 20);
            this.textBoxSvodVUTFilePath.TabIndex = 7;
            this.textBoxSvodVUTFilePath.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // buttonSelectSvodVUTFilePath
            // 
            this.buttonSelectSvodVUTFilePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSelectSvodVUTFilePath.BackgroundImage")));
            this.buttonSelectSvodVUTFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSelectSvodVUTFilePath.FlatAppearance.BorderSize = 0;
            this.buttonSelectSvodVUTFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectSvodVUTFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectSvodVUTFilePath.Location = new System.Drawing.Point(755, 150);
            this.buttonSelectSvodVUTFilePath.Name = "buttonSelectSvodVUTFilePath";
            this.buttonSelectSvodVUTFilePath.Size = new System.Drawing.Size(20, 20);
            this.buttonSelectSvodVUTFilePath.TabIndex = 8;
            this.buttonSelectSvodVUTFilePath.UseVisualStyleBackColor = true;
            this.buttonSelectSvodVUTFilePath.MouseLeave += new System.EventHandler(this.buttonSelectSvodFilePath_MouseLeave);
            this.buttonSelectSvodVUTFilePath.Click += new System.EventHandler(this.buttonSelectSvodVUTFilePath_Click);
            this.buttonSelectSvodVUTFilePath.MouseEnter += new System.EventHandler(this.buttonSelectSvodFilePath_MouseEnter);
            // 
            // buttonExit
            // 
            this.buttonExit.BackgroundImage = global::StatisticHelper.Properties.Resources.Exit32;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonExit.Location = new System.Drawing.Point(709, 264);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(40, 40);
            this.buttonExit.TabIndex = 22;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.MouseLeave += new System.EventHandler(this.buttonExit_MouseLeave);
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.MouseEnter += new System.EventHandler(this.buttonExit_MouseEnter);
            // 
            // buttonAbout
            // 
            this.buttonAbout.BackgroundImage = global::StatisticHelper.Properties.Resources.Info32;
            this.buttonAbout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAbout.FlatAppearance.BorderSize = 0;
            this.buttonAbout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAbout.Location = new System.Drawing.Point(647, 264);
            this.buttonAbout.Name = "buttonAbout";
            this.buttonAbout.Size = new System.Drawing.Size(40, 40);
            this.buttonAbout.TabIndex = 21;
            this.buttonAbout.UseVisualStyleBackColor = true;
            this.buttonAbout.MouseLeave += new System.EventHandler(this.buttonAbout_MouseLeave);
            this.buttonAbout.Click += new System.EventHandler(this.buttonAbout_Click);
            this.buttonAbout.MouseEnter += new System.EventHandler(this.buttonAbout_MouseEnter);
            // 
            // buttonSelectSvod100FilePath
            // 
            this.buttonSelectSvod100FilePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSelectSvod100FilePath.BackgroundImage")));
            this.buttonSelectSvod100FilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSelectSvod100FilePath.FlatAppearance.BorderSize = 0;
            this.buttonSelectSvod100FilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectSvod100FilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectSvod100FilePath.Location = new System.Drawing.Point(755, 90);
            this.buttonSelectSvod100FilePath.Name = "buttonSelectSvod100FilePath";
            this.buttonSelectSvod100FilePath.Size = new System.Drawing.Size(20, 20);
            this.buttonSelectSvod100FilePath.TabIndex = 6;
            this.buttonSelectSvod100FilePath.UseVisualStyleBackColor = true;
            this.buttonSelectSvod100FilePath.MouseLeave += new System.EventHandler(this.buttonSelectSvodFilePath_MouseLeave);
            this.buttonSelectSvod100FilePath.Click += new System.EventHandler(this.buttonSelectSvod100FilePath_Click);
            this.buttonSelectSvod100FilePath.MouseEnter += new System.EventHandler(this.buttonSelectSvodFilePath_MouseEnter);
            // 
            // buttonStartWork
            // 
            this.buttonStartWork.BackgroundImage = global::StatisticHelper.Properties.Resources.Run32;
            this.buttonStartWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonStartWork.FlatAppearance.BorderSize = 0;
            this.buttonStartWork.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStartWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStartWork.Location = new System.Drawing.Point(15, 264);
            this.buttonStartWork.Name = "buttonStartWork";
            this.buttonStartWork.Size = new System.Drawing.Size(40, 40);
            this.buttonStartWork.TabIndex = 20;
            this.buttonStartWork.UseVisualStyleBackColor = true;
            this.buttonStartWork.MouseLeave += new System.EventHandler(this.buttonStartWork_MouseLeave);
            this.buttonStartWork.Click += new System.EventHandler(this.buttonStartWork_Click);
            this.buttonStartWork.MouseEnter += new System.EventHandler(this.buttonStartWork_MouseEnter);
            // 
            // buttonSelectHospitalFileFolder
            // 
            this.buttonSelectHospitalFileFolder.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSelectHospitalFileFolder.BackgroundImage")));
            this.buttonSelectHospitalFileFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSelectHospitalFileFolder.FlatAppearance.BorderSize = 0;
            this.buttonSelectHospitalFileFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectHospitalFileFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectHospitalFileFolder.Location = new System.Drawing.Point(755, 210);
            this.buttonSelectHospitalFileFolder.Name = "buttonSelectHospitalFileFolder";
            this.buttonSelectHospitalFileFolder.Size = new System.Drawing.Size(20, 20);
            this.buttonSelectHospitalFileFolder.TabIndex = 11;
            this.buttonSelectHospitalFileFolder.UseVisualStyleBackColor = true;
            this.buttonSelectHospitalFileFolder.MouseLeave += new System.EventHandler(this.buttonSelectSvodFilePath_MouseLeave);
            this.buttonSelectHospitalFileFolder.Click += new System.EventHandler(this.buttonSelectHospitalFileFolder_Click);
            this.buttonSelectHospitalFileFolder.MouseEnter += new System.EventHandler(this.buttonSelectSvodFilePath_MouseEnter);
            // 
            // buttonSelectSvodFilePath
            // 
            this.buttonSelectSvodFilePath.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSelectSvodFilePath.BackgroundImage")));
            this.buttonSelectSvodFilePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSelectSvodFilePath.FlatAppearance.BorderSize = 0;
            this.buttonSelectSvodFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelectSvodFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSelectSvodFilePath.Location = new System.Drawing.Point(755, 29);
            this.buttonSelectSvodFilePath.Name = "buttonSelectSvodFilePath";
            this.buttonSelectSvodFilePath.Size = new System.Drawing.Size(20, 20);
            this.buttonSelectSvodFilePath.TabIndex = 2;
            this.buttonSelectSvodFilePath.UseVisualStyleBackColor = true;
            this.buttonSelectSvodFilePath.MouseLeave += new System.EventHandler(this.buttonSelectSvodFilePath_MouseLeave);
            this.buttonSelectSvodFilePath.Click += new System.EventHandler(this.buttonSelectSvodFilePath_Click);
            this.buttonSelectSvodFilePath.MouseEnter += new System.EventHandler(this.buttonSelectSvodFilePath_MouseEnter);
            // 
            // buttonStopWork
            // 
            this.buttonStopWork.BackgroundImage = global::StatisticHelper.Properties.Resources.Stop32;
            this.buttonStopWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonStopWork.FlatAppearance.BorderSize = 0;
            this.buttonStopWork.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStopWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonStopWork.Location = new System.Drawing.Point(61, 264);
            this.buttonStopWork.Name = "buttonStopWork";
            this.buttonStopWork.Size = new System.Drawing.Size(40, 40);
            this.buttonStopWork.TabIndex = 23;
            this.buttonStopWork.UseVisualStyleBackColor = true;
            this.buttonStopWork.Visible = false;
            this.buttonStopWork.MouseLeave += new System.EventHandler(this.buttonStopWork_MouseLeave);
            this.buttonStopWork.Click += new System.EventHandler(this.buttonStopWork_Click);
            this.buttonStopWork.MouseEnter += new System.EventHandler(this.buttonStopWork_MouseEnter);
            // 
            // checkBoxCulture
            // 
            this.checkBoxCulture.AutoSize = true;
            this.checkBoxCulture.Location = new System.Drawing.Point(15, 312);
            this.checkBoxCulture.Name = "checkBoxCulture";
            this.checkBoxCulture.Size = new System.Drawing.Size(236, 17);
            this.checkBoxCulture.TabIndex = 25;
            this.checkBoxCulture.Text = "Менять культуру в процессе выполнения";
            this.checkBoxCulture.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 332);
            this.Controls.Add(this.checkBoxCulture);
            this.Controls.Add(this.buttonSelectSvodVUTFilePath);
            this.Controls.Add(this.labelSvodVUTFileInfo);
            this.Controls.Add(this.textBoxSvodVUTFilePath);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonAbout);
            this.Controls.Add(this.labelCurrentAction);
            this.Controls.Add(this.progressBarWorkInfo);
            this.Controls.Add(this.labelSecondMonth);
            this.Controls.Add(this.labelFirstMonth);
            this.Controls.Add(this.textBoxSvod100FilePath);
            this.Controls.Add(this.labelSvod100FileInfo);
            this.Controls.Add(this.buttonSelectSvod100FilePath);
            this.Controls.Add(this.buttonStartWork);
            this.Controls.Add(this.buttonSelectHospitalFileFolder);
            this.Controls.Add(this.labelPeriodInfo);
            this.Controls.Add(this.labelHospitalFilesInfo);
            this.Controls.Add(this.comboBoxSecondMonth);
            this.Controls.Add(this.comboBoxFirstMonth);
            this.Controls.Add(this.textBoxHospitalFilesFolder);
            this.Controls.Add(this.textBoxSvodFilePath);
            this.Controls.Add(this.labelSvodFileInfo);
            this.Controls.Add(this.buttonSelectSvodFilePath);
            this.Controls.Add(this.buttonStopWork);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Статистический помощник";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonSelectSvodFilePath;
        private System.Windows.Forms.Label labelSvodFileInfo;
        private System.Windows.Forms.TextBox textBoxSvodFilePath;
        private System.Windows.Forms.TextBox textBoxHospitalFilesFolder;
        private System.Windows.Forms.ComboBox comboBoxFirstMonth;
        private System.Windows.Forms.ComboBox comboBoxSecondMonth;
        private System.Windows.Forms.Label labelHospitalFilesInfo;
        private System.Windows.Forms.Label labelPeriodInfo;
        private System.Windows.Forms.Button buttonSelectHospitalFileFolder;
        private System.Windows.Forms.Button buttonStartWork;
        private System.Windows.Forms.TextBox textBoxSvod100FilePath;
        private System.Windows.Forms.Label labelSvod100FileInfo;
        private System.Windows.Forms.Button buttonSelectSvod100FilePath;
        private System.Windows.Forms.Label labelFirstMonth;
        private System.Windows.Forms.Label labelSecondMonth;
        private System.Windows.Forms.ProgressBar progressBarWorkInfo;
        private System.Windows.Forms.Label labelCurrentAction;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonAbout;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonStopWork;
        private System.Windows.Forms.Button buttonSelectSvodVUTFilePath;
        private System.Windows.Forms.Label labelSvodVUTFileInfo;
        private System.Windows.Forms.TextBox textBoxSvodVUTFilePath;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxCulture;
    }
}

