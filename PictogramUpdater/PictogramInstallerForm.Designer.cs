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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PictogramInstallerForm));
            this.usernameTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.verifyLabel = new System.Windows.Forms.LinkLabel();
            this.passwordTextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.overwriteCheckbox = new System.Windows.Forms.CheckBox();
            this.soundCheckbox = new System.Windows.Forms.CheckBox();
            this.plainTextCheckbox = new System.Windows.Forms.CheckBox();
            this.updateLinkLabel = new System.Windows.Forms.LinkLabel();
            this.languagesComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logTextbox = new System.Windows.Forms.TextBox();
            this.installButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label4 = new System.Windows.Forms.Label();
            this.contactLink = new System.Windows.Forms.LinkLabel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.versionLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.exitLink = new System.Windows.Forms.LinkLabel();
            this.wmfDirectoryChooser = new PictogramUpdater.DirectoryChooser();
            this.plainTextDirectoryChooser = new PictogramUpdater.DirectoryChooser();
            this.soundDirectoryChooser = new PictogramUpdater.DirectoryChooser();
            this.groupBox1.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.flowLayoutPanel3.SuspendLayout();
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
            this.groupBox1.Location = new System.Drawing.Point(31, 84);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(263, 89);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Inloggningsuppgifter till Picto Online";
            // 
            // verifyLabel
            // 
            this.verifyLabel.AutoSize = true;
            this.verifyLabel.Location = new System.Drawing.Point(88, 62);
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
            this.statusStrip.Location = new System.Drawing.Point(0, 516);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(854, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 5;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(737, 17);
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
            // overwriteCheckbox
            // 
            this.overwriteCheckbox.AutoSize = true;
            this.overwriteCheckbox.Location = new System.Drawing.Point(12, 5);
            this.overwriteCheckbox.Name = "overwriteCheckbox";
            this.overwriteCheckbox.Size = new System.Drawing.Size(147, 17);
            this.overwriteCheckbox.TabIndex = 4;
            this.overwriteCheckbox.Text = "Skriv över befintliga bilder";
            this.overwriteCheckbox.UseVisualStyleBackColor = true;
            this.overwriteCheckbox.Visible = false;
            // 
            // soundCheckbox
            // 
            this.soundCheckbox.AutoSize = true;
            this.soundCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.soundCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.soundCheckbox.Location = new System.Drawing.Point(487, 301);
            this.soundCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.soundCheckbox.Name = "soundCheckbox";
            this.soundCheckbox.Size = new System.Drawing.Size(155, 29);
            this.soundCheckbox.TabIndex = 20;
            this.soundCheckbox.Text = "Installera ljud";
            this.soundCheckbox.UseVisualStyleBackColor = true;
            // 
            // plainTextCheckbox
            // 
            this.plainTextCheckbox.AutoSize = true;
            this.plainTextCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.plainTextCheckbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.plainTextCheckbox.Location = new System.Drawing.Point(487, 221);
            this.plainTextCheckbox.Margin = new System.Windows.Forms.Padding(0);
            this.plainTextCheckbox.Name = "plainTextCheckbox";
            this.plainTextCheckbox.Size = new System.Drawing.Size(307, 29);
            this.plainTextCheckbox.TabIndex = 14;
            this.plainTextCheckbox.Text = "Installera pictobilder i klartext";
            this.plainTextCheckbox.UseVisualStyleBackColor = true;
            // 
            // updateLinkLabel
            // 
            this.updateLinkLabel.AutoSize = true;
            this.updateLinkLabel.Location = new System.Drawing.Point(713, 103);
            this.updateLinkLabel.Name = "updateLinkLabel";
            this.updateLinkLabel.Size = new System.Drawing.Size(57, 13);
            this.updateLinkLabel.TabIndex = 1;
            this.updateLinkLabel.TabStop = true;
            this.updateLinkLabel.Text = "Uppdatera";
            this.updateLinkLabel.Visible = false;
            this.updateLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.UpdateLinkLabel_LinkClicked);
            // 
            // languagesComboBox
            // 
            this.languagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languagesComboBox.FormattingEnabled = true;
            this.languagesComboBox.Location = new System.Drawing.Point(508, 100);
            this.languagesComboBox.MinimumSize = new System.Drawing.Size(140, 0);
            this.languagesComboBox.Name = "languagesComboBox";
            this.languagesComboBox.Size = new System.Drawing.Size(199, 21);
            this.languagesComboBox.TabIndex = 0;
            this.languagesComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_Change);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(503, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Välj språk";
            // 
            // logTextbox
            // 
            this.logTextbox.BackColor = System.Drawing.Color.White;
            this.logTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logTextbox.Location = new System.Drawing.Point(12, 388);
            this.logTextbox.Multiline = true;
            this.logTextbox.Name = "logTextbox";
            this.logTextbox.ReadOnly = true;
            this.logTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTextbox.Size = new System.Drawing.Size(449, 116);
            this.logTextbox.TabIndex = 7;
            this.logTextbox.TabStop = false;
            // 
            // installButton
            // 
            this.installButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.installButton.Location = new System.Drawing.Point(508, 420);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(164, 45);
            this.installButton.TabIndex = 8;
            this.installButton.Text = "&Installera";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(503, 148);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(204, 25);
            this.label4.TabIndex = 11;
            this.label4.Text = "Installera pictobilder";
            // 
            // contactLink
            // 
            this.contactLink.AutoSize = true;
            this.contactLink.Location = new System.Drawing.Point(127, 0);
            this.contactLink.Name = "contactLink";
            this.contactLink.Size = new System.Drawing.Size(171, 13);
            this.contactLink.TabIndex = 22;
            this.contactLink.TabStop = true;
            this.contactLink.Text = "http://www.pictogram.se/kontakt/";
            this.contactLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.contactLink_LinkClicked);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.contactLink);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(548, 491);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(301, 22);
            this.flowLayoutPanel1.TabIndex = 23;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(55, 0);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(78, 13);
            this.versionLabel.TabIndex = 24;
            this.versionLabel.Text = "Version 3.0.0.7";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.versionLabel);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(713, 5);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(136, 20);
            this.flowLayoutPanel2.TabIndex = 25;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(449, 321);
            this.pictureBox1.TabIndex = 26;
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label5);
            this.flowLayoutPanel3.Controls.Add(this.exitLink);
            this.flowLayoutPanel3.Location = new System.Drawing.Point(678, 432);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(125, 33);
            this.flowLayoutPanel3.TabIndex = 27;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "eller";
            // 
            // exitLink
            // 
            this.exitLink.AutoSize = true;
            this.exitLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exitLink.Location = new System.Drawing.Point(38, 0);
            this.exitLink.Margin = new System.Windows.Forms.Padding(0);
            this.exitLink.Name = "exitLink";
            this.exitLink.Size = new System.Drawing.Size(59, 20);
            this.exitLink.TabIndex = 1;
            this.exitLink.TabStop = true;
            this.exitLink.Text = "avsluta";
            this.exitLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.exitLink_LinkClicked);
            // 
            // wmfDirectoryChooser
            // 
            this.wmfDirectoryChooser.DisplayDirectoryInput = false;
            this.wmfDirectoryChooser.InstallPath = "C:\\Picto";
            this.wmfDirectoryChooser.Location = new System.Drawing.Point(508, 176);
            this.wmfDirectoryChooser.Name = "wmfDirectoryChooser";
            this.wmfDirectoryChooser.Size = new System.Drawing.Size(394, 29);
            this.wmfDirectoryChooser.TabIndex = 7;
            // 
            // plainTextDirectoryChooser
            // 
            this.plainTextDirectoryChooser.DisplayDirectoryInput = false;
            this.plainTextDirectoryChooser.InstallPath = "C:\\Picto\\WmfSV";
            this.plainTextDirectoryChooser.Location = new System.Drawing.Point(508, 253);
            this.plainTextDirectoryChooser.Name = "plainTextDirectoryChooser";
            this.plainTextDirectoryChooser.Size = new System.Drawing.Size(380, 26);
            this.plainTextDirectoryChooser.TabIndex = 12;
            // 
            // soundDirectoryChooser
            // 
            this.soundDirectoryChooser.DisplayDirectoryInput = false;
            this.soundDirectoryChooser.InstallPath = "C:\\Picto\\WmfSV";
            this.soundDirectoryChooser.Location = new System.Drawing.Point(508, 333);
            this.soundDirectoryChooser.Name = "soundDirectoryChooser";
            this.soundDirectoryChooser.Size = new System.Drawing.Size(380, 26);
            this.soundDirectoryChooser.TabIndex = 13;
            // 
            // PictogramInstallerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(854, 538);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.overwriteCheckbox);
            this.Controls.Add(this.updateLinkLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.languagesComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.soundCheckbox);
            this.Controls.Add(this.logTextbox);
            this.Controls.Add(this.plainTextCheckbox);
            this.Controls.Add(this.installButton);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.wmfDirectoryChooser);
            this.Controls.Add(this.plainTextDirectoryChooser);
            this.Controls.Add(this.soundDirectoryChooser);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PictogramInstallerForm";
            this.Text = "Uppdatering Bildbas Pictogram";
            this.Load += new System.EventHandler(this.PictogramInstallerForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_Paint);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
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
        private System.Windows.Forms.TextBox logTextbox;
        private System.Windows.Forms.Button installButton;
        private System.Windows.Forms.LinkLabel verifyLabel;
        private System.Windows.Forms.ComboBox languagesComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.LinkLabel updateLinkLabel;
        private System.Windows.Forms.CheckBox overwriteCheckbox;
        private System.Windows.Forms.CheckBox plainTextCheckbox;
        private System.Windows.Forms.CheckBox soundCheckbox;
        private DirectoryChooser wmfDirectoryChooser;
        private DirectoryChooser plainTextDirectoryChooser;
        private DirectoryChooser soundDirectoryChooser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel contactLink;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label versionLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel exitLink;
    }
}

