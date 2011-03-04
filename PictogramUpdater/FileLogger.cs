using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PictogramUpdater {
    class FileLogger {

        private StreamWriter output;
        private bool disabled = false;

        public FileLogger() {
         
        }

        private void CreateFile() {
            try {
                string dir = Environment.GetEnvironmentVariable("TEMP") + @"\PictogramUppdatering";
                Directory.CreateDirectory(dir);
                var path = dir + @"\logg.txt";
                this.output = File.CreateText(path);
                disabled = false;
            } catch  {
                /* Could not create logging file. Logging disabled. */
                disabled = true;
            }
        }

        public void LogToFile(string message) {

            if (disabled) {
                return;
            }


            if (this.output == null) {
                CreateFile();

                /* Must check disabled flag again after CreateFile() has been called. */
                if (disabled) {
                    return;
                }

            }




            output.WriteLine(message);
            output.Flush();
        }

        public void Close() {
            output.Close();
        }
    }}
