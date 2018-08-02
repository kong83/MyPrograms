namespace Notepad
{
    partial class ProcessInfoForm
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
            this.ProcessInfoList = new System.Windows.Forms.DataGridView();
            this.column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelInfo = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ProcessInfoList)).BeginInit();
            this.SuspendLayout();
            // 
            // ProcessInfoList
            // 
            this.ProcessInfoList.AllowUserToAddRows = false;
            this.ProcessInfoList.AllowUserToDeleteRows = false;
            this.ProcessInfoList.AllowUserToResizeRows = false;
            this.ProcessInfoList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessInfoList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.ProcessInfoList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ProcessInfoList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.ProcessInfoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProcessInfoList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.column1,
            this.column2});
            this.ProcessInfoList.Location = new System.Drawing.Point(3, 12);
            this.ProcessInfoList.MultiSelect = false;
            this.ProcessInfoList.Name = "ProcessInfoList";
            this.ProcessInfoList.ReadOnly = true;
            this.ProcessInfoList.RowHeadersVisible = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProcessInfoList.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.ProcessInfoList.RowTemplate.Height = 17;
            this.ProcessInfoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.ProcessInfoList.Size = new System.Drawing.Size(483, 410);
            this.ProcessInfoList.StandardTab = true;
            this.ProcessInfoList.TabIndex = 47;
            this.ProcessInfoList.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.ProcessInfoList_CellMouseDoubleClick);
            // 
            // column1
            // 
            this.column1.HeaderText = "Название";
            this.column1.Name = "column1";
            this.column1.ReadOnly = true;
            this.column1.Width = 200;
            // 
            // column2
            // 
            this.column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.column2.HeaderText = "Информация";
            this.column2.Name = "column2";
            this.column2.ReadOnly = true;
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::Notepad.Properties.Resources.cansel24;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Location = new System.Drawing.Point(449, 428);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 50;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // labelInfo
            // 
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(0, 428);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(298, 13);
            this.labelInfo.TabIndex = 51;
            this.labelInfo.Text = "Кликните дважды для копирования информации в буфер";
            // 
            // ProcessInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 473);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.ProcessInfoList);
            this.Name = "ProcessInfoForm";
            this.Text = "Дополнительная информация";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ProcessInfoForm_Load);
            this.Activated += new System.EventHandler(this.ProcessInfoForm_Activated);
            ((System.ComponentModel.ISupportInitialize)(this.ProcessInfoList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView ProcessInfoList;
        private System.Windows.Forms.DataGridViewTextBoxColumn column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn column2;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelInfo;
    }
}