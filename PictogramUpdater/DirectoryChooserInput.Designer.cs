namespace PictogramUpdater {
    partial class DirectoryChooserInput {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoryChooserInput));
            this.directoryOkButton = new System.Windows.Forms.Button();
            this.directoryBrowseButton = new System.Windows.Forms.Button();
            this.directoryTextbox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // directoryOkButton
            // 
            resources.ApplyResources(this.directoryOkButton, "directoryOkButton");
            this.directoryOkButton.Name = "directoryOkButton";
            this.directoryOkButton.UseVisualStyleBackColor = true;
            this.directoryOkButton.Click += new System.EventHandler(this.directoryOkButton_Click);
            // 
            // directoryBrowseButton
            // 
            resources.ApplyResources(this.directoryBrowseButton, "directoryBrowseButton");
            this.directoryBrowseButton.Name = "directoryBrowseButton";
            this.directoryBrowseButton.UseVisualStyleBackColor = true;
            this.directoryBrowseButton.Click += new System.EventHandler(this.directoryBrowseButton_Click);
            // 
            // directoryTextbox
            // 
            resources.ApplyResources(this.directoryTextbox, "directoryTextbox");
            this.directoryTextbox.Name = "directoryTextbox";
            this.directoryTextbox.TextChanged += new System.EventHandler(this.directoryTextbox_TextChanged);
            // 
            // folderBrowserDialog
            // 
            resources.ApplyResources(this.folderBrowserDialog, "folderBrowserDialog");
            // 
            // DirectoryChooserInput
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.directoryOkButton);
            this.Controls.Add(this.directoryBrowseButton);
            this.Controls.Add(this.directoryTextbox);
            this.Name = "DirectoryChooserInput";
            this.VisibleChanged += new System.EventHandler(this.DirectoryChooserInput_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button directoryOkButton;
        private System.Windows.Forms.Button directoryBrowseButton;
        private System.Windows.Forms.TextBox directoryTextbox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}
