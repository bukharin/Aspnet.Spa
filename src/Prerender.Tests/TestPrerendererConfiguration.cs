using PrerenderService.Service;

namespace Prerender.Tests
{
    internal class TestPrerendererConfiguration : IPrerenderServiceConfiguration
    {
        #region IPrerenderServiceConfiguration Members

        public string ServiceUrl
        {
            get { return "http://service.prerender.io"; }
        }

        public string ProxyUrl
        {
            get { return null; }
        }

        #endregion
    }
}