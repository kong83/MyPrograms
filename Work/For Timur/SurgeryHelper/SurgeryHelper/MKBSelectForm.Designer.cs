namespace SurgeryHelper
{
    partial class MKBSelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MKBSelectForm));
            this.MKBCodesList = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FIOColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxFilterCode = new System.Windows.Forms.TextBox();
            this.textBoxFilterName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBoxDoNotShowAll = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.MKBCodesList)).BeginInit();
            this.SuspendLayout();
            // 
            // MKBCodesList
            // 
            this.MKBCodesList.AllowUserToAddRows = false;
            this.MKBCodesList.AllowUserToDeleteRows = false;
            this.MKBCodesList.AllowUserToResizeRows = false;
            this.MKBCodesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MKBCodesList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.MKBCodesList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MKBCodesList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.MKBCodesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MKBCodesList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.FIOColumn});
            this.MKBCodesList.Location = new System.Drawing.Point(12, 12);
            this.MKBCodesList.MultiSelect = false;
            this.MKBCodesList.Name = "MKBCodesList";
            this.MKBCodesList.ReadOnly = true;
            this.MKBCodesList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.MKBCodesList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.MKBCodesList.RowTemplate.Height = 17;
            this.MKBCodesList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.MKBCodesList.Size = new System.Drawing.Size(530, 284);
            this.MKBCodesList.StandardTab = true;
            this.MKBCodesList.TabIndex = 11;
            this.MKBCodesList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.MKBCodesList_CellMouseDoubleClick);
            this.MKBCodesList.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.MKBCodesList_ColumnWidthChanged);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Код";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FIOColumn
            // 
            this.FIOColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.FIOColumn.HeaderText = "Название";
            this.FIOColumn.Name = "FIOColumn";
            this.FIOColumn.ReadOnly = true;
            this.FIOColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(417, 330);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 58;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(481, 331);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 57;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // textBoxFilterCode
            // 
            this.textBoxFilterCode.Location = new System.Drawing.Point(12, 305);
            this.textBoxFilterCode.Name = "textBoxFilterCode";
            this.textBoxFilterCode.Size = new System.Drawing.Size(94, 20);
            this.textBoxFilterCode.TabIndex = 59;
            this.textBoxFilterCode.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
            // 
            // textBoxFilterName
            // 
            this.textBoxFilterName.Location = new System.Drawing.Point(112, 305);
            this.textBoxFilterName.Name = "textBoxFilterName";
            this.textBoxFilterName.Size = new System.Drawing.Size(430, 20);
            this.textBoxFilterName.TabIndex = 60;
            this.textBoxFilterName.TextChanged += new System.EventHandler(this.textBoxFilter_TextChanged);
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
            // MKBSelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 382);
            this.Controls.Add(this.checkBoxDoNotShowAll);
            this.Controls.Add(this.textBoxFilterName);
            this.Controls.Add(this.textBoxFilterCode);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.MKBCodesList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(360, 280);
            this.Name = "MKBSelectForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Выбор кода МКБ";
            this.Load += new System.EventHandler(this.MKBSelectForm_Load);
            this.SizeChanged += new System.EventHandler(this.MKBSelectForm_SizeChanged);
            this.Shown += new System.EventHandler(this.MKBSelectForm_Shown);
            this.LocationChanged += new System.EventHandler(this.MKBSelectForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.MKBCodesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView MKBCodesList;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn FIOColumn;
        private System.Windows.Forms.TextBox textBoxFilterCode;
        private System.Windows.Forms.TextBox textBoxFilterName;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBoxDoNotShowAll;
    }
}