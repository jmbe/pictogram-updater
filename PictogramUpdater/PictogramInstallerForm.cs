using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using AMS.Profile;
using PictogramUpdater;

namespace PictogramUpdater {
    internal delegate void SetProgressStyleCallback(ProgressBarStyle style);

    internal delegate void SetControlEnabledCallback(Control control, bool enabled);

    internal delegate void LogMessageCallback(string message);

    internal delegate void SetLanguageDataSourceCallback(IList source);

    internal delegate void SetStatusCallback(string message);

    internal delegate string GetLanguageCallback();

    internal delegate void CurrentProgressCallback(ProgressBarStyle style, int current, int max);

    internal delegate void LanguageChangedCallback();

    /// <summary>
    /// Inneh�ller metoder som har med anv�ndargr�nssnittet att g�ra.
    /// 
    /// M�nga metoder har att g�ra med att kunna anropa kontroller p� ett 
    /// tr�ds�kert s�tt. Information ang�ende det finns p� 
    /// http://msdn2.microsoft.com/en-us/library/ms171728.aspx.
    /// </summary>
    public partial class PictogramInstallerForm : Form {
        private LanguageProvider _languageProvider;
        private DownloadManager _downloader;
        private Thread _currentWorkingThread;
        private AuthenticationService _authenticationService;
        private Config _config;
        private LanguageSelection _languageSelection;
        private string _installPath;
        private string _clearTextInstallPath;
        private string _soundInstallPath;
        private bool _displayInstallPathInputField;
        private bool _displayClearTextInstallPathInputField;
        private bool _displaySoundInstallPathInputField;
        private InstallationManager _manager;

        public PictogramInstallerForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Laddar ner pictogram som saknas. Avsett att k�ras i egen tr�d.
        /// </summary>
        private void Download() {
            SetControlsEnabled(false);
            var language = _languageSelection.Language;
            _config.CreateOrUpdateWmfINI(language, _installPath, _clearTextInstallPath);
            
            _manager.Download(_installPath, language, overwriteCheckbox.Checked, false, false, usernameTextbox.Text, passwordTextbox.Text);

            if(clearTextCheckbox.Checked) {
                _manager.Download(_clearTextInstallPath, language, overwriteCheckbox.Checked, true, false,
                                           usernameTextbox.Text, passwordTextbox.Text);
            }

            if(soundCheckbox.Checked) {
                _manager.Download(_soundInstallPath, language, overwriteCheckbox.Checked, false, true,
                                           usernameTextbox.Text, passwordTextbox.Text);
                _config.CreateOrUpdateWavIni(language, _soundInstallPath);
            }

            SetControlsEnabled(true);
            this._currentWorkingThread = null;

        }

        /// <summary>
        /// Laddar ner pictogram som zipfil. Avsett att k�ras i egen tr�d.
        /// </summary>
        private void DownloadZip() {
            SetControlsEnabled(false);

            _manager.DownloadZip(_installPath, usernameTextbox.Text, passwordTextbox.Text, _languageSelection.Language);

            SetControlsEnabled(true);
            _currentWorkingThread = null;
        }

