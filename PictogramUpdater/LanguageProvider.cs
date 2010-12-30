using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Net;

namespace PictogramUpdater {

    /// <summary>
    /// Klass f�r att hantera spr�k.
    /// </summary>
    class LanguageProvider {
        private Hashtable languageToLocaleMapping;

        public event LogMessageCallback LogMessage;

        private PictogramRestService pictogramRestService;

        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public LanguageProvider(PictogramRestService pictogramRestService) {
            languageToLocaleMapping = new Hashtable();
            this.pictogramRestService = pictogramRestService;
        }

        /// <summary>
        /// Returnerar spr�kkod givet spr�knamn.
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
        /// H�mtar giltiga spr�k.
        /// </summary>
        public void RefreshLanguages() {
            try {
                
                IList<Language> languages = pictogramRestService.getSwedishLanguageNames();
                foreach(Language language in languages) {
                    var languageName = language.Name;
                    var languageCode = language.Code;

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
        /// Returnerar en lista med spr�k.
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
