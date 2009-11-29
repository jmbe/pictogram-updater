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

        private PictosysWebService pictosysWebService;

        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public LanguageProvider(PictosysWebService pictosysWebService) {
            languageToLocaleMapping = new Hashtable();
            this.pictosysWebService = pictosysWebService;
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
                
                var languages = pictosysWebService.getSwedishLanguageNames();
                for (var i = 0; i < languages.Length; i += 2) {
                    var languageName = languages[i];
                    var languageCode =languages[i + 1];

                    

                    if (SkippedLanguageCodes.Contains(languageCode)) {
                        continue;
                    }
    
                    languageToLocaleMapping[languageName] = languageCode;
                }

            } catch {
                LogMessage("Kunde inte ansluta till server.");
            }
       }

        private IList<string> SkippedLanguageCodes {
            get {
                List<string> skippedLanguagesCodes = new List<string>();
                skippedLanguagesCodes.Add("lv");
                skippedLanguagesCodes.Add("lt");
                skippedLanguagesCodes.Add("pl");
                skippedLanguagesCodes.Add("ru");

                return skippedLanguagesCodes;
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
