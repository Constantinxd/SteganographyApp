
namespace Steganography
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialogImage = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialogMessageFile = new System.Windows.Forms.OpenFileDialog();
            this.groupBoxMenuHide = new System.Windows.Forms.GroupBox();
            this.buttonHide = new System.Windows.Forms.Button();
            this.groupBoxHideSettings = new System.Windows.Forms.GroupBox();
            this.labelHideSettingsSize = new System.Windows.Forms.Label();
            this.labelHideSettingsMaxSize = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBoxHideMessageMethod = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxHideMessage = new System.Windows.Forms.GroupBox();
            this.textBoxHideMessageText = new System.Windows.Forms.TextBox();
            this.buttonHideMessageFile = new System.Windows.Forms.Button();
            this.textBoxHideMessageFile = new System.Windows.Forms.TextBox();
            this.radioButtonHideMessageText = new System.Windows.Forms.RadioButton();
            this.radioButtonHideMessageFile = new System.Windows.Forms.RadioButton();
            this.groupBoxHideImage = new System.Windows.Forms.GroupBox();
            this.buttonHideImageOpenFile = new System.Windows.Forms.Button();
            this.textBoxHideImage = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxMenuExtract = new System.Windows.Forms.GroupBox();
            this.buttonExtract = new System.Windows.Forms.Button();
            this.groupBoxExtractMessage = new System.Windows.Forms.GroupBox();
            this.textBoxExtractMessageText = new System.Windows.Forms.TextBox();
            this.groupBoxExtractImage = new System.Windows.Forms.GroupBox();
            this.buttonExtractImageOpenFile = new System.Windows.Forms.Button();
            this.textBoxExtractImage = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonMenuHide = new System.Windows.Forms.Button();
            this.buttonMenuExtract = new System.Windows.Forms.Button();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBoxMenuHide.SuspendLayout();
            this.groupBoxHideSettings.SuspendLayout();
            this.groupBoxHideMessage.SuspendLayout();
            this.groupBoxHideImage.SuspendLayout();
            this.groupBoxMenuExtract.SuspendLayout();
            this.groupBoxExtractMessage.SuspendLayout();
            this.groupBoxExtractImage.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogImage
            // 
            this.openFileDialogImage.Filter = "All Supported Image Files|*.bmp;*.png;*.gif|BMP Files (*.bmp)|*.bmp|PNG Files (*." +
    "png)|*.png|GIF Files (*.gif)|*.gif";
            // 
            // openFileDialogMessageFile
            // 
            this.openFileDialogMessageFile.Filter = "All Files (*.*)|*.*";
            // 
            // groupBoxMenuHide
            // 
            this.groupBoxMenuHide.Controls.Add(this.buttonHide);
            this.groupBoxMenuHide.Controls.Add(this.groupBoxHideSettings);
            this.groupBoxMenuHide.Controls.Add(this.groupBoxHideMessage);
            this.groupBoxMenuHide.Controls.Add(this.groupBoxHideImage);
            this.groupBoxMenuHide.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxMenuHide.Location = new System.Drawing.Point(12, 73);
            this.groupBoxMenuHide.Name = "groupBoxMenuHide";
            this.groupBoxMenuHide.Size = new System.Drawing.Size(537, 472);
            this.groupBoxMenuHide.TabIndex = 0;
            this.groupBoxMenuHide.TabStop = false;
            this.groupBoxMenuHide.Text = "Сокрытие информации";
            // 
            // buttonHide
            // 
            this.buttonHide.Location = new System.Drawing.Point(162, 429);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(200, 33);
            this.buttonHide.TabIndex = 8;
            this.buttonHide.Text = "Сокрыть информацию";
            this.buttonHide.UseVisualStyleBackColor = true;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // groupBoxHideSettings
            // 
            this.groupBoxHideSettings.Controls.Add(this.labelHideSettingsSize);
            this.groupBoxHideSettings.Controls.Add(this.labelHideSettingsMaxSize);
            this.groupBoxHideSettings.Controls.Add(this.label5);
            this.groupBoxHideSettings.Controls.Add(this.label4);
            this.groupBoxHideSettings.Controls.Add(this.comboBoxHideMessageMethod);
            this.groupBoxHideSettings.Controls.Add(this.label2);
            this.groupBoxHideSettings.Location = new System.Drawing.Point(6, 320);
            this.groupBoxHideSettings.Name = "groupBoxHideSettings";
            this.groupBoxHideSettings.Size = new System.Drawing.Size(525, 103);
            this.groupBoxHideSettings.TabIndex = 2;
            this.groupBoxHideSettings.TabStop = false;
            this.groupBoxHideSettings.Text = "Параметры";
            // 
            // labelHideSettingsSize
            // 
            this.labelHideSettingsSize.AutoSize = true;
            this.labelHideSettingsSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelHideSettingsSize.Location = new System.Drawing.Point(134, 76);
            this.labelHideSettingsSize.Name = "labelHideSettingsSize";
            this.labelHideSettingsSize.Size = new System.Drawing.Size(44, 15);
            this.labelHideSettingsSize.TabIndex = 5;
            this.labelHideSettingsSize.Text = "0 байт.";
            // 
            // labelHideSettingsMaxSize
            // 
            this.labelHideSettingsMaxSize.AutoSize = true;
            this.labelHideSettingsMaxSize.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.labelHideSettingsMaxSize.Location = new System.Drawing.Point(262, 50);
            this.labelHideSettingsMaxSize.Name = "labelHideSettingsMaxSize";
            this.labelHideSettingsMaxSize.Size = new System.Drawing.Size(44, 15);
            this.labelHideSettingsMaxSize.TabIndex = 4;
            this.labelHideSettingsMaxSize.Text = "0 байт.";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(8, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 15);
            this.label5.TabIndex = 3;
            this.label5.Text = "Размер сообщения: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(8, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(232, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "Макс. размер скрываемого сообщения: ";
            // 
            // comboBoxHideMessageMethod
            // 
            this.comboBoxHideMessageMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHideMessageMethod.Enabled = false;
            this.comboBoxHideMessageMethod.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBoxHideMessageMethod.FormattingEnabled = true;
            this.comboBoxHideMessageMethod.Location = new System.Drawing.Point(180, 17);
            this.comboBoxHideMessageMethod.Name = "comboBoxHideMessageMethod";
            this.comboBoxHideMessageMethod.Size = new System.Drawing.Size(77, 23);
            this.comboBoxHideMessageMethod.TabIndex = 7;
            this.comboBoxHideMessageMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxHideMessageMethod_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(8, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "Стеганографический метод: ";
            // 
            // groupBoxHideMessage
            // 
            this.groupBoxHideMessage.Controls.Add(this.textBoxHideMessageText);
            this.groupBoxHideMessage.Controls.Add(this.buttonHideMessageFile);
            this.groupBoxHideMessage.Controls.Add(this.textBoxHideMessageFile);
            this.groupBoxHideMessage.Controls.Add(this.radioButtonHideMessageText);
            this.groupBoxHideMessage.Controls.Add(this.radioButtonHideMessageFile);
            this.groupBoxHideMessage.Location = new System.Drawing.Point(6, 109);
            this.groupBoxHideMessage.Name = "groupBoxHideMessage";
            this.groupBoxHideMessage.Size = new System.Drawing.Size(525, 208);
            this.groupBoxHideMessage.TabIndex = 1;
            this.groupBoxHideMessage.TabStop = false;
            this.groupBoxHideMessage.Text = "Сообщение";
            // 
            // textBoxHideMessageText
            // 
            this.textBoxHideMessageText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxHideMessageText.Location = new System.Drawing.Point(6, 110);
            this.textBoxHideMessageText.MaxLength = 4000000;
            this.textBoxHideMessageText.Multiline = true;
            this.textBoxHideMessageText.Name = "textBoxHideMessageText";
            this.textBoxHideMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxHideMessageText.Size = new System.Drawing.Size(444, 89);
            this.textBoxHideMessageText.TabIndex = 6;
            this.textBoxHideMessageText.TextChanged += new System.EventHandler(this.textBoxHideMessageText_TextChanged);
            // 
            // buttonHideMessageFile
            // 
            this.buttonHideMessageFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonHideMessageFile.Location = new System.Drawing.Point(456, 50);
            this.buttonHideMessageFile.Name = "buttonHideMessageFile";
            this.buttonHideMessageFile.Size = new System.Drawing.Size(31, 23);
            this.buttonHideMessageFile.TabIndex = 4;
            this.buttonHideMessageFile.Text = "...";
            this.buttonHideMessageFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonHideMessageFile.UseVisualStyleBackColor = true;
            this.buttonHideMessageFile.Click += new System.EventHandler(this.buttonHideMessageFile_Click);
            // 
            // textBoxHideMessageFile
            // 
            this.textBoxHideMessageFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxHideMessageFile.Location = new System.Drawing.Point(6, 50);
            this.textBoxHideMessageFile.Name = "textBoxHideMessageFile";
            this.textBoxHideMessageFile.ReadOnly = true;
            this.textBoxHideMessageFile.Size = new System.Drawing.Size(444, 23);
            this.textBoxHideMessageFile.TabIndex = 6;
            // 
            // radioButtonHideMessageText
            // 
            this.radioButtonHideMessageText.AutoSize = true;
            this.radioButtonHideMessageText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonHideMessageText.Location = new System.Drawing.Point(6, 85);
            this.radioButtonHideMessageText.Name = "radioButtonHideMessageText";
            this.radioButtonHideMessageText.Size = new System.Drawing.Size(54, 19);
            this.radioButtonHideMessageText.TabIndex = 5;
            this.radioButtonHideMessageText.Text = "Текст";
            this.radioButtonHideMessageText.UseVisualStyleBackColor = true;
            this.radioButtonHideMessageText.Click += new System.EventHandler(this.radioButtonHideMessageText_Click);
            // 
            // radioButtonHideMessageFile
            // 
            this.radioButtonHideMessageFile.AutoSize = true;
            this.radioButtonHideMessageFile.Checked = true;
            this.radioButtonHideMessageFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.radioButtonHideMessageFile.Location = new System.Drawing.Point(6, 25);
            this.radioButtonHideMessageFile.Name = "radioButtonHideMessageFile";
            this.radioButtonHideMessageFile.Size = new System.Drawing.Size(54, 19);
            this.radioButtonHideMessageFile.TabIndex = 5;
            this.radioButtonHideMessageFile.TabStop = true;
            this.radioButtonHideMessageFile.Text = "Файл";
            this.radioButtonHideMessageFile.UseVisualStyleBackColor = true;
            this.radioButtonHideMessageFile.Click += new System.EventHandler(this.radioButtonHideMessageFile_Click);
            // 
            // groupBoxHideImage
            // 
            this.groupBoxHideImage.Controls.Add(this.buttonHideImageOpenFile);
            this.groupBoxHideImage.Controls.Add(this.textBoxHideImage);
            this.groupBoxHideImage.Controls.Add(this.label1);
            this.groupBoxHideImage.Location = new System.Drawing.Point(6, 28);
            this.groupBoxHideImage.Name = "groupBoxHideImage";
            this.groupBoxHideImage.Size = new System.Drawing.Size(525, 75);
            this.groupBoxHideImage.TabIndex = 0;
            this.groupBoxHideImage.TabStop = false;
            this.groupBoxHideImage.Text = "Изображение";
            // 
            // buttonHideImageOpenFile
            // 
            this.buttonHideImageOpenFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonHideImageOpenFile.Location = new System.Drawing.Point(456, 43);
            this.buttonHideImageOpenFile.Name = "buttonHideImageOpenFile";
            this.buttonHideImageOpenFile.Size = new System.Drawing.Size(31, 23);
            this.buttonHideImageOpenFile.TabIndex = 3;
            this.buttonHideImageOpenFile.Text = "...";
            this.buttonHideImageOpenFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonHideImageOpenFile.UseVisualStyleBackColor = true;
            this.buttonHideImageOpenFile.Click += new System.EventHandler(this.buttonHideImageOpenFile_Click);
            // 
            // textBoxHideImage
            // 
            this.textBoxHideImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxHideImage.Location = new System.Drawing.Point(6, 43);
            this.textBoxHideImage.Name = "textBoxHideImage";
            this.textBoxHideImage.ReadOnly = true;
            this.textBoxHideImage.Size = new System.Drawing.Size(444, 23);
            this.textBoxHideImage.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Путь к изображению";
            // 
            // groupBoxMenuExtract
            // 
            this.groupBoxMenuExtract.Controls.Add(this.buttonExtract);
            this.groupBoxMenuExtract.Controls.Add(this.groupBoxExtractMessage);
            this.groupBoxMenuExtract.Controls.Add(this.groupBoxExtractImage);
            this.groupBoxMenuExtract.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.groupBoxMenuExtract.Location = new System.Drawing.Point(571, 73);
            this.groupBoxMenuExtract.Name = "groupBoxMenuExtract";
            this.groupBoxMenuExtract.Size = new System.Drawing.Size(537, 472);
            this.groupBoxMenuExtract.TabIndex = 1;
            this.groupBoxMenuExtract.TabStop = false;
            this.groupBoxMenuExtract.Text = "Извлечение информации";
            // 
            // buttonExtract
            // 
            this.buttonExtract.Location = new System.Drawing.Point(162, 429);
            this.buttonExtract.Name = "buttonExtract";
            this.buttonExtract.Size = new System.Drawing.Size(200, 33);
            this.buttonExtract.TabIndex = 10;
            this.buttonExtract.Text = "Извлечь информацию";
            this.buttonExtract.UseVisualStyleBackColor = true;
            this.buttonExtract.Click += new System.EventHandler(this.buttonExtract_Click);
            // 
            // groupBoxExtractMessage
            // 
            this.groupBoxExtractMessage.Controls.Add(this.textBoxExtractMessageText);
            this.groupBoxExtractMessage.Location = new System.Drawing.Point(6, 109);
            this.groupBoxExtractMessage.Name = "groupBoxExtractMessage";
            this.groupBoxExtractMessage.Size = new System.Drawing.Size(525, 314);
            this.groupBoxExtractMessage.TabIndex = 1;
            this.groupBoxExtractMessage.TabStop = false;
            this.groupBoxExtractMessage.Text = "Сообщение";
            // 
            // textBoxExtractMessageText
            // 
            this.textBoxExtractMessageText.Enabled = false;
            this.textBoxExtractMessageText.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxExtractMessageText.Location = new System.Drawing.Point(6, 28);
            this.textBoxExtractMessageText.MaxLength = 4000000;
            this.textBoxExtractMessageText.Multiline = true;
            this.textBoxExtractMessageText.Name = "textBoxExtractMessageText";
            this.textBoxExtractMessageText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxExtractMessageText.Size = new System.Drawing.Size(513, 270);
            this.textBoxExtractMessageText.TabIndex = 0;
            // 
            // groupBoxExtractImage
            // 
            this.groupBoxExtractImage.Controls.Add(this.buttonExtractImageOpenFile);
            this.groupBoxExtractImage.Controls.Add(this.textBoxExtractImage);
            this.groupBoxExtractImage.Controls.Add(this.label3);
            this.groupBoxExtractImage.Location = new System.Drawing.Point(6, 28);
            this.groupBoxExtractImage.Name = "groupBoxExtractImage";
            this.groupBoxExtractImage.Size = new System.Drawing.Size(525, 75);
            this.groupBoxExtractImage.TabIndex = 0;
            this.groupBoxExtractImage.TabStop = false;
            this.groupBoxExtractImage.Text = "Изображение";
            // 
            // buttonExtractImageOpenFile
            // 
            this.buttonExtractImageOpenFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonExtractImageOpenFile.Location = new System.Drawing.Point(456, 43);
            this.buttonExtractImageOpenFile.Name = "buttonExtractImageOpenFile";
            this.buttonExtractImageOpenFile.Size = new System.Drawing.Size(31, 23);
            this.buttonExtractImageOpenFile.TabIndex = 9;
            this.buttonExtractImageOpenFile.Text = "...";
            this.buttonExtractImageOpenFile.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.buttonExtractImageOpenFile.UseVisualStyleBackColor = true;
            this.buttonExtractImageOpenFile.Click += new System.EventHandler(this.buttonExtractImageOpenFile_Click);
            // 
            // textBoxExtractImage
            // 
            this.textBoxExtractImage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBoxExtractImage.Location = new System.Drawing.Point(6, 43);
            this.textBoxExtractImage.Name = "textBoxExtractImage";
            this.textBoxExtractImage.ReadOnly = true;
            this.textBoxExtractImage.Size = new System.Drawing.Size(444, 23);
            this.textBoxExtractImage.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(8, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(123, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "Путь к изображению";
            // 
            // buttonMenuHide
            // 
            this.buttonMenuHide.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonMenuHide.Location = new System.Drawing.Point(12, 27);
            this.buttonMenuHide.Name = "buttonMenuHide";
            this.buttonMenuHide.Size = new System.Drawing.Size(269, 40);
            this.buttonMenuHide.TabIndex = 0;
            this.buttonMenuHide.Text = "Сокрыть";
            this.buttonMenuHide.UseVisualStyleBackColor = true;
            this.buttonMenuHide.Click += new System.EventHandler(this.buttonMenuHide_Click);
            // 
            // buttonMenuExtract
            // 
            this.buttonMenuExtract.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.buttonMenuExtract.Location = new System.Drawing.Point(281, 27);
            this.buttonMenuExtract.Name = "buttonMenuExtract";
            this.buttonMenuExtract.Size = new System.Drawing.Size(268, 40);
            this.buttonMenuExtract.TabIndex = 1;
            this.buttonMenuExtract.Text = "Извлечь";
            this.buttonMenuExtract.UseVisualStyleBackColor = true;
            this.buttonMenuExtract.Click += new System.EventHandler(this.buttonMenuExtract_Click);
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuHelp});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1118, 24);
            this.mainMenuStrip.TabIndex = 4;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // toolStripMenuHelp
            // 
            this.toolStripMenuHelp.Name = "toolStripMenuHelp";
            this.toolStripMenuHelp.Size = new System.Drawing.Size(68, 20);
            this.toolStripMenuHelp.Text = "Помощь";
            this.toolStripMenuHelp.Click += new System.EventHandler(this.toolStripMenuHelp_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1118, 551);
            this.Controls.Add(this.buttonMenuExtract);
            this.Controls.Add(this.buttonMenuHide);
            this.Controls.Add(this.groupBoxMenuExtract);
            this.Controls.Add(this.groupBoxMenuHide);
            this.Controls.Add(this.mainMenuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.mainMenuStrip;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1134, 590);
            this.MinimumSize = new System.Drawing.Size(575, 590);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SteganographyApp";
            this.TransparencyKey = System.Drawing.SystemColors.ActiveBorder;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxMenuHide.ResumeLayout(false);
            this.groupBoxHideSettings.ResumeLayout(false);
            this.groupBoxHideSettings.PerformLayout();
            this.groupBoxHideMessage.ResumeLayout(false);
            this.groupBoxHideMessage.PerformLayout();
            this.groupBoxHideImage.ResumeLayout(false);
            this.groupBoxHideImage.PerformLayout();
            this.groupBoxMenuExtract.ResumeLayout(false);
            this.groupBoxExtractMessage.ResumeLayout(false);
            this.groupBoxExtractMessage.PerformLayout();
            this.groupBoxExtractImage.ResumeLayout(false);
            this.groupBoxExtractImage.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialogImage;
        private System.Windows.Forms.OpenFileDialog openFileDialogMessageFile;
        private System.Windows.Forms.GroupBox groupBoxMenuHide;
        private System.Windows.Forms.GroupBox groupBoxHideMessage;
        private System.Windows.Forms.TextBox textBoxHideMessageText;
        private System.Windows.Forms.Button buttonHideMessageFile;
        private System.Windows.Forms.TextBox textBoxHideMessageFile;
        private System.Windows.Forms.RadioButton radioButtonHideMessageText;
        private System.Windows.Forms.RadioButton radioButtonHideMessageFile;
        private System.Windows.Forms.GroupBox groupBoxHideImage;
        private System.Windows.Forms.Button buttonHideImageOpenFile;
        private System.Windows.Forms.TextBox textBoxHideImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxHideSettings;
        private System.Windows.Forms.ComboBox comboBoxHideMessageMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBoxMenuExtract;
        private System.Windows.Forms.GroupBox groupBoxExtractImage;
        private System.Windows.Forms.TextBox textBoxExtractImage;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonExtractImageOpenFile;
        private System.Windows.Forms.GroupBox groupBoxExtractMessage;
        private System.Windows.Forms.Button buttonHide;
        private System.Windows.Forms.Button buttonExtract;
        private System.Windows.Forms.TextBox textBoxExtractMessageText;
        private System.Windows.Forms.Label labelHideSettingsSize;
        private System.Windows.Forms.Label labelHideSettingsMaxSize;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonMenuHide;
        private System.Windows.Forms.Button buttonMenuExtract;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuHelp;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

