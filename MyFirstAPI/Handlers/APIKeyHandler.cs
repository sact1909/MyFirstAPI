using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Web.Http;
using System.Web.Http.Dispatcher;

namespace MyFirstAPI.Handlers
{
    public class APIKeyHandler : DelegatingHandler
    {
        private const string ApiKey = "AIzaSyClzfrOzB818x55FASHvX4JuGQciR9lv7q";
        
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequestMessage, CancellationToken cancellationToken) 
        {

            bool validKey = false;
            IEnumerable<string> requestHeader;
            var checkApiKeyExist = httpRequestMessage.Headers.TryGetValues("OAuth-ApiKey-Hi", out requestHeader);
            if (checkApiKeyExist)
            {
                
                var HttpAPIKey = requestHeader.FirstOrDefault().ToString();
                if (requestHeader.FirstOrDefault().Equals(ApiKey)) {
                    validKey = true;
                }
            }

            if (!validKey) {
                return httpRequestMessage.CreateResponse(
                    HttpStatusCode.Forbidden, 
                    new { 
                        error = "API Key Invalid" , 
                        status = HttpStatusCode.Forbidden
                    });
            }
            
            var response = await base.SendAsync(httpRequestMessage, cancellationToken);
            
            return response;
        }

       
    }
}