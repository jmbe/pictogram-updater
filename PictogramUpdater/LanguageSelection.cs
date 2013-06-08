using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    internal class LanguageSelection {
        private Language _language = new Language("sv", "Svenska");

        public event LanguageChangedCallback LanguageChanged;

        public Language Language {
            get { return _language; }
            set {
                _language = value;
                LanguageChanged.Invoke();
            }
        }
    }

    public class Language {
        public Language(string code, string name) {
            Code = code;
            Name = name;
        }

        public string Name {
            get;
            private set;
        }

        public string Code {
            get;
            private set;
        }

        /// <summary>
        /// Swedish is special since it is the only language which should have sounds enabled.
        /// </summary>
        public bool IsSwedish {
            get {
                return "sv".ToLower().Equals(Code.ToLower());
            }
        }

        public bool IsTextless {
            get {
                return "xx".Equals(Code.ToLower());
            }
        }
    }
}