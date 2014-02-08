using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PictogramUpdater {
    class Files {

        public static Boolean DirectoryFromRegistryExists(string path) {
            return path != null && new DirectoryInfo(path).Exists;
        }

        public static Boolean FileFromRegistryExists(string path) {
            return path != null && new FileInfo(path).Exists;
        }
    }
}