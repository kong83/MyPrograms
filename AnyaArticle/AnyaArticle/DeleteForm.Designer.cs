namespace AnyaArticle
{
  partial class DeleteForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteForm));
      this.button1 = new System.Windows.Forms.Button();
      this.textBox1 = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.button4 = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // button1
      // 
      this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.button1.Image = global::AnyaArticle.Properties.Resources.OK;
      this.button1.Location = new System.Drawing.Point(100, 69);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(40, 40);
      this.button1.TabIndex = 1;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
      this.button1.Click += new System.EventHandler(this.button1_Click);
      this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
      // 
      // textBox1
      // 
      this.textBox1.Location = new System.Drawing.Point(12, 37);
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new System.Drawing.Size(179, 20);
      this.textBox1.TabIndex = 0;
      this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
      this.textBox1.MouseLeave += new System.EventHandler(this.textBox1_MouseLeave);
      this.textBox1.MouseEnter += new System.EventHandler(this.textBox1_MouseEnter);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.label1.Location = new System.Drawing.Point(36, 6);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(114, 26);
      this.label1.TabIndex = 100;
      this.label1.Text = "¬ведите номер п/п\r\nзаписи дл€ удалени€";
      this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // button4
      // 
      this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
      this.button4.Image = global::AnyaArticle.Properties.Resources.close;
      this.button4.Location = new System.Drawing.Point(151, 69);
      this.button4.Name = "button4";
      this.button4.Size = new System.Drawing.Size(40, 40);
      this.button4.TabIndex = 2;
      this.button4.UseVisualStyleBackColor = true;
      this.button4.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
      this.button4.Click += new System.EventHandler(this.button4_Click);
      this.button4.MouseEnter += new System.EventHandler(this.button4_MouseEnter);
      // 
      // DeleteForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(203, 119);
      this.Controls.Add(this.button4);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textBox1);
      this.Controls.Add(this.button1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DeleteForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "”даление записей";
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Button button4;
		private System.Windows.Forms.ToolTip toolTip1;
  }
}