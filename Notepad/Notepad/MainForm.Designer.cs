namespace Notepad
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
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.contextMenuCopy = new System.Windows.Forms.ToolStripMenuItem();
        this.contextMenuCut = new System.Windows.Forms.ToolStripMenuItem();
        this.contextMenuPaste = new System.Windows.Forms.ToolStripMenuItem();
        this.contextMenuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFileNew = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFileSave = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        this.menuFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
        this.menuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
        this.menuFileExit = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEdit = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditWriteHistory = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditUndo = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditRedo = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        this.menuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditCut = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditGoTo = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
        this.menuEditFont = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditWrap = new System.Windows.Forms.ToolStripMenuItem();
        this.menuEditAttrib = new System.Windows.Forms.ToolStripMenuItem();
        this.menuAction = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionFind = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionReplace = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionQuickReplace = new System.Windows.Forms.ToolStripMenuItem();
        this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
        this.menuActionConvert = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionUninstall = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionProcess = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionZlibArchive = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionInvolvedGuids = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionReplaceGuids = new System.Windows.Forms.ToolStripMenuItem();
        this.menuActionAutostart = new System.Windows.Forms.ToolStripMenuItem();
        this.menuInform = new System.Windows.Forms.ToolStripMenuItem();
        this.menuInformAbout = new System.Windows.Forms.ToolStripMenuItem();
        this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
        this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
        this.statusStrip1 = new System.Windows.Forms.StatusStrip();
        this.toolStripRows = new System.Windows.Forms.ToolStripStatusLabel();
        this.toolStripColumns = new System.Windows.Forms.ToolStripStatusLabel();
        this.toolStripModify = new System.Windows.Forms.ToolStripStatusLabel();
        this.fontDialog1 = new System.Windows.Forms.FontDialog();
        this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
        this.printDocument1 = new System.Drawing.Printing.PrintDocument();
        this.printDialog1 = new System.Windows.Forms.PrintDialog();
        this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
        this.textWindow = new System.Windows.Forms.TextBox();
        this.contextMenuStrip1.SuspendLayout();
        this.menuStrip1.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuCopy,
            this.contextMenuCut,
            this.contextMenuPaste,
            this.contextMenuSelectAll});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(146, 92);
        // 
        // contextMenuCopy
        // 
        this.contextMenuCopy.Image = global::Notepad.Properties.Resources.copy16;
        this.contextMenuCopy.Name = "contextMenuCopy";
        this.contextMenuCopy.Size = new System.Drawing.Size(145, 22);
        this.contextMenuCopy.Text = "Копировать";
        this.contextMenuCopy.Click += new System.EventHandler(this.copy_Click);
        // 
        // contextMenuCut
        // 
        this.contextMenuCut.Image = global::Notepad.Properties.Resources.cut16;
        this.contextMenuCut.Name = "contextMenuCut";
        this.contextMenuCut.Size = new System.Drawing.Size(145, 22);
        this.contextMenuCut.Text = "Вырезать";
        this.contextMenuCut.Click += new System.EventHandler(this.cut_Click);
        // 
        // contextMenuPaste
        // 
        this.contextMenuPaste.Image = global::Notepad.Properties.Resources.paste16;
        this.contextMenuPaste.Name = "contextMenuPaste";
        this.contextMenuPaste.Size = new System.Drawing.Size(145, 22);
        this.contextMenuPaste.Text = "Вставить";
        this.contextMenuPaste.Click += new System.EventHandler(this.paste_Click);
        // 
        // contextMenuSelectAll
        // 
        this.contextMenuSelectAll.Image = global::Notepad.Properties.Resources.selectAllNew16;
        this.contextMenuSelectAll.Name = "contextMenuSelectAll";
        this.contextMenuSelectAll.Size = new System.Drawing.Size(145, 22);
        this.contextMenuSelectAll.Text = "Выделить всё";
        this.contextMenuSelectAll.Click += new System.EventHandler(this.selectAll_Click);
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuEdit,
            this.menuAction,
            this.menuInform});
        this.menuStrip1.Location = new System.Drawing.Point(0, 0);
        this.menuStrip1.Name = "menuStrip1";
        this.menuStrip1.Size = new System.Drawing.Size(575, 24);
        this.menuStrip1.TabIndex = 1;
        this.menuStrip1.Text = "menuStrip1";
        // 
        // menuFile
        // 
        this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFileNew,
            this.menuFileOpen,
            this.menuFileSave,
            this.menuFileSaveAs,
            this.toolStripSeparator1,
            this.menuFilePageSetup,
            this.menuFilePreview,
            this.menuFilePrint,
            this.toolStripSeparator4,
            this.menuFileExit});
        this.menuFile.Name = "menuFile";
        this.menuFile.Size = new System.Drawing.Size(45, 20);
        this.menuFile.Text = "&Файл";
        // 
        // menuFileNew
        // 
        this.menuFileNew.Image = global::Notepad.Properties.Resources.new16;
        this.menuFileNew.Name = "menuFileNew";
        this.menuFileNew.ShortcutKeys = System.Windows.Forms.Keys.F2;
        this.menuFileNew.Size = new System.Drawing.Size(230, 22);
        this.menuFileNew.Text = "&Новый";
        this.menuFileNew.Click += new System.EventHandler(this.menuFileNew_Click);
        // 
        // menuFileOpen
        // 
        this.menuFileOpen.Image = global::Notepad.Properties.Resources.open16;
        this.menuFileOpen.Name = "menuFileOpen";
        this.menuFileOpen.ShortcutKeys = System.Windows.Forms.Keys.F6;
        this.menuFileOpen.Size = new System.Drawing.Size(230, 22);
        this.menuFileOpen.Text = "&Открыть...";
        this.menuFileOpen.Click += new System.EventHandler(this.menuFileOpen_Click);
        // 
        // menuFileSave
        // 
        this.menuFileSave.Image = global::Notepad.Properties.Resources.save16;
        this.menuFileSave.Name = "menuFileSave";
        this.menuFileSave.ShortcutKeys = System.Windows.Forms.Keys.F5;
        this.menuFileSave.Size = new System.Drawing.Size(230, 22);
        this.menuFileSave.Text = "&Сохранить...";
        this.menuFileSave.Click += new System.EventHandler(this.menuFileSave_Click);
        // 
        // menuFileSaveAs
        // 
        this.menuFileSaveAs.Image = global::Notepad.Properties.Resources.saveas16;
        this.menuFileSaveAs.Name = "menuFileSaveAs";
        this.menuFileSaveAs.ShortcutKeys = System.Windows.Forms.Keys.F4;
        this.menuFileSaveAs.Size = new System.Drawing.Size(230, 22);
        this.menuFileSaveAs.Text = "Сохранить &как...";
        this.menuFileSaveAs.Click += new System.EventHandler(this.menuFileSaveAs_Click);
        // 
        // toolStripSeparator1
        // 
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new System.Drawing.Size(227, 6);
        // 
        // menuFilePageSetup
        // 
        this.menuFilePageSetup.Image = global::Notepad.Properties.Resources.pageSettings16;
        this.menuFilePageSetup.Name = "menuFilePageSetup";
        this.menuFilePageSetup.Size = new System.Drawing.Size(230, 22);
        this.menuFilePageSetup.Text = "Параметры страни&цы...";
        this.menuFilePageSetup.Click += new System.EventHandler(this.menuFilePageSetup_Click);
        // 
        // menuFilePreview
        // 
        this.menuFilePreview.Image = global::Notepad.Properties.Resources.pintView16;
        this.menuFilePreview.Name = "menuFilePreview";
        this.menuFilePreview.Size = new System.Drawing.Size(230, 22);
        this.menuFilePreview.Text = "П&редварительный просмотр...";
        this.menuFilePreview.Click += new System.EventHandler(this.menuFilePreview_Click);
        // 
        // menuFilePrint
        // 
        this.menuFilePrint.Image = global::Notepad.Properties.Resources.print16;
        this.menuFilePrint.Name = "menuFilePrint";
        this.menuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
        this.menuFilePrint.Size = new System.Drawing.Size(230, 22);
        this.menuFilePrint.Text = "&Печать";
        this.menuFilePrint.Click += new System.EventHandler(this.menuFilePrint_Click);
        // 
        // toolStripSeparator4
        // 
        this.toolStripSeparator4.Name = "toolStripSeparator4";
        this.toolStripSeparator4.Size = new System.Drawing.Size(227, 6);
        // 
        // menuFileExit
        // 
        this.menuFileExit.Image = global::Notepad.Properties.Resources.close16;
        this.menuFileExit.Name = "menuFileExit";
        this.menuFileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
        this.menuFileExit.Size = new System.Drawing.Size(230, 22);
        this.menuFileExit.Text = "&Выход";
        this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
        // 
        // menuEdit
        // 
        this.menuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEditWriteHistory,
            this.menuEditUndo,
            this.menuEditRedo,
            this.toolStripSeparator2,
            this.menuEditCopy,
            this.menuEditCut,
            this.menuEditPaste,
            this.menuEditSelectAll,
            this.menuEditGoTo,
            this.toolStripSeparator5,
            this.menuEditFont,
            this.menuEditWrap,
            this.menuEditAttrib});
        this.menuEdit.Name = "menuEdit";
        this.menuEdit.Size = new System.Drawing.Size(104, 20);
        this.menuEdit.Text = "&Редактирование";
        // 
        // menuEditWriteHistory
        // 
        this.menuEditWriteHistory.CheckOnClick = true;
        this.menuEditWriteHistory.Name = "menuEditWriteHistory";
        this.menuEditWriteHistory.Size = new System.Drawing.Size(221, 22);
        this.menuEditWriteHistory.Text = "&Вести историю";
        this.menuEditWriteHistory.CheckedChanged += new System.EventHandler(this.menuHistoryWrite_CheckedChanged);
        // 
        // menuEditUndo
        // 
        this.menuEditUndo.Enabled = false;
        this.menuEditUndo.Image = global::Notepad.Properties.Resources.undo16;
        this.menuEditUndo.Name = "menuEditUndo";
        this.menuEditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
        this.menuEditUndo.Size = new System.Drawing.Size(221, 22);
        this.menuEditUndo.Text = "&Отменить";
        this.menuEditUndo.Click += new System.EventHandler(this.menuHistoryUndo_Click);
        // 
        // menuEditRedo
        // 
        this.menuEditRedo.Enabled = false;
        this.menuEditRedo.Image = global::Notepad.Properties.Resources.redo16;
        this.menuEditRedo.Name = "menuEditRedo";
        this.menuEditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
        this.menuEditRedo.Size = new System.Drawing.Size(221, 22);
        this.menuEditRedo.Text = "&Вернуть";
        this.menuEditRedo.Click += new System.EventHandler(this.menuHistoryRedo_Click);
        // 
        // toolStripSeparator2
        // 
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new System.Drawing.Size(218, 6);
        // 
        // menuEditCopy
        // 
        this.menuEditCopy.Image = global::Notepad.Properties.Resources.copy16;
        this.menuEditCopy.Name = "menuEditCopy";
        this.menuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
        this.menuEditCopy.Size = new System.Drawing.Size(221, 22);
        this.menuEditCopy.Text = "Копировать";
        this.menuEditCopy.Click += new System.EventHandler(this.copy_Click);
        // 
        // menuEditCut
        // 
        this.menuEditCut.Image = global::Notepad.Properties.Resources.cut16;
        this.menuEditCut.Name = "menuEditCut";
        this.menuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
        this.menuEditCut.Size = new System.Drawing.Size(221, 22);
        this.menuEditCut.Text = "Вырезать";
        this.menuEditCut.Click += new System.EventHandler(this.cut_Click);
        // 
        // menuEditPaste
        // 
        this.menuEditPaste.Image = global::Notepad.Properties.Resources.paste16;
        this.menuEditPaste.Name = "menuEditPaste";
        this.menuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
        this.menuEditPaste.Size = new System.Drawing.Size(221, 22);
        this.menuEditPaste.Text = "Вставить";
        this.menuEditPaste.Click += new System.EventHandler(this.paste_Click);
        // 
        // menuEditSelectAll
        // 
        this.menuEditSelectAll.Image = global::Notepad.Properties.Resources.selectAllNew16;
        this.menuEditSelectAll.Name = "menuEditSelectAll";
        this.menuEditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
        this.menuEditSelectAll.Size = new System.Drawing.Size(221, 22);
        this.menuEditSelectAll.Text = "Выделить всё";
        this.menuEditSelectAll.Click += new System.EventHandler(this.selectAll_Click);
        // 
        // menuEditGoTo
        // 
        this.menuEditGoTo.Image = global::Notepad.Properties.Resources.goto16;
        this.menuEditGoTo.Name = "menuEditGoTo";
        this.menuEditGoTo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
        this.menuEditGoTo.Size = new System.Drawing.Size(221, 22);
        this.menuEditGoTo.Text = "Перейти на строку...";
        this.menuEditGoTo.Click += new System.EventHandler(this.menuActionGoTo_Click);
        // 
        // toolStripSeparator5
        // 
        this.toolStripSeparator5.Name = "toolStripSeparator5";
        this.toolStripSeparator5.Size = new System.Drawing.Size(218, 6);
        // 
        // menuEditFont
        // 
        this.menuEditFont.Image = global::Notepad.Properties.Resources.font16;
        this.menuEditFont.Name = "menuEditFont";
        this.menuEditFont.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
        this.menuEditFont.Size = new System.Drawing.Size(221, 22);
        this.menuEditFont.Text = "Шрифт";
        this.menuEditFont.Click += new System.EventHandler(this.menuHistoryFont_Click);
        // 
        // menuEditWrap
        // 
        this.menuEditWrap.CheckOnClick = true;
        this.menuEditWrap.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        this.menuEditWrap.Name = "menuEditWrap";
        this.menuEditWrap.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
        this.menuEditWrap.Size = new System.Drawing.Size(221, 22);
        this.menuEditWrap.Text = "&Перенос строк";
        this.menuEditWrap.CheckedChanged += new System.EventHandler(this.menuActionWrap_CheckedChanged);
        // 
        // menuEditAttrib
        // 
        this.menuEditAttrib.Name = "menuEditAttrib";
        this.menuEditAttrib.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
        this.menuEditAttrib.Size = new System.Drawing.Size(221, 22);
        this.menuEditAttrib.Text = "Аттрибуты";
        this.menuEditAttrib.Click += new System.EventHandler(this.menuEditAttrib_Click);
        // 
        // menuAction
        // 
        this.menuAction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuActionFind,
            this.menuActionReplace,
            this.menuActionQuickReplace,
            this.toolStripSeparator3,
            this.menuActionConvert,
            this.menuActionUninstall,
            this.menuActionProcess,
            this.menuActionZlibArchive,
            this.menuActionInvolvedGuids,
            this.menuActionReplaceGuids,
            this.menuActionAutostart});
        this.menuAction.Name = "menuAction";
        this.menuAction.Size = new System.Drawing.Size(68, 20);
        this.menuAction.Text = "&Действия";
        // 
        // menuActionFind
        // 
        this.menuActionFind.Image = global::Notepad.Properties.Resources.find16;
        this.menuActionFind.Name = "menuActionFind";
        this.menuActionFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
        this.menuActionFind.Size = new System.Drawing.Size(224, 22);
        this.menuActionFind.Text = "&Поиск...";
        this.menuActionFind.Click += new System.EventHandler(this.menuActionFindReplace_Click);
        // 
        // menuActionReplace
        // 
        this.menuActionReplace.Image = global::Notepad.Properties.Resources.replace16;
        this.menuActionReplace.Name = "menuActionReplace";
        this.menuActionReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
        this.menuActionReplace.Size = new System.Drawing.Size(224, 22);
        this.menuActionReplace.Text = "&Замена...";
        this.menuActionReplace.Click += new System.EventHandler(this.menuActionFindReplace_Click);
        // 
        // menuActionQuickReplace
        // 
        this.menuActionQuickReplace.Image = global::Notepad.Properties.Resources.replace16;
        this.menuActionQuickReplace.Name = "menuActionQuickReplace";
        this.menuActionQuickReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
        this.menuActionQuickReplace.Size = new System.Drawing.Size(224, 22);
        this.menuActionQuickReplace.Text = "Быстрая замена...";
        this.menuActionQuickReplace.Click += new System.EventHandler(this.menuActionQuickReplace_Click);
        // 
        // toolStripSeparator3
        // 
        this.toolStripSeparator3.Name = "toolStripSeparator3";
        this.toolStripSeparator3.Size = new System.Drawing.Size(221, 6);
        // 
        // menuActionConvert
        // 
        this.menuActionConvert.Image = global::Notepad.Properties.Resources.convert16;
        this.menuActionConvert.Name = "menuActionConvert";
        this.menuActionConvert.Size = new System.Drawing.Size(224, 22);
        this.menuActionConvert.Text = "&Конвертация...";
        this.menuActionConvert.Click += new System.EventHandler(this.menuActionConvert_Click);
        // 
        // menuActionUninstall
        // 
        this.menuActionUninstall.Image = global::Notepad.Properties.Resources.UninstallProgram16;
        this.menuActionUninstall.Name = "menuActionUninstall";
        this.menuActionUninstall.Size = new System.Drawing.Size(224, 22);
        this.menuActionUninstall.Text = "&Установленные программы...";
        this.menuActionUninstall.Click += new System.EventHandler(this.menuActionUninstall_Click);
        // 
        // menuActionProcess
        // 
        this.menuActionProcess.Image = global::Notepad.Properties.Resources.process16;
        this.menuActionProcess.Name = "menuActionProcess";
        this.menuActionProcess.Size = new System.Drawing.Size(224, 22);
        this.menuActionProcess.Text = "Работа с процессами";
        this.menuActionProcess.Click += new System.EventHandler(this.menuActionProcess_Click);
        // 
        // menuActionZlibArchive
        // 
        this.menuActionZlibArchive.Image = global::Notepad.Properties.Resources.zlib16;
        this.menuActionZlibArchive.Name = "menuActionZlibArchive";
        this.menuActionZlibArchive.Size = new System.Drawing.Size(224, 22);
        this.menuActionZlibArchive.Text = "Работа с zlib архивом";
        this.menuActionZlibArchive.Click += new System.EventHandler(this.menuActionZlibArchive_Click);
        // 
        // menuActionInvolvedGuids
        // 
        this.menuActionInvolvedGuids.Name = "menuActionInvolvedGuids";
        this.menuActionInvolvedGuids.Size = new System.Drawing.Size(224, 22);
        this.menuActionInvolvedGuids.Text = "ГУИДы";
        this.menuActionInvolvedGuids.Click += new System.EventHandler(this.menuActionInvolvedGuids_Click);
        // 
        // menuActionReplaceGuids
        // 
        this.menuActionReplaceGuids.Name = "menuActionReplaceGuids";
        this.menuActionReplaceGuids.Size = new System.Drawing.Size(224, 22);
        this.menuActionReplaceGuids.Text = "Замена текста в файлах";
        this.menuActionReplaceGuids.Click += new System.EventHandler(this.menuActionReplaceGuids_Click);
        // 
        // menuActionAutostart
        // 
        this.menuActionAutostart.Name = "menuActionAutostart";
        this.menuActionAutostart.Size = new System.Drawing.Size(224, 22);
        this.menuActionAutostart.Text = "&Автозагрузка";
        this.menuActionAutostart.Click += new System.EventHandler(this.menuActionAutostart_Click);
        // 
        // menuInform
        // 
        this.menuInform.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuInformAbout});
        this.menuInform.Name = "menuInform";
        this.menuInform.Size = new System.Drawing.Size(82, 20);
        this.menuInform.Text = "&Информация";
        // 
        // menuInformAbout
        // 
        this.menuInformAbout.Image = global::Notepad.Properties.Resources.about16;
        this.menuInformAbout.Name = "menuInformAbout";
        this.menuInformAbout.ShortcutKeys = System.Windows.Forms.Keys.F1;
        this.menuInformAbout.Size = new System.Drawing.Size(169, 22);
        this.menuInformAbout.Text = "О &программе...";
        this.menuInformAbout.Click += new System.EventHandler(this.menuInformAbout_Click);
        // 
        // openFileDialog1
        // 
        this.openFileDialog1.Filter = "Текстовый файл|*.txt|Все файлы|*.*";
        // 
        // saveFileDialog1
        // 
        this.saveFileDialog1.Filter = "Текстовый файл|*.txt|Все файлы|*.*";
        // 
        // statusStrip1
        // 
        this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripRows,
            this.toolStripColumns,
            this.toolStripModify});
        this.statusStrip1.Location = new System.Drawing.Point(0, 480);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new System.Drawing.Size(575, 22);
        this.statusStrip1.TabIndex = 2;
        this.statusStrip1.Text = "statusStrip1";
        // 
        // toolStripRows
        // 
        this.toolStripRows.Name = "toolStripRows";
        this.toolStripRows.Size = new System.Drawing.Size(67, 17);
        this.toolStripRows.Text = "Строка: 1/1";
        // 
        // toolStripColumns
        // 
        this.toolStripColumns.Name = "toolStripColumns";
        this.toolStripColumns.Size = new System.Drawing.Size(73, 17);
        this.toolStripColumns.Text = "Столбец: 0/0";
        // 
        // toolStripModify
        // 
        this.toolStripModify.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
        this.toolStripModify.Name = "toolStripModify";
        this.toolStripModify.Size = new System.Drawing.Size(420, 17);
        this.toolStripModify.Spring = true;
        this.toolStripModify.Text = "Изменён (F5-сохр)";
        this.toolStripModify.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        // 
        // fontDialog1
        // 
        this.fontDialog1.AllowScriptChange = false;
        this.fontDialog1.ShowColor = true;
        // 
        // pageSetupDialog1
        // 
        this.pageSetupDialog1.Document = this.printDocument1;
        this.pageSetupDialog1.EnableMetric = true;
        // 
        // printDocument1
        // 
        this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
        // 
        // printDialog1
        // 
        this.printDialog1.UseEXDialog = true;
        // 
        // printPreviewDialog1
        // 
        this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
        this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
        this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
        this.printPreviewDialog1.Document = this.printDocument1;
        this.printPreviewDialog1.Enabled = true;
        this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
        this.printPreviewDialog1.Name = "printPreviewDialog1";
        this.printPreviewDialog1.UseAntiAlias = true;
        this.printPreviewDialog1.Visible = false;
        // 
        // textWindow
        // 
        this.textWindow.AllowDrop = true;
        this.textWindow.ContextMenuStrip = this.contextMenuStrip1;
        this.textWindow.HideSelection = false;
        this.textWindow.Location = new System.Drawing.Point(0, 24);
        this.textWindow.MaxLength = 2000000000;
        this.textWindow.Multiline = true;
        this.textWindow.Name = "textWindow";
        this.textWindow.ScrollBars = System.Windows.Forms.ScrollBars.Both;
        this.textWindow.Size = new System.Drawing.Size(575, 457);
        this.textWindow.TabIndex = 0;
        this.textWindow.TabStop = false;
        this.textWindow.WordWrap = false;
        this.textWindow.TextChanged += new System.EventHandler(this.textWindow_TextChanged);
        this.textWindow.DragDrop += new System.Windows.Forms.DragEventHandler(this.textWindow_DragDrop);
        this.textWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textWindow_KeyDown);
        this.textWindow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textWindow_KeyUp);
        this.textWindow.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textWindow_MouseDown);
        this.textWindow.MouseUp += new System.Windows.Forms.MouseEventHandler(this.textWindow_MouseUp);
        this.textWindow.DragEnter += new System.Windows.Forms.DragEventHandler(this.textWindow_DragEnter);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(575, 502);
        this.Controls.Add(this.statusStrip1);
        this.Controls.Add(this.textWindow);
        this.Controls.Add(this.menuStrip1);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.MainMenuStrip = this.menuStrip1;
        this.MinimumSize = new System.Drawing.Size(320, 220);
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Блокнот";
        this.Load += new System.EventHandler(this.MainForm_Load);
        this.InputLanguageChanged += new System.Windows.Forms.InputLanguageChangedEventHandler(this.MainForm_InputLanguageChanged);
        this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
        this.Shown += new System.EventHandler(this.MainForm_Shown);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
        this.contextMenuStrip1.ResumeLayout(false);
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem menuFile;
    private System.Windows.Forms.ToolStripMenuItem menuFileOpen;
    private System.Windows.Forms.ToolStripMenuItem menuFileSave;
    private System.Windows.Forms.ToolStripMenuItem menuFileSaveAs;
    private System.Windows.Forms.ToolStripMenuItem menuFileExit;
    private System.Windows.Forms.ToolStripMenuItem menuAction;
    private System.Windows.Forms.ToolStripMenuItem menuActionFind;
    private System.Windows.Forms.ToolStripMenuItem menuActionConvert;
    private System.Windows.Forms.OpenFileDialog openFileDialog1;
    private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    private System.Windows.Forms.ToolStripMenuItem menuActionReplace;
    private System.Windows.Forms.ToolStripMenuItem menuInform;
    private System.Windows.Forms.ToolStripMenuItem menuInformAbout;
    private System.Windows.Forms.ToolStripMenuItem menuEdit;
    private System.Windows.Forms.ToolStripMenuItem menuEditWriteHistory;
    private System.Windows.Forms.ToolStripMenuItem menuEditUndo;
    private System.Windows.Forms.ToolStripMenuItem menuEditRedo;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel toolStripRows;
    private System.Windows.Forms.ToolStripStatusLabel toolStripColumns;
    private System.Windows.Forms.ToolStripMenuItem menuEditSelectAll;
    private System.Windows.Forms.ToolStripMenuItem menuFileNew;
    private System.Windows.Forms.ToolStripMenuItem menuEditFont;
    private System.Windows.Forms.FontDialog fontDialog1;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripMenuItem menuFilePageSetup;
    private System.Windows.Forms.ToolStripMenuItem menuFilePreview;
    private System.Windows.Forms.ToolStripMenuItem menuFilePrint;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
    private System.Windows.Forms.PrintDialog printDialog1;
    private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
    private System.Drawing.Printing.PrintDocument printDocument1;
    private System.Windows.Forms.ToolStripMenuItem menuEditWrap;
    private System.Windows.Forms.ToolStripStatusLabel toolStripModify;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem contextMenuCopy;
    private System.Windows.Forms.ToolStripMenuItem contextMenuCut;
    private System.Windows.Forms.ToolStripMenuItem contextMenuPaste;
    private System.Windows.Forms.ToolStripMenuItem contextMenuSelectAll;
    private System.Windows.Forms.ToolStripMenuItem menuEditCopy;
    private System.Windows.Forms.ToolStripMenuItem menuEditCut;
    private System.Windows.Forms.ToolStripMenuItem menuEditPaste;
    private System.Windows.Forms.ToolStripMenuItem menuEditGoTo;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    public System.Windows.Forms.TextBox textWindow;
    private System.Windows.Forms.ToolStripMenuItem menuActionQuickReplace;
    private System.Windows.Forms.ToolStripMenuItem menuActionUninstall;
    private System.Windows.Forms.ToolStripMenuItem menuEditAttrib;
    private System.Windows.Forms.ToolStripMenuItem menuActionInvolvedGuids;
    private System.Windows.Forms.ToolStripMenuItem menuActionReplaceGuids;
    private System.Windows.Forms.ToolStripMenuItem menuActionZlibArchive;
    private System.Windows.Forms.ToolStripMenuItem menuActionProcess;
    private System.Windows.Forms.ToolStripMenuItem menuActionAutostart;
  }
}

