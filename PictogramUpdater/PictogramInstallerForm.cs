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
using System.Diagnostics;
using System.Globalization;

namespace PictogramUpdater {
    internal delegate void SetProgressStyleCallback(ProgressBarStyle style);

    internal delegate void SetControlEnabledCallback(Control control, bool enabled);

    internal delegate void SetControlVisibleCallback(Control control, bool visible);

    internal delegate void SetTextCallback(string text);

    internal delegate void LogMessageCallback(string message);

    internal delegate void LogToFileCallback(string message);

    internal delegate void LogErrorCallback(string message);

    internal delegate void ExitCallback();

    internal delegate void SetLanguageDataSourceCallback(IList<string> source);

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
        private LanguageProvider languageProvider;
        private DownloadManager downloadManager;
        private Thread _currentWorkingThread;
        private AuthenticationService _authenticationService;
        private Config config;
        private LanguageSelection _languageSelection;
        private InstallationManager installationManager;
        private CategoryRepository categoryRepository;
        private CategoryTranslationService categoryTranslationService;
        private DownloadListManager downloadListManager;
        
        private IniFileFactory iniFileFactory;
        private PictogramRestService pictogramRestService;
        private FileLogger fileLogger;
        private CultureInfo culture;
        private ImageFormatProvider imageFormatProvider;
        private HargdataProducts hargdata;

        public PictogramInstallerForm(CultureInfo culture) {
            this.culture = culture;
            InitializeComponent();
        }

        /// <summary>
        /// Laddar ner pictogram som saknas. Avsett att köras i egen tråd.
        /// </summary>
        private void Download() {
            ClearDownloadLog("");

            SetControlsEnabled(false);

            if (!this.downloadManager.checkLogin(this.usernameTextbox.Text, this.passwordTextbox.Text)) {
                if (this._authenticationService.UseFreeAccount) {
                    /* "Please download new version." */
                    ShowError(TextResources.errorDownloadNew);
                } else {
                    /* "Check username and password" */
                    ShowError(TextResources.errorLoginFailed);
                }

                DownloadFinished();
                return;
            }



            try {
                var language = _languageSelection.Language;
                config.CreateOrUpdateWmfIni(_languageSelection, wmfDirectoryChooser.InstallPath, plainTextDirectoryChooser.InstallPath);


                LogMessage(TextResources.lookingForNewImages);
                if (language.IsTextless) {
                    installationManager.Download(wmfDirectoryChooser.InstallPath, _languageSelection, overwriteCheckbox.Checked, InstallationType.TEXTLESS, usernameTextbox.Text, passwordTextbox.Text);
                } else {
                    installationManager.Download(wmfDirectoryChooser.InstallPath, _languageSelection, overwriteCheckbox.Checked, InstallationType.CODE, usernameTextbox.Text, passwordTextbox.Text);
                }
                LogMessage(TextResources.downloadComplete);
                LogMessage("");


                if (plainTextCheckbox.Checked) {
                    LogMessage(TextResources.lookingForNewImagesWithText);
                    installationManager.Download(plainTextDirectoryChooser.InstallPath, _languageSelection, overwriteCheckbox.Checked, InstallationType.PLAIN_TEXT,
                                                 usernameTextbox.Text, passwordTextbox.Text);
                    /* "Nedladdning av pictobilder i klartext klar." */
                    LogMessage(TextResources.downloadPlainTextComplete);
                    LogMessage("");

                }

                if (textlessCheckBox.Checked) {
                    LogMessage(TextResources.lookingForNewImagesWithoutText);
                    installationManager.Download(textlessDirectoryChooser.InstallPath, _languageSelection, overwriteCheckbox.Checked, InstallationType.TEXTLESS, usernameTextbox.Text, passwordTextbox.Text);
                    LogMessage(TextResources.downloadTextlessComplete);
                    LogMessage("");
                }

                if (soundCheckbox.Checked) {
                    LogMessage(TextResources.lookingForNewSounds);
                    installationManager.Download(soundDirectoryChooser.InstallPath, _languageSelection, overwriteCheckbox.Checked, InstallationType.SOUND,
                                                 usernameTextbox.Text, passwordTextbox.Text);
                    config.CreateOrUpdateWavIni(language, soundDirectoryChooser.InstallPath);
                    LogMessage(TextResources.downloadSoundsComplete);
                    LogMessage("");
                }


                if (_languageSelection.Language.IsSwedish) {

                    if (hargdata.IsSymWriterInstalled) {
                        LogMessage("Installerar bilder för SymWriter...");
                        LanguageSelection ls = new LanguageSelection(Language.SWEDISH, ImageFormat.SVG);
                        installationManager.Download(hargdata.SymWriterImagesDirectory, ls, overwriteCheckbox.Checked, InstallationType.PLAIN_TEXT, usernameTextbox.Text, passwordTextbox.Text);
                        LogMessage("Installerar ordlista för SymWriter...");
                        hargdata.InstallSymWriterDictionary();
                        LogMessage("");
                    }

                    if (hargdata.IsWidgitInstalled) {
                        LogMessage("Installerar bilder för Widgit Symbolskrift...");
                        LanguageSelection ls = new LanguageSelection(Language.SWEDISH, ImageFormat.JPG);
                        installationManager.Download(hargdata.WidgitImagesDirectory, ls, overwriteCheckbox.Checked, InstallationType.PLAIN_TEXT, usernameTextbox.Text, passwordTextbox.Text);
                        LogMessage("Installerar ordlista för Widgit Symbolskrift...");
                        hargdata.installWidgitDictionary();
                        LogMessage("");
                    }

                    if (hargdata.IsCommunicateInstalled) {
                        LogMessage("Installerar bilder för Communicate: In Print");
                        LanguageSelection ls = new LanguageSelection(Language.SWEDISH, ImageFormat.JPG);
                        installationManager.Download(hargdata.CommunicateImagesDirectory, ls, overwriteCheckbox.Checked, InstallationType.PLAIN_TEXT, usernameTextbox.Text, passwordTextbox.Text);
                        LogMessage("Installerar ordlista för Communicate: In Print...");
                        hargdata.installCommunicateDictionary();
                        LogMessage("");
                    }
                } else {
                    LogToFile("Skipping dictionary installation because language is " + _languageSelection.Language);
                }

                config.CreateGenericPicWmfIni(_languageSelection);


                DownloadFinished();

                LogMessage("");
                LogMessage(TextResources.installationComplete);
                SetStatus(TextResources.installationComplete);

                InstallationFinishedSuccessfully();
            } catch (UnauthorizedAccessException e) {
                LogMessage(TextResources.failedFileAccess +  "(" + e.Message + ")");
                LogToFile(e.ToString());
            } catch (System.Net.WebException e) {
                LogMessage(TextResources.failedNetworkAccess + "(" + e.Message + ")");
                LogToFile(e.ToString());
            }

        }

