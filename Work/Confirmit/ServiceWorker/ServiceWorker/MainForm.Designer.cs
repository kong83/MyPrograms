namespace ServiceWorker
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonInfo = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.gridServices = new System.Windows.Forms.DataGridView();
            this.nameParth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MarketForDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BinaryPath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmptyColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.buttonMagic = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridServices)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRefresh.Location = new System.Drawing.Point(647, 423);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 5;
            this.buttonRefresh.Text = "Refresh";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCreate.Location = new System.Drawing.Point(12, 423);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(75, 23);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonRemove.Location = new System.Drawing.Point(93, 423);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 2;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonInfo
            // 
            this.buttonInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonInfo.Location = new System.Drawing.Point(174, 423);
            this.buttonInfo.Name = "buttonInfo";
            this.buttonInfo.Size = new System.Drawing.Size(75, 23);
            this.buttonInfo.TabIndex = 3;
            this.buttonInfo.Text = "Info";
            this.buttonInfo.UseVisualStyleBackColor = true;
            this.buttonInfo.Click += new System.EventHandler(this.buttonInfo_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStart.Location = new System.Drawing.Point(12, 387);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(75, 23);
            this.buttonStart.TabIndex = 6;
            this.buttonStart.Text = "Start";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonStop.Location = new System.Drawing.Point(93, 387);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(75, 23);
            this.buttonStop.TabIndex = 7;
            this.buttonStop.Text = "Stop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPause.Location = new System.Drawing.Point(174, 387);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(75, 23);
            this.buttonPause.TabIndex = 8;
            this.buttonPause.Text = "Pause";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // gridServices
            // 
            this.gridServices.AllowUserToAddRows = false;
            this.gridServices.AllowUserToDeleteRows = false;
            this.gridServices.AllowUserToResizeRows = false;
            this.gridServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridServices.BackgroundColor = System.Drawing.SystemColors.Window;
            this.gridServices.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gridServices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.nameParth,
            this.Login,
            this.MarketForDelete,
            this.BinaryPath,
            this.Status,
            this.Type,
            this.StartType,
            this.EmptyColumn});
            this.gridServices.Location = new System.Drawing.Point(12, 12);
            this.gridServices.MultiSelect = false;
            this.gridServices.Name = "gridServices";
            this.gridServices.ReadOnly = true;
            this.gridServices.RowHeadersVisible = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.gridServices.RowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridServices.RowTemplate.Height = 17;
            this.gridServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridServices.Size = new System.Drawing.Size(710, 362);
            this.gridServices.StandardTab = true;
            this.gridServices.TabIndex = 11;
            // 
            // nameParth
            // 
            this.nameParth.HeaderText = "Service name";
            this.nameParth.Name = "nameParth";
            this.nameParth.ReadOnly = true;
            // 
            // Login
            // 
            this.Login.HeaderText = "Display name";
            this.Login.Name = "Login";
            this.Login.ReadOnly = true;
            // 
            // MarketForDelete
            // 
            this.MarketForDelete.HeaderText = "Market for delete";
            this.MarketForDelete.Name = "MarketForDelete";
            this.MarketForDelete.ReadOnly = true;
            this.MarketForDelete.Width = 80;
            // 
            // BinaryPath
            // 
            this.BinaryPath.HeaderText = "File path";
            this.BinaryPath.Name = "BinaryPath";
            this.BinaryPath.ReadOnly = true;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Type
            // 
            this.Type.HeaderText = "Type";
            this.Type.Name = "Type";
            this.Type.ReadOnly = true;
            // 
            // StartType
            // 
            this.StartType.HeaderText = "Start type";
            this.StartType.Name = "StartType";
            this.StartType.ReadOnly = true;
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
            // buttonMagic
            // 
            this.buttonMagic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMagic.Location = new System.Drawing.Point(647, 387);
            this.buttonMagic.Name = "buttonMagic";
            this.buttonMagic.Size = new System.Drawing.Size(75, 23);
            this.buttonMagic.TabIndex = 12;
            this.buttonMagic.Text = "Magic";
            this.buttonMagic.UseVisualStyleBackColor = true;
            this.buttonMagic.Click += new System.EventHandler(this.buttonMagic_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelVersion.AutoSize = true;
            this.labelVersion.Location = new System.Drawing.Point(13, 447);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(68, 13);
            this.labelVersion.TabIndex = 13;
            this.labelVersion.Text = "version 2.0.4";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 461);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.buttonMagic);
            this.Controls.Add(this.gridServices);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.buttonInfo);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonCreate);
            this.Controls.Add(this.buttonRefresh);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 225);
            this.Name = "MainForm";
            this.Text = "Service Worker";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridServices)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonInfo;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonPause;
        public System.Windows.Forms.DataGridView gridServices;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameParth;
        private System.Windows.Forms.DataGridViewTextBoxColumn Login;
        private System.Windows.Forms.DataGridViewTextBoxColumn MarketForDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn BinaryPath;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Type;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartType;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmptyColumn;
        private System.Windows.Forms.Button buttonMagic;
        private System.Windows.Forms.Label labelVersion;
    }
}

