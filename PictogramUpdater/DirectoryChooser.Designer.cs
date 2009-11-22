namespace PictogramUpdater {
    partial class DirectoryChooser {
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
            this.directoryChooserInfo = new PictogramUpdater.DirectoryChooserInfo();
            this.input = new PictogramUpdater.DirectoryChooserInput();
            this.SuspendLayout();
            // 
            // directoryChooserInfo
            // 
            this.directoryChooserInfo.InstallPath = "C:\\Picto\\WmfSV";
            this.directoryChooserInfo.Location = new System.Drawing.Point(0, 0);
            this.directoryChooserInfo.Name = "directoryChooserInfo";
            this.directoryChooserInfo.Size = new System.Drawing.Size(380, 26);
            this.directoryChooserInfo.TabIndex = 2;
            this.directoryChooserInfo.Load += new System.EventHandler(this.directoryChooserInfo1_Load);
            this.directoryChooserInfo.RequestEditPath += new PictogramUpdater.DirectoryChooserInfo.RequestEditPathHandler(this.directoryChooserInfo_RequestEditPath);
            // 
            // input
            // 
            this.input.InstallPath = null;
            this.input.Location = new System.Drawing.Point(0, 0);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(380, 26);
            this.input.TabIndex = 1;
            this.input.Visible = false;
            this.input.pathChanged += new PictogramUpdater.DirectoryChooserInput.PathChanged(this.input_pathChanged);
            this.input.RequestHideEditor += new PictogramUpdater.DirectoryChooserInput.HideEditor(this.input_RequestHideEditor);
            // 
            // DirectoryChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.directoryChooserInfo);
            this.Controls.Add(this.input);
            this.Name = "DirectoryChooser";
            this.Size = new System.Drawing.Size(380, 26);
            this.ResumeLayout(false);

        }

        #endregion

        private DirectoryChooserInput input;
        private DirectoryChooserInfo directoryChooserInfo;


    }
}
