using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AMS.Profile;

namespace PictogramUpdater {
    internal class AuthenticationService {
        private ISettingsPersistence settings;

        public AuthenticationService(ISettingsPersistence settings) {
            this.settings = settings;
        }

        public Boolean IsPictogramManagerInstalled() {
            var iniFilePath = Environment.GetEnvironmentVariable("WINDIR") + "\\PictogramManager.ini";
            var iniFile = new FileInfo(iniFilePath);
            if (!iniFile.Exists) {
                return false;
            }
            var ini = new Ini(iniFilePath);
            var exeFilePath = ini.GetValue("Inst", "Dir") as string;
            if(exeFilePath == null) {
                return false;
            }
            var exeFile = new FileInfo(exeFilePath + "\\PictogramManager.exe");
            return exeFile.Exists;
        }

        public String GetUsername() {
            return IsPictogramManagerInstalled() ? "webservice" : settings.getProperty("username");
        }

        public String GetPassword() {
            return IsPictogramManagerInstalled() ? "tbn2wswzcrf4" : settings.getProperty("password");
        }
    }
}