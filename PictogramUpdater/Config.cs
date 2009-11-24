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

        public Config(CategoryRepository categoryRepository, CategoryTranslationService categoryTranslationService) {
            _queue = new List<PictogramEntry>();
            this.categoryRepository = categoryRepository;
            this.categoryTranslationService = categoryTranslationService;
        }

        public bool IsPictoWmfInstalled(Language language) {
            var iniFilePath = GetPictoWmfIniFilePath(language);
            return new FileInfo(iniFilePath).Exists;
        }

        private static string GetPictoWmfIniFilePath(Language language) {
            return Environment.GetEnvironmentVariable("WINDIR") + @"\PicWmf" + language.Code + @".ini";
        }

        private static string GetPictoWavIniFilePath(Language language) {
            return Environment.GetEnvironmentVariable("WINDIR") + @"\PicWav" + language.Code + @".ini";
        }

        public string GetPictoInstallPath(Language language) {
            Profile settings = new Ini(GetPictoWmfIniFilePath(language));
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultPath(language);
        }

        public string GetPictoPlainTextInstallPath(Language language) {
            Profile settings = new Ini(GetPictoWmfIniFilePath(language));
            var path = settings.GetValue("ProgDir", "PlainTextDir") as string;
            return path ?? GetDefaultPlainTextPath(language);
        }

        public string getInstallPathForLanguage(Language language, InstallationType installationType) {
            switch (installationType) {
                case InstallationType.PLAIN_TEXT:
                    return GetPictoPlainTextInstallPath(language);
                case InstallationType.SOUND:
                    return GetPictoSoundInstallPath(language);
                default:
                    return GetPictoInstallPath(language);
            }

        }

        public string GetPictoSoundInstallPath(Language language) {
            Profile settings = new Ini(GetPictoWavIniFilePath(language));
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultSoundPath(language);
        }

        public void CreateOrUpdateWmfINI(Language language, string installPath, string plainTextInstallPath) {
            if (!IsPictoWmfInstalled(language)) {
                CreateNewIniFile(language);
            }

            /* WmfIni */

            var iniFilePath = GetPictoWmfIniFilePath(language);
            Profile profile = new Ini(iniFilePath);

            try {
                profile.SetValue("ProgDir", "Dir", installPath);
                profile.SetValue("ProgDir", "PlainTextDir", plainTextInstallPath);
            
                if (profile.GetValue("ProgDir", "Extension") == null) {
                    profile.SetValue("ProgDir", "Extension", "WMF");
                }
                if (profile.GetValue("ProgDir", "langWMF") == null) {
                    profile.SetValue("ProgDir", "langWMF", language.Code);
                }
                if (profile.GetValue("ProgDir", "verWMF") == null) {
                    profile.SetValue("ProgDir", "verWMF", "7.0");
                }
                if (profile.GetValue("ProgDir", "CD") == null) {
                    profile.SetValue("ProgDir", "CD", "-");
                }
                if (profile.GetValue("ProgDir", "IDNAME") == null) {
                    profile.SetValue("ProgDir", "IDNAME", "");
                }
            } catch (System.ComponentModel.Win32Exception e) {
                throw new UnauthorizedAccessException("Access to the path '" + iniFilePath + "' is denied.", e);
            }

            //Update categories
            
            var categories = this.categoryRepository.FindAll();
            profile.SetValue("Grupper", "Antal", categories.Count);
            profile.RemoveSection("Grupper");
            foreach (var category in categories) {
                var translation = this.categoryTranslationService.Translate(category, language);
                profile.SetValue("Grupper", category.Index.ToString(), translation);
                profile.SetValue(translation, "Antal", "0");
                profile.SetValue(translation, "Kod", category.Code);
            }

        }

        public void CreateOrUpdateWavIni(Language language, string soundInstallPath) {
            /* WavIni */
            var profile = new Ini(GetPictoWavIniFilePath(language));
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

        private static void CreateNewIniFile(Language language) {
            var info = new FileInfo(GetPictoWmfIniFilePath(language));
            info.Create().Close();
            

            info = new FileInfo(GetPictoWavIniFilePath(language));
            info.Create().Close();
            
        }

        public string GetDefaultPath(Language language) {
            return @"C:\Picto\Wmf" + language.Code.ToUpper();
        }

        public string GetDefaultPlainTextPath(Language language) {
            return @"C:\Picto\Wmf" + language.Code.ToUpper() + " i klartext";
        }

        public string GetDefaultSoundPath(Language language) {
            return @"C:\Picto\Wav" + language.Code.ToUpper();
        }

        public void CommitEntries(Language language, List<PictogramEntry> entries) {
            Profile profile = new Ini(GetPictoWmfIniFilePath(language));
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

        public void SetPictoInstallPaths(Language language, string installPath, string plainTextInstallPath, string soundInstallPath) {
            var profile = new Ini(GetPictoWmfIniFilePath(language));
            profile.SetValue("ProgDir", "Dir", installPath);
            profile.SetValue("ProgDir", "PlainTextDir", plainTextInstallPath);

            profile = new Ini(GetPictoWavIniFilePath(language));
            profile.SetValue("ProgDir", "Dir", soundInstallPath);
        }
    }

    public class PictogramEntry :IComparable<PictogramEntry> {
        private readonly Regex _indexPattern = new Regex(@"\d+$");

        public PictogramEntry(string fullCode, string name) {
            FullCode = fullCode;
            Name = name;
            var indexMatch = _indexPattern.Match(fullCode);
            CategoryCode = fullCode.Substring(0, indexMatch.Index);
            Index = Convert.ToInt32(fullCode.Substring(indexMatch.Index));
        }

        public string FullCode { get; set; }
        public string Name { get; set; }

        public string CategoryCode { get; private set; }

        public int Index { get; private set; }

        public string ToFilename(InstallationType installationType) {

            if (InstallationType.SOUND.Equals(installationType)) {
                return FullCode + ".wav";
            }

            if (InstallationType.PLAIN_TEXT.Equals(installationType)) {
                return Name.Trim() + ".wmf";
            }

            // InstallationType.CODE
            return FullCode + ".wmf";
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