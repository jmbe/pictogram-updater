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




            if (operation.statusCode == HttpStatusCode.Unauthorized) {
                return false;
            } else if (operation.statusCode == HttpStatusCode.OK) {
                return true;
            }

            throw operation.Error as Exception;
        }

        private dynamic createRestClient(string path, RestService type) {
            return new RestClient("http://" + hostname + "/rest/pictogram-1.0" + path, type);
        }
    }
}

