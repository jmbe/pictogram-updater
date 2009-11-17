using System;
using System.Collections.Generic;
using System.IO;

namespace PictogramUpdater {
    internal class DownloadListManager {
        /// <summary>
        /// Download phrases from service and then create a list of PictogramEntries from it.
        /// The list should be filtered from files already existing on disk
        /// </summary>
        /// <param name="password"></param>
        /// <param name="language"></param>
        /// <param name="username"></param>
        /// <param name="config"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public List<PictogramEntry> GetEntriesToInstall(string username, string password, Language language, Config config, bool filter) {
            var entries = GetEntries(username, password, language);
            if(filter) {
                entries = FilterEntries(config, language, entries);    
            }
            
            return entries;
        }

        private static List<PictogramEntry> GetEntries(string username, string password, Language language) {
            var entries = new List<PictogramEntry>();
            var service = new PictosysWebService();
            var phrases = service.getPictogramPhrasesByLocale(username, password, language.Code.ToLower());

            for (var i = 0; i + 1 < phrases.Length; i = i + 2) {
                var name = phrases[i];
                var code = phrases[i + 1];
                entries.Add(new PictogramEntry(code, name));
            }
            return entries;
        }

        private static List<PictogramEntry> FilterEntries(Config config, Language language,
                                                          IEnumerable<PictogramEntry> entries) {
            var installPath = config.GetPictoWmfInstallPath(language);
            var extension = config.GetExtension(language);

            var newEntries = new List<PictogramEntry>();

            foreach (var entry in entries) {
                var fileInfo = new FileInfo(installPath + @"\" + entry.FullCode + "." + extension);
                if (!fileInfo.Exists) {
                    newEntries.Add(entry);
                }
            }

            return newEntries;
        }
    }
}