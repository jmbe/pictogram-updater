using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PictogramUpdater {
    class Strings {

        /// <returns>string between start and end delimiter in given string contents</returns>
        public static string FindInString(string contents, string startSubString, string endSubString) {
            int start = contents.IndexOf(startSubString);
            int end = contents.IndexOf(endSubString);
            string substring = contents.Substring(start + startSubString.Length, end - start - startSubString.Length);
            return substring;
        }
    }
}