        /// <summary>
        /// H�mtar url f�r att ladda ner pictogramzipfil.
        /// Avsett att k�ras i egen tr�d.
        /// </summary>
        private void GetZipUrl() {
            SetControlsEnabled(false);

            _downloader.TargetPath = this.directoryTextbox.Text;
            _downloader.DownloadPictogramZipUrl(this.usernameTextbox.Text, this.passwordTextbox.Text, GetLanguage());

            SetControlsEnabled(true);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Kontrollerar om inloggningsuppgifterna �r giltiga. Avsett att k�ras i egen tr�d.
        /// </summary>
        private void CheckLogin() {
            SetControlsEnabled(false);
            SetStatus("Kontrollerar kontouppgifter...");
            _downloader.checkLogin(this.usernameTextbox.Text, this.passwordTextbox.Text);
            SetControlsEnabled(true);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// H�mtar giltiga spr�k och fyller spr�klistan med dem. Avsett att k�ras i egen tr�d.
        /// </summary>
        private void RefreshLanguages() {
            SetProgressBarStyle(ProgressBarStyle.Marquee);
            SetControlsEnabled(false);
            SetStatus("Laddar ner spr�k...");

            _languageProvider.RefreshLanguages();
            SetLanguageDataSource(_languageProvider.Languages);

            SetStatus("Klar");
            SetControlsEnabled(true);
            SetProgressBarStyle(ProgressBarStyle.Blocks);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Metod f�r att h�mta valt spr�k p� ett tr�ds�kert s�tt.
        /// </summary>
        /// <returns>valt spr�k</returns>
        private string GetLanguage() {
            if (languagesComboBox.InvokeRequired) {
                return (string) languagesComboBox.Invoke(new GetLanguageCallback(GetLanguage));
            }
            return languagesComboBox.Text;
        }

        /// <summary>
        /// Skriver ut ett meddelande i loggen p� ett tr�ds�kert s�tt.
        /// </summary>
        /// <param name="message">meddelande</param>
        private void LogMessage(string message) {
            if (logTextbox.InvokeRequired) {
                this.logTextbox.Invoke(new LogMessageCallback(LogMessage), new object[] {message});
            } else {
                logTextbox.Text = message + "\r\n" + logTextbox.Text;
            }
        }

        /// <summary>
        /// Anger meddelande att visa i statusf�ltet.
        /// </summary>
        /// <param name="message">meddelande</param>
        private void SetStatus(string message) {
            statusLabel.Text = message;
        }

        /// <summary>
        /// Metod f�r att �ndra en progressbars style p� ett 
        /// tr�ds�kert s�tt.
        /// 
        /// 
        /// </summary>
        /// <param name="progressBarStyle"></param>
        private void SetProgressBarStyle(ProgressBarStyle progressBarStyle) {
            if (this.statusProgressBar.ProgressBar.InvokeRequired) {
                this.statusProgressBar.ProgressBar.Invoke(new SetProgressStyleCallback(SetProgressBarStyle),
                                                          new object[] {progressBarStyle});
            } else {
                this.statusProgressBar.ProgressBar.Style = progressBarStyle;
            }
        }

        /// <summary>
        /// Metod f�r att s�tta egenskaper f�r progressbar p� ett tr�ds�kert s�tt.
        /// </summary>
        /// <param name="style"></param>
        /// <param name="current"></param>
        /// <param name="max"></param>
        private void SetCurrentProgress(ProgressBarStyle style, int current, int max) {
            if (this.statusProgressBar.ProgressBar.InvokeRequired) {
                this.statusProgressBar.ProgressBar.Invoke(new CurrentProgressCallback(SetCurrentProgress),
                                                          new object[] {style, current, max});
            } else {
                this.statusProgressBar.ProgressBar.Style = style;
                this.statusProgressBar.ProgressBar.Maximum = max;
                this.statusProgressBar.ProgressBar.Value = current;
            }
        }

        /// <summary>
        /// Hj�lpmetod f�r att aktivera eller inaktivera m�nga kontroller i taget.
        /// </summary>
        /// <param name="enabled"></param>
        private void SetControlsEnabled(bool enabled) {
            foreach (
                Control control in
                    new Control[]
                    {verifyLabel, installButton, updateLinkLabel, overwriteCheckbox, zipButton, getZipUrlButton}) {
                SetControlEnabled(control, enabled);
            }
        }

        /// <summary>
        /// Metod f�r att aktivera eller inaktiver en kontroll p� ett tr�ds�kert s�tt.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="enabled"></param>
        public void SetControlEnabled(Control control, bool enabled) {
            if (control.InvokeRequired) {
                control.Invoke(new SetControlEnabledCallback(SetControlEnabled), new object[] {control, enabled});
            } else {
                control.Enabled = enabled;
            }
        }

        /// <summary>
        /// Ritar ut grafik p� formul�ret.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Paint(object sender, PaintEventArgs e) {
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, 50));
            e.Graphics.DrawString("Pictograminstalleraren", new Font("Arial", 20), Brushes.SteelBlue, new PointF(10, 10));
            e.Graphics.DrawLine(Pens.Black, new Point(0, 50), new Point(this.Width, 50));
        }

        /// <summary>
        /// Hanterar klick p� "Kontrollera"-knappen. Kontrollerar att
        /// kontouppgifterna �r giltiga.
        /// </summary>
        private void VerifyLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this._currentWorkingThread = new Thread(CheckLogin);
            this._currentWorkingThread.Start();
        }

        /// <summary>
        /// Hanterar klick p� "Bl�ddra"-knappen. L�ter anv�ndaren
        /// v�lja m�lkatalog.
        /// </summary>
        private void BrowseButton_Click(object sender, EventArgs e) {
            folderBrowserDialog.ShowNewFolderButton = true;
            folderBrowserDialog.SelectedPath = directoryTextbox.Text;
            folderBrowserDialog.ShowDialog();
            directoryTextbox.Text = folderBrowserDialog.SelectedPath;
        }

