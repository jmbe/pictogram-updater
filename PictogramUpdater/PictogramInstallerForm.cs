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
    /// Innehåller metoder som har med användargränssnittet att göra.
    /// 
    /// Många metoder har att göra med att kunna anropa kontroller på ett 
    /// trådsäkert sätt. Information angående det finns på 
    /// http://msdn2.microsoft.com/en-us/library/ms171728.aspx.
    /// </summary>
    public partial class PictogramInstallerForm : Form {
        private LanguageProvider _languageProvider;
        private DownloadManager _downloader;
        private Thread _currentWorkingThread;
        private AuthenticationService _authenticationService;
        private Config _config;
        private LanguageSelection _languageSelection;
        private InstallationManager _manager;
        private CategoryRepository categoryRepository;
        private CategoryTranslationService categoryTranslationService;

        public PictogramInstallerForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Laddar ner pictogram som saknas. Avsett att köras i egen tråd.
        /// </summary>
        private void Download() {
            SetControlsEnabled(false);
            var language = _languageSelection.Language;
            _config.CreateOrUpdateWmfINI(language, wmfDirectoryChooser.InstallPath, plainTextDirectoryChooser.InstallPath);
            
            _manager.Download(wmfDirectoryChooser.InstallPath, language, overwriteCheckbox.Checked, false, false, usernameTextbox.Text, passwordTextbox.Text);

            if(clearTextCheckbox.Checked) {
                _manager.Download(plainTextDirectoryChooser.InstallPath, language, overwriteCheckbox.Checked, true, false,
                                           usernameTextbox.Text, passwordTextbox.Text);
            }

            if(soundCheckbox.Checked) {
                _manager.Download(soundDirectoryChooser.InstallPath, language, overwriteCheckbox.Checked, false, true,
                                           usernameTextbox.Text, passwordTextbox.Text);
                _config.CreateOrUpdateWavIni(language, soundDirectoryChooser.InstallPath);
            }

            SetControlsEnabled(true);
            this._currentWorkingThread = null;

        }

        /// <summary>
        /// Laddar ner pictogram som zipfil. Avsett att köras i egen tråd.
        /// </summary>
        private void DownloadZip() {
            SetControlsEnabled(false);

            _manager.DownloadZip(wmfDirectoryChooser.InstallPath, usernameTextbox.Text, passwordTextbox.Text, _languageSelection.Language);

            SetControlsEnabled(true);
            _currentWorkingThread = null;
        }

        /// <summary>
        /// Hämtar url för att ladda ner pictogramzipfil.
        /// Avsett att köras i egen tråd.
        /// </summary>
        private void GetZipUrl() {
            SetControlsEnabled(false);

            _downloader.TargetPath = this.wmfDirectoryChooser.InstallPath;
            _downloader.DownloadPictogramZipUrl(this.usernameTextbox.Text, this.passwordTextbox.Text, GetLanguage());

            SetControlsEnabled(true);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Kontrollerar om inloggningsuppgifterna är giltiga. Avsett att köras i egen tråd.
        /// </summary>
        private void CheckLogin() {
            SetControlsEnabled(false);
            SetStatus("Kontrollerar kontouppgifter...");
            _downloader.checkLogin(this.usernameTextbox.Text, this.passwordTextbox.Text);
            SetControlsEnabled(true);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Hämtar giltiga språk och fyller språklistan med dem. Avsett att köras i egen tråd.
        /// </summary>
        private void RefreshLanguages() {
            SetProgressBarStyle(ProgressBarStyle.Marquee);
            SetControlsEnabled(false);
            SetStatus("Laddar ner språk...");

            _languageProvider.RefreshLanguages();
            SetLanguageDataSource(_languageProvider.Languages);

            SetStatus("Redo");
            SetControlsEnabled(true);
            SetProgressBarStyle(ProgressBarStyle.Blocks);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Metod för att hämta valt språk på ett trådsäkert sätt.
        /// </summary>
        /// <returns>valt språk</returns>
        private string GetLanguage() {
            if (languagesComboBox.InvokeRequired) {
                return (string) languagesComboBox.Invoke(new GetLanguageCallback(GetLanguage));
            }
            return languagesComboBox.Text;
        }

        /// <summary>
        /// Skriver ut ett meddelande i loggen på ett trådsäkert sätt.
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
        /// Anger meddelande att visa i statusfältet.
        /// </summary>
        /// <param name="message">meddelande</param>
        private void SetStatus(string message) {
            statusLabel.Text = message;
        }

        /// <summary>
        /// Metod för att ändra en progressbars style på ett 
        /// trådsäkert sätt.
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
        /// Metod för att sätta egenskaper för progressbar på ett trådsäkert sätt.
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

            /*
            if (this.wmfProgressBar.InvokeRequired) {
                this.wmfProgressBar.Invoke(new CurrentProgressCallback(SetCurrentProgress), new object[] { style, current, max });
            } else {
                this.wmfProgressBar.Style = style;
                this.wmfProgressBar.Maximum = max;
                this.wmfProgressBar.Value = current;
            }
             */

        }

        /// <summary>
        /// Hjälpmetod för att aktivera eller inaktivera många kontroller i taget.
        /// </summary>
        /// <param name="enabled"></param>
        private void SetControlsEnabled(bool enabled) {
            foreach (
                Control control in
                    new Control[]
                    {verifyLabel, installButton, updateLinkLabel, overwriteCheckbox, zipButton, getZipUrlButton, wmfDirectoryChooser, soundDirectoryChooser, plainTextDirectoryChooser}) {
                SetControlEnabled(control, enabled);
            }
        }

        /// <summary>
        /// Metod för att aktivera eller inaktiver en kontroll på ett trådsäkert sätt.
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
        /// Ritar ut grafik på formuläret.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Paint(object sender, PaintEventArgs e) {
            int topLineY = 60;

            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, topLineY));
            e.Graphics.DrawString("Pictograminstalleraren", new Font("Arial", 25, FontStyle.Bold), Brushes.SteelBlue, new PointF(10, 10));
            
            e.Graphics.DrawLine(Pens.LightGray, new Point(0, topLineY), new Point(this.Width, topLineY));

            int bottomLineY = topLineY + pictureBox1.Height + 1;
            e.Graphics.DrawLine(Pens.LightGray, new Point(0, bottomLineY), new Point(this.Width, bottomLineY));
        }

        /// <summary>
        /// Hanterar klick på "Kontrollera"-knappen. Kontrollerar att
        /// kontouppgifterna är giltiga.
        /// </summary>
        private void VerifyLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this._currentWorkingThread = new Thread(CheckLogin);
            this._currentWorkingThread.Start();
        }

        private void PictogramInstallerForm_Load(object sender, EventArgs e) {

            this.logTextbox.ScrollBars = ScrollBars.None;

            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) {
                this.versionLabel.Text =  "Version " + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
                this.versionLabel.Show();
            }

            /* Handler för när formuläret stängs */
            Closing += PictogramInstallerForm_Closing;

            /* Ladda sparade inställningar */

            
            
            _languageSelection = new LanguageSelection();
            _authenticationService = new AuthenticationService();


            this.categoryRepository = new CategoryRepository();
            this.categoryTranslationService = new CategoryTranslationService();

            _config = new Config(this.categoryRepository, this.categoryTranslationService);



            /* Installationskatalog för wmf */
            this.wmfDirectoryChooser.InstallPath = _config.GetDefaultPath(_languageSelection.Language);

            /* Plain text install dir*/
            this.plainTextDirectoryChooser.InstallPath = _config.GetDefaultPlainTextPath(_languageSelection.Language);

            /* Sound install dir*/
            this.soundDirectoryChooser.InstallPath = _config.GetDefaultSoundPath(_languageSelection.Language);


            if (_authenticationService.IsPictogramLibraryInstalled()) {
                groupBox1.Visible = false;

            }

            usernameTextbox.Text = _authenticationService.GetUsername();
            passwordTextbox.Text = _authenticationService.GetPassword();

            /* Ladda ner språk */
            _languageProvider = new LanguageProvider();
            _languageProvider.LogMessage += LogMessage;
            _currentWorkingThread = new Thread(RefreshLanguages);
            _currentWorkingThread.Start();

            _manager = new InstallationManager(_config);
            _manager.LogMessage += LogMessage;
            _manager.ProgressChanged += SetCurrentProgress;
            _manager.StatusChanged += SetStatus;

            /* Klass att använda för att kommunicera med webservice. */
            _downloader = new PictogramUpdater.DownloadManager(_languageProvider);
            _downloader.LogMessage += LogMessage;
            _downloader.ProgressChanged += SetCurrentProgress;
            _downloader.StatusChanged += SetStatus;

            _languageSelection.LanguageChanged += LanguageChanged;
        }

        private void LanguageChanged() {

            string wmfPath = _config.GetPictoInstallPath(_languageSelection.Language);
            wmfDirectoryChooser.languageChanged(wmfPath);

            string plainTextPath = _config.GetPictoPlainTextInstallPath(_languageSelection.Language);
            plainTextDirectoryChooser.languageChanged(plainTextPath);

            string soundPath = _config.GetPictoSoundInstallPath(_languageSelection.Language);
            soundDirectoryChooser.languageChanged(soundPath);


            if (_languageSelection.Language.IsSwedish) {
                soundDirectoryChooser.Show();
                soundCheckbox.Show();
            } else {
                
                soundDirectoryChooser.Hide();
                soundCheckbox.Checked = false;
                soundCheckbox.Hide();
            }
        }

        /// <summary>
        /// Hanterar att formuläret stängs. Stänger av arbetstråden och sparar inställningar.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictogramInstallerForm_Closing(object sender, CancelEventArgs e) {
            try {
                abortDownload();

                /* Spara inställningar */
                _authenticationService.SaveUsername(usernameTextbox.Text);
                _authenticationService.SavePassword(passwordTextbox.Text);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Hanterar klick på uppdateringslänken. Laddar ner giltiga språk.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this._currentWorkingThread = new Thread(new ThreadStart(RefreshLanguages));
            _currentWorkingThread.Start();
        }

        /// <summary>
        /// Hanterar klick på installationsknappen. Sätter igång nedladdning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallButton_Click(object sender, EventArgs e) {

            this.logTextbox.ScrollBars = ScrollBars.Both;

            this._currentWorkingThread = new Thread(new ThreadStart(Download));
            _currentWorkingThread.Start();
        }

        /// <summary>
        /// Metod för att sätta val för språk på ett trådsäkert sätt.
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
        /// Hanterar klick på progressbaren. Avbryter nedladdning om sådan pågår.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatusProgressBar_Click(object sender, EventArgs e) {
            abortDownload();
        }

        private void abortDownload() {
            if (_manager.CurrentWorkingThread != null) {
                _manager.CurrentWorkingThread.Abort();
            }
            if (this._currentWorkingThread != null) {
                this._currentWorkingThread.Abort();
                SetStatus("Nedladdning avbruten");
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

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            
            System.Diagnostics.Process.Start("http://pictoonline.pictogram.se/contact");
        }

    }
}