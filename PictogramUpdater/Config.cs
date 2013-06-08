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

        public bool IsPictoWmfInstalled(Language language) {
            var picWmf = this.iniFileFactory.CreatePictoWmfIni(language);
            return picWmf.Exists;
        }



        private string GetPictoInstallPath(Language language) {
            if (language.IsTextless) {
                return GetTextLessInstallPath();
            }

            Profile settings = this.iniFileFactory.CreatePictoWmfIni(language).ToIni();
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultPath(language);
        }

        private string GetPictoPlainTextInstallPath(Language language) {
            Profile settings = this.iniFileFactory.CreatePictoWmfIni(language).ToIni();
            var path = settings.GetValue("ProgDir", "PlainTextDir") as string;
            return path ?? GetDefaultPlainTextPath(language);
        }

        private String GetTextLessInstallPath() {
            Profile settings = this.iniFileFactory.CreatePictoWmfIni(null).ToIni();
            var path = settings.GetValue("ProgDir", "TextlessDir") as string;
            return path ?? GetDefaultTextlessDir();
        }

        public string getInstallPathForLanguage(Language language, InstallationType installationType) {
            switch (installationType) {
                case InstallationType.PLAIN_TEXT:
                    return GetPictoPlainTextInstallPath(language);
                case InstallationType.SOUND:
                    return GetPictoSoundInstallPath(language);
                case InstallationType.TEXTLESS:
                    /* fall-through */
                case InstallationType.CODE:
                    /* fall-through */
                default:
                    return GetPictoInstallPath(language);
            }

        }

        public string GetPictoSoundInstallPath(Language language) {
            Profile settings = this.iniFileFactory.GetPictoWavIniFilePath(language).ToIni();
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultSoundPath(language);
        }

        public void CreateOrUpdateWmfIni(Language language, string installPath, string plainTextInstallPath) {
            if (!IsPictoWmfInstalled(language)) {
                CreateNewIniFile(language);
            }

            /* WmfIni */

            PicIni picWmf = this.iniFileFactory.CreatePictoWmfIni(language);
            Profile profile = picWmf.ToIni();

            try {
                profile.SetValue("ProgDir", "Dir", language.IsTextless ? GetPictoInstallPath(language) : installPath);

                safeWriteToProfile(profile, "ProgDir", "Extension", "WMF");
                if (!language.IsTextless) {
                    safeWriteToProfile(profile, "ProgDir", "langWMF", language.Code);
                }
                safeWriteToProfile(profile, "ProgDir", "verWMF", "7.0");
                safeWriteToProfile(profile, "ProgDir", "CD", "-");
                safeWriteToProfile(profile, "ProgDir", "IDNAME", "");

                // Place PlainTextDir last in section.
                if (!language.IsTextless) {
                    profile.SetValue("ProgDir", "PlainTextDir", plainTextInstallPath);
                }

                profile.SetValue("ProgDir", "TextlessDir", language.IsTextless ? installPath : GetTextLessInstallPath());

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

        private void CreateNewIniFile(Language language) {
            var info = this.iniFileFactory.CreatePictoWmfIni(language).ToFileInfo();
            info.Create().Close();
            
            
        }

        public string GetDefaultPath(Language language) {
            return @"C:\Picto\Wmf" + language.Code.ToUpper();
        }

        public string GetDefaultPlainTextPath(Language language) {
            return @"C:\Picto\Wmf" + language.Code.ToUpper() + " " + TextResources.inPlainText;
        }

        private string GetDefaultTextlessDir() {
            return @"C:\Picto\Wmf";
        }

        public string GetDefaultSoundPath(Language language) {
            return @"C:\Picto\Wav" + language.Code.ToUpper();
        }

        public void CommitEntries(Language language, List<PictogramEntry> entries) {
            Profile profile = this.iniFileFactory.CreatePictoWmfIni(language).ToIni();
            Console.WriteLine("Commiting " + entries.Count + " Entries");

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
        public void CreateGenericPicWmfIni(Language language) {
            var iniFile = this.iniFileFactory.CreatePictoWmfIni(language).ToFileInfo();
            var picWmfGeneric = this.iniFileFactory.CreatePictoWmfIni(null).ToFileInfo();

            /* Only overwrite the generic file with the Swedish file, or create new file in any language. */
            if (language.IsSwedish || !picWmfGeneric.Exists) {
                iniFile.CopyTo(picWmfGeneric.FullName, true);
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
        public PicIni CreatePictoWmfIni(Language language) {
            var languageCode = "";
            if (language != null && !language.IsTextless) {
                languageCode = language.Code;
            }
            var path = Environment.GetEnvironmentVariable("WINDIR") + @"\PicWmf" + languageCode + @".ini";
            return new PicIni(path);
        }


        public PicIni GetPictoWavIniFilePath(Language language) {
            var path =  Environment.GetEnvironmentVariable("WINDIR") + @"\PicWav" + language.Code + @".ini";

            return new PicIni(path);
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

        public string ToFilename(InstallationType installationType) {

            if (InstallationType.SOUND.Equals(installationType)) {
                return FullCode + ".wav";
            }

            if (InstallationType.PLAIN_TEXT.Equals(installationType)) {
                return LegalFilename + ".wmf";
            }

            // InstallationType.CODE
            return FullCode + ".wmf";
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