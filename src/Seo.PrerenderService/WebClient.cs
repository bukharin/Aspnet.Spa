using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seo.PrerenderService
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
            var serviceUrl = _config.ServiceUrl.EndsWith("/") ? _config.ServiceUrl : _config.ServiceUrl + "/";
            var webRequest = (HttpWebRequest)WebRequest.Create(serviceUrl + uri);

            webRequest.Method = "GET";
            if (!string.IsNullOrEmpty(_config.ProxyUrl))
            {
                webRequest.Proxy = new WebProxy(_config.ProxyUrl);
            }

            return await webRequest.GetResponseAsync();
        }
    }
}
