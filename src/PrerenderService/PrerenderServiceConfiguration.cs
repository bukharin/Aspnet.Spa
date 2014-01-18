using PrerenderService.Configuration;
using PrerenderService.Service;

namespace PrerenderService
{
    public class PrerenderServiceConfiguration : IPrerenderServiceConfiguration
    {
        private readonly PrerenderConfig _config;

        public PrerenderServiceConfiguration()
            : this(PrerenderConfig.GetCurrent())
        {
        }

        public PrerenderServiceConfiguration(PrerenderConfig config)
        {
            _config = config;
        }

        #region IPrerenderServiceConfiguration Members

        public string ServiceUrl
        {
            get { return _config.Configuration.Endpoint.Url; }
        }

        public string ProxyUrl
        {
            get { return _config.Configuration.Endpoint.Proxy; }
        }

        #endregion
    }
}