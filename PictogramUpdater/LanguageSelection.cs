using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    internal class LanguageSelection {
        private string _locale = "SV";

        public event LanguageChangedCallback LanguageChanged;

        public string Locale {
            get { return _locale; }
            set {
                _locale = value;
                LanguageChanged.Invoke();
            }
        }
    }
}