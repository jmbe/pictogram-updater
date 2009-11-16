using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager {
    internal class LanguageSelection {
        private string _locale = "SV";
        private Language _language = new Language("SV", "Svenska");

        public event LanguageChangedCallback LanguageChanged;

        public Language Language {
            get { return _language; }
            set {
                _language = value;
                LanguageChanged.Invoke();
            }
        }
    }

    internal class Language {
        private readonly string _name;
        private readonly string _code;

        public Language(string code, string name) {
            _code = code;
            _name = name;
        }

        public string Name {
            get { return _name; }
        }

        public string Code {
            get { return _code; }
        }
    }
}