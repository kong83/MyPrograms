namespace ServiceWorker
{
    partial class InfoServiceForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.buttonOK = new System.Windows.Forms.Button();
            this.gridServiceInfo = new System.Windows.Forms.DataGridView();
            this.nameParth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridServiceInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonOK.Location = new System.Drawing.Point(191, 369);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // gridServiceInfo
            // 
            this.gridServiceInfo.AllowUserToAddRows = false;
            this.gridServiceInfo.AllowUserToDeleteRows = false;
            this.gridServiceInfo.AllowUserToResizeRows = false;
            this.gridServiceInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.gridServiceInfo.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridServiceInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridServiceInfo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridServiceInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridServiceInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameParth,
            this.Login,
            this.EmptyColumn});
            this.gridServiceInfo.Location = new System.Drawing.Point(12, 8);
            this.gridServiceInfo.MultiSelect = false;
            this.gridServiceInfo.Name = "gridServiceInfo";
            this.gridServiceInfo.ReadOnly = true;
            this.gridServiceInfo.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.gridServiceInfo.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.gridServiceInfo.RowTemplate.Height = 17;
            this.gridServiceInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridServiceInfo.Size = new System.Drawing.Size(423, 345);
            this.gridServiceInfo.StandardTab = true;
            this.gridServiceInfo.TabIndex = 10;
            this.gridServiceInfo.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gridServiceInfo_CellMouseDoubleClick);
            // 
            // nameParth
            // 
            this.nameParth.HeaderText = "Name";
            this.nameParth.Name = "nameParth";
            this.nameParth.ReadOnly = true;
            this.nameParth.Width = 200;
            // 
            // Login
            // 
            this.Login.HeaderText = "Value";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            this.Login.Width = 200;
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
            // InfoServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(447, 403);
            this.Controls.Add(this.gridServiceInfo);
            this.Controls.Add(this.buttonOK);
            this.MaximumSize = new System.Drawing.Size(455, 600);
            this.MinimumSize = new System.Drawing.Size(455, 200);
            this.Name = "InfoServiceForm";
            this.Text = "Service Info";
            this.Load += new System.EventHandler(this.InfoServiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridServiceInfo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        public System.Windows.Forms.DataGridView gridServiceInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameParth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
    }
}