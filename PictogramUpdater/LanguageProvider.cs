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
        private Dictionary<string, string> languageToLocaleMapping;
        /// <summary>
        /// Maps sv -> Svenska (key is always lowercase)
        /// </summary>
        private Dictionary<string, string> codeToDisplayNameMapping;

        public event LogMessageCallback LogMessage;
        public event LogToFileCallback LogToFile;

        private PictogramRestService pictogramRestService;

        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public LanguageProvider(PictogramRestService pictogramRestService) {
            languageToLocaleMapping = new Dictionary<string, string>();
            codeToDisplayNameMapping = new Dictionary<string, string>();
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
                foreach (Language language in languages) {
                    AddLanguage(language);
                }

                AddLanguage(new Language("xx", "(" + TextResources.noText + ")"));
            } catch (Exception e) {
                LogMessage(TextResources.couldNotConnectToServer);
                LogToFile(e.ToString());
            }
       }

        private void AddLanguage(Language language) {
            var languageName = language.Name;
            var languageCode = language.Code;

            if (SkippedLanguageCodes.Contains(languageCode)) {
                return;
            }

            languageToLocaleMapping[languageName] = languageCode;
            codeToDisplayNameMapping[languageCode.ToLower()] = languageName;
        }

        private IList<string> SkippedLanguageCodes {
            get {
                List<string> skippedLanguagesCodes = new List<string>();
                /* Skip some languages because their translations cannot be written properly to an ISO-8859-1 ini file. */
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
        public IList<string> Languages {
            get {
                List<string> result = new List<string>();

                foreach (string language in languageToLocaleMapping.Keys) {
                    result.Add(language);
                }

                result.Sort();

                return result;
            }
        }
    }
}
