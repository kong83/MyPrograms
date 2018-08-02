namespace SurgeryHelper.Forms
{
    partial class HospitalizationViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HospitalizationViewForm));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.linkLabelDiagnose = new System.Windows.Forms.LinkLabel();
            this.textBoxDiagnose = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonGenerateFolder = new System.Windows.Forms.Button();
            this.textBoxFotoFolderName = new System.Windows.Forms.TextBox();
            this.linkLabelFoto = new System.Windows.Forms.LinkLabel();
            this.comboBoxDoctorInChargeOfTheCase = new System.Windows.Forms.ComboBox();
            this.linkLabelDoctorInCase = new System.Windows.Forms.LinkLabel();
            this.dateTimePickerDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxCaseHistory = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.dateTimePickerReleaseDate = new System.Windows.Forms.DateTimePicker();
            this.OperationList = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameParth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonProtocol = new System.Windows.Forms.Button();
            this.buttonView = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonCards = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonDocuments = new System.Windows.Forms.Button();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperationList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.linkLabelDiagnose);
            this.groupBox4.Controls.Add(this.textBoxDiagnose);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(476, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(267, 347);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            // 
            // linkLabelDiagnose
            // 
            this.linkLabelDiagnose.AutoSize = true;
            this.linkLabelDiagnose.BackColor = System.Drawing.SystemColors.Control;
            this.linkLabelDiagnose.Location = new System.Drawing.Point(9, 1);
            this.linkLabelDiagnose.Name = "linkLabelDiagnose";
            this.linkLabelDiagnose.Size = new System.Drawing.Size(67, 13);
            this.linkLabelDiagnose.TabIndex = 6;
            this.linkLabelDiagnose.TabStop = true;
            this.linkLabelDiagnose.Text = "* Диагноз";
            this.linkLabelDiagnose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDiagnose_LinkClicked);
            this.linkLabelDiagnose.MouseEnter += new System.EventHandler(this.linkLabelDiagnose_MouseEnter);
            this.linkLabelDiagnose.MouseLeave += new System.EventHandler(this.linkLabelDiagnose_MouseLeave);
            // 
            // textBoxDiagnose
            // 
            this.textBoxDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDiagnose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDiagnose.Location = new System.Drawing.Point(3, 16);
            this.textBoxDiagnose.MaxLength = 20000000;
            this.textBoxDiagnose.Multiline = true;
            this.textBoxDiagnose.Name = "textBoxDiagnose";
            this.textBoxDiagnose.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDiagnose.Size = new System.Drawing.Size(261, 323);
            this.textBoxDiagnose.TabIndex = 5;
            this.textBoxDiagnose.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxDiagnose.Enter += new System.EventHandler(this.box_Focused);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonGenerateFolder);
            this.groupBox3.Controls.Add(this.textBoxFotoFolderName);
            this.groupBox3.Controls.Add(this.linkLabelFoto);
            this.groupBox3.Controls.Add(this.comboBoxDoctorInChargeOfTheCase);
            this.groupBox3.Controls.Add(this.linkLabelDoctorInCase);
            this.groupBox3.Controls.Add(this.dateTimePickerDeliveryDate);
            this.groupBox3.Controls.Add(this.textBoxCaseHistory);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.dateTimePickerReleaseDate);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(456, 103);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Учётные данные";
            // 
            // buttonGenerateFolder
            // 
            this.buttonGenerateFolder.BackgroundImage = global::SurgeryHelper.Properties.Resources.generate16;
            this.buttonGenerateFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonGenerateFolder.FlatAppearance.BorderSize = 0;
            this.buttonGenerateFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGenerateFolder.Location = new System.Drawing.Point(96, 72);
            this.buttonGenerateFolder.Name = "buttonGenerateFolder";
            this.buttonGenerateFolder.Size = new System.Drawing.Size(20, 20);
            this.buttonGenerateFolder.TabIndex = 102;
            this.buttonGenerateFolder.TabStop = false;
            this.buttonGenerateFolder.UseVisualStyleBackColor = true;
            this.buttonGenerateFolder.Click += new System.EventHandler(this.buttonGenerateFolder_Click);
            this.buttonGenerateFolder.MouseEnter += new System.EventHandler(this.buttonGenerateFolder_MouseEnter);
            this.buttonGenerateFolder.MouseLeave += new System.EventHandler(this.buttonGenerateFolder_MouseLeave);
            // 
            // textBoxFotoFolderName
            // 
            this.textBoxFotoFolderName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxFotoFolderName.Location = new System.Drawing.Point(123, 72);
            this.textBoxFotoFolderName.Name = "textBoxFotoFolderName";
            this.textBoxFotoFolderName.Size = new System.Drawing.Size(113, 20);
            this.textBoxFotoFolderName.TabIndex = 31;
            this.textBoxFotoFolderName.TextChanged += new System.EventHandler(this.textBoxFotoFolderName_TextChanged);
            this.textBoxFotoFolderName.Enter += new System.EventHandler(this.box_Focused);
            // 
            // linkLabelFoto
            // 
            this.linkLabelFoto.AutoSize = true;
            this.linkLabelFoto.Enabled = false;
            this.linkLabelFoto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelFoto.Location = new System.Drawing.Point(6, 76);
            this.linkLabelFoto.Name = "linkLabelFoto";
            this.linkLabelFoto.Size = new System.Drawing.Size(88, 13);
            this.linkLabelFoto.TabIndex = 30;
            this.linkLabelFoto.TabStop = true;
            this.linkLabelFoto.Text = "Папка для фото";
            this.linkLabelFoto.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelFoto_LinkClicked);
            this.linkLabelFoto.MouseEnter += new System.EventHandler(this.linkLabelFoto_MouseEnter);
            this.linkLabelFoto.MouseLeave += new System.EventHandler(this.linkLabelFoto_MouseLeave);
            // 
            // comboBoxDoctorInChargeOfTheCase
            // 
            this.comboBoxDoctorInChargeOfTheCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDoctorInChargeOfTheCase.FormattingEnabled = true;
            this.comboBoxDoctorInChargeOfTheCase.Location = new System.Drawing.Point(123, 43);
            this.comboBoxDoctorInChargeOfTheCase.MaxDropDownItems = 20;
            this.comboBoxDoctorInChargeOfTheCase.Name = "comboBoxDoctorInChargeOfTheCase";
            this.comboBoxDoctorInChargeOfTheCase.Size = new System.Drawing.Size(113, 21);
            this.comboBoxDoctorInChargeOfTheCase.TabIndex = 29;
            this.comboBoxDoctorInChargeOfTheCase.TextChanged += new System.EventHandler(this.comboBox_TextChenged);
            this.comboBoxDoctorInChargeOfTheCase.Enter += new System.EventHandler(this.box_Focused);
            // 
            // linkLabelDoctorInCase
            // 
            this.linkLabelDoctorInCase.AutoSize = true;
            this.linkLabelDoctorInCase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelDoctorInCase.Location = new System.Drawing.Point(6, 46);
            this.linkLabelDoctorInCase.Name = "linkLabelDoctorInCase";
            this.linkLabelDoctorInCase.Size = new System.Drawing.Size(86, 13);
            this.linkLabelDoctorInCase.TabIndex = 28;
            this.linkLabelDoctorInCase.TabStop = true;
            this.linkLabelDoctorInCase.Text = "* Лечащий врач";
            this.linkLabelDoctorInCase.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDoctorInCase_LinkClicked);
            this.linkLabelDoctorInCase.MouseEnter += new System.EventHandler(this.linkLabelDoctorInCase_MouseEnter);
            this.linkLabelDoctorInCase.MouseLeave += new System.EventHandler(this.linkLabelDoctorInCase_MouseLeave);
            // 
            // dateTimePickerDeliveryDate
            // 
            this.dateTimePickerDeliveryDate.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePickerDeliveryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDeliveryDate.Location = new System.Drawing.Point(324, 21);
            this.dateTimePickerDeliveryDate.Name = "dateTimePickerDeliveryDate";
            this.dateTimePickerDeliveryDate.Size = new System.Drawing.Size(123, 20);
            this.dateTimePickerDeliveryDate.TabIndex = 25;
            this.dateTimePickerDeliveryDate.ValueChanged += new System.EventHandler(this.dateTime_ValueChanged);
            this.dateTimePickerDeliveryDate.Enter += new System.EventHandler(this.box_Focused);
            // 
            // textBoxCaseHistory
            // 
            this.textBoxCaseHistory.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxCaseHistory.Location = new System.Drawing.Point(123, 16);
            this.textBoxCaseHistory.Name = "textBoxCaseHistory";
            this.textBoxCaseHistory.Size = new System.Drawing.Size(113, 20);
            this.textBoxCaseHistory.TabIndex = 20;
            this.textBoxCaseHistory.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxCaseHistory.Enter += new System.EventHandler(this.box_Focused);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(6, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(114, 13);
            this.label12.TabIndex = 89;
            this.label12.Text = "* № истории болезни";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(248, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(51, 26);
            this.label11.TabIndex = 88;
            this.label11.Text = "Дата\r\nвыписки";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(248, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 26);
            this.label10.TabIndex = 87;
            this.label10.Text = "* Дата\r\n  поступления";
            // 
            // dateTimePickerReleaseDate
            // 
            this.dateTimePickerReleaseDate.Checked = false;
            this.dateTimePickerReleaseDate.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerReleaseDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerReleaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerReleaseDate.Location = new System.Drawing.Point(324, 66);
            this.dateTimePickerReleaseDate.Name = "dateTimePickerReleaseDate";
            this.dateTimePickerReleaseDate.ShowCheckBox = true;
            this.dateTimePickerReleaseDate.Size = new System.Drawing.Size(123, 20);
            this.dateTimePickerReleaseDate.TabIndex = 27;
            this.dateTimePickerReleaseDate.ValueChanged += new System.EventHandler(this.dateTime_ValueChanged);
            this.dateTimePickerReleaseDate.Enter += new System.EventHandler(this.box_Focused);
            // 
            // OperationList
            // 
            this.OperationList.AllowUserToAddRows = false;
            this.OperationList.AllowUserToDeleteRows = false;
            this.OperationList.AllowUserToResizeRows = false;
            this.OperationList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.OperationList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.OperationList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.OperationList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.OperationList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OperationList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.Login,
            this.nameParth,
            this.EmptyColumn});
            this.OperationList.Location = new System.Drawing.Point(12, 122);
            this.OperationList.MultiSelect = false;
            this.OperationList.Name = "OperationList";
            this.OperationList.ReadOnly = true;
            this.OperationList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.OperationList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.OperationList.RowTemplate.Height = 17;
            this.OperationList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.OperationList.Size = new System.Drawing.Size(456, 193);
            this.OperationList.StandardTab = true;
            this.OperationList.TabIndex = 91;
            this.OperationList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.OperationList_CellMouseDoubleClick);
            this.OperationList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.OperationList_ColumnWidthChanged);
            this.OperationList.Enter += new System.EventHandler(this.box_Focused);
            // 
            // id
            // 
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // Login
            // 
            this.Login.HeaderText = "Дата операции";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            this.Login.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Login.Width = 110;
            // 
            // nameParth
            // 
            this.nameParth.FillWeight = 300F;
            this.nameParth.HeaderText = "Название операции";
            this.nameParth.Name = "nameParth";
            this.nameParth.ReadOnly = true;
            this.nameParth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameParth.Width = 310;
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
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackgroundImage = global::SurgeryHelper.Properties.Resources.operation_add;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(284, 321);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(40, 40);
            this.buttonAdd.TabIndex = 95;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            // 
            // buttonProtocol
            // 
            this.buttonProtocol.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonProtocol.BackgroundImage = global::SurgeryHelper.Properties.Resources.DOCUMENT;
            this.buttonProtocol.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonProtocol.FlatAppearance.BorderSize = 0;
            this.buttonProtocol.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonProtocol.Location = new System.Drawing.Point(12, 321);
            this.buttonProtocol.Name = "buttonProtocol";
            this.buttonProtocol.Size = new System.Drawing.Size(40, 40);
            this.buttonProtocol.TabIndex = 94;
            this.buttonProtocol.TabStop = false;
            this.buttonProtocol.UseVisualStyleBackColor = true;
            this.buttonProtocol.Click += new System.EventHandler(this.buttonProtocol_Click);
            this.buttonProtocol.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonProtocol.MouseEnter += new System.EventHandler(this.buttonProtocol_MouseEnter);
            this.buttonProtocol.MouseLeave += new System.EventHandler(this.buttonProtocol_MouseLeave);
            // 
            // buttonView
            // 
            this.buttonView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonView.BackgroundImage = global::SurgeryHelper.Properties.Resources.operation_view;
            this.buttonView.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonView.FlatAppearance.BorderSize = 0;
            this.buttonView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonView.Location = new System.Drawing.Point(386, 321);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(40, 40);
            this.buttonView.TabIndex = 93;
            this.buttonView.TabStop = false;
            this.buttonView.UseVisualStyleBackColor = true;
            this.buttonView.Click += new System.EventHandler(this.buttonView_Click);
            this.buttonView.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonView.MouseEnter += new System.EventHandler(this.buttonView_MouseEnter);
            this.buttonView.MouseLeave += new System.EventHandler(this.buttonView_MouseLeave);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.BackgroundImage = global::SurgeryHelper.Properties.Resources.operation_delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(335, 321);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 92;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            // 
            // buttonCards
            // 
            this.buttonCards.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCards.BackgroundImage = global::SurgeryHelper.Properties.Resources.two_cards;
            this.buttonCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCards.FlatAppearance.BorderSize = 0;
            this.buttonCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCards.Location = new System.Drawing.Point(749, 73);
            this.buttonCards.Name = "buttonCards";
            this.buttonCards.Size = new System.Drawing.Size(40, 40);
            this.buttonCards.TabIndex = 90;
            this.buttonCards.TabStop = false;
            this.buttonCards.UseVisualStyleBackColor = true;
            this.buttonCards.Click += new System.EventHandler(this.buttonCards_Click);
            this.buttonCards.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonCards.MouseEnter += new System.EventHandler(this.buttonCards_MouseEnter);
            this.buttonCards.MouseLeave += new System.EventHandler(this.buttonCards_MouseLeave);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(746, 250);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 79;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(746, 311);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 78;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            // 
            // buttonDocuments
            // 
            this.buttonDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonDocuments.BackgroundImage = global::SurgeryHelper.Properties.Resources.documents;
            this.buttonDocuments.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDocuments.FlatAppearance.BorderSize = 0;
            this.buttonDocuments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDocuments.Location = new System.Drawing.Point(749, 12);
            this.buttonDocuments.Name = "buttonDocuments";
            this.buttonDocuments.Size = new System.Drawing.Size(40, 40);
            this.buttonDocuments.TabIndex = 77;
            this.buttonDocuments.TabStop = false;
            this.buttonDocuments.UseVisualStyleBackColor = true;
            this.buttonDocuments.Click += new System.EventHandler(this.buttonDocuments_Click);
            this.buttonDocuments.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonDocuments.MouseEnter += new System.EventHandler(this.buttonDocuments_MouseEnter);
            this.buttonDocuments.MouseLeave += new System.EventHandler(this.buttonDocuments_MouseLeave);
            // 
            // HospitalizationViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 363);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonProtocol);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.OperationList);
            this.Controls.Add(this.buttonCards);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDocuments);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.Name = "HospitalizationViewForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HospitalizationViewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HospitalizationViewForm_FormClosing);
            this.Load += new System.EventHandler(this.HospitalizationViewForm_Load);
            this.SizeChanged += new System.EventHandler(this.HospitalizationViewForm_SizeChanged);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperationList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox textBoxDiagnose;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox comboBoxDoctorInChargeOfTheCase;
        private System.Windows.Forms.LinkLabel linkLabelDoctorInCase;
        private System.Windows.Forms.DateTimePicker dateTimePickerDeliveryDate;
        private System.Windows.Forms.TextBox textBoxCaseHistory;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker dateTimePickerReleaseDate;
        private System.Windows.Forms.Button buttonDocuments;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.LinkLabel linkLabelFoto;
        private System.Windows.Forms.Button buttonGenerateFolder;
        private System.Windows.Forms.TextBox textBoxFotoFolderName;
        private System.Windows.Forms.Button buttonCards;
        public System.Windows.Forms.DataGridView OperationList;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonProtocol;
        private System.Windows.Forms.Button buttonView;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameParth;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
        private System.Windows.Forms.LinkLabel linkLabelDiagnose;
    }
}