using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AMS.Profile;

namespace PictogramUpdater {
    internal class AuthenticationService {
        private readonly Profile _settings;

        public AuthenticationService() {
            var applicationDataDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PictogramUpdater";
            var settingsFile = applicationDataDirectory + @"\PictogramUpdater.ini";
            Console.WriteLine(settingsFile);
            _settings = new Ini(settingsFile);
        }

        public Boolean IsPictogramManagerInstalled() {
            var iniFilePath = Environment.GetEnvironmentVariable("WINDIR") + @"\PictogramManager.ini";
            var iniFile = new FileInfo(iniFilePath);
            if (!iniFile.Exists) {
                return false;
            }
            var ini = new Ini(iniFilePath);
            var exeFilePath = ini.GetValue("Inst", "Dir") as string;
            if(exeFilePath == null) {
                return false;
            }
            var exeFile = new FileInfo(exeFilePath + @"\PictogramManager.exe");
            return exeFile.Exists;
        }

        public String GetUsername() {
            return IsPictogramManagerInstalled() ? "webservice" : _settings.GetValue("Auth", "username") as string;
        }

        public String GetPassword() {
            return IsPictogramManagerInstalled() ? "tbn2wswzcrf4" : _settings.GetValue("Auth", "password") as string;
        }

        public void SaveUsername(string username) {
            if(!IsPictogramManagerInstalled()) {
                _settings.SetValue("Auth", "username", username);    
            }
        }

        public void SavePassword(string password) {
            if (!IsPictogramManagerInstalled()) {
                _settings.SetValue("Auth", "password", password);
            }
        }
    }
}