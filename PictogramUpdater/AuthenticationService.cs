using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AMS.Profile;

namespace PictogramUpdater {
    internal class AuthenticationService {

        private PictoOnIni pictoOnIni;

        public AuthenticationService() {
            this.pictoOnIni = new PictoOnIni();
        }

        public bool IsPictogramLibraryInstalled() {
            var windowsDir = Environment.GetEnvironmentVariable("WINDIR");
            var file = new FileInfo(windowsDir);
            var dir = new DirectoryInfo(windowsDir);
            var files = dir.GetFiles("picwmf*.ini");
            return files.Length > 0;
        }

        /*
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
        */


        public bool RequiresUserAccount() {
            return !UseFreeAccount();
        }

        public bool UseFreeAccount() {
            return IsPictogramLibraryInstalled();
        }

        public String GetUsername() {
            return UseFreeAccount() ? "webservice" : this.pictoOnIni.Username;
        }

        public String GetPassword() {
            return UseFreeAccount() ? "tbn2wswzcrf4" : this.pictoOnIni.Password;
        }

        public void saveAccount(string username, string password) {
            if (IsFreeAccount(username)) {
                return;
            }

            this.pictoOnIni.Username = username;
            this.pictoOnIni.Password = password;
        }

        private bool IsFreeAccount(string username) {
            return "webservice".Equals(username);
        }

    }


    public class PictoOnIni {

        private readonly Profile _settings;

        public PictoOnIni() {

            var settingsFile = Environment.GetEnvironmentVariable("WINDIR") + @"\PictoOn.ini";
            this._settings = new Ini(settingsFile);

        }

        public string Username {
            get {
                return _settings.GetValue("Login", "Owner") as string;
            }

            set {
                _settings.SetValue("Login", "Owner", value);    
            }
        }

        public string Password {
            get {
                return _settings.GetValue("Login", "LoginWord") as string;
            }

            set {
                _settings.SetValue("Login", "LoginWord", value);
            }
        }

    }
}