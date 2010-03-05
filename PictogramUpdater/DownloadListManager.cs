using System;
using System.Collections.Generic;
using System.IO;

namespace PictogramUpdater {
    internal class DownloadListManager {

        private PictosysWebService pictosysWebService;
        private Config config;

        public DownloadListManager(PictosysWebService pictosysWebService, Config config) {
            this.pictosysWebService = pictosysWebService;
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
            var result = new List<PictogramEntry>();

            var sounds = this.pictosysWebService.getAvailableSoundsByLocale(username, password, language.Code.ToLower());

            foreach (string code in sounds) {
                try {
                    result.Add(new PictogramEntry(code, ""));
                } catch (FormatException e) {
                    /* Ignored. Some incorrect names are expected. */
                }
            }

            return result;
        }

        private List<PictogramEntry> GetEntries(string username, string password, Language language) {
            var result = new List<PictogramEntry>();
            
            var phrases = this.pictosysWebService.getPictogramPhrasesByLocale(username, password, language.Code.ToLower());

            for (var i = 0; i + 1 < phrases.Length; i = i + 2) {
                var name = phrases[i];
                var code = phrases[i + 1];
                try {
                    result.Add(new PictogramEntry(code, name));
                } catch (FormatException e) {
                    /* Ignored. Some incorrect names are expected. */
                }

            }
            return result;
        }

        private List<PictogramEntry> FilterEntries(Language language,
                                                          IEnumerable<PictogramEntry> entries, InstallationType installationType) {

            string installPath = this.config.getInstallPathForLanguage(language, installationType);
            
            var newEntries = new List<PictogramEntry>();

            foreach (var entry in entries) {
                var fileName = entry.ToFilename(installationType);

                var fileInfo = new FileInfo(installPath + @"\" + fileName);
                if (!fileInfo.Exists || fileInfo.Length == 0) {
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