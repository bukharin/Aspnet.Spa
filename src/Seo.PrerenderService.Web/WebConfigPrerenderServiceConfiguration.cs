using Seo.PrerenderService.Web.Configuration;

namespace Seo.PrerenderService.Web
{
    public class WebConfigPrerenderServiceConfiguration : IPrerenderServiceConfiguration
    {
        private readonly PrerenderConfig _config;

        public WebConfigPrerenderServiceConfiguration()
            :this(PrerenderConfig.GetCurrent()) { }

        public WebConfigPrerenderServiceConfiguration(PrerenderConfig config)
        {
            _config = config;
        }

        public string ServiceUrl
        {
            get { return _config.Configuration.Endpoint.Url; }
        }
        public string ProxyUrl
        {
            get { return _config.Configuration.Endpoint.Proxy; }
        }
    }
}
