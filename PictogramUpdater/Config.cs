using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using AMS.Profile;

namespace PictogramUpdater {
    internal class Config {
        private List<PictogramEntry> _queue;
        private CategoryRepository categoryRepository;
        private CategoryTranslationService categoryTranslationService;
        private IniFileFactory iniFileFactory;

        public Config(CategoryRepository categoryRepository, CategoryTranslationService categoryTranslationService, IniFileFactory iniFileFactory) {
            _queue = new List<PictogramEntry>();
            this.categoryRepository = categoryRepository;
            this.categoryTranslationService = categoryTranslationService;
            this.iniFileFactory = iniFileFactory;
        }

        public bool IsPictoWmfInstalled(LanguageSelection selection) {
            var picWmf = this.iniFileFactory.CreatePictoWmfIni(selection);
            return picWmf.Exists;
        }



        private string GetPictoInstallPath(LanguageSelection selection) {
            Language language = selection.Language;
            if (language.IsTextless) {
                return GetTextLessInstallPath(selection);
            }

            Profile settings = this.iniFileFactory.CreatePictoWmfIni(selection).ToIni();
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultPath(selection);
        }

        private string GetPictoPlainTextInstallPath(LanguageSelection selection) {
            Profile settings = this.iniFileFactory.CreatePictoWmfIni(selection).ToIni();
            var path = settings.GetValue("ProgDir", "PlainTextDir") as string;
            return path ?? GetDefaultPlainTextPath(selection);
        }

        private String GetTextLessInstallPath(LanguageSelection selection) {
            Profile settings = this.iniFileFactory.CreatePictoWmfIni(selection).ToIni();
            var path = settings.GetValue("ProgDir", "TextlessDir") as string;
            return path ?? GetDefaultTextlessDir(selection);
        }

        public string getInstallPathForLanguage(LanguageSelection selection, InstallationType installationType) {
            switch (installationType) {
                case InstallationType.PLAIN_TEXT:
                    return GetPictoPlainTextInstallPath(selection);
                case InstallationType.SOUND:
                    return GetPictoSoundInstallPath(selection.Language);
                case InstallationType.TEXTLESS:
                    /* fall-through */
                case InstallationType.CODE:
                    /* fall-through */
                default:
                    return GetPictoInstallPath(selection);
            }

        }

        public bool NeedsRefresh(LanguageSelection selection, InstallationType installationType) {
            string key = GetLastRefreshKey(selection, installationType);

            Profile settings = this.iniFileFactory.GetPictoIniFile(selection, installationType).ToIni();

            string unparsed = settings.GetValue("ProgDir", key) as string;

            if (string.IsNullOrEmpty(unparsed)) {
                return true;
            }

            try {
                DateTimeOffset dto = DateTimeOffset.Parse(unparsed);

                /* Set to later date to force refresh at a later time. */
                DateTimeOffset cutoff = DateTimeOffset.Parse("2013-07-10 21:00:00 +02:00");
                if (dto.CompareTo(cutoff) < 0) {
                    return true;
                }

            } catch (FormatException) {
                return true;
            }

            return false;
        }

        public void UpdateRefresh(LanguageSelection selection, InstallationType installationType, DateTimeOffset refreshedAt) {
            string key = GetLastRefreshKey(selection, installationType);
            Profile settings = this.iniFileFactory.GetPictoIniFile(selection, installationType).ToIni();
            settings.SetValue("ProgDir", key, refreshedAt);
        }

        private string GetLastRefreshKey(LanguageSelection selection, InstallationType installationType) {
            switch (installationType) {
                case InstallationType.PLAIN_TEXT:
                    return "PlainTextRefresh";
                case InstallationType.TEXTLESS:
                    return "TextlessRefresh";
                case InstallationType.SOUND:
                    return "SoundRefresh";
                case InstallationType.CODE:
                /* fall-through */
                default:
                    return "DirRefresh";
            }
        }

        public string GetPictoSoundInstallPath(Language language) {
            Profile settings = this.iniFileFactory.GetPictoWavIniFilePath(language).ToIni();
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultSoundPath(language);
        }