        private void InstallationFinishedSuccessfully() {
            /* Set progress bar to 100% when installation is complete. */
            SetCurrentProgress(ProgressBarStyle.Blocks, 1, 1);



            if (IsPictogramManagerRunning()) {
                MessageBox.Show(TextResources.warningPictogramManagerRunning, TextResources.restartPictogramManager, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            DialogResult result = MessageBox.Show(TextResources.dialogInstallationCompleteExit, TextResources.dialogExitTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                Exit();
            }
        }

        private Boolean IsPictogramManagerRunning() {
            try {
                return Process.GetProcessesByName("PictogramManager").Length > 0;
            } catch {
                /* Ignored - not important */
            }
            return false;

        }

        private void Exit() {
            if (this.InvokeRequired) {
                this.Invoke(new ExitCallback(Exit));
            } else {
                this.Close();
            }

        }


        /// <summary>
        /// Cleanup after download has finished, whether it finished successfully or not.
        /// </summary>
        private void DownloadFinished() {
            SetControlsEnabled(true);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Kontrollerar om inloggningsuppgifterna är giltiga. Avsett att köras i egen tråd.
        /// </summary>
        private void CheckLogin() {
            SetControlsEnabled(false);
            SetStatus(TextResources.checkingAccountDetails);
            downloadManager.checkLogin(this.usernameTextbox.Text, this.passwordTextbox.Text);
            SetControlsEnabled(true);
            this._currentWorkingThread = null;
        }

        /// <summary>
        /// Hämtar giltiga språk och fyller språklistan med dem. Avsett att köras i egen tråd.
        /// </summary>
        private void RefreshLanguages() {
            SetProgressBarStyle(ProgressBarStyle.Marquee);
            SetControlsEnabled(false);
            SetStatus(TextResources.downloadingLanguages);

            languageProvider.RefreshLanguages();
            SetLanguageDataSource(languageProvider.Languages);

            SetStatus(TextResources.ready);
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
                return (string)languagesComboBox.Invoke(new GetLanguageCallback(GetLanguage));
            }
            return languagesComboBox.Text;
        }

        /// <summary>
        /// Skriver ut ett meddelande i loggen på ett trådsäkert sätt.
        /// </summary>
        /// <param name="message">meddelande</param>
        private void LogMessage(string message) {
            if (logTextbox.InvokeRequired) {
                this.logTextbox.Invoke(new LogMessageCallback(LogMessage), new object[] { message });
            } else {
                logTextbox.AppendText(message + Environment.NewLine);
                logTextbox.SelectionStart = logTextbox.Text.Length;
                LogToFile("Info: " + message);
            }
        }

        private void LogToFile(string message) {
            this.fileLogger.LogToFile(message);
        }

        private void ClearDownloadLog(string optionalMessage) {
            if (logTextbox.InvokeRequired) {
                this.logTextbox.Invoke(new LogMessageCallback(ClearDownloadLog), new object[] { optionalMessage });
            } else {
                logTextbox.Text = optionalMessage == null ? "" : optionalMessage;
            }
        }


        private void ShowError(string message) {
            MessageBox.Show(message, TextResources.installationAborted, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            LogToFile(TextResources.installationAborted);
            LogMessage(message);
        }

        /// <summary>
        /// Anger meddelande att visa i statusfältet.
        /// </summary>
        /// <param name="message">meddelande</param>
        private void SetStatus(string message) {
            if (this.statusStrip.InvokeRequired) {
                this.statusStrip.Invoke(new SetTextCallback(SetStatus), new object[] { message });
            } else {
                statusLabel.Text = message;
                LogToFile("Status: " + message);
            }
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
                                                          new object[] { progressBarStyle });
            } else {
                this.statusProgressBar.ProgressBar.Style = progressBarStyle;
            }
        }

        private void SetControlVisible(Control control, bool visible) {
            if (control.InvokeRequired) {
                control.Invoke(new SetControlVisibleCallback(SetControlVisible), new object[] { control, visible });
            } else {
                control.Visible = visible;
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
                                                          new object[] { style, current, max });
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
                    new Control[] { verifyLabel, installButton, overwriteCheckbox, wmfDirectoryChooser, soundDirectoryChooser, plainTextDirectoryChooser, textlessDirectoryChooser, languagesComboBox, formatComboBox }) {
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
                control.Invoke(new SetControlEnabledCallback(SetControlEnabled), new object[] { control, enabled });
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
            string header = this._authenticationService.UseFreeAccount ? TextResources.cdHeader : TextResources.webbHeader;
            e.Graphics.DrawString(header, new Font("Arial", 25, FontStyle.Bold), Brushes.SteelBlue, new PointF(10, 10));

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
            this._currentWorkingThread.CurrentUICulture = this.culture;
            this._currentWorkingThread.Start();
        }

        private void PictogramInstallerForm_Load(object sender, EventArgs e) {

            this.logTextbox.ScrollBars = ScrollBars.None;

            string version = TextResources.currentVersion;
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) {
                version = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }

            this.versionLabel.Text = "Version " + version;
            this.versionLabel.Show();

            /* Handler för när formuläret stängs */
            Closing += PictogramInstallerForm_Closing;

            createDependencyGraph();

            /* Ladda sparade inställningar */
            usernameTextbox.Text = _authenticationService.GetUsername();
            passwordTextbox.Text = _authenticationService.GetPassword();


            /* Installationskatalog för wmf */
            this.wmfDirectoryChooser.InstallPath = config.GetDefaultPath(_languageSelection);

            /* Plain text install dir*/
            this.plainTextDirectoryChooser.InstallPath = config.GetDefaultPlainTextPath(_languageSelection);

            /* Sound install dir*/
            this.soundDirectoryChooser.InstallPath = config.GetDefaultSoundPath(_languageSelection.Language);

            /* Textless install dir */
            this.textlessDirectoryChooser.InstallPath = config.GetTextLessInstallPath(_languageSelection);

            if (_authenticationService.UseFreeAccount) {
                groupBox1.Visible = false;
            }

            LogToFile("Installationen startas " + DateTime.Now.ToString());

            /* Ladda ner språk */
            languageProvider.LogMessage += LogMessage;
            languageProvider.LogToFile += LogToFile;
            _currentWorkingThread = new Thread(RefreshLanguages);
            this._currentWorkingThread.CurrentUICulture = this.culture;
            _currentWorkingThread.Start();


            installationManager.LogMessage += LogMessage;
            installationManager.LogToFile += LogToFile;
            installationManager.ProgressChanged += SetCurrentProgress;
            installationManager.StatusChanged += SetStatus;


            /* Klass att använda för att kommunicera med webservice. */
            downloadManager.LogMessage += LogMessage;
            downloadManager.LogToFile += LogToFile;
            downloadManager.ProgressChanged += SetCurrentProgress;
            downloadManager.StatusChanged += SetStatus;

            _languageSelection.LanguageChanged += LanguageChanged;

            SetFormatsDataSource(this.imageFormatProvider.Formats);
        }