        private void PictogramInstallerForm_Load(object sender, EventArgs e) {
            /* Handler f�r n�r formul�ret st�ngs */
            Closing += PictogramInstallerForm_Closing;

            /* Ladda sparade inst�llningar */

            _languageSelection = new LanguageSelection();
            _authenticationService = new AuthenticationService();
            _config = new Config();

            /* Installationskatalog */
            if (_displayInstallPathInputField) {
                directoryTextbox.Text = _config.GetDefaultPath(_languageSelection.Language);
            } else {
                HideInstallPathInput(true);
            }

            /* Clear text install dir*/
            if (_displayClearTextInstallPathInputField) {
                clearTextDirectoryTextbox.Text = _config.GetDefaultClearTextPath(_languageSelection.Language);
            } else {
                HideClearTextInstallPathInput(true);
            }

            /* Sound install dir*/
            if (_displaySoundInstallPathInputField) {
                soundDirectoryTextbox.Text = _config.GetDefaultSoundPath(_languageSelection.Language);
            } else {
                HideSoundInstallPathInput(true);
            }

            if (_authenticationService.IsPictogramManagerInstalled()) {
                groupBox1.Visible = false;
                groupBox2.Location = new Point(12, 62);
            }

            usernameTextbox.Text = _authenticationService.GetUsername();
            passwordTextbox.Text = _authenticationService.GetPassword();

            /* Ladda ner spr�k */
            _languageProvider = new LanguageProvider();
            _languageProvider.LogMessage += LogMessage;
            _currentWorkingThread = new Thread(RefreshLanguages);
            _currentWorkingThread.Start();

            _manager = new InstallationManager(_config);
            _manager.LogMessage += LogMessage;
            _manager.ProgressChanged += SetCurrentProgress;
            _manager.StatusChanged += SetStatus;

            /* Klass att anv�nda f�r att kommunicera med webservice. */
            _downloader = new PictogramUpdater.DownloadManager(_languageProvider);
            _downloader.LogMessage += LogMessage;
            _downloader.ProgressChanged += SetCurrentProgress;
            _downloader.StatusChanged += SetStatus;

            _languageSelection.LanguageChanged += LanguageChanged;
        }

        private void HideInstallPathInput(bool hide) {
            directoryLabel.Visible = !hide;
            directoryTextbox.Visible = !hide;
            directoryBrowseButton.Visible = !hide;

            directoryPathLabel.Visible = hide;
            changeInstallPathSelectionLinkLabel.Visible = hide;
        }

        private void HideClearTextInstallPathInput(bool hide) {
            clearTextDirectoryLabel.Visible = !hide;
            clearTextDirectoryTextbox.Visible = !hide;
            clearTextDirectoryBrowseButton.Visible = !hide;

            clearTextDirectoryPathLabel.Visible = hide;
            clearTextChangeInstallPathSelectionLinkLabel.Visible = hide;
        }

        private void HideSoundInstallPathInput(bool hide) {
            soundDirectoryLabel.Visible = !hide;
            soundDirectoryTextbox.Visible = !hide;
            soundDirectoryBrowseButton.Visible = !hide;

            soundDirectoryPathLabel.Visible = hide;
            soundChangeInstallPathSelectionLinkLabel.Visible = hide;
        }

