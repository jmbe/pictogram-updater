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
            this.directoryOkButton = new System.Windows.Forms.Button();
            this.directoryBrowseButton = new System.Windows.Forms.Button();
            this.directoryLabel = new System.Windows.Forms.Label();
            this.directoryTextbox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // directoryOkButton
            // 
            this.directoryOkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryOkButton.Location = new System.Drawing.Point(343, 1);
            this.directoryOkButton.Name = "directoryOkButton";
            this.directoryOkButton.Size = new System.Drawing.Size(34, 23);
            this.directoryOkButton.TabIndex = 30;
            this.directoryOkButton.Text = "OK";
            this.directoryOkButton.UseVisualStyleBackColor = true;
            this.directoryOkButton.Click += new System.EventHandler(this.directoryOkButton_Click);
            // 
            // directoryBrowseButton
            // 
            this.directoryBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryBrowseButton.Location = new System.Drawing.Point(262, 1);
            this.directoryBrowseButton.Name = "directoryBrowseButton";
            this.directoryBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.directoryBrowseButton.TabIndex = 29;
            this.directoryBrowseButton.Text = "&Bläddra ...";
            this.directoryBrowseButton.UseVisualStyleBackColor = true;
            this.directoryBrowseButton.Click += new System.EventHandler(this.directoryBrowseButton_Click);
            // 
            // directoryLabel
            // 
            this.directoryLabel.AutoSize = true;
            this.directoryLabel.Location = new System.Drawing.Point(0, 6);
            this.directoryLabel.Name = "directoryLabel";
            this.directoryLabel.Size = new System.Drawing.Size(43, 13);
            this.directoryLabel.TabIndex = 27;
            this.directoryLabel.Text = "Katalog";
            // 
            // directoryTextbox
            // 
            this.directoryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.directoryTextbox.Location = new System.Drawing.Point(52, 3);
            this.directoryTextbox.Name = "directoryTextbox";
            this.directoryTextbox.Size = new System.Drawing.Size(204, 20);
            this.directoryTextbox.TabIndex = 28;
            this.directoryTextbox.TextChanged += new System.EventHandler(this.directoryTextbox_TextChanged);
            // 
            // DirectoryChooserInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.directoryOkButton);
            this.Controls.Add(this.directoryBrowseButton);
            this.Controls.Add(this.directoryLabel);
            this.Controls.Add(this.directoryTextbox);
            this.Name = "DirectoryChooserInput";
            this.Size = new System.Drawing.Size(380, 26);
            this.VisibleChanged += new System.EventHandler(this.DirectoryChooserInput_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button directoryOkButton;
        private System.Windows.Forms.Button directoryBrowseButton;
        private System.Windows.Forms.Label directoryLabel;
        private System.Windows.Forms.TextBox directoryTextbox;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
    }
}
