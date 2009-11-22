using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PictogramUpdater {
    public partial class DirectoryChooser : UserControl {
        public DirectoryChooser() {
            InitializeComponent();
        }

        [Category("Domain")]
        public bool DisplayDirectoryInput {
            get;
            set;
        }

        [Category("Domain"), Bindable(true), Browsable(true)]
        public string InstallPath {
            get { return this.directoryChooserInfo.InstallPath; }
            set { this.directoryChooserInfo.InstallPath = value; }
        }

        private void directoryChooserInfo1_Load(object sender, EventArgs e) {
            this.directoryChooserInfo.InstallPath = InstallPath;
        }

        private void directoryChooserInfo_RequestEditPath() {
            this.directoryChooserInfo.Hide();

            this.input.InstallPath = InstallPath;
            this.input.Show();
        }

        private void input_RequestHideEditor() {
            SwitchToInfo();
        }

        private void SwitchToInfo() {
            this.input.Hide();
            this.directoryChooserInfo.Show();
        }

        private void input_pathChanged(string path) {
            InstallPath = path;
        }

        internal void languageChanged(string newPath) {
            InstallPath = newPath;

            if (this.input.Visible) {
                SwitchToInfo();
            }
        }
    }
}
