namespace DownloadManager
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
            this.overwriteCheckbox = new System.Windows.Forms.CheckBox();
            this.updateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.directoryBrowseButton = new System.Windows.Forms.Button();
            this.languagesComboBox = new System.Windows.Forms.ComboBox();
            this.directoryLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.directoryPathTextbox = new System.Windows.Forms.TextBox();
            this.directoryPathLabel = new System.Windows.Forms.Label();
            this.logTextbox = new System.Windows.Forms.TextBox();
            this.installButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.zipButton = new System.Windows.Forms.Button();
            this.getZipUrlButton = new System.Windows.Forms.Button();
            this.changeInstallPathSelectionLinkLabel = new System.Windows.Forms.LinkLabel();
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
            this.label2.Text = "Användarnamn";
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
            this.groupBox1.Size = new System.Drawing.Size(201, 86);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inloggningsuppgifter";
            // 
            // verifyLabel
            // 
            this.verifyLabel.AutoSize = true;
            this.verifyLabel.Location = new System.Drawing.Point(134, 62);
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
            this.label3.Text = "Lösenord";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgressBar});
            this.statusStrip.Location = new System.Drawing.Point(0, 298);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(511, 22);
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(394, 17);
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
            this.groupBox2.Controls.Add(this.overwriteCheckbox);
            this.groupBox2.Controls.Add(this.updateLinkLabel);
            this.groupBox2.Controls.Add(this.languagesComboBox);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.directoryBrowseButton);
            this.groupBox2.Controls.Add(this.directoryLabel);
            this.groupBox2.Controls.Add(this.directoryPathTextbox);
            this.groupBox2.Controls.Add(this.changeInstallPathSelectionLinkLabel);
            this.groupBox2.Controls.Add(this.directoryPathLabel);
            this.groupBox2.Location = new System.Drawing.Point(219, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(280, 86);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Pictogram";
            // 
            // overwriteCheckbox
            // 
            this.overwriteCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.overwriteCheckbox.AutoSize = true;
            this.overwriteCheckbox.Location = new System.Drawing.Point(10, 63);
            this.overwriteCheckbox.Name = "overwriteCheckbox";
            this.overwriteCheckbox.Size = new System.Drawing.Size(147, 17);
            this.overwriteCheckbox.TabIndex = 4;
            this.overwriteCheckbox.Text = "Skriv över befintliga bilder";
            this.overwriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // updateLinkLabel
            // 
            this.updateLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updateLinkLabel.AutoSize = true;
            this.updateLinkLabel.Location = new System.Drawing.Point(199, 20);
            this.updateLinkLabel.Name = "updateLinkLabel";
            this.updateLinkLabel.Size = new System.Drawing.Size(57, 13);
            this.updateLinkLabel.TabIndex = 1;
            this.updateLinkLabel.TabStop = true;
            this.updateLinkLabel.Text = "Uppdatera";
            this.updateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateLinkLabel_LinkClicked);
            // 
            // directoryBrowseButton
            // 
            this.directoryBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryBrowseButton.Location = new System.Drawing.Point(199, 36);
            this.directoryBrowseButton.Name = "directoryBrowseButton";
            this.directoryBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.directoryBrowseButton.TabIndex = 3;
            this.directoryBrowseButton.Text = "&Bläddra ...";
            this.directoryBrowseButton.UseVisualStyleBackColor = true;
            this.directoryBrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
            // 
            // languagesComboBox
            // 
            this.languagesComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.languagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languagesComboBox.FormattingEnabled = true;
            this.languagesComboBox.Location = new System.Drawing.Point(59, 15);
            this.languagesComboBox.Name = "languagesComboBox";
            this.languagesComboBox.Size = new System.Drawing.Size(134, 21);
            this.languagesComboBox.TabIndex = 0;
            this.languagesComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_Change);
            // 
            // directoryLabel
            // 
            this.directoryLabel.AutoSize = true;
            this.directoryLabel.Location = new System.Drawing.Point(6, 42);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(43, 13);
            this.directoryLabel.TabIndex = 2;
            this.directoryLabel.Text = "Katalog";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Språk";
            // 
            // directoryPathTextbox
            // 
            this.directoryPathTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryPathTextbox.Location = new System.Drawing.Point(59, 39);
            this.directoryPathTextbox.Name = "directoryPathTextbox";
            this.directoryPathTextbox.Size = new System.Drawing.Size(134, 20);
            this.directoryPathTextbox.TabIndex = 2;
            this.directoryPathTextbox.TextChanged += new System.EventHandler(this.DirectoryPathTextbox_Changed);
            // 
            // directoryPathLabel
            // 
            this.directoryPathLabel.AutoSize = true;
            this.directoryPathLabel.Location = new System.Drawing.Point(7, 42);
            this.directoryPathLabel.Name = "directoryPathLabel";
            this.directoryPathLabel.Size = new System.Drawing.Size(66, 13);
            this.directoryPathLabel.TabIndex = 12;
            this.directoryPathLabel.Text = "Installeras till";
            this.directoryPathLabel.Visible = false;
            // 
            // logTextbox
            // 
            this.logTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.logTextbox.BackColor = System.Drawing.Color.White;
            this.logTextbox.Location = new System.Drawing.Point(12, 154);
            this.logTextbox.Multiline = true;
            this.logTextbox.Name = "logTextbox";
            this.logTextbox.ReadOnly = true;
            this.logTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextbox.Size = new System.Drawing.Size(487, 112);
            this.logTextbox.TabIndex = 7;
            this.logTextbox.TabStop = false;
            this.logTextbox.Text = "1. Fyll i användarnamn och lösenord.\r\n2. Klicka på Bläddra för att välja målkatal" +
                "og.\r\n3. Klicka på Installera för att ladda ner pictogram.";
            // 
            // installButton
            // 
            this.installButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.installButton.Location = new System.Drawing.Point(424, 272);
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
            this.zipButton.Location = new System.Drawing.Point(333, 272);
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
            this.getZipUrlButton.Location = new System.Drawing.Point(229, 272);
            this.getZipUrlButton.Name = "getZipUrlButton";
            this.getZipUrlButton.Size = new System.Drawing.Size(98, 23);
            this.getZipUrlButton.TabIndex = 10;
            this.getZipUrlButton.Text = "&Hämta zip-URL";
            this.getZipUrlButton.UseVisualStyleBackColor = true;
            this.getZipUrlButton.Click += new System.EventHandler(this.GetZipUrlButton_Click);
            // 
            // changeInstallPathSelectionLinkLabel
            // 
            this.changeInstallPathSelectionLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.changeInstallPathSelectionLinkLabel.AutoSize = true;
            this.changeInstallPathSelectionLinkLabel.Location = new System.Drawing.Point(199, 42);
            this.changeInstallPathSelectionLinkLabel.Name = "changeInstallPathSelectionLinkLabel";
            this.changeInstallPathSelectionLinkLabel.Size = new System.Drawing.Size(35, 13);
            this.changeInstallPathSelectionLinkLabel.TabIndex = 13;
            this.changeInstallPathSelectionLinkLabel.TabStop = true;
            this.changeInstallPathSelectionLinkLabel.Text = "Ändra";
            this.changeInstallPathSelectionLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.ChangeInstallPathSelectionLabel_LinkClicked);
            // 
            // PictogramInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(511, 320);
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
        private System.Windows.Forms.Button directoryBrowseButton;
        private System.Windows.Forms.ComboBox languagesComboBox;
        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox directoryPathTextbox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.LinkLabel updateLinkLabel;
        private System.Windows.Forms.CheckBox overwriteCheckbox;
        private System.Windows.Forms.Button zipButton;
        private System.Windows.Forms.Button getZipUrlButton;
        private System.Windows.Forms.Label directoryPathLabel;
        private System.Windows.Forms.LinkLabel changeInstallPathSelectionLinkLabel;
    }
}

