using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace PictogramUpdater {
    public class LanguageSelection {
        private Language _language = Language.SWEDISH;
        private ImageFormat imageFormat = new ImageFormat("wmf", "wmf"); // todo get from provider

        internal event LanguageChangedCallback LanguageChanged;

        public LanguageSelection() {
        }

        public LanguageSelection(Language language, ImageFormat imageFormat) {
            this._language = language;
            this.imageFormat = imageFormat;
        }

        private void onChange() {
            if (LanguageChanged != null) {
                LanguageChanged.Invoke();
            }
        }

        public Language Language {
            get { return _language; }
            set {
                _language = value;
                onChange();
            }
        }

        public ImageFormat ImageFormat {
            get {
                return this.imageFormat;
            }
            set {
                this.imageFormat = value;
                onChange();
            }
        }

        public string CapitalizedExtension {
            get {
                return imageFormat.CapitalizedExtension;
            }
        }

        public string LanguageCode {
            get {
                return Language.Code.ToUpper();
            }
        }

        public bool IsVectorFormat {
            get {
                return imageFormat.IsVectorFormat;
            }
        }
    }

    public class Language {

        public static readonly Language SWEDISH = new Language("sv", "Svenska");

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

        public override string ToString() {
            return Code;
        }
    }
}