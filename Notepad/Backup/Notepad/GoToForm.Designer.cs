namespace Notepad
{
  partial class GoToForm
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
      this.textGoTo = new System.Windows.Forms.TextBox();
      this.button2 = new System.Windows.Forms.Button();
      this.button1 = new System.Windows.Forms.Button();
      this.labelInfo = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textGoTo
      // 
      this.textGoTo.Location = new System.Drawing.Point(4, 23);
      this.textGoTo.Name = "textGoTo";
      this.textGoTo.Size = new System.Drawing.Size(75, 20);
      this.textGoTo.TabIndex = 0;
      this.textGoTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textGoTo_KeyPress);
      // 
      // button2
      // 
      this.button2.Image = global::Notepad.Properties.Resources.cansel24;
      this.button2.Location = new System.Drawing.Point(49, 52);
      this.button2.Name = "button2";
      this.button2.Size = new System.Drawing.Size(30, 30);
      this.button2.TabIndex = 2;
      this.button2.UseVisualStyleBackColor = true;
      this.button2.Click += new System.EventHandler(this.button2_Click);
      // 
      // button1
      // 
      this.button1.Image = global::Notepad.Properties.Resources.ok24;
      this.button1.Location = new System.Drawing.Point(4, 52);
      this.button1.Name = "button1";
      this.button1.Size = new System.Drawing.Size(30, 30);
      this.button1.TabIndex = 1;
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new System.EventHandler(this.button1_Click);
      // 
      // labelInfo
      // 
      this.labelInfo.Location = new System.Drawing.Point(-2, 3);
      this.labelInfo.Name = "labelInfo";
      this.labelInfo.Size = new System.Drawing.Size(90, 15);
      this.labelInfo.TabIndex = 3;
      this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // GoToForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(84, 84);
      this.ControlBox = false;
      this.Controls.Add(this.labelInfo);
      this.Controls.Add(this.button2);
      this.Controls.Add(this.button1);
      this.Controls.Add(this.textGoTo);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(90, 90);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(90, 90);
      this.Name = "GoToForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textGoTo;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Label labelInfo;
  }
}