        public void CreateOrUpdateWmfIni(LanguageSelection selection, string installPath, string plainTextInstallPath) {
            Language language = selection.Language;
            if (!IsPictoWmfInstalled(selection)) {
                CreateNewIniFile(selection);
            }

            /* WmfIni */

            PicIni picWmf = this.iniFileFactory.CreatePictoWmfIni(selection);
            Profile profile = picWmf.ToIni();

            try {
                if (!language.IsTextless) {
                    profile.SetValue("ProgDir", "Dir", installPath);
                }

                /* Extension in ini file should be all upper case. */
                string extension = selection.ImageFormat.AllCapsExtension;

                safeWriteToProfile(profile, "ProgDir", "Extension", extension);
                if (!language.IsTextless) {
                    safeWriteToProfile(profile, "ProgDir", "lang" + extension, language.Code);
                }
                safeWriteToProfile(profile, "ProgDir", "ver" + extension, "7.0");
                safeWriteToProfile(profile, "ProgDir", "CD", "-");
                safeWriteToProfile(profile, "ProgDir", "IDNAME", "");

                // Place PlainTextDir last in section.
                if (!language.IsTextless) {
                    profile.SetValue("ProgDir", "PlainTextDir", plainTextInstallPath);
                }

                profile.SetValue("ProgDir", "TextlessDir", language.IsTextless ? installPath : GetTextLessInstallPath(selection));

            } catch (System.ComponentModel.Win32Exception e) {
                throw new UnauthorizedAccessException("Access to the path '" + picWmf.Path + "' is denied.", e);
            }

            UpdateCategories(profile, language);

        }

        private void UpdateCategories(Profile profile, Language language) {
            if (language.IsTextless) {
                return;
            }


            var categories = this.categoryRepository.FindAll();
            profile.SetValue("Grupper", "Antal", categories.Count);

            foreach (var category in categories) {
                var translation = this.categoryTranslationService.Translate(category, language);

                // [Grupper]1=People
                safeWriteToProfile(profile, "Grupper", category.Index.ToString(), translation);

                safeWriteToProfile(profile, translation, "Antal", "0");
                safeWriteToProfile(profile, translation, "Kod", category.Code);
            }
        }

        /// <summary>
        /// Writes a value only if a previous value does not already exist.
        /// </summary>
        private void safeWriteToProfile(Profile profile, string section, string entry, string value) {
            if (profile.HasEntry(section, entry)) {
                return;
            }

            profile.SetValue(section, entry, value);
        }

        public void CreateOrUpdateWavIni(Language language, string soundInstallPath) {
            /* WavIni */
            var profile = this.iniFileFactory.GetPictoWavIniFilePath(language).ToIni();
            profile.SetValue("ProgDir", "Dir", soundInstallPath);
            if (profile.GetValue("ProgDir", "Extension") == null) {
                profile.SetValue("ProgDir", "Extension", "wav");
            }
            if (profile.GetValue("ProgDir", "lang") == null) {
                profile.SetValue("ProgDir", "lang", language.Code);
            }
            if (profile.GetValue("ProgDir", "ver") == null) {
                profile.SetValue("ProgDir", "ver", "7.0");
            }
        }

        private void CreateNewIniFile(LanguageSelection selection) {
            var info = this.iniFileFactory.CreatePictoWmfIni(selection).ToFileInfo();
            info.Create().Close();
            
            
        }

        public string GetDefaultPath(LanguageSelection selection) {
            return @"C:\Picto\" + selection.CapitalizedExtension + selection.LanguageCode; ;
        }

        public string GetDefaultPlainTextPath(LanguageSelection selection) {
            return @"C:\Picto\" + selection.ImageFormat.CapitalizedExtension + selection.LanguageCode + " " + TextResources.inPlainText;
        }

        private string GetDefaultTextlessDir(LanguageSelection selection) {
            return @"C:\Picto\" + selection.CapitalizedExtension;
        }

        public string GetDefaultSoundPath(Language language) {
            return @"C:\Picto\Wav" + language.Code.ToUpper();
        }

