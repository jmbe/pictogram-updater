using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Threading;
using System.Web.Services.Protocols;
using AMS.Profile;
using PictogramUpdater;

namespace PictogramUpdater {
    /// <summary>
    /// Laddar ner bilder från webbtjänsten.
    /// </summary>
    class DownloadManager {
        private readonly LanguageProvider _languageProvider;
        
        private PictogramRestService pictogramRestService;        

        public event LogMessageCallback LogMessage;
        public event LogToFileCallback LogToFile;
        public event CurrentProgressCallback ProgressChanged;
        public event SetStatusCallback StatusChanged;


        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public DownloadManager(LanguageProvider languageProvider, PictogramRestService pictogramRestService) {
            if (languageProvider == null) {
                throw new NullReferenceException("Language provider cannot be null");
            }
            this._languageProvider = languageProvider;

            if (pictogramRestService == null) {
                throw new NullReferenceException("Pictogram REST service cannot be null");
            }

            this.pictogramRestService = pictogramRestService;

        }

        
        /// <summary>
        /// Anger om befintliga filer ska skrivas över.
        /// </summary>
        public bool OverwriteExistingFiles { get; set; }
        public InstallationType InstallationType{ get; set; }

        public bool PlainText {
            get {
                return InstallationType.PLAIN_TEXT.Equals(InstallationType);
            }
        }

        public bool Sound {
            get {
                return InstallationType.SOUND.Equals(InstallationType);
            }
        }
        

        /// <summary>
        /// Anger i vilken katalog nedladdade filer ska sparas.
        /// </summary>
        public string TargetPath { get; set; }

        public string Username { get; set;
        }

        public List<PictogramEntry> DownloadList { get; set; }

        public string Password { get; set; }

        public Language Language { get; set; }


        /// <summary>
        /// Sätter igång nedladdning av pictogrambilder.
        /// </summary>
        public void Download() {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("Laddar ner " + DownloadList.Count + " nya filer...");

            try {
                /* Skapa målkatalog */
                var target = CreateTargetDirectory();


                if (DownloadList.Count > 0) {
                    var current = 0;
                    foreach (var entry in DownloadList) {


                        var fileName = entry.ToFilename(InstallationType);
                        var file = target.FullName + @"\" + fileName;

                        LogMessage("Laddar ner " + fileName + "...");
                        Stream stream = null;
                        
                        try {
                            stream = new FileStream(file, FileMode.OpenOrCreate);
                            
                            if (Sound) {
                                this.pictogramRestService.downloadSound(Username, Password, entry.FullCode, Language.Code.ToLower(), stream);
                            } else {
                                this.pictogramRestService.downloadWmf(Username, Password, entry.FullCode, Language.Code.ToLower(), stream);
                            }
                            
                        } catch (Exception e) {
                            LogMessage("Fel vid nedladdning av " + fileName + ": " + e.Message);
                            LogToFile(e.ToString());
                        } finally {
                            if (stream != null) {
                                stream.Close();
                            }
                        }
                        
                        ProgressChanged(ProgressBarStyle.Blocks, current++, DownloadList.Count);
                    }

                    //StatusChanged("Klar");
                    //LogMessage("");
                    //LogMessage("Installationen är klar.");
                } else {
                    /* Fanns tydligen inga pictogram att ladda ner. Kontrollera inloggningsuppgifterna. */
                    //checkLogin(Username, Password);
                }
            } catch (ArgumentException ex) {
                LogToFile(ex.ToString());
            }
        }

        /// <summary>
        /// Skapar målkatalogen om den inte redan finns.
        /// </summary>
        private DirectoryInfo CreateTargetDirectory() {
            DirectoryInfo target = new DirectoryInfo(TargetPath.Trim());
            if (target.Exists) {
                //LogMessage("Målkatalogen finns.");
            } else {
                Directory.CreateDirectory(target.FullName);
                LogMessage("Skapar ny katalog " + target.FullName + "...");
            }
            return target;
        }


        /// <summary>
        /// Metod för att kontrollera om inloggningsuppgifterna är giltiga.
        /// </summary>
        /// <param name="username">Användarnamn</param>
        /// <param name="password">Lösenord</param>
        /// <returns>en boolean som talar om ifall kontouppgifterna var giltiga</returns>
        public bool checkLogin(string username, string password) {
            ProgressChanged(ProgressBarStyle.Marquee, 0, 1);
            bool login = false;
            try {
                login = pictogramRestService.verifyLogin(username, password);

                if (login) {
                    StatusChanged("Kontouppgifterna är giltiga.");
                } else {
                    StatusChanged("Kontrollera kontouppgifterna!");
                }
            } catch (Exception e) {
                LogMessage("Kunde inte ansluta till server.");
                StatusChanged("");
                LogToFile(e.ToString());
            }
            ProgressChanged(ProgressBarStyle.Blocks, 0, 1);
            return login;

        }
    }
}