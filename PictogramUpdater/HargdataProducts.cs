using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace PictogramUpdater {
    class HargdataProducts {

        public event LogMessageCallback LogMessage;
        public event LogToFileCallback LogToFile;

        /// <summary>
        /// Date of last dictionary update.
        /// </summary>
        private DateTimeOffset DictionaryDate {
            get {
                return DateTimeOffset.Parse("2017-10-12 14:00:00 +02:00");
            }
        }

        public string WidgitDictionaryPath {
            get {
                return (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Widgit\WSS2000\Application", "Word List Path", null) ?? Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\WSS2000\Application", "Word List Path", null));
            }
        }

        public Boolean IsWidgitInstalled {
            get {
                return Files.DirectoryFromRegistryExists(WidgitDictionaryPath);
            }
        }

        public string WidgitImagesDirectory {
            get {
                return (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Widgit\WWS3\Resources\ResDir3", "path", null) ?? Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\WWS3\Resources\ResDir3", "path", null)) + @"\graphics\Pictogram";
            }
        }

        public string CommunicateDictionaryPath {
            get {
                return (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Widgit\Communicate_Sw\In Print\Application", "Word List Path", null) ?? Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\Communicate_Sw\In Print\Application", "Word List Path", null));
            }
        }

        public string CommunicateImagesDirectory {
            get {
                return (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Widgit\Communicate_Sw\Resources\ResDir3", "path", null) ?? Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\Communicate_Sw\Resources\ResDir3", "path", null)) + @"\graphics\Pictogram";
            }
        }

        public Boolean IsCommunicateInstalled {
            get {
                return Files.DirectoryFromRegistryExists(CommunicateDictionaryPath);
            }
        }

        /* Public for logging reasons. */
        public string SymWriterDirectoriesXmlPath {
            get {
                return (string)(Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\Widgit\Communicate\DirectoryManifest", null, null) ?? Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\Communicate\DirectoryManifest", null, null));
            }
        }

        public Boolean IsSymWriterInstalled {
            get {
                return Files.FileFromRegistryExists(SymWriterDirectoriesXmlPath);
            }
        }

        public string SymWriterImagesDirectory {
            get {
                string contents = File.ReadAllText(SymWriterDirectoriesXmlPath);
                return ExpandSymWriterConstants(Strings.FindInString(contents, "<ThirdPartyResources>", "</ThirdPartyResources>")) + "Pictogram";
            }
        }

        public string SymWriterDictionaryDirectory {
            get {
                string contents = File.ReadAllText(SymWriterDirectoriesXmlPath);
                return ExpandSymWriterConstants(Strings.FindInString(contents, "<ThirdPartyWordlists>", "</ThirdPartyWordlists>")) + "Pictogram";
            }
        }

        private string ExpandSymWriterConstants(string s) {
            string pf = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86);
            /* ProgramFilesX86 will be empty(!) on Windows XP */
            if ("".Equals(pf)) {
                pf = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
            }
            string commonAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
            return s.Replace("$(ProgramFiles)", pf).Replace("$(SharedApplicationData)", commonAppData);
        }

        internal void InstallSymWriterDictionary() {
            copyDictionaryTo("cfwl", SymWriterDictionaryDirectory);
        }

        internal void installWidgitDictionary() {
            copyDictionaryTo("wus", WidgitDictionaryPath);
        }

        internal void installCommunicateDictionary() {
            copyDictionaryTo("cwl", CommunicateDictionaryPath);
        }

        private void copyDictionaryTo(string extension, string targetDir) {
            Directory.CreateDirectory(targetDir);
            DirectoryInfo startup = new DirectoryInfo(Application.StartupPath);
            FileInfo included = new FileInfo(startup.FullName + @"\hargdata\Pictogram." + extension);
            FileInfo target = new FileInfo(targetDir + @"\" + included.Name);

            LogToFile("Installerar " + included + " till " + target);

            try {
                if (Files.shouldWriteTo(target, DictionaryDate.DateTime)) {
                    included.CopyTo(target.FullName, true);
                    File.SetLastWriteTime(target.FullName, DictionaryDate.DateTime);
                }
            } catch (FileNotFoundException e) {
                LogMessage("Kunde inte installera ordlista pga: " + e.Message);
            } catch (DirectoryNotFoundException e) {
                LogMessage("Kunde inte installera ordlista pga: " + e.Message);
            }
        }

    }
}

