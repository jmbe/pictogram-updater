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



namespace PictogramUpdater {

	delegate void SetProgressStyleCallback(ProgressBarStyle style);
	delegate void SetControlEnabledCallback(Control control, bool enabled);
	delegate void LogMessageCallback(string message);
	delegate void SetLanguageDataSourceCallback(IList source);
	delegate void SetStatusCallback(string message);
	delegate string GetLanguageCallback();
	delegate void CurrentProgressCallback(ProgressBarStyle style, int current, int max);

	/// <summary>
	/// Innehåller metoder som har med användargränssnittet att göra.
	/// 
	/// Många metoder har att göra med att kunna anropa kontroller på ett 
	/// trådsäkert sätt. Information angående det finns på 
	/// http://msdn2.microsoft.com/en-us/library/ms171728.aspx.
	/// </summary>
	public partial class PictogramInstallerForm : Form {

		private ISettingsPersistence settings;
		private LanguageProvider languageProvider;
		private PictogramDownloader downloader;
		private Thread currentWorkingThread;

		public PictogramInstallerForm() {
			InitializeComponent();
		}


		/// <summary>
		/// Laddar ner pictogram som saknas. Avsett att köras i egen tråd.
		/// </summary>
		private void Download() {
			SetControlsEnabled(false);

			downloader.OverwriteExistingFiles = this.overwriteCheckbox.Checked;
			downloader.TargetPath = this.pathTextbox.Text;

			downloader.Download(this.usernameTextbox.Text, this.passwordTextbox.Text, GetLanguage());
			SetControlsEnabled(true);
			this.currentWorkingThread = null;

		}

		/// <summary>
		/// Laddar ner pictogram som zipfil. Avsett att köras i egen tråd.
		/// </summary>
		private void DownloadZip() {
			SetControlsEnabled(false);

			downloader.TargetPath = this.pathTextbox.Text;
			downloader.DownloadZip(this.usernameTextbox.Text, this.passwordTextbox.Text, GetLanguage());

			SetControlsEnabled(true);
			this.currentWorkingThread = null;
		}

		/// <summary>
		/// Hämtar url för att ladda ner pictogramzipfil.
		/// Avsett att köras i egen tråd.
		/// </summary>
		private void GetZipUrl() {
			SetControlsEnabled(false);

			downloader.TargetPath = this.pathTextbox.Text;
			downloader.DownloadPictogramZipUrl(this.usernameTextbox.Text, this.passwordTextbox.Text, GetLanguage());

			SetControlsEnabled(true);
			this.currentWorkingThread = null;
		}



		/// <summary>
		/// Kontrollerar om inloggningsuppgifterna är giltiga. Avsett att köras i egen tråd.
		/// </summary>
		private void CheckLogin() {

			SetControlsEnabled(false);
			SetStatus("Kontrollerar kontouppgifter...");
			downloader.checkLogin(this.usernameTextbox.Text, this.passwordTextbox.Text);
			SetControlsEnabled(true);
			this.currentWorkingThread = null;

		}

		/// <summary>
		/// Hämtar giltiga språk och fyller språklistan med dem. Avsett att köras i egen tråd.
		/// </summary>
		private void RefreshLanguages() {
			SetProgressBarStyle(ProgressBarStyle.Marquee);
			SetControlsEnabled(false);
			SetStatus("Laddar ner språk...");

			languageProvider.RefreshLanguages();
			SetLanguageDataSource(languageProvider.Languages);

			SetStatus("Klar");
			SetControlsEnabled(true);
			SetProgressBarStyle(ProgressBarStyle.Blocks);
			this.currentWorkingThread = null;

		}

		/// <summary>
		/// Metod för att hämta valt språk på ett trådsäkert sätt.
		/// </summary>
		/// <returns>valt språk</returns>
		private string GetLanguage() {
			if (this.languagesComboBox.InvokeRequired) {
				return (string)this.languagesComboBox.Invoke(new GetLanguageCallback(GetLanguage));
			} else {
				return this.languagesComboBox.Text;
			}
		}

		/// <summary>
		/// Skriver ut ett meddelande i loggen på ett trådsäkert sätt.
		/// </summary>
		/// <param name="message">meddelande</param>
		private void LogMessage(string message) {
			if (logTextbox.InvokeRequired) {
				this.logTextbox.Invoke(new LogMessageCallback(LogMessage), new object[] { message });
			} else {
				logTextbox.Text = message + "\r\n" + logTextbox.Text;
			}
		}

		private static Boolean IsRunningAsWebservice() {
			String windir = System.Environment.GetEnvironmentVariable("WINDIR");
			return new FileInfo(windir + "\\PictogramManager.ini").Exists;
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
				this.statusProgressBar.ProgressBar.Invoke(new SetProgressStyleCallback(SetProgressBarStyle), new object[] { progressBarStyle });
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
				this.statusProgressBar.ProgressBar.Invoke(new CurrentProgressCallback(SetCurrentProgress), new object[] { style, current, max });
			} else {
				this.statusProgressBar.ProgressBar.Style = style;
				this.statusProgressBar.ProgressBar.Maximum = max;
				this.statusProgressBar.ProgressBar.Value = current;
			}

		}

