namespace SurgeryHelper.Forms
{
    partial class MergeForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MergeForm));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelInfo = new System.Windows.Forms.Label();
            this.groupBoxMerge = new System.Windows.Forms.GroupBox();
            this.buttonCopyAllNewToOwn = new System.Windows.Forms.Button();
            this.buttonCopyAllNewToForeign = new System.Windows.Forms.Button();
            this.checkBoxCopyPrivateFolderData = new System.Windows.Forms.CheckBox();
            this.ForeignMergeList = new SurgeryHelper.MyControls.MultiRowListBox();
            this.OwnMergeList = new SurgeryHelper.MyControls.MultiRowListBox();
            this.buttonCopySelectedToOwn = new System.Windows.Forms.Button();
            this.buttonCopySelectedToForeign = new System.Windows.Forms.Button();
            this.groupBoxBoth = new System.Windows.Forms.GroupBox();
            this.checkBoxShowPrivateFolderDiffs = new System.Windows.Forms.CheckBox();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.BothMergeList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxStatus = new System.Windows.Forms.TextBox();
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.labelMove = new System.Windows.Forms.Label();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.groupBoxMerge.SuspendLayout();
            this.groupBoxBoth.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BothMergeList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 16);
            this.label1.TabIndex = 83;
            this.label1.Text = "Список объектов нашей базы";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(305, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(249, 16);
            this.label2.TabIndex = 84;
            this.label2.Text = "Список объектов внешней базы";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelInfo
            // 
            this.labelInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelInfo.AutoSize = true;
            this.labelInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInfo.Location = new System.Drawing.Point(5, 312);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(361, 13);
            this.labelInfo.TabIndex = 87;
            this.labelInfo.Text = "Запущено сравнение пациентов из нашей и внешней баз...";
            // 
            // groupBoxMerge
            // 
            this.groupBoxMerge.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBoxMerge.Controls.Add(this.buttonCopyAllNewToOwn);
            this.groupBoxMerge.Controls.Add(this.buttonCopyAllNewToForeign);
            this.groupBoxMerge.Controls.Add(this.checkBoxCopyPrivateFolderData);
            this.groupBoxMerge.Controls.Add(this.ForeignMergeList);
            this.groupBoxMerge.Controls.Add(this.OwnMergeList);
            this.groupBoxMerge.Controls.Add(this.label1);
            this.groupBoxMerge.Controls.Add(this.labelInfo);
            this.groupBoxMerge.Controls.Add(this.buttonCopySelectedToOwn);
            this.groupBoxMerge.Controls.Add(this.label2);
            this.groupBoxMerge.Controls.Add(this.buttonCopySelectedToForeign);
            this.groupBoxMerge.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxMerge.Location = new System.Drawing.Point(4, 7);
            this.groupBoxMerge.Name = "groupBoxMerge";
            this.groupBoxMerge.Size = new System.Drawing.Size(563, 347);
            this.groupBoxMerge.TabIndex = 88;
            this.groupBoxMerge.TabStop = false;
            this.groupBoxMerge.Text = "Список объектов для экспорта/импорта";
            // 
            // buttonCopyAllNewToOwn
            // 
            this.buttonCopyAllNewToOwn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCopyAllNewToOwn.BackgroundImage = global::SurgeryHelper.Properties.Resources.CopyAllNewToOwn;
            this.buttonCopyAllNewToOwn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopyAllNewToOwn.FlatAppearance.BorderSize = 0;
            this.buttonCopyAllNewToOwn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyAllNewToOwn.Location = new System.Drawing.Point(261, 235);
            this.buttonCopyAllNewToOwn.Name = "buttonCopyAllNewToOwn";
            this.buttonCopyAllNewToOwn.Size = new System.Drawing.Size(40, 34);
            this.buttonCopyAllNewToOwn.TabIndex = 90;
            this.buttonCopyAllNewToOwn.UseVisualStyleBackColor = true;
            this.buttonCopyAllNewToOwn.Click += new System.EventHandler(this.buttonCopyAllNewToOwn_Click);
            this.buttonCopyAllNewToOwn.Enter += new System.EventHandler(this.buttonCopySelectedToOwn_Enter);
            this.buttonCopyAllNewToOwn.MouseEnter += new System.EventHandler(this.buttonCopyAllNewToOwn_MouseEnter);
            this.buttonCopyAllNewToOwn.MouseLeave += new System.EventHandler(this.buttonCopyAllNewToOwn_MouseLeave);
            // 
            // buttonCopyAllNewToForeign
            // 
            this.buttonCopyAllNewToForeign.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCopyAllNewToForeign.BackgroundImage = global::SurgeryHelper.Properties.Resources.CopyAllNewToForeign;
            this.buttonCopyAllNewToForeign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopyAllNewToForeign.FlatAppearance.BorderSize = 0;
            this.buttonCopyAllNewToForeign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopyAllNewToForeign.Location = new System.Drawing.Point(261, 77);
            this.buttonCopyAllNewToForeign.Name = "buttonCopyAllNewToForeign";
            this.buttonCopyAllNewToForeign.Size = new System.Drawing.Size(40, 34);
            this.buttonCopyAllNewToForeign.TabIndex = 89;
            this.buttonCopyAllNewToForeign.UseVisualStyleBackColor = true;
            this.buttonCopyAllNewToForeign.Click += new System.EventHandler(this.buttonCopyAllNewToForeign_Click);
            this.buttonCopyAllNewToForeign.Enter += new System.EventHandler(this.buttonCopySelectedToForeign_Enter);
            this.buttonCopyAllNewToForeign.MouseEnter += new System.EventHandler(this.buttonCopyAllNewToForeign_MouseEnter);
            this.buttonCopyAllNewToForeign.MouseLeave += new System.EventHandler(this.buttonCopyAllNewToForeign_MouseLeave);
            // 
            // checkBoxCopyPrivateFolderData
            // 
            this.checkBoxCopyPrivateFolderData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxCopyPrivateFolderData.AutoSize = true;
            this.checkBoxCopyPrivateFolderData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxCopyPrivateFolderData.Location = new System.Drawing.Point(8, 327);
            this.checkBoxCopyPrivateFolderData.Name = "checkBoxCopyPrivateFolderData";
            this.checkBoxCopyPrivateFolderData.Size = new System.Drawing.Size(344, 17);
            this.checkBoxCopyPrivateFolderData.TabIndex = 88;
            this.checkBoxCopyPrivateFolderData.Text = "Копировать файлы из личных папок при добавлении пациента";
            this.checkBoxCopyPrivateFolderData.UseVisualStyleBackColor = true;
            this.checkBoxCopyPrivateFolderData.CheckedChanged += new System.EventHandler(this.checkBoxCopyPrivateFolderData_CheckedChanged);
            // 
            // ForeignMergeList
            // 
            this.ForeignMergeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ForeignMergeList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ForeignMergeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ForeignMergeList.FormattingEnabled = true;
            this.ForeignMergeList.IntegralHeight = false;
            this.ForeignMergeList.Location = new System.Drawing.Point(305, 35);
            this.ForeignMergeList.Name = "ForeignMergeList";
            this.ForeignMergeList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.ForeignMergeList.Size = new System.Drawing.Size(249, 275);
            this.ForeignMergeList.TabIndex = 2;
            this.ForeignMergeList.VScrollingChange += new System.EventHandler<SurgeryHelper.Tools.CScrollEventArgs>(this.ForeignMergeList_VScrollingChange);
            this.ForeignMergeList.SelectedIndexChanged += new System.EventHandler(this.ForeignMergeList_SelectedIndexChanged);
            this.ForeignMergeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ForeignMergeList_MouseDoubleClick);
            // 
            // OwnMergeList
            // 
            this.OwnMergeList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.OwnMergeList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.OwnMergeList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.OwnMergeList.FormattingEnabled = true;
            this.OwnMergeList.IntegralHeight = false;
            this.OwnMergeList.Location = new System.Drawing.Point(8, 35);
            this.OwnMergeList.Name = "OwnMergeList";
            this.OwnMergeList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.OwnMergeList.Size = new System.Drawing.Size(249, 275);
            this.OwnMergeList.TabIndex = 0;
            this.OwnMergeList.VScrollingChange += new System.EventHandler<SurgeryHelper.Tools.CScrollEventArgs>(this.OwnMergeList_VScrollingChange);
            this.OwnMergeList.SelectedIndexChanged += new System.EventHandler(this.OwnMergeList_SelectedIndexChanged);
            this.OwnMergeList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.OwnMergeList_MouseDoubleClick);
            // 
            // buttonCopySelectedToOwn
            // 
            this.buttonCopySelectedToOwn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCopySelectedToOwn.BackgroundImage = global::SurgeryHelper.Properties.Resources.CopySelectedToOwn;
            this.buttonCopySelectedToOwn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopySelectedToOwn.FlatAppearance.BorderSize = 0;
            this.buttonCopySelectedToOwn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopySelectedToOwn.Location = new System.Drawing.Point(261, 195);
            this.buttonCopySelectedToOwn.Name = "buttonCopySelectedToOwn";
            this.buttonCopySelectedToOwn.Size = new System.Drawing.Size(40, 34);
            this.buttonCopySelectedToOwn.TabIndex = 86;
            this.buttonCopySelectedToOwn.UseVisualStyleBackColor = true;
            this.buttonCopySelectedToOwn.Click += new System.EventHandler(this.buttonCopySelectedToOwn_Click);
            this.buttonCopySelectedToOwn.Enter += new System.EventHandler(this.buttonCopySelectedToOwn_Enter);
            this.buttonCopySelectedToOwn.MouseEnter += new System.EventHandler(this.buttonCopySelectedToOwn_MouseEnter);
            this.buttonCopySelectedToOwn.MouseLeave += new System.EventHandler(this.buttonCopySelectedToOwn_MouseLeave);
            // 
            // buttonCopySelectedToForeign
            // 
            this.buttonCopySelectedToForeign.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonCopySelectedToForeign.BackgroundImage = global::SurgeryHelper.Properties.Resources.CopySelectedToForeign;
            this.buttonCopySelectedToForeign.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopySelectedToForeign.FlatAppearance.BorderSize = 0;
            this.buttonCopySelectedToForeign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopySelectedToForeign.Location = new System.Drawing.Point(261, 117);
            this.buttonCopySelectedToForeign.Name = "buttonCopySelectedToForeign";
            this.buttonCopySelectedToForeign.Size = new System.Drawing.Size(40, 34);
            this.buttonCopySelectedToForeign.TabIndex = 85;
            this.buttonCopySelectedToForeign.UseVisualStyleBackColor = true;
            this.buttonCopySelectedToForeign.Click += new System.EventHandler(this.buttonCopySelectedToForeign_Click);
            this.buttonCopySelectedToForeign.Enter += new System.EventHandler(this.buttonCopySelectedToForeign_Enter);
            this.buttonCopySelectedToForeign.MouseEnter += new System.EventHandler(this.buttonCopySelectedToForeign_MouseEnter);
            this.buttonCopySelectedToForeign.MouseLeave += new System.EventHandler(this.buttonCopySelectedToForeign_MouseLeave);
            // 
            // groupBoxBoth
            // 
            this.groupBoxBoth.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxBoth.Controls.Add(this.checkBoxShowPrivateFolderDiffs);
            this.groupBoxBoth.Controls.Add(this.buttonInfo);
            this.groupBoxBoth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxBoth.Location = new System.Drawing.Point(573, 7);
            this.groupBoxBoth.Name = "groupBoxBoth";
            this.groupBoxBoth.Size = new System.Drawing.Size(314, 347);
            this.groupBoxBoth.TabIndex = 89;
            this.groupBoxBoth.TabStop = false;
            this.groupBoxBoth.Text = "Список пациентов с отличиями в данных";
            // 
            // checkBoxShowPrivateFolderDiffs
            // 
            this.checkBoxShowPrivateFolderDiffs.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxShowPrivateFolderDiffs.AutoSize = true;
            this.checkBoxShowPrivateFolderDiffs.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxShowPrivateFolderDiffs.Location = new System.Drawing.Point(9, 320);
            this.checkBoxShowPrivateFolderDiffs.Name = "checkBoxShowPrivateFolderDiffs";
            this.checkBoxShowPrivateFolderDiffs.Size = new System.Drawing.Size(224, 17);
            this.checkBoxShowPrivateFolderDiffs.TabIndex = 87;
            this.checkBoxShowPrivateFolderDiffs.Text = "Отображать различия в личных папках";
            this.checkBoxShowPrivateFolderDiffs.UseVisualStyleBackColor = true;
            this.checkBoxShowPrivateFolderDiffs.CheckedChanged += new System.EventHandler(this.checkBoxShowPrivateFolderDiffs_CheckedChanged);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonInfo.BackgroundImage = global::SurgeryHelper.Properties.Resources.information24;
            this.buttonInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonInfo.FlatAppearance.BorderSize = 0;
            this.buttonInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonInfo.Location = new System.Drawing.Point(245, 316);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(62, 25);
            this.buttonInfo.TabIndex = 86;
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            this.buttonInfo.Enter += new System.EventHandler(this.buttonInfo_Enter);
            this.buttonInfo.MouseEnter += new System.EventHandler(this.buttonInfo_MouseEnter);
            this.buttonInfo.MouseLeave += new System.EventHandler(this.buttonInfo_MouseLeave);
            // 
            // BothMergeList
            // 
            this.BothMergeList.AllowUserToAddRows = false;
            this.BothMergeList.AllowUserToDeleteRows = false;
            this.BothMergeList.AllowUserToResizeRows = false;
            this.BothMergeList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BothMergeList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.BothMergeList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.BothMergeList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.BothMergeList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BothMergeList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn8});
            this.BothMergeList.Location = new System.Drawing.Point(582, 26);
            this.BothMergeList.MultiSelect = false;
            this.BothMergeList.Name = "BothMergeList";
            this.BothMergeList.ReadOnly = true;
            this.BothMergeList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.BothMergeList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.BothMergeList.RowTemplate.Height = 17;
            this.BothMergeList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.BothMergeList.Size = new System.Drawing.Size(298, 291);
            this.BothMergeList.StandardTab = true;
            this.BothMergeList.TabIndex = 83;
            this.BothMergeList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.BothMergeList_CellMouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "ФИО пациента";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn6.Width = 170;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Нозология";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "difference";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn5.Visible = false;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn8.HeaderText = "";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // textBoxStatus
            // 
            this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStatus.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxStatus.Location = new System.Drawing.Point(4, 366);
            this.textBoxStatus.MaxLength = 200000;
            this.textBoxStatus.Multiline = true;
            this.textBoxStatus.Name = "textBoxStatus";
            this.textBoxStatus.ReadOnly = true;
            this.textBoxStatus.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxStatus.Size = new System.Drawing.Size(751, 59);
            this.textBoxStatus.TabIndex = 90;
            this.textBoxStatus.TabStop = false;
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.BackgroundImage = global::SurgeryHelper.Properties.Resources.refresh;
            this.buttonRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonRefresh.FlatAppearance.BorderSize = 0;
            this.buttonRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRefresh.Location = new System.Drawing.Point(804, 378);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(35, 35);
            this.buttonRefresh.TabIndex = 88;
            this.buttonRefresh.TabStop = false;
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            this.buttonRefresh.Enter += new System.EventHandler(this.buttonRefresh_Enter);
            this.buttonRefresh.MouseEnter += new System.EventHandler(this.buttonRefresh_MouseEnter);
            this.buttonRefresh.MouseLeave += new System.EventHandler(this.buttonRefresh_MouseLeave);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(845, 378);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(35, 35);
            this.buttonOk.TabIndex = 91;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            // 
            // labelMove
            // 
            this.labelMove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMove.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.labelMove.Location = new System.Drawing.Point(4, 354);
            this.labelMove.Name = "labelMove";
            this.labelMove.Size = new System.Drawing.Size(883, 11);
            this.labelMove.TabIndex = 92;
            this.labelMove.LocationChanged += new System.EventHandler(this.labelMove_LocationChanged);
            this.labelMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.labelMove_MouseDown);
            this.labelMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.labelMove_MouseMove);
            this.labelMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.labelMove_MouseUp);
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClearLog.BackgroundImage = global::SurgeryHelper.Properties.Resources.CleanLog;
            this.buttonClearLog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClearLog.FlatAppearance.BorderSize = 0;
            this.buttonClearLog.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClearLog.Location = new System.Drawing.Point(761, 378);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(35, 35);
            this.buttonClearLog.TabIndex = 93;
            this.buttonClearLog.TabStop = false;
            this.buttonClearLog.UseVisualStyleBackColor = true;
            this.buttonClearLog.Click += new System.EventHandler(this.buttonClearLog_Click);
            this.buttonClearLog.Enter += new System.EventHandler(this.buttonRefresh_Enter);
            this.buttonClearLog.MouseEnter += new System.EventHandler(this.buttonClearLog_MouseEnter);
            this.buttonClearLog.MouseLeave += new System.EventHandler(this.buttonClearLog_MouseLeave);
            // 
            // MergeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 429);
            this.Controls.Add(this.buttonClearLog);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.textBoxStatus);
            this.Controls.Add(this.BothMergeList);
            this.Controls.Add(this.groupBoxBoth);
            this.Controls.Add(this.groupBoxMerge);
            this.Controls.Add(this.labelMove);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(900, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 380);
            this.Name = "MergeForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Объединение данных для двух баз";
            this.Load += new System.EventHandler(this.MergeForm_Load);
            this.Shown += new System.EventHandler(this.MergeForm_Shown);
            this.LocationChanged += new System.EventHandler(this.MergeForm_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.MergeForm_SizeChanged);
            this.groupBoxMerge.ResumeLayout(false);
            this.groupBoxMerge.PerformLayout();
            this.groupBoxBoth.ResumeLayout(false);
            this.groupBoxBoth.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BothMergeList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonCopySelectedToForeign;
        private System.Windows.Forms.Button buttonCopySelectedToOwn;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.GroupBox groupBoxMerge;
        private System.Windows.Forms.GroupBox groupBoxBoth;
        private System.Windows.Forms.Button buttonInfo;
        public System.Windows.Forms.DataGridView BothMergeList;
        private System.Windows.Forms.TextBox textBoxStatus;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label labelMove;
        private SurgeryHelper.MyControls.MultiRowListBox OwnMergeList;
        private SurgeryHelper.MyControls.MultiRowListBox ForeignMergeList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.CheckBox checkBoxShowPrivateFolderDiffs;
        private System.Windows.Forms.CheckBox checkBoxCopyPrivateFolderData;
        private System.Windows.Forms.Button buttonCopyAllNewToOwn;
        private System.Windows.Forms.Button buttonCopyAllNewToForeign;
        private System.Windows.Forms.Button buttonClearLog;
    }
}