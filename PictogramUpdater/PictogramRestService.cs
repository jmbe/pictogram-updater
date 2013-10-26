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

        internal IList<Language> getForeignLanguageNames() {
            List<Language> languages = new List<Language>();

            dynamic client = createRestClient("/languages/", RestService.Xml);
            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            foreach (dynamic language in operation.Result.SelectAll("language")) {
                languages.Add(new Language(language.@name, language.@NativeName));
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
                    DateTimeOffset dto = DateTimeOffset.Parse(sound.@modified);
                    DateTime modified = dto.DateTime;
                    result.Add(new PictogramEntry(sound.@name, "", "", modified));
                } catch (FormatException e) {
                    /* Some incorrect names are expected. */
                    LogTofile(e.ToString());
                }
            }

            return result;
        }

        internal List<PictogramEntry> getPictogramPhrasesByLanguage(Language language) {

            string languageCode = language.Code.ToLower();

            if (language.IsTextless) {
                /* Get Swedish list, since ws does not support not providing any language. */
                languageCode = "sv";
            }

            List<PictogramEntry> result = new List<PictogramEntry>();

            dynamic client = createRestClient("/languages/" + languageCode + "/pictograms/", RestService.Xml);
            dynamic operation = client.get();

            if (operation.Error != null) {
                throw operation.Error as Exception;
            }

            foreach (dynamic pictogram in operation.Result.SelectAll("pictogram")) {
                dynamic translation = pictogram.SelectAll("translation")[0];
                String discriminator = translation.@discriminator;
                String text = translation.@text;
                String code = pictogram.@name;

                try {
                    DateTimeOffset dto = DateTimeOffset.Parse(pictogram.@modified);
                    DateTime modified = dto.DateTime;
                    result.Add(new PictogramEntry(code, text, discriminator, modified));
                } catch (FormatException e) {
                    /* Some incorrect names are expected. */
                    LogTofile(e.ToString());
                }
            }

            return result;
        }

        internal void downloadImage(string Username, string Password, string pictogramCode, string languageCode, InstallationType installationType, LanguageSelection selection, Stream output) {

            int size = 500;

            if (selection.IsVectorFormat) {
                size = 96;
            }

            string url = "/languages/" + languageCode + "/pictograms/" + pictogramCode + "/images/" + size + "." + selection.ImageFormat.Extension;

            if (InstallationType.TEXTLESS.Equals(installationType)) {
                url += "?textless=true";
            }
            dynamic client = createRestClient(url, RestService.Binary).withCredentials(new NetworkCredential(Username, Password));

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


