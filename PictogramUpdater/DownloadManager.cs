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

        public event LogMessageCallback LogMessage;
        public event CurrentProgressCallback ProgressChanged;
        public event SetStatusCallback StatusChanged;


        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public DownloadManager(LanguageProvider languageProvider) {
            this._languageProvider = languageProvider;
        }

        public DownloadManager() {}
        
        /// <summary>
        /// Anger om befintliga filer ska skrivas över.
        /// </summary>
        public bool OverwriteExistingFiles { get; set; }

        /// <summary>
        /// Anger i vilken katalog nedladdade filer ska sparas.
        /// </summary>
        public string TargetPath { get; set; }

        public string Username { get; set;
        }

        public List<PictogramEntry> DownloadList { get; set; }

        public string Password { get; set; }

        public Language Language { get; set; }

        public void DownloadPictogramZipUrl(string username, string password, string languageName) {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("Hämtar pictogram-URL...");

            try {
                PictosysWebService service = new PictosysWebService();
                string locale = this._languageProvider.GetLocale(languageName);
                LogMessage(service.getPictogramZipDownloadUrl(username, password, locale));
            } catch (Exception e) {
                LogMessage(e.Message);
            }

            ProgressChanged(ProgressBarStyle.Blocks, 0, 1);
            StatusChanged("Klar");

        }

        /// <summary>
        /// Sätter igång nedladdning av pictogrambilder.
        /// </summary>
        public void Download() {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("Laddar ner " + DownloadList.Count + " pictogram...");

            try {
                /* Skapa målkatalog */
                var target = CreateTargetDirectory();

                var service = new PictosysWebService();

                if (DownloadList.Count > 0) {
                    var current = 0;
                    foreach (var entry in DownloadList) {
                        var file = target.FullName + @"\" + entry.FullCode + ".wmf";

                        LogMessage("Laddar ner " + entry.FullCode + "...");
                        BinaryWriter writer = null;
                        try {
                            var buffer = service.downloadWMF(Username, Password, entry.FullCode, Language.Code);
                            writer = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate));
                            writer.Write(buffer);
                        } catch (SoapException e) {
                            LogMessage("Fel vid nedladdning av " + entry.FullCode + ": " + e.Message);
                        } finally {
                            if (writer != null) {
                                writer.Close();
                            }
                        }
                        
                        ProgressChanged(ProgressBarStyle.Blocks, current++, DownloadList.Count);
                    }

                    StatusChanged("Klar");
                    LogMessage("");
                    LogMessage("Installationen är klar.");
                } else {
                    /* Fanns tydligen inga pictogram att ladda ner. Kontrollera inloggningsuppgifterna. */
                    checkLogin(Username, Password);
                }
            } catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private Dictionary<string, PictogramEntry> GetPictogramEntriesByCode(string[] strings) {
            var entriesByCode = new Dictionary<string, PictogramEntry>();
            for (var i = 0; i < strings.Length; i = i + 2 ) {
                var code = strings[i];
                var name = strings[i + 1];
                entriesByCode[code] = new PictogramEntry(code, name);
            }
            return entriesByCode;
        }

        public void DownloadZip(string username, string password, Language language) {
            ProgressChanged(ProgressBarStyle.Marquee, 0, 1);
            StatusChanged("Laddar ner pictogramzip...");

            try {
                /* Skapa målkatalog */
                DirectoryInfo target = CreateTargetDirectory();

                PictosysWebService service = new PictosysWebService();

                BinaryWriter writer = null;
                try {
                    string file = target.FullName + @"\pict" + language + ".zip";
                    LogMessage("Laddar ner zipfil till " + file + "...");
                    byte[] buffer = service.downloadPictogramZip(username, password, language.Code);
                    if (buffer.Length > 0) {
                        writer = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate));
                        writer.Write(buffer);
                    } else {
                        LogMessage("No zipfile for that language exists or insufficient rights.");
                    }
                } catch (SoapException e) {
                    LogMessage("Fel vid nedladdning av zipfil: " + e.Message);
                } catch (FileNotFoundException e) {
                    LogMessage("Fel vid nedladdning av zipfil: " + e.Message);
                } finally {
                    if (writer != null) {
                        writer.Close();
                    }
                }


            } catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }
            ProgressChanged(ProgressBarStyle.Blocks, 0, 1);
            StatusChanged("Klar");
        }


        /// <summary>
        /// Skapar målkatalogen om den inte redan finns.
        /// </summary>
        private DirectoryInfo CreateTargetDirectory() {
            DirectoryInfo target = new DirectoryInfo(TargetPath.Trim());
            if (target.Exists) {
                LogMessage("Målkatalogen finns.");
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
                PictosysWebService service = new PictosysWebService();
                login = service.verifyLogin(username, password);

                if (login) {
                    StatusChanged("Kontouppgifterna är giltiga.");
                } else {
                    StatusChanged("Kontrollera kontouppgifterna!");
                }
            } catch {
                LogMessage("Kunde inte ansluta till server.");
            }
            ProgressChanged(ProgressBarStyle.Blocks, 0, 1);
            return login;

        }
    }
}