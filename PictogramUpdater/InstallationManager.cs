﻿using System;
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

            
            DownloadList downloadList = _downloadListManager.GetEntriesToInstall(username, password, language, installationType, overwrite);
            
           
            var downloadManager = CreateDownloadManager(targetPath, username, password, language);

            downloadManager.DownloadList = downloadList.Missing;
            downloadManager.InstallationType = installationType;

            LogMessage("Det finns " + downloadList.Missing.Count + " nya filer att ladda ner.");
            downloadManager.Download();

            if (InstallationType.CODE.Equals(installationType)) {
                LogMessage("Uppdaterar ini-fil...");
                _config.CommitEntries(language, downloadList.All);
                LogMessage("Ini-fil uppdaterad.");
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