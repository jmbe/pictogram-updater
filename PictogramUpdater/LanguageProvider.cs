using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;

namespace DownloadManager {

    /// <summary>
    /// Klass för att hantera språk.
    /// </summary>
    class LanguageProvider {
        private Hashtable languageToLocaleMapping;

        public event LogMessageCallback LogMessage;

        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public LanguageProvider() {
            languageToLocaleMapping = new Hashtable();
        }

        /// <summary>
        /// Returnerar språkkod givet språknamn.
        /// </summary>
        public string GetLocale(string language) {
            if (languageToLocaleMapping.ContainsKey(language)) {
                var locale = languageToLocaleMapping[language] as string;
                if (locale != null) {
                    return locale.ToUpper();                    
                }
            }
            return "";
            
        }

        /// <summary>
        /// Hämtar giltiga språk.
        /// </summary>
        public void RefreshLanguages() {
            try {
                var service = new PictosysWebService();
                var languages = service.getSwedishLanguageNames();
                for (var i = 0; i < languages.Length; i += 2) {
                    languageToLocaleMapping[languages[i]] = languages[i + 1];
                    Console.WriteLine(languages[i]);
                }

            } catch {
                LogMessage("Kunde inte ansluta till server.");
            }
       }

        /// <summary>
        /// Returnerar en lista med språk.
        /// </summary>
        public IList Languages {
            get {
                var result = new string[languageToLocaleMapping.Keys.Count];
                var i = 0;
                foreach (string language in languageToLocaleMapping.Keys) {
                    result[i++] = language;
                }
                return result;
            }
        }
    }
}
