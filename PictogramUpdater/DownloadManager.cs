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
    /// Laddar ner bilder fr�n webbtj�nsten.
    /// </summary>
    class DownloadManager {
        private string _targetPath;
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

        /// <summary>
        /// Anger om befintliga filer ska skrivas �ver.
        /// </summary>
        public bool OverwriteExistingFiles { get; set; }

        /// <summary>
        /// Anger i vilken katalog nedladdade filer ska sparas.
        /// </summary>
        public string TargetPath {
            get {
                return this._targetPath;
            }
            set {
                this._targetPath = value;
            }
        }


        public void DownloadPictogramZipUrl(string username, string password, string languageName) {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("H�mtar pictogram-URL...");

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
        /// S�tter ig�ng nedladdning av pictogrambilder.
        /// </summary>
        public void Download(string username, string password, Language language, List<PictogramEntry> entries) {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("Laddar ner " + entries.Count + " pictogram...");

            try {
                /* Skapa m�lkatalog */
                var target = CreateTargetDirectory();

                var service = new PictosysWebService();

                if (entries.Count > 0) {
                    var current = 0;
                    foreach (var entry in entries) {
                        var file = target.FullName + @"\" + entry.FullCode + ".wmf";

                        LogMessage("Laddar ner " + entry.FullCode + "...");
                        BinaryWriter writer = null;
                        try {
                            var buffer = service.downloadWMF(username, password, entry.FullCode, language.Code);
                            writer = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate));
                            writer.Write(buffer);
                        } catch (SoapException e) {
                            LogMessage("Fel vid nedladdning av " + entry.FullCode + ": " + e.Message);
                        } finally {
                            if (writer != null) {
                                writer.Close();
                            }
                        }
                        
                        ProgressChanged(ProgressBarStyle.Blocks, current++, entries.Count);
                    }

                    StatusChanged("Klar");
                    LogMessage("");
                    LogMessage("Installationen �r klar.");
                } else {
                    /* Fanns tydligen inga pictogram att ladda ner. Kontrollera inloggningsuppgifterna. */
                    checkLogin(username, password);
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

        public void DownloadZip(string username, string password, string language) {
            ProgressChanged(ProgressBarStyle.Marquee, 0, 1);
            StatusChanged("Laddar ner pictogramzip...");

            try {
                /* Skapa m�lkatalog */
                DirectoryInfo target = CreateTargetDirectory();

                PictosysWebService service = new PictosysWebService();
                string locale = this._languageProvider.GetLocale(language);
                
                BinaryWriter writer = null;
                try {
                    string file = target.FullName + @"\pict" + language + ".zip";
                    LogMessage("Laddar ner zipfil till " + file + "...");
                    byte[] buffer = service.downloadPictogramZip(username, password, locale);
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
        /// Skapar m�lkatalogen om den inte redan finns.
        /// </summary>
        private DirectoryInfo CreateTargetDirectory() {
            DirectoryInfo target = new DirectoryInfo(TargetPath.Trim());
            if (target.Exists) {
                LogMessage("M�lkatalogen finns.");
            } else {
                Directory.CreateDirectory(target.FullName);
                LogMessage("Skapar ny katalog " + target.FullName + "...");
            }
            return target;
        }


        /// <summary>
        /// Metod f�r att kontrollera om inloggningsuppgifterna �r giltiga.
        /// </summary>
        /// <param name="username">Anv�ndarnamn</param>
        /// <param name="password">L�senord</param>
        /// <returns>en boolean som talar om ifall kontouppgifterna var giltiga</returns>
        public bool checkLogin(string username, string password) {
            ProgressChanged(ProgressBarStyle.Marquee, 0, 1);
            bool login = false;
            try {
                PictosysWebService service = new PictosysWebService();
                login = service.verifyLogin(username, password);

                if (login) {
                    StatusChanged("Kontouppgifterna �r giltiga.");
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