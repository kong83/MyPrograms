namespace SurgeryHelper
{
    partial class PrescriptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrescriptionForm));
            this.groupBoxConservativeTherapy = new System.Windows.Forms.GroupBox();
            this.buttonTherapyDown = new System.Windows.Forms.Button();
            this.buttonTherapyUp = new System.Windows.Forms.Button();
            this.pictureBoxInfo = new System.Windows.Forms.PictureBox();
            this.buttonDeleteTherapy = new System.Windows.Forms.Button();
            this.buttonAddTherapy = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonDeleteSurvey = new System.Windows.Forms.Button();
            this.buttonAddSurvey = new System.Windows.Forms.Button();
            this.SurveyList = new System.Windows.Forms.DataGridView();
            this.TherapyList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonExportSurveys = new System.Windows.Forms.Button();
            this.buttonExportPrescriptionList = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonSurveyDown = new System.Windows.Forms.Button();
            this.buttonSurveyUp = new System.Windows.Forms.Button();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameParth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBoxConservativeTherapy.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SurveyList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TherapyList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxConservativeTherapy
            // 
            this.groupBoxConservativeTherapy.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxConservativeTherapy.Controls.Add(this.buttonTherapyDown);
            this.groupBoxConservativeTherapy.Controls.Add(this.buttonTherapyUp);
            this.groupBoxConservativeTherapy.Controls.Add(this.pictureBoxInfo);
            this.groupBoxConservativeTherapy.Controls.Add(this.buttonDeleteTherapy);
            this.groupBoxConservativeTherapy.Controls.Add(this.buttonAddTherapy);
            this.groupBoxConservativeTherapy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxConservativeTherapy.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConservativeTherapy.Name = "groupBoxConservativeTherapy";
            this.groupBoxConservativeTherapy.Size = new System.Drawing.Size(709, 195);
            this.groupBoxConservativeTherapy.TabIndex = 1;
            this.groupBoxConservativeTherapy.TabStop = false;
            this.groupBoxConservativeTherapy.Text = "Консервативная терапия";
            // 
            // buttonTherapyDown
            // 
            this.buttonTherapyDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonTherapyDown.BackgroundImage = global::SurgeryHelper.Properties.Resources.down;
            this.buttonTherapyDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTherapyDown.FlatAppearance.BorderSize = 0;
            this.buttonTherapyDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTherapyDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTherapyDown.Location = new System.Drawing.Point(663, 145);
            this.buttonTherapyDown.Name = "buttonTherapyDown";
            this.buttonTherapyDown.Size = new System.Drawing.Size(40, 40);
            this.buttonTherapyDown.TabIndex = 93;
            this.buttonTherapyDown.TabStop = false;
            this.buttonTherapyDown.UseVisualStyleBackColor = true;
            this.buttonTherapyDown.Click += new System.EventHandler(this.buttonTherapyDown_Click);
            this.buttonTherapyDown.MouseEnter += new System.EventHandler(this.buttonTherapyDown_MouseEnter);
            this.buttonTherapyDown.MouseLeave += new System.EventHandler(this.buttonTherapyDown_MouseLeave);
            // 
            // buttonTherapyUp
            // 
            this.buttonTherapyUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonTherapyUp.BackgroundImage = global::SurgeryHelper.Properties.Resources.up;
            this.buttonTherapyUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTherapyUp.FlatAppearance.BorderSize = 0;
            this.buttonTherapyUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTherapyUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTherapyUp.Location = new System.Drawing.Point(663, 53);
            this.buttonTherapyUp.Name = "buttonTherapyUp";
            this.buttonTherapyUp.Size = new System.Drawing.Size(40, 40);
            this.buttonTherapyUp.TabIndex = 92;
            this.buttonTherapyUp.TabStop = false;
            this.buttonTherapyUp.UseVisualStyleBackColor = true;
            this.buttonTherapyUp.Click += new System.EventHandler(this.buttonTherapyUp_Click);
            this.buttonTherapyUp.MouseEnter += new System.EventHandler(this.buttonTherapyUp_MouseEnter);
            this.buttonTherapyUp.MouseLeave += new System.EventHandler(this.buttonTherapyUp_MouseLeave);
            // 
            // pictureBoxInfo
            // 
            this.pictureBoxInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxInfo.BackgroundImage = global::SurgeryHelper.Properties.Resources.information;
            this.pictureBoxInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBoxInfo.Location = new System.Drawing.Point(669, 8);
            this.pictureBoxInfo.Name = "pictureBoxInfo";
            this.pictureBoxInfo.Size = new System.Drawing.Size(25, 25);
            this.pictureBoxInfo.TabIndex = 91;
            this.pictureBoxInfo.TabStop = false;
            this.pictureBoxInfo.MouseEnter += new System.EventHandler(this.pictureBoxInfo_MouseEnter);
            this.pictureBoxInfo.MouseLeave += new System.EventHandler(this.pictureBoxInfo_MouseLeave);
            // 
            // buttonDeleteTherapy
            // 
            this.buttonDeleteTherapy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDeleteTherapy.BackgroundImage = global::SurgeryHelper.Properties.Resources.delete;
            this.buttonDeleteTherapy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDeleteTherapy.FlatAppearance.BorderSize = 0;
            this.buttonDeleteTherapy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteTherapy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDeleteTherapy.Location = new System.Drawing.Point(663, 99);
            this.buttonDeleteTherapy.Name = "buttonDeleteTherapy";
            this.buttonDeleteTherapy.Size = new System.Drawing.Size(40, 40);
            this.buttonDeleteTherapy.TabIndex = 12;
            this.buttonDeleteTherapy.TabStop = false;
            this.buttonDeleteTherapy.UseVisualStyleBackColor = true;
            this.buttonDeleteTherapy.Click += new System.EventHandler(this.buttonDeleteTherapy_Click);
            this.buttonDeleteTherapy.MouseEnter += new System.EventHandler(this.buttonDeleteTherapy_MouseEnter);
            this.buttonDeleteTherapy.MouseLeave += new System.EventHandler(this.buttonDeleteTherapy_MouseLeave);
            // 
            // buttonAddTherapy
            // 
            this.buttonAddTherapy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddTherapy.BackgroundImage = global::SurgeryHelper.Properties.Resources.add;
            this.buttonAddTherapy.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAddTherapy.FlatAppearance.BorderSize = 0;
            this.buttonAddTherapy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddTherapy.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddTherapy.Location = new System.Drawing.Point(13, 15);
            this.buttonAddTherapy.Name = "buttonAddTherapy";
            this.buttonAddTherapy.Size = new System.Drawing.Size(646, 32);
            this.buttonAddTherapy.TabIndex = 7;
            this.buttonAddTherapy.TabStop = false;
            this.buttonAddTherapy.UseVisualStyleBackColor = true;
            this.buttonAddTherapy.Click += new System.EventHandler(this.buttonAddTherapy_Click);
            this.buttonAddTherapy.MouseEnter += new System.EventHandler(this.buttonAddTherapy_MouseEnter);
            this.buttonAddTherapy.MouseLeave += new System.EventHandler(this.buttonAddTherapy_MouseLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonSurveyDown);
            this.groupBox1.Controls.Add(this.buttonSurveyUp);
            this.groupBox1.Controls.Add(this.buttonDeleteSurvey);
            this.groupBox1.Controls.Add(this.buttonAddSurvey);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 213);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(709, 196);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Дополнительные методы обследования";
            // 
            // buttonDeleteSurvey
            // 
            this.buttonDeleteSurvey.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonDeleteSurvey.BackgroundImage = global::SurgeryHelper.Properties.Resources.delete;
            this.buttonDeleteSurvey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDeleteSurvey.FlatAppearance.BorderSize = 0;
            this.buttonDeleteSurvey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDeleteSurvey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDeleteSurvey.Location = new System.Drawing.Point(663, 102);
            this.buttonDeleteSurvey.Name = "buttonDeleteSurvey";
            this.buttonDeleteSurvey.Size = new System.Drawing.Size(40, 40);
            this.buttonDeleteSurvey.TabIndex = 22;
            this.buttonDeleteSurvey.TabStop = false;
            this.buttonDeleteSurvey.UseVisualStyleBackColor = true;
            this.buttonDeleteSurvey.Click += new System.EventHandler(this.buttonDeleteSurvey_Click);
            this.buttonDeleteSurvey.MouseEnter += new System.EventHandler(this.buttonDeleteSurvey_MouseEnter);
            this.buttonDeleteSurvey.MouseLeave += new System.EventHandler(this.buttonDeleteSurvey_MouseLeave);
            // 
            // buttonAddSurvey
            // 
            this.buttonAddSurvey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddSurvey.BackgroundImage = global::SurgeryHelper.Properties.Resources.add;
            this.buttonAddSurvey.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAddSurvey.FlatAppearance.BorderSize = 0;
            this.buttonAddSurvey.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAddSurvey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAddSurvey.Location = new System.Drawing.Point(13, 19);
            this.buttonAddSurvey.Name = "buttonAddSurvey";
            this.buttonAddSurvey.Size = new System.Drawing.Size(646, 32);
            this.buttonAddSurvey.TabIndex = 7;
            this.buttonAddSurvey.TabStop = false;
            this.buttonAddSurvey.UseVisualStyleBackColor = true;
            this.buttonAddSurvey.Click += new System.EventHandler(this.buttonAddSurvey_Click);
            this.buttonAddSurvey.MouseEnter += new System.EventHandler(this.buttonAddSurvey_MouseEnter);
            this.buttonAddSurvey.MouseLeave += new System.EventHandler(this.buttonAddSurvey_MouseLeave);
            // 
            // SurveyList
            // 
            this.SurveyList.AllowUserToAddRows = false;
            this.SurveyList.AllowUserToDeleteRows = false;
            this.SurveyList.AllowUserToResizeRows = false;
            this.SurveyList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SurveyList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.SurveyList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SurveyList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.SurveyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.SurveyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Login,
            this.nameParth,
            this.EmptyColumn});
            this.SurveyList.Location = new System.Drawing.Point(25, 269);
            this.SurveyList.MultiSelect = false;
            this.SurveyList.Name = "SurveyList";
            this.SurveyList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.SurveyList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.SurveyList.RowTemplate.Height = 17;
            this.SurveyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.SurveyList.Size = new System.Drawing.Size(646, 132);
            this.SurveyList.StandardTab = true;
            this.SurveyList.TabIndex = 9;
            this.SurveyList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.SurveyList_ColumnWidthChanged);
            // 
            // TherapyList
            // 
            this.TherapyList.AllowUserToAddRows = false;
            this.TherapyList.AllowUserToDeleteRows = false;
            this.TherapyList.AllowUserToResizeRows = false;
            this.TherapyList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TherapyList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TherapyList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TherapyList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.TherapyList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TherapyList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.ColumnDuration,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.TherapyList.Location = new System.Drawing.Point(25, 65);
            this.TherapyList.MultiSelect = false;
            this.TherapyList.Name = "TherapyList";
            this.TherapyList.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.TherapyList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.TherapyList.RowTemplate.Height = 17;
            this.TherapyList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TherapyList.Size = new System.Drawing.Size(646, 136);
            this.TherapyList.StandardTab = true;
            this.TherapyList.TabIndex = 8;
            this.TherapyList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.TherapyList_ColumnWidthChanged);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 350F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Препарат (название, доза, способ и время введения)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 350;
            // 
            // ColumnDuration
            // 
            this.ColumnDuration.FillWeight = 120F;
            this.ColumnDuration.HeaderText = "Продолжительность";
            this.ColumnDuration.Name = "ColumnDuration";
            this.ColumnDuration.Width = 120;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 150F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Дата назначения";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonExportSurveys
            // 
            this.buttonExportSurveys.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportSurveys.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonExportSurveys.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExportSurveys.FlatAppearance.BorderSize = 0;
            this.buttonExportSurveys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportSurveys.Location = new System.Drawing.Point(727, 213);
            this.buttonExportSurveys.Name = "buttonExportSurveys";
            this.buttonExportSurveys.Size = new System.Drawing.Size(40, 40);
            this.buttonExportSurveys.TabIndex = 20;
            this.buttonExportSurveys.TabStop = false;
            this.buttonExportSurveys.UseVisualStyleBackColor = true;
            this.buttonExportSurveys.Click += new System.EventHandler(this.buttonExportSurveys_Click);
            this.buttonExportSurveys.MouseEnter += new System.EventHandler(this.buttonExportSurveys_MouseEnter);
            this.buttonExportSurveys.MouseLeave += new System.EventHandler(this.buttonExportSurveys_MouseLeave);
            // 
            // buttonExportPrescriptionList
            // 
            this.buttonExportPrescriptionList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportPrescriptionList.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonExportPrescriptionList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonExportPrescriptionList.FlatAppearance.BorderSize = 0;
            this.buttonExportPrescriptionList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExportPrescriptionList.Location = new System.Drawing.Point(727, 16);
            this.buttonExportPrescriptionList.Name = "buttonExportPrescriptionList";
            this.buttonExportPrescriptionList.Size = new System.Drawing.Size(40, 40);
            this.buttonExportPrescriptionList.TabIndex = 10;
            this.buttonExportPrescriptionList.TabStop = false;
            this.buttonExportPrescriptionList.UseVisualStyleBackColor = true;
            this.buttonExportPrescriptionList.Click += new System.EventHandler(this.buttonExportPrescriptionList_Click);
            this.buttonExportPrescriptionList.MouseEnter += new System.EventHandler(this.buttonExportPrescriptionList_MouseEnter);
            this.buttonExportPrescriptionList.MouseLeave += new System.EventHandler(this.buttonExportPrescriptionList_MouseLeave);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Location = new System.Drawing.Point(727, 364);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(40, 40);
            this.buttonOK.TabIndex = 10;
            this.buttonOK.TabStop = false;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            this.buttonOK.MouseEnter += new System.EventHandler(this.buttonOK_MouseEnter);
            this.buttonOK.MouseLeave += new System.EventHandler(this.buttonOK_MouseLeave);
            // 
            // buttonSurveyDown
            // 
            this.buttonSurveyDown.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSurveyDown.BackgroundImage = global::SurgeryHelper.Properties.Resources.down;
            this.buttonSurveyDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSurveyDown.FlatAppearance.BorderSize = 0;
            this.buttonSurveyDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSurveyDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSurveyDown.Location = new System.Drawing.Point(665, 148);
            this.buttonSurveyDown.Name = "buttonSurveyDown";
            this.buttonSurveyDown.Size = new System.Drawing.Size(40, 40);
            this.buttonSurveyDown.TabIndex = 95;
            this.buttonSurveyDown.TabStop = false;
            this.buttonSurveyDown.UseVisualStyleBackColor = true;
            this.buttonSurveyDown.Click += new System.EventHandler(this.buttonSurveyDown_Click);
            this.buttonSurveyDown.MouseEnter += new System.EventHandler(this.buttonSurveyDown_MouseEnter);
            this.buttonSurveyDown.MouseLeave += new System.EventHandler(this.buttonSurveyDown_MouseLeave);
            // 
            // buttonSurveyUp
            // 
            this.buttonSurveyUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.buttonSurveyUp.BackgroundImage = global::SurgeryHelper.Properties.Resources.up;
            this.buttonSurveyUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSurveyUp.FlatAppearance.BorderSize = 0;
            this.buttonSurveyUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSurveyUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonSurveyUp.Location = new System.Drawing.Point(665, 56);
            this.buttonSurveyUp.Name = "buttonSurveyUp";
            this.buttonSurveyUp.Size = new System.Drawing.Size(40, 40);
            this.buttonSurveyUp.TabIndex = 94;
            this.buttonSurveyUp.TabStop = false;
            this.buttonSurveyUp.UseVisualStyleBackColor = true;
            this.buttonSurveyUp.Click += new System.EventHandler(this.buttonSurveyUp_Click);
            this.buttonSurveyUp.MouseEnter += new System.EventHandler(this.buttonSurveyUp_MouseEnter);
            this.buttonSurveyUp.MouseLeave += new System.EventHandler(this.buttonSurveyUp_MouseLeave);
            // 
            // Login
            // 
            this.Login.FillWeight = 480F;
            this.Login.HeaderText = "Метод обследования";
            this.Login.Name = "Login";
            this.Login.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Login.Width = 480;
            // 
            // nameParth
            // 
            this.nameParth.FillWeight = 140F;
            this.nameParth.HeaderText = "Дата назначения";
            this.nameParth.Name = "nameParth";
            this.nameParth.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.nameParth.Width = 140;
            // 
            // EmptyColumn
            // 
            this.EmptyColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.EmptyColumn.HeaderText = "";
            this.EmptyColumn.MinimumWidth = 2;
            this.EmptyColumn.Name = "EmptyColumn";
            this.EmptyColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PrescriptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(770, 416);
            this.Controls.Add(this.buttonExportSurveys);
            this.Controls.Add(this.buttonExportPrescriptionList);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.TherapyList);
            this.Controls.Add(this.SurveyList);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBoxConservativeTherapy);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 455);
            this.Name = "PrescriptionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Назначения";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrescriptionForm_FormClosing);
            this.Load += new System.EventHandler(this.PrescriptionForm_Load);
            this.LocationChanged += new System.EventHandler(this.PrescriptionForm_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.PrescriptionForm_SizeChanged);
            this.groupBoxConservativeTherapy.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxInfo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SurveyList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TherapyList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxConservativeTherapy;
        private System.Windows.Forms.Button buttonAddTherapy;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonAddSurvey;
        private System.Windows.Forms.Button buttonDeleteTherapy;
        public System.Windows.Forms.DataGridView TherapyList;
        private System.Windows.Forms.Button buttonDeleteSurvey;
        public System.Windows.Forms.DataGridView SurveyList;
        private System.Windows.Forms.Button buttonExportPrescriptionList;
        private System.Windows.Forms.Button buttonExportSurveys;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.PictureBox pictureBoxInfo;
        private System.Windows.Forms.Button buttonTherapyDown;
        private System.Windows.Forms.Button buttonTherapyUp;
        private System.Windows.Forms.Button buttonSurveyDown;
        private System.Windows.Forms.Button buttonSurveyUp;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameParth;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
    }
}