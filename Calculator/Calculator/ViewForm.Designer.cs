namespace Calculator
{
	partial class ViewForm
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
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewForm));
      textBox1 = new System.Windows.Forms.TextBox();
      label1 = new System.Windows.Forms.Label();
      textBox2 = new System.Windows.Forms.TextBox();
      textBox3 = new System.Windows.Forms.TextBox();
      textBox4 = new System.Windows.Forms.TextBox();
      label2 = new System.Windows.Forms.Label();
      label3 = new System.Windows.Forms.Label();
      label4 = new System.Windows.Forms.Label();
      toolTip1 = new System.Windows.Forms.ToolTip(components);
      button1 = new System.Windows.Forms.Button();
      SuspendLayout();
      // 
      // textBox1
      // 
      textBox1.Location = new System.Drawing.Point(35, 12);
      textBox1.Name = "textBox1";
      textBox1.ReadOnly = true;
      textBox1.Size = new System.Drawing.Size(107, 20);
      textBox1.TabIndex = 0;
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new System.Drawing.Point(10, 15);
      label1.Name = "label1";
      label1.Size = new System.Drawing.Size(19, 13);
      label1.TabIndex = 1;
      label1.Text = "2 -";
      // 
      // textBox2
      // 
      textBox2.Location = new System.Drawing.Point(35, 38);
      textBox2.Name = "textBox2";
      textBox2.ReadOnly = true;
      textBox2.Size = new System.Drawing.Size(107, 20);
      textBox2.TabIndex = 2;
      // 
      // textBox3
      // 
      textBox3.Location = new System.Drawing.Point(35, 64);
      textBox3.Name = "textBox3";
      textBox3.ReadOnly = true;
      textBox3.Size = new System.Drawing.Size(107, 20);
      textBox3.TabIndex = 3;
      // 
      // textBox4
      // 
      textBox4.Location = new System.Drawing.Point(35, 90);
      textBox4.Name = "textBox4";
      textBox4.ReadOnly = true;
      textBox4.Size = new System.Drawing.Size(107, 20);
      textBox4.TabIndex = 4;
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new System.Drawing.Point(10, 41);
      label2.Name = "label2";
      label2.Size = new System.Drawing.Size(19, 13);
      label2.TabIndex = 5;
      label2.Text = "8 -";
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new System.Drawing.Point(4, 67);
      label3.Name = "label3";
      label3.Size = new System.Drawing.Size(25, 13);
      label3.TabIndex = 6;
      label3.Text = "10 -";
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new System.Drawing.Point(4, 93);
      label4.Name = "label4";
      label4.Size = new System.Drawing.Size(25, 13);
      label4.TabIndex = 7;
      label4.Text = "16 -";
      // 
      // button1
      // 
      button1.Image = global::Calculator.Properties.Resources.exit;
      button1.Location = new System.Drawing.Point(102, 117);
      button1.Name = "button1";
      button1.Size = new System.Drawing.Size(40, 40);
      button1.TabIndex = 8;
      button1.UseVisualStyleBackColor = true;
      button1.MouseLeave += new System.EventHandler(button1_MouseLeave);
      button1.Click += new System.EventHandler(button1_Click);
      button1.MouseEnter += new System.EventHandler(button1_MouseEnter);
      // 
      // ViewForm
      // 
      AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      ClientSize = new System.Drawing.Size(154, 162);
      Controls.Add(button1);
      Controls.Add(label4);
      Controls.Add(label3);
      Controls.Add(label2);
      Controls.Add(textBox4);
      Controls.Add(textBox3);
      Controls.Add(textBox2);
      Controls.Add(label1);
      Controls.Add(textBox1);
      FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
      Location = new System.Drawing.Point(50, 50);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "ViewForm";
      ShowInTaskbar = false;
      StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      Text = "Системы счисления";
      ResumeLayout(false);
      PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		public System.Windows.Forms.TextBox textBox1;
		public System.Windows.Forms.TextBox textBox2;
		public System.Windows.Forms.TextBox textBox3;
		public System.Windows.Forms.TextBox textBox4;
		private System.Windows.Forms.ToolTip toolTip1;
	}
}