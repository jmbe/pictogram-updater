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
            this.installTextLabel = new System.Windows.Forms.Label();
            this.changeDestinationLink = new System.Windows.Forms.LinkLabel();
            this.destinationLabel = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // installTextLabel
            // 
            this.installTextLabel.AutoSize = true;
            this.installTextLabel.Location = new System.Drawing.Point(0, 7);
            this.installTextLabel.Margin = new System.Windows.Forms.Padding(0);
            this.installTextLabel.Name = "installTextLabel";
            this.installTextLabel.Size = new System.Drawing.Size(66, 13);
            this.installTextLabel.TabIndex = 3;
            this.installTextLabel.Text = "Installeras till";
            // 
            // changeDestinationLink
            // 
            this.changeDestinationLink.AutoSize = true;
            this.changeDestinationLink.Location = new System.Drawing.Point(173, 7);
            this.changeDestinationLink.Name = "changeDestinationLink";
            this.changeDestinationLink.Size = new System.Drawing.Size(35, 13);
            this.changeDestinationLink.TabIndex = 7;
            this.changeDestinationLink.TabStop = true;
            this.changeDestinationLink.Text = "Ändra";
            this.changeDestinationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changeDestinationLink_LinkClicked);
            // 
            // destinationLabel
            // 
            this.destinationLabel.AutoSize = true;
            this.destinationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.destinationLabel.Location = new System.Drawing.Point(66, 7);
            this.destinationLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this.destinationLabel.Name = "destinationLabel";
            this.destinationLabel.Size = new System.Drawing.Size(101, 13);
            this.destinationLabel.TabIndex = 6;
            this.destinationLabel.Text = "C:\\Picto\\WmfSV";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.installTextLabel);
            this.flowLayoutPanel1.Controls.Add(this.destinationLabel);
            this.flowLayoutPanel1.Controls.Add(this.changeDestinationLink);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(380, 26);
            this.flowLayoutPanel1.TabIndex = 8;
            // 
            // DirectoryChooserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "DirectoryChooserInfo";
            this.Size = new System.Drawing.Size(380, 26);
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
