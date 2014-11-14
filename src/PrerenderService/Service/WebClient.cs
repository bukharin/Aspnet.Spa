using System.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace PrerenderService.Service
{
    internal class WebClient
    {
        private readonly IPrerenderServiceConfiguration _config;

        public WebClient(IPrerenderServiceConfiguration config)
        {
            _config = config;
        }

        public async Task<WebResponse> Get(string uri)
        {
            string serviceUrl = _config.ServiceUrl.EndsWith("/") ? _config.ServiceUrl : _config.ServiceUrl + "/";
            var webRequest = (HttpWebRequest) WebRequest.Create(serviceUrl + uri);

            var token = System.Environment.GetEnvironmentVariable("Prerender-Token") ?? ConfigurationManager.AppSettings["Prerender-Token"];
            if (!string.IsNullOrEmpty(token))
                webRequest.Headers.Add("X-Prerender-Token", token);

            webRequest.Method = "GET";
            if (!string.IsNullOrEmpty(_config.ProxyUrl))
            {
                webRequest.Proxy = new WebProxy(_config.ProxyUrl);
            }

            return await webRequest.GetResponseAsync().ConfigureAwait(false);
        }
    }
}