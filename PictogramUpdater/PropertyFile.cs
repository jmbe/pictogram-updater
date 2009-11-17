using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace PictogramUpdater {

    /// <summary>
    /// Implementation en av .properties-file för att
    /// lagra inställningar.
    /// </summary>
    class PropertyFile : ISettingsPersistence {

        private System.Collections.Hashtable map;
        private string settingsFile;

        /// <summary>
        /// Skapar en ny instans av klassen.
        /// </summary>
        public PropertyFile() {
            this.map = new Hashtable();

            /* Katalog för inställningar */
            string applicationDataDirectory = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\PictogramUpdater";
            this.settingsFile = applicationDataDirectory + @"\PictogramUpdater.properties";

            if (!Directory.Exists(applicationDataDirectory)) {
                Directory.CreateDirectory(applicationDataDirectory);
            }

            loadSettings();
        }

        /// <summary>
        /// Laddar inställningar från fil.
        /// </summary>
        private void loadSettings() {
            StreamReader reader = null;
            try {
                if (File.Exists(this.settingsFile)) {
                    reader = new StreamReader(new FileStream(this.settingsFile, FileMode.Open));
                    string line;
                    while ((line = reader.ReadLine()) != null) {
                        Console.WriteLine(line);
                        string[] tokens = line.Split(new char[] { '=' });
                        if (tokens.Length == 2) {
                            this.map[tokens[0]] = tokens[1];
                        }
                    }
                    reader.Close();
                }
            } catch (IOException e) {
                Console.WriteLine(e.Message);
            } finally {
                if (reader != null) {
                    reader.Close();
                }
            }
        }

        /// <summary>
        /// Skriver inställningar till fil.
        /// </summary>
        private void saveSettings() {
            StreamWriter writer = null;
            try {
                writer = new StreamWriter(new FileStream(this.settingsFile, FileMode.OpenOrCreate));
                foreach (string key in this.map.Keys) {
                    writer.WriteLine(key + "=" + getProperty(key));
                }

            } catch (IOException e) {
                Console.WriteLine(e.Message);
            } finally {
                if (writer != null) {
                    writer.Close();
                }
            }
        }

        #region ISettingsPersistence Members

        public string getProperty(string key) {
            if (this.map.ContainsKey(key)) {
                return (string) this.map[key];
            } else {
                return "";
            }
        }

        public void setProperty(string key, string value) {
            this.map[key] = value;
            saveSettings();
        }

        #endregion
    }
}
