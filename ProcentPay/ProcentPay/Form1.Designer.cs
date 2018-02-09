namespace ProcentPay
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
      this.textSumm = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.textProcent = new System.Windows.Forms.TextBox();
      this.textResult1 = new System.Windows.Forms.TextBox();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.textMonth = new System.Windows.Forms.TextBox();
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.groupBox2 = new System.Windows.Forms.GroupBox();
      this.label5 = new System.Windows.Forms.Label();
      this.button2 = new System.Windows.Forms.Button();
      this.textResult2 = new System.Windows.Forms.TextBox();
      this.textPay = new System.Windows.Forms.TextBox();
      this.label6 = new System.Windows.Forms.Label();
      this.textPayAdd = new System.Windows.Forms.TextBox();
      this.label7 = new System.Windows.Forms.Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Location = new System.Drawing.Point(50, 96);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(89, 23);
      this.button1.TabIndex = 2;
      this.button1.Text = "Выполнить";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // textSumm
      // 
      this.textSumm.Location = new System.Drawing.Point(101, 25);
      this.textSumm.Name = "textSumm";
      this.textSumm.Size = new System.Drawing.Size(100, 20);
      this.textSumm.TabIndex = 0;
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(100, 9);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(98, 13);
      this.label1.TabIndex = 2;
      this.label1.Text = "Сумма кредита";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label2.Location = new System.Drawing.Point(204, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(106, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Процентов в год";
      // 
      // textProcent
      // 
      this.textProcent.Location = new System.Drawing.Point(207, 25);
      this.textProcent.Name = "textProcent";
      this.textProcent.Size = new System.Drawing.Size(106, 20);
      this.textProcent.TabIndex = 1;
      // 
      // textResult1
      // 
      this.textResult1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.textResult1.Location = new System.Drawing.Point(71, 64);
      this.textResult1.Name = "textResult1";
      this.textResult1.Size = new System.Drawing.Size(100, 20);
      this.textResult1.TabIndex = 1;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label3.Location = new System.Drawing.Point(6, 61);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(42, 26);
      this.label3.TabIndex = 6;
      this.label3.Text = "Общая\r\nсумма";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label4.Location = new System.Drawing.Point(6, 21);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(51, 26);
      this.label4.TabIndex = 8;
      this.label4.Text = "Кол-во\r\nмесяцев";
      // 
      // textMonth
      // 
      this.textMonth.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.textMonth.Location = new System.Drawing.Point(71, 25);
      this.textMonth.Name = "textMonth";
      this.textMonth.Size = new System.Drawing.Size(100, 20);
      this.textMonth.TabIndex = 0;
      this.textMonth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textMonth_KeyPress);
      // 
      // groupBox1
      // 
      this.groupBox1.Controls.Add(this.label4);
      this.groupBox1.Controls.Add(this.button1);
      this.groupBox1.Controls.Add(this.textResult1);
      this.groupBox1.Controls.Add(this.textMonth);
      this.groupBox1.Controls.Add(this.label3);
      this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.groupBox1.Location = new System.Drawing.Point(12, 56);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new System.Drawing.Size(189, 128);
      this.groupBox1.TabIndex = 2;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Набегающая сумма";
      // 
      // groupBox2
      // 
      this.groupBox2.Controls.Add(this.label7);
      this.groupBox2.Controls.Add(this.textPayAdd);
      this.groupBox2.Controls.Add(this.label5);
      this.groupBox2.Controls.Add(this.button2);
      this.groupBox2.Controls.Add(this.textResult2);
      this.groupBox2.Controls.Add(this.textPay);
      this.groupBox2.Controls.Add(this.label6);
      this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.groupBox2.Location = new System.Drawing.Point(207, 56);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new System.Drawing.Size(189, 157);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Время погашения";
      // 
      // label5
      // 
      this.label5.AutoSize = true;
      this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label5.Location = new System.Drawing.Point(9, 21);
      this.label5.Name = "label5";
      this.label5.Size = new System.Drawing.Size(52, 26);
      this.label5.TabIndex = 13;
      this.label5.Text = "Сумма\r\nвыплаты";
      // 
      // button2
      // 
      this.button2.Location = new System.Drawing.Point(53, 126);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(89, 23);
      this.button2.TabIndex = 11;
      this.button2.Text = "Выполнить";
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // textResult2
      // 
      this.textResult2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.textResult2.Location = new System.Drawing.Point(74, 64);
      this.textResult2.Name = "textResult2";
      this.textResult2.Size = new System.Drawing.Size(100, 20);
      this.textResult2.TabIndex = 1;
      // 
      // textPay
      // 
      this.textPay.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.textPay.Location = new System.Drawing.Point(74, 25);
      this.textPay.Name = "textPay";
      this.textPay.Size = new System.Drawing.Size(100, 20);
      this.textPay.TabIndex = 0;
      this.textPay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textPay_KeyPress);
      // 
      // label6
      // 
      this.label6.AutoSize = true;
      this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label6.Location = new System.Drawing.Point(9, 60);
      this.label6.Name = "label6";
      this.label6.Size = new System.Drawing.Size(51, 26);
      this.label6.TabIndex = 12;
      this.label6.Text = "Кол-во\r\nмесяцев";
      // 
      // textPayAdd
      // 
      this.textPayAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.textPayAdd.Location = new System.Drawing.Point(74, 95);
      this.textPayAdd.Name = "textPayAdd";
      this.textPayAdd.Size = new System.Drawing.Size(100, 20);
      this.textPayAdd.TabIndex = 14;
      // 
      // label7
      // 
      this.label7.AutoSize = true;
      this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label7.Location = new System.Drawing.Point(10, 98);
      this.label7.Name = "label7";
      this.label7.Size = new System.Drawing.Size(62, 13);
      this.label7.TabIndex = 15;
      this.label7.Text = "Переплата";
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(407, 225);
      this.Controls.Add(this.groupBox2);
      this.Controls.Add(this.groupBox1);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.textProcent);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textSumm);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Подсчёт";
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox textSumm;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textProcent;
		private System.Windows.Forms.TextBox textResult1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textMonth;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textResult2;
		private System.Windows.Forms.TextBox textPay;
		private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.TextBox textPayAdd;
	}
}

