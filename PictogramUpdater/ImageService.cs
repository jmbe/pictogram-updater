using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using AMS.Profile;

namespace PictogramUpdater {
    class ImageService {
        public bool IsPictoWmfInstalled(string language) {
            string iniFilePath = GetPictoWmfIniFilePath(language);
            return new FileInfo(iniFilePath).Exists;
        }

        private string GetPictoWmfIniFilePath(string language) {
            return Environment.GetEnvironmentVariable("WINDIR") + @"\PicWmf" + language + @".ini";
        }

        public string GetPictoWmfInstallPath(string language) {
            Profile settings = new Ini(GetPictoWmfIniFilePath(language));
            return settings.GetValue("ProgDir", "Dir") as string;
        }
    }
}
