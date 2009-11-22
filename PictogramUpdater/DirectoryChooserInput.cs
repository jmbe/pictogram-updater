using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PictogramUpdater {
    public partial class DirectoryChooserInput : UserControl {

        public delegate void HideEditor();
        public event HideEditor RequestHideEditor;

        public delegate void PathChanged(string path);
        public event PathChanged pathChanged;

        public string InstallPath {
            get { return this.directoryTextbox.Text; }
            set { this.directoryTextbox.Text = value; }
        }

        public DirectoryChooserInput() {
            InitializeComponent();
        }

        private void directoryOkButton_Click(object sender, EventArgs e) {
            RequestHideEditor();
        }

        /// <summary>
        /// Hanterar klick på "Bläddra"-knappen. Låter användaren
        /// välja målkatalog.
        /// </summary>
        private void directoryBrowseButton_Click(object sender, EventArgs e) {
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = directoryTextbox.Text;
            folderBrowserDialog.ShowDialog();

            InstallPath = folderBrowserDialog.SelectedPath;
        }

        private void directoryTextbox_TextChanged(object sender, EventArgs e) {
            pathChanged(InstallPath);
        }

        private void DirectoryChooserInput_VisibleChanged(object sender, EventArgs e) {
            /* deselect text and move caret to end of text. */
            this.directoryTextbox.Focus();
            this.directoryTextbox.SelectionStart = this.directoryTextbox.Text.Length;
        }
    }
}
