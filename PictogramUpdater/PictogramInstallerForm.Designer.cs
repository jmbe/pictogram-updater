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
            this.languagesComboBox = new System.Windows.Forms.ComboBox();
            this.languageLabel = new System.Windows.Forms.Label();
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
            this.formatComboBox = new System.Windows.Forms.ComboBox();
            this.formatLabel = new System.Windows.Forms.Label();
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
            resources.ApplyResources(this.usernameTextbox, "usernameTextbox");
            this.usernameTextbox.Name = "usernameTextbox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verifyLabel);
            this.groupBox1.Controls.Add(this.passwordTextbox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.usernameTextbox);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // verifyLabel
            // 
            resources.ApplyResources(this.verifyLabel, "verifyLabel");
            this.verifyLabel.Name = "verifyLabel";
            this.verifyLabel.TabStop = true;
            this.verifyLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.VerifyLabel_LinkClicked);
            // 
            // passwordTextbox
            // 
            resources.ApplyResources(this.passwordTextbox, "passwordTextbox");
            this.passwordTextbox.Name = "passwordTextbox";
            this.passwordTextbox.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.statusProgressBar});
            resources.ApplyResources(this.statusStrip, "statusStrip");
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.SizingGrip = false;
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            resources.ApplyResources(this.statusLabel, "statusLabel");
            this.statusLabel.Spring = true;
            // 
            // statusProgressBar
            // 
            this.statusProgressBar.Name = "statusProgressBar";
            resources.ApplyResources(this.statusProgressBar, "statusProgressBar");
            this.statusProgressBar.Click += new System.EventHandler(this.StatusProgressBar_Click);
            // 
            // overwriteCheckbox
            // 
            resources.ApplyResources(this.overwriteCheckbox, "overwriteCheckbox");
            this.overwriteCheckbox.Name = "overwriteCheckbox";
            this.overwriteCheckbox.UseVisualStyleBackColor = true;
            // 
            // soundCheckbox
            // 
            resources.ApplyResources(this.soundCheckbox, "soundCheckbox");
            this.soundCheckbox.Name = "soundCheckbox";
            this.soundCheckbox.UseVisualStyleBackColor = true;
            // 
            // plainTextCheckbox
            // 
            resources.ApplyResources(this.plainTextCheckbox, "plainTextCheckbox");
            this.plainTextCheckbox.Name = "plainTextCheckbox";
            this.plainTextCheckbox.UseVisualStyleBackColor = true;
            // 
            // languagesComboBox
            // 
            this.languagesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.languagesComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.languagesComboBox, "languagesComboBox");
            this.languagesComboBox.Name = "languagesComboBox";
            this.languagesComboBox.SelectedIndexChanged += new System.EventHandler(this.LanguageComboBox_Change);
            // 
            // languageLabel
            // 
            resources.ApplyResources(this.languageLabel, "languageLabel");
            this.languageLabel.Name = "languageLabel";
            // 
            // logTextbox
            // 
            this.logTextbox.BackColor = System.Drawing.Color.White;
            this.logTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.logTextbox, "logTextbox");
            this.logTextbox.Name = "logTextbox";
            this.logTextbox.ReadOnly = true;
            this.logTextbox.TabStop = false;
            // 
            // installButton
            // 
            resources.ApplyResources(this.installButton, "installButton");
            this.installButton.Name = "installButton";
            this.installButton.UseVisualStyleBackColor = true;
            this.installButton.Click += new System.EventHandler(this.InstallButton_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // contactLink
            // 
            resources.ApplyResources(this.contactLink, "contactLink");
            this.contactLink.Name = "contactLink";
            this.contactLink.TabStop = true;
            this.contactLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.contactLink_LinkClicked);
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.contactLink);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // versionLabel
            // 
            resources.ApplyResources(this.versionLabel, "versionLabel");
            this.versionLabel.Name = "versionLabel";
            // 
            // flowLayoutPanel2
            // 
            resources.ApplyResources(this.flowLayoutPanel2, "flowLayoutPanel2");
            this.flowLayoutPanel2.Controls.Add(this.versionLabel);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label5);
            this.flowLayoutPanel3.Controls.Add(this.exitLink);
            resources.ApplyResources(this.flowLayoutPanel3, "flowLayoutPanel3");
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // exitLink
            // 
            resources.ApplyResources(this.exitLink, "exitLink");
            this.exitLink.Name = "exitLink";
            this.exitLink.TabStop = true;
            this.exitLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.exitLink_LinkClicked);
            // 
            // formatComboBox
            // 
            this.formatComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatComboBox.FormattingEnabled = true;
            resources.ApplyResources(this.formatComboBox, "formatComboBox");
            this.formatComboBox.Name = "formatComboBox";
            this.formatComboBox.SelectedIndexChanged += new System.EventHandler(this.formatComboBox_SelectedIndexChanged);
            // 
            // formatLabel
            // 
            resources.ApplyResources(this.formatLabel, "formatLabel");
            this.formatLabel.Name = "formatLabel";
            // 
            // wmfDirectoryChooser
            // 
            this.wmfDirectoryChooser.DisplayDirectoryInput = false;
            this.wmfDirectoryChooser.InstallPath = "C:\\Picto";
            resources.ApplyResources(this.wmfDirectoryChooser, "wmfDirectoryChooser");
            this.wmfDirectoryChooser.Name = "wmfDirectoryChooser";
            // 
            // plainTextDirectoryChooser
            // 
            this.plainTextDirectoryChooser.DisplayDirectoryInput = false;
            this.plainTextDirectoryChooser.InstallPath = "C:\\Picto\\WmfSV";
            resources.ApplyResources(this.plainTextDirectoryChooser, "plainTextDirectoryChooser");
            this.plainTextDirectoryChooser.Name = "plainTextDirectoryChooser";
            // 
            // soundDirectoryChooser
            // 
            this.soundDirectoryChooser.DisplayDirectoryInput = false;
            this.soundDirectoryChooser.InstallPath = "C:\\Picto\\WmfSV";
            resources.ApplyResources(this.soundDirectoryChooser, "soundDirectoryChooser");
            this.soundDirectoryChooser.Name = "soundDirectoryChooser";
            // 
            // PictogramInstallerForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.formatLabel);
            this.Controls.Add(this.formatComboBox);
            this.Controls.Add(this.flowLayoutPanel3);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.overwriteCheckbox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.languagesComboBox);
            this.Controls.Add(this.languageLabel);
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
            this.Name = "PictogramInstallerForm";
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
        private System.Windows.Forms.Label languageLabel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
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
        private System.Windows.Forms.ComboBox formatComboBox;
        private System.Windows.Forms.Label formatLabel;
    }
}

