using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seo.PrerenderService;

namespace Seo.Prerender.Tests
{
    class TestPrerendererConfiguration : IPrerenderServiceConfiguration
    {
        public string ServiceUrl
        {
            get { return "http://service.prerender.io"; }
        }
        public string ProxyUrl
        {
            get { return null; }
        }
    }
}
