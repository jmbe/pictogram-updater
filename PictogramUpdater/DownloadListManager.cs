using System;
using System.Collections.Generic;
using System.IO;

namespace PictogramUpdater {
    internal class DownloadListManager {

        private PictogramRestService pictogramRestService;
        private Config config;

        public DownloadListManager(PictogramRestService pictogramRestService, Config config) {
            this.pictogramRestService = pictogramRestService;
            this.config = config;
        }

        public DownloadList GetEntriesToInstall(string username, string password, Language language, InstallationType installationType, bool overwrite) {

            List<PictogramEntry> all = GetCompleteList(username, password, language, installationType);

            var missing = overwrite ? all : FilterEntries(language, all, installationType);

            return new DownloadList(all, missing);
        }

        private List<PictogramEntry> GetCompleteList(string username, string password, Language language, InstallationType installationType) {
            List<PictogramEntry> completeList = null;

            switch (installationType) {
                case InstallationType.SOUND:
                    completeList = GetSoundEntries(username, password, language);
                    break;
                default:
                    completeList = GetEntriesToInstall(username, password, language);
                    break;
            }
            return completeList;
        }

        /// <summary>
        /// Download phrases from service and then create a list of PictogramEntries from it.
        /// The list should be filtered from files already existing on disk
        /// </summary>
        /// <param name="password"></param>
        /// <param name="language"></param>
        /// <param name="username"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private List<PictogramEntry> GetEntriesToInstall(string username, string password, Language language) {
            var entries = GetEntries(username, password, language);
            entries.Sort();
            return entries;
        }

        private List<PictogramEntry> GetSoundEntries(String username, String password, Language language) {
            List<PictogramEntry> sounds = this.pictogramRestService.getAvailableSoundsByLanguage(language.Code.ToLower());
            return sounds;
        }

        private List<PictogramEntry> GetEntries(string username, string password, Language language) {
            List<PictogramEntry> phrases = this.pictogramRestService.getPictogramPhrasesByLanguage(language);
            return phrases;
        }

        private List<PictogramEntry> FilterEntries(Language language,
                                                          IEnumerable<PictogramEntry> entries, InstallationType installationType) {

            string installPath = this.config.getInstallPathForLanguage(language, installationType);
            
            var newEntries = new List<PictogramEntry>();

            foreach (var entry in entries) {
                var fileName = entry.ToFilename(installationType);

                var fileInfo = new FileInfo(installPath + @"\" + fileName);
                if (!fileInfo.Exists || fileInfo.Length == 0  || fileInfo.LastWriteTime.CompareTo(entry.Modified) < 0) {
                    newEntries.Add(entry);
                }
            }

            return newEntries;
        }
    }


    public class DownloadList {

        public DownloadList(List<PictogramEntry> all, List<PictogramEntry> missing) {
            All = all;
            Missing = missing;
        }

        public List<PictogramEntry> All {
            get;
            private set;
        }

        public List<PictogramEntry> Missing {
            get;
            private set;
        }



    }
}