﻿using System;
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

        public void Download(string targetPath, LanguageSelection selection, bool overwrite, InstallationType installationType,
                             string username, string password) {

            LogToFile("Downloading " + selection + " files of type " + installationType + " to " + targetPath);

            bool refresh = _config.NeedsRefresh(selection, installationType);
            DateTimeOffset refreshedAt = DateTimeOffset.Now;

            DownloadList downloadList = _downloadListManager.GetEntriesToInstall(targetPath, username, password, selection, installationType, overwrite || refresh);

            var downloadManager = CreateDownloadManager(targetPath, username, password, selection.Language);

            downloadManager.DownloadList = downloadList.Missing;
            downloadManager.InstallationType = installationType;
            downloadManager.LanguageSelection = selection;

            LogMessage(TextResources.thereAre + " " + downloadList.Missing.Count + " " + TextResources.newFilesToDownload);
            downloadManager.Download();

            if (InstallationType.CODE.Equals(installationType)) {
                LogMessage(TextResources.updatingIniFile);
                _config.CommitEntries(selection, downloadList.All);
                LogMessage(TextResources.iniFileUpdated);
            }

            if (refresh) {
                _config.UpdateRefresh(selection, installationType, refreshedAt);
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
            downloadManager.LogToFile += LogToFile;
            downloadManager.ProgressChanged += ProgressChanged;
            downloadManager.StatusChanged += StatusChanged;
            return downloadManager;
        }
    }
}