        /// <summary>
        /// Should be refactored to use proper dependency injection.
        /// </summary>
        private void createDependencyGraph() {

            this.fileLogger = new FileLogger();

            this._languageSelection = new LanguageSelection();
            this._authenticationService = new AuthenticationService();

            this.categoryRepository = new CategoryRepository();
            this.categoryTranslationService = new CategoryTranslationService();

            this.pictogramRestService = new PictogramRestService("www.pictogram.se");

            this.languageProvider = new LanguageProvider(this.pictogramRestService);

            this.iniFileFactory = new IniFileFactory();

            this.config = new Config(this.categoryRepository, this.categoryTranslationService, this.iniFileFactory);
            this.downloadManager = new DownloadManager(this.languageProvider, this.pictogramRestService);


            this.downloadListManager = new DownloadListManager(this.pictogramRestService);

            this.installationManager = new InstallationManager(this.config, this.downloadListManager, this.languageProvider, this.pictogramRestService);

            this.imageFormatProvider = new ImageFormatProvider();

            this.hargdata = new HargdataProducts();
        }

        private void LanguageChanged() {

            string wmfPath = config.getInstallPathForLanguage(_languageSelection, InstallationType.CODE);
            wmfDirectoryChooser.languageChanged(wmfPath);

            string plainTextPath = config.getInstallPathForLanguage(_languageSelection, InstallationType.PLAIN_TEXT);
            plainTextDirectoryChooser.languageChanged(plainTextPath);

            string soundPath = config.getInstallPathForLanguage(_languageSelection, InstallationType.SOUND);
            soundDirectoryChooser.languageChanged(soundPath);

            string textlessPath = config.getInstallPathForLanguage(_languageSelection, InstallationType.TEXTLESS);
            textlessDirectoryChooser.languageChanged(textlessPath);

            if (_languageSelection.Language.IsSwedish) {
                soundDirectoryChooser.Show();
                soundCheckbox.Show();
            } else {

                soundDirectoryChooser.Hide();
                soundCheckbox.Checked = false;
                soundCheckbox.Hide();
            }

            if (!_languageSelection.Language.IsTextless) {
                plainTextDirectoryChooser.Show();
                plainTextCheckbox.Show();
                textlessDirectoryChooser.Show();
                textlessCheckBox.Show();
            } else {
                plainTextDirectoryChooser.Hide();
                plainTextCheckbox.Checked = false;
                plainTextCheckbox.Hide();

                textlessDirectoryChooser.Hide();
                textlessCheckBox.Checked = false;
                textlessCheckBox.Hide();
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
                _authenticationService.saveAccount(usernameTextbox.Text, passwordTextbox.Text);

                LogToFile("Installationen avslutas " + DateTime.Now.ToString());

            } catch (Exception ex) {
                LogToFile(ex.ToString());
            }
        }

