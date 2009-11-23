using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AMS.Profile;

namespace PictogramUpdater {
    internal class AuthenticationService {
        private readonly Profile _settings;

        public AuthenticationService() {
            var settingsFile = Environment.GetEnvironmentVariable("WINDIR") + @"\PictoOn.ini";;
            _settings = new Ini(settingsFile);
        }

        public bool IsPictogramLibraryInstalled() {


            var windowsDir = Environment.GetEnvironmentVariable("WINDIR");
            var file = new FileInfo(windowsDir);
            var dir = new DirectoryInfo(windowsDir);
            var files = dir.GetFiles("picwmf*.ini");
            return files.Length > 0;
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
            return IsPictogramManagerInstalled() ? "webservice" : _settings.GetValue("Login", "Owner") as string;
        }

        public String GetPassword() {
            return IsPictogramManagerInstalled() ? "tbn2wswzcrf4" : _settings.GetValue("Login", "LoginWord") as string;
        }

        public void SaveUsername(string username) {
            if(!IsPictogramManagerInstalled()) {
                _settings.SetValue("Login", "Owner", username);    
            }
        }

        public void SavePassword(string password) {
            if (!IsPictogramManagerInstalled()) {
                _settings.SetValue("Login", "LoginWord", password);
            }
        }
    }
}