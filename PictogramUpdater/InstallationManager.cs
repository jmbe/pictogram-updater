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

        public InstallationManager(Config config) {
            _config = config;
            _downloadListManager = new DownloadListManager();
        }

        public void Download(string targetPath, Language language, bool overwrite, string username, string password) {
            var completeList = _downloadListManager.GetEntriesToInstall(username, password, language, _config);
            var downloadList = completeList;
            if (!overwrite) {
                downloadList = _downloadListManager.FilterEntries(_config, language,
                                                                       downloadList);
            }

            var downloadManager = GetDownloadManager(targetPath, username, password, language);
            downloadManager.DownloadList = downloadList;
            
            var currentWorkingThread = new Thread(new ThreadStart(downloadManager.Download));
            currentWorkingThread.Start();
            currentWorkingThread.Join();

            _config.CommitEntries(language, completeList);
        }

        public void DownloadZip(string targetPath, string username, string password, Language language) {
            var downloadManager = GetDownloadManager(targetPath, username, password, language);
            downloadManager.DownloadZip(username, password, language);
        }

        private DownloadManager GetDownloadManager(string targetPath, string username, string password, Language language) {
            var downloadManager = new DownloadManager {TargetPath = targetPath, Username = username, Password = password, Language = language};
            downloadManager.LogMessage += LogMessage;
            downloadManager.ProgressChanged += ProgressChanged;
            downloadManager.StatusChanged += StatusChanged;
            return downloadManager;
        }
    }
}