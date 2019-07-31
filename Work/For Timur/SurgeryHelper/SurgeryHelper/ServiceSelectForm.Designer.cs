namespace SurgeryHelper
{
    partial class ServiceSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceSelectForm));
            this.ServiceCodesList = new System.Windows.Forms.DataGridView();
            this.ColumnServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnServiceCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnKsgCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnKsgDecoding = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxFilterServiceName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxDoNotShowAll = new System.Windows.Forms.CheckBox();
            this.textBoxFilterServiceCode = new System.Windows.Forms.TextBox();
            this.textBoxFilterKsgCode = new System.Windows.Forms.TextBox();
            this.textBoxFilterKsgDecoding = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ServiceCodesList)).BeginInit();
            this.SuspendLayout();
            // 
            // ServiceCodesList
            // 
            this.ServiceCodesList.AllowUserToAddRows = false;
            this.ServiceCodesList.AllowUserToDeleteRows = false;
            this.ServiceCodesList.AllowUserToResizeRows = false;
            this.ServiceCodesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServiceCodesList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ServiceCodesList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ServiceCodesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ServiceCodesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ServiceCodesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnServiceName,
            this.ColumnServiceCode,
            this.ColumnKsgCode,
            this.ColumnKsgDecoding});
            this.ServiceCodesList.Location = new System.Drawing.Point(12, 12);
            this.ServiceCodesList.MultiSelect = false;
            this.ServiceCodesList.Name = "ServiceCodesList";
            this.ServiceCodesList.ReadOnly = true;
            this.ServiceCodesList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ServiceCodesList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.ServiceCodesList.RowTemplate.Height = 17;
            this.ServiceCodesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ServiceCodesList.Size = new System.Drawing.Size(728, 284);
            this.ServiceCodesList.StandardTab = true;
            this.ServiceCodesList.TabIndex = 11;
            this.ServiceCodesList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ServiceCodesList_CellMouseDoubleClick);
            this.ServiceCodesList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.ServiceCodesList_ColumnWidthChanged);
            // 
            // ColumnServiceName
            // 
            this.ColumnServiceName.HeaderText = "Название услуги";
            this.ColumnServiceName.Name = "ColumnServiceName";
            this.ColumnServiceName.ReadOnly = true;
            this.ColumnServiceName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnServiceName.Width = 266;
            // 
            // ColumnServiceCode
            // 
            this.ColumnServiceCode.HeaderText = "Код услуги";
            this.ColumnServiceCode.Name = "ColumnServiceCode";
            this.ColumnServiceCode.ReadOnly = true;
            this.ColumnServiceCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnServiceCode.Width = 70;
            // 
            // ColumnKsgCode
            // 
            this.ColumnKsgCode.HeaderText = "Код КСГ";
            this.ColumnKsgCode.Name = "ColumnKsgCode";
            this.ColumnKsgCode.ReadOnly = true;
            this.ColumnKsgCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnKsgCode.Width = 60;
            // 
            // ColumnKsgDecoding
            // 
            this.ColumnKsgDecoding.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ColumnKsgDecoding.HeaderText = "Расшифровка КСГ";
            this.ColumnKsgDecoding.Name = "ColumnKsgDecoding";
            this.ColumnKsgDecoding.ReadOnly = true;
            this.ColumnKsgDecoding.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(615, 330);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 58;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
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
            this.buttonClose.Location = new System.Drawing.Point(679, 331);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 57;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            // 
            // textBoxFilterServiceName
            // 
            this.textBoxFilterServiceName.Location = new System.Drawing.Point(12, 302);
            this.textBoxFilterServiceName.Name = "textBoxFilterServiceName";
            this.textBoxFilterServiceName.Size = new System.Drawing.Size(273, 20);
            this.textBoxFilterServiceName.TabIndex = 60;
            this.textBoxFilterServiceName.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 750;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBoxDoNotShowAll
            // 
            this.checkBoxDoNotShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxDoNotShowAll.AutoSize = true;
            this.checkBoxDoNotShowAll.Checked = true;
            this.checkBoxDoNotShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxDoNotShowAll.Location = new System.Drawing.Point(12, 343);
            this.checkBoxDoNotShowAll.Name = "checkBoxDoNotShowAll";
            this.checkBoxDoNotShowAll.Size = new System.Drawing.Size(124, 17);
            this.checkBoxDoNotShowAll.TabIndex = 61;
            this.checkBoxDoNotShowAll.Text = "Не отображать всё";
            this.checkBoxDoNotShowAll.UseVisualStyleBackColor = true;
            this.checkBoxDoNotShowAll.CheckedChanged += new System.EventHandler(this.checkBoxDoNotShowAll_CheckedChanged);
            // 
            // textBoxFilterServiceCode
            // 
            this.textBoxFilterServiceCode.Location = new System.Drawing.Point(291, 302);
            this.textBoxFilterServiceCode.Name = "textBoxFilterServiceCode";
            this.textBoxFilterServiceCode.Size = new System.Drawing.Size(66, 20);
            this.textBoxFilterServiceCode.TabIndex = 62;
            this.textBoxFilterServiceCode.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilterKsgCode
            // 
            this.textBoxFilterKsgCode.Location = new System.Drawing.Point(363, 302);
            this.textBoxFilterKsgCode.Name = "textBoxFilterKsgCode";
            this.textBoxFilterKsgCode.Size = new System.Drawing.Size(53, 20);
            this.textBoxFilterKsgCode.TabIndex = 63;
            this.textBoxFilterKsgCode.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilterKsgDecoding
            // 
            this.textBoxFilterKsgDecoding.Location = new System.Drawing.Point(422, 302);
            this.textBoxFilterKsgDecoding.Name = "textBoxFilterKsgDecoding";
            this.textBoxFilterKsgDecoding.Size = new System.Drawing.Size(318, 20);
            this.textBoxFilterKsgDecoding.TabIndex = 64;
            this.textBoxFilterKsgDecoding.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // ServiceSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(752, 382);
            this.Controls.Add(this.textBoxFilterKsgDecoding);
            this.Controls.Add(this.textBoxFilterKsgCode);
            this.Controls.Add(this.textBoxFilterServiceCode);
            this.Controls.Add(this.checkBoxDoNotShowAll);
            this.Controls.Add(this.textBoxFilterServiceName);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.ServiceCodesList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(615, 280);
            this.Name = "ServiceSelectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор услуги";
            this.Load += new System.EventHandler(this.ServiceSelectForm_Load);
            this.Shown += new System.EventHandler(this.ServiceSelectForm_Shown);
            this.LocationChanged += new System.EventHandler(this.ServiceSelectForm_LocationChanged);
            this.SizeChanged += new System.EventHandler(this.ServiceSelectForm_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.ServiceCodesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ServiceCodesList;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxFilterServiceName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBoxDoNotShowAll;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnServiceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKsgCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnKsgDecoding;
        private System.Windows.Forms.TextBox textBoxFilterServiceCode;
        private System.Windows.Forms.TextBox textBoxFilterKsgCode;
        private System.Windows.Forms.TextBox textBoxFilterKsgDecoding;
    }
}