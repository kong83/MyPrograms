namespace SurgeryHelper.Forms
{
    partial class VisitViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VisitViewForm));
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.textBoxDiagnose = new System.Windows.Forms.TextBox();
            this.textBoxRecommendation = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxComments = new System.Windows.Forms.TextBox();
            this.buttonCards = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dateTimePickerVisitDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxEvenly = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonWordExportConsultation = new System.Windows.Forms.Button();
            this.checkBoxLastParagraph = new System.Windows.Forms.CheckBox();
            this.buttonWordExportInformedConsent = new System.Windows.Forms.Button();
            this.buttonWordExportContract = new System.Windows.Forms.Button();
            this.comboBoxDoctor = new System.Windows.Forms.ComboBox();
            this.linkLabelDoctor = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxHeader = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.dateTimePickerPassInfoDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.textBoxPassInfoOrganization = new System.Windows.Forms.TextBox();
            this.textBoxPassInfoSubdivisionCode = new System.Windows.Forms.TextBox();
            this.textBoxPassInfoNumber = new System.Windows.Forms.TextBox();
            this.textBoxPassInfoSeries = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.comboBoxAdditionalDocuments = new System.Windows.Forms.ComboBox();
            this.buttonAdditionalDocument = new System.Windows.Forms.Button();
            this.checkBoxLastParagraphOdkb = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.BackgroundImage = global::SurgeryHelper.Properties.Resources.OK;
            this.buttonOk.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Location = new System.Drawing.Point(636, 413);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(40, 40);
            this.buttonOk.TabIndex = 58;
            this.buttonOk.TabStop = false;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.MouseLeave += new System.EventHandler(this.buttonOk_MouseLeave);
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            this.buttonOk.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonOk.MouseEnter += new System.EventHandler(this.buttonOk_MouseEnter);
            // 
            // buttonClose
            // 
            this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonClose.BackgroundImage = global::SurgeryHelper.Properties.Resources.close;
            this.buttonClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonClose.FlatAppearance.BorderSize = 0;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(682, 413);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(40, 40);
            this.buttonClose.TabIndex = 60;
            this.buttonClose.TabStop = false;
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.MouseLeave += new System.EventHandler(this.buttonClose_MouseLeave);
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            this.buttonClose.MouseEnter += new System.EventHandler(this.buttonClose_MouseEnter);
            // 
            // textBoxDiagnose
            // 
            this.textBoxDiagnose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxDiagnose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxDiagnose.Location = new System.Drawing.Point(102, 39);
            this.textBoxDiagnose.MaxLength = 20000000;
            this.textBoxDiagnose.Multiline = true;
            this.textBoxDiagnose.Name = "textBoxDiagnose";
            this.textBoxDiagnose.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDiagnose.Size = new System.Drawing.Size(620, 46);
            this.textBoxDiagnose.TabIndex = 3;
            this.textBoxDiagnose.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxDiagnose.Enter += new System.EventHandler(this.box_Focused);
            // 
            // textBoxRecommendation
            // 
            this.textBoxRecommendation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRecommendation.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxRecommendation.Location = new System.Drawing.Point(102, 143);
            this.textBoxRecommendation.MaxLength = 20000000;
            this.textBoxRecommendation.Multiline = true;
            this.textBoxRecommendation.Name = "textBoxRecommendation";
            this.textBoxRecommendation.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRecommendation.Size = new System.Drawing.Size(620, 45);
            this.textBoxRecommendation.TabIndex = 7;
            this.textBoxRecommendation.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxRecommendation.Enter += new System.EventHandler(this.box_Focused);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 84;
            this.label1.Text = "* Диагноз";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 209);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "Комментарии";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 86;
            this.label3.Text = "Рекомендации";
            // 
            // textBoxComments
            // 
            this.textBoxComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxComments.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxComments.Location = new System.Drawing.Point(102, 194);
            this.textBoxComments.MaxLength = 20000000;
            this.textBoxComments.Multiline = true;
            this.textBoxComments.Name = "textBoxComments";
            this.textBoxComments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxComments.Size = new System.Drawing.Size(620, 45);
            this.textBoxComments.TabIndex = 9;
            this.textBoxComments.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxComments.Enter += new System.EventHandler(this.box_Focused);
            // 
            // buttonCards
            // 
            this.buttonCards.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCards.BackgroundImage = global::SurgeryHelper.Properties.Resources.two_cards;
            this.buttonCards.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonCards.FlatAppearance.BorderSize = 0;
            this.buttonCards.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCards.Location = new System.Drawing.Point(636, 367);
            this.buttonCards.Name = "buttonCards";
            this.buttonCards.Size = new System.Drawing.Size(40, 40);
            this.buttonCards.TabIndex = 56;
            this.buttonCards.TabStop = false;
            this.buttonCards.UseVisualStyleBackColor = true;
            this.buttonCards.MouseLeave += new System.EventHandler(this.buttonCards_MouseLeave);
            this.buttonCards.Click += new System.EventHandler(this.buttonCards_Click);
            this.buttonCards.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonCards.MouseEnter += new System.EventHandler(this.buttonCards_MouseEnter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 26);
            this.label4.TabIndex = 90;
            this.label4.Text = "* Дата\r\n   консультации";
            // 
            // dateTimePickerVisitDate
            // 
            this.dateTimePickerVisitDate.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateTimePickerVisitDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerVisitDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerVisitDate.Location = new System.Drawing.Point(102, 13);
            this.dateTimePickerVisitDate.Name = "dateTimePickerVisitDate";
            this.dateTimePickerVisitDate.Size = new System.Drawing.Size(144, 20);
            this.dateTimePickerVisitDate.TabIndex = 1;
            this.dateTimePickerVisitDate.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            this.dateTimePickerVisitDate.Enter += new System.EventHandler(this.box_Focused);
            // 
            // textBoxEvenly
            // 
            this.textBoxEvenly.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEvenly.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxEvenly.Location = new System.Drawing.Point(102, 91);
            this.textBoxEvenly.MaxLength = 20000000;
            this.textBoxEvenly.Multiline = true;
            this.textBoxEvenly.Name = "textBoxEvenly";
            this.textBoxEvenly.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxEvenly.Size = new System.Drawing.Size(620, 46);
            this.textBoxEvenly.TabIndex = 5;
            this.textBoxEvenly.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.textBoxEvenly.Enter += new System.EventHandler(this.box_Focused);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 13);
            this.label5.TabIndex = 94;
            this.label5.Text = "Объективно";
            // 
            // buttonWordExportConsultation
            // 
            this.buttonWordExportConsultation.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWordExportConsultation.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonWordExportConsultation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonWordExportConsultation.FlatAppearance.BorderSize = 0;
            this.buttonWordExportConsultation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWordExportConsultation.Location = new System.Drawing.Point(102, 373);
            this.buttonWordExportConsultation.Name = "buttonWordExportConsultation";
            this.buttonWordExportConsultation.Size = new System.Drawing.Size(95, 34);
            this.buttonWordExportConsultation.TabIndex = 50;
            this.buttonWordExportConsultation.TabStop = false;
            this.buttonWordExportConsultation.Text = "          Справка";
            this.buttonWordExportConsultation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonWordExportConsultation.UseVisualStyleBackColor = true;
            this.buttonWordExportConsultation.MouseLeave += new System.EventHandler(this.buttonWordExportConsultation_MouseLeave);
            this.buttonWordExportConsultation.Click += new System.EventHandler(this.buttonWordExportConsultation_Click);
            this.buttonWordExportConsultation.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonWordExportConsultation.MouseEnter += new System.EventHandler(this.buttonWordExportConsultation_MouseEnter);
            // 
            // checkBoxLastParagraph
            // 
            this.checkBoxLastParagraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxLastParagraph.AutoSize = true;
            this.checkBoxLastParagraph.Location = new System.Drawing.Point(102, 347);
            this.checkBoxLastParagraph.Name = "checkBoxLastParagraph";
            this.checkBoxLastParagraph.Size = new System.Drawing.Size(261, 17);
            this.checkBoxLastParagraph.TabIndex = 23;
            this.checkBoxLastParagraph.Text = "Добавить в справку анализы для Соловьёвки";
            this.checkBoxLastParagraph.UseVisualStyleBackColor = true;
            this.checkBoxLastParagraph.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // buttonWordExportInformedConsent
            // 
            this.buttonWordExportInformedConsent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWordExportInformedConsent.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonWordExportInformedConsent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonWordExportInformedConsent.FlatAppearance.BorderSize = 0;
            this.buttonWordExportInformedConsent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWordExportInformedConsent.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonWordExportInformedConsent.Location = new System.Drawing.Point(203, 373);
            this.buttonWordExportInformedConsent.Name = "buttonWordExportInformedConsent";
            this.buttonWordExportInformedConsent.Size = new System.Drawing.Size(146, 34);
            this.buttonWordExportInformedConsent.TabIndex = 52;
            this.buttonWordExportInformedConsent.TabStop = false;
            this.buttonWordExportInformedConsent.Text = "          Информированное \r\n          согласие";
            this.buttonWordExportInformedConsent.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonWordExportInformedConsent.UseVisualStyleBackColor = true;
            this.buttonWordExportInformedConsent.MouseLeave += new System.EventHandler(this.buttonWordExportInformedConsent_MouseLeave);
            this.buttonWordExportInformedConsent.Click += new System.EventHandler(this.buttonWordExportInformedConsent_Click);
            this.buttonWordExportInformedConsent.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonWordExportInformedConsent.MouseEnter += new System.EventHandler(this.buttonWordExportInformedConsent_MouseEnter);
            // 
            // buttonWordExportContract
            // 
            this.buttonWordExportContract.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonWordExportContract.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonWordExportContract.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonWordExportContract.FlatAppearance.BorderSize = 0;
            this.buttonWordExportContract.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonWordExportContract.Location = new System.Drawing.Point(355, 373);
            this.buttonWordExportContract.Name = "buttonWordExportContract";
            this.buttonWordExportContract.Size = new System.Drawing.Size(195, 34);
            this.buttonWordExportContract.TabIndex = 54;
            this.buttonWordExportContract.TabStop = false;
            this.buttonWordExportContract.Text = "          Договор о возмездном \r\n          оказании медицинских услуг";
            this.buttonWordExportContract.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonWordExportContract.UseVisualStyleBackColor = true;
            this.buttonWordExportContract.MouseLeave += new System.EventHandler(this.buttonWordExportContract_MouseLeave);
            this.buttonWordExportContract.Click += new System.EventHandler(this.buttonWordExportContract_Click);
            this.buttonWordExportContract.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonWordExportContract.MouseEnter += new System.EventHandler(this.buttonWordExportContract_MouseEnter);
            // 
            // comboBoxDoctor
            // 
            this.comboBoxDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBoxDoctor.FormattingEnabled = true;
            this.comboBoxDoctor.Location = new System.Drawing.Point(102, 284);
            this.comboBoxDoctor.MaxDropDownItems = 20;
            this.comboBoxDoctor.Name = "comboBoxDoctor";
            this.comboBoxDoctor.Size = new System.Drawing.Size(242, 21);
            this.comboBoxDoctor.TabIndex = 21;
            this.comboBoxDoctor.TextChanged += new System.EventHandler(this.comboBox_TextChanged);
            // 
            // linkLabelDoctor
            // 
            this.linkLabelDoctor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabelDoctor.AutoSize = true;
            this.linkLabelDoctor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkLabelDoctor.Location = new System.Drawing.Point(9, 287);
            this.linkLabelDoctor.Name = "linkLabelDoctor";
            this.linkLabelDoctor.Size = new System.Drawing.Size(31, 13);
            this.linkLabelDoctor.TabIndex = 11;
            this.linkLabelDoctor.TabStop = true;
            this.linkLabelDoctor.Text = "Врач";
            this.linkLabelDoctor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDoctor_LinkClicked);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 316);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 26);
            this.label6.TabIndex = 95;
            this.label6.Text = "Шапка для \r\nсправки";
            // 
            // textBoxHeader
            // 
            this.textBoxHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxHeader.BackColor = System.Drawing.Color.WhiteSmoke;
            this.textBoxHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxHeader.Location = new System.Drawing.Point(102, 308);
            this.textBoxHeader.MaxLength = 20000000;
            this.textBoxHeader.Multiline = true;
            this.textBoxHeader.Name = "textBoxHeader";
            this.textBoxHeader.ReadOnly = true;
            this.textBoxHeader.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxHeader.Size = new System.Drawing.Size(620, 33);
            this.textBoxHeader.TabIndex = 96;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label16.Location = new System.Drawing.Point(9, 249);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(70, 26);
            this.label16.TabIndex = 98;
            this.label16.Text = "Паспортные\r\nданные";
            // 
            // label20
            // 
            this.label20.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(273, 244);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(73, 13);
            this.label20.TabIndex = 109;
            this.label20.Text = "Дата выдачи";
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(217, 244);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(24, 13);
            this.label19.TabIndex = 108;
            this.label19.Text = "к/п";
            // 
            // dateTimePickerPassInfoDeliveryDate
            // 
            this.dateTimePickerPassInfoDeliveryDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dateTimePickerPassInfoDeliveryDate.Checked = false;
            this.dateTimePickerPassInfoDeliveryDate.CustomFormat = "dd.MM.yyyy";
            this.dateTimePickerPassInfoDeliveryDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateTimePickerPassInfoDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerPassInfoDeliveryDate.Location = new System.Drawing.Point(262, 258);
            this.dateTimePickerPassInfoDeliveryDate.Name = "dateTimePickerPassInfoDeliveryDate";
            this.dateTimePickerPassInfoDeliveryDate.ShowCheckBox = true;
            this.dateTimePickerPassInfoDeliveryDate.Size = new System.Drawing.Size(98, 20);
            this.dateTimePickerPassInfoDeliveryDate.TabIndex = 17;
            this.dateTimePickerPassInfoDeliveryDate.ValueChanged += new System.EventHandler(this.dateTimePicker_ValueChanged);
            // 
            // textBoxPassInfoOrganization
            // 
            this.textBoxPassInfoOrganization.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPassInfoOrganization.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassInfoOrganization.Location = new System.Drawing.Point(366, 258);
            this.textBoxPassInfoOrganization.Name = "textBoxPassInfoOrganization";
            this.textBoxPassInfoOrganization.Size = new System.Drawing.Size(356, 20);
            this.textBoxPassInfoOrganization.TabIndex = 19;
            this.textBoxPassInfoOrganization.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxPassInfoSubdivisionCode
            // 
            this.textBoxPassInfoSubdivisionCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPassInfoSubdivisionCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassInfoSubdivisionCode.Location = new System.Drawing.Point(204, 258);
            this.textBoxPassInfoSubdivisionCode.Name = "textBoxPassInfoSubdivisionCode";
            this.textBoxPassInfoSubdivisionCode.Size = new System.Drawing.Size(52, 20);
            this.textBoxPassInfoSubdivisionCode.TabIndex = 15;
            this.textBoxPassInfoSubdivisionCode.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxPassInfoNumber
            // 
            this.textBoxPassInfoNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPassInfoNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassInfoNumber.Location = new System.Drawing.Point(146, 258);
            this.textBoxPassInfoNumber.Name = "textBoxPassInfoNumber";
            this.textBoxPassInfoNumber.Size = new System.Drawing.Size(52, 20);
            this.textBoxPassInfoNumber.TabIndex = 13;
            this.textBoxPassInfoNumber.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBoxPassInfoSeries
            // 
            this.textBoxPassInfoSeries.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxPassInfoSeries.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPassInfoSeries.Location = new System.Drawing.Point(102, 258);
            this.textBoxPassInfoSeries.Name = "textBoxPassInfoSeries";
            this.textBoxPassInfoSeries.Size = new System.Drawing.Size(38, 20);
            this.textBoxPassInfoSeries.TabIndex = 11;
            this.textBoxPassInfoSeries.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(492, 244);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 13);
            this.label17.TabIndex = 106;
            this.label17.Text = "Кем выдан";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(102, 244);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 105;
            this.label7.Text = "Серия";
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(153, 244);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 107;
            this.label18.Text = "Номер";
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonHelp.BackgroundImage = global::SurgeryHelper.Properties.Resources.help;
            this.buttonHelp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonHelp.Location = new System.Drawing.Point(100, 413);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.buttonHelp.Size = new System.Drawing.Size(40, 40);
            this.buttonHelp.TabIndex = 113;
            this.buttonHelp.TabStop = false;
            this.buttonHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonHelp.UseVisualStyleBackColor = true;
            this.buttonHelp.MouseLeave += new System.EventHandler(this.buttonHelp_MouseLeave);
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            this.buttonHelp.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonHelp.MouseEnter += new System.EventHandler(this.buttonHelp_MouseEnter);
            // 
            // comboBoxAdditionalDocuments
            // 
            this.comboBoxAdditionalDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxAdditionalDocuments.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxAdditionalDocuments.FormattingEnabled = true;
            this.comboBoxAdditionalDocuments.Location = new System.Drawing.Point(146, 424);
            this.comboBoxAdditionalDocuments.MaxDropDownItems = 20;
            this.comboBoxAdditionalDocuments.Name = "comboBoxAdditionalDocuments";
            this.comboBoxAdditionalDocuments.Size = new System.Drawing.Size(268, 21);
            this.comboBoxAdditionalDocuments.TabIndex = 110;
            // 
            // buttonAdditionalDocument
            // 
            this.buttonAdditionalDocument.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdditionalDocument.BackgroundImage = global::SurgeryHelper.Properties.Resources.Untitled;
            this.buttonAdditionalDocument.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonAdditionalDocument.FlatAppearance.BorderSize = 0;
            this.buttonAdditionalDocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdditionalDocument.Location = new System.Drawing.Point(420, 416);
            this.buttonAdditionalDocument.Name = "buttonAdditionalDocument";
            this.buttonAdditionalDocument.Size = new System.Drawing.Size(135, 34);
            this.buttonAdditionalDocument.TabIndex = 114;
            this.buttonAdditionalDocument.TabStop = false;
            this.buttonAdditionalDocument.Text = "          Другой документ";
            this.buttonAdditionalDocument.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdditionalDocument.UseVisualStyleBackColor = true;
            this.buttonAdditionalDocument.MouseLeave += new System.EventHandler(this.buttonAdditionalDocument_MouseLeave);
            this.buttonAdditionalDocument.Click += new System.EventHandler(this.buttonAdditionalDocument_Click);
            this.buttonAdditionalDocument.Enter += new System.EventHandler(this.button_DropFocus);
            this.buttonAdditionalDocument.MouseEnter += new System.EventHandler(this.buttonAdditionalDocument_MouseEnter);
            // 
            // checkBoxLastParagraphOdkb
            // 
            this.checkBoxLastParagraphOdkb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxLastParagraphOdkb.AutoSize = true;
            this.checkBoxLastParagraphOdkb.Location = new System.Drawing.Point(491, 347);
            this.checkBoxLastParagraphOdkb.Name = "checkBoxLastParagraphOdkb";
            this.checkBoxLastParagraphOdkb.Size = new System.Drawing.Size(231, 17);
            this.checkBoxLastParagraphOdkb.TabIndex = 115;
            this.checkBoxLastParagraphOdkb.Text = "Добавить в справку анализы для ОДКБ";
            this.checkBoxLastParagraphOdkb.UseVisualStyleBackColor = true;
            this.checkBoxLastParagraphOdkb.CheckedChanged += new System.EventHandler(this.checkBox_CheckedChanged);
            // 
            // VisitViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 457);
            this.Controls.Add(this.checkBoxLastParagraphOdkb);
            this.Controls.Add(this.buttonAdditionalDocument);
            this.Controls.Add(this.buttonHelp);
            this.Controls.Add(this.comboBoxAdditionalDocuments);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dateTimePickerPassInfoDeliveryDate);
            this.Controls.Add(this.textBoxHeader);
            this.Controls.Add(this.textBoxPassInfoOrganization);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxPassInfoSubdivisionCode);
            this.Controls.Add(this.comboBoxDoctor);
            this.Controls.Add(this.textBoxPassInfoNumber);
            this.Controls.Add(this.linkLabelDoctor);
            this.Controls.Add(this.textBoxPassInfoSeries);
            this.Controls.Add(this.buttonWordExportContract);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.buttonWordExportInformedConsent);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.checkBoxLastParagraph);
            this.Controls.Add(this.buttonWordExportConsultation);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxEvenly);
            this.Controls.Add(this.dateTimePickerVisitDate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonCards);
            this.Controls.Add(this.textBoxComments);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxDiagnose);
            this.Controls.Add(this.textBoxRecommendation);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonClose);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(709, 450);
            this.Name = "VisitViewForm";
            this.Text = "VisitViewForm";
            this.Load += new System.EventHandler(this.VisitViewForm_Load);
            this.SizeChanged += new System.EventHandler(this.VisitViewForm_SizeChanged);
            this.Activated += new System.EventHandler(this.VisitViewForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VisitViewForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.TextBox textBoxDiagnose;
        private System.Windows.Forms.TextBox textBoxRecommendation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxComments;
        private System.Windows.Forms.Button buttonCards;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dateTimePickerVisitDate;
        private System.Windows.Forms.TextBox textBoxEvenly;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonWordExportConsultation;
        private System.Windows.Forms.CheckBox checkBoxLastParagraph;
        private System.Windows.Forms.Button buttonWordExportInformedConsent;
        private System.Windows.Forms.Button buttonWordExportContract;
        private System.Windows.Forms.ComboBox comboBoxDoctor;
        private System.Windows.Forms.LinkLabel linkLabelDoctor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxHeader;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.DateTimePicker dateTimePickerPassInfoDeliveryDate;
        private System.Windows.Forms.TextBox textBoxPassInfoOrganization;
        private System.Windows.Forms.TextBox textBoxPassInfoSubdivisionCode;
        private System.Windows.Forms.TextBox textBoxPassInfoNumber;
        private System.Windows.Forms.TextBox textBoxPassInfoSeries;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.ComboBox comboBoxAdditionalDocuments;
        private System.Windows.Forms.Button buttonAdditionalDocument;
        private System.Windows.Forms.CheckBox checkBoxLastParagraphOdkb;
    }
}