		/// <summary>
		/// Hjälpmetod för att aktivera eller inaktivera många kontroller i taget.
		/// </summary>
		/// <param name="enabled"></param>
		private void SetControlsEnabled(bool enabled) {
			foreach (Control control in new Control[] { verifyLabel, installButton, updateLinkLabel, overwriteCheckbox, zipButton, getZipUrlButton }) {
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
			e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, this.Width, 50));
			e.Graphics.DrawString("Pictograminstalleraren", new Font("Arial", 20), Brushes.SteelBlue, new PointF(10, 10));
			e.Graphics.DrawLine(Pens.Black, new Point(0, 50), new Point(this.Width, 50));
		}



		/// <summary>
		/// Hanterar klick på "Kontrollera"-knappen. Kontrollerar att
		/// kontouppgifterna är giltiga.
		/// </summary>
		private void VerifyLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
			this.currentWorkingThread = new Thread(CheckLogin);
			this.currentWorkingThread.Start();
		}


		/// <summary>
		/// Hanterar klick på "Bläddra"-knappen. Låter användaren
		/// välja målkatalog.
		/// </summary>
		private void BrowseButton_Click(object sender, EventArgs e) {
			folderBrowserDialog.ShowNewFolderButton = true;
			folderBrowserDialog.SelectedPath = pathTextbox.Text;
			folderBrowserDialog.ShowDialog();
			pathTextbox.Text = folderBrowserDialog.SelectedPath;
		}

		private void PictogramInstallerForm_Load(object sender, EventArgs e) {
			/* Installationskatalog */
			//string tempDirectory = System.Environment.GetEnvironmentVariable("temp");
			//DirectoryInfo tempDir = new DirectoryInfo(tempDirectory);

			pathTextbox.Text = @"C:\Picto\WmfSV";

			/* Handler för när formuläret stängs */
			this.Closing += new CancelEventHandler(PictogramInstallerForm_Closing);

			/* Ladda sparade inställningar */
			this.settings = new PropertyFile();

			string path = this.settings.getProperty("path");
			if (path != null && path.Length > 0) {
				pathTextbox.Text = path;
			}

			if (IsRunningAsWebservice()) {
				this.groupBox1.Visible = false;
				this.groupBox2.Location = new Point(12, 62);
				this.usernameTextbox.Text = "webservice";
				this.passwordTextbox.Text = "tbn2wswzcrf4";
			} else {
				this.usernameTextbox.Text = this.settings.getProperty("username");
				this.passwordTextbox.Text = this.settings.getProperty("password");
			}

			/* Ladda ner språk */
			this.languageProvider = new LanguageProvider();
			this.languageProvider.LogMessage += new LogMessageCallback(LogMessage);
			this.currentWorkingThread = new Thread(new ThreadStart(RefreshLanguages));
			currentWorkingThread.Start();

			/* Klass att använda för att kommunicera med webservice. */
			this.downloader = new PictogramDownloader(this.languageProvider);
			this.downloader.LogMessage += new LogMessageCallback(LogMessage);
			this.downloader.ProgressChanged += new CurrentProgressCallback(SetCurrentProgress);
			this.downloader.StatusChanged += new SetStatusCallback(SetStatus);

		}

		/// <summary>
		/// Hanterar att formuläret stängs. Stänger av arbetstråden och sparar inställningar.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void PictogramInstallerForm_Closing(object sender, CancelEventArgs e) {
			try {
				if (this.currentWorkingThread != null) {
					this.currentWorkingThread.Abort();
				}

				/* Spara inställningar */

				if (!IsRunningAsWebservice()) {
					this.settings.setProperty("username", this.usernameTextbox.Text);
					this.settings.setProperty("password", this.passwordTextbox.Text);
				}

				this.settings.setProperty("path", this.pathTextbox.Text);
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
			this.currentWorkingThread = new Thread(new ThreadStart(RefreshLanguages));
			currentWorkingThread.Start();
		}

		/// <summary>
		/// Hanterar klick på installationsknappen. Sätter igång nedladdning.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InstallButton_Click(object sender, EventArgs e) {
			this.currentWorkingThread = new Thread(new ThreadStart(Download));
			currentWorkingThread.Start();
		}

		/// <summary>
		/// Metod för att sätta val för språk på ett trådsäkert sätt.
		/// </summary>
		/// <param name="source"></param>
		private void SetLanguageDataSource(IList source) {
			if (this.languagesComboBox.InvokeRequired) {
				this.languagesComboBox.Invoke(new SetLanguageDataSourceCallback(SetLanguageDataSource), new object[] { source });
			} else {
				this.languagesComboBox.DataSource = source;
				this.languagesComboBox.SelectedIndex = this.languagesComboBox.FindString("Svenska");
			}
		}


		/// <summary>
		/// Hanterar klick på progressbaren. Avbryter nedladdning om sådan pågår.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void StatusProgressBar_Click(object sender, EventArgs e) {
			if (this.currentWorkingThread != null) {
				this.currentWorkingThread.Abort();
				SetStatus("Nedladdning avbruten!");
				SetControlsEnabled(true);
			}
		}

		private void ZipButton_Click(object sender, EventArgs e) {
			this.currentWorkingThread = new Thread(new ThreadStart(DownloadZip));
			currentWorkingThread.Start();
		}

		private void GetZipUrlButton_Click(object sender, EventArgs e) {
			this.currentWorkingThread = new Thread(new ThreadStart(GetZipUrl));
			this.currentWorkingThread.Start();
		}
	}
}