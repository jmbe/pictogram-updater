using System;
using System.Collections.Generic;
using System.Text;

namespace DownloadManager {

    /// <summary>
    /// Interface f�r persistenslagret.
    /// </summary>
    interface ISettingsPersistence {
        /// <summary>
        /// H�mtar ett v�rde kopplat till en nyckel.
        /// </summary>
        /// <param name="key">nyckel</param>
        /// <returns>v�rde</returns>
        string getProperty(string key);
        /// <summary>
        /// Sparar ett v�rde kopplat till en nyckel
        /// </summary>
        /// <param name="key">nyckel</param>
        /// <param name="value">v�rde</param>
        void setProperty(string key, string value);



    }
}
