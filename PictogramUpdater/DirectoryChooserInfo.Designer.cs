namespace PictogramUpdater {
    partial class DirectoryChooserInfo {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DirectoryChooserInfo));
            this.installTextLabel = new System.Windows.Forms.Label();
            this.changeDestinationLink = new System.Windows.Forms.LinkLabel();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // installTextLabel
            // 
            resources.ApplyResources(this.installTextLabel, "installTextLabel");
            this.installTextLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.installTextLabel.Name = "installTextLabel";
            // 
            // changeDestinationLink
            // 
            resources.ApplyResources(this.changeDestinationLink, "changeDestinationLink");
            this.changeDestinationLink.Name = "changeDestinationLink";
            this.changeDestinationLink.TabStop = true;
            this.changeDestinationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changeDestinationLink_LinkClicked);
            // 
            // destinationLabel
            // 
            resources.ApplyResources(this.destinationLabel, "destinationLabel");
            this.destinationLabel.Name = "destinationLabel";
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.Controls.Add(this.installTextLabel);
            this.flowLayoutPanel1.Controls.Add(this.destinationLabel);
            this.flowLayoutPanel1.Controls.Add(this.changeDestinationLink);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // DirectoryChooserInfo
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DirectoryChooserInfo";
            this.Load += new System.EventHandler(this.DirectoryChooserInfo_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label installTextLabel;
        private System.Windows.Forms.LinkLabel changeDestinationLink;
        private System.Windows.Forms.Label destinationLabel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}
