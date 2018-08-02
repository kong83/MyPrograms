namespace LtuErrorsParser
{
  partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnServerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnEventTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFullText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnErrorCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Empty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.richTextBoxDetails = new System.Windows.Forms.RichTextBox();
            this.labelErrorCount = new System.Windows.Forms.Label();
            this.labelErrorTypes = new System.Windows.Forms.Label();
            this.panelMove = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(56, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Get";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnServerName,
            this.ColumnEventTime,
            this.ColumnFullText,
            this.ColumnText,
            this.ColumnErrorCount,
            this.Empty});
            this.dataGridView.GridColor = System.Drawing.Color.Black;
            this.dataGridView.Location = new System.Drawing.Point(12, 79);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(1402, 415);
            this.dataGridView.TabIndex = 13;
            this.dataGridView.TabStop = false;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // ColumnServerName
            // 
            this.ColumnServerName.HeaderText = "ServerName";
            this.ColumnServerName.Name = "ColumnServerName";
            this.ColumnServerName.Width = 110;
            // 
            // ColumnEventTime
            // 
            this.ColumnEventTime.HeaderText = "EventTime";
            this.ColumnEventTime.Name = "ColumnEventTime";
            this.ColumnEventTime.Width = 110;
            // 
            // ColumnFullText
            // 
            this.ColumnFullText.HeaderText = "FullText";
            this.ColumnFullText.Name = "ColumnFullText";
            this.ColumnFullText.Visible = false;
            // 
            // ColumnText
            // 
            this.ColumnText.HeaderText = "Text";
            this.ColumnText.Name = "ColumnText";
            this.ColumnText.Width = 1080;
            // 
            // ColumnErrorCount
            // 
            this.ColumnErrorCount.HeaderText = "ErrorCount";
            this.ColumnErrorCount.Name = "ColumnErrorCount";
            this.ColumnErrorCount.Width = 80;
            // 
            // Empty
            // 
            this.Empty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Empty.HeaderText = "";
            this.Empty.Name = "Empty";
            // 
            // richTextBoxDetails
            // 
            this.richTextBoxDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBoxDetails.Location = new System.Drawing.Point(12, 512);
            this.richTextBoxDetails.Name = "richTextBoxDetails";
            this.richTextBoxDetails.Size = new System.Drawing.Size(1402, 245);
            this.richTextBoxDetails.TabIndex = 14;
            this.richTextBoxDetails.Text = "";
            // 
            // labelErrorCount
            // 
            this.labelErrorCount.AutoSize = true;
            this.labelErrorCount.Location = new System.Drawing.Point(176, 21);
            this.labelErrorCount.Name = "labelErrorCount";
            this.labelErrorCount.Size = new System.Drawing.Size(34, 13);
            this.labelErrorCount.TabIndex = 15;
            this.labelErrorCount.Text = "count";
            // 
            // labelErrorTypes
            // 
            this.labelErrorTypes.AutoSize = true;
            this.labelErrorTypes.Location = new System.Drawing.Point(176, 43);
            this.labelErrorTypes.Name = "labelErrorTypes";
            this.labelErrorTypes.Size = new System.Drawing.Size(27, 13);
            this.labelErrorTypes.TabIndex = 16;
            this.labelErrorTypes.Text = "type";
            // 
            // panelMove
            // 
            this.panelMove.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.panelMove.Location = new System.Drawing.Point(12, 494);
            this.panelMove.Name = "panelMove";
            this.panelMove.Size = new System.Drawing.Size(1402, 18);
            this.panelMove.TabIndex = 17;
            this.panelMove.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMove_MouseDown);
            this.panelMove.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMove_MouseMove);
            this.panelMove.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMove_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1426, 769);
            this.Controls.Add(this.panelMove);
            this.Controls.Add(this.labelErrorTypes);
            this.Controls.Add(this.labelErrorCount);
            this.Controls.Add(this.richTextBoxDetails);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Test";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.DataGridView dataGridView;
    private System.Windows.Forms.RichTextBox richTextBoxDetails;
    private System.Windows.Forms.Label labelErrorCount;
    private System.Windows.Forms.Label labelErrorTypes;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnServerName;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnEventTime;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFullText;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnText;
    private System.Windows.Forms.DataGridViewTextBoxColumn ColumnErrorCount;
    private System.Windows.Forms.DataGridViewTextBoxColumn Empty;
    private System.Windows.Forms.Panel panelMove;
  }
}

