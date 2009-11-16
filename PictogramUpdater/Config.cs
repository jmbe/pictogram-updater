using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AMS.Profile;

namespace PictogramUpdater {
    internal class Config {
        public bool IsPictoWmfInstalled(string locale) {
            var iniFilePath = GetPictoWmfIniFilePath(locale);
            return new FileInfo(iniFilePath).Exists;
        }

        private static string GetPictoWmfIniFilePath(string locale) {
            return Environment.GetEnvironmentVariable("WINDIR") + @"\PicWmf" + locale + @".ini";
        }

        public string GetPictoWmfInstallPath(string locale) {
            Profile settings = new Ini(GetPictoWmfIniFilePath(locale));
            var path = settings.GetValue("ProgDir", "Dir") as string;
            return path ?? GetDefaultPath(locale);
        }

        public void CreateOrUpdateINI(string locale, string path) {
            if (!IsPictoWmfInstalled(locale)) {
                CreateNewIniFile(locale);
            }
            Profile profile = new Ini(GetPictoWmfIniFilePath(locale));
            profile.SetValue("ProgDir", "Dir", path);
            
            if (profile.GetValue("ProgDir", "Extension") == null) {
                profile.SetValue("ProgDir", "Extension", "WMF");
            }
            if (profile.GetValue("ProgDir", "langWMF") == null) {
                profile.SetValue("ProgDir", "langWMF", locale);
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
        }

        private static void CreateNewIniFile(string locale) {
            var info = new FileInfo(GetPictoWmfIniFilePath(locale));
            info.Create().Close();
            Console.WriteLine("Create new inifile");
        }

        public string GetDefaultPath(string locale) {
            return @"C:\Picto\Wmf" + locale.ToUpper();
        }
    }
}