        public void CommitEntries(LanguageSelection selection, List<PictogramEntry> entries) {
            Profile profile = this.iniFileFactory.CreatePictoWmfIni(selection).ToIni();
            Language language = selection.Language;

            var categoryCounts = new Dictionary<Category, int>();
            foreach (var entry in entries) {
                var category = this.categoryRepository.FindByCode(entry.CategoryCode);
                var categoryCount = 0;
                var categoryTranslation = this.categoryTranslationService.Translate(category, language);
                if (categoryCounts.ContainsKey(category)) {
                    categoryCount = categoryCounts[category];
                }
                profile.SetValue(categoryTranslation, entry.FullCode, entry.Name);
                categoryCount++;
                categoryCounts[category] = categoryCount;
            }
            foreach (var category in categoryCounts.Keys) {
                profile.SetValue(categoryTranslationService.Translate(category, language), "Antal", categoryCounts[category]);
            }
        }

        /// <summary>
        /// A generic picwmf.ini (without language in the name) is required for Pictogram Manager.
        /// </summary>
        /// <param name="language"></param>
        public void CreateGenericPicWmfIni(LanguageSelection selection) {
            PicIni picIni = this.iniFileFactory.CreatePictoWmfIni(selection);
            var withoutLanguage = new LanguageSelection();
            withoutLanguage.ImageFormat = selection.ImageFormat;
            withoutLanguage.Language = null;
            PicIni picGeneric = this.iniFileFactory.CreatePictoWmfIni(withoutLanguage);
            var picGenericFile = picGeneric.ToFileInfo();

            /* Rescue some values from file that will be overwritten */
            if (picGenericFile.Exists ) {
                picIni.ToIni().SetValue("ProgDir", "TextlessRefresh", picGeneric.ToIni().GetValue("ProgDir", "TextlessRefresh", ""));
            }

            /* Only overwrite the generic file with the Swedish file, or create new file in any language. */
            if (selection.Language.IsSwedish || !picGenericFile.Exists) {
                picIni.ToFileInfo().CopyTo(picGenericFile.FullName, true);
            }
        }
    }


    
    /// <summary>
    /// Represents an ini file, either for wmf or wav.
    /// </summary>
    public class PicIni {

        public PicIni(string path) {
            Path = path;
        }

        public Ini ToIni() {
            return new Ini(ToFileInfo().FullName);
        }

        public FileInfo ToFileInfo() {
            return new FileInfo(Path);
        }

        public bool Exists {
            get {
                return ToFileInfo().Exists;
            }
        }

        public string Path {
            get;
            private set;
        }

    }

    public class IniFileFactory {
         public PicIni CreatePictoWmfIni(LanguageSelection selection) {
            var languageCode = "";
            Language language = selection.Language;
            if (language != null && !language.IsTextless) {
                languageCode = language.Code;
            }
            var path = Environment.GetEnvironmentVariable("WINDIR") + @"\Pic" + selection.ImageFormat.CapitalizedExtension + languageCode + @".ini";
            return new PicIni(path);
        }


        public PicIni GetPictoWavIniFilePath(Language language) {
            var path =  Environment.GetEnvironmentVariable("WINDIR") + @"\PicWav" + language.Code + @".ini";

            return new PicIni(path);
        }

        public PicIni GetPictoIniFile(LanguageSelection selection, InstallationType installationType) {
            if (InstallationType.SOUND.Equals(installationType)) {
                return GetPictoWavIniFilePath(selection.Language);
            } else {
                return CreatePictoWmfIni(selection);
            }
        }

    }

    public class PictogramEntry :IComparable<PictogramEntry> {
        private readonly Regex _indexPattern = new Regex(@"\d+$");
        private readonly Regex removeInvalidChars = new Regex(String.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))), RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// 
        /// Throws FormatException when incorrectly named filenames are encounted.
        /// 
        /// FullCode is expected to be formatted like a30, j1 etc.
        /// Name is the translation of the image.
        /// 
        public PictogramEntry(string fullCode, string name, DateTime modified)  {
            FullCode = fullCode;
            Name = name;
            Modified = modified;
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
                return LegalFilename + "." + selection.ImageFormat.Extension;
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
            if(result == 0 ) {
                result = Index.CompareTo(other.Index);
            }
            
            return result;
        }
    }
}