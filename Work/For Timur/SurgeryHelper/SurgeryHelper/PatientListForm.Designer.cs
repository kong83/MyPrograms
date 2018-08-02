namespace SurgeryHelper
{
    partial class PatientListForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientListForm));
            this.PatientList = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIOColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliveryDateColumne = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReleaseDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NosologyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiagnozColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationCntColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxFilterFIO = new System.Windows.Forms.TextBox();
            this.textBoxFilterNosology = new System.Windows.Forms.TextBox();
            this.textBoxFilterDiagnose = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBoxFilterOperationCnt = new System.Windows.Forms.TextBox();
            this.dateTimePickerFilterDeliveryDateStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterDeliveryDateEnd = new System.Windows.Forms.DateTimePicker();
            this.comboBoxFilterOperationCntMode = new System.Windows.Forms.ComboBox();
            this.textBoxFilterDN = new System.Windows.Forms.TextBox();
            this.comboBoxFilterKDMode = new System.Windows.Forms.ComboBox();
            this.textBoxFilterKD = new System.Windows.Forms.TextBox();
            this.textBoxFilterDoctor = new System.Windows.Forms.TextBox();
            this.buttonHideFilter = new System.Windows.Forms.Button();
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonView = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonExportToExcel = new System.Windows.Forms.Button();
            this.buttonFilterRemove = new System.Windows.Forms.Button();
            this.buttonShowFilter = new System.Windows.Forms.Button();
            this.dateTimePickerFilterReleaseDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterReleaseDateStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterOperationDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterOperationDateStart = new System.Windows.Forms.DateTimePicker();
            this.buttonImportKSG = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PatientList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // PatientList
            // 
            this.PatientList.AllowUserToAddRows = false;
            this.PatientList.AllowUserToDeleteRows = false;
            this.PatientList.AllowUserToResizeRows = false;
            this.PatientList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PatientList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.PatientList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PatientList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.PatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.Column4,
            this.FIOColumn,
            this.DeliveryDateColumne,
            this.ReleaseDateColumn,
            this.OperationDateColumn,
            this.NosologyColumn,
            this.DiagnozColumn,
            this.OperationCntColumn,
            this.Column1,
            this.Column2,
            this.Column3,
            this.EmptyColumn});
            this.PatientList.Location = new System.Drawing.Point(3, 6);
            this.PatientList.MultiSelect = false;
            this.PatientList.Name = "PatientList";
            this.PatientList.ReadOnly = true;
            this.PatientList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PatientList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.PatientList.RowTemplate.Height = 17;
            this.PatientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PatientList.Size = new System.Drawing.Size(983, 339);
            this.PatientList.StandardTab = true;
            this.PatientList.TabIndex = 10;
            this.PatientList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatientList_CellClick);
            this.PatientList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PatientList_CellMouseDoubleClick);
            this.PatientList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.PatientList_ColumnWidthChanged);
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "id";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.Visible = false;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "N";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column4.Width = 20;
            // 
            // FIOColumn
            // 
            this.FIOColumn.HeaderText = "ФИО пациента";
            this.FIOColumn.Name = "FIOColumn";
            this.FIOColumn.ReadOnly = true;
            this.FIOColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.FIOColumn.Width = 170;
            // 
            // DeliveryDateColumne
            // 
            this.DeliveryDateColumne.HeaderText = "Дата поступления";
            this.DeliveryDateColumne.Name = "DeliveryDateColumne";
            this.DeliveryDateColumne.ReadOnly = true;
            this.DeliveryDateColumne.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // ReleaseDateColumn
            // 
            this.ReleaseDateColumn.HeaderText = "Дата выписки";
            this.ReleaseDateColumn.Name = "ReleaseDateColumn";
            this.ReleaseDateColumn.ReadOnly = true;
            this.ReleaseDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // OperationDateColumn
            // 
            this.OperationDateColumn.HeaderText = "Дата последней операции";
            this.OperationDateColumn.Name = "OperationDateColumn";
            this.OperationDateColumn.ReadOnly = true;
            this.OperationDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // NosologyColumn
            // 
            this.NosologyColumn.HeaderText = "Нозология";
            this.NosologyColumn.Name = "NosologyColumn";
            this.NosologyColumn.ReadOnly = true;
            this.NosologyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NosologyColumn.Width = 75;
            // 
            // DiagnozColumn
            // 
            this.DiagnozColumn.HeaderText = "Диагноз";
            this.DiagnozColumn.Name = "DiagnozColumn";
            this.DiagnozColumn.ReadOnly = true;
            this.DiagnozColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DiagnozColumn.Width = 180;
            // 
            // OperationCntColumn
            // 
            this.OperationCntColumn.HeaderText = "Кол-во операций";
            this.OperationCntColumn.Name = "OperationCntColumn";
            this.OperationCntColumn.ReadOnly = true;
            this.OperationCntColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OperationCntColumn.Width = 60;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Д/Н";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 35;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "к/д";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 30;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Лечащий врач";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column3.Width = 80;
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
            // textBoxFilterFIO
            // 
            this.textBoxFilterFIO.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterFIO.Location = new System.Drawing.Point(38, 351);
            this.textBoxFilterFIO.Name = "textBoxFilterFIO";
            this.textBoxFilterFIO.Size = new System.Drawing.Size(164, 20);
            this.textBoxFilterFIO.TabIndex = 55;
            this.textBoxFilterFIO.Visible = false;
            this.textBoxFilterFIO.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilterNosology
            // 
            this.textBoxFilterNosology.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterNosology.Location = new System.Drawing.Point(510, 351);
            this.textBoxFilterNosology.Name = "textBoxFilterNosology";
            this.textBoxFilterNosology.Size = new System.Drawing.Size(61, 20);
            this.textBoxFilterNosology.TabIndex = 57;
            this.textBoxFilterNosology.Visible = false;
            this.textBoxFilterNosology.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilterDiagnose
            // 
            this.textBoxFilterDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterDiagnose.Location = new System.Drawing.Point(577, 351);
            this.textBoxFilterDiagnose.Name = "textBoxFilterDiagnose";
            this.textBoxFilterDiagnose.Size = new System.Drawing.Size(176, 20);
            this.textBoxFilterDiagnose.TabIndex = 59;
            this.textBoxFilterDiagnose.Visible = false;
            this.textBoxFilterDiagnose.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 750;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBoxFilterOperationCnt
            // 
            this.textBoxFilterOperationCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterOperationCnt.Location = new System.Drawing.Point(759, 377);
            this.textBoxFilterOperationCnt.Name = "textBoxFilterOperationCnt";
            this.textBoxFilterOperationCnt.Size = new System.Drawing.Size(51, 20);
            this.textBoxFilterOperationCnt.TabIndex = 61;
            this.textBoxFilterOperationCnt.Visible = false;
            this.textBoxFilterOperationCnt.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // dateTimePickerFilterDeliveryDateStart
            // 
            this.dateTimePickerFilterDeliveryDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterDeliveryDateStart.Checked = false;
            this.dateTimePickerFilterDeliveryDateStart.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterDeliveryDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterDeliveryDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterDeliveryDateStart.Location = new System.Drawing.Point(208, 351);
            this.dateTimePickerFilterDeliveryDateStart.Name = "dateTimePickerFilterDeliveryDateStart";
            this.dateTimePickerFilterDeliveryDateStart.ShowCheckBox = true;
            this.dateTimePickerFilterDeliveryDateStart.Size = new System.Drawing.Size(96, 20);
            this.dateTimePickerFilterDeliveryDateStart.TabIndex = 62;
            this.dateTimePickerFilterDeliveryDateStart.Visible = false;
            this.dateTimePickerFilterDeliveryDateStart.MouseLeave += new System.EventHandler(this.dateTimePickerFilterDeliveryDateStart_MouseLeave);
            this.dateTimePickerFilterDeliveryDateStart.ValueChanged += new System.EventHandler(this.dateTimePickerFilter_ValueChanged);
            this.dateTimePickerFilterDeliveryDateStart.MouseEnter += new System.EventHandler(this.dateTimePickerFilterDeliveryDateStart_MouseEnter);
            // 
            // dateTimePickerFilterDeliveryDateEnd
            // 
            this.dateTimePickerFilterDeliveryDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterDeliveryDateEnd.Checked = false;
            this.dateTimePickerFilterDeliveryDateEnd.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterDeliveryDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterDeliveryDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterDeliveryDateEnd.Location = new System.Drawing.Point(208, 377);
            this.dateTimePickerFilterDeliveryDateEnd.Name = "dateTimePickerFilterDeliveryDateEnd";
            this.dateTimePickerFilterDeliveryDateEnd.ShowCheckBox = true;
            this.dateTimePickerFilterDeliveryDateEnd.Size = new System.Drawing.Size(96, 20);
            this.dateTimePickerFilterDeliveryDateEnd.TabIndex = 63;
            this.dateTimePickerFilterDeliveryDateEnd.Visible = false;
            this.dateTimePickerFilterDeliveryDateEnd.MouseLeave += new System.EventHandler(this.dateTimePickerFilterDeliveryDateEnd_MouseLeave);
            this.dateTimePickerFilterDeliveryDateEnd.ValueChanged += new System.EventHandler(this.dateTimePickerFilter_ValueChanged);
            this.dateTimePickerFilterDeliveryDateEnd.MouseEnter += new System.EventHandler(this.dateTimePickerFilterDeliveryDateEnd_MouseEnter);
            // 
            // comboBoxFilterOperationCntMode
            // 
            this.comboBoxFilterOperationCntMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFilterOperationCntMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterOperationCntMode.FormattingEnabled = true;
            this.comboBoxFilterOperationCntMode.Items.AddRange(new object[] {
            "",
            "=",
            "<",
            ">"});
            this.comboBoxFilterOperationCntMode.Location = new System.Drawing.Point(759, 350);
            this.comboBoxFilterOperationCntMode.Name = "comboBoxFilterOperationCntMode";
            this.comboBoxFilterOperationCntMode.Size = new System.Drawing.Size(51, 21);
            this.comboBoxFilterOperationCntMode.TabIndex = 64;
            this.comboBoxFilterOperationCntMode.Visible = false;
            this.comboBoxFilterOperationCntMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterOperationCntMode_SelectedIndexChanged);
            // 
            // textBoxFilterDN
            // 
            this.textBoxFilterDN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterDN.Location = new System.Drawing.Point(815, 351);
            this.textBoxFilterDN.Name = "textBoxFilterDN";
            this.textBoxFilterDN.Size = new System.Drawing.Size(30, 20);
            this.textBoxFilterDN.TabIndex = 69;
            this.textBoxFilterDN.Visible = false;
            this.textBoxFilterDN.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // comboBoxFilterKDMode
            // 
            this.comboBoxFilterKDMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFilterKDMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterKDMode.FormattingEnabled = true;
            this.comboBoxFilterKDMode.Items.AddRange(new object[] {
            "",
            "=",
            "<",
            ">"});
            this.comboBoxFilterKDMode.Location = new System.Drawing.Point(851, 350);
            this.comboBoxFilterKDMode.Name = "comboBoxFilterKDMode";
            this.comboBoxFilterKDMode.Size = new System.Drawing.Size(30, 21);
            this.comboBoxFilterKDMode.TabIndex = 72;
            this.comboBoxFilterKDMode.Visible = false;
            this.comboBoxFilterKDMode.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilterOperationCntMode_SelectedIndexChanged);
            // 
            // textBoxFilterKD
            // 
            this.textBoxFilterKD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterKD.Location = new System.Drawing.Point(851, 377);
            this.textBoxFilterKD.Name = "textBoxFilterKD";
            this.textBoxFilterKD.Size = new System.Drawing.Size(30, 20);
            this.textBoxFilterKD.TabIndex = 71;
            this.textBoxFilterKD.Visible = false;
            this.textBoxFilterKD.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilterDoctor
            // 
            this.textBoxFilterDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterDoctor.Location = new System.Drawing.Point(887, 350);
            this.textBoxFilterDoctor.Name = "textBoxFilterDoctor";
            this.textBoxFilterDoctor.Size = new System.Drawing.Size(99, 20);
            this.textBoxFilterDoctor.TabIndex = 73;
            this.textBoxFilterDoctor.Visible = false;
            this.textBoxFilterDoctor.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // buttonHideFilter
            // 
            this.buttonHideFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonHideFilter.BackgroundImage = global::SurgeryHelper.Properties.Resources.FilterHide;
            this.buttonHideFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonHideFilter.FlatAppearance.BorderSize = 0;
            this.buttonHideFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHideFilter.Location = new System.Drawing.Point(410, 407);
            this.buttonHideFilter.Name = "buttonHideFilter";
            this.buttonHideFilter.Size = new System.Drawing.Size(40, 40);
            this.buttonHideFilter.TabIndex = 74;
            this.buttonHideFilter.TabStop = false;
            this.buttonHideFilter.UseVisualStyleBackColor = true;
            this.buttonHideFilter.MouseLeave += new System.EventHandler(this.buttonHideFilter_MouseLeave);
            this.buttonHideFilter.Click += new System.EventHandler(this.buttonHideFilter_Click);
            this.buttonHideFilter.MouseEnter += new System.EventHandler(this.buttonHideFilter_MouseEnter);
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxInfo.Image = global::SurgeryHelper.Properties.Resources.information;
            this.pictureBoxInfo.Location = new System.Drawing.Point(3, 377);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxInfo.TabIndex = 68;
            this.pictureBoxInfo.TabStop = false;
            this.pictureBoxInfo.Visible = false;
            this.pictureBoxInfo.MouseLeave += new System.EventHandler(this.pictureBoxInfo_MouseLeave);
            this.pictureBoxInfo.MouseEnter += new System.EventHandler(this.pictureBoxInfo_MouseEnter);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_add;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(792, 407);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(40, 40);
            this.buttonAdd.TabIndex = 54;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.BackgroundImage = global::SurgeryHelper.Properties.Resources.patients;
            this.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopy.FlatAppearance.BorderSize = 0;
            this.buttonCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopy.Location = new System.Drawing.Point(930, 407);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(40, 40);
            this.buttonCopy.TabIndex = 52;
            this.buttonCopy.TabStop = false;
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.MouseLeave += new System.EventHandler(this.buttonCopy_MouseLeave);
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            this.buttonCopy.MouseEnter += new System.EventHandler(this.buttonCopy_MouseEnter);
            // 
            // buttonView
            // 
            this.buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonView.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_view;
            this.buttonView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonView.FlatAppearance.BorderSize = 0;
            this.buttonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonView.Location = new System.Drawing.Point(884, 407);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(40, 40);
            this.buttonView.TabIndex = 50;
            this.buttonView.TabStop = false;
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.MouseLeave += new System.EventHandler(this.buttonView_MouseLeave);
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            this.buttonView.MouseEnter += new System.EventHandler(this.buttonView_MouseEnter);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(838, 407);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 47;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            // 
            // buttonExportToExcel
            // 
            this.buttonExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExportToExcel.BackgroundImage = global::SurgeryHelper.Properties.Resources.ExportToExcel;
            this.buttonExportToExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExportToExcel.FlatAppearance.BorderSize = 0;
            this.buttonExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportToExcel.Location = new System.Drawing.Point(67, 407);
            this.buttonExportToExcel.Name = "buttonExportToExcel";
            this.buttonExportToExcel.Size = new System.Drawing.Size(40, 40);
            this.buttonExportToExcel.TabIndex = 66;
            this.buttonExportToExcel.TabStop = false;
            this.buttonExportToExcel.UseVisualStyleBackColor = true;
            this.buttonExportToExcel.MouseLeave += new System.EventHandler(this.buttonExportToExcel_MouseLeave);
            this.buttonExportToExcel.Click += new System.EventHandler(this.buttonExportToExcel_Click);
            this.buttonExportToExcel.MouseEnter += new System.EventHandler(this.buttonExportToExcel_MouseEnter);
            // 
            // buttonFilterRemove
            // 
            this.buttonFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFilterRemove.BackgroundImage = global::SurgeryHelper.Properties.Resources.filterDelete;
            this.buttonFilterRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilterRemove.FlatAppearance.BorderSize = 0;
            this.buttonFilterRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilterRemove.Location = new System.Drawing.Point(452, 407);
            this.buttonFilterRemove.Name = "buttonFilterRemove";
            this.buttonFilterRemove.Size = new System.Drawing.Size(40, 40);
            this.buttonFilterRemove.TabIndex = 54;
            this.buttonFilterRemove.TabStop = false;
            this.buttonFilterRemove.UseVisualStyleBackColor = true;
            this.buttonFilterRemove.Visible = false;
            this.buttonFilterRemove.MouseLeave += new System.EventHandler(this.buttonFilterRemove_MouseLeave);
            this.buttonFilterRemove.Click += new System.EventHandler(this.buttonFilterRemove_Click);
            this.buttonFilterRemove.MouseEnter += new System.EventHandler(this.buttonFilterRemove_MouseEnter);
            // 
            // buttonShowFilter
            // 
            this.buttonShowFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonShowFilter.BackgroundImage = global::SurgeryHelper.Properties.Resources.FilterShow;
            this.buttonShowFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonShowFilter.FlatAppearance.BorderSize = 0;
            this.buttonShowFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowFilter.Location = new System.Drawing.Point(410, 407);
            this.buttonShowFilter.Name = "buttonShowFilter";
            this.buttonShowFilter.Size = new System.Drawing.Size(40, 40);
            this.buttonShowFilter.TabIndex = 75;
            this.buttonShowFilter.TabStop = false;
            this.buttonShowFilter.UseVisualStyleBackColor = true;
            this.buttonShowFilter.MouseLeave += new System.EventHandler(this.buttonShowFilter_MouseLeave);
            this.buttonShowFilter.Click += new System.EventHandler(this.buttonShowFilter_Click);
            this.buttonShowFilter.MouseEnter += new System.EventHandler(this.buttonShowFilter_MouseEnter);
            // 
            // dateTimePickerFilterReleaseDateEnd
            // 
            this.dateTimePickerFilterReleaseDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterReleaseDateEnd.Checked = false;
            this.dateTimePickerFilterReleaseDateEnd.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterReleaseDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterReleaseDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterReleaseDateEnd.Location = new System.Drawing.Point(310, 377);
            this.dateTimePickerFilterReleaseDateEnd.Name = "dateTimePickerFilterReleaseDateEnd";
            this.dateTimePickerFilterReleaseDateEnd.ShowCheckBox = true;
            this.dateTimePickerFilterReleaseDateEnd.Size = new System.Drawing.Size(92, 20);
            this.dateTimePickerFilterReleaseDateEnd.TabIndex = 77;
            this.dateTimePickerFilterReleaseDateEnd.Visible = false;
            this.dateTimePickerFilterReleaseDateEnd.MouseLeave += new System.EventHandler(this.dateTimePickerFilterReleaseDateEnd_MouseLeave);
            this.dateTimePickerFilterReleaseDateEnd.ValueChanged += new System.EventHandler(this.dateTimePickerFilter_ValueChanged);
            this.dateTimePickerFilterReleaseDateEnd.MouseEnter += new System.EventHandler(this.dateTimePickerFilterReleaseDateEnd_MouseEnter);
            // 
            // dateTimePickerFilterReleaseDateStart
            // 
            this.dateTimePickerFilterReleaseDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterReleaseDateStart.Checked = false;
            this.dateTimePickerFilterReleaseDateStart.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterReleaseDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterReleaseDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterReleaseDateStart.Location = new System.Drawing.Point(310, 351);
            this.dateTimePickerFilterReleaseDateStart.Name = "dateTimePickerFilterReleaseDateStart";
            this.dateTimePickerFilterReleaseDateStart.ShowCheckBox = true;
            this.dateTimePickerFilterReleaseDateStart.Size = new System.Drawing.Size(92, 20);
            this.dateTimePickerFilterReleaseDateStart.TabIndex = 76;
            this.dateTimePickerFilterReleaseDateStart.Visible = false;
            this.dateTimePickerFilterReleaseDateStart.MouseLeave += new System.EventHandler(this.dateTimePickerFilterReleaseDateStart_MouseLeave);
            this.dateTimePickerFilterReleaseDateStart.ValueChanged += new System.EventHandler(this.dateTimePickerFilter_ValueChanged);
            this.dateTimePickerFilterReleaseDateStart.MouseEnter += new System.EventHandler(this.dateTimePickerFilterReleaseDateStart_MouseEnter);
            // 
            // dateTimePickerFilterOperationDateEnd
            // 
            this.dateTimePickerFilterOperationDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterOperationDateEnd.Checked = false;
            this.dateTimePickerFilterOperationDateEnd.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterOperationDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterOperationDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterOperationDateEnd.Location = new System.Drawing.Point(408, 377);
            this.dateTimePickerFilterOperationDateEnd.Name = "dateTimePickerFilterOperationDateEnd";
            this.dateTimePickerFilterOperationDateEnd.ShowCheckBox = true;
            this.dateTimePickerFilterOperationDateEnd.Size = new System.Drawing.Size(96, 20);
            this.dateTimePickerFilterOperationDateEnd.TabIndex = 79;
            this.dateTimePickerFilterOperationDateEnd.Visible = false;
            this.dateTimePickerFilterOperationDateEnd.MouseLeave += new System.EventHandler(this.dateTimePickerFilterOperationDateEnd_MouseLeave);
            this.dateTimePickerFilterOperationDateEnd.ValueChanged += new System.EventHandler(this.dateTimePickerFilter_ValueChanged);
            this.dateTimePickerFilterOperationDateEnd.MouseEnter += new System.EventHandler(this.dateTimePickerFilterOperationDateEnd_MouseEnter);
            // 
            // dateTimePickerFilterOperationDateStart
            // 
            this.dateTimePickerFilterOperationDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterOperationDateStart.Checked = false;
            this.dateTimePickerFilterOperationDateStart.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterOperationDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterOperationDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterOperationDateStart.Location = new System.Drawing.Point(408, 351);
            this.dateTimePickerFilterOperationDateStart.Name = "dateTimePickerFilterOperationDateStart";
            this.dateTimePickerFilterOperationDateStart.ShowCheckBox = true;
            this.dateTimePickerFilterOperationDateStart.Size = new System.Drawing.Size(96, 20);
            this.dateTimePickerFilterOperationDateStart.TabIndex = 78;
            this.dateTimePickerFilterOperationDateStart.Visible = false;
            this.dateTimePickerFilterOperationDateStart.MouseLeave += new System.EventHandler(this.dateTimePickerFilterOperationDateStart_MouseLeave);
            this.dateTimePickerFilterOperationDateStart.ValueChanged += new System.EventHandler(this.dateTimePickerFilter_ValueChanged);
            this.dateTimePickerFilterOperationDateStart.MouseEnter += new System.EventHandler(this.dateTimePickerFilterOperationDateStart_MouseEnter);
            // 
            // buttonImportKSG
            // 
            this.buttonImportKSG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonImportKSG.BackgroundImage = global::SurgeryHelper.Properties.Resources.importKSG;
            this.buttonImportKSG.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonImportKSG.FlatAppearance.BorderSize = 0;
            this.buttonImportKSG.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonImportKSG.Location = new System.Drawing.Point(126, 407);
            this.buttonImportKSG.Name = "buttonImportKSG";
            this.buttonImportKSG.Size = new System.Drawing.Size(40, 40);
            this.buttonImportKSG.TabIndex = 80;
            this.buttonImportKSG.TabStop = false;
            this.buttonImportKSG.UseVisualStyleBackColor = true;
            this.buttonImportKSG.MouseLeave += new System.EventHandler(this.buttonImportKSG_MouseLeave);
            this.buttonImportKSG.Click += new System.EventHandler(this.buttonImportKSG_Click);
            this.buttonImportKSG.MouseEnter += new System.EventHandler(this.buttonImportKSG_MouseEnter);
            // 
            // PatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 452);
            this.Controls.Add(this.buttonImportKSG);
            this.Controls.Add(this.dateTimePickerFilterOperationDateEnd);
            this.Controls.Add(this.dateTimePickerFilterOperationDateStart);
            this.Controls.Add(this.dateTimePickerFilterReleaseDateEnd);
            this.Controls.Add(this.dateTimePickerFilterReleaseDateStart);
            this.Controls.Add(this.buttonHideFilter);
            this.Controls.Add(this.buttonShowFilter);
            this.Controls.Add(this.textBoxFilterDoctor);
            this.Controls.Add(this.comboBoxFilterKDMode);
            this.Controls.Add(this.textBoxFilterKD);
            this.Controls.Add(this.textBoxFilterDN);
            this.Controls.Add(this.pictureBoxInfo);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonExportToExcel);
            this.Controls.Add(this.buttonFilterRemove);
            this.Controls.Add(this.comboBoxFilterOperationCntMode);
            this.Controls.Add(this.dateTimePickerFilterDeliveryDateEnd);
            this.Controls.Add(this.dateTimePickerFilterDeliveryDateStart);
            this.Controls.Add(this.textBoxFilterOperationCnt);
            this.Controls.Add(this.textBoxFilterDiagnose);
            this.Controls.Add(this.textBoxFilterNosology);
            this.Controls.Add(this.textBoxFilterFIO);
            this.Controls.Add(this.PatientList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(440, 230);
            this.Name = "PatientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Список пациентов";
            this.Load += new System.EventHandler(this.PatientForm_Load);
            this.SizeChanged += new System.EventHandler(this.PatientForm_SizeChanged);
            this.Shown += new System.EventHandler(this.PatientForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientForm_FormClosing);
            this.LocationChanged += new System.EventHandler(this.PatientForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView PatientList;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxFilterFIO;
        private System.Windows.Forms.TextBox textBoxFilterNosology;
        private System.Windows.Forms.TextBox textBoxFilterDiagnose;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBoxFilterOperationCnt;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterDeliveryDateStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterDeliveryDateEnd;
        private System.Windows.Forms.ComboBox comboBoxFilterOperationCntMode;
        private System.Windows.Forms.Button buttonFilterRemove;
        private System.Windows.Forms.Button buttonExportToExcel;
        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.TextBox textBoxFilterDN;
        private System.Windows.Forms.ComboBox comboBoxFilterKDMode;
        private System.Windows.Forms.TextBox textBoxFilterKD;
        private System.Windows.Forms.TextBox textBoxFilterDoctor;
        private System.Windows.Forms.Button buttonHideFilter;
        private System.Windows.Forms.Button buttonShowFilter;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterReleaseDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterReleaseDateStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterOperationDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterOperationDateStart;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIOColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryDateColumne;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReleaseDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NosologyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagnozColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationCntColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
        private System.Windows.Forms.Button buttonImportKSG;

    }
}