namespace SurgeryHelper.Forms
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientListForm));
            this.PatientList = new System.Windows.Forms.DataGridView();
            this.idColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NumberColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIOColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NosologyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiagnoseColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VisitDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeliveryDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReleaseDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KDColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HospitalizationCntColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ConsultayionCntColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationCntColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationTypesColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxFilterFIO = new System.Windows.Forms.TextBox();
            this.textBoxFilterNosology = new System.Windows.Forms.TextBox();
            this.timerShowPatients = new System.Windows.Forms.Timer(this.components);
            this.textBoxFilterDiagnose = new System.Windows.Forms.TextBox();
            this.comboBoxFilterAgeMode = new System.Windows.Forms.ComboBox();
            this.dateTimePickerFilterDeliveryDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterDeliveryDateStart = new System.Windows.Forms.DateTimePicker();
            this.textBoxFilterAge = new System.Windows.Forms.TextBox();
            this.textBoxFilterKD = new System.Windows.Forms.TextBox();
            this.comboBoxFilterHospitalizationCntMode = new System.Windows.Forms.ComboBox();
            this.textBoxFilterHospitalizationCnt = new System.Windows.Forms.TextBox();
            this.comboBoxFilterVisitCntMode = new System.Windows.Forms.ComboBox();
            this.textBoxFilterVisitCnt = new System.Windows.Forms.TextBox();
            this.comboBoxFilterOperationCntMode = new System.Windows.Forms.ComboBox();
            this.textBoxFilterOperationCnt = new System.Windows.Forms.TextBox();
            this.comboBoxFilterKDMode = new System.Windows.Forms.ComboBox();
            this.buttonHideFilter = new System.Windows.Forms.Button();
            this.buttonShowFilter = new System.Windows.Forms.Button();
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.buttonExportToExcel = new System.Windows.Forms.Button();
            this.buttonFilterRemove = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonView = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.dateTimePickerFilterVisitDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterVisitDateStart = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterReleaseDateEnd = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerFilterReleaseDateStart = new System.Windows.Forms.DateTimePicker();
            this.textBoxFilterOperationType = new System.Windows.Forms.TextBox();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatientList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PatientList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatientList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idColumn,
            this.NumberColumn,
            this.FIOColumn,
            this.AgeColumn,
            this.NosologyColumn,
            this.DiagnoseColumn,
            this.VisitDateColumn,
            this.DeliveryDateColumn,
            this.ReleaseDateColumn,
            this.KDColumn,
            this.HospitalizationCntColumn,
            this.ConsultayionCntColumn,
            this.OperationCntColumn,
            this.OperationTypesColumn,
            this.EmptyColumn});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PatientList.DefaultCellStyle = dataGridViewCellStyle2;
            this.PatientList.Location = new System.Drawing.Point(3, 6);
            this.PatientList.MultiSelect = false;
            this.PatientList.Name = "PatientList";
            this.PatientList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatientList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.PatientList.RowHeadersVisible = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.PatientList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.PatientList.RowTemplate.Height = 17;
            this.PatientList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.PatientList.Size = new System.Drawing.Size(1003, 264);
            this.PatientList.StandardTab = true;
            this.PatientList.TabIndex = 0;
            this.PatientList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatientList_CellClick);
            this.PatientList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.PatientList_CellMouseDoubleClick);
            this.PatientList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.PatientList_ColumnWidthChanged);
            // 
            // idColumn
            // 
            this.idColumn.HeaderText = "id";
            this.idColumn.Name = "idColumn";
            this.idColumn.ReadOnly = true;
            this.idColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.idColumn.Visible = false;
            this.idColumn.Width = 30;
            // 
            // NumberColumn
            // 
            this.NumberColumn.HeaderText = "N";
            this.NumberColumn.MinimumWidth = 25;
            this.NumberColumn.Name = "NumberColumn";
            this.NumberColumn.ReadOnly = true;
            this.NumberColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NumberColumn.Width = 25;
            // 
            // FIOColumn
            // 
            this.FIOColumn.HeaderText = "ФИО пациента";
            this.FIOColumn.Name = "FIOColumn";
            this.FIOColumn.ReadOnly = true;
            this.FIOColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.FIOColumn.Width = 150;
            // 
            // AgeColumn
            // 
            this.AgeColumn.HeaderText = "Воз раст";
            this.AgeColumn.Name = "AgeColumn";
            this.AgeColumn.ReadOnly = true;
            this.AgeColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.AgeColumn.Width = 35;
            // 
            // NosologyColumn
            // 
            this.NosologyColumn.HeaderText = "Нозология";
            this.NosologyColumn.Name = "NosologyColumn";
            this.NosologyColumn.ReadOnly = true;
            this.NosologyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NosologyColumn.Width = 70;
            // 
            // DiagnoseColumn
            // 
            this.DiagnoseColumn.HeaderText = "Диагноз";
            this.DiagnoseColumn.Name = "DiagnoseColumn";
            this.DiagnoseColumn.ReadOnly = true;
            this.DiagnoseColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DiagnoseColumn.Width = 125;
            // 
            // VisitDateColumn
            // 
            this.VisitDateColumn.HeaderText = "Дата консультации";
            this.VisitDateColumn.Name = "VisitDateColumn";
            this.VisitDateColumn.ReadOnly = true;
            this.VisitDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.VisitDateColumn.Width = 95;
            // 
            // DeliveryDateColumn
            // 
            this.DeliveryDateColumn.HeaderText = "Дата поступления";
            this.DeliveryDateColumn.Name = "DeliveryDateColumn";
            this.DeliveryDateColumn.ReadOnly = true;
            this.DeliveryDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            this.DeliveryDateColumn.Width = 95;
            // 
            // ReleaseDateColumn
            // 
            this.ReleaseDateColumn.HeaderText = "Дата выписки";
            this.ReleaseDateColumn.Name = "ReleaseDateColumn";
            this.ReleaseDateColumn.ReadOnly = true;
            this.ReleaseDateColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ReleaseDateColumn.Width = 95;
            // 
            // KDColumn
            // 
            this.KDColumn.HeaderText = "к/д";
            this.KDColumn.Name = "KDColumn";
            this.KDColumn.ReadOnly = true;
            this.KDColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KDColumn.Width = 30;
            // 
            // HospitalizationCntColumn
            // 
            this.HospitalizationCntColumn.HeaderText = "Кол-во госпит.";
            this.HospitalizationCntColumn.Name = "HospitalizationCntColumn";
            this.HospitalizationCntColumn.ReadOnly = true;
            this.HospitalizationCntColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HospitalizationCntColumn.Width = 45;
            // 
            // ConsultayionCntColumn
            // 
            this.ConsultayionCntColumn.HeaderText = "Кол-во конс.";
            this.ConsultayionCntColumn.Name = "ConsultayionCntColumn";
            this.ConsultayionCntColumn.ReadOnly = true;
            this.ConsultayionCntColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ConsultayionCntColumn.Width = 45;
            // 
            // OperationCntColumn
            // 
            this.OperationCntColumn.HeaderText = "Кол-во опер.";
            this.OperationCntColumn.Name = "OperationCntColumn";
            this.OperationCntColumn.ReadOnly = true;
            this.OperationCntColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OperationCntColumn.Width = 45;
            // 
            // OperationTypesColumn
            // 
            this.OperationTypesColumn.HeaderText = "Типы операций";
            this.OperationTypesColumn.Name = "OperationTypesColumn";
            this.OperationTypesColumn.ReadOnly = true;
            this.OperationTypesColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OperationTypesColumn.Width = 125;
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
            this.textBoxFilterFIO.Location = new System.Drawing.Point(32, 275);
            this.textBoxFilterFIO.Name = "textBoxFilterFIO";
            this.textBoxFilterFIO.Size = new System.Drawing.Size(152, 20);
            this.textBoxFilterFIO.TabIndex = 1;
            this.textBoxFilterFIO.Visible = false;
            this.textBoxFilterFIO.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxFilterNosology
            // 
            this.textBoxFilterNosology.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterNosology.Location = new System.Drawing.Point(231, 275);
            this.textBoxFilterNosology.Name = "textBoxFilterNosology";
            this.textBoxFilterNosology.Size = new System.Drawing.Size(63, 20);
            this.textBoxFilterNosology.TabIndex = 6;
            this.textBoxFilterNosology.Visible = false;
            this.textBoxFilterNosology.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // timerShowPatients
            // 
            this.timerShowPatients.Interval = 750;
            this.timerShowPatients.Tick += new System.EventHandler(this.timerShowPatients_Tick);
            // 
            // textBoxFilterDiagnose
            // 
            this.textBoxFilterDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterDiagnose.Location = new System.Drawing.Point(300, 275);
            this.textBoxFilterDiagnose.Name = "textBoxFilterDiagnose";
            this.textBoxFilterDiagnose.Size = new System.Drawing.Size(125, 20);
            this.textBoxFilterDiagnose.TabIndex = 8;
            this.textBoxFilterDiagnose.Visible = false;
            this.textBoxFilterDiagnose.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // comboBoxFilterAgeMode
            // 
            this.comboBoxFilterAgeMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFilterAgeMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterAgeMode.FormattingEnabled = true;
            this.comboBoxFilterAgeMode.Items.AddRange(new object[] {
            "",
            "=",
            "<",
            ">"});
            this.comboBoxFilterAgeMode.Location = new System.Drawing.Point(190, 275);
            this.comboBoxFilterAgeMode.Name = "comboBoxFilterAgeMode";
            this.comboBoxFilterAgeMode.Size = new System.Drawing.Size(35, 21);
            this.comboBoxFilterAgeMode.TabIndex = 2;
            this.comboBoxFilterAgeMode.Visible = false;
            this.comboBoxFilterAgeMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // dateTimePickerFilterDeliveryDateEnd
            // 
            this.dateTimePickerFilterDeliveryDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterDeliveryDateEnd.Checked = false;
            this.dateTimePickerFilterDeliveryDateEnd.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterDeliveryDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterDeliveryDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterDeliveryDateEnd.Location = new System.Drawing.Point(526, 303);
            this.dateTimePickerFilterDeliveryDateEnd.Name = "dateTimePickerFilterDeliveryDateEnd";
            this.dateTimePickerFilterDeliveryDateEnd.ShowCheckBox = true;
            this.dateTimePickerFilterDeliveryDateEnd.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerFilterDeliveryDateEnd.TabIndex = 12;
            this.dateTimePickerFilterDeliveryDateEnd.Visible = false;
            this.dateTimePickerFilterDeliveryDateEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // dateTimePickerFilterDeliveryDateStart
            // 
            this.dateTimePickerFilterDeliveryDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterDeliveryDateStart.Checked = false;
            this.dateTimePickerFilterDeliveryDateStart.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterDeliveryDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterDeliveryDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterDeliveryDateStart.Location = new System.Drawing.Point(526, 277);
            this.dateTimePickerFilterDeliveryDateStart.Name = "dateTimePickerFilterDeliveryDateStart";
            this.dateTimePickerFilterDeliveryDateStart.ShowCheckBox = true;
            this.dateTimePickerFilterDeliveryDateStart.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerFilterDeliveryDateStart.TabIndex = 10;
            this.dateTimePickerFilterDeliveryDateStart.Visible = false;
            this.dateTimePickerFilterDeliveryDateStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // textBoxFilterAge
            // 
            this.textBoxFilterAge.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterAge.Location = new System.Drawing.Point(190, 301);
            this.textBoxFilterAge.Name = "textBoxFilterAge";
            this.textBoxFilterAge.Size = new System.Drawing.Size(35, 20);
            this.textBoxFilterAge.TabIndex = 4;
            this.textBoxFilterAge.Visible = false;
            this.textBoxFilterAge.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxFilterKD
            // 
            this.textBoxFilterKD.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterKD.Location = new System.Drawing.Point(716, 302);
            this.textBoxFilterKD.Name = "textBoxFilterKD";
            this.textBoxFilterKD.Size = new System.Drawing.Size(25, 20);
            this.textBoxFilterKD.TabIndex = 16;
            this.textBoxFilterKD.Visible = false;
            this.textBoxFilterKD.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // comboBoxFilterHospitalizationCntMode
            // 
            this.comboBoxFilterHospitalizationCntMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFilterHospitalizationCntMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterHospitalizationCntMode.FormattingEnabled = true;
            this.comboBoxFilterHospitalizationCntMode.Items.AddRange(new object[] {
            "",
            "=",
            "<",
            ">"});
            this.comboBoxFilterHospitalizationCntMode.Location = new System.Drawing.Point(748, 275);
            this.comboBoxFilterHospitalizationCntMode.Name = "comboBoxFilterHospitalizationCntMode";
            this.comboBoxFilterHospitalizationCntMode.Size = new System.Drawing.Size(40, 21);
            this.comboBoxFilterHospitalizationCntMode.TabIndex = 22;
            this.comboBoxFilterHospitalizationCntMode.Visible = false;
            this.comboBoxFilterHospitalizationCntMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // textBoxFilterHospitalizationCnt
            // 
            this.textBoxFilterHospitalizationCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterHospitalizationCnt.Location = new System.Drawing.Point(748, 300);
            this.textBoxFilterHospitalizationCnt.Name = "textBoxFilterHospitalizationCnt";
            this.textBoxFilterHospitalizationCnt.Size = new System.Drawing.Size(40, 20);
            this.textBoxFilterHospitalizationCnt.TabIndex = 24;
            this.textBoxFilterHospitalizationCnt.Visible = false;
            this.textBoxFilterHospitalizationCnt.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // comboBoxFilterVisitCntMode
            // 
            this.comboBoxFilterVisitCntMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFilterVisitCntMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilterVisitCntMode.FormattingEnabled = true;
            this.comboBoxFilterVisitCntMode.Items.AddRange(new object[] {
            "",
            "=",
            "<",
            ">"});
            this.comboBoxFilterVisitCntMode.Location = new System.Drawing.Point(794, 274);
            this.comboBoxFilterVisitCntMode.Name = "comboBoxFilterVisitCntMode";
            this.comboBoxFilterVisitCntMode.Size = new System.Drawing.Size(41, 21);
            this.comboBoxFilterVisitCntMode.TabIndex = 26;
            this.comboBoxFilterVisitCntMode.Visible = false;
            this.comboBoxFilterVisitCntMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // textBoxFilterVisitCnt
            // 
            this.textBoxFilterVisitCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterVisitCnt.Location = new System.Drawing.Point(794, 300);
            this.textBoxFilterVisitCnt.Name = "textBoxFilterVisitCnt";
            this.textBoxFilterVisitCnt.Size = new System.Drawing.Size(41, 20);
            this.textBoxFilterVisitCnt.TabIndex = 28;
            this.textBoxFilterVisitCnt.Visible = false;
            this.textBoxFilterVisitCnt.TextChanged += new System.EventHandler(this.textBox_TextChanged);
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
            this.comboBoxFilterOperationCntMode.Location = new System.Drawing.Point(841, 274);
            this.comboBoxFilterOperationCntMode.Name = "comboBoxFilterOperationCntMode";
            this.comboBoxFilterOperationCntMode.Size = new System.Drawing.Size(40, 21);
            this.comboBoxFilterOperationCntMode.TabIndex = 30;
            this.comboBoxFilterOperationCntMode.Visible = false;
            this.comboBoxFilterOperationCntMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // textBoxFilterOperationCnt
            // 
            this.textBoxFilterOperationCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterOperationCnt.Location = new System.Drawing.Point(841, 300);
            this.textBoxFilterOperationCnt.Name = "textBoxFilterOperationCnt";
            this.textBoxFilterOperationCnt.Size = new System.Drawing.Size(40, 20);
            this.textBoxFilterOperationCnt.TabIndex = 32;
            this.textBoxFilterOperationCnt.Visible = false;
            this.textBoxFilterOperationCnt.TextChanged += new System.EventHandler(this.textBox_TextChanged);
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
            this.comboBoxFilterKDMode.Location = new System.Drawing.Point(716, 275);
            this.comboBoxFilterKDMode.Name = "comboBoxFilterKDMode";
            this.comboBoxFilterKDMode.Size = new System.Drawing.Size(25, 21);
            this.comboBoxFilterKDMode.TabIndex = 15;
            this.comboBoxFilterKDMode.Visible = false;
            this.comboBoxFilterKDMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_SelectedIndexChanged);
            // 
            // buttonHideFilter
            // 
            this.buttonHideFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonHideFilter.BackgroundImage = global::SurgeryHelper.Properties.Resources.FilterHide;
            this.buttonHideFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonHideFilter.FlatAppearance.BorderSize = 0;
            this.buttonHideFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHideFilter.Location = new System.Drawing.Point(398, 329);
            this.buttonHideFilter.Name = "buttonHideFilter";
            this.buttonHideFilter.Size = new System.Drawing.Size(40, 40);
            this.buttonHideFilter.TabIndex = 71;
            this.buttonHideFilter.TabStop = false;
            this.buttonHideFilter.UseVisualStyleBackColor = true;
            this.buttonHideFilter.Click += new System.EventHandler(this.buttonHideFilter_Click);
            this.buttonHideFilter.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonHideFilter.MouseEnter += new System.EventHandler(this.buttonHideFilter_MouseEnter);
            this.buttonHideFilter.MouseLeave += new System.EventHandler(this.buttonHideFilter_MouseLeave);
            // 
            // buttonShowFilter
            // 
            this.buttonShowFilter.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonShowFilter.BackgroundImage = global::SurgeryHelper.Properties.Resources.FilterShow;
            this.buttonShowFilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonShowFilter.FlatAppearance.BorderSize = 0;
            this.buttonShowFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShowFilter.Location = new System.Drawing.Point(398, 329);
            this.buttonShowFilter.Name = "buttonShowFilter";
            this.buttonShowFilter.Size = new System.Drawing.Size(40, 40);
            this.buttonShowFilter.TabIndex = 70;
            this.buttonShowFilter.TabStop = false;
            this.buttonShowFilter.UseVisualStyleBackColor = true;
            this.buttonShowFilter.Click += new System.EventHandler(this.buttonShowFilter_Click);
            this.buttonShowFilter.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonShowFilter.MouseEnter += new System.EventHandler(this.buttonShowFilter_MouseEnter);
            this.buttonShowFilter.MouseLeave += new System.EventHandler(this.buttonShowFilter_MouseLeave);
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBoxInfo.Image = global::SurgeryHelper.Properties.Resources.information;
            this.pictureBoxInfo.Location = new System.Drawing.Point(2, 275);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(24, 24);
            this.pictureBoxInfo.TabIndex = 68;
            this.pictureBoxInfo.TabStop = false;
            this.pictureBoxInfo.MouseEnter += new System.EventHandler(this.pictureBoxInfo_MouseEnter);
            this.pictureBoxInfo.MouseLeave += new System.EventHandler(this.pictureBoxInfo_MouseLeave);
            // 
            // buttonExportToExcel
            // 
            this.buttonExportToExcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonExportToExcel.BackgroundImage = global::SurgeryHelper.Properties.Resources.ExportToExcel;
            this.buttonExportToExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExportToExcel.FlatAppearance.BorderSize = 0;
            this.buttonExportToExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportToExcel.Location = new System.Drawing.Point(39, 329);
            this.buttonExportToExcel.Name = "buttonExportToExcel";
            this.buttonExportToExcel.Size = new System.Drawing.Size(40, 40);
            this.buttonExportToExcel.TabIndex = 66;
            this.buttonExportToExcel.TabStop = false;
            this.buttonExportToExcel.UseVisualStyleBackColor = true;
            this.buttonExportToExcel.Click += new System.EventHandler(this.buttonExportToExcel_Click);
            this.buttonExportToExcel.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonExportToExcel.MouseEnter += new System.EventHandler(this.buttonExportToExcel_MouseEnter);
            this.buttonExportToExcel.MouseLeave += new System.EventHandler(this.buttonExportToExcel_MouseLeave);
            // 
            // buttonFilterRemove
            // 
            this.buttonFilterRemove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFilterRemove.BackgroundImage = global::SurgeryHelper.Properties.Resources.filterDelete;
            this.buttonFilterRemove.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonFilterRemove.FlatAppearance.BorderSize = 0;
            this.buttonFilterRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonFilterRemove.Location = new System.Drawing.Point(444, 329);
            this.buttonFilterRemove.Name = "buttonFilterRemove";
            this.buttonFilterRemove.Size = new System.Drawing.Size(40, 40);
            this.buttonFilterRemove.TabIndex = 65;
            this.buttonFilterRemove.TabStop = false;
            this.buttonFilterRemove.UseVisualStyleBackColor = true;
            this.buttonFilterRemove.Visible = false;
            this.buttonFilterRemove.Click += new System.EventHandler(this.buttonFilterRemove_Click);
            this.buttonFilterRemove.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonFilterRemove.MouseEnter += new System.EventHandler(this.buttonFilterRemove_MouseEnter);
            this.buttonFilterRemove.MouseLeave += new System.EventHandler(this.buttonFilterRemove_MouseLeave);
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCopy.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_copy;
            this.buttonCopy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCopy.FlatAppearance.BorderSize = 0;
            this.buttonCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCopy.Location = new System.Drawing.Point(942, 329);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(40, 40);
            this.buttonCopy.TabIndex = 69;
            this.buttonCopy.TabStop = false;
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            this.buttonCopy.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonCopy.MouseEnter += new System.EventHandler(this.buttonCopy_MouseEnter);
            this.buttonCopy.MouseLeave += new System.EventHandler(this.buttonCopy_MouseLeave);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_add;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(804, 329);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(40, 40);
            this.buttonAdd.TabIndex = 54;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            // 
            // buttonView
            // 
            this.buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonView.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_view;
            this.buttonView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonView.FlatAppearance.BorderSize = 0;
            this.buttonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonView.Location = new System.Drawing.Point(896, 329);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(40, 40);
            this.buttonView.TabIndex = 50;
            this.buttonView.TabStop = false;
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            this.buttonView.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonView.MouseEnter += new System.EventHandler(this.buttonView_MouseEnter);
            this.buttonView.MouseLeave += new System.EventHandler(this.buttonView_MouseLeave);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDelete.BackgroundImage = global::SurgeryHelper.Properties.Resources.patient_delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(850, 329);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 47;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            // 
            // dateTimePickerFilterVisitDateEnd
            // 
            this.dateTimePickerFilterVisitDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterVisitDateEnd.Checked = false;
            this.dateTimePickerFilterVisitDateEnd.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterVisitDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterVisitDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterVisitDateEnd.Location = new System.Drawing.Point(431, 302);
            this.dateTimePickerFilterVisitDateEnd.Name = "dateTimePickerFilterVisitDateEnd";
            this.dateTimePickerFilterVisitDateEnd.ShowCheckBox = true;
            this.dateTimePickerFilterVisitDateEnd.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerFilterVisitDateEnd.TabIndex = 73;
            this.dateTimePickerFilterVisitDateEnd.Visible = false;
            this.dateTimePickerFilterVisitDateEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // dateTimePickerFilterVisitDateStart
            // 
            this.dateTimePickerFilterVisitDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterVisitDateStart.Checked = false;
            this.dateTimePickerFilterVisitDateStart.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterVisitDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterVisitDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterVisitDateStart.Location = new System.Drawing.Point(431, 276);
            this.dateTimePickerFilterVisitDateStart.Name = "dateTimePickerFilterVisitDateStart";
            this.dateTimePickerFilterVisitDateStart.ShowCheckBox = true;
            this.dateTimePickerFilterVisitDateStart.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerFilterVisitDateStart.TabIndex = 72;
            this.dateTimePickerFilterVisitDateStart.Visible = false;
            this.dateTimePickerFilterVisitDateStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // dateTimePickerFilterReleaseDateEnd
            // 
            this.dateTimePickerFilterReleaseDateEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterReleaseDateEnd.Checked = false;
            this.dateTimePickerFilterReleaseDateEnd.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterReleaseDateEnd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterReleaseDateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterReleaseDateEnd.Location = new System.Drawing.Point(621, 303);
            this.dateTimePickerFilterReleaseDateEnd.Name = "dateTimePickerFilterReleaseDateEnd";
            this.dateTimePickerFilterReleaseDateEnd.ShowCheckBox = true;
            this.dateTimePickerFilterReleaseDateEnd.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerFilterReleaseDateEnd.TabIndex = 75;
            this.dateTimePickerFilterReleaseDateEnd.Visible = false;
            this.dateTimePickerFilterReleaseDateEnd.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // dateTimePickerFilterReleaseDateStart
            // 
            this.dateTimePickerFilterReleaseDateStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerFilterReleaseDateStart.Checked = false;
            this.dateTimePickerFilterReleaseDateStart.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerFilterReleaseDateStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerFilterReleaseDateStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerFilterReleaseDateStart.Location = new System.Drawing.Point(621, 277);
            this.dateTimePickerFilterReleaseDateStart.Name = "dateTimePickerFilterReleaseDateStart";
            this.dateTimePickerFilterReleaseDateStart.ShowCheckBox = true;
            this.dateTimePickerFilterReleaseDateStart.Size = new System.Drawing.Size(89, 20);
            this.dateTimePickerFilterReleaseDateStart.TabIndex = 74;
            this.dateTimePickerFilterReleaseDateStart.Visible = false;
            this.dateTimePickerFilterReleaseDateStart.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // textBoxFilterOperationType
            // 
            this.textBoxFilterOperationType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxFilterOperationType.Location = new System.Drawing.Point(887, 275);
            this.textBoxFilterOperationType.Name = "textBoxFilterOperationType";
            this.textBoxFilterOperationType.Size = new System.Drawing.Size(89, 20);
            this.textBoxFilterOperationType.TabIndex = 76;
            this.textBoxFilterOperationType.Visible = false;
            this.textBoxFilterOperationType.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // PatientListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 373);
            this.Controls.Add(this.textBoxFilterOperationType);
            this.Controls.Add(this.dateTimePickerFilterReleaseDateEnd);
            this.Controls.Add(this.dateTimePickerFilterReleaseDateStart);
            this.Controls.Add(this.dateTimePickerFilterVisitDateEnd);
            this.Controls.Add(this.dateTimePickerFilterVisitDateStart);
            this.Controls.Add(this.buttonHideFilter);
            this.Controls.Add(this.PatientList);
            this.Controls.Add(this.buttonShowFilter);
            this.Controls.Add(this.comboBoxFilterKDMode);
            this.Controls.Add(this.comboBoxFilterOperationCntMode);
            this.Controls.Add(this.textBoxFilterOperationCnt);
            this.Controls.Add(this.comboBoxFilterVisitCntMode);
            this.Controls.Add(this.textBoxFilterVisitCnt);
            this.Controls.Add(this.comboBoxFilterHospitalizationCntMode);
            this.Controls.Add(this.textBoxFilterHospitalizationCnt);
            this.Controls.Add(this.textBoxFilterKD);
            this.Controls.Add(this.pictureBoxInfo);
            this.Controls.Add(this.buttonExportToExcel);
            this.Controls.Add(this.buttonFilterRemove);
            this.Controls.Add(this.comboBoxFilterAgeMode);
            this.Controls.Add(this.dateTimePickerFilterDeliveryDateEnd);
            this.Controls.Add(this.dateTimePickerFilterDeliveryDateStart);
            this.Controls.Add(this.textBoxFilterAge);
            this.Controls.Add(this.textBoxFilterDiagnose);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.textBoxFilterNosology);
            this.Controls.Add(this.textBoxFilterFIO);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(10, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(415, 250);
            this.Name = "PatientListForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Список пациентов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatientForm_FormClosing);
            this.Load += new System.EventHandler(this.PatientForm_Load);
            this.Shown += new System.EventHandler(this.PatientForm_Shown);
            this.LocationChanged += new System.EventHandler(this.PatientForm_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.PatientForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.PatientList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView PatientList;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textBoxFilterFIO;
        private System.Windows.Forms.TextBox textBoxFilterNosology;
        private System.Windows.Forms.Timer timerShowPatients;
        private System.Windows.Forms.Button buttonFilterRemove;
        private System.Windows.Forms.Button buttonExportToExcel;
        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.Button buttonCopy;
        private System.Windows.Forms.TextBox textBoxFilterDiagnose;
        private System.Windows.Forms.ComboBox comboBoxFilterAgeMode;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterDeliveryDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterDeliveryDateStart;
        private System.Windows.Forms.TextBox textBoxFilterAge;
        private System.Windows.Forms.TextBox textBoxFilterKD;
        private System.Windows.Forms.ComboBox comboBoxFilterHospitalizationCntMode;
        private System.Windows.Forms.TextBox textBoxFilterHospitalizationCnt;
        private System.Windows.Forms.ComboBox comboBoxFilterVisitCntMode;
        private System.Windows.Forms.TextBox textBoxFilterVisitCnt;
        private System.Windows.Forms.ComboBox comboBoxFilterOperationCntMode;
        private System.Windows.Forms.TextBox textBoxFilterOperationCnt;
        private System.Windows.Forms.ComboBox comboBoxFilterKDMode;
        private System.Windows.Forms.Button buttonShowFilter;
        private System.Windows.Forms.Button buttonHideFilter;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterVisitDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterVisitDateStart;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterReleaseDateEnd;
        private System.Windows.Forms.DateTimePicker dateTimePickerFilterReleaseDateStart;
        private System.Windows.Forms.TextBox textBoxFilterOperationType;
        private System.Windows.Forms.DataGridViewTextBoxColumn idColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumberColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIOColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NosologyColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiagnoseColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn VisitDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeliveryDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ReleaseDateColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn KDColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn HospitalizationCntColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ConsultayionCntColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationCntColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationTypesColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;

    }
}