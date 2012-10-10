using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PictogramUpdater {
    internal class InstallationManager {
        public event LogMessageCallback LogMessage;
        public event LogToFileCallback LogToFile;
        public event CurrentProgressCallback ProgressChanged;
        public event SetStatusCallback StatusChanged;

        private readonly Config _config;
        private readonly DownloadListManager _downloadListManager;
        private LanguageProvider languageProvider;
        private PictogramRestService pictogramRestService;

        public Thread CurrentWorkingThread { get; set; }

        public InstallationManager(Config config, DownloadListManager downloadListManager, LanguageProvider languageProvider, PictogramRestService pictogramRestService) {
            _config = config;
            _downloadListManager = downloadListManager;
            this.languageProvider = languageProvider;
            this.pictogramRestService = pictogramRestService;
        }

        public void Download(string targetPath, Language language, bool overwrite, InstallationType installationType,
                             string username, string password) {

            
            DownloadList downloadList = _downloadListManager.GetEntriesToInstall(username, password, language, installationType, overwrite);
            
           
            var downloadManager = CreateDownloadManager(targetPath, username, password, language);

            downloadManager.DownloadList = downloadList.Missing;
            downloadManager.InstallationType = installationType;

            LogMessage(TextResources.thereAre + " " + downloadList.Missing.Count + " " + TextResources.newFilesToDownload);
            downloadManager.Download();

            if (InstallationType.CODE.Equals(installationType)) {
                LogMessage(TextResources.updatingIniFile);
                _config.CommitEntries(language, downloadList.All);
                LogMessage(TextResources.iniFileUpdated);
            }
        }

        private DownloadManager CreateDownloadManager(string targetPath, string username, string password,
                                                   Language language) {
            var downloadManager = new DownloadManager(this.languageProvider, this.pictogramRestService);

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