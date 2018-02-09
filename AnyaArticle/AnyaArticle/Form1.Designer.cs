namespace AnyaArticle
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        this.dataSet1 = new System.Data.DataSet();
        this.dataTable1 = new System.Data.DataTable();
        this.dataColumn1 = new System.Data.DataColumn();
        this.dataColumn2 = new System.Data.DataColumn();
        this.dataColumn3 = new System.Data.DataColumn();
        this.dataColumn4 = new System.Data.DataColumn();
        this.dataColumn5 = new System.Data.DataColumn();
        this.dataColumn6 = new System.Data.DataColumn();
        this.dataColumn7 = new System.Data.DataColumn();
        this.dataColumn8 = new System.Data.DataColumn();
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.добавитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.изменитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.удалитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.menuStrip1 = new System.Windows.Forms.MenuStrip();
        this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.сохранитьToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.действияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.добавитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.изменитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.удалитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.фильтрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.фильтрToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
        this.измененияToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.принятьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.откатитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.ArticleList = new System.Windows.Forms.DataGridView();
        this.button10 = new System.Windows.Forms.Button();
        this.button7 = new System.Windows.Forms.Button();
        this.button8 = new System.Windows.Forms.Button();
        this.button5 = new System.Windows.Forms.Button();
        this.button9 = new System.Windows.Forms.Button();
        this.button3 = new System.Windows.Forms.Button();
        this.button4 = new System.Windows.Forms.Button();
        this.button6 = new System.Windows.Forms.Button();
        this.button2 = new System.Windows.Forms.Button();
        this.button1 = new System.Windows.Forms.Button();
        this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.добавитьToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
        this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.autorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.temaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.languageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.pathDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.yearDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.aboutDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
        this.contextMenuStrip1.SuspendLayout();
        this.menuStrip1.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)(this.ArticleList)).BeginInit();
        this.contextMenuStrip2.SuspendLayout();
        this.SuspendLayout();
        // 
        // dataSet1
        // 
        this.dataSet1.DataSetName = "NewDataSet";
        this.dataSet1.Tables.AddRange(new System.Data.DataTable[] {
            this.dataTable1});
        // 
        // dataTable1
        // 
        this.dataTable1.Columns.AddRange(new System.Data.DataColumn[] {
            this.dataColumn1,
            this.dataColumn2,
            this.dataColumn3,
            this.dataColumn4,
            this.dataColumn5,
            this.dataColumn6,
            this.dataColumn7,
            this.dataColumn8});
        this.dataTable1.TableName = "Table1";
        // 
        // dataColumn1
        // 
        this.dataColumn1.AllowDBNull = false;
        this.dataColumn1.AutoIncrement = true;
        this.dataColumn1.Caption = "Номер по порядку";
        this.dataColumn1.ColumnName = "Id";
        this.dataColumn1.DataType = typeof(int);
        this.dataColumn1.ReadOnly = true;
        // 
        // dataColumn2
        // 
        this.dataColumn2.Caption = "Название статьи";
        this.dataColumn2.ColumnName = "Name";
        this.dataColumn2.MaxLength = 255;
        // 
        // dataColumn3
        // 
        this.dataColumn3.Caption = "Автор статьи";
        this.dataColumn3.ColumnName = "Autor";
        this.dataColumn3.MaxLength = 255;
        // 
        // dataColumn4
        // 
        this.dataColumn4.Caption = "Тема статьи";
        this.dataColumn4.ColumnName = "Tema";
        this.dataColumn4.MaxLength = 255;
        // 
        // dataColumn5
        // 
        this.dataColumn5.Caption = "Язык";
        this.dataColumn5.ColumnName = "Language";
        this.dataColumn5.DataType = typeof(char);
        this.dataColumn5.ReadOnly = true;
        // 
        // dataColumn6
        // 
        this.dataColumn6.Caption = "Путь к статье";
        this.dataColumn6.ColumnName = "Path";
        this.dataColumn6.MaxLength = 255;
        // 
        // dataColumn7
        // 
        this.dataColumn7.Caption = "Год выпуска статьи";
        this.dataColumn7.ColumnName = "Year";
        this.dataColumn7.DataType = typeof(System.DateTime);
        this.dataColumn7.ReadOnly = true;
        // 
        // dataColumn8
        // 
        this.dataColumn8.Caption = "Заметки про статью";
        this.dataColumn8.ColumnName = "About";
        this.dataColumn8.MaxLength = 255;
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem1,
            this.изменитьToolStripMenuItem1,
            this.удалитьToolStripMenuItem1});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
        // 
        // добавитьToolStripMenuItem1
        // 
        this.добавитьToolStripMenuItem1.Name = "добавитьToolStripMenuItem1";
        resources.ApplyResources(this.добавитьToolStripMenuItem1, "добавитьToolStripMenuItem1");
        this.добавитьToolStripMenuItem1.Click += new System.EventHandler(this.Add_Click);
        // 
        // изменитьToolStripMenuItem1
        // 
        this.изменитьToolStripMenuItem1.Name = "изменитьToolStripMenuItem1";
        resources.ApplyResources(this.изменитьToolStripMenuItem1, "изменитьToolStripMenuItem1");
        this.изменитьToolStripMenuItem1.Click += new System.EventHandler(this.Edit_Click);
        // 
        // удалитьToolStripMenuItem1
        // 
        this.удалитьToolStripMenuItem1.Name = "удалитьToolStripMenuItem1";
        resources.ApplyResources(this.удалитьToolStripMenuItem1, "удалитьToolStripMenuItem1");
        this.удалитьToolStripMenuItem1.Click += new System.EventHandler(this.Delete_Click);
        // 
        // menuStrip1
        // 
        this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.действияToolStripMenuItem,
            this.измененияToolStripMenuItem});
        resources.ApplyResources(this.menuStrip1, "menuStrip1");
        this.menuStrip1.Name = "menuStrip1";
        // 
        // файлToolStripMenuItem
        // 
        this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьToolStripMenuItem1,
            this.печатьToolStripMenuItem,
            this.выходToolStripMenuItem});
        this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
        resources.ApplyResources(this.файлToolStripMenuItem, "файлToolStripMenuItem");
        // 
        // сохранитьToolStripMenuItem1
        // 
        this.сохранитьToolStripMenuItem1.Name = "сохранитьToolStripMenuItem1";
        resources.ApplyResources(this.сохранитьToolStripMenuItem1, "сохранитьToolStripMenuItem1");
        this.сохранитьToolStripMenuItem1.Click += new System.EventHandler(this.Save_Click);
        // 
        // печатьToolStripMenuItem
        // 
        this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
        resources.ApplyResources(this.печатьToolStripMenuItem, "печатьToolStripMenuItem");
        this.печатьToolStripMenuItem.Click += new System.EventHandler(this.Print_Click);
        // 
        // выходToolStripMenuItem
        // 
        this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
        resources.ApplyResources(this.выходToolStripMenuItem, "выходToolStripMenuItem");
        this.выходToolStripMenuItem.Click += new System.EventHandler(this.Exit_Click);
        // 
        // действияToolStripMenuItem
        // 
        this.действияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem,
            this.изменитьToolStripMenuItem,
            this.удалитьToolStripMenuItem,
            this.фильтрToolStripMenuItem,
            this.фильтрToolStripMenuItem1});
        this.действияToolStripMenuItem.Name = "действияToolStripMenuItem";
        resources.ApplyResources(this.действияToolStripMenuItem, "действияToolStripMenuItem");
        // 
        // добавитьToolStripMenuItem
        // 
        this.добавитьToolStripMenuItem.Name = "добавитьToolStripMenuItem";
        resources.ApplyResources(this.добавитьToolStripMenuItem, "добавитьToolStripMenuItem");
        this.добавитьToolStripMenuItem.Click += new System.EventHandler(this.Add_Click);
        // 
        // изменитьToolStripMenuItem
        // 
        this.изменитьToolStripMenuItem.Name = "изменитьToolStripMenuItem";
        resources.ApplyResources(this.изменитьToolStripMenuItem, "изменитьToolStripMenuItem");
        this.изменитьToolStripMenuItem.Click += new System.EventHandler(this.Edit_Click);
        // 
        // удалитьToolStripMenuItem
        // 
        this.удалитьToolStripMenuItem.Name = "удалитьToolStripMenuItem";
        resources.ApplyResources(this.удалитьToolStripMenuItem, "удалитьToolStripMenuItem");
        this.удалитьToolStripMenuItem.Click += new System.EventHandler(this.Delete_Click);
        // 
        // фильтрToolStripMenuItem
        // 
        this.фильтрToolStripMenuItem.Name = "фильтрToolStripMenuItem";
        resources.ApplyResources(this.фильтрToolStripMenuItem, "фильтрToolStripMenuItem");
        this.фильтрToolStripMenuItem.Click += new System.EventHandler(this.Filter_Click);
        // 
        // фильтрToolStripMenuItem1
        // 
        this.фильтрToolStripMenuItem1.Name = "фильтрToolStripMenuItem1";
        resources.ApplyResources(this.фильтрToolStripMenuItem1, "фильтрToolStripMenuItem1");
        this.фильтрToolStripMenuItem1.Click += new System.EventHandler(this.ShowAll_Click);
        // 
        // измененияToolStripMenuItem
        // 
        this.измененияToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.принятьToolStripMenuItem,
            this.откатитьToolStripMenuItem});
        this.измененияToolStripMenuItem.Name = "измененияToolStripMenuItem";
        resources.ApplyResources(this.измененияToolStripMenuItem, "измененияToolStripMenuItem");
        // 
        // принятьToolStripMenuItem
        // 
        this.принятьToolStripMenuItem.Name = "принятьToolStripMenuItem";
        resources.ApplyResources(this.принятьToolStripMenuItem, "принятьToolStripMenuItem");
        this.принятьToolStripMenuItem.Click += new System.EventHandler(this.Fix_Click);
        // 
        // откатитьToolStripMenuItem
        // 
        this.откатитьToolStripMenuItem.Name = "откатитьToolStripMenuItem";
        resources.ApplyResources(this.откатитьToolStripMenuItem, "откатитьToolStripMenuItem");
        this.откатитьToolStripMenuItem.Click += new System.EventHandler(this.Refresh_Click);
        // 
        // crystalReportViewer1
        // 
        this.crystalReportViewer1.ActiveViewIndex = -1;
        this.crystalReportViewer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        resources.ApplyResources(this.crystalReportViewer1, "crystalReportViewer1");
        this.crystalReportViewer1.Name = "crystalReportViewer1";
        this.crystalReportViewer1.SelectionFormula = "";
        this.crystalReportViewer1.ViewTimeSelectionFormula = "";
        // 
        // ArticleList
        // 
        this.ArticleList.AllowUserToAddRows = false;
        this.ArticleList.AllowUserToDeleteRows = false;
        this.ArticleList.AllowUserToResizeRows = false;
        resources.ApplyResources(this.ArticleList, "ArticleList");
        this.ArticleList.AutoGenerateColumns = false;
        this.ArticleList.BackgroundColor = System.Drawing.SystemColors.Window;
        this.ArticleList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.ArticleList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
        this.ArticleList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.ArticleList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.autorDataGridViewTextBoxColumn,
            this.temaDataGridViewTextBoxColumn,
            this.languageDataGridViewTextBoxColumn,
            this.pathDataGridViewTextBoxColumn,
            this.yearDataGridViewTextBoxColumn,
            this.aboutDataGridViewTextBoxColumn,
            this.EmptyColumn});
        this.ArticleList.DataMember = "Table1";
        this.ArticleList.DataSource = this.dataSet1;
        this.ArticleList.MultiSelect = false;
        this.ArticleList.Name = "ArticleList";
        this.ArticleList.ReadOnly = true;
        this.ArticleList.RowHeadersVisible = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        this.ArticleList.RowsDefaultCellStyle = dataGridViewCellStyle1;
        this.ArticleList.RowTemplate.Height = 16;
        this.ArticleList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.ArticleList.TabStop = false;
        this.ArticleList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ArticleList_CellDoubleClick);
        this.ArticleList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ArticleList_MouseClick);
        this.ArticleList.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ArticleList_CellMouseDown);        
        this.ArticleList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.ArticleList_ColumnWidthChanged);
        // 
        // button10
        // 
        resources.ApplyResources(this.button10, "button10");
        this.button10.Image = global::AnyaArticle.Properties.Resources.export;
        this.button10.Name = "button10";
        this.button10.TabStop = false;
        this.button10.UseVisualStyleBackColor = true;
        this.button10.MouseLeave += new System.EventHandler(this.button10_MouseLeave);
        this.button10.Click += new System.EventHandler(this.Print_Click);
        this.button10.MouseEnter += new System.EventHandler(this.button10_MouseEnter);
        // 
        // button7
        // 
        resources.ApplyResources(this.button7, "button7");
        this.button7.Image = global::AnyaArticle.Properties.Resources.back;
        this.button7.Name = "button7";
        this.button7.TabStop = false;
        this.button7.UseVisualStyleBackColor = true;
        this.button7.MouseLeave += new System.EventHandler(this.button7_MouseLeave);
        this.button7.Click += new System.EventHandler(this.Refresh_Click);
        this.button7.MouseEnter += new System.EventHandler(this.button7_MouseEnter);
        // 
        // button8
        // 
        resources.ApplyResources(this.button8, "button8");
        this.button8.Image = global::AnyaArticle.Properties.Resources.find;
        this.button8.Name = "button8";
        this.button8.TabStop = false;
        this.button8.UseVisualStyleBackColor = true;
        this.button8.MouseLeave += new System.EventHandler(this.button8_MouseLeave);
        this.button8.Click += new System.EventHandler(this.Filter_Click);
        this.button8.MouseEnter += new System.EventHandler(this.button8_MouseEnter);
        // 
        // button5
        // 
        resources.ApplyResources(this.button5, "button5");
        this.button5.Image = global::AnyaArticle.Properties.Resources.close;
        this.button5.Name = "button5";
        this.button5.TabStop = false;
        this.button5.UseVisualStyleBackColor = true;
        this.button5.MouseLeave += new System.EventHandler(this.button5_MouseLeave);
        this.button5.Click += new System.EventHandler(this.Exit_Click);
        this.button5.MouseEnter += new System.EventHandler(this.button5_MouseEnter);
        // 
        // button9
        // 
        resources.ApplyResources(this.button9, "button9");
        this.button9.Image = global::AnyaArticle.Properties.Resources.edit;
        this.button9.Name = "button9";
        this.button9.TabStop = false;
        this.button9.UseVisualStyleBackColor = true;
        this.button9.MouseLeave += new System.EventHandler(this.button9_MouseLeave);
        this.button9.Click += new System.EventHandler(this.Edit_Click);
        this.button9.MouseEnter += new System.EventHandler(this.button9_MouseEnter);
        // 
        // button3
        // 
        resources.ApplyResources(this.button3, "button3");
        this.button3.Image = global::AnyaArticle.Properties.Resources.fix;
        this.button3.Name = "button3";
        this.button3.TabStop = false;
        this.button3.UseVisualStyleBackColor = true;
        this.button3.MouseLeave += new System.EventHandler(this.button3_MouseLeave);
        this.button3.Click += new System.EventHandler(this.Fix_Click);
        this.button3.MouseEnter += new System.EventHandler(this.button3_MouseEnter);
        // 
        // button4
        // 
        resources.ApplyResources(this.button4, "button4");
        this.button4.Image = global::AnyaArticle.Properties.Resources.delete;
        this.button4.Name = "button4";
        this.button4.TabStop = false;
        this.button4.UseVisualStyleBackColor = true;
        this.button4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
        this.button4.Click += new System.EventHandler(this.Delete_Click);
        this.button4.MouseEnter += new System.EventHandler(this.button4_MouseEnter);
        // 
        // button6
        // 
        resources.ApplyResources(this.button6, "button6");
        this.button6.Image = global::AnyaArticle.Properties.Resources.find_all;
        this.button6.Name = "button6";
        this.button6.TabStop = false;
        this.button6.UseVisualStyleBackColor = true;
        this.button6.MouseLeave += new System.EventHandler(this.button6_MouseLeave);
        this.button6.Click += new System.EventHandler(this.ShowAll_Click);
        this.button6.MouseEnter += new System.EventHandler(this.button6_MouseEnter);
        // 
        // button2
        // 
        resources.ApplyResources(this.button2, "button2");
        this.button2.Image = global::AnyaArticle.Properties.Resources.add;
        this.button2.Name = "button2";
        this.button2.TabStop = false;
        this.button2.UseVisualStyleBackColor = true;
        this.button2.MouseLeave += new System.EventHandler(this.button2_MouseLeave);
        this.button2.Click += new System.EventHandler(this.Add_Click);
        this.button2.MouseEnter += new System.EventHandler(this.button2_MouseEnter);
        // 
        // button1
        // 
        resources.ApplyResources(this.button1, "button1");
        this.button1.Image = global::AnyaArticle.Properties.Resources.save;
        this.button1.Name = "button1";
        this.button1.TabStop = false;
        this.button1.UseVisualStyleBackColor = true;
        this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
        this.button1.Click += new System.EventHandler(this.Save_Click);
        this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
        // 
        // contextMenuStrip2
        // 
        this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.добавитьToolStripMenuItem2});
        this.contextMenuStrip2.Name = "contextMenuStrip2";
        resources.ApplyResources(this.contextMenuStrip2, "contextMenuStrip2");
        // 
        // добавитьToolStripMenuItem2
        // 
        this.добавитьToolStripMenuItem2.Name = "добавитьToolStripMenuItem2";
        resources.ApplyResources(this.добавитьToolStripMenuItem2, "добавитьToolStripMenuItem2");
        this.добавитьToolStripMenuItem2.Click += new System.EventHandler(this.Add_Click);
        // 
        // idDataGridViewTextBoxColumn
        // 
        this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
        resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
        this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
        this.idDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // nameDataGridViewTextBoxColumn
        // 
        this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
        resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
        this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
        this.nameDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // autorDataGridViewTextBoxColumn
        // 
        this.autorDataGridViewTextBoxColumn.DataPropertyName = "Autor";
        resources.ApplyResources(this.autorDataGridViewTextBoxColumn, "autorDataGridViewTextBoxColumn");
        this.autorDataGridViewTextBoxColumn.Name = "autorDataGridViewTextBoxColumn";
        this.autorDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // temaDataGridViewTextBoxColumn
        // 
        this.temaDataGridViewTextBoxColumn.DataPropertyName = "Tema";
        resources.ApplyResources(this.temaDataGridViewTextBoxColumn, "temaDataGridViewTextBoxColumn");
        this.temaDataGridViewTextBoxColumn.Name = "temaDataGridViewTextBoxColumn";
        this.temaDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // languageDataGridViewTextBoxColumn
        // 
        this.languageDataGridViewTextBoxColumn.DataPropertyName = "Language";
        resources.ApplyResources(this.languageDataGridViewTextBoxColumn, "languageDataGridViewTextBoxColumn");
        this.languageDataGridViewTextBoxColumn.Name = "languageDataGridViewTextBoxColumn";
        this.languageDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // pathDataGridViewTextBoxColumn
        // 
        this.pathDataGridViewTextBoxColumn.DataPropertyName = "Path";
        resources.ApplyResources(this.pathDataGridViewTextBoxColumn, "pathDataGridViewTextBoxColumn");
        this.pathDataGridViewTextBoxColumn.Name = "pathDataGridViewTextBoxColumn";
        this.pathDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // yearDataGridViewTextBoxColumn
        // 
        this.yearDataGridViewTextBoxColumn.DataPropertyName = "Year";
        resources.ApplyResources(this.yearDataGridViewTextBoxColumn, "yearDataGridViewTextBoxColumn");
        this.yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
        this.yearDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // aboutDataGridViewTextBoxColumn
        // 
        this.aboutDataGridViewTextBoxColumn.DataPropertyName = "About";
        resources.ApplyResources(this.aboutDataGridViewTextBoxColumn, "aboutDataGridViewTextBoxColumn");
        this.aboutDataGridViewTextBoxColumn.Name = "aboutDataGridViewTextBoxColumn";
        this.aboutDataGridViewTextBoxColumn.ReadOnly = true;
        // 
        // EmptyColumn
        // 
        this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        resources.ApplyResources(this.EmptyColumn, "EmptyColumn");
        this.EmptyColumn.Name = "EmptyColumn";
        this.EmptyColumn.ReadOnly = true;
        this.EmptyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // Form1
        // 
        resources.ApplyResources(this, "$this");
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.Controls.Add(this.button10);
        this.Controls.Add(this.ArticleList);
        this.Controls.Add(this.button7);
        this.Controls.Add(this.button8);
        this.Controls.Add(this.button5);
        this.Controls.Add(this.button9);
        this.Controls.Add(this.button3);
        this.Controls.Add(this.button4);
        this.Controls.Add(this.button6);
        this.Controls.Add(this.button2);
        this.Controls.Add(this.menuStrip1);
        this.Controls.Add(this.button1);
        this.MainMenuStrip = this.menuStrip1;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "Form1";
        this.Load += new System.EventHandler(this.Form1_Load);
        this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
        this.LocationChanged += new System.EventHandler(this.Form1_LocationChanged);
        ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
        ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
        this.contextMenuStrip1.ResumeLayout(false);
        this.menuStrip1.ResumeLayout(false);
        this.menuStrip1.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)(this.ArticleList)).EndInit();
        this.contextMenuStrip2.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    private System.Data.DataTable dataTable1;
    private System.Data.DataColumn dataColumn1;
    private System.Data.DataColumn dataColumn2;
    private System.Data.DataColumn dataColumn3;
    private System.Data.DataColumn dataColumn4;
    private System.Data.DataColumn dataColumn5;
    private System.Data.DataColumn dataColumn6;
    private System.Data.DataColumn dataColumn7;
    private System.Data.DataColumn dataColumn8;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
    private System.Windows.Forms.Button button3;
    private System.Windows.Forms.Button button4;
    private System.Windows.Forms.Button button5;
    public System.Data.DataSet dataSet1;
    private System.Windows.Forms.Button button6;
    private System.Windows.Forms.Button button7;
    private System.Windows.Forms.ToolStripMenuItem действияToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem измененияToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem принятьToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem откатитьToolStripMenuItem;
    private System.Windows.Forms.Button button8;
    private System.Windows.Forms.ToolStripMenuItem фильтрToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem фильтрToolStripMenuItem1;
    private System.Windows.Forms.Button button9;
    private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
    private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
    private System.Windows.Forms.ToolStripMenuItem сохранитьToolStripMenuItem1;
		private System.Windows.Forms.ToolTip toolTip1;
        public System.Windows.Forms.DataGridView ArticleList;
		private System.Windows.Forms.Button button10;
		private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem изменитьToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem удалитьToolStripMenuItem1;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
		private System.Windows.Forms.ToolStripMenuItem добавитьToolStripMenuItem2;
        private System.Windows.Forms.DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn autorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn temaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn languageDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn pathDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn aboutDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
  }
}

