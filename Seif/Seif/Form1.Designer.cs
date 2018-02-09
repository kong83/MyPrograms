namespace Seif
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
      System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
      this.button1 = new System.Windows.Forms.Button();
      this.text1 = new System.Windows.Forms.TextBox();
      this.text2 = new System.Windows.Forms.TextBox();
      this.text3 = new System.Windows.Forms.TextBox();
      this.text4 = new System.Windows.Forms.TextBox();
      this.text5 = new System.Windows.Forms.TextBox();
      this.TableList = new System.Windows.Forms.DataGridView();
      this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
      this.button2 = new System.Windows.Forms.Button();
      this.label1 = new System.Windows.Forms.Label();
      this.button3 = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.TableList)).BeginInit();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.button1.Location = new System.Drawing.Point(14, 14);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(87, 27);
      this.button1.TabIndex = 0;
      this.button1.Text = "Игра!";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // text1
      // 
      this.text1.Enabled = false;
      this.text1.Location = new System.Drawing.Point(14, 69);
      this.text1.Name = "text1";
      this.text1.Size = new System.Drawing.Size(49, 20);
      this.text1.TabIndex = 1;
      this.text1.TextChanged += new System.EventHandler(this.text1_TextChanged);
      // 
      // text2
      // 
      this.text2.Enabled = false;
      this.text2.Location = new System.Drawing.Point(71, 69);
      this.text2.Name = "text2";
      this.text2.Size = new System.Drawing.Size(49, 20);
      this.text2.TabIndex = 2;
      this.text2.TextChanged += new System.EventHandler(this.text2_TextChanged);
      // 
      // text3
      // 
      this.text3.Enabled = false;
      this.text3.Location = new System.Drawing.Point(127, 69);
      this.text3.Name = "text3";
      this.text3.Size = new System.Drawing.Size(49, 20);
      this.text3.TabIndex = 3;
      this.text3.TextChanged += new System.EventHandler(this.text3_TextChanged);
      // 
      // text4
      // 
      this.text4.Enabled = false;
      this.text4.Location = new System.Drawing.Point(182, 69);
      this.text4.Name = "text4";
      this.text4.Size = new System.Drawing.Size(49, 20);
      this.text4.TabIndex = 4;
      this.text4.TextChanged += new System.EventHandler(this.text4_TextChanged);
      // 
      // text5
      // 
      this.text5.Enabled = false;
      this.text5.Location = new System.Drawing.Point(237, 69);
      this.text5.Name = "text5";
      this.text5.Size = new System.Drawing.Size(49, 20);
      this.text5.TabIndex = 5;
      this.text5.TextChanged += new System.EventHandler(this.text5_TextChanged);
      // 
      // TableList
      // 
      this.TableList.AllowUserToAddRows = false;
      this.TableList.AllowUserToDeleteRows = false;
      this.TableList.AllowUserToResizeColumns = false;
      this.TableList.AllowUserToResizeRows = false;
      this.TableList.BackgroundColor = System.Drawing.Color.White;
      dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
      dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
      dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
      dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
      dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
      dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
      this.TableList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
      this.TableList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.TableList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
      this.TableList.Location = new System.Drawing.Point(12, 95);
      this.TableList.MultiSelect = false;
      this.TableList.Name = "TableList";
      this.TableList.ReadOnly = true;
      this.TableList.RowHeadersVisible = false;
      this.TableList.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.TableList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.TableList.Size = new System.Drawing.Size(442, 241);
      this.TableList.TabIndex = 6;
      // 
      // Column1
      // 
      this.Column1.HeaderText = "1";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column1.Width = 55;
      // 
      // Column2
      // 
      this.Column2.HeaderText = "2";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column2.Width = 55;
      // 
      // Column3
      // 
      this.Column3.HeaderText = "3";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column3.Width = 55;
      // 
      // Column4
      // 
      this.Column4.HeaderText = "4";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column4.Width = 55;
      // 
      // Column5
      // 
      this.Column5.HeaderText = "5";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column5.Width = 55;
      // 
      // Column6
      // 
      this.Column6.HeaderText = "Угадано";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column6.Width = 70;
      // 
      // Column7
      // 
      this.Column7.HeaderText = "На месте";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
      this.Column7.Width = 70;
      // 
      // button2
      // 
      this.button2.Enabled = false;
      this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.button2.Location = new System.Drawing.Point(292, 69);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(83, 20);
      this.button2.TabIndex = 7;
      this.button2.Text = "Принять";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(120, 21);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(0, 13);
      this.label1.TabIndex = 8;
      // 
      // button3
      // 
      this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.button3.Location = new System.Drawing.Point(375, 16);
      this.button3.Name = "button3";
      this.button3.Size = new System.Drawing.Size(79, 23);
      this.button3.TabIndex = 9;
      this.button3.Text = "Правила...";
      this.button3.UseVisualStyleBackColor = true;
      this.button3.Click += new System.EventHandler(this.button3_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(458, 348);
      this.Controls.Add(this.button3);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.TableList);
      this.Controls.Add(this.text5);
      this.Controls.Add(this.text4);
      this.Controls.Add(this.text3);
      this.Controls.Add(this.text2);
      this.Controls.Add(this.text1);
      this.Controls.Add(this.button1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Сейф";
      ((System.ComponentModel.ISupportInitialize)(this.TableList)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox text1;
		private System.Windows.Forms.TextBox text2;
		private System.Windows.Forms.TextBox text3;
		private System.Windows.Forms.TextBox text4;
		private System.Windows.Forms.TextBox text5;
		private System.Windows.Forms.DataGridView TableList;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    private System.Windows.Forms.Button button3;
	}
}

