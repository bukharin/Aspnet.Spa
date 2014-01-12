using System;
using System.Net;
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

        public async Task<WebResponse> Get(Uri uri)
        {
            Uri endpoint;
            if (!Uri.TryCreate(new Uri(_config.ServiceUrl), uri.AbsoluteUri, out endpoint))
            {
                throw new WebException(string.Format("Unable to create url to request service: serviceUrl='{0}', requestUrl: '{1}'",_config.ServiceUrl, uri.AbsoluteUri));
            }

            var webRequest = (HttpWebRequest)WebRequest.Create(endpoint);
            webRequest.Method = "GET";

            if (!string.IsNullOrEmpty(_config.ProxyUrl))
            {
                webRequest.Proxy = new WebProxy(_config.ProxyUrl);
            }

            return await webRequest.GetResponseAsync();
        }
    }
}
