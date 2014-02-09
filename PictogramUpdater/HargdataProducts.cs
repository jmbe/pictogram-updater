using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace PictogramUpdater {
    class HargdataProducts {


        public string WidgitDictionaryPath {
            get {
                return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\WSS2000\Application", "Word List Path", null);
            }
        }

        public Boolean IsWidgitInstalled {
            get {
                return Files.DirectoryFromRegistryExists(WidgitDictionaryPath);
            }
        }

        public string WidgitImagesDirectory {
            get {
                return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\WWS3\Resources\ResDir3", "path", @"C:\WSS2000") + @"\graphics\Pictogram";
            }
        }

        public string CommunicateDictionaryPath {
            get {
                return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\Communicate_Sw\In Print\Application", "Word List Path", null);
            }
        }

        public string CommunicateImagesDirectory {
            get {
                return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\Communicate_Sw\Resources\ResDir3", "path", @"C:\InPrint") + @"\graphics\Pictogram";
            }
        }

        public Boolean IsCommunicateInstalled {
            get {
                return Files.DirectoryFromRegistryExists(CommunicateDictionaryPath);
            }
        }

        private string SymWriterDirectoriesXmlPath {
            get {
                return (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Widgit\Communicate\DirectoryManifest", null, null);
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
            string pf = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles);
            string commonAppData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);
            return s.Replace("$(ProgramFiles)", pf).Replace("$(SharedApplicationData)", commonAppData);
        }

        internal void InstallSymWriterDictionary() {
            copyDictionaryTo("cfwl", SymWriterDictionaryDirectory);
        }

        private void copyDictionaryTo(string extension, string targetDir) {
            Directory.CreateDirectory(targetDir);
            DirectoryInfo startup = new DirectoryInfo(Application.StartupPath);
            FileInfo included = new FileInfo(startup.FullName + @"\hargdata\Pictogram." + extension);
            included.CopyTo(targetDir + @"\" + included.Name);
        }

        internal void installWidgitDictionary() {
            copyDictionaryTo("wus", WidgitDictionaryPath);
        }

        internal void installCommunicateDictionary() {
            copyDictionaryTo("cwl", CommunicateDictionaryPath);
        }
    }
}

