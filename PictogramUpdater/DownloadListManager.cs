﻿using System;
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
        /// <returns></returns>
        public List<PictogramEntry> GetEntriesToInstall(string username, string password, Language language, Config config) {
            var entries = GetEntries(username, password, language);
            entries.Sort();
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

        public List<PictogramEntry> FilterEntries(Config config, Language language,
                                                          IEnumerable<PictogramEntry> entries, bool clearText, bool sound) {
            string installPath; 
            if(clearText) {
                installPath = config.GetPictoClearTextInstallPath(language);
            } else if(sound) {
                installPath = config.GetPictoSoundInstallPath(language);
            } else {
                installPath = config.GetPictoInstallPath(language);    
            }

            var extension = sound ? "wav" : "wmf";

            var newEntries = new List<PictogramEntry>();

            foreach (var entry in entries) {
                var fileName = clearText ? entry.Name.Trim() : entry.FullCode;
                var fileInfo = new FileInfo(installPath + @"\" + fileName + "." + extension);
                if (!fileInfo.Exists) {
                    newEntries.Add(entry);
                }
            }

            return newEntries;
        }
    }
}