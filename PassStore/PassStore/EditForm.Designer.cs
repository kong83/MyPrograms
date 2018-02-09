namespace PassStore
{
  partial class EditForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
      this.label3 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.textPassword = new System.Windows.Forms.TextBox();
      this.textLogin = new System.Windows.Forms.TextBox();
      this.textParth = new System.Windows.Forms.TextBox();
      this.buttonOK = new System.Windows.Forms.Button();
      this.buttonClose = new System.Windows.Forms.Button();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.SuspendLayout();
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(12, 65);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(45, 13);
      this.label3.TabIndex = 53;
      this.label3.Text = "Пароль";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(12, 39);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 52;
      this.label2.Text = "Логин";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(12, 13);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(102, 13);
      this.label1.TabIndex = 51;
      this.label1.Text = "Название раздела";
      // 
      // textPassword
      // 
      this.textPassword.Location = new System.Drawing.Point(120, 62);
      this.textPassword.Name = "textPassword";
      this.textPassword.Size = new System.Drawing.Size(188, 20);
      this.textPassword.TabIndex = 2;
      // 
      // textLogin
      // 
      this.textLogin.Location = new System.Drawing.Point(120, 36);
      this.textLogin.Name = "textLogin";
      this.textLogin.Size = new System.Drawing.Size(188, 20);
      this.textLogin.TabIndex = 1;
      // 
      // textParth
      // 
      this.textParth.Location = new System.Drawing.Point(120, 10);
      this.textParth.Name = "textParth";
      this.textParth.Size = new System.Drawing.Size(188, 20);
      this.textParth.TabIndex = 0;
      // 
      // buttonOK
      // 
      this.buttonOK.BackgroundImage = global::PassStore.Properties.Resources.OK;
      this.buttonOK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.buttonOK.FlatAppearance.BorderSize = 0;
      this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.buttonOK.Location = new System.Drawing.Point(97, 96);
      this.buttonOK.Name = "buttonOK";
      this.buttonOK.Size = new System.Drawing.Size(40, 40);
      this.buttonOK.TabIndex = 10;
      this.buttonOK.TabStop = false;
      this.buttonOK.UseVisualStyleBackColor = true;
      this.buttonOK.MouseLeave += new System.EventHandler(this.buttonOK_MouseLeave);
      this.buttonOK.Click += new System.EventHandler(this.OK_Click);
      this.buttonOK.MouseEnter += new System.EventHandler(this.buttonOK_MouseEnter);
      // 
      // buttonClose
      // 
      this.buttonClose.BackgroundImage = global::PassStore.Properties.Resources.close;
      this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
      this.buttonClose.FlatAppearance.BorderSize = 0;
      this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.buttonClose.Location = new System.Drawing.Point(179, 96);
      this.buttonClose.Name = "buttonClose";
      this.buttonClose.Size = new System.Drawing.Size(40, 40);
      this.buttonClose.TabIndex = 11;
      this.buttonClose.TabStop = false;
      this.buttonClose.UseVisualStyleBackColor = true;
      this.buttonClose.MouseLeave += new System.EventHandler(this.buttonDelete_MouseLeave);
      this.buttonClose.Click += new System.EventHandler(this.Cancel_Click);
      this.buttonClose.MouseEnter += new System.EventHandler(this.buttonDelete_MouseEnter);
      // 
      // EditForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(321, 147);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.textPassword);
      this.Controls.Add(this.textLogin);
      this.Controls.Add(this.textParth);
      this.Controls.Add(this.buttonOK);
      this.Controls.Add(this.buttonClose);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EditForm";
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
      this.Text = "Редактирование записи";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textPassword;
    private System.Windows.Forms.TextBox textLogin;
    private System.Windows.Forms.TextBox textParth;
    private System.Windows.Forms.Button buttonOK;
    private System.Windows.Forms.Button buttonClose;
    private System.Windows.Forms.ToolTip toolTip1;
  }
}