using MyFirstAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace MyFirstAPI.Handlers
{
    public class APIKeyHandler : DelegatingHandler
    {
        private const string ApiKey = "AIzaSyClzfrOzB818x55FASHvX4JuGQciR9lv7q";
        private readonly MyFirstDbEntities db = new MyFirstDbEntities();
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            bool validApi = false;
            IEnumerable<string> requesHeader;
            var checkApiKeyExist = request.Headers.TryGetValues("ApiKey-First-App", out requesHeader);

            var metodo = request.Method.ToString();
            if (metodo == "OPTIONS") {
                return request.CreateResponse(
                    HttpStatusCode.OK);
            }

            if (checkApiKeyExist) {

                var HttpApiKey = requesHeader.FirstOrDefault().ToString();

                if (db.T_USER.Where(w=>w.APIKEY == HttpApiKey).Any()) {
                    validApi = true;
                }
            }

            if (!validApi) {
                return request.CreateResponse(
                    HttpStatusCode.Forbidden, 
                    new { 
                        error = "API Key Invalid", 
                        status = HttpStatusCode.Forbidden 
                    });
            }

            var response = await base.SendAsync(request, cancellationToken);

            return response;
        }
    }
}