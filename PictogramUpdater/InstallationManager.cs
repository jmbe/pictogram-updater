using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PictogramUpdater {
    internal class InstallationManager {
        public event LogMessageCallback LogMessage;
        public event CurrentProgressCallback ProgressChanged;
        public event SetStatusCallback StatusChanged;

        private readonly Config _config;
        private readonly DownloadListManager _downloadListManager;
        private LanguageProvider languageProvider;
        private PictosysWebService pictosysWebService;

        public Thread CurrentWorkingThread { get; set; }

        public InstallationManager(Config config, DownloadListManager downloadListManager, LanguageProvider languageProvider, PictosysWebService pictosysWebService) {
            _config = config;
            _downloadListManager = downloadListManager;
            this.languageProvider = languageProvider;
            this.pictosysWebService = pictosysWebService;
        }

        public void Download(string targetPath, Language language, bool overwrite, InstallationType installationType,
                             string username, string password) {
            var completeList = _downloadListManager.GetEntriesToInstall(username, password, language, _config);
            var downloadList = completeList;

            if (!overwrite) {
                downloadList = _downloadListManager.FilterEntries(_config, language,
                                                                  downloadList, installationType);
            }

            var downloadManager = CreateDownloadManager(targetPath, username, password, language);

            downloadManager.DownloadList = downloadList;
            downloadManager.InstallationType = installationType;

            LogMessage("Det finns " + downloadList.Count + " nya filer att ladda ner.");

            CurrentWorkingThread = new Thread(new ThreadStart(downloadManager.Download));
            CurrentWorkingThread.Start();
            CurrentWorkingThread.Join();

            

            if (InstallationType.CODE.Equals(installationType) && downloadList.Count > 0) {
                LogMessage("Uppdaterar ini-fil");
                _config.CommitEntries(language, completeList);
                LogMessage("Ini-fil uppdaterad");
            }
        }

        public void DownloadZip(string targetPath, string username, string password, Language language) {
            var downloadManager = CreateDownloadManager(targetPath, username, password, language);
            downloadManager.DownloadZip(username, password, language);
        }

        private DownloadManager CreateDownloadManager(string targetPath, string username, string password,
                                                   Language language) {
            var downloadManager = new DownloadManager(this.languageProvider, this.pictosysWebService);

            downloadManager.TargetPath = targetPath;
            downloadManager.Username = username;
            downloadManager.Password = password;
            downloadManager.Language = language;

            downloadManager.LogMessage += LogMessage;
            downloadManager.ProgressChanged += ProgressChanged;
            downloadManager.StatusChanged += StatusChanged;
            return downloadManager;
        }
    }
}