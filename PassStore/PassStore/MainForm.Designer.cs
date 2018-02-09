namespace PassStore
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
        System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
        this.PassList = new System.Windows.Forms.DataGridView();
        this.nameParth = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
        this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
        this.buttonClose = new System.Windows.Forms.Button();
        this.buttonDelete = new System.Windows.Forms.Button();
        this.buttonAdd = new System.Windows.Forms.Button();
        this.buttonEdit = new System.Windows.Forms.Button();
        this.labelInfo = new System.Windows.Forms.Label();
        this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
        this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
        this.восстановитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.закрытьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.timerNotification = new System.Windows.Forms.Timer(this.components);
        ((System.ComponentModel.ISupportInitialize)(this.PassList)).BeginInit();
        this.contextMenuStrip1.SuspendLayout();
        this.SuspendLayout();
        // 
        // PassList
        // 
        this.PassList.AllowUserToAddRows = false;
        this.PassList.AllowUserToDeleteRows = false;
        this.PassList.AllowUserToResizeRows = false;
        this.PassList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                    | System.Windows.Forms.AnchorStyles.Left)
                    | System.Windows.Forms.AnchorStyles.Right)));
        this.PassList.BackgroundColor = System.Drawing.SystemColors.Window;
        this.PassList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
        this.PassList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
        this.PassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.PassList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameParth,
            this.Login,
            this.Password,
            this.EmptyColumn});
        this.PassList.Location = new System.Drawing.Point(12, 18);
        this.PassList.MultiSelect = false;
        this.PassList.Name = "PassList";
        this.PassList.ReadOnly = true;
        this.PassList.RowHeadersVisible = false;
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
        this.PassList.RowsDefaultCellStyle = dataGridViewCellStyle1;
        this.PassList.RowTemplate.Height = 17;
        this.PassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
        this.PassList.Size = new System.Drawing.Size(546, 351);
        this.PassList.StandardTab = true;
        this.PassList.TabIndex = 9;
        this.PassList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PassList_CellMouseDoubleClick);
        this.PassList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.PassList_ColumnWidthChanged);
        // 
        // nameParth
        // 
        this.nameParth.HeaderText = "Название раздела";
        this.nameParth.Name = "nameParth";
        this.nameParth.ReadOnly = true;
        this.nameParth.Width = 230;
        // 
        // Login
        // 
        this.Login.HeaderText = "Логин";
        this.Login.Name = "Login";
        this.Login.ReadOnly = true;
        this.Login.Width = 150;
        // 
        // Password
        // 
        this.Password.HeaderText = "Пароль";
        this.Password.Name = "Password";
        this.Password.ReadOnly = true;
        this.Password.Width = 150;
        // 
        // EmptyColumn
        // 
        this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
        this.EmptyColumn.HeaderText = "";
        this.EmptyColumn.MinimumWidth = 2;
        this.EmptyColumn.Name = "EmptyColumn";
        this.EmptyColumn.ReadOnly = true;
        this.EmptyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
        // 
        // buttonClose
        // 
        this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
        this.buttonClose.BackgroundImage = global::PassStore.Properties.Resources.close;
        this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonClose.FlatAppearance.BorderSize = 0;
        this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.buttonClose.Location = new System.Drawing.Point(518, 379);
        this.buttonClose.Name = "buttonClose";
        this.buttonClose.Size = new System.Drawing.Size(40, 40);
        this.buttonClose.TabIndex = 38;
        this.buttonClose.TabStop = false;
        this.buttonClose.UseVisualStyleBackColor = true;
        this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
        this.buttonClose.Click += new System.EventHandler(this.Close_Click);
        this.buttonClose.Enter += new System.EventHandler(this.Drop_Focus);
        this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
        // 
        // buttonDelete
        // 
        this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.buttonDelete.BackgroundImage = global::PassStore.Properties.Resources.delete;
        this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonDelete.FlatAppearance.BorderSize = 0;
        this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.buttonDelete.Location = new System.Drawing.Point(51, 379);
        this.buttonDelete.Name = "buttonDelete";
        this.buttonDelete.Size = new System.Drawing.Size(40, 40);
        this.buttonDelete.TabIndex = 37;
        this.buttonDelete.TabStop = false;
        this.buttonDelete.UseVisualStyleBackColor = true;
        this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
        this.buttonDelete.Click += new System.EventHandler(this.Delete_Click);
        this.buttonDelete.Enter += new System.EventHandler(this.Drop_Focus);
        this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
        // 
        // buttonAdd
        // 
        this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.buttonAdd.BackgroundImage = global::PassStore.Properties.Resources.add;
        this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlText;
        this.buttonAdd.FlatAppearance.BorderSize = 0;
        this.buttonAdd.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
        this.buttonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
        this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.buttonAdd.Location = new System.Drawing.Point(11, 379);
        this.buttonAdd.Name = "buttonAdd";
        this.buttonAdd.Size = new System.Drawing.Size(40, 40);
        this.buttonAdd.TabIndex = 35;
        this.buttonAdd.TabStop = false;
        this.buttonAdd.UseVisualStyleBackColor = true;
        this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
        this.buttonAdd.Click += new System.EventHandler(this.Add_Click);
        this.buttonAdd.Enter += new System.EventHandler(this.Drop_Focus);
        this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
        // 
        // buttonEdit
        // 
        this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
        this.buttonEdit.BackgroundImage = global::PassStore.Properties.Resources.edit;
        this.buttonEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
        this.buttonEdit.FlatAppearance.BorderSize = 0;
        this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        this.buttonEdit.Location = new System.Drawing.Point(91, 379);
        this.buttonEdit.Name = "buttonEdit";
        this.buttonEdit.Size = new System.Drawing.Size(40, 40);
        this.buttonEdit.TabIndex = 36;
        this.buttonEdit.TabStop = false;
        this.buttonEdit.UseVisualStyleBackColor = true;
        this.buttonEdit.MouseLeave += new System.EventHandler(this.buttonEdit_MouseLeave);
        this.buttonEdit.Click += new System.EventHandler(this.Edit_Click);
        this.buttonEdit.Enter += new System.EventHandler(this.Drop_Focus);
        this.buttonEdit.MouseEnter += new System.EventHandler(this.buttonEdit_MouseEnter);
        // 
        // labelInfo
        // 
        this.labelInfo.AutoSize = true;
        this.labelInfo.Location = new System.Drawing.Point(12, 2);
        this.labelInfo.Name = "labelInfo";
        this.labelInfo.Size = new System.Drawing.Size(403, 13);
        this.labelInfo.TabIndex = 39;
        this.labelInfo.Text = "Кликните дважды на ячейке для копирования хранящейся в ней информации";
        // 
        // notifyIcon1
        // 
        this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
        this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
        this.notifyIcon1.Text = "Хранилище паролей";
        this.notifyIcon1.Visible = true;
        this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
        // 
        // contextMenuStrip1
        // 
        this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.восстановитьToolStripMenuItem,
            this.закрытьToolStripMenuItem});
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new System.Drawing.Size(156, 48);
        // 
        // восстановитьToolStripMenuItem
        // 
        this.восстановитьToolStripMenuItem.Name = "восстановитьToolStripMenuItem";
        this.восстановитьToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        this.восстановитьToolStripMenuItem.Text = "Восстановить";
        this.восстановитьToolStripMenuItem.Click += new System.EventHandler(this.восстановитьToolStripMenuItem_Click);
        // 
        // закрытьToolStripMenuItem
        // 
        this.закрытьToolStripMenuItem.Name = "закрытьToolStripMenuItem";
        this.закрытьToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
        this.закрытьToolStripMenuItem.Text = "Выход";
        this.закрытьToolStripMenuItem.Click += new System.EventHandler(this.закрытьToolStripMenuItem_Click);
        // 
        // timerNotification
        // 
        this.timerNotification.Enabled = true;
        this.timerNotification.Interval = 59999;
        this.timerNotification.Tick += new System.EventHandler(this.timerNotification_Tick);
        // 
        // MainForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(570, 427);
        this.Controls.Add(this.buttonClose);
        this.Controls.Add(this.buttonDelete);
        this.Controls.Add(this.buttonAdd);
        this.Controls.Add(this.buttonEdit);
        this.Controls.Add(this.PassList);
        this.Controls.Add(this.labelInfo);
        this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        this.Location = new System.Drawing.Point(100, 100);
        this.MinimumSize = new System.Drawing.Size(250, 170);
        this.Name = "MainForm";
        this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
        this.Text = "Хранилище паролей";
        this.Load += new System.EventHandler(this.MainForm_Load);
        this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
        this.Resize += new System.EventHandler(this.MainForm_Resize);
        this.LocationChanged += new System.EventHandler(this.MainForm_LocationChanged);
        ((System.ComponentModel.ISupportInitialize)(this.PassList)).EndInit();
        this.contextMenuStrip1.ResumeLayout(false);
        this.ResumeLayout(false);
        this.PerformLayout();

    }

    #endregion

    public System.Windows.Forms.DataGridView PassList;
    private System.Windows.Forms.DataGridViewTextBoxColumn nameParth;
    private System.Windows.Forms.DataGridViewTextBoxColumn Login;
    private System.Windows.Forms.DataGridViewTextBoxColumn Password;
    private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
    private System.Windows.Forms.Button buttonDelete;
    private System.Windows.Forms.Button buttonAdd;
    private System.Windows.Forms.Button buttonEdit;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Label labelInfo;
    private System.Windows.Forms.NotifyIcon notifyIcon1;
    private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    private System.Windows.Forms.ToolStripMenuItem восстановитьToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem закрытьToolStripMenuItem;
    private System.Windows.Forms.Timer timerNotification;
  }
}

