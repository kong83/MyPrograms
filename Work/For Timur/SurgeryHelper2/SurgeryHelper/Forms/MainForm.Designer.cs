namespace SurgeryHelper.Forms
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
            this.menuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileDatabaseSaveFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileGlobalSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileColorInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileChangePassword = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItemWindows = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsPatientList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsNoslogyList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsSurgeonList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsScrubNurseList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsOrderlyList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemWindowsOperationTypeList = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemTools = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolsNegatoskop = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolsImport = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemToolsCheckDB = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuItemFile
            // 
            this.menuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFileDatabaseSaveFolder,
            this.menuItemFileGlobalSettings,
            this.menuItemFileColorInfo,
            this.menuItemFileChangePassword,
            this.menuItemFileExit});
            this.menuItemFile.Name = "menuItemFile";
            this.menuItemFile.Size = new System.Drawing.Size(48, 20);
            this.menuItemFile.Text = "&Файл";
            // 
            // menuItemFileDatabaseSaveFolder
            // 
            this.menuItemFileDatabaseSaveFolder.Name = "menuItemFileDatabaseSaveFolder";
            this.menuItemFileDatabaseSaveFolder.Size = new System.Drawing.Size(260, 22);
            this.menuItemFileDatabaseSaveFolder.Text = "Открыть сохранённые версии &баз";
            this.menuItemFileDatabaseSaveFolder.Click += new System.EventHandler(this.menuItemFileDatabaseSaveFolder_Click);
            // 
            // menuItemFileGlobalSettings
            // 
            this.menuItemFileGlobalSettings.Name = "menuItemFileGlobalSettings";
            this.menuItemFileGlobalSettings.Size = new System.Drawing.Size(260, 22);
            this.menuItemFileGlobalSettings.Text = "&Глобальные настройки...";
            this.menuItemFileGlobalSettings.Click += new System.EventHandler(this.menuItemFileGlobalSettings_Click);
            // 
            // menuItemFileColorInfo
            // 
            this.menuItemFileColorInfo.Name = "menuItemFileColorInfo";
            this.menuItemFileColorInfo.Size = new System.Drawing.Size(260, 22);
            this.menuItemFileColorInfo.Text = "&Подсветка строк...";
            this.menuItemFileColorInfo.Click += new System.EventHandler(this.menuItemFileColorInfo_Click);
            // 
            // menuItemFileChangePassword
            // 
            this.menuItemFileChangePassword.Name = "menuItemFileChangePassword";
            this.menuItemFileChangePassword.Size = new System.Drawing.Size(260, 22);
            this.menuItemFileChangePassword.Text = "&Изменить пароль...";
            this.menuItemFileChangePassword.Click += new System.EventHandler(this.menuItemFileChangePassword_Click);
            // 
            // menuItemFileExit
            // 
            this.menuItemFileExit.Name = "menuItemFileExit";
            this.menuItemFileExit.Size = new System.Drawing.Size(260, 22);
            this.menuItemFileExit.Text = "&Выход";
            this.menuItemFileExit.Click += new System.EventHandler(this.menuItemFileExit_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemHelpAbout});
            this.menuItemHelp.Name = "menuItemHelp";
            this.menuItemHelp.Size = new System.Drawing.Size(68, 20);
            this.menuItemHelp.Text = "&Помощь";
            // 
            // menuItemHelpAbout
            // 
            this.menuItemHelpAbout.Name = "menuItemHelpAbout";
            this.menuItemHelpAbout.Size = new System.Drawing.Size(158, 22);
            this.menuItemHelpAbout.Text = "&О программе...";
            this.menuItemHelpAbout.Click += new System.EventHandler(this.menuItemHelpAbout_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemFile,
            this.menuItemWindows,
            this.menuItemTools,
            this.menuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItemWindows
            // 
            this.menuItemWindows.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemWindowsPatientList,
            this.menuItemWindowsNoslogyList,
            this.menuItemWindowsSurgeonList,
            this.menuItemWindowsScrubNurseList,
            this.menuItemWindowsOrderlyList,
            this.menuItemWindowsOperationTypeList});
            this.menuItemWindows.Name = "menuItemWindows";
            this.menuItemWindows.Size = new System.Drawing.Size(47, 20);
            this.menuItemWindows.Text = "&Окна";
            // 
            // menuItemWindowsPatientList
            // 
            this.menuItemWindowsPatientList.Name = "menuItemWindowsPatientList";
            this.menuItemWindowsPatientList.Size = new System.Drawing.Size(264, 22);
            this.menuItemWindowsPatientList.Text = "Отобразить список &пациентов";
            this.menuItemWindowsPatientList.Click += new System.EventHandler(this.menuItemWindowsPatientList_Click);
            // 
            // menuItemWindowsNoslogyList
            // 
            this.menuItemWindowsNoslogyList.Name = "menuItemWindowsNoslogyList";
            this.menuItemWindowsNoslogyList.Size = new System.Drawing.Size(264, 22);
            this.menuItemWindowsNoslogyList.Text = "Открыть список &нозологий...";
            this.menuItemWindowsNoslogyList.Click += new System.EventHandler(this.menuItemWindowsNoslogyList_Click);
            // 
            // menuItemWindowsSurgeonList
            // 
            this.menuItemWindowsSurgeonList.Name = "menuItemWindowsSurgeonList";
            this.menuItemWindowsSurgeonList.Size = new System.Drawing.Size(264, 22);
            this.menuItemWindowsSurgeonList.Text = "Открыть список &хирургов...";
            this.menuItemWindowsSurgeonList.Click += new System.EventHandler(this.menuItemWindowsSurgeonList_Click);
            // 
            // menuItemWindowsScrubNurseList
            // 
            this.menuItemWindowsScrubNurseList.Name = "menuItemWindowsScrubNurseList";
            this.menuItemWindowsScrubNurseList.Size = new System.Drawing.Size(264, 22);
            this.menuItemWindowsScrubNurseList.Text = "Открыть список &мед. сестёр...";
            this.menuItemWindowsScrubNurseList.Click += new System.EventHandler(this.menuItemWindowsScrubNurseList_Click);
            // 
            // menuItemWindowsOrderlyList
            // 
            this.menuItemWindowsOrderlyList.Name = "menuItemWindowsOrderlyList";
            this.menuItemWindowsOrderlyList.Size = new System.Drawing.Size(264, 22);
            this.menuItemWindowsOrderlyList.Text = "Открыть список &санитаров...";
            this.menuItemWindowsOrderlyList.Click += new System.EventHandler(this.menuItemWindowsOrderlyList_Click);
            // 
            // menuItemWindowsOperationTypeList
            // 
            this.menuItemWindowsOperationTypeList.Name = "menuItemWindowsOperationTypeList";
            this.menuItemWindowsOperationTypeList.Size = new System.Drawing.Size(264, 22);
            this.menuItemWindowsOperationTypeList.Text = "Открыть список типов &операций...";
            this.menuItemWindowsOperationTypeList.Click += new System.EventHandler(this.menuItemWindowsOperationTypeList_Click);
            // 
            // menuItemTools
            // 
            this.menuItemTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemToolsNegatoskop,
            this.menuItemToolsImport,
            this.menuItemToolsCheckDB});
            this.menuItemTools.Name = "menuItemTools";
            this.menuItemTools.Size = new System.Drawing.Size(95, 20);
            this.menuItemTools.Text = "&Инструменты";
            // 
            // menuItemToolsNegatoskop
            // 
            this.menuItemToolsNegatoskop.Name = "menuItemToolsNegatoskop";
            this.menuItemToolsNegatoskop.Size = new System.Drawing.Size(201, 22);
            this.menuItemToolsNegatoskop.Text = "&Негатоскоп";
            this.menuItemToolsNegatoskop.Click += new System.EventHandler(this.menuItemToolsNegatoskop_Click);
            // 
            // menuItemToolsImport
            // 
            this.menuItemToolsImport.Name = "menuItemToolsImport";
            this.menuItemToolsImport.Size = new System.Drawing.Size(201, 22);
            this.menuItemToolsImport.Text = "&Объединение данных...";
            this.menuItemToolsImport.Click += new System.EventHandler(this.menuItemToolsImport_Click);
            // 
            // menuItemToolsCheckDB
            // 
            this.menuItemToolsCheckDB.Name = "menuItemToolsCheckDB";
            this.menuItemToolsCheckDB.Size = new System.Drawing.Size(201, 22);
            this.menuItemToolsCheckDB.Text = "&Проверка базы...";
            this.menuItemToolsCheckDB.Click += new System.EventHandler(this.menuItemToolsCheckDB_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 486);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "MainForm";
            this.Text = "Электронный ординатор";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem menuItemFile;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileExit;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem menuItemHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileChangePassword;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileGlobalSettings;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemTools;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsNegatoskop;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsImport;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileDatabaseSaveFolder;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindows;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsPatientList;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsNoslogyList;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsSurgeonList;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsScrubNurseList;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsOrderlyList;
        private System.Windows.Forms.ToolStripMenuItem menuItemWindowsOperationTypeList;
        private System.Windows.Forms.ToolStripMenuItem menuItemFileColorInfo;
        private System.Windows.Forms.ToolStripMenuItem menuItemToolsCheckDB;
    }
}

