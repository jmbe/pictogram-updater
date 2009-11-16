using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager {

    /// <summary>
    /// Interface för persistenslagret.
    /// </summary>
    interface ISettingsPersistence {
        /// <summary>
        /// Hämtar ett värde kopplat till en nyckel.
        /// </summary>
        /// <param name="key">nyckel</param>
        /// <returns>värde</returns>
        string getProperty(string key);
        /// <summary>
        /// Sparar ett värde kopplat till en nyckel
        /// </summary>
        /// <param name="key">nyckel</param>
        /// <param name="value">värde</param>
        void setProperty(string key, string value);



    }
}
