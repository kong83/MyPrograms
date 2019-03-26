namespace DatabaseWorker
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBoxBackup = new System.Windows.Forms.GroupBox();
            this.comboBoxBackupDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonBackupDatabase = new System.Windows.Forms.Button();
            this.textBoxPathBackup = new System.Windows.Forms.TextBox();
            this.labelPathBackup = new System.Windows.Forms.Label();
            this.buttonOpenBackupDatabase = new System.Windows.Forms.Button();
            this.labelBackupDatabaseName = new System.Windows.Forms.Label();
            this.checkBoxUseBackupCompression = new System.Windows.Forms.CheckBox();
            this.groupBoxRestore = new System.Windows.Forms.GroupBox();
            this.labelAction = new System.Windows.Forms.Label();
            this.checkBoxRestoreAllDb = new System.Windows.Forms.CheckBox();
            this.checkBoxSetTrustworthy = new System.Windows.Forms.CheckBox();
            this.checkBoxChangeDBOwner = new System.Windows.Forms.CheckBox();
            this.comboBoxRestoreDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonRestoreDatabase = new System.Windows.Forms.Button();
            this.textBoxPathRestore = new System.Windows.Forms.TextBox();
            this.labelPathRestore = new System.Windows.Forms.Label();
            this.buttonOpenRestoreDatabase = new System.Windows.Forms.Button();
            this.labelRestoreDatabaseName = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonExit = new System.Windows.Forms.Button();
            this.groupBoxServerSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxWindowsAuthorization = new System.Windows.Forms.CheckBox();
            this.buttonCheckConnection = new System.Windows.Forms.Button();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxServerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxLogin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxDrop = new System.Windows.Forms.GroupBox();
            this.checkBoxDropDatabaseStartWith = new System.Windows.Forms.CheckBox();
            this.comboBoxDropDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonDropDatabase = new System.Windows.Forms.Button();
            this.labelDropDatabaseName = new System.Windows.Forms.Label();
            this.groupBoxCopyDatabase = new System.Windows.Forms.GroupBox();
            this.checkBoxSetNewBroker = new System.Windows.Forms.CheckBox();
            this.checkBoxSetTrustworthyOn = new System.Windows.Forms.CheckBox();
            this.comboBoxToDatabaseName = new System.Windows.Forms.ComboBox();
            this.comboBoxFromDatabaseName = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonCopyDatabase = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.labelVersion = new System.Windows.Forms.Label();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.labelGetInfo = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBoxItemList = new System.Windows.Forms.ComboBox();
            this.textBoxListOfItems = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comboBoxSelectDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonGetListOfItems = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBoxRename = new System.Windows.Forms.GroupBox();
            this.labelReplaceInstruction = new System.Windows.Forms.Label();
            this.comboBoxReplaceString = new System.Windows.Forms.ComboBox();
            this.comboBoxFindString = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.buttonReplace = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBoxDetach = new System.Windows.Forms.GroupBox();
            this.comboBoxDetachDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonDetach = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBoxAttach = new System.Windows.Forms.GroupBox();
            this.textBoxPathLdf = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.buttonOpenLdfFile = new System.Windows.Forms.Button();
            this.comboBoxAttachDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonAttach = new System.Windows.Forms.Button();
            this.textBoxPathMdf = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonOpenMdfFile = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.comboBoxExecutedDatabaseName = new System.Windows.Forms.ComboBox();
            this.buttonExecuteSql = new System.Windows.Forms.Button();
            this.textBoxSqlCommand = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelCatiInstancesRemovingAction = new System.Windows.Forms.Label();
            this.textBoxFirstIndexForRemoving = new System.Windows.Forms.TextBox();
            this.textBoxLastIndexForRemoving = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.buttonRemoveCatiInstances = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonRemoveTestRuns = new System.Windows.Forms.Button();
            this.checkBoxSelectAllTestRuns = new System.Windows.Forms.CheckBox();
            this.buttonGetTestRuns = new System.Windows.Forms.Button();
            this.checkedListBoxTestRunList = new System.Windows.Forms.CheckedListBox();
            this.label19 = new System.Windows.Forms.Label();
            this.buttonProfilingReportGenerate = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.progressBarCATICreation = new System.Windows.Forms.ProgressBar();
            this.textBoxFirstIndex = new System.Windows.Forms.TextBox();
            this.textBoxLastIndex = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.buttonCreateCATIInstances = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxSetNewBrokerOnResore = new System.Windows.Forms.CheckBox();
            this.groupBoxBackup.SuspendLayout();
            this.groupBoxRestore.SuspendLayout();
            this.groupBoxServerSettings.SuspendLayout();
            this.groupBoxDrop.SuspendLayout();
            this.groupBoxCopyDatabase.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBoxRename.SuspendLayout();
            this.groupBoxDetach.SuspendLayout();
            this.groupBoxAttach.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBackup
            // 
            this.groupBoxBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBackup.Controls.Add(this.comboBoxBackupDatabaseName);
            this.groupBoxBackup.Controls.Add(this.buttonBackupDatabase);
            this.groupBoxBackup.Controls.Add(this.textBoxPathBackup);
            this.groupBoxBackup.Controls.Add(this.labelPathBackup);
            this.groupBoxBackup.Controls.Add(this.buttonOpenBackupDatabase);
            this.groupBoxBackup.Controls.Add(this.labelBackupDatabaseName);
            this.groupBoxBackup.Controls.Add(this.checkBoxUseBackupCompression);
            this.groupBoxBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxBackup.Location = new System.Drawing.Point(20, 9);
            this.groupBoxBackup.Name = "groupBoxBackup";
            this.groupBoxBackup.Size = new System.Drawing.Size(634, 86);
            this.groupBoxBackup.TabIndex = 2;
            this.groupBoxBackup.TabStop = false;
            this.groupBoxBackup.Text = "Backup database";
            // 
            // comboBoxBackupDatabaseName
            // 
            this.comboBoxBackupDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxBackupDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBackupDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxBackupDatabaseName.FormattingEnabled = true;
            this.comboBoxBackupDatabaseName.Location = new System.Drawing.Point(126, 17);
            this.comboBoxBackupDatabaseName.Name = "comboBoxBackupDatabaseName";
            this.comboBoxBackupDatabaseName.Size = new System.Drawing.Size(396, 21);
            this.comboBoxBackupDatabaseName.TabIndex = 7;
            this.comboBoxBackupDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // buttonBackupDatabase
            // 
            this.buttonBackupDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBackupDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBackupDatabase.Location = new System.Drawing.Point(543, 10);
            this.buttonBackupDatabase.Name = "buttonBackupDatabase";
            this.buttonBackupDatabase.Size = new System.Drawing.Size(84, 28);
            this.buttonBackupDatabase.TabIndex = 6;
            this.buttonBackupDatabase.Text = "Execute";
            this.buttonBackupDatabase.UseVisualStyleBackColor = true;
            this.buttonBackupDatabase.Click += new System.EventHandler(this.buttonBackupDatabase_Click);
            // 
            // textBoxPathBackup
            // 
            this.textBoxPathBackup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPathBackup.Location = new System.Drawing.Point(126, 44);
            this.textBoxPathBackup.Name = "textBoxPathBackup";
            this.textBoxPathBackup.Size = new System.Drawing.Size(464, 20);
            this.textBoxPathBackup.TabIndex = 0;
            // 
            // labelPathBackup
            // 
            this.labelPathBackup.AutoSize = true;
            this.labelPathBackup.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPathBackup.Location = new System.Drawing.Point(5, 47);
            this.labelPathBackup.Name = "labelPathBackup";
            this.labelPathBackup.Size = new System.Drawing.Size(96, 13);
            this.labelPathBackup.TabIndex = 3;
            this.labelPathBackup.Text = "Path to backup file";
            // 
            // buttonOpenBackupDatabase
            // 
            this.buttonOpenBackupDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenBackupDatabase.Location = new System.Drawing.Point(596, 42);
            this.buttonOpenBackupDatabase.Name = "buttonOpenBackupDatabase";
            this.buttonOpenBackupDatabase.Size = new System.Drawing.Size(31, 23);
            this.buttonOpenBackupDatabase.TabIndex = 2;
            this.buttonOpenBackupDatabase.Text = "...";
            this.buttonOpenBackupDatabase.UseVisualStyleBackColor = true;
            this.buttonOpenBackupDatabase.Click += new System.EventHandler(this.buttonOpenBackupDatabase_Click);
            // 
            // labelBackupDatabaseName
            // 
            this.labelBackupDatabaseName.AutoSize = true;
            this.labelBackupDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelBackupDatabaseName.Location = new System.Drawing.Point(5, 20);
            this.labelBackupDatabaseName.Name = "labelBackupDatabaseName";
            this.labelBackupDatabaseName.Size = new System.Drawing.Size(82, 13);
            this.labelBackupDatabaseName.TabIndex = 0;
            this.labelBackupDatabaseName.Text = "Database name";
            // 
            // checkBoxUseBackupCompression
            // 
            this.checkBoxUseBackupCompression.AutoSize = true;
            this.checkBoxUseBackupCompression.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxUseBackupCompression.Location = new System.Drawing.Point(126, 65);
            this.checkBoxUseBackupCompression.Name = "checkBoxUseBackupCompression";
            this.checkBoxUseBackupCompression.Size = new System.Drawing.Size(146, 17);
            this.checkBoxUseBackupCompression.TabIndex = 11;
            this.checkBoxUseBackupCompression.Text = "Use backup compression";
            this.checkBoxUseBackupCompression.UseVisualStyleBackColor = true;
            // 
            // groupBoxRestore
            // 
            this.groupBoxRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxRestore.Controls.Add(this.checkBoxSetNewBrokerOnResore);
            this.groupBoxRestore.Controls.Add(this.labelAction);
            this.groupBoxRestore.Controls.Add(this.checkBoxRestoreAllDb);
            this.groupBoxRestore.Controls.Add(this.checkBoxSetTrustworthy);
            this.groupBoxRestore.Controls.Add(this.checkBoxChangeDBOwner);
            this.groupBoxRestore.Controls.Add(this.comboBoxRestoreDatabaseName);
            this.groupBoxRestore.Controls.Add(this.buttonRestoreDatabase);
            this.groupBoxRestore.Controls.Add(this.textBoxPathRestore);
            this.groupBoxRestore.Controls.Add(this.labelPathRestore);
            this.groupBoxRestore.Controls.Add(this.buttonOpenRestoreDatabase);
            this.groupBoxRestore.Controls.Add(this.labelRestoreDatabaseName);
            this.groupBoxRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxRestore.Location = new System.Drawing.Point(20, 113);
            this.groupBoxRestore.Name = "groupBoxRestore";
            this.groupBoxRestore.Size = new System.Drawing.Size(634, 129);
            this.groupBoxRestore.TabIndex = 3;
            this.groupBoxRestore.TabStop = false;
            this.groupBoxRestore.Text = "Restore database";
            // 
            // labelAction
            // 
            this.labelAction.AutoSize = true;
            this.labelAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelAction.Location = new System.Drawing.Point(291, 107);
            this.labelAction.Name = "labelAction";
            this.labelAction.Size = new System.Drawing.Size(0, 13);
            this.labelAction.TabIndex = 15;
            // 
            // checkBoxRestoreAllDb
            // 
            this.checkBoxRestoreAllDb.AutoSize = true;
            this.checkBoxRestoreAllDb.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxRestoreAllDb.Location = new System.Drawing.Point(126, 106);
            this.checkBoxRestoreAllDb.Name = "checkBoxRestoreAllDb";
            this.checkBoxRestoreAllDb.Size = new System.Drawing.Size(151, 17);
            this.checkBoxRestoreAllDb.TabIndex = 14;
            this.checkBoxRestoreAllDb.Text = "Restore all DBs from folder";
            this.checkBoxRestoreAllDb.UseVisualStyleBackColor = true;
            this.checkBoxRestoreAllDb.CheckedChanged += new System.EventHandler(this.checkBoxRestoreAllDb_CheckedChanged);
            // 
            // checkBoxSetTrustworthy
            // 
            this.checkBoxSetTrustworthy.AutoSize = true;
            this.checkBoxSetTrustworthy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxSetTrustworthy.Location = new System.Drawing.Point(127, 88);
            this.checkBoxSetTrustworthy.Name = "checkBoxSetTrustworthy";
            this.checkBoxSetTrustworthy.Size = new System.Drawing.Size(135, 17);
            this.checkBoxSetTrustworthy.TabIndex = 13;
            this.checkBoxSetTrustworthy.Text = "Set \'Trustworthy\' to ON";
            this.checkBoxSetTrustworthy.UseVisualStyleBackColor = true;
            // 
            // checkBoxChangeDBOwner
            // 
            this.checkBoxChangeDBOwner.AutoSize = true;
            this.checkBoxChangeDBOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxChangeDBOwner.Location = new System.Drawing.Point(127, 69);
            this.checkBoxChangeDBOwner.Name = "checkBoxChangeDBOwner";
            this.checkBoxChangeDBOwner.Size = new System.Drawing.Size(113, 17);
            this.checkBoxChangeDBOwner.TabIndex = 12;
            this.checkBoxChangeDBOwner.Text = "Change DB owner";
            this.checkBoxChangeDBOwner.UseVisualStyleBackColor = true;
            // 
            // comboBoxRestoreDatabaseName
            // 
            this.comboBoxRestoreDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxRestoreDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxRestoreDatabaseName.FormattingEnabled = true;
            this.comboBoxRestoreDatabaseName.Location = new System.Drawing.Point(127, 44);
            this.comboBoxRestoreDatabaseName.Name = "comboBoxRestoreDatabaseName";
            this.comboBoxRestoreDatabaseName.Size = new System.Drawing.Size(370, 21);
            this.comboBoxRestoreDatabaseName.TabIndex = 8;
            this.comboBoxRestoreDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // buttonRestoreDatabase
            // 
            this.buttonRestoreDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRestoreDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRestoreDatabase.Location = new System.Drawing.Point(544, 47);
            this.buttonRestoreDatabase.Name = "buttonRestoreDatabase";
            this.buttonRestoreDatabase.Size = new System.Drawing.Size(84, 28);
            this.buttonRestoreDatabase.TabIndex = 6;
            this.buttonRestoreDatabase.Text = "Execute";
            this.buttonRestoreDatabase.UseVisualStyleBackColor = true;
            this.buttonRestoreDatabase.Click += new System.EventHandler(this.buttonRestoreDatabase_Click);
            // 
            // textBoxPathRestore
            // 
            this.textBoxPathRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPathRestore.Location = new System.Drawing.Point(127, 17);
            this.textBoxPathRestore.Name = "textBoxPathRestore";
            this.textBoxPathRestore.Size = new System.Drawing.Size(464, 20);
            this.textBoxPathRestore.TabIndex = 0;
            // 
            // labelPathRestore
            // 
            this.labelPathRestore.AutoSize = true;
            this.labelPathRestore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPathRestore.Location = new System.Drawing.Point(8, 20);
            this.labelPathRestore.Name = "labelPathRestore";
            this.labelPathRestore.Size = new System.Drawing.Size(92, 13);
            this.labelPathRestore.TabIndex = 3;
            this.labelPathRestore.Text = "Path to restore file";
            // 
            // buttonOpenRestoreDatabase
            // 
            this.buttonOpenRestoreDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenRestoreDatabase.Location = new System.Drawing.Point(597, 15);
            this.buttonOpenRestoreDatabase.Name = "buttonOpenRestoreDatabase";
            this.buttonOpenRestoreDatabase.Size = new System.Drawing.Size(31, 23);
            this.buttonOpenRestoreDatabase.TabIndex = 2;
            this.buttonOpenRestoreDatabase.Text = "...";
            this.buttonOpenRestoreDatabase.UseVisualStyleBackColor = true;
            this.buttonOpenRestoreDatabase.Click += new System.EventHandler(this.buttonOpenRestoreDatabase_Click);
            // 
            // labelRestoreDatabaseName
            // 
            this.labelRestoreDatabaseName.AutoSize = true;
            this.labelRestoreDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelRestoreDatabaseName.Location = new System.Drawing.Point(8, 47);
            this.labelRestoreDatabaseName.Name = "labelRestoreDatabaseName";
            this.labelRestoreDatabaseName.Size = new System.Drawing.Size(82, 13);
            this.labelRestoreDatabaseName.TabIndex = 0;
            this.labelRestoreDatabaseName.Text = "Database name";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Backup files|*.bak|All files|*.*";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Backup files|*.bak|All files|*.*";
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExit.Location = new System.Drawing.Point(597, 59);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(75, 30);
            this.buttonExit.TabIndex = 0;
            this.buttonExit.Text = "Exit";
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // groupBoxServerSettings
            // 
            this.groupBoxServerSettings.Controls.Add(this.checkBoxWindowsAuthorization);
            this.groupBoxServerSettings.Controls.Add(this.buttonCheckConnection);
            this.groupBoxServerSettings.Controls.Add(this.textBoxPassword);
            this.groupBoxServerSettings.Controls.Add(this.label3);
            this.groupBoxServerSettings.Controls.Add(this.textBoxServerName);
            this.groupBoxServerSettings.Controls.Add(this.label1);
            this.groupBoxServerSettings.Controls.Add(this.textBoxLogin);
            this.groupBoxServerSettings.Controls.Add(this.label2);
            this.groupBoxServerSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxServerSettings.Location = new System.Drawing.Point(12, 12);
            this.groupBoxServerSettings.Name = "groupBoxServerSettings";
            this.groupBoxServerSettings.Size = new System.Drawing.Size(559, 77);
            this.groupBoxServerSettings.TabIndex = 1;
            this.groupBoxServerSettings.TabStop = false;
            this.groupBoxServerSettings.Text = "Server settings";
            // 
            // checkBoxWindowsAuthorization
            // 
            this.checkBoxWindowsAuthorization.AutoSize = true;
            this.checkBoxWindowsAuthorization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxWindowsAuthorization.Location = new System.Drawing.Point(212, 57);
            this.checkBoxWindowsAuthorization.Name = "checkBoxWindowsAuthorization";
            this.checkBoxWindowsAuthorization.Size = new System.Drawing.Size(146, 17);
            this.checkBoxWindowsAuthorization.TabIndex = 7;
            this.checkBoxWindowsAuthorization.Text = "Use windows autorization";
            this.checkBoxWindowsAuthorization.UseVisualStyleBackColor = true;
            this.checkBoxWindowsAuthorization.CheckedChanged += new System.EventHandler(this.checkBoxWindowsAuthorization_CheckedChanged);
            // 
            // buttonCheckConnection
            // 
            this.buttonCheckConnection.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheckConnection.Location = new System.Drawing.Point(454, 21);
            this.buttonCheckConnection.Name = "buttonCheckConnection";
            this.buttonCheckConnection.Size = new System.Drawing.Size(92, 42);
            this.buttonCheckConnection.TabIndex = 6;
            this.buttonCheckConnection.Text = "Reload connection";
            this.buttonCheckConnection.UseVisualStyleBackColor = true;
            this.buttonCheckConnection.Click += new System.EventHandler(this.buttonCheckConnection_Click);
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassword.Location = new System.Drawing.Point(330, 37);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(98, 20);
            this.textBoxPassword.TabIndex = 4;
            this.textBoxPassword.Text = "firm";
            this.textBoxPassword.TextChanged += new System.EventHandler(this.ServerSetting_TextChanges);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(350, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Password";
            // 
            // textBoxServerName
            // 
            this.textBoxServerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxServerName.Location = new System.Drawing.Point(12, 37);
            this.textBoxServerName.Name = "textBoxServerName";
            this.textBoxServerName.Size = new System.Drawing.Size(180, 20);
            this.textBoxServerName.TabIndex = 0;
            this.textBoxServerName.Text = "localhost";
            this.textBoxServerName.TextChanged += new System.EventHandler(this.ServerSetting_TextChanges);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(70, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Server name";
            // 
            // textBoxLogin
            // 
            this.textBoxLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLogin.Location = new System.Drawing.Point(212, 37);
            this.textBoxLogin.Name = "textBoxLogin";
            this.textBoxLogin.Size = new System.Drawing.Size(98, 20);
            this.textBoxLogin.TabIndex = 2;
            this.textBoxLogin.Text = "sa";
            this.textBoxLogin.TextChanged += new System.EventHandler(this.ServerSetting_TextChanges);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(245, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Login";
            // 
            // groupBoxDrop
            // 
            this.groupBoxDrop.Controls.Add(this.checkBoxDropDatabaseStartWith);
            this.groupBoxDrop.Controls.Add(this.comboBoxDropDatabaseName);
            this.groupBoxDrop.Controls.Add(this.buttonDropDatabase);
            this.groupBoxDrop.Controls.Add(this.labelDropDatabaseName);
            this.groupBoxDrop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxDrop.Location = new System.Drawing.Point(8, 6);
            this.groupBoxDrop.Name = "groupBoxDrop";
            this.groupBoxDrop.Size = new System.Drawing.Size(213, 110);
            this.groupBoxDrop.TabIndex = 4;
            this.groupBoxDrop.TabStop = false;
            this.groupBoxDrop.Text = "Drop database";
            // 
            // checkBoxDropDatabaseStartWith
            // 
            this.checkBoxDropDatabaseStartWith.AutoSize = true;
            this.checkBoxDropDatabaseStartWith.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxDropDatabaseStartWith.Location = new System.Drawing.Point(18, 74);
            this.checkBoxDropDatabaseStartWith.Name = "checkBoxDropDatabaseStartWith";
            this.checkBoxDropDatabaseStartWith.Size = new System.Drawing.Size(76, 17);
            this.checkBoxDropDatabaseStartWith.TabIndex = 10;
            this.checkBoxDropDatabaseStartWith.Text = "Starts from";
            this.checkBoxDropDatabaseStartWith.UseVisualStyleBackColor = true;
            this.checkBoxDropDatabaseStartWith.MouseEnter += new System.EventHandler(this.checkBoxDropDatabaseStartWith_MouseEnter);
            this.checkBoxDropDatabaseStartWith.MouseLeave += new System.EventHandler(this.checkBoxDropDatabaseStartWith_MouseLeave);
            // 
            // comboBoxDropDatabaseName
            // 
            this.comboBoxDropDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDropDatabaseName.FormattingEnabled = true;
            this.comboBoxDropDatabaseName.Location = new System.Drawing.Point(18, 41);
            this.comboBoxDropDatabaseName.Name = "comboBoxDropDatabaseName";
            this.comboBoxDropDatabaseName.Size = new System.Drawing.Size(180, 21);
            this.comboBoxDropDatabaseName.TabIndex = 9;
            this.comboBoxDropDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // buttonDropDatabase
            // 
            this.buttonDropDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDropDatabase.Location = new System.Drawing.Point(114, 71);
            this.buttonDropDatabase.Name = "buttonDropDatabase";
            this.buttonDropDatabase.Size = new System.Drawing.Size(84, 28);
            this.buttonDropDatabase.TabIndex = 2;
            this.buttonDropDatabase.Text = "Execute";
            this.buttonDropDatabase.UseVisualStyleBackColor = true;
            this.buttonDropDatabase.Click += new System.EventHandler(this.buttonDropDatabase_Click);
            // 
            // labelDropDatabaseName
            // 
            this.labelDropDatabaseName.AutoSize = true;
            this.labelDropDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelDropDatabaseName.Location = new System.Drawing.Point(65, 22);
            this.labelDropDatabaseName.Name = "labelDropDatabaseName";
            this.labelDropDatabaseName.Size = new System.Drawing.Size(82, 13);
            this.labelDropDatabaseName.TabIndex = 0;
            this.labelDropDatabaseName.Text = "Database name";
            // 
            // groupBoxCopyDatabase
            // 
            this.groupBoxCopyDatabase.Controls.Add(this.checkBoxSetNewBroker);
            this.groupBoxCopyDatabase.Controls.Add(this.checkBoxSetTrustworthyOn);
            this.groupBoxCopyDatabase.Controls.Add(this.comboBoxToDatabaseName);
            this.groupBoxCopyDatabase.Controls.Add(this.comboBoxFromDatabaseName);
            this.groupBoxCopyDatabase.Controls.Add(this.label6);
            this.groupBoxCopyDatabase.Controls.Add(this.label5);
            this.groupBoxCopyDatabase.Controls.Add(this.buttonCopyDatabase);
            this.groupBoxCopyDatabase.Controls.Add(this.label4);
            this.groupBoxCopyDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxCopyDatabase.Location = new System.Drawing.Point(233, 6);
            this.groupBoxCopyDatabase.Name = "groupBoxCopyDatabase";
            this.groupBoxCopyDatabase.Size = new System.Drawing.Size(424, 110);
            this.groupBoxCopyDatabase.TabIndex = 5;
            this.groupBoxCopyDatabase.TabStop = false;
            this.groupBoxCopyDatabase.Text = "Copy database";
            // 
            // checkBoxSetNewBroker
            // 
            this.checkBoxSetNewBroker.AutoSize = true;
            this.checkBoxSetNewBroker.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxSetNewBroker.Location = new System.Drawing.Point(229, 87);
            this.checkBoxSetNewBroker.Name = "checkBoxSetNewBroker";
            this.checkBoxSetNewBroker.Size = new System.Drawing.Size(101, 17);
            this.checkBoxSetNewBroker.TabIndex = 13;
            this.checkBoxSetNewBroker.Text = "Set new_broker";
            this.checkBoxSetNewBroker.UseVisualStyleBackColor = true;
            // 
            // checkBoxSetTrustworthyOn
            // 
            this.checkBoxSetTrustworthyOn.AutoSize = true;
            this.checkBoxSetTrustworthyOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxSetTrustworthyOn.Location = new System.Drawing.Point(229, 68);
            this.checkBoxSetTrustworthyOn.Name = "checkBoxSetTrustworthyOn";
            this.checkBoxSetTrustworthyOn.Size = new System.Drawing.Size(111, 17);
            this.checkBoxSetTrustworthyOn.TabIndex = 12;
            this.checkBoxSetTrustworthyOn.Text = "Set trustworthy on";
            this.checkBoxSetTrustworthyOn.UseVisualStyleBackColor = true;
            // 
            // comboBoxToDatabaseName
            // 
            this.comboBoxToDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxToDatabaseName.FormattingEnabled = true;
            this.comboBoxToDatabaseName.Location = new System.Drawing.Point(229, 41);
            this.comboBoxToDatabaseName.Name = "comboBoxToDatabaseName";
            this.comboBoxToDatabaseName.Size = new System.Drawing.Size(180, 21);
            this.comboBoxToDatabaseName.TabIndex = 11;
            this.comboBoxToDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // comboBoxFromDatabaseName
            // 
            this.comboBoxFromDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFromDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFromDatabaseName.FormattingEnabled = true;
            this.comboBoxFromDatabaseName.Location = new System.Drawing.Point(12, 41);
            this.comboBoxFromDatabaseName.Name = "comboBoxFromDatabaseName";
            this.comboBoxFromDatabaseName.Size = new System.Drawing.Size(180, 21);
            this.comboBoxFromDatabaseName.TabIndex = 10;
            this.comboBoxFromDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(204, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(16, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "->";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(271, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "To database name";
            // 
            // buttonCopyDatabase
            // 
            this.buttonCopyDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCopyDatabase.Location = new System.Drawing.Point(107, 71);
            this.buttonCopyDatabase.Name = "buttonCopyDatabase";
            this.buttonCopyDatabase.Size = new System.Drawing.Size(84, 28);
            this.buttonCopyDatabase.TabIndex = 2;
            this.buttonCopyDatabase.Text = "Execute";
            this.buttonCopyDatabase.UseVisualStyleBackColor = true;
            this.buttonCopyDatabase.Click += new System.EventHandler(this.buttonCopyDatabase_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(49, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "From database name";
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(601, 43);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(69, 13);
            this.labelVersion.TabIndex = 6;
            this.labelVersion.Text = "Version 3.2.0";
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRefresh.Location = new System.Drawing.Point(597, 12);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 30);
            this.buttonRefresh.TabIndex = 7;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // labelGetInfo
            // 
            this.labelGetInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.labelGetInfo.AutoSize = true;
            this.labelGetInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGetInfo.Location = new System.Drawing.Point(189, 125);
            this.labelGetInfo.Name = "labelGetInfo";
            this.labelGetInfo.Size = new System.Drawing.Size(45, 26);
            this.labelGetInfo.TabIndex = 13;
            this.labelGetInfo.Text = "Results:\r\n0 rows";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(65, 121);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 13);
            this.label8.TabIndex = 12;
            this.label8.Text = "Item type";
            // 
            // comboBoxItemList
            // 
            this.comboBoxItemList.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxItemList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxItemList.FormattingEnabled = true;
            this.comboBoxItemList.Items.AddRange(new object[] {
            "Tables",
            "Columns",
            "Checks",
            "Triggers",
            "Indexes",
            "Foreign Keys",
            "Table Properties",
            "Stored Proccedures",
            "Users",
            "Views",
            "Database Properties",
            "Database List"});
            this.comboBoxItemList.Location = new System.Drawing.Point(3, 141);
            this.comboBoxItemList.Name = "comboBoxItemList";
            this.comboBoxItemList.Size = new System.Drawing.Size(180, 21);
            this.comboBoxItemList.TabIndex = 11;
            // 
            // textBoxListOfItems
            // 
            this.textBoxListOfItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxListOfItems.ContextMenuStrip = this.contextMenuStrip1;
            this.textBoxListOfItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxListOfItems.Location = new System.Drawing.Point(333, 12);
            this.textBoxListOfItems.MaxLength = 2000000;
            this.textBoxListOfItems.Multiline = true;
            this.textBoxListOfItems.Name = "textBoxListOfItems";
            this.textBoxListOfItems.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxListOfItems.Size = new System.Drawing.Size(321, 232);
            this.textBoxListOfItems.TabIndex = 10;
            this.textBoxListOfItems.WordWrap = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAllToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 26);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.selectAllToolStripMenuItem.Text = "Select all";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // comboBoxSelectDatabaseName
            // 
            this.comboBoxSelectDatabaseName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.comboBoxSelectDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxSelectDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxSelectDatabaseName.FormattingEnabled = true;
            this.comboBoxSelectDatabaseName.Location = new System.Drawing.Point(3, 92);
            this.comboBoxSelectDatabaseName.Name = "comboBoxSelectDatabaseName";
            this.comboBoxSelectDatabaseName.Size = new System.Drawing.Size(180, 21);
            this.comboBoxSelectDatabaseName.TabIndex = 9;
            this.comboBoxSelectDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // buttonGetListOfItems
            // 
            this.buttonGetListOfItems.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonGetListOfItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGetListOfItems.Location = new System.Drawing.Point(216, 82);
            this.buttonGetListOfItems.Name = "buttonGetListOfItems";
            this.buttonGetListOfItems.Size = new System.Drawing.Size(84, 28);
            this.buttonGetListOfItems.TabIndex = 2;
            this.buttonGetListOfItems.Text = "Execute";
            this.buttonGetListOfItems.UseVisualStyleBackColor = true;
            this.buttonGetListOfItems.Click += new System.EventHandler(this.buttonGetListOfItems_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(50, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Database name";
            // 
            // groupBoxRename
            // 
            this.groupBoxRename.Controls.Add(this.labelReplaceInstruction);
            this.groupBoxRename.Controls.Add(this.comboBoxReplaceString);
            this.groupBoxRename.Controls.Add(this.comboBoxFindString);
            this.groupBoxRename.Controls.Add(this.label9);
            this.groupBoxRename.Controls.Add(this.label10);
            this.groupBoxRename.Controls.Add(this.buttonReplace);
            this.groupBoxRename.Controls.Add(this.label11);
            this.groupBoxRename.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxRename.Location = new System.Drawing.Point(8, 129);
            this.groupBoxRename.Name = "groupBoxRename";
            this.groupBoxRename.Size = new System.Drawing.Size(649, 110);
            this.groupBoxRename.TabIndex = 9;
            this.groupBoxRename.TabStop = false;
            this.groupBoxRename.Text = "Rename databases";
            // 
            // labelReplaceInstruction
            // 
            this.labelReplaceInstruction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelReplaceInstruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelReplaceInstruction.Location = new System.Drawing.Point(417, 16);
            this.labelReplaceInstruction.Name = "labelReplaceInstruction";
            this.labelReplaceInstruction.Size = new System.Drawing.Size(222, 85);
            this.labelReplaceInstruction.TabIndex = 12;
            this.labelReplaceInstruction.Text = "Write \"Find string\" and \"Replace string\".\r\nPress \"Execute\" button.\r\nIf program fi" +
    "nds database name,\r\nthat contains first string, program \r\nreplaces this string t" +
    "o \"Replace string\".\r\n";
            // 
            // comboBoxReplaceString
            // 
            this.comboBoxReplaceString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxReplaceString.FormattingEnabled = true;
            this.comboBoxReplaceString.Location = new System.Drawing.Point(229, 41);
            this.comboBoxReplaceString.Name = "comboBoxReplaceString";
            this.comboBoxReplaceString.Size = new System.Drawing.Size(180, 21);
            this.comboBoxReplaceString.TabIndex = 11;
            this.comboBoxReplaceString.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // comboBoxFindString
            // 
            this.comboBoxFindString.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxFindString.FormattingEnabled = true;
            this.comboBoxFindString.Location = new System.Drawing.Point(12, 41);
            this.comboBoxFindString.Name = "comboBoxFindString";
            this.comboBoxFindString.Size = new System.Drawing.Size(180, 21);
            this.comboBoxFindString.TabIndex = 10;
            this.comboBoxFindString.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(204, 45);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(16, 13);
            this.label9.TabIndex = 5;
            this.label9.Text = "->";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(271, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(75, 13);
            this.label10.TabIndex = 3;
            this.label10.Text = "Replace string";
            // 
            // buttonReplace
            // 
            this.buttonReplace.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonReplace.Location = new System.Drawing.Point(176, 73);
            this.buttonReplace.Name = "buttonReplace";
            this.buttonReplace.Size = new System.Drawing.Size(84, 28);
            this.buttonReplace.TabIndex = 2;
            this.buttonReplace.Text = "Execute";
            this.buttonReplace.UseVisualStyleBackColor = true;
            this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(49, 22);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(55, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Find string";
            // 
            // groupBoxDetach
            // 
            this.groupBoxDetach.Controls.Add(this.comboBoxDetachDatabaseName);
            this.groupBoxDetach.Controls.Add(this.buttonDetach);
            this.groupBoxDetach.Controls.Add(this.label12);
            this.groupBoxDetach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxDetach.Location = new System.Drawing.Point(8, 146);
            this.groupBoxDetach.Name = "groupBoxDetach";
            this.groupBoxDetach.Size = new System.Drawing.Size(308, 72);
            this.groupBoxDetach.TabIndex = 10;
            this.groupBoxDetach.TabStop = false;
            this.groupBoxDetach.Text = "Detach database";
            // 
            // comboBoxDetachDatabaseName
            // 
            this.comboBoxDetachDatabaseName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDetachDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDetachDatabaseName.FormattingEnabled = true;
            this.comboBoxDetachDatabaseName.Location = new System.Drawing.Point(18, 41);
            this.comboBoxDetachDatabaseName.Name = "comboBoxDetachDatabaseName";
            this.comboBoxDetachDatabaseName.Size = new System.Drawing.Size(180, 21);
            this.comboBoxDetachDatabaseName.TabIndex = 9;
            this.comboBoxDetachDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // buttonDetach
            // 
            this.buttonDetach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDetach.Location = new System.Drawing.Point(214, 34);
            this.buttonDetach.Name = "buttonDetach";
            this.buttonDetach.Size = new System.Drawing.Size(84, 28);
            this.buttonDetach.TabIndex = 2;
            this.buttonDetach.Text = "Execute";
            this.buttonDetach.UseVisualStyleBackColor = true;
            this.buttonDetach.Click += new System.EventHandler(this.buttonDetach_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(65, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Database name";
            // 
            // groupBoxAttach
            // 
            this.groupBoxAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxAttach.Controls.Add(this.textBoxPathLdf);
            this.groupBoxAttach.Controls.Add(this.label15);
            this.groupBoxAttach.Controls.Add(this.buttonOpenLdfFile);
            this.groupBoxAttach.Controls.Add(this.comboBoxAttachDatabaseName);
            this.groupBoxAttach.Controls.Add(this.buttonAttach);
            this.groupBoxAttach.Controls.Add(this.textBoxPathMdf);
            this.groupBoxAttach.Controls.Add(this.label13);
            this.groupBoxAttach.Controls.Add(this.buttonOpenMdfFile);
            this.groupBoxAttach.Controls.Add(this.label14);
            this.groupBoxAttach.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxAttach.Location = new System.Drawing.Point(8, 15);
            this.groupBoxAttach.Name = "groupBoxAttach";
            this.groupBoxAttach.Size = new System.Drawing.Size(646, 110);
            this.groupBoxAttach.TabIndex = 11;
            this.groupBoxAttach.TabStop = false;
            this.groupBoxAttach.Text = "Attach database";
            // 
            // textBoxPathLdf
            // 
            this.textBoxPathLdf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathLdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPathLdf.Location = new System.Drawing.Point(95, 47);
            this.textBoxPathLdf.Name = "textBoxPathLdf";
            this.textBoxPathLdf.Size = new System.Drawing.Size(504, 20);
            this.textBoxPathLdf.TabIndex = 9;
            this.textBoxPathLdf.MouseEnter += new System.EventHandler(this.textBoxPathLdf_MouseEnter);
            this.textBoxPathLdf.MouseLeave += new System.EventHandler(this.textBoxPathLdf_MouseLeave);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(6, 50);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(80, 13);
            this.label15.TabIndex = 11;
            this.label15.Text = "Path to LDF file";
            // 
            // buttonOpenLdfFile
            // 
            this.buttonOpenLdfFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenLdfFile.Location = new System.Drawing.Point(605, 44);
            this.buttonOpenLdfFile.Name = "buttonOpenLdfFile";
            this.buttonOpenLdfFile.Size = new System.Drawing.Size(31, 23);
            this.buttonOpenLdfFile.TabIndex = 10;
            this.buttonOpenLdfFile.Text = "...";
            this.buttonOpenLdfFile.UseVisualStyleBackColor = true;
            this.buttonOpenLdfFile.Click += new System.EventHandler(this.buttonOpenLdfFile_Click);
            // 
            // comboBoxAttachDatabaseName
            // 
            this.comboBoxAttachDatabaseName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxAttachDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxAttachDatabaseName.FormattingEnabled = true;
            this.comboBoxAttachDatabaseName.Location = new System.Drawing.Point(94, 78);
            this.comboBoxAttachDatabaseName.Name = "comboBoxAttachDatabaseName";
            this.comboBoxAttachDatabaseName.Size = new System.Drawing.Size(385, 21);
            this.comboBoxAttachDatabaseName.TabIndex = 8;
            this.comboBoxAttachDatabaseName.DropDown += new System.EventHandler(this.DatabaseComboBox_DropDown);
            // 
            // buttonAttach
            // 
            this.buttonAttach.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAttach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAttach.Location = new System.Drawing.Point(515, 73);
            this.buttonAttach.Name = "buttonAttach";
            this.buttonAttach.Size = new System.Drawing.Size(84, 28);
            this.buttonAttach.TabIndex = 6;
            this.buttonAttach.Text = "Execute";
            this.buttonAttach.UseVisualStyleBackColor = true;
            this.buttonAttach.Click += new System.EventHandler(this.buttonAttach_Click);
            // 
            // textBoxPathMdf
            // 
            this.textBoxPathMdf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPathMdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPathMdf.Location = new System.Drawing.Point(95, 20);
            this.textBoxPathMdf.Name = "textBoxPathMdf";
            this.textBoxPathMdf.Size = new System.Drawing.Size(504, 20);
            this.textBoxPathMdf.TabIndex = 0;
            this.textBoxPathMdf.MouseEnter += new System.EventHandler(this.textBoxPathMdf_MouseEnter);
            this.textBoxPathMdf.MouseLeave += new System.EventHandler(this.textBoxPathMdf_MouseLeave);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(6, 23);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 3;
            this.label13.Text = "Path to MDF file";
            // 
            // buttonOpenMdfFile
            // 
            this.buttonOpenMdfFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOpenMdfFile.Location = new System.Drawing.Point(605, 16);
            this.buttonOpenMdfFile.Name = "buttonOpenMdfFile";
            this.buttonOpenMdfFile.Size = new System.Drawing.Size(31, 23);
            this.buttonOpenMdfFile.TabIndex = 2;
            this.buttonOpenMdfFile.Text = "...";
            this.buttonOpenMdfFile.UseVisualStyleBackColor = true;
            this.buttonOpenMdfFile.Click += new System.EventHandler(this.buttonOpenMdfFile_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(6, 81);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 13);
            this.label14.TabIndex = 0;
            this.label14.Text = "Database name";
            // 
            // comboBoxExecutedDatabaseName
            // 
            this.comboBoxExecutedDatabaseName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxExecutedDatabaseName.FormattingEnabled = true;
            this.comboBoxExecutedDatabaseName.Location = new System.Drawing.Point(8, 26);
            this.comboBoxExecutedDatabaseName.Name = "comboBoxExecutedDatabaseName";
            this.comboBoxExecutedDatabaseName.Size = new System.Drawing.Size(314, 21);
            this.comboBoxExecutedDatabaseName.TabIndex = 8;
            // 
            // buttonExecuteSql
            // 
            this.buttonExecuteSql.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonExecuteSql.Location = new System.Drawing.Point(349, 21);
            this.buttonExecuteSql.Name = "buttonExecuteSql";
            this.buttonExecuteSql.Size = new System.Drawing.Size(84, 28);
            this.buttonExecuteSql.TabIndex = 6;
            this.buttonExecuteSql.Text = "Execute";
            this.buttonExecuteSql.UseVisualStyleBackColor = true;
            this.buttonExecuteSql.Click += new System.EventHandler(this.buttonExecuteSql_Click);
            // 
            // textBoxSqlCommand
            // 
            this.textBoxSqlCommand.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxSqlCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSqlCommand.Location = new System.Drawing.Point(8, 53);
            this.textBoxSqlCommand.MaxLength = 200000;
            this.textBoxSqlCommand.Multiline = true;
            this.textBoxSqlCommand.Name = "textBoxSqlCommand";
            this.textBoxSqlCommand.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxSqlCommand.Size = new System.Drawing.Size(649, 191);
            this.textBoxSqlCommand.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(493, 34);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "SQL command";
            // 
            // label17
            // 
            this.label17.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(5, 10);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(82, 13);
            this.label17.TabIndex = 0;
            this.label17.Text = "Database name";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Location = new System.Drawing.Point(12, 95);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(668, 274);
            this.tabControl1.TabIndex = 13;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxBackup);
            this.tabPage1.Controls.Add(this.groupBoxRestore);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(660, 248);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Backup/Restore";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBoxRename);
            this.tabPage2.Controls.Add(this.groupBoxDrop);
            this.tabPage2.Controls.Add(this.groupBoxCopyDatabase);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(660, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Drop/Copy/Rename";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBoxDetach);
            this.tabPage3.Controls.Add(this.groupBoxAttach);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(660, 248);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Attach/Detach";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.comboBoxExecutedDatabaseName);
            this.tabPage4.Controls.Add(this.buttonExecuteSql);
            this.tabPage4.Controls.Add(this.label17);
            this.tabPage4.Controls.Add(this.textBoxSqlCommand);
            this.tabPage4.Controls.Add(this.label16);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(660, 248);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Execute script";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.labelGetInfo);
            this.tabPage5.Controls.Add(this.label8);
            this.tabPage5.Controls.Add(this.textBoxListOfItems);
            this.tabPage5.Controls.Add(this.comboBoxItemList);
            this.tabPage5.Controls.Add(this.label7);
            this.tabPage5.Controls.Add(this.buttonGetListOfItems);
            this.tabPage5.Controls.Add(this.comboBoxSelectDatabaseName);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(660, 248);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Get items info";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.groupBox3);
            this.tabPage6.Controls.Add(this.groupBox2);
            this.tabPage6.Controls.Add(this.groupBox1);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(660, 248);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "CATI";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.labelCatiInstancesRemovingAction);
            this.groupBox3.Controls.Add(this.textBoxFirstIndexForRemoving);
            this.groupBox3.Controls.Add(this.textBoxLastIndexForRemoving);
            this.groupBox3.Controls.Add(this.label21);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.buttonRemoveCatiInstances);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(8, 108);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(226, 99);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remove CATI database instances";
            // 
            // labelCatiInstancesRemovingAction
            // 
            this.labelCatiInstancesRemovingAction.AutoSize = true;
            this.labelCatiInstancesRemovingAction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCatiInstancesRemovingAction.Location = new System.Drawing.Point(5, 73);
            this.labelCatiInstancesRemovingAction.Name = "labelCatiInstancesRemovingAction";
            this.labelCatiInstancesRemovingAction.Size = new System.Drawing.Size(37, 13);
            this.labelCatiInstancesRemovingAction.TabIndex = 18;
            this.labelCatiInstancesRemovingAction.Text = "Action";
            this.labelCatiInstancesRemovingAction.Visible = false;
            // 
            // textBoxFirstIndexForRemoving
            // 
            this.textBoxFirstIndexForRemoving.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFirstIndexForRemoving.Location = new System.Drawing.Point(68, 19);
            this.textBoxFirstIndexForRemoving.Name = "textBoxFirstIndexForRemoving";
            this.textBoxFirstIndexForRemoving.Size = new System.Drawing.Size(35, 20);
            this.textBoxFirstIndexForRemoving.TabIndex = 14;
            this.textBoxFirstIndexForRemoving.Text = "1";
            // 
            // textBoxLastIndexForRemoving
            // 
            this.textBoxLastIndexForRemoving.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLastIndexForRemoving.Location = new System.Drawing.Point(68, 45);
            this.textBoxLastIndexForRemoving.Name = "textBoxLastIndexForRemoving";
            this.textBoxLastIndexForRemoving.Size = new System.Drawing.Size(35, 20);
            this.textBoxLastIndexForRemoving.TabIndex = 17;
            this.textBoxLastIndexForRemoving.Text = "100";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(4, 22);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 13);
            this.label21.TabIndex = 11;
            this.label21.Text = "First index";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label22.Location = new System.Drawing.Point(4, 48);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(55, 13);
            this.label22.TabIndex = 16;
            this.label22.Text = "Last index";
            // 
            // buttonRemoveCatiInstances
            // 
            this.buttonRemoveCatiInstances.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRemoveCatiInstances.Location = new System.Drawing.Point(128, 28);
            this.buttonRemoveCatiInstances.Name = "buttonRemoveCatiInstances";
            this.buttonRemoveCatiInstances.Size = new System.Drawing.Size(84, 28);
            this.buttonRemoveCatiInstances.TabIndex = 12;
            this.buttonRemoveCatiInstances.Text = "Execute";
            this.buttonRemoveCatiInstances.UseVisualStyleBackColor = true;
            this.buttonRemoveCatiInstances.Click += new System.EventHandler(this.buttonRemoveCatiInstances_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.buttonRemoveTestRuns);
            this.groupBox2.Controls.Add(this.checkBoxSelectAllTestRuns);
            this.groupBox2.Controls.Add(this.buttonGetTestRuns);
            this.groupBox2.Controls.Add(this.checkedListBoxTestRunList);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.buttonProfilingReportGenerate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(240, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(414, 241);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Generate profiling report";
            // 
            // buttonRemoveTestRuns
            // 
            this.buttonRemoveTestRuns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRemoveTestRuns.Location = new System.Drawing.Point(328, 194);
            this.buttonRemoveTestRuns.Name = "buttonRemoveTestRuns";
            this.buttonRemoveTestRuns.Size = new System.Drawing.Size(80, 40);
            this.buttonRemoveTestRuns.TabIndex = 16;
            this.buttonRemoveTestRuns.Text = "Remove test run";
            this.buttonRemoveTestRuns.UseVisualStyleBackColor = true;
            this.buttonRemoveTestRuns.Click += new System.EventHandler(this.ButtonRemoveTestRunsClick);
            // 
            // checkBoxSelectAllTestRuns
            // 
            this.checkBoxSelectAllTestRuns.AutoSize = true;
            this.checkBoxSelectAllTestRuns.Location = new System.Drawing.Point(9, 18);
            this.checkBoxSelectAllTestRuns.Name = "checkBoxSelectAllTestRuns";
            this.checkBoxSelectAllTestRuns.Size = new System.Drawing.Size(15, 14);
            this.checkBoxSelectAllTestRuns.TabIndex = 15;
            this.checkBoxSelectAllTestRuns.UseVisualStyleBackColor = true;
            this.checkBoxSelectAllTestRuns.CheckedChanged += new System.EventHandler(this.checkBoxSelectAllTestRuns_CheckedChanged);
            // 
            // buttonGetTestRuns
            // 
            this.buttonGetTestRuns.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonGetTestRuns.Location = new System.Drawing.Point(328, 35);
            this.buttonGetTestRuns.Name = "buttonGetTestRuns";
            this.buttonGetTestRuns.Size = new System.Drawing.Size(80, 40);
            this.buttonGetTestRuns.TabIndex = 14;
            this.buttonGetTestRuns.Text = "Get test runs";
            this.buttonGetTestRuns.UseVisualStyleBackColor = true;
            this.buttonGetTestRuns.Click += new System.EventHandler(this.buttonGetTestRuns_Click);
            // 
            // checkedListBoxTestRunList
            // 
            this.checkedListBoxTestRunList.CheckOnClick = true;
            this.checkedListBoxTestRunList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkedListBoxTestRunList.FormattingEnabled = true;
            this.checkedListBoxTestRunList.HorizontalScrollbar = true;
            this.checkedListBoxTestRunList.Location = new System.Drawing.Point(6, 35);
            this.checkedListBoxTestRunList.Name = "checkedListBoxTestRunList";
            this.checkedListBoxTestRunList.Size = new System.Drawing.Size(316, 199);
            this.checkedListBoxTestRunList.Sorted = true;
            this.checkedListBoxTestRunList.TabIndex = 13;
            this.checkedListBoxTestRunList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxTestRunList_ItemCheck);
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(6, 17);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(312, 13);
            this.label19.TabIndex = 11;
            this.label19.Text = "Select test runs";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonProfilingReportGenerate
            // 
            this.buttonProfilingReportGenerate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonProfilingReportGenerate.Location = new System.Drawing.Point(328, 81);
            this.buttonProfilingReportGenerate.Name = "buttonProfilingReportGenerate";
            this.buttonProfilingReportGenerate.Size = new System.Drawing.Size(80, 40);
            this.buttonProfilingReportGenerate.TabIndex = 12;
            this.buttonProfilingReportGenerate.Text = "Generate report";
            this.buttonProfilingReportGenerate.UseVisualStyleBackColor = true;
            this.buttonProfilingReportGenerate.Click += new System.EventHandler(this.ButtonProfilingReportGenerateClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.progressBarCATICreation);
            this.groupBox1.Controls.Add(this.textBoxFirstIndex);
            this.groupBox1.Controls.Add(this.textBoxLastIndex);
            this.groupBox1.Controls.Add(this.label18);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.buttonCreateCATIInstances);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(8, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 99);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add new CATI database instances";
            // 
            // progressBarCATICreation
            // 
            this.progressBarCATICreation.Location = new System.Drawing.Point(7, 68);
            this.progressBarCATICreation.Name = "progressBarCATICreation";
            this.progressBarCATICreation.Size = new System.Drawing.Size(205, 23);
            this.progressBarCATICreation.TabIndex = 18;
            // 
            // textBoxFirstIndex
            // 
            this.textBoxFirstIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFirstIndex.Location = new System.Drawing.Point(68, 19);
            this.textBoxFirstIndex.Name = "textBoxFirstIndex";
            this.textBoxFirstIndex.Size = new System.Drawing.Size(35, 20);
            this.textBoxFirstIndex.TabIndex = 14;
            this.textBoxFirstIndex.Text = "1";
            // 
            // textBoxLastIndex
            // 
            this.textBoxLastIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxLastIndex.Location = new System.Drawing.Point(68, 45);
            this.textBoxLastIndex.Name = "textBoxLastIndex";
            this.textBoxLastIndex.Size = new System.Drawing.Size(35, 20);
            this.textBoxLastIndex.TabIndex = 17;
            this.textBoxLastIndex.Text = "100";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(4, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(54, 13);
            this.label18.TabIndex = 11;
            this.label18.Text = "First index";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(4, 48);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(55, 13);
            this.label20.TabIndex = 16;
            this.label20.Text = "Last index";
            // 
            // buttonCreateCATIInstances
            // 
            this.buttonCreateCATIInstances.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCreateCATIInstances.Location = new System.Drawing.Point(128, 28);
            this.buttonCreateCATIInstances.Name = "buttonCreateCATIInstances";
            this.buttonCreateCATIInstances.Size = new System.Drawing.Size(84, 28);
            this.buttonCreateCATIInstances.TabIndex = 12;
            this.buttonCreateCATIInstances.Text = "Execute";
            this.buttonCreateCATIInstances.UseVisualStyleBackColor = true;
            this.buttonCreateCATIInstances.Click += new System.EventHandler(this.buttonCreateCATIInstances_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxSetNewBrokerOnResore
            // 
            this.checkBoxSetNewBrokerOnResore.AutoSize = true;
            this.checkBoxSetNewBrokerOnResore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxSetNewBrokerOnResore.Location = new System.Drawing.Point(306, 71);
            this.checkBoxSetNewBrokerOnResore.Name = "checkBoxSetNewBrokerOnResore";
            this.checkBoxSetNewBrokerOnResore.Size = new System.Drawing.Size(98, 17);
            this.checkBoxSetNewBrokerOnResore.TabIndex = 16;
            this.checkBoxSetNewBrokerOnResore.Text = "Set new broker";
            this.checkBoxSetNewBrokerOnResore.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692, 373);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.groupBoxServerSettings);
            this.Controls.Add(this.buttonExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 400);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database worker";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxBackup.ResumeLayout(false);
            this.groupBoxBackup.PerformLayout();
            this.groupBoxRestore.ResumeLayout(false);
            this.groupBoxRestore.PerformLayout();
            this.groupBoxServerSettings.ResumeLayout(false);
            this.groupBoxServerSettings.PerformLayout();
            this.groupBoxDrop.ResumeLayout(false);
            this.groupBoxDrop.PerformLayout();
            this.groupBoxCopyDatabase.ResumeLayout(false);
            this.groupBoxCopyDatabase.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBoxRename.ResumeLayout(false);
            this.groupBoxRename.PerformLayout();
            this.groupBoxDetach.ResumeLayout(false);
            this.groupBoxDetach.PerformLayout();
            this.groupBoxAttach.ResumeLayout(false);
            this.groupBoxAttach.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBackup;
        private System.Windows.Forms.Button buttonBackupDatabase;
        private System.Windows.Forms.TextBox textBoxPathBackup;
        private System.Windows.Forms.Label labelPathBackup;
        private System.Windows.Forms.Button buttonOpenBackupDatabase;
        private System.Windows.Forms.Label labelBackupDatabaseName;
        private System.Windows.Forms.GroupBox groupBoxRestore;
        private System.Windows.Forms.Button buttonRestoreDatabase;
        private System.Windows.Forms.TextBox textBoxPathRestore;
        private System.Windows.Forms.Label labelPathRestore;
        private System.Windows.Forms.Button buttonOpenRestoreDatabase;
        private System.Windows.Forms.Label labelRestoreDatabaseName;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.GroupBox groupBoxServerSettings;
        private System.Windows.Forms.Button buttonCheckConnection;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxServerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxLogin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxDrop;
        private System.Windows.Forms.Button buttonDropDatabase;
        private System.Windows.Forms.Label labelDropDatabaseName;
        private System.Windows.Forms.GroupBox groupBoxCopyDatabase;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonCopyDatabase;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBoxBackupDatabaseName;
        private System.Windows.Forms.ComboBox comboBoxRestoreDatabaseName;
        private System.Windows.Forms.ComboBox comboBoxDropDatabaseName;
        private System.Windows.Forms.ComboBox comboBoxToDatabaseName;
        private System.Windows.Forms.ComboBox comboBoxFromDatabaseName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBoxItemList;
        private System.Windows.Forms.TextBox textBoxListOfItems;
        private System.Windows.Forms.ComboBox comboBoxSelectDatabaseName;
        private System.Windows.Forms.Button buttonGetListOfItems;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelGetInfo;
        private System.Windows.Forms.GroupBox groupBoxRename;
        private System.Windows.Forms.ComboBox comboBoxReplaceString;
        private System.Windows.Forms.ComboBox comboBoxFindString;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button buttonReplace;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labelReplaceInstruction;
        private System.Windows.Forms.GroupBox groupBoxDetach;
        private System.Windows.Forms.ComboBox comboBoxDetachDatabaseName;
        private System.Windows.Forms.Button buttonDetach;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBoxAttach;
        private System.Windows.Forms.TextBox textBoxPathLdf;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button buttonOpenLdfFile;
        private System.Windows.Forms.ComboBox comboBoxAttachDatabaseName;
        private System.Windows.Forms.Button buttonAttach;
        private System.Windows.Forms.TextBox textBoxPathMdf;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button buttonOpenMdfFile;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBoxDropDatabaseStartWith;
        private System.Windows.Forms.CheckBox checkBoxWindowsAuthorization;
        private System.Windows.Forms.ComboBox comboBoxExecutedDatabaseName;
        private System.Windows.Forms.Button buttonExecuteSql;
        private System.Windows.Forms.TextBox textBoxSqlCommand;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.CheckBox checkBoxUseBackupCompression;
        private System.Windows.Forms.CheckBox checkBoxSetTrustworthy;
        private System.Windows.Forms.CheckBox checkBoxChangeDBOwner;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.CheckBox checkBoxRestoreAllDb;
        private System.Windows.Forms.Label labelAction;
        private System.Windows.Forms.CheckBox checkBoxSetNewBroker;
        private System.Windows.Forms.CheckBox checkBoxSetTrustworthyOn;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxFirstIndex;
        private System.Windows.Forms.TextBox textBoxLastIndex;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button buttonCreateCATIInstances;
        private System.Windows.Forms.ProgressBar progressBarCATICreation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button buttonProfilingReportGenerate;
        private System.Windows.Forms.CheckedListBox checkedListBoxTestRunList;
        private System.Windows.Forms.Button buttonGetTestRuns;
        private System.Windows.Forms.CheckBox checkBoxSelectAllTestRuns;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonRemoveTestRuns;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBoxFirstIndexForRemoving;
        private System.Windows.Forms.TextBox textBoxLastIndexForRemoving;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button buttonRemoveCatiInstances;
        private System.Windows.Forms.Label labelCatiInstancesRemovingAction;
        private System.Windows.Forms.CheckBox checkBoxSetNewBrokerOnResore;
    }
}

