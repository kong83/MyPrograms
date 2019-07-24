namespace SurgeryHelper
{
    partial class CureForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CureForm));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.CureList = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDuration = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonmakeLast = new System.Windows.Forms.Button();
            this.buttonMakeFirst = new System.Windows.Forms.Button();
            this.buttonTherapyDown = new System.Windows.Forms.Button();
            this.buttonTherapyUp = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.CureList)).BeginInit();
            this.SuspendLayout();
            // 
            // CureList
            // 
            this.CureList.AllowUserToAddRows = false;
            this.CureList.AllowUserToDeleteRows = false;
            this.CureList.AllowUserToResizeRows = false;
            this.CureList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CureList.BackgroundColor = System.Drawing.SystemColors.Window;
            this.CureList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CureList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.CureList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CureList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.Column1,
            this.ColumnDuration,
            this.dataGridViewTextBoxColumn4});
            this.CureList.Location = new System.Drawing.Point(12, 12);
            this.CureList.MultiSelect = false;
            this.CureList.Name = "CureList";
            this.CureList.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.CureList.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.CureList.RowTemplate.Height = 17;
            this.CureList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CureList.Size = new System.Drawing.Size(734, 242);
            this.CureList.StandardTab = true;
            this.CureList.TabIndex = 74;
            this.CureList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.curesList_MouseDoubleClick);
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.FillWeight = 350F;
            this.dataGridViewTextBoxColumn2.HeaderText = "Препарат (название и доза)";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Width = 350;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 120F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Дефолтное кол-во приёмов";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn3.Width = 120;
            // 
            // Column1
            // 
            this.Column1.FillWeight = 120F;
            this.Column1.HeaderText = "Дефолтный способ введения";
            this.Column1.Name = "Column1";
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column1.Width = 120;
            // 
            // ColumnDuration
            // 
            this.ColumnDuration.FillWeight = 120F;
            this.ColumnDuration.HeaderText = "Дефолтная продолжительность";
            this.ColumnDuration.Name = "ColumnDuration";
            this.ColumnDuration.Width = 120;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn4.HeaderText = "";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 2;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // buttonmakeLast
            // 
            this.buttonmakeLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonmakeLast.BackgroundImage = global::SurgeryHelper.Properties.Resources.makeLast;
            this.buttonmakeLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonmakeLast.FlatAppearance.BorderSize = 0;
            this.buttonmakeLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonmakeLast.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonmakeLast.Location = new System.Drawing.Point(598, 260);
            this.buttonmakeLast.Name = "buttonmakeLast";
            this.buttonmakeLast.Size = new System.Drawing.Size(40, 40);
            this.buttonmakeLast.TabIndex = 97;
            this.buttonmakeLast.TabStop = false;
            this.buttonmakeLast.UseVisualStyleBackColor = true;
            this.buttonmakeLast.Click += new System.EventHandler(this.buttonmakeLast_Click);
            this.buttonmakeLast.MouseEnter += new System.EventHandler(this.buttonmakeLast_MouseEnter);
            this.buttonmakeLast.MouseLeave += new System.EventHandler(this.buttonmakeLast_MouseLeave);
            // 
            // buttonMakeFirst
            // 
            this.buttonMakeFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMakeFirst.BackgroundImage = global::SurgeryHelper.Properties.Resources.makeFirst;
            this.buttonMakeFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonMakeFirst.FlatAppearance.BorderSize = 0;
            this.buttonMakeFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMakeFirst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonMakeFirst.Location = new System.Drawing.Point(460, 260);
            this.buttonMakeFirst.Name = "buttonMakeFirst";
            this.buttonMakeFirst.Size = new System.Drawing.Size(40, 40);
            this.buttonMakeFirst.TabIndex = 96;
            this.buttonMakeFirst.TabStop = false;
            this.buttonMakeFirst.UseVisualStyleBackColor = true;
            this.buttonMakeFirst.Click += new System.EventHandler(this.buttonMakeFirst_Click);
            this.buttonMakeFirst.MouseEnter += new System.EventHandler(this.buttonMakeFirst_MouseEnter);
            this.buttonMakeFirst.MouseLeave += new System.EventHandler(this.buttonMakeFirst_MouseLeave);
            // 
            // buttonTherapyDown
            // 
            this.buttonTherapyDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTherapyDown.BackgroundImage = global::SurgeryHelper.Properties.Resources.down;
            this.buttonTherapyDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTherapyDown.FlatAppearance.BorderSize = 0;
            this.buttonTherapyDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTherapyDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTherapyDown.Location = new System.Drawing.Point(552, 260);
            this.buttonTherapyDown.Name = "buttonTherapyDown";
            this.buttonTherapyDown.Size = new System.Drawing.Size(40, 40);
            this.buttonTherapyDown.TabIndex = 95;
            this.buttonTherapyDown.TabStop = false;
            this.buttonTherapyDown.UseVisualStyleBackColor = true;
            this.buttonTherapyDown.Click += new System.EventHandler(this.buttonTherapyDown_Click);
            this.buttonTherapyDown.MouseEnter += new System.EventHandler(this.buttonTherapyDown_MouseEnter);
            this.buttonTherapyDown.MouseLeave += new System.EventHandler(this.buttonTherapyDown_MouseLeave);
            // 
            // buttonTherapyUp
            // 
            this.buttonTherapyUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTherapyUp.BackgroundImage = global::SurgeryHelper.Properties.Resources.up;
            this.buttonTherapyUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonTherapyUp.FlatAppearance.BorderSize = 0;
            this.buttonTherapyUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTherapyUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTherapyUp.Location = new System.Drawing.Point(506, 260);
            this.buttonTherapyUp.Name = "buttonTherapyUp";
            this.buttonTherapyUp.Size = new System.Drawing.Size(40, 40);
            this.buttonTherapyUp.TabIndex = 94;
            this.buttonTherapyUp.TabStop = false;
            this.buttonTherapyUp.UseVisualStyleBackColor = true;
            this.buttonTherapyUp.Click += new System.EventHandler(this.buttonTherapyUp_Click);
            this.buttonTherapyUp.MouseEnter += new System.EventHandler(this.buttonTherapyUp_MouseEnter);
            this.buttonTherapyUp.MouseLeave += new System.EventHandler(this.buttonTherapyUp_MouseLeave);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonEdit.BackgroundImage = global::SurgeryHelper.Properties.Resources.edit;
            this.buttonEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonEdit.FlatAppearance.BorderSize = 0;
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.Location = new System.Drawing.Point(104, 260);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(40, 40);
            this.buttonEdit.TabIndex = 73;
            this.buttonEdit.TabStop = false;
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            this.buttonEdit.MouseEnter += new System.EventHandler(this.buttonEdit_MouseEnter);
            this.buttonEdit.MouseLeave += new System.EventHandler(this.buttonEdit_MouseLeave);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelete.BackgroundImage = global::SurgeryHelper.Properties.Resources.delete;
            this.buttonDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Location = new System.Drawing.Point(58, 260);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(40, 40);
            this.buttonDelete.TabIndex = 72;
            this.buttonDelete.TabStop = false;
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            this.buttonDelete.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
            this.buttonDelete.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.BackgroundImage = global::SurgeryHelper.Properties.Resources.add;
            this.buttonAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Location = new System.Drawing.Point(12, 260);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(40, 40);
            this.buttonAdd.TabIndex = 71;
            this.buttonAdd.TabStop = false;
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            this.buttonAdd.MouseEnter += new System.EventHandler(this.buttonAdd_MouseEnter);
            this.buttonAdd.MouseLeave += new System.EventHandler(this.buttonAdd_MouseLeave);
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(706, 260);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 70;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            // 
            // CureForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 312);
            this.Controls.Add(this.buttonmakeLast);
            this.Controls.Add(this.buttonMakeFirst);
            this.Controls.Add(this.buttonTherapyDown);
            this.Controls.Add(this.buttonTherapyUp);
            this.Controls.Add(this.CureList);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.buttonOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(400, 298);
            this.Name = "CureForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Список лекарств";
            this.Load += new System.EventHandler(this.CureForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.CureList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonOk;
        public System.Windows.Forms.DataGridView CureList;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDuration;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.Button buttonTherapyDown;
        private System.Windows.Forms.Button buttonTherapyUp;
        private System.Windows.Forms.Button buttonMakeFirst;
        private System.Windows.Forms.Button buttonmakeLast;
    }
}