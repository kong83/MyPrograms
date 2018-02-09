namespace Fonotec
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
            this.listBoxDiskNumber = new System.Windows.Forms.ListBox();
            this.listBoxFilmName = new System.Windows.Forms.ListBox();
            this.textBoxFilmInfo = new System.Windows.Forms.TextBox();
            this.labelDisk = new System.Windows.Forms.Label();
            this.labelFilm = new System.Windows.Forms.Label();
            this.textBoxDiskInfo = new System.Windows.Forms.TextBox();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.textBoxFilterFilmName = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonSetting = new System.Windows.Forms.Button();
            this.buttonFindFilm = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonDiskDecrease = new System.Windows.Forms.Button();
            this.buttonDiskIncrease = new System.Windows.Forms.Button();
            this.buttonFilmDecrease = new System.Windows.Forms.Button();
            this.buttonFilmIncrease = new System.Windows.Forms.Button();
            this.buttonFilmDelete = new System.Windows.Forms.Button();
            this.buttonFilmEdit = new System.Windows.Forms.Button();
            this.buttonFilmAdd = new System.Windows.Forms.Button();
            this.buttonDiskDelete = new System.Windows.Forms.Button();
            this.buttonDiskEdit = new System.Windows.Forms.Button();
            this.buttonDiskAdd = new System.Windows.Forms.Button();
            this.comboBoxFilterGenre = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // listBoxDiskNumber
            // 
            this.listBoxDiskNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.listBoxDiskNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxDiskNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxDiskNumber.FormattingEnabled = true;
            this.listBoxDiskNumber.HorizontalScrollbar = true;
            this.listBoxDiskNumber.IntegralHeight = false;
            this.listBoxDiskNumber.ItemHeight = 16;
            this.listBoxDiskNumber.Location = new System.Drawing.Point(51, 35);
            this.listBoxDiskNumber.Name = "listBoxDiskNumber";
            this.listBoxDiskNumber.Size = new System.Drawing.Size(52, 466);
            this.listBoxDiskNumber.TabIndex = 10;
            this.listBoxDiskNumber.SelectedIndexChanged += new System.EventHandler(this.listBoxDiskNumber_SelectedIndexChanged);
            this.listBoxDiskNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            // 
            // listBoxFilmName
            // 
            this.listBoxFilmName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxFilmName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listBoxFilmName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxFilmName.FormattingEnabled = true;
            this.listBoxFilmName.HorizontalScrollbar = true;
            this.listBoxFilmName.IntegralHeight = false;
            this.listBoxFilmName.ItemHeight = 16;
            this.listBoxFilmName.Location = new System.Drawing.Point(274, 62);
            this.listBoxFilmName.Name = "listBoxFilmName";
            this.listBoxFilmName.Size = new System.Drawing.Size(227, 407);
            this.listBoxFilmName.TabIndex = 30;
            this.listBoxFilmName.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listBoxFilmName_MouseDoubleClick);
            this.listBoxFilmName.SelectedIndexChanged += new System.EventHandler(this.listBoxFilmName_SelectedIndexChanged);
            this.listBoxFilmName.MouseEnter += new System.EventHandler(this.listBoxFilmName_MouseEnter);
            this.listBoxFilmName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.listBoxFilmName.MouseLeave += new System.EventHandler(this.listBoxFilmName_MouseLeave);
            // 
            // textBoxFilmInfo
            // 
            this.textBoxFilmInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilmInfo.BackColor = System.Drawing.Color.White;
            this.textBoxFilmInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxFilmInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFilmInfo.Location = new System.Drawing.Point(511, 35);
            this.textBoxFilmInfo.MaxLength = 2000000000;
            this.textBoxFilmInfo.Multiline = true;
            this.textBoxFilmInfo.Name = "textBoxFilmInfo";
            this.textBoxFilmInfo.ReadOnly = true;
            this.textBoxFilmInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxFilmInfo.Size = new System.Drawing.Size(263, 222);
            this.textBoxFilmInfo.TabIndex = 31;
            this.textBoxFilmInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            // 
            // labelDisk
            // 
            this.labelDisk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDisk.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDisk.Location = new System.Drawing.Point(10, 9);
            this.labelDisk.Name = "labelDisk";
            this.labelDisk.Size = new System.Drawing.Size(180, 20);
            this.labelDisk.TabIndex = 14;
            this.labelDisk.Text = "Диск";
            this.labelDisk.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelFilm
            // 
            this.labelFilm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelFilm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelFilm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelFilm.Location = new System.Drawing.Point(225, 9);
            this.labelFilm.Name = "labelFilm";
            this.labelFilm.Size = new System.Drawing.Size(545, 20);
            this.labelFilm.TabIndex = 15;
            this.labelFilm.Text = "                                                 Фильм";
            this.labelFilm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDiskInfo
            // 
            this.textBoxDiskInfo.BackColor = System.Drawing.Color.White;
            this.textBoxDiskInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxDiskInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDiskInfo.Location = new System.Drawing.Point(109, 35);
            this.textBoxDiskInfo.MaxLength = 2000000000;
            this.textBoxDiskInfo.Multiline = true;
            this.textBoxDiskInfo.Name = "textBoxDiskInfo";
            this.textBoxDiskInfo.ReadOnly = true;
            this.textBoxDiskInfo.Size = new System.Drawing.Size(81, 137);
            this.textBoxDiskInfo.TabIndex = 11;
            this.textBoxDiskInfo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Location = new System.Drawing.Point(205, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(2, 499);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // textBoxFilterFilmName
            // 
            this.textBoxFilterFilmName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFilterFilmName.Location = new System.Drawing.Point(274, 481);
            this.textBoxFilterFilmName.Name = "textBoxFilterFilmName";
            this.textBoxFilterFilmName.Size = new System.Drawing.Size(227, 20);
            this.textBoxFilterFilmName.TabIndex = 35;
            this.textBoxFilterFilmName.TextChanged += new System.EventHandler(this.textBoxFilterFilmName_TextChanged);
            // 
            // timer
            // 
            this.timer.Interval = 500;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(117, 197);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(62, 13);
            this.labelVersion.TabIndex = 38;
            this.labelVersion.Text = "Версия 2.0";
            // 
            // buttonExport
            // 
            this.buttonExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExport.BackgroundImage = global::Fonotec.Properties.Resources.ExportToExcel;
            this.buttonExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExport.FlatAppearance.BorderSize = 0;
            this.buttonExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExport.Location = new System.Drawing.Point(564, 271);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(35, 35);
            this.buttonExport.TabIndex = 40;
            this.buttonExport.TabStop = false;
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.MouseLeave += new System.EventHandler(this.buttonExport_MouseLeave);
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            this.buttonExport.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonExport.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonExport.MouseEnter += new System.EventHandler(this.buttonExport_MouseEnter);
            // 
            // buttonSetting
            // 
            this.buttonSetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSetting.BackgroundImage = global::Fonotec.Properties.Resources.Setting;
            this.buttonSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSetting.FlatAppearance.BorderSize = 0;
            this.buttonSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSetting.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSetting.Location = new System.Drawing.Point(617, 271);
            this.buttonSetting.Name = "buttonSetting";
            this.buttonSetting.Size = new System.Drawing.Size(35, 35);
            this.buttonSetting.TabIndex = 39;
            this.buttonSetting.TabStop = false;
            this.buttonSetting.UseVisualStyleBackColor = true;
            this.buttonSetting.MouseLeave += new System.EventHandler(this.buttonSetting_MouseLeave);
            this.buttonSetting.Click += new System.EventHandler(this.buttonSetting_Click);
            this.buttonSetting.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonSetting.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonSetting.MouseEnter += new System.EventHandler(this.buttonSetting_MouseEnter);
            // 
            // buttonFindFilm
            // 
            this.buttonFindFilm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonFindFilm.BackgroundImage = global::Fonotec.Properties.Resources.find;
            this.buttonFindFilm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFindFilm.FlatAppearance.BorderSize = 0;
            this.buttonFindFilm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFindFilm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFindFilm.Location = new System.Drawing.Point(676, 271);
            this.buttonFindFilm.Name = "buttonFindFilm";
            this.buttonFindFilm.Size = new System.Drawing.Size(35, 35);
            this.buttonFindFilm.TabIndex = 37;
            this.buttonFindFilm.TabStop = false;
            this.buttonFindFilm.UseVisualStyleBackColor = true;
            this.buttonFindFilm.MouseLeave += new System.EventHandler(this.buttonFindFilm_MouseLeave);
            this.buttonFindFilm.Click += new System.EventHandler(this.buttonFindFilm_Click);
            this.buttonFindFilm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonFindFilm.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFindFilm.MouseEnter += new System.EventHandler(this.buttonFindFilm_MouseEnter);
            // 
            // buttonExit
            // 
            this.buttonExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExit.BackgroundImage = global::Fonotec.Properties.Resources.Exit;
            this.buttonExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(735, 271);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(35, 35);
            this.buttonExit.TabIndex = 36;
            this.buttonExit.TabStop = false;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.MouseLeave += new System.EventHandler(this.buttonExit_MouseLeave);
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonExit.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonExit.MouseEnter += new System.EventHandler(this.buttonExit_MouseEnter);
            // 
            // buttonDiskDecrease
            // 
            this.buttonDiskDecrease.BackgroundImage = global::Fonotec.Properties.Resources.down;
            this.buttonDiskDecrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDiskDecrease.FlatAppearance.BorderSize = 0;
            this.buttonDiskDecrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiskDecrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDiskDecrease.Location = new System.Drawing.Point(10, 76);
            this.buttonDiskDecrease.Name = "buttonDiskDecrease";
            this.buttonDiskDecrease.Size = new System.Drawing.Size(35, 35);
            this.buttonDiskDecrease.TabIndex = 1;
            this.buttonDiskDecrease.TabStop = false;
            this.buttonDiskDecrease.UseVisualStyleBackColor = true;
            this.buttonDiskDecrease.MouseLeave += new System.EventHandler(this.buttonDiskDecrease_MouseLeave);
            this.buttonDiskDecrease.Click += new System.EventHandler(this.buttonDiskDecrease_Click);
            this.buttonDiskDecrease.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonDiskDecrease.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDiskDecrease.MouseEnter += new System.EventHandler(this.buttonDiskDecrease_MouseEnter);
            // 
            // buttonDiskIncrease
            // 
            this.buttonDiskIncrease.BackgroundImage = global::Fonotec.Properties.Resources.up;
            this.buttonDiskIncrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDiskIncrease.FlatAppearance.BorderSize = 0;
            this.buttonDiskIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiskIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDiskIncrease.Location = new System.Drawing.Point(10, 35);
            this.buttonDiskIncrease.Name = "buttonDiskIncrease";
            this.buttonDiskIncrease.Size = new System.Drawing.Size(35, 35);
            this.buttonDiskIncrease.TabIndex = 0;
            this.buttonDiskIncrease.TabStop = false;
            this.buttonDiskIncrease.UseVisualStyleBackColor = true;
            this.buttonDiskIncrease.MouseLeave += new System.EventHandler(this.buttonDiskIncrease_MouseLeave);
            this.buttonDiskIncrease.Click += new System.EventHandler(this.buttonDiskIncrease_Click);
            this.buttonDiskIncrease.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonDiskIncrease.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDiskIncrease.MouseEnter += new System.EventHandler(this.buttonDiskIncrease_MouseEnter);
            // 
            // buttonFilmDecrease
            // 
            this.buttonFilmDecrease.BackgroundImage = global::Fonotec.Properties.Resources.down;
            this.buttonFilmDecrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilmDecrease.FlatAppearance.BorderSize = 0;
            this.buttonFilmDecrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilmDecrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFilmDecrease.Location = new System.Drawing.Point(225, 76);
            this.buttonFilmDecrease.Name = "buttonFilmDecrease";
            this.buttonFilmDecrease.Size = new System.Drawing.Size(35, 35);
            this.buttonFilmDecrease.TabIndex = 21;
            this.buttonFilmDecrease.TabStop = false;
            this.buttonFilmDecrease.UseVisualStyleBackColor = true;
            this.buttonFilmDecrease.MouseLeave += new System.EventHandler(this.buttonFilmDecrease_MouseLeave);
            this.buttonFilmDecrease.Click += new System.EventHandler(this.buttonFilmDecrease_Click);
            this.buttonFilmDecrease.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonFilmDecrease.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFilmDecrease.MouseEnter += new System.EventHandler(this.buttonFilmDecrease_MouseEnter);
            // 
            // buttonFilmIncrease
            // 
            this.buttonFilmIncrease.BackgroundImage = global::Fonotec.Properties.Resources.up;
            this.buttonFilmIncrease.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilmIncrease.FlatAppearance.BorderSize = 0;
            this.buttonFilmIncrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilmIncrease.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFilmIncrease.Location = new System.Drawing.Point(225, 35);
            this.buttonFilmIncrease.Name = "buttonFilmIncrease";
            this.buttonFilmIncrease.Size = new System.Drawing.Size(35, 35);
            this.buttonFilmIncrease.TabIndex = 20;
            this.buttonFilmIncrease.TabStop = false;
            this.buttonFilmIncrease.UseVisualStyleBackColor = true;
            this.buttonFilmIncrease.MouseLeave += new System.EventHandler(this.buttonFilmIncrease_MouseLeave);
            this.buttonFilmIncrease.Click += new System.EventHandler(this.buttonFilmIncrease_Click);
            this.buttonFilmIncrease.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonFilmIncrease.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFilmIncrease.MouseEnter += new System.EventHandler(this.buttonFilmIncrease_MouseEnter);
            // 
            // buttonFilmDelete
            // 
            this.buttonFilmDelete.BackgroundImage = global::Fonotec.Properties.Resources.delete;
            this.buttonFilmDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilmDelete.FlatAppearance.BorderSize = 0;
            this.buttonFilmDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilmDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFilmDelete.Location = new System.Drawing.Point(225, 235);
            this.buttonFilmDelete.Name = "buttonFilmDelete";
            this.buttonFilmDelete.Size = new System.Drawing.Size(35, 35);
            this.buttonFilmDelete.TabIndex = 24;
            this.buttonFilmDelete.TabStop = false;
            this.buttonFilmDelete.UseVisualStyleBackColor = true;
            this.buttonFilmDelete.MouseLeave += new System.EventHandler(this.buttonFilmDelete_MouseLeave);
            this.buttonFilmDelete.Click += new System.EventHandler(this.buttonFilmDelete_Click);
            this.buttonFilmDelete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonFilmDelete.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFilmDelete.MouseEnter += new System.EventHandler(this.buttonFilmDelete_MouseEnter);
            // 
            // buttonFilmEdit
            // 
            this.buttonFilmEdit.BackgroundImage = global::Fonotec.Properties.Resources.edit;
            this.buttonFilmEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilmEdit.FlatAppearance.BorderSize = 0;
            this.buttonFilmEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilmEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFilmEdit.Location = new System.Drawing.Point(225, 186);
            this.buttonFilmEdit.Name = "buttonFilmEdit";
            this.buttonFilmEdit.Size = new System.Drawing.Size(35, 35);
            this.buttonFilmEdit.TabIndex = 23;
            this.buttonFilmEdit.TabStop = false;
            this.buttonFilmEdit.UseVisualStyleBackColor = true;
            this.buttonFilmEdit.MouseLeave += new System.EventHandler(this.buttonFilmEdit_MouseLeave);
            this.buttonFilmEdit.Click += new System.EventHandler(this.buttonFilmEdit_Click);
            this.buttonFilmEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonFilmEdit.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFilmEdit.MouseEnter += new System.EventHandler(this.buttonFilmEdit_MouseEnter);
            // 
            // buttonFilmAdd
            // 
            this.buttonFilmAdd.BackgroundImage = global::Fonotec.Properties.Resources.add;
            this.buttonFilmAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilmAdd.FlatAppearance.BorderSize = 0;
            this.buttonFilmAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilmAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFilmAdd.Location = new System.Drawing.Point(225, 137);
            this.buttonFilmAdd.Name = "buttonFilmAdd";
            this.buttonFilmAdd.Size = new System.Drawing.Size(35, 35);
            this.buttonFilmAdd.TabIndex = 22;
            this.buttonFilmAdd.TabStop = false;
            this.buttonFilmAdd.UseVisualStyleBackColor = true;
            this.buttonFilmAdd.MouseLeave += new System.EventHandler(this.buttonFilmAdd_MouseLeave);
            this.buttonFilmAdd.Click += new System.EventHandler(this.buttonFilmAdd_Click);
            this.buttonFilmAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonFilmAdd.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonFilmAdd.MouseEnter += new System.EventHandler(this.buttonFilmAdd_MouseEnter);
            // 
            // buttonDiskDelete
            // 
            this.buttonDiskDelete.BackgroundImage = global::Fonotec.Properties.Resources.delete;
            this.buttonDiskDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDiskDelete.FlatAppearance.BorderSize = 0;
            this.buttonDiskDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiskDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDiskDelete.Location = new System.Drawing.Point(10, 235);
            this.buttonDiskDelete.Name = "buttonDiskDelete";
            this.buttonDiskDelete.Size = new System.Drawing.Size(35, 35);
            this.buttonDiskDelete.TabIndex = 4;
            this.buttonDiskDelete.TabStop = false;
            this.buttonDiskDelete.UseVisualStyleBackColor = true;
            this.buttonDiskDelete.MouseLeave += new System.EventHandler(this.buttonDiskDelete_MouseLeave);
            this.buttonDiskDelete.Click += new System.EventHandler(this.buttonDiskDelete_Click);
            this.buttonDiskDelete.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonDiskDelete.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDiskDelete.MouseEnter += new System.EventHandler(this.buttonDiskDelete_MouseEnter);
            // 
            // buttonDiskEdit
            // 
            this.buttonDiskEdit.BackgroundImage = global::Fonotec.Properties.Resources.edit;
            this.buttonDiskEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDiskEdit.FlatAppearance.BorderSize = 0;
            this.buttonDiskEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiskEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDiskEdit.Location = new System.Drawing.Point(10, 186);
            this.buttonDiskEdit.Name = "buttonDiskEdit";
            this.buttonDiskEdit.Size = new System.Drawing.Size(35, 35);
            this.buttonDiskEdit.TabIndex = 3;
            this.buttonDiskEdit.TabStop = false;
            this.buttonDiskEdit.UseVisualStyleBackColor = true;
            this.buttonDiskEdit.MouseLeave += new System.EventHandler(this.buttonDiskEdit_MouseLeave);
            this.buttonDiskEdit.Click += new System.EventHandler(this.buttonDiskEdit_Click);
            this.buttonDiskEdit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonDiskEdit.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDiskEdit.MouseEnter += new System.EventHandler(this.buttonDiskEdit_MouseEnter);
            // 
            // buttonDiskAdd
            // 
            this.buttonDiskAdd.BackgroundImage = global::Fonotec.Properties.Resources.add;
            this.buttonDiskAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDiskAdd.FlatAppearance.BorderSize = 0;
            this.buttonDiskAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDiskAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDiskAdd.Location = new System.Drawing.Point(10, 137);
            this.buttonDiskAdd.Name = "buttonDiskAdd";
            this.buttonDiskAdd.Size = new System.Drawing.Size(35, 35);
            this.buttonDiskAdd.TabIndex = 2;
            this.buttonDiskAdd.TabStop = false;
            this.buttonDiskAdd.UseVisualStyleBackColor = true;
            this.buttonDiskAdd.MouseLeave += new System.EventHandler(this.buttonDiskAdd_MouseLeave);
            this.buttonDiskAdd.Click += new System.EventHandler(this.buttonDiskAdd_Click);
            this.buttonDiskAdd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.buttonDiskAdd.Enter += new System.EventHandler(this.Drop_Focus);
            this.buttonDiskAdd.MouseEnter += new System.EventHandler(this.buttonDiskAdd_MouseEnter);
            // 
            // comboBoxFilterGenre
            // 
            this.comboBoxFilterGenre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxFilterGenre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterGenre.FormattingEnabled = true;
            this.comboBoxFilterGenre.Items.AddRange(new object[] {
            "Все жанры",
            "Боевики",
            "Комедии",
            "Мультики",
            "Фэнтэзи",
            "Мелодрамы",
            "Фантастика",
            "Триллеры",
            "Ужасы",
            "Детективы",
            "Сказки",
            "Исторические",
            "Про жизнь",
            "Другие"});
            this.comboBoxFilterGenre.Location = new System.Drawing.Point(274, 35);
            this.comboBoxFilterGenre.Name = "comboBoxFilterGenre";
            this.comboBoxFilterGenre.Size = new System.Drawing.Size(227, 21);
            this.comboBoxFilterGenre.TabIndex = 41;
            this.comboBoxFilterGenre.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterGenre_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 508);
            this.Controls.Add(this.comboBoxFilterGenre);
            this.Controls.Add(this.buttonExport);
            this.Controls.Add(this.buttonSetting);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.buttonFindFilm);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBoxFilterFilmName);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonDiskDecrease);
            this.Controls.Add(this.buttonDiskIncrease);
            this.Controls.Add(this.buttonFilmDecrease);
            this.Controls.Add(this.buttonFilmIncrease);
            this.Controls.Add(this.textBoxDiskInfo);
            this.Controls.Add(this.labelFilm);
            this.Controls.Add(this.labelDisk);
            this.Controls.Add(this.buttonFilmDelete);
            this.Controls.Add(this.buttonFilmEdit);
            this.Controls.Add(this.buttonFilmAdd);
            this.Controls.Add(this.buttonDiskDelete);
            this.Controls.Add(this.buttonDiskEdit);
            this.Controls.Add(this.buttonDiskAdd);
            this.Controls.Add(this.textBoxFilmInfo);
            this.Controls.Add(this.listBoxFilmName);
            this.Controls.Add(this.listBoxDiskNumber);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(670, 350);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Библиотека фильмов";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.MainForm_InputLanguageChanged);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.StartFilteringByKeyDown);
            this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxDiskNumber;
        private System.Windows.Forms.ListBox listBoxFilmName;
        private System.Windows.Forms.TextBox textBoxFilmInfo;
        private System.Windows.Forms.Button buttonDiskAdd;
        private System.Windows.Forms.Button buttonDiskEdit;
        private System.Windows.Forms.Button buttonDiskDelete;
        private System.Windows.Forms.Button buttonFilmDelete;
        private System.Windows.Forms.Button buttonFilmEdit;
        private System.Windows.Forms.Button buttonFilmAdd;
        private System.Windows.Forms.Label labelDisk;
        private System.Windows.Forms.Label labelFilm;
        private System.Windows.Forms.TextBox textBoxDiskInfo;
        private System.Windows.Forms.Button buttonFilmIncrease;
        private System.Windows.Forms.Button buttonFilmDecrease;
        private System.Windows.Forms.Button buttonDiskDecrease;
        private System.Windows.Forms.Button buttonDiskIncrease;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxFilterFilmName;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonFindFilm;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonSetting;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.ComboBox comboBoxFilterGenre;
    }
}

