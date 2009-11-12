using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    internal class LanguageSelection {
        private string _language = "SV";

        public event LanguageChangedCallback LanguageChanged;

        public string Language {
            get { return _language; }
            set {
                _language = value;
                LanguageChanged.Invoke();
            }
        }
    }
}