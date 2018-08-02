namespace FilesName
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
        this.buttonPath = new System.Windows.Forms.Button();
        this.textPath = new System.Windows.Forms.TextBox();
        this.textFindString = new System.Windows.Forms.TextBox();
        this.textReplaceString = new System.Windows.Forms.TextBox();
        this.buttonStartReplace = new System.Windows.Forms.Button();
        this.checkNestingFolder = new System.Windows.Forms.CheckBox();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.buttonExit = new System.Windows.Forms.Button();
        this.label4 = new System.Windows.Forms.Label();
        this.groupBox1 = new System.Windows.Forms.GroupBox();
        this.radioCheckBody = new System.Windows.Forms.RadioButton();
        this.radioCheckExp = new System.Windows.Forms.RadioButton();
        this.labelInfo = new System.Windows.Forms.Label();
        this.progressBarInfo = new System.Windows.Forms.ProgressBar();
        this.radioCheckFile = new System.Windows.Forms.RadioButton();
        this.comboReplaceTo = new System.Windows.Forms.ComboBox();
        this.label3 = new System.Windows.Forms.Label();
        this.buttonAttributes = new System.Windows.Forms.Button();
        this.buttonLoverUpper = new System.Windows.Forms.Button();
        this.groupBox2 = new System.Windows.Forms.GroupBox();
        this.radioAttrArchive = new System.Windows.Forms.RadioButton();
        this.radioAttrTemporary = new System.Windows.Forms.RadioButton();
        this.radioAttrHide = new System.Windows.Forms.RadioButton();
        this.radioAttrSys = new System.Windows.Forms.RadioButton();
        this.radioAttrReadOnly = new System.Windows.Forms.RadioButton();
        this.radioAttrNo = new System.Windows.Forms.RadioButton();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.информацияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.comboConvertion = new System.Windows.Forms.ComboBox();
        this.buttonCopyInBuffer = new System.Windows.Forms.Button();
        this.groupBox3 = new System.Windows.Forms.GroupBox();
        this.radioFolderName = new System.Windows.Forms.RadioButton();
        this.radioFileNameWithExtentions = new System.Windows.Forms.RadioButton();
        this.radioFileName = new System.Windows.Forms.RadioButton();
        this.groupBox4 = new System.Windows.Forms.GroupBox();
        this.groupBox5 = new System.Windows.Forms.GroupBox();
        this.radioModifyNumeration = new System.Windows.Forms.RadioButton();
        this.labelMinCountDigits = new System.Windows.Forms.Label();
        this.buttonNumeration = new System.Windows.Forms.Button();
        this.labelStartNumber = new System.Windows.Forms.Label();
        this.radioRemoveNumeration = new System.Windows.Forms.RadioButton();
        this.textMinCountDigits = new System.Windows.Forms.TextBox();
        this.textStartNumber = new System.Windows.Forms.TextBox();
        this.radioDoNumeration = new System.Windows.Forms.RadioButton();
        this.label5 = new System.Windows.Forms.Label();
        this.groupBox1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.menuStrip1.SuspendLayout();
        this.groupBox3.SuspendLayout();
        this.groupBox4.SuspendLayout();
        this.groupBox5.SuspendLayout();
        this.SuspendLayout();
        // 
        // folderBrowserDialog1
        // 
        this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
        this.folderBrowserDialog1.ShowNewFolderButton = false;
        // 
        // buttonPath
        // 
        this.buttonPath.Location = new System.Drawing.Point(596, 29);
        this.buttonPath.Name = "buttonPath";
        this.buttonPath.Size = new System.Drawing.Size(75, 23);
        this.buttonPath.TabIndex = 0;
        this.buttonPath.Text = "Путь";
        this.buttonPath.UseVisualStyleBackColor = true;
        this.buttonPath.Click += new System.EventHandler(this.buttonPath_Click);
        // 
        // textPath
        // 
        this.textPath.Location = new System.Drawing.Point(12, 31);
        this.textPath.Name = "textPath";
        this.textPath.Size = new System.Drawing.Size(578, 20);
        this.textPath.TabIndex = 1;
        this.textPath.TextChanged += new System.EventHandler(this.textPath_TextChanged);
        // 
        // textFindString
        // 
        this.textFindString.Location = new System.Drawing.Point(135, 19);
        this.textFindString.Name = "textFindString";
        this.textFindString.Size = new System.Drawing.Size(314, 20);
        this.textFindString.TabIndex = 3;
        this.textFindString.TextChanged += new System.EventHandler(this.textFindString_TextChanged);
        // 
        // textReplaceString
        // 
        this.textReplaceString.Location = new System.Drawing.Point(135, 66);
        this.textReplaceString.Name = "textReplaceString";
        this.textReplaceString.Size = new System.Drawing.Size(314, 20);
        this.textReplaceString.TabIndex = 4;
        this.textReplaceString.TextChanged += new System.EventHandler(this.textReplaceString_TextChanged);
        // 
        // buttonStartReplace
        // 
        this.buttonStartReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.buttonStartReplace.Location = new System.Drawing.Point(13, 115);
        this.buttonStartReplace.Name = "buttonStartReplace";
        this.buttonStartReplace.Size = new System.Drawing.Size(96, 38);
        this.buttonStartReplace.TabIndex = 7;
        this.buttonStartReplace.Text = "Заменить в";
        this.buttonStartReplace.UseVisualStyleBackColor = true;
        this.buttonStartReplace.Click += new System.EventHandler(this.buttonStartReplace_Click);
        // 
        // checkNestingFolder
        // 
        this.checkNestingFolder.AutoSize = true;
        this.checkNestingFolder.Location = new System.Drawing.Point(12, 55);
        this.checkNestingFolder.Name = "checkNestingFolder";
        this.checkNestingFolder.Size = new System.Drawing.Size(200, 17);
        this.checkNestingFolder.TabIndex = 2;
        this.checkNestingFolder.Text = "Просматривать вложенные папки";
        this.checkNestingFolder.UseVisualStyleBackColor = true;
        this.checkNestingFolder.CheckedChanged += new System.EventHandler(this.checkNestingFolder_CheckedChanged);
        // 
        // label1
        // 
        this.label1.AutoSize = true;
        this.label1.Location = new System.Drawing.Point(6, 26);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(103, 13);
        this.label1.TabIndex = 6;
        this.label1.Text = "Строка для поиска";
        // 
        // label2
        // 
        this.label2.AutoSize = true;
        this.label2.Location = new System.Drawing.Point(6, 69);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(107, 13);
        this.label2.TabIndex = 7;
        this.label2.Text = "Строка для замены";
        // 
        // buttonExit
        // 
        this.buttonExit.Location = new System.Drawing.Point(572, 202);
        this.buttonExit.Name = "buttonExit";
        this.buttonExit.Size = new System.Drawing.Size(75, 29);
        this.buttonExit.TabIndex = 13;
        this.buttonExit.Text = "Выход";
        this.buttonExit.UseVisualStyleBackColor = true;
        this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
        // 
        // label4
        // 
        this.label4.AutoSize = true;
        this.label4.Location = new System.Drawing.Point(467, 19);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(173, 39);
        this.label4.TabIndex = 10;
        this.label4.Text = "Если строка для поиска пуста, \r\nто значение строки для замены \r\nдобавится в";
        // 
        // groupBox1
        // 
        this.groupBox1.Controls.Add(this.radioCheckBody);
        this.groupBox1.Controls.Add(this.radioCheckExp);
        this.groupBox1.Controls.Add(this.labelInfo);
        this.groupBox1.Controls.Add(this.progressBarInfo);
        this.groupBox1.Controls.Add(this.radioCheckFile);
        this.groupBox1.Controls.Add(this.buttonStartReplace);
        this.groupBox1.Controls.Add(this.comboReplaceTo);
        this.groupBox1.Controls.Add(this.textFindString);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.textReplaceString);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Location = new System.Drawing.Point(12, 88);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new System.Drawing.Size(659, 180);
        this.groupBox1.TabIndex = 6;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "Замена строк";
        // 
        // radioCheckBody
        // 
        this.radioCheckBody.AutoSize = true;
        this.radioCheckBody.Location = new System.Drawing.Point(135, 148);
        this.radioCheckBody.Name = "radioCheckBody";
        this.radioCheckBody.Size = new System.Drawing.Size(94, 17);
        this.radioCheckBody.TabIndex = 2;
        this.radioCheckBody.Text = "тексте файла";
        this.radioCheckBody.UseVisualStyleBackColor = true;
        this.radioCheckBody.CheckedChanged += new System.EventHandler(this.SaveTypeOfReplace_CheckedChanged);
        // 
        // radioCheckExp
        // 
        this.radioCheckExp.AutoSize = true;
        this.radioCheckExp.Location = new System.Drawing.Point(135, 126);
        this.radioCheckExp.Name = "radioCheckExp";
        this.radioCheckExp.Size = new System.Drawing.Size(122, 17);
        this.radioCheckExp.TabIndex = 1;
        this.radioCheckExp.Text = "расширении файла";
        this.radioCheckExp.UseVisualStyleBackColor = true;
        this.radioCheckExp.CheckedChanged += new System.EventHandler(this.SaveTypeOfReplace_CheckedChanged);
        // 
        // labelInfo
        // 
        this.labelInfo.AutoSize = true;
        this.labelInfo.Location = new System.Drawing.Point(342, 115);
        this.labelInfo.Name = "labelInfo";
        this.labelInfo.Size = new System.Drawing.Size(75, 13);
        this.labelInfo.TabIndex = 20;
        this.labelInfo.Text = "Директория: ";
        this.labelInfo.Visible = false;
        // 
        // progressBarInfo
        // 
        this.progressBarInfo.Location = new System.Drawing.Point(342, 133);
        this.progressBarInfo.Name = "progressBarInfo";
        this.progressBarInfo.Size = new System.Drawing.Size(305, 23);
        this.progressBarInfo.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
        this.progressBarInfo.TabIndex = 12;
        this.progressBarInfo.Visible = false;
        // 
        // radioCheckFile
        // 
        this.radioCheckFile.AutoSize = true;
        this.radioCheckFile.Checked = true;
        this.radioCheckFile.Location = new System.Drawing.Point(135, 103);
        this.radioCheckFile.Name = "radioCheckFile";
        this.radioCheckFile.Size = new System.Drawing.Size(92, 17);
        this.radioCheckFile.TabIndex = 0;
        this.radioCheckFile.TabStop = true;
        this.radioCheckFile.Text = "имени файла";
        this.radioCheckFile.UseVisualStyleBackColor = true;
        this.radioCheckFile.CheckedChanged += new System.EventHandler(this.SaveTypeOfReplace_CheckedChanged);
        // 
        // comboReplaceTo
        // 
        this.comboReplaceTo.FormattingEnabled = true;
        this.comboReplaceTo.Items.AddRange(new object[] {
            "начало",
            "конец"});
        this.comboReplaceTo.Location = new System.Drawing.Point(538, 49);
        this.comboReplaceTo.Name = "comboReplaceTo";
        this.comboReplaceTo.Size = new System.Drawing.Size(100, 21);
        this.comboReplaceTo.TabIndex = 5;
        this.comboReplaceTo.Text = "начало";
        this.comboReplaceTo.SelectedIndexChanged += new System.EventHandler(this.comboReplaceTo_SelectedIndexChanged);
        // 
        // label3
        // 
        this.label3.AutoSize = true;
        this.label3.Location = new System.Drawing.Point(467, 73);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(180, 13);
        this.label3.TabIndex = 13;
        this.label3.Text = "имени файла или его расширения";
        // 
        // buttonAttributes
        // 
        this.buttonAttributes.Location = new System.Drawing.Point(353, 19);
        this.buttonAttributes.Name = "buttonAttributes";
        this.buttonAttributes.Size = new System.Drawing.Size(278, 26);
        this.buttonAttributes.TabIndex = 9;
        this.buttonAttributes.Text = "Установить аттрибуты";
        this.buttonAttributes.UseVisualStyleBackColor = true;
        this.buttonAttributes.Click += new System.EventHandler(this.buttonAttributes_Click);
        // 
        // buttonLoverUpper
        // 
        this.buttonLoverUpper.Location = new System.Drawing.Point(551, 117);
        this.buttonLoverUpper.Name = "buttonLoverUpper";
        this.buttonLoverUpper.Size = new System.Drawing.Size(96, 29);
        this.buttonLoverUpper.TabIndex = 11;
        this.buttonLoverUpper.Text = "Преобразовать";
        this.buttonLoverUpper.UseVisualStyleBackColor = true;
        this.buttonLoverUpper.Click += new System.EventHandler(this.buttonPath_Click_Click);
        // 
        // groupBox2
        // 
        this.groupBox2.Controls.Add(this.radioAttrArchive);
        this.groupBox2.Controls.Add(this.radioAttrTemporary);
        this.groupBox2.Controls.Add(this.radioAttrHide);
        this.groupBox2.Controls.Add(this.radioAttrSys);
        this.groupBox2.Controls.Add(this.radioAttrReadOnly);
        this.groupBox2.Controls.Add(this.radioAttrNo);
        this.groupBox2.Location = new System.Drawing.Point(339, 51);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new System.Drawing.Size(308, 60);
        this.groupBox2.TabIndex = 8;
        this.groupBox2.TabStop = false;
        // 
        // radioAttrArchive
        // 
        this.radioAttrArchive.AutoSize = true;
        this.radioAttrArchive.Location = new System.Drawing.Point(218, 36);
        this.radioAttrArchive.Name = "radioAttrArchive";
        this.radioAttrArchive.Size = new System.Drawing.Size(74, 17);
        this.radioAttrArchive.TabIndex = 5;
        this.radioAttrArchive.Text = "архивный";
        this.radioAttrArchive.UseVisualStyleBackColor = true;
        this.radioAttrArchive.CheckedChanged += new System.EventHandler(this.SaveSetAttribute_CheckedChanged);
        // 
        // radioAttrTemporary
        // 
        this.radioAttrTemporary.AutoSize = true;
        this.radioAttrTemporary.Location = new System.Drawing.Point(218, 13);
        this.radioAttrTemporary.Name = "radioAttrTemporary";
        this.radioAttrTemporary.Size = new System.Drawing.Size(83, 17);
        this.radioAttrTemporary.TabIndex = 4;
        this.radioAttrTemporary.Text = "временный";
        this.radioAttrTemporary.UseVisualStyleBackColor = true;
        this.radioAttrTemporary.CheckedChanged += new System.EventHandler(this.SaveSetAttribute_CheckedChanged);
        // 
        // radioAttrHide
        // 
        this.radioAttrHide.AutoSize = true;
        this.radioAttrHide.Location = new System.Drawing.Point(130, 36);
        this.radioAttrHide.Name = "radioAttrHide";
        this.radioAttrHide.Size = new System.Drawing.Size(70, 17);
        this.radioAttrHide.TabIndex = 3;
        this.radioAttrHide.Text = "скрытый";
        this.radioAttrHide.UseVisualStyleBackColor = true;
        this.radioAttrHide.CheckedChanged += new System.EventHandler(this.SaveSetAttribute_CheckedChanged);
        // 
        // radioAttrSys
        // 
        this.radioAttrSys.AutoSize = true;
        this.radioAttrSys.Location = new System.Drawing.Point(130, 13);
        this.radioAttrSys.Name = "radioAttrSys";
        this.radioAttrSys.Size = new System.Drawing.Size(82, 17);
        this.radioAttrSys.TabIndex = 1;
        this.radioAttrSys.Text = "системный";
        this.radioAttrSys.UseVisualStyleBackColor = true;
        this.radioAttrSys.CheckedChanged += new System.EventHandler(this.SaveSetAttribute_CheckedChanged);
        // 
        // radioAttrReadOnly
        // 
        this.radioAttrReadOnly.AutoSize = true;
        this.radioAttrReadOnly.Location = new System.Drawing.Point(6, 36);
        this.radioAttrReadOnly.Name = "radioAttrReadOnly";
        this.radioAttrReadOnly.Size = new System.Drawing.Size(118, 17);
        this.radioAttrReadOnly.TabIndex = 2;
        this.radioAttrReadOnly.Text = "только для чтения";
        this.radioAttrReadOnly.UseVisualStyleBackColor = true;
        this.radioAttrReadOnly.CheckedChanged += new System.EventHandler(this.SaveSetAttribute_CheckedChanged);
        // 
        // radioAttrNo
        // 
        this.radioAttrNo.AutoSize = true;
        this.radioAttrNo.Checked = true;
        this.radioAttrNo.Location = new System.Drawing.Point(6, 13);
        this.radioAttrNo.Name = "radioAttrNo";
        this.radioAttrNo.Size = new System.Drawing.Size(101, 17);
        this.radioAttrNo.TabIndex = 0;
        this.radioAttrNo.TabStop = true;
        this.radioAttrNo.Text = "нет аттрибутов";
        this.radioAttrNo.UseVisualStyleBackColor = true;
        this.radioAttrNo.CheckedChanged += new System.EventHandler(this.SaveSetAttribute_CheckedChanged);
        // 
        // menuStrip1
        // 
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(683, 24);
        this.menuStrip1.TabIndex = 19;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // файлToolStripMenuItem
        // 
        this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.выходToolStripMenuItem});
        this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
        this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
        this.файлToolStripMenuItem.Text = "&Файл";
        // 
        // выходToolStripMenuItem
        // 
        this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
        this.выходToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
        this.выходToolStripMenuItem.Text = "Выход";
        this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
        // 
        // информацияToolStripMenuItem
        // 
        this.информацияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem});
        this.информацияToolStripMenuItem.Name = "информацияToolStripMenuItem";
        this.информацияToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
        this.информацияToolStripMenuItem.Text = "&Информация";
        // 
        // оПрограммеToolStripMenuItem
        // 
        this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
        this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
        this.оПрограммеToolStripMenuItem.Text = "&О программе...";
        this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
        // 
        // comboConvertion
        // 
        this.comboConvertion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.comboConvertion.FormattingEnabled = true;
        this.comboConvertion.Items.AddRange(new object[] {
            "НАЗВАНИЕ_ФАЙЛА.РАСШ -> Название файла.расш",
            "1 НАЗВАНИЕ_ФАЙЛА.РАСШ -> 1 Название файла.расш",
            "NAZVANIE_FAILA.RASSH -> Название файла.rassh",
            "1 NAZVANIE_FAILA.RASSH -> 1 Название файла.rassh"});
        this.comboConvertion.Location = new System.Drawing.Point(9, 122);
        this.comboConvertion.Name = "comboConvertion";
        this.comboConvertion.Size = new System.Drawing.Size(530, 21);
        this.comboConvertion.TabIndex = 10;
        this.comboConvertion.SelectedIndexChanged += new System.EventHandler(this.comboConvertion_SelectedIndexChanged);
        // 
        // buttonCopyInBuffer
        // 
        this.buttonCopyInBuffer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.buttonCopyInBuffer.Location = new System.Drawing.Point(28, 19);
        this.buttonCopyInBuffer.Name = "buttonCopyInBuffer";
        this.buttonCopyInBuffer.Size = new System.Drawing.Size(293, 26);
        this.buttonCopyInBuffer.TabIndex = 21;
        this.buttonCopyInBuffer.Text = "Скопировать в буфер";
        this.buttonCopyInBuffer.UseVisualStyleBackColor = true;
        this.buttonCopyInBuffer.Click += new System.EventHandler(this.buttonCopyInBuffer_Click);
        // 
        // groupBox3
        // 
        this.groupBox3.Controls.Add(this.radioFolderName);
        this.groupBox3.Controls.Add(this.radioFileNameWithExtentions);
        this.groupBox3.Controls.Add(this.radioFileName);
        this.groupBox3.Location = new System.Drawing.Point(9, 51);
        this.groupBox3.Name = "groupBox3";
        this.groupBox3.Size = new System.Drawing.Size(324, 60);
        this.groupBox3.TabIndex = 22;
        this.groupBox3.TabStop = false;
        // 
        // radioFolderName
        // 
        this.radioFolderName.AutoSize = true;
        this.radioFolderName.Location = new System.Drawing.Point(6, 36);
        this.radioFolderName.Name = "radioFolderName";
        this.radioFolderName.Size = new System.Drawing.Size(90, 17);
        this.radioFolderName.TabIndex = 3;
        this.radioFolderName.Text = "имена папок";
        this.radioFolderName.UseVisualStyleBackColor = true;
        this.radioFolderName.CheckedChanged += new System.EventHandler(this.SaveCopyBuffer_CheckedChanged);
        // 
        // radioFileNameWithExtentions
        // 
        this.radioFileNameWithExtentions.AutoSize = true;
        this.radioFileNameWithExtentions.Location = new System.Drawing.Point(126, 13);
        this.radioFileNameWithExtentions.Name = "radioFileNameWithExtentions";
        this.radioFileNameWithExtentions.Size = new System.Drawing.Size(186, 17);
        this.radioFileNameWithExtentions.TabIndex = 2;
        this.radioFileNameWithExtentions.Text = "имена файлов с расширениями";
        this.radioFileNameWithExtentions.UseVisualStyleBackColor = true;
        this.radioFileNameWithExtentions.CheckedChanged += new System.EventHandler(this.SaveCopyBuffer_CheckedChanged);
        // 
        // radioFileName
        // 
        this.radioFileName.AutoSize = true;
        this.radioFileName.Checked = true;
        this.radioFileName.Location = new System.Drawing.Point(6, 13);
        this.radioFileName.Name = "radioFileName";
        this.radioFileName.Size = new System.Drawing.Size(98, 17);
        this.radioFileName.TabIndex = 0;
        this.radioFileName.TabStop = true;
        this.radioFileName.Text = "имена файлов";
        this.radioFileName.UseVisualStyleBackColor = true;
        this.radioFileName.CheckedChanged += new System.EventHandler(this.SaveCopyBuffer_CheckedChanged);
        // 
        // groupBox4
        // 
        this.groupBox4.Controls.Add(this.groupBox5);
        this.groupBox4.Controls.Add(this.groupBox3);
        this.groupBox4.Controls.Add(this.comboConvertion);
        this.groupBox4.Controls.Add(this.buttonExit);
        this.groupBox4.Controls.Add(this.buttonCopyInBuffer);
        this.groupBox4.Controls.Add(this.buttonLoverUpper);
        this.groupBox4.Controls.Add(this.groupBox2);
        this.groupBox4.Controls.Add(this.buttonAttributes);
        this.groupBox4.Location = new System.Drawing.Point(12, 288);
        this.groupBox4.Name = "groupBox4";
        this.groupBox4.Size = new System.Drawing.Size(659, 247);
        this.groupBox4.TabIndex = 23;
        this.groupBox4.TabStop = false;
        this.groupBox4.Text = "Дополнительные возможности";
        // 
        // groupBox5
        // 
        this.groupBox5.Controls.Add(this.radioModifyNumeration);
        this.groupBox5.Controls.Add(this.labelMinCountDigits);
        this.groupBox5.Controls.Add(this.buttonNumeration);
        this.groupBox5.Controls.Add(this.labelStartNumber);
        this.groupBox5.Controls.Add(this.radioRemoveNumeration);
        this.groupBox5.Controls.Add(this.textMinCountDigits);
        this.groupBox5.Controls.Add(this.textStartNumber);
        this.groupBox5.Controls.Add(this.radioDoNumeration);
        this.groupBox5.Location = new System.Drawing.Point(0, 151);
        this.groupBox5.Name = "groupBox5";
        this.groupBox5.Size = new System.Drawing.Size(539, 91);
        this.groupBox5.TabIndex = 27;
        this.groupBox5.TabStop = false;
        // 
        // radioModifyNumeration
        // 
        this.radioModifyNumeration.AutoSize = true;
        this.radioModifyNumeration.Location = new System.Drawing.Point(154, 13);
        this.radioModifyNumeration.Name = "radioModifyNumeration";
        this.radioModifyNumeration.Size = new System.Drawing.Size(102, 17);
        this.radioModifyNumeration.TabIndex = 29;
        this.radioModifyNumeration.Text = "преобразовать";
        this.radioModifyNumeration.UseVisualStyleBackColor = true;
        // 
        // labelMinCountDigits
        // 
        this.labelMinCountDigits.AutoSize = true;
        this.labelMinCountDigits.Location = new System.Drawing.Point(163, 36);
        this.labelMinCountDigits.Name = "labelMinCountDigits";
        this.labelMinCountDigits.Size = new System.Drawing.Size(94, 26);
        this.labelMinCountDigits.TabIndex = 28;
        this.labelMinCountDigits.Text = "Минимальное\r\nколичество цифр";
        this.labelMinCountDigits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // buttonNumeration
        // 
        this.buttonNumeration.Location = new System.Drawing.Point(437, 33);
        this.buttonNumeration.Name = "buttonNumeration";
        this.buttonNumeration.Size = new System.Drawing.Size(96, 29);
        this.buttonNumeration.TabIndex = 23;
        this.buttonNumeration.Text = "Преобразовать";
        this.buttonNumeration.UseVisualStyleBackColor = true;
        this.buttonNumeration.Click += new System.EventHandler(this.buttonNumeration_Click);
        // 
        // labelStartNumber
        // 
        this.labelStartNumber.AutoSize = true;
        this.labelStartNumber.Location = new System.Drawing.Point(51, 36);
        this.labelStartNumber.Name = "labelStartNumber";
        this.labelStartNumber.Size = new System.Drawing.Size(62, 26);
        this.labelStartNumber.TabIndex = 27;
        this.labelStartNumber.Text = "Стартовый\r\nномер";
        this.labelStartNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // radioRemoveNumeration
        // 
        this.radioRemoveNumeration.AutoSize = true;
        this.radioRemoveNumeration.Location = new System.Drawing.Point(298, 13);
        this.radioRemoveNumeration.Name = "radioRemoveNumeration";
        this.radioRemoveNumeration.Size = new System.Drawing.Size(119, 17);
        this.radioRemoveNumeration.TabIndex = 3;
        this.radioRemoveNumeration.Text = "убрать нумерацию";
        this.radioRemoveNumeration.UseVisualStyleBackColor = true;
        this.radioRemoveNumeration.CheckedChanged += new System.EventHandler(this.SetNumeration_CheckedChanged);
        // 
        // textMinCountDigits
        // 
        this.textMinCountDigits.Location = new System.Drawing.Point(184, 65);
        this.textMinCountDigits.Name = "textMinCountDigits";
        this.textMinCountDigits.Size = new System.Drawing.Size(55, 20);
        this.textMinCountDigits.TabIndex = 26;
        this.textMinCountDigits.Text = "2";
        this.textMinCountDigits.TextChanged += new System.EventHandler(this.textMinCountDigits_TextChanged);
        // 
        // textStartNumber
        // 
        this.textStartNumber.Location = new System.Drawing.Point(54, 63);
        this.textStartNumber.Name = "textStartNumber";
        this.textStartNumber.Size = new System.Drawing.Size(55, 20);
        this.textStartNumber.TabIndex = 25;
        this.textStartNumber.Text = "1";
        this.textStartNumber.TextChanged += new System.EventHandler(this.textStartNumber_TextChanged);
        // 
        // radioDoNumeration
        // 
        this.radioDoNumeration.AutoSize = true;
        this.radioDoNumeration.Checked = true;
        this.radioDoNumeration.Location = new System.Drawing.Point(6, 13);
        this.radioDoNumeration.Name = "radioDoNumeration";
        this.radioDoNumeration.Size = new System.Drawing.Size(103, 17);
        this.radioDoNumeration.TabIndex = 0;
        this.radioDoNumeration.TabStop = true;
        this.radioDoNumeration.Text = "пронумеровать";
        this.radioDoNumeration.UseVisualStyleBackColor = true;
        this.radioDoNumeration.CheckedChanged += new System.EventHandler(this.SetNumeration_CheckedChanged);
        // 
        // label5
        // 
        this.label5.AutoSize = true;
        this.label5.Location = new System.Drawing.Point(601, 59);
        this.label5.Name = "label5";
        this.label5.Size = new System.Drawing.Size(62, 13);
        this.label5.TabIndex = 24;
        this.label5.Text = "Версия 3.0";
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(683, 540);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.groupBox4);
        this.Controls.Add(this.groupBox1);
        this.Controls.Add(this.checkNestingFolder);
        this.Controls.Add(this.textPath);
        this.Controls.Add(this.buttonPath);
        this.Controls.Add(this.menuStrip1);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MainMenuStrip = this.menuStrip1;
        this.MaximizeBox = false;
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Замена имен файлов";
        this.Load += new System.EventHandler(this.MainForm_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.groupBox3.ResumeLayout(false);
        this.groupBox3.PerformLayout();
        this.groupBox4.ResumeLayout(false);
        this.groupBox5.ResumeLayout(false);
        this.groupBox5.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    private System.Windows.Forms.Button buttonPath;
    private System.Windows.Forms.TextBox textPath;
    private System.Windows.Forms.TextBox textFindString;
    private System.Windows.Forms.TextBox textReplaceString;
    private System.Windows.Forms.Button buttonStartReplace;
    private System.Windows.Forms.CheckBox checkNestingFolder;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button buttonExit;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.RadioButton radioCheckExp;
    private System.Windows.Forms.RadioButton radioCheckFile;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.ComboBox comboReplaceTo;
    private System.Windows.Forms.Button buttonAttributes;
    private System.Windows.Forms.Button buttonLoverUpper;
    private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.RadioButton radioAttrHide;
    private System.Windows.Forms.RadioButton radioAttrSys;
    private System.Windows.Forms.RadioButton radioAttrReadOnly;
    private System.Windows.Forms.RadioButton radioAttrNo;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem информацияToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
    private System.Windows.Forms.ComboBox comboConvertion;
    private System.Windows.Forms.RadioButton radioAttrArchive;
    private System.Windows.Forms.RadioButton radioAttrTemporary;
      private System.Windows.Forms.RadioButton radioCheckBody;
    private System.Windows.Forms.ProgressBar progressBarInfo;
    private System.Windows.Forms.Label labelInfo;
    private System.Windows.Forms.Button buttonCopyInBuffer;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.RadioButton radioFolderName;
    private System.Windows.Forms.RadioButton radioFileNameWithExtentions;
    private System.Windows.Forms.RadioButton radioFileName;
    private System.Windows.Forms.GroupBox groupBox4;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.GroupBox groupBox5;
    private System.Windows.Forms.RadioButton radioRemoveNumeration;
    private System.Windows.Forms.TextBox textMinCountDigits;
    private System.Windows.Forms.TextBox textStartNumber;
    private System.Windows.Forms.RadioButton radioDoNumeration;
    private System.Windows.Forms.Button buttonNumeration;
    private System.Windows.Forms.Label labelMinCountDigits;
    private System.Windows.Forms.Label labelStartNumber;
    private System.Windows.Forms.RadioButton radioModifyNumeration;
  }
}

