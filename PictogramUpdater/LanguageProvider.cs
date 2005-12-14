using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;

namespace PictogramUpdater {

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
            this.languageToLocaleMapping = new Hashtable();
        }

        /// <summary>
        /// Returnerar språkkod givet språknamn.
        /// </summary>
        public string GetLocale(string language) {
            if (languageToLocaleMapping.ContainsKey(language)) {
                return (string) languageToLocaleMapping[language];
            } else {
                return "";
            }
        }

        /// <summary>
        /// Hämtar giltiga språk.
        /// </summary>
        public void RefreshLanguages() {
            try {
                PictosysWebService service = new PictosysWebService();
                string[] languages = service.getSwedishLanguageNames();
                for (int i = 0; i < languages.Length; i += 2) {
                    this.languageToLocaleMapping[languages[i]] = languages[i + 1];
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
                string[] result = new string[languageToLocaleMapping.Keys.Count];
                int i = 0;
                foreach (string language in languageToLocaleMapping.Keys) {
                    result[i++] = language;
                }
                return result;
            }
        }
    }
}
