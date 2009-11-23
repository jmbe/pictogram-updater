using System;
using System.Collections.Generic;
using System.IO;

namespace PictogramUpdater {
    internal class DownloadListManager {

        private PictosysWebService pictosysWebService;

        public DownloadListManager(PictosysWebService pictosysWebService) {
            this.pictosysWebService = pictosysWebService;
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
        public List<PictogramEntry> GetEntriesToInstall(string username, string password, Language language, Config config) {
            var entries = GetEntries(username, password, language);
            entries.Sort();
            return entries;
        }

        

        private List<PictogramEntry> GetEntries(string username, string password, Language language) {
            var result = new List<PictogramEntry>();
            
            var phrases = this.pictosysWebService.getPictogramPhrasesByLocale(username, password, language.Code.ToLower());

            for (var i = 0; i + 1 < phrases.Length; i = i + 2) {
                var name = phrases[i];
                var code = phrases[i + 1];
                result.Add(new PictogramEntry(code, name));
            }
            return result;
        }

        public List<PictogramEntry> FilterEntries(Config config, Language language,
                                                          IEnumerable<PictogramEntry> entries, InstallationType installationType) {

            string installPath = config.getInstallPathForLanguage(language, installationType);
            
            var newEntries = new List<PictogramEntry>();

            foreach (var entry in entries) {
                var fileName = entry.ToFilename(installationType);

                var fileInfo = new FileInfo(installPath + @"\" + fileName);
                if (!fileInfo.Exists) {
                    newEntries.Add(entry);
                }
            }

            return newEntries;
        }
    }
}