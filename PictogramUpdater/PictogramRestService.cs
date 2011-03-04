using System;
using System.Collections.Generic;
using System.Text;
using DynamicRest;
using System.Net;
using System.IO;
using System.Globalization;

namespace PictogramUpdater {
    class PictogramRestService {

        public event LogToFileCallback LogTofile;

        private string hostname;

        public PictogramRestService(String hostname) {
            this.hostname = hostname;
        }



        internal bool verifyLogin(string username, string password) {
            dynamic client = createRestClient("/accounts/" + username, RestService.Xml).withCredentials(new NetworkCredential(username, password));
            dynamic operation = client.check();

            if (operation.StatusCode == HttpStatusCode.Unauthorized) {
                return false;
            } else if (operation.StatusCode == HttpStatusCode.OK) {
                return true;
            }

            throw operation.Error as Exception;
        }

        private dynamic createRestClient(string path, RestService type) {
            return new RestClient("http://" + hostname + "/rest/pictogram-1.0" + path, type);
        }

        internal IList<Language> getSwedishLanguageNames() {
            List<Language> languages = new List<Language>();

            dynamic client = createRestClient("/languages/", RestService.Xml);
            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            foreach (dynamic language in operation.Result.SelectAll("language")) {
                languages.Add(new Language(language.@name, language.@SwedishName));
            }

            return languages;
        }

        internal List<PictogramEntry> getAvailableSoundsByLanguage(string languageCode) {
            List<PictogramEntry> result = new List<PictogramEntry>();

            dynamic client = createRestClient("/languages/" + languageCode + "/sounds/", RestService.Xml);
            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            foreach (dynamic sound in operation.Result.SelectAll("sound")) {
                try {
                    DateTime modified = DateTime.ParseExact(sound.@modified, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    result.Add(new PictogramEntry(sound.@name, "", modified));
                } catch (FormatException e) {
                    /* Some incorrect names are expected. */
                    LogTofile(e.ToString());
                }
            }

            return result;
        }

        internal List<PictogramEntry> getPictogramPhrasesByLanguage(string languageCode) {

            List<PictogramEntry> result = new List<PictogramEntry>();

            dynamic client = createRestClient("/languages/" + languageCode + "/pictograms/", RestService.Xml);
            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            foreach (dynamic pictogram in operation.Result.SelectAll("pictogram")) {
                String text = pictogram.SelectAll("translation")[0].@text;
                String code = pictogram.@name;

                try {
                    DateTime modified = DateTime.ParseExact(pictogram.@modified, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                    result.Add(new PictogramEntry(code, text, modified));
                } catch (FormatException e) {
                    /* Some incorrect names are expected. */
                    LogTofile(e.ToString());
                }
            }

            return result;
        }


        internal void downloadWmf(string Username, string Password, string pictogramCode, string languageCode, Stream output) {
            /* "96" is the size, not pictogram id. */
            dynamic client = createRestClient("/languages/" + languageCode + "/pictograms/" + pictogramCode + "/images/96.wmf", RestService.Binary).withCredentials(new NetworkCredential(Username, Password));

            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            operation.Result.CopyTo(output);
        }

        internal void downloadSound(string Username, string Password, string pictogramCode, string languageCode, Stream output) {
            dynamic client = createRestClient("/languages/" + languageCode + "/sounds/" + pictogramCode + "/", RestService.Binary).withCredentials(new NetworkCredential(Username, Password));

            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            operation.Result.CopyTo(output);
        }

    }
}


