using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using AMS.Profile;

namespace PictogramUpdater {
    internal class Config {
        private List<PictogramEntry> _queue;

        public Config() {
            _queue = new List<PictogramEntry>();
        }

        public bool IsPictoWmfInstalled(Language language) {
            var iniFilePath = GetPictoWmfIniFilePath(language);
            return new FileInfo(iniFilePath).Exists;
        }

        private static string GetPictoWmfIniFilePath(Language language) {
            return Environment.GetEnvironmentVariable("WINDIR") + @"\PicWmf" + language.Code + @".ini";
        }

        public string GetPictoWmfInstallPath(Language language
            ) {
            Profile settings = new Ini(GetPictoWmfIniFilePath(language));
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultPath(language);
        }

        public string GetExtension(Language language) {
            Profile settings = new Ini(GetPictoWmfIniFilePath(language));
            return settings.GetValue("ProgDir", "Extension") as string;
        }

        public Profile CreateOrUpdateINI(Language language, string path) {
            if (!IsPictoWmfInstalled(language)) {
                CreateNewIniFile(language);
            }
            Profile profile = new Ini(GetPictoWmfIniFilePath(language));

            profile.SetValue("ProgDir", "Dir", path);

            if (profile.GetValue("ProgDir", "Extension") == null) {
                profile.SetValue("ProgDir", "Extension", "WMF");
            }
            if (profile.GetValue("ProgDir", "langWMF") == null) {
                profile.SetValue("ProgDir", "langWMF", language);
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

            //Update categories
            var repository = new CategoryRepository();
            var translationService = new CategoryTranslationService();
            var categories = repository.FindAll();
            profile.SetValue("Grupper", "Antal", categories.Count);
            profile.RemoveSection("Grupper");
            foreach (var category in categories) {
                var translation = translationService.Translate(category, language);
                profile.SetValue("Grupper", category.Index.ToString(), translation);
                profile.SetValue(translation, "Antal", "0");
                profile.SetValue(translation, "Kod", category.Code);
            }

            return profile;
        }

        private static void CreateNewIniFile(Language locale) {
            var info = new FileInfo(GetPictoWmfIniFilePath(locale));
            info.Create().Close();
            Console.WriteLine("Create new inifile");
        }

        public string GetDefaultPath(Language language) {
            return @"C:\Picto\Wmf" + language.Code.ToUpper();
        }

        public void CommitEntries(Language language, List<PictogramEntry> entries) {
            var translationService = new CategoryTranslationService();
            var repository = new CategoryRepository();
            Profile profile = new Ini(GetPictoWmfIniFilePath(language));
            Console.WriteLine("Commiting " + entries.Count + " Entries");

            var categoryCounts = new Dictionary<Category, int>();
            foreach (var entry in entries) {
                var category = repository.FindByCode(entry.Code);
                var categoryCount = 0;
                var categoryTranslation = translationService.Translate(category, language);
                if (categoryCounts.ContainsKey(category)) {
                    categoryCount = categoryCounts[category];
                }
                profile.SetValue(categoryTranslation, entry.FullCode, entry.Name);
                categoryCount++;
                categoryCounts[category] = categoryCount;
            }
            foreach (var category in categoryCounts.Keys) {
                profile.SetValue(translationService.Translate(category, language), "Antal", categoryCounts[category]);
            }
        }
    }

    internal class PictogramEntry :IComparable<PictogramEntry> {
        private readonly Regex _indexPattern = new Regex(@"\d+$");

        public PictogramEntry(string fullCode, string name) {
            FullCode = fullCode;
            Name = name;
            var indexMatch = _indexPattern.Match(fullCode);
            Code = fullCode.Substring(0, indexMatch.Index);
            Index = Convert.ToInt32(fullCode.Substring(indexMatch.Index));
        }

        public string FullCode { get; set; }
        public string Name { get; set; }

        public string Code { get; private set; }

        public int Index { get; private set; }

        public int CompareTo(PictogramEntry other) {
            var result = Code.CompareTo(other.Code);
            if(result == 0 ) {
                result = Index.CompareTo(other.Index);
            }
            
            return result;
        }
    }
}