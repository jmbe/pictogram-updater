using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PictogramUpdater {
    public partial class DirectoryChooserInfo : UserControl {
        public delegate void RequestEditPathHandler();
        public event RequestEditPathHandler RequestEditPath;

        

        public DirectoryChooserInfo() {
            InitializeComponent();
        }

        [Category("DirectoryChooser"), Browsable(true)]
        public string InstallPath {
            get {
                return this.destinationLabel.Text;
            }
            set {
                this.destinationLabel.Text = value;
            }
        }

        private void DirectoryChooserInfo_Load(object sender, EventArgs e) {
            this.destinationLabel.Text = InstallPath;
        }

        private void changeDestinationLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            RequestEditPath();
        }

    }


}