        private void LanguageChanged() {
            if (_displayInstallPathInputField) {
                if (directoryTextbox.Text.Length == 0) {
                    directoryTextbox.Text = _config.GetDefaultPath(_languageSelection.Language);
                }
                _installPath = directoryTextbox.Text;
                HideInstallPathInput(false);
                _displayInstallPathInputField = false;
            } else {
                _installPath = _config.GetPictoInstallPath(_languageSelection.Language);
                directoryPathLabel.Text = "Installeras till '" + _installPath + "'";
                directoryTextbox.Text = _installPath;
                HideInstallPathInput(true);
            }
            if (_displayClearTextInstallPathInputField) {
                if (clearTextDirectoryTextbox.Text.Length == 0) {
                    clearTextDirectoryTextbox.Text = _config.GetDefaultClearTextPath(_languageSelection.Language);
                }
                _clearTextInstallPath = clearTextDirectoryTextbox.Text;
                HideClearTextInstallPathInput(false);
                _displayClearTextInstallPathInputField = false;
            } else {
                _clearTextInstallPath = _config.GetPictoClearTextInstallPath(_languageSelection.Language);
                clearTextDirectoryPathLabel.Text = "Installeras till '" + _clearTextInstallPath + "'";
                clearTextDirectoryTextbox.Text = _clearTextInstallPath;
                HideClearTextInstallPathInput(true);
            }
            if (_displaySoundInstallPathInputField) {
                if (soundDirectoryTextbox.Text.Length == 0) {
                    soundDirectoryTextbox.Text = _config.GetDefaultSoundPath(_languageSelection.Language);
                }
                _soundInstallPath = soundDirectoryTextbox.Text;
                HideSoundInstallPathInput(false);
                _displaySoundInstallPathInputField = false;
            } else {
                _soundInstallPath = _config.GetPictoSoundInstallPath(_languageSelection.Language);
                soundDirectoryPathLabel.Text = "Installeras till '" + _soundInstallPath + "'";
                soundDirectoryTextbox.Text = _soundInstallPath;
                HideSoundInstallPathInput(true);
            }
        }

        /// <summary>
        /// Hanterar att formul�ret st�ngs. St�nger av arbetstr�den och sparar inst�llningar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictogramInstallerForm_Closing(object sender, CancelEventArgs e) {
            try {
                if (_currentWorkingThread != null) {
                    _currentWorkingThread.Abort();
                }

                /* Spara inst�llningar */
                _authenticationService.SaveUsername(usernameTextbox.Text);
                _authenticationService.SavePassword(passwordTextbox.Text);

                _config.SetPictoInstallPaths(_languageSelection.Language, directoryTextbox.Text, clearTextDirectoryTextbox.Text, soundDirectoryTextbox.Text);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Hanterar klick p� uppdateringsl�nken. Laddar ner giltiga spr�k.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this._currentWorkingThread = new Thread(new ThreadStart(RefreshLanguages));
            _currentWorkingThread.Start();
        }

        /// <summary>
        /// Hanterar klick p� installationsknappen. S�tter ig�ng nedladdning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallButton_Click(object sender, EventArgs e) {
            this._currentWorkingThread = new Thread(new ThreadStart(Download));
            _currentWorkingThread.Start();
        }

        /// <summary>
        /// Metod f�r att s�tta val f�r spr�k p� ett tr�ds�kert s�tt.
        /// </summary>
        /// <param name="source"></param>
        private void SetLanguageDataSource(IList source) {
            if (languagesComboBox.InvokeRequired) {
                languagesComboBox.Invoke(new SetLanguageDataSourceCallback(SetLanguageDataSource), new object[] {source});
            } else {
                languagesComboBox.DataSource = source;
                languagesComboBox.SelectedIndex = languagesComboBox.FindString("Svenska");
            }
        }

        private void LanguageComboBox_Change(object sender, EventArgs e) {
            var languageName = languagesComboBox.SelectedItem as string;
            _languageSelection.Language = new Language(_languageProvider.GetLocale(languageName), languageName);
        }

        /// <summary>
        /// Hanterar klick p� progressbaren. Avbryter nedladdning om s�dan p�g�r.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusProgressBar_Click(object sender, EventArgs e) {
            if (this._currentWorkingThread != null) {
                this._currentWorkingThread.Abort();
                SetStatus("Nedladdning avbruten!");
                SetControlsEnabled(true);
            }
        }

        private void ZipButton_Click(object sender, EventArgs e) {
            this._currentWorkingThread = new Thread(new ThreadStart(DownloadZip));
            _currentWorkingThread.Start();
        }

        private void GetZipUrlButton_Click(object sender, EventArgs e) {
            this._currentWorkingThread = new Thread(new ThreadStart(GetZipUrl));
            this._currentWorkingThread.Start();
        }

        private void DirectoryPathTextbox_Changed(object sender, EventArgs e) {
            _installPath = directoryTextbox.Text;
        }

        private void ChangeInstallPathSelectionLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            HideInstallPathInput(false);
            _displayInstallPathInputField = false;
        }

        private void ChangeClearTextInstallPathSelectionLabel_LinkClicked(object sender, EventArgs e) {
            HideClearTextInstallPathInput(false);
            _displayClearTextInstallPathInputField = false;
        }

        private void ChangeSoundInstallPathSelectionLabel_LinkClicked(object sender, EventArgs e) {
            HideSoundInstallPathInput(false);
            _displaySoundInstallPathInputField = false;
        }
    }
}