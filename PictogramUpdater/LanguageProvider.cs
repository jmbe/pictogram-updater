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
        /// <summary>
        /// Maps Svenska -> SV
        /// </summary>
        private Hashtable languageToLocaleMapping;
        /// <summary>
        /// Maps sv -> Svenska (key is always lowercase)
        /// </summary>
        private Hashtable codeToDisplayNameMapping;

        public event LogMessageCallback LogMessage;
        public event LogToFileCallback LogToFile;

        private PictogramRestService pictogramRestService;

        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public LanguageProvider(PictogramRestService pictogramRestService) {
            languageToLocaleMapping = new Hashtable();
            codeToDisplayNameMapping = new Hashtable();
            this.pictogramRestService = pictogramRestService;
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

        public String getNativeName(string languageCode) {
            if (codeToDisplayNameMapping.ContainsKey(languageCode)) {
                var name = codeToDisplayNameMapping[languageCode] as string;
                if (name != null) {
                    return name;
                }
            }
            return "";
        }

        /// <summary>
        /// Hämtar giltiga språk.
        /// </summary>
        public void RefreshLanguages() {
            try {

                IList<Language> languages = pictogramRestService.getForeignLanguageNames();
                foreach(Language language in languages) {
                    var languageName = language.Name;
                    var languageCode = language.Code;

                    if (SkippedLanguageCodes.Contains(languageCode)) {
                        continue;
                    }
    
                    languageToLocaleMapping[languageName] = languageCode;
                    codeToDisplayNameMapping[languageCode.ToLower()] = languageName;
                }

            } catch (Exception e) {
                LogMessage(TextResources.couldNotConnectToServer);
                LogToFile(e.ToString());
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
