namespace Proba
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
			this.btnCallAsynch = new System.Windows.Forms.Button();
			this.btnCallSynch = new System.Windows.Forms.Button();
			this.textBoxResult = new System.Windows.Forms.TextBox();
			this.comboAction = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnCallAsynch
			// 
			this.btnCallAsynch.Location = new System.Drawing.Point(12, 242);
			this.btnCallAsynch.Name = "btnCallAsynch";
			this.btnCallAsynch.Size = new System.Drawing.Size(136, 23);
			this.btnCallAsynch.TabIndex = 4;
			this.btnCallAsynch.Text = "Call Asynchronously";
			this.btnCallAsynch.Click += new System.EventHandler(this.btnCallAsynch_Click);
			// 
			// btnCallSynch
			// 
			this.btnCallSynch.Location = new System.Drawing.Point(12, 210);
			this.btnCallSynch.Name = "btnCallSynch";
			this.btnCallSynch.Size = new System.Drawing.Size(136, 23);
			this.btnCallSynch.TabIndex = 3;
			this.btnCallSynch.Text = "Call Synchronously";
			this.btnCallSynch.Click += new System.EventHandler(this.btnCallSynch_Click);
			// 
			// textBoxResult
			// 
			this.textBoxResult.Location = new System.Drawing.Point(12, 91);
			this.textBoxResult.Multiline = true;
			this.textBoxResult.Name = "textBoxResult";
			this.textBoxResult.Size = new System.Drawing.Size(124, 113);
			this.textBoxResult.TabIndex = 2;
			// 
			// comboAction
			// 
			this.comboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboAction.FormattingEnabled = true;
			this.comboAction.Items.AddRange(new object[] {
            "Время",
            "Список времён"});
			this.comboAction.Location = new System.Drawing.Point(12, 28);
			this.comboAction.Name = "comboAction";
			this.comboAction.Size = new System.Drawing.Size(124, 21);
			this.comboAction.TabIndex = 0;
			this.comboAction.SelectedIndexChanged += new System.EventHandler(this.comboAction_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(127, 13);
			this.label1.TabIndex = 12;
			this.label1.Text = "Выберите, что получать";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Location = new System.Drawing.Point(12, 55);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(40, 20);
			this.numericUpDown1.TabIndex = 13;
			this.numericUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.numericUpDown1.Visible = false;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(158, 277);
			this.Controls.Add(this.numericUpDown1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboAction);
			this.Controls.Add(this.textBoxResult);
			this.Controls.Add(this.btnCallAsynch);
			this.Controls.Add(this.btnCallSynch);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Location = new System.Drawing.Point(300, 100);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Клиент";
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnCallAsynch;
		private System.Windows.Forms.Button btnCallSynch;
		private System.Windows.Forms.TextBox textBoxResult;
		private System.Windows.Forms.ComboBox comboAction;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;

	}
}

