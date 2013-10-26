using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace PictogramUpdater {
    public class PictogramEntry : IComparable<PictogramEntry> {
        private readonly Regex _indexPattern = new Regex(@"\d+$");
        private readonly Regex removeInvalidChars = new Regex(String.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))), RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private string discriminator;

        /// 
        /// Throws FormatException when incorrectly named filenames are encounted.
        /// 
        /// FullCode is expected to be formatted like a30, j1 etc.
        /// Name is the translation of the image.
        /// 
        public PictogramEntry(string fullCode, string name, string discriminator, DateTime modified) {
            FullCode = fullCode;
            Name = name;
            Modified = modified;
            this.discriminator = discriminator;
            var indexMatch = _indexPattern.Match(fullCode);
            CategoryCode = fullCode.Substring(0, indexMatch.Index);
            Index = Convert.ToInt32(fullCode.Substring(indexMatch.Index));
        }

        public string FullCode { get; set; }
        public string Name { get; set; }
        public DateTime Modified { get; set; }

        public string CategoryCode { get; private set; }

        public int Index { get; private set; }

        public string ToFilename(InstallationType installationType, LanguageSelection selection) {

            if (InstallationType.SOUND.Equals(installationType)) {
                return FullCode + ".wav";
            }

            if (InstallationType.PLAIN_TEXT.Equals(installationType)) {
                string extra = "";
                if (!string.IsNullOrEmpty(discriminator)) {
                    extra = " (" + this.discriminator + ")";
                }
                return LegalFilename + extra + "." + selection.ImageFormat.Extension;
            }

            // InstallationType.CODE
            return FullCode + "." + selection.ImageFormat.Extension;
        }

        private string LegalFilename {
            get {
                return removeInvalidChars.Replace(Name.Trim(), "");
            }
        }

        public int CompareTo(PictogramEntry other) {
            var result = CategoryCode.CompareTo(other.CategoryCode);
            if (result == 0) {
                result = Index.CompareTo(other.Index);
            }

            return result;
        }
    }
}
