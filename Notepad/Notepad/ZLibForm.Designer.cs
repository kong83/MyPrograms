namespace Notepad
{
    partial class ZLibForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ZLibForm));
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.labelText = new System.Windows.Forms.Label();
            this.textBoxText = new System.Windows.Forms.TextBox();
            this.buttonUnpack = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxExtraParameters = new System.Windows.Forms.GroupBox();
            this.buttonFindTheEnd = new System.Windows.Forms.Button();
            this.labelFindTheEnd = new System.Windows.Forms.Label();
            this.comboBoxCoding = new System.Windows.Forms.ComboBox();
            this.labelCoding = new System.Windows.Forms.Label();
            this.textBoxSkipLast = new System.Windows.Forms.TextBox();
            this.labelSkipLast = new System.Windows.Forms.Label();
            this.textBoxSkipFirst = new System.Windows.Forms.TextBox();
            this.labelSkipFirst = new System.Windows.Forms.Label();
            this.buttonShowExtraParameters = new System.Windows.Forms.Button();
            this.buttonHideExtraParameters = new System.Windows.Forms.Button();
            this.buttonPack = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.groupBoxExtraParameters.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(92, 6);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(524, 20);
            this.textBoxPath.TabIndex = 0;
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 9);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(74, 13);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Путь к файлу";
            // 
            // buttonSelect
            // 
            this.buttonSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSelect.BackgroundImage = global::Notepad.Properties.Resources.open16;
            this.buttonSelect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonSelect.Location = new System.Drawing.Point(622, 4);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(24, 23);
            this.buttonSelect.TabIndex = 2;
            this.buttonSelect.UseVisualStyleBackColor = true;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // labelText
            // 
            this.labelText.AutoSize = true;
            this.labelText.Location = new System.Drawing.Point(12, 109);
            this.labelText.Name = "labelText";
            this.labelText.Size = new System.Drawing.Size(37, 13);
            this.labelText.TabIndex = 3;
            this.labelText.Text = "Текст";
            // 
            // textBoxText
            // 
            this.textBoxText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxText.Location = new System.Drawing.Point(92, 42);
            this.textBoxText.MaxLength = 2000000000;
            this.textBoxText.Multiline = true;
            this.textBoxText.Name = "textBoxText";
            this.textBoxText.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxText.Size = new System.Drawing.Size(524, 224);
            this.textBoxText.TabIndex = 4;
            this.textBoxText.WordWrap = false;
            // 
            // buttonUnpack
            // 
            this.buttonUnpack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonUnpack.BackgroundImage = global::Notepad.Properties.Resources.Unpack;
            this.buttonUnpack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonUnpack.Location = new System.Drawing.Point(177, 277);
            this.buttonUnpack.Name = "buttonUnpack";
            this.buttonUnpack.Size = new System.Drawing.Size(40, 40);
            this.buttonUnpack.TabIndex = 8;
            this.buttonUnpack.UseVisualStyleBackColor = true;
            this.buttonUnpack.MouseLeave += new System.EventHandler(this.buttonUnpack_MouseLeave);
            this.buttonUnpack.Click += new System.EventHandler(this.buttonUnpack_Click);
            this.buttonUnpack.MouseEnter += new System.EventHandler(this.buttonUnpack_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::Notepad.Properties.Resources.cansel24;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.Location = new System.Drawing.Point(615, 280);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(35, 35);
            this.buttonClose.TabIndex = 12;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.CheckPathExists = false;
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // groupBoxExtraParameters
            // 
            this.groupBoxExtraParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxExtraParameters.Controls.Add(this.buttonFindTheEnd);
            this.groupBoxExtraParameters.Controls.Add(this.labelFindTheEnd);
            this.groupBoxExtraParameters.Controls.Add(this.comboBoxCoding);
            this.groupBoxExtraParameters.Controls.Add(this.labelCoding);
            this.groupBoxExtraParameters.Controls.Add(this.textBoxSkipLast);
            this.groupBoxExtraParameters.Controls.Add(this.labelSkipLast);
            this.groupBoxExtraParameters.Controls.Add(this.textBoxSkipFirst);
            this.groupBoxExtraParameters.Controls.Add(this.labelSkipFirst);
            this.groupBoxExtraParameters.Location = new System.Drawing.Point(92, 337);
            this.groupBoxExtraParameters.Name = "groupBoxExtraParameters";
            this.groupBoxExtraParameters.Size = new System.Drawing.Size(524, 100);
            this.groupBoxExtraParameters.TabIndex = 10;
            this.groupBoxExtraParameters.TabStop = false;
            this.groupBoxExtraParameters.Text = "Дополнительные параметры";
            this.groupBoxExtraParameters.Visible = false;
            // 
            // buttonFindTheEnd
            // 
            this.buttonFindTheEnd.Location = new System.Drawing.Point(391, 66);
            this.buttonFindTheEnd.Name = "buttonFindTheEnd";
            this.buttonFindTheEnd.Size = new System.Drawing.Size(78, 23);
            this.buttonFindTheEnd.TabIndex = 6;
            this.buttonFindTheEnd.Text = "Выполнить";
            this.buttonFindTheEnd.UseVisualStyleBackColor = true;
            // 
            // labelFindTheEnd
            // 
            this.labelFindTheEnd.AutoSize = true;
            this.labelFindTheEnd.Location = new System.Drawing.Point(245, 49);
            this.labelFindTheEnd.Name = "labelFindTheEnd";
            this.labelFindTheEnd.Size = new System.Drawing.Size(172, 26);
            this.labelFindTheEnd.TabIndex = 7;
            this.labelFindTheEnd.Text = "Определить количество лишних \r\nсимволов в конце архива";
            // 
            // comboBoxCoding
            // 
            this.comboBoxCoding.FormattingEnabled = true;
            this.comboBoxCoding.Items.AddRange(new object[] {
            "По умолчанию",
            "Unicode",
            "KOI8-R",
            "ASCII",
            "windows-1251"});
            this.comboBoxCoding.Location = new System.Drawing.Point(313, 22);
            this.comboBoxCoding.Name = "comboBoxCoding";
            this.comboBoxCoding.Size = new System.Drawing.Size(157, 21);
            this.comboBoxCoding.TabIndex = 5;
            this.comboBoxCoding.Text = "По умолчанию";
            // 
            // labelCoding
            // 
            this.labelCoding.AutoSize = true;
            this.labelCoding.Location = new System.Drawing.Point(245, 26);
            this.labelCoding.Name = "labelCoding";
            this.labelCoding.Size = new System.Drawing.Size(62, 13);
            this.labelCoding.TabIndex = 4;
            this.labelCoding.Text = "Кодировка";
            // 
            // textBoxSkipLast
            // 
            this.textBoxSkipLast.Location = new System.Drawing.Point(127, 55);
            this.textBoxSkipLast.Name = "textBoxSkipLast";
            this.textBoxSkipLast.Size = new System.Drawing.Size(30, 20);
            this.textBoxSkipLast.TabIndex = 3;
            this.textBoxSkipLast.Text = "0";
            this.textBoxSkipLast.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSkipLast
            // 
            this.labelSkipLast.AutoSize = true;
            this.labelSkipLast.Location = new System.Drawing.Point(10, 58);
            this.labelSkipLast.Name = "labelSkipLast";
            this.labelSkipLast.Size = new System.Drawing.Size(182, 13);
            this.labelSkipLast.TabIndex = 2;
            this.labelSkipLast.Text = "Не читать последние               байт";
            // 
            // textBoxSkipFirst
            // 
            this.textBoxSkipFirst.Location = new System.Drawing.Point(121, 23);
            this.textBoxSkipFirst.Name = "textBoxSkipFirst";
            this.textBoxSkipFirst.Size = new System.Drawing.Size(30, 20);
            this.textBoxSkipFirst.TabIndex = 1;
            this.textBoxSkipFirst.Text = "0";
            this.textBoxSkipFirst.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelSkipFirst
            // 
            this.labelSkipFirst.AutoSize = true;
            this.labelSkipFirst.Location = new System.Drawing.Point(10, 26);
            this.labelSkipFirst.Name = "labelSkipFirst";
            this.labelSkipFirst.Size = new System.Drawing.Size(175, 13);
            this.labelSkipFirst.TabIndex = 0;
            this.labelSkipFirst.Text = "Пропустить первые               байт";
            // 
            // buttonShowExtraParameters
            // 
            this.buttonShowExtraParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonShowExtraParameters.BackgroundImage = global::Notepad.Properties.Resources.showExtraParam;
            this.buttonShowExtraParameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonShowExtraParameters.Location = new System.Drawing.Point(217, 277);
            this.buttonShowExtraParameters.Name = "buttonShowExtraParameters";
            this.buttonShowExtraParameters.Size = new System.Drawing.Size(25, 40);
            this.buttonShowExtraParameters.TabIndex = 10;
            this.buttonShowExtraParameters.UseVisualStyleBackColor = true;
            this.buttonShowExtraParameters.MouseLeave += new System.EventHandler(this.buttonShowExtraParameters_MouseLeave);
            this.buttonShowExtraParameters.Click += new System.EventHandler(this.buttonParameters_Click);
            this.buttonShowExtraParameters.MouseEnter += new System.EventHandler(this.buttonShowExtraParameters_MouseEnter);
            // 
            // buttonHideExtraParameters
            // 
            this.buttonHideExtraParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHideExtraParameters.BackgroundImage = global::Notepad.Properties.Resources.hideEztraParam;
            this.buttonHideExtraParameters.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonHideExtraParameters.Location = new System.Drawing.Point(217, 277);
            this.buttonHideExtraParameters.Name = "buttonHideExtraParameters";
            this.buttonHideExtraParameters.Size = new System.Drawing.Size(25, 40);
            this.buttonHideExtraParameters.TabIndex = 13;
            this.buttonHideExtraParameters.UseVisualStyleBackColor = true;
            this.buttonHideExtraParameters.Visible = false;
            this.buttonHideExtraParameters.Click += new System.EventHandler(this.buttonParameters_Click);
            // 
            // buttonPack
            // 
            this.buttonPack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonPack.BackgroundImage = global::Notepad.Properties.Resources.Pack;
            this.buttonPack.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonPack.Location = new System.Drawing.Point(105, 277);
            this.buttonPack.Name = "buttonPack";
            this.buttonPack.Size = new System.Drawing.Size(40, 40);
            this.buttonPack.TabIndex = 6;
            this.buttonPack.UseVisualStyleBackColor = true;
            this.buttonPack.MouseLeave += new System.EventHandler(this.buttonPack_MouseLeave);
            this.buttonPack.Click += new System.EventHandler(this.buttonPack_Click);
            this.buttonPack.MouseEnter += new System.EventHandler(this.buttonPack_MouseEnter);
            // 
            // ZLibForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(658, 326);
            this.Controls.Add(this.buttonShowExtraParameters);
            this.Controls.Add(this.buttonHideExtraParameters);
            this.Controls.Add(this.groupBoxExtraParameters);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonUnpack);
            this.Controls.Add(this.textBoxText);
            this.Controls.Add(this.buttonPack);
            this.Controls.Add(this.labelText);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxPath);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 350);
            this.Name = "ZLibForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Работа с zlib архивами";
            this.Click += new System.EventHandler(this.buttonParameters_Click);
            this.groupBoxExtraParameters.ResumeLayout(false);
            this.groupBoxExtraParameters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.Label labelText;
        private System.Windows.Forms.Button buttonPack;
        private System.Windows.Forms.TextBox textBoxText;
        private System.Windows.Forms.Button buttonUnpack;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonShowExtraParameters;
        private System.Windows.Forms.GroupBox groupBoxExtraParameters;
        private System.Windows.Forms.TextBox textBoxSkipFirst;
        private System.Windows.Forms.Label labelSkipFirst;
        private System.Windows.Forms.ComboBox comboBoxCoding;
        private System.Windows.Forms.Label labelCoding;
        private System.Windows.Forms.TextBox textBoxSkipLast;
        private System.Windows.Forms.Label labelSkipLast;
        private System.Windows.Forms.Button buttonFindTheEnd;
        private System.Windows.Forms.Label labelFindTheEnd;
        private System.Windows.Forms.Button buttonHideExtraParameters;
        private System.Windows.Forms.ToolTip toolTip;
    }
}