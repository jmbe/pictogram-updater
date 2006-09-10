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

namespace PictogramUpdater {
    
    /// <summary>
    /// Laddar ner bilder fr�n webbtj�nsten.
    /// </summary>
    class PictogramDownloader {

        private bool overwriteExistingFiles;
        private string targetPath;
        private LanguageProvider languageProvider;

        public event LogMessageCallback LogMessage;
        public event CurrentProgressCallback ProgressChanged;
        public event SetStatusCallback StatusChanged;


        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public PictogramDownloader(LanguageProvider languageProvider) {
            this.languageProvider = languageProvider;
        }

        /// <summary>
        /// Anger om befintliga filer ska skrivas �ver.
        /// </summary>
        public bool OverwriteExistingFiles {
            get {
                return this.overwriteExistingFiles;
            }
            set {
                this.overwriteExistingFiles = value;
            }
        }

        /// <summary>
        /// Anger i vilken katalog nedladdade filer ska sparas.
        /// </summary>
        public string TargetPath {
            get {
                return this.targetPath;
            }
            set {
                this.targetPath = value;
            }
        }


        public void DownloadPictogramZipUrl(string username, string password, string languageName) {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("H�mtar pictogram-URL...");

            try {
                PictosysWebService service = new PictosysWebService();
                string locale = this.languageProvider.GetLocale(languageName);
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
        public void Download(string username, string password, string language) {
            ProgressChanged(ProgressBarStyle.Continuous, 0, 1);
            StatusChanged("Laddar ner pictogram...");
            
            try {
                /* Skapa m�lkatalog */
                DirectoryInfo target = CreateTargetDirectory();


                /* Ladda ner lista med pictogram och h�mta de som saknas. */
                PictosysWebService service = new PictosysWebService();
                string[] names = service.getPictogramNames(username, password);
                if (names.Length > 0) {
                    string locale = this.languageProvider.GetLocale(language);
                    int current = 0;
                    foreach (string name in names) {

                        string file = target.FullName + @"\" + name + ".wmf";
                        if (!File.Exists(file) || OverwriteExistingFiles) {
                            LogMessage("Laddar ner " + name + "...");
                            BinaryWriter writer = null;
                            try {
                                byte[] buffer = service.downloadWMF(username, password, name, locale);
                                writer = new BinaryWriter(new FileStream(file, FileMode.OpenOrCreate));
                                writer.Write(buffer);
                                
                            } catch (SoapException e) {
                                LogMessage("Fel vid nedladdning av " + name + ": " + e.Message);
                            } finally {
                                if (writer != null) {
                                    writer.Close();
                                }
                            }

                        } else {
                            LogMessage(name + " finns redan.");
                        }
                        ProgressChanged(ProgressBarStyle.Blocks, current++, names.Length);
                    }
                    StatusChanged("Klar");
                } else {
                    /* Fanns tydligen inga pictogram att ladda ner. Kontrollera inloggningsuppgifterna. */
                    checkLogin(username, password);
                }
            } catch (ArgumentException ex) {
                Console.WriteLine(ex.Message);
            }
        }

        public void DownloadZip(string username, string password, string language) {
            ProgressChanged(ProgressBarStyle.Marquee, 0, 1);
            StatusChanged("Laddar ner pictogramzip...");

            try {
                /* Skapa m�lkatalog */
                DirectoryInfo target = CreateTargetDirectory();

                PictosysWebService service = new PictosysWebService();
                string locale = this.languageProvider.GetLocale(language);
                
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
