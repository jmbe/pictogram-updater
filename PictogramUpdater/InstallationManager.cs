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

        public Thread CurrentWorkingThread { get; set; }

        public InstallationManager(Config config, DownloadListManager downloadListManager) {
            _config = config;
            _downloadListManager = downloadListManager;
        }

        public void Download(string targetPath, Language language, bool overwrite, bool plainText, bool sound,
                             string username, string password) {
            var completeList = _downloadListManager.GetEntriesToInstall(username, password, language, _config);
            var downloadList = completeList;
            if (!overwrite) {
                downloadList = _downloadListManager.FilterEntries(_config, language,
                                                                  downloadList, plainText, sound);
            }

            var downloadManager = GetDownloadManager(targetPath, username, password, language);
            downloadManager.DownloadList = downloadList;
            downloadManager.ClearText = plainText;
            downloadManager.Sound = sound;

            LogMessage("Startar delinstallation.");
            
            CurrentWorkingThread = new Thread(new ThreadStart(downloadManager.Download));
            CurrentWorkingThread.Start();
            CurrentWorkingThread.Join();

            LogMessage("Delinstallation klar.");

            if (!plainText && ! sound) {
                LogMessage("Uppdaterar ini-fil");
                _config.CommitEntries(language, completeList);
                LogMessage("Ini-fil uppdaterad");
            }
        }

        public void DownloadZip(string targetPath, string username, string password, Language language) {
            var downloadManager = GetDownloadManager(targetPath, username, password, language);
            downloadManager.DownloadZip(username, password, language);
        }

        private DownloadManager GetDownloadManager(string targetPath, string username, string password,
                                                   Language language) {
            var downloadManager = new DownloadManager {
                                                          TargetPath = targetPath,
                                                          Username = username,
                                                          Password = password,
                                                          Language = language
                                                      };
            downloadManager.LogMessage += LogMessage;
            downloadManager.ProgressChanged += ProgressChanged;
            downloadManager.StatusChanged += StatusChanged;
            return downloadManager;
        }
    }
}