        /// <summary>
        /// Hanterar klick på installationsknappen. Sätter igång nedladdning.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InstallButton_Click(object sender, EventArgs e) {

            this.logTextbox.ScrollBars = ScrollBars.Both;

            this._currentWorkingThread = new Thread(new ThreadStart(Download));
            this._currentWorkingThread.CurrentUICulture = this.culture;
            _currentWorkingThread.Start();
        }

        /// <summary>
        /// Metod för att sätta val för språk på ett trådsäkert sätt.
        /// </summary>
        /// <param name="source"></param>
        private void SetLanguageDataSource(IList<string> source) {
            if (languagesComboBox.InvokeRequired) {
                languagesComboBox.Invoke(new SetLanguageDataSourceCallback(SetLanguageDataSource), new object[] { source });
            } else {
                languagesComboBox.DataSource = source;
                languagesComboBox.SelectedIndex = languagesComboBox.FindString("Svenska");

                String name = this.languageProvider.getNativeName(this.culture.TwoLetterISOLanguageName);
                int index =languagesComboBox.FindString(name);

                if (index > 0) {
                    languagesComboBox.SelectedIndex = index;
                }
            }
        }

        private void SetFormatsDataSource(IList<ImageFormat> source) {
            formatComboBox.DisplayMember = "Display";
            formatComboBox.ValueMember = null; // use actual ImageFormat as value

            formatComboBox.DataSource = source;
        }

        private void LanguageComboBox_Change(object sender, EventArgs e) {
            var languageName = languagesComboBox.SelectedItem as string;
            _languageSelection.Language = new Language(languageProvider.GetLocale(languageName), languageName);
        }

        private void formatComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            ImageFormat format = formatComboBox.SelectedItem as ImageFormat;
            _languageSelection.ImageFormat = format;
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
            if (installationManager.CurrentWorkingThread != null) {
                installationManager.CurrentWorkingThread.Abort();
            }
            if (this._currentWorkingThread != null) {
                this._currentWorkingThread.Abort();
                SetStatus(TextResources.downloadAborted);
                SetControlsEnabled(true);
            }
        }

        private void contactLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start(contactLink.Text);
        }

        private void exitLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
        }

    }
}