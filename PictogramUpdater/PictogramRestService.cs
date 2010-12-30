using System;
using System.Collections.Generic;
using System.Text;
using DynamicRest;
using System.Net;

namespace PictogramUpdater {
    class PictogramRestService {
        private string hostname;

        public PictogramRestService(String hostname) {
            this.hostname = hostname;
        }



        internal bool verifyLogin(string username, string password)  {
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
    }
}

