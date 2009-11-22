namespace PictogramUpdater
{
    partial class PictogramInstallerForm
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
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.verifyLabel = new System.Windows.Forms.LinkLabel();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.soundCheckbox = new System.Windows.Forms.CheckBox();
            this.clearTextCheckbox = new System.Windows.Forms.CheckBox();
            this.updateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.languagesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.overwriteCheckbox = new System.Windows.Forms.CheckBox();
            this.logTextbox = new System.Windows.Forms.TextBox();
            this.installButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.zipButton = new System.Windows.Forms.Button();
            this.getZipUrlButton = new System.Windows.Forms.Button();
            this.soundDirectoryChooser = new PictogramUpdater.DirectoryChooser();
            this.plainTextDirectoryChooser = new PictogramUpdater.DirectoryChooser();
            this.wmfDirectoryChooser = new PictogramUpdater.DirectoryChooser();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // usernameTextbox
            // 
            this.usernameTextbox.Location = new System.Drawing.Point(91, 17);
            this.usernameTextbox.Name = "usernameTextbox";
            this.usernameTextbox.Size = new System.Drawing.Size(100, 20);
            this.usernameTextbox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Anv�ndarnamn";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verifyLabel);
            this.groupBox1.Controls.Add(this.passwordTextbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.usernameTextbox);
            this.groupBox1.Location = new System.Drawing.Point(12, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 176);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inloggningsuppgifter";
            // 
            // verifyLabel
            // 
            this.verifyLabel.AutoSize = true;
            this.verifyLabel.Location = new System.Drawing.Point(134, 66);
            this.verifyLabel.Name = "verifyLabel";
            this.verifyLabel.Size = new System.Drawing.Size(57, 13);
            this.verifyLabel.TabIndex = 6;
            this.verifyLabel.TabStop = true;
            this.verifyLabel.Text = "Kontrollera";
            this.verifyLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.VerifyLabel_LinkClicked);
            // 
            // passwordTextbox
            // 
            this.passwordTextbox.Location = new System.Drawing.Point(91, 39);
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.Size = new System.Drawing.Size(100, 20);
            this.passwordTextbox.TabIndex = 5;
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "L�senord";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 524);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(786, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(669, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "Klar";
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            this.statusProgressBar.Size = new System.Drawing.Size(100, 16);
            this.statusProgressBar.Click += new System.EventHandler(this.StatusProgressBar_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.soundDirectoryChooser);
            this.groupBox2.Controls.Add(this.overwriteCheckbox);
            this.groupBox2.Controls.Add(this.soundCheckbox);
            this.groupBox2.Controls.Add(this.plainTextDirectoryChooser);
            this.groupBox2.Controls.Add(this.clearTextCheckbox);
            this.groupBox2.Controls.Add(this.wmfDirectoryChooser);
            this.groupBox2.Controls.Add(this.updateLinkLabel);
            this.groupBox2.Controls.Add(this.languagesComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(326, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(448, 176);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pictogram";
            // 
            // soundCheckbox
            // 
            this.soundCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.soundCheckbox.AutoSize = true;
            this.soundCheckbox.Location = new System.Drawing.Point(10, 121);
            this.soundCheckbox.Name = "soundCheckbox";
            this.soundCheckbox.Size = new System.Drawing.Size(87, 17);
            this.soundCheckbox.TabIndex = 20;
            this.soundCheckbox.Text = "Installera ljud";
            this.soundCheckbox.UseVisualStyleBackColor = true;
            // 
            // clearTextCheckbox
            // 
            this.clearTextCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.clearTextCheckbox.AutoSize = true;
            this.clearTextCheckbox.Location = new System.Drawing.Point(10, 66);
            this.clearTextCheckbox.Name = "clearTextCheckbox";
            this.clearTextCheckbox.Size = new System.Drawing.Size(161, 17);
            this.clearTextCheckbox.TabIndex = 14;
            this.clearTextCheckbox.Text = "Installera pictobilder i klartext";
            this.clearTextCheckbox.UseVisualStyleBackColor = true;
            // 
            // updateLinkLabel
            // 
            this.updateLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateLinkLabel.AutoSize = true;
            this.updateLinkLabel.Location = new System.Drawing.Point(367, 20);
            this.updateLinkLabel.Name = "updateLinkLabel";
            this.updateLinkLabel.Size = new System.Drawing.Size(57, 13);
            this.updateLinkLabel.TabIndex = 1;
            this.updateLinkLabel.TabStop = true;
            this.updateLinkLabel.Text = "Uppdatera";
            this.updateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateLinkLabel_LinkClicked);
            // 
            // languagesComboBox
            // 
            this.languagesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.languagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languagesComboBox.FormattingEnabled = true;
            this.languagesComboBox.Location = new System.Drawing.Point(59, 15);
            this.languagesComboBox.Name = "languagesComboBox";
            this.languagesComboBox.Size = new System.Drawing.Size(302, 21);
            this.languagesComboBox.TabIndex = 0;
            this.languagesComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_Change);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Spr�k";
            // 
            // overwriteCheckbox
            // 
            this.overwriteCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.overwriteCheckbox.AutoSize = true;
            this.overwriteCheckbox.Location = new System.Drawing.Point(243, 66);
            this.overwriteCheckbox.Name = "overwriteCheckbox";
            this.overwriteCheckbox.Size = new System.Drawing.Size(147, 17);
            this.overwriteCheckbox.TabIndex = 4;
            this.overwriteCheckbox.Text = "Skriv �ver befintliga bilder";
            this.overwriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // logTextbox
            // 
            this.logTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextbox.BackColor = System.Drawing.Color.White;
            this.logTextbox.Location = new System.Drawing.Point(12, 244);
            this.logTextbox.Multiline = true;
            this.logTextbox.Name = "logTextbox";
            this.logTextbox.ReadOnly = true;
            this.logTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextbox.Size = new System.Drawing.Size(762, 248);
            this.logTextbox.TabIndex = 7;
            this.logTextbox.TabStop = false;
            this.logTextbox.Text = "1. Fyll i anv�ndarnamn och l�senord.\r\n2. Klicka p� Bl�ddra f�r att v�lja m�lkatal" +
                "og.\r\n3. Klicka p� Installera f�r att ladda ner pictogram.";
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.Location = new System.Drawing.Point(699, 498);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(75, 23);
            this.installButton.TabIndex = 8;
            this.installButton.Text = "&Installera";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // zipButton
            // 
            this.zipButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.zipButton.Location = new System.Drawing.Point(608, 498);
            this.zipButton.Name = "zipButton";
            this.zipButton.Size = new System.Drawing.Size(85, 23);
            this.zipButton.TabIndex = 9;
            this.zipButton.Text = "&Ladda ner zip";
            this.zipButton.UseVisualStyleBackColor = true;
            this.zipButton.Click += new System.EventHandler(this.ZipButton_Click);
            // 
            // getZipUrlButton
            // 
            this.getZipUrlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.getZipUrlButton.Location = new System.Drawing.Point(504, 498);
            this.getZipUrlButton.Name = "getZipUrlButton";
            this.getZipUrlButton.Size = new System.Drawing.Size(98, 23);
            this.getZipUrlButton.TabIndex = 10;
            this.getZipUrlButton.Text = "&H�mta zip-URL";
            this.getZipUrlButton.UseVisualStyleBackColor = true;
            this.getZipUrlButton.Click += new System.EventHandler(this.GetZipUrlButton_Click);
            // 
            // soundDirectoryChooser
            // 
            this.soundDirectoryChooser.DisplayDirectoryInput = false;
            this.soundDirectoryChooser.InstallPath = "C:\\Picto\\WmfSV";
            this.soundDirectoryChooser.Location = new System.Drawing.Point(10, 144);
            this.soundDirectoryChooser.Name = "soundDirectoryChooser";
            this.soundDirectoryChooser.Size = new System.Drawing.Size(380, 26);
            this.soundDirectoryChooser.TabIndex = 13;
            // 
            // plainTextDirectoryChooser
            // 
            this.plainTextDirectoryChooser.DisplayDirectoryInput = false;
            this.plainTextDirectoryChooser.InstallPath = "C:\\Picto\\WmfSV";
            this.plainTextDirectoryChooser.Location = new System.Drawing.Point(10, 89);
            this.plainTextDirectoryChooser.Name = "plainTextDirectoryChooser";
            this.plainTextDirectoryChooser.Size = new System.Drawing.Size(380, 26);
            this.plainTextDirectoryChooser.TabIndex = 12;
            // 
            // wmfDirectoryChooser
            // 
            this.wmfDirectoryChooser.DisplayDirectoryInput = false;
            this.wmfDirectoryChooser.InstallPath = "C:\\Picto";
            this.wmfDirectoryChooser.Location = new System.Drawing.Point(10, 42);
            this.wmfDirectoryChooser.Name = "wmfDirectoryChooser";
            this.wmfDirectoryChooser.Size = new System.Drawing.Size(394, 29);
            this.wmfDirectoryChooser.TabIndex = 7;
            // 
            // PictogramInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(786, 546);
            this.Controls.Add(this.getZipUrlButton);
            this.Controls.Add(this.zipButton);
            this.Controls.Add(this.logTextbox);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox1);
            this.Name = "PictogramInstallerForm";
            this.Text = "Pictograminstalleraren";
            this.Load += new System.EventHandler(this.PictogramInstallerForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox usernameTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox passwordTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar statusProgressBar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox logTextbox;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.LinkLabel verifyLabel;
        private System.Windows.Forms.ComboBox languagesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.LinkLabel updateLinkLabel;
        private System.Windows.Forms.CheckBox overwriteCheckbox;
        private System.Windows.Forms.Button zipButton;
        private System.Windows.Forms.Button getZipUrlButton;
        private System.Windows.Forms.CheckBox clearTextCheckbox;
        private System.Windows.Forms.CheckBox soundCheckbox;
        private DirectoryChooser wmfDirectoryChooser;
        private DirectoryChooser plainTextDirectoryChooser;
        private DirectoryChooser soundDirectoryChooser;
    }
}

