namespace Notepad
{
    partial class ReplaceFilesTextForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReplaceFilesTextForm));
            this.checkNestingFolder = new System.Windows.Forms.CheckBox();
            this.textPath = new System.Windows.Forms.TextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.progressBarInfo = new System.Windows.Forms.ProgressBar();
            this.textBoxFilterExt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonReplaceGuid = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonReplaceText = new System.Windows.Forms.Button();
            this.textBoxFilterName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxFindString = new System.Windows.Forms.TextBox();
            this.textBoxReplaceString = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkUseRegular = new System.Windows.Forms.CheckBox();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkNestingFolder
            // 
            this.checkNestingFolder.AutoSize = true;
            this.checkNestingFolder.Location = new System.Drawing.Point(12, 55);
            this.checkNestingFolder.Name = "checkNestingFolder";
            this.checkNestingFolder.Size = new System.Drawing.Size(200, 17);
            this.checkNestingFolder.TabIndex = 5;
            this.checkNestingFolder.Text = "Просматривать вложенные папки";
            this.checkNestingFolder.UseVisualStyleBackColor = true;
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(12, 31);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(616, 20);
            this.textPath.TabIndex = 4;
            this.textPath.Text = "D:\\";
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(300, 166);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(75, 13);
            this.labelInfo.TabIndex = 23;
            this.labelInfo.Text = "Директория: ";
            this.labelInfo.Visible = false;
            // 
            // progressBarInfo
            // 
            this.progressBarInfo.Location = new System.Drawing.Point(300, 184);
            this.progressBarInfo.Name = "progressBarInfo";
            this.progressBarInfo.Size = new System.Drawing.Size(277, 23);
            this.progressBarInfo.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarInfo.TabIndex = 21;
            this.progressBarInfo.Visible = false;
            // 
            // textBoxFilterExt
            // 
            this.textBoxFilterExt.Location = new System.Drawing.Point(435, 56);
            this.textBoxFilterExt.Name = "textBoxFilterExt";
            this.textBoxFilterExt.Size = new System.Drawing.Size(31, 20);
            this.textBoxFilterExt.TabIndex = 24;
            this.textBoxFilterExt.Text = "*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 13);
            this.label1.TabIndex = 25;
            this.label1.Text = "Фильтр по файлам:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Путь к файлам";
            // 
            // buttonReplaceGuid
            // 
            this.buttonReplaceGuid.Location = new System.Drawing.Point(12, 178);
            this.buttonReplaceGuid.Name = "buttonReplaceGuid";
            this.buttonReplaceGuid.Size = new System.Drawing.Size(111, 29);
            this.buttonReplaceGuid.TabIndex = 27;
            this.buttonReplaceGuid.Text = "Заменить гуиды";
            this.buttonReplaceGuid.UseVisualStyleBackColor = true;
            this.buttonReplaceGuid.Click += new System.EventHandler(this.buttonReplaceGuid_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // buttonReplaceText
            // 
            this.buttonReplaceText.Location = new System.Drawing.Point(12, 87);
            this.buttonReplaceText.Name = "buttonReplaceText";
            this.buttonReplaceText.Size = new System.Drawing.Size(111, 49);
            this.buttonReplaceText.TabIndex = 28;
            this.buttonReplaceText.Text = "Заменить текст";
            this.buttonReplaceText.UseVisualStyleBackColor = true;
            this.buttonReplaceText.Click += new System.EventHandler(this.buttonReplaceText_Click);
            // 
            // textBoxFilterName
            // 
            this.textBoxFilterName.Location = new System.Drawing.Point(354, 56);
            this.textBoxFilterName.Name = "textBoxFilterName";
            this.textBoxFilterName.Size = new System.Drawing.Size(62, 20);
            this.textBoxFilterName.TabIndex = 29;
            this.textBoxFilterName.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(421, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = ".";
            // 
            // textBoxFindString
            // 
            this.textBoxFindString.Location = new System.Drawing.Point(243, 87);
            this.textBoxFindString.Name = "textBoxFindString";
            this.textBoxFindString.Size = new System.Drawing.Size(372, 20);
            this.textBoxFindString.TabIndex = 31;
            // 
            // textBoxReplaceString
            // 
            this.textBoxReplaceString.Location = new System.Drawing.Point(243, 116);
            this.textBoxReplaceString.Name = "textBoxReplaceString";
            this.textBoxReplaceString.Size = new System.Drawing.Size(372, 20);
            this.textBoxReplaceString.TabIndex = 32;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(132, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 33;
            this.label4.Text = "Строка для поиска:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(132, 119);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 34;
            this.label5.Text = "Строка для замены:";
            // 
            // checkUseRegular
            // 
            this.checkUseRegular.AutoSize = true;
            this.checkUseRegular.Location = new System.Drawing.Point(135, 141);
            this.checkUseRegular.Name = "checkUseRegular";
            this.checkUseRegular.Size = new System.Drawing.Size(223, 17);
            this.checkUseRegular.TabIndex = 36;
            this.checkUseRegular.Text = "Использовать регулярные выражения";
            this.checkUseRegular.UseVisualStyleBackColor = true;
            this.checkUseRegular.CheckedChanged += new System.EventHandler(this.checkUseRegular_CheckedChanged);
            // 
            // buttonInfo
            // 
            this.buttonInfo.BackgroundImage = global::Notepad.Properties.Resources.about16;
            this.buttonInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonInfo.Enabled = false;
            this.buttonInfo.Location = new System.Drawing.Point(623, 87);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(35, 49);
            this.buttonInfo.TabIndex = 35;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.BackgroundImage = global::Notepad.Properties.Resources.close16;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonClose.Location = new System.Drawing.Point(623, 172);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 22;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonPath
            // 
            this.buttonPath.BackgroundImage = global::Notepad.Properties.Resources.open16;
            this.buttonPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonPath.Location = new System.Drawing.Point(634, 28);
            this.buttonPath.Name = "buttonPath";
            this.buttonPath.Size = new System.Drawing.Size(24, 24);
            this.buttonPath.TabIndex = 3;
            this.buttonPath.UseVisualStyleBackColor = true;
            this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
            // 
            // ReplaceFilesTextForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 216);
            this.Controls.Add(this.checkUseRegular);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxReplaceString);
            this.Controls.Add(this.textBoxFindString);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxFilterName);
            this.Controls.Add(this.buttonReplaceText);
            this.Controls.Add(this.buttonReplaceGuid);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFilterExt);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.progressBarInfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.checkNestingFolder);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.buttonPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReplaceFilesTextForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Замена текста в группе файлов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkNestingFolder;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button buttonPath;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.ProgressBar progressBarInfo;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxFilterExt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonReplaceGuid;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button buttonReplaceText;
        private System.Windows.Forms.TextBox textBoxFilterName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxFindString;
        private System.Windows.Forms.TextBox textBoxReplaceString;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.CheckBox checkUseRegular;
    }
}