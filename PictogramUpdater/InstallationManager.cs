using System;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    internal class InstallationManager {
        public event LogMessageCallback LogMessage;
        public event CurrentProgressCallback ProgressChanged;
        public event SetStatusCallback StatusChanged;

        private readonly Config _config;
        private readonly DownloadListManager _downloadListManager;

        public InstallationManager(Config config) {
            _config = config;
            _downloadListManager = new DownloadListManager();
        }

        public void Download(string targetPath, Language language, bool overwrite, string username, string password) {
            var completeEntryList = _downloadListManager.GetEntriesToInstall(username, password, language, _config);
            var downloadEntryList = completeEntryList;
            if (!overwrite) {
                downloadEntryList = _downloadListManager.FilterEntries(_config, language,
                                                                       downloadEntryList);
            }

            var downloadManager = GetDownloadManager(targetPath);
            downloadManager.Download(username, password, language, downloadEntryList);

            _config.CommitEntries(language, completeEntryList);
        }

        public void DownloadZip(string targetPath, string username, string password, Language language) {
            var downloadManager = GetDownloadManager(targetPath);
            downloadManager.DownloadZip(username, password, language);
        }

        private DownloadManager GetDownloadManager(string targetPath) {
            var downloadManager = new DownloadManager {TargetPath = targetPath};
            downloadManager.LogMessage += LogMessage;
            downloadManager.ProgressChanged += ProgressChanged;
            downloadManager.StatusChanged += StatusChanged;
            return downloadManager;
        }
    }
}