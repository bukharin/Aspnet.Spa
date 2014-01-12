using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Seo.PrerenderService
{
    public class SnapshotRenderer
    {
        private readonly WebClient _client;

        public SnapshotRenderer(IPrerenderServiceConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");
            _client = new WebClient(configuration);
        }

        public async Task<PrerenderResult> RenderPage(Uri url)
        {
            if (url == null)
                throw new ArgumentNullException("url");
            var response = await _client.Get(url) as HttpWebResponse;
            if(response==null)
                throw new SystemException("response is not HttpWebResponse");

            var respStream = response.GetResponseStream() ;
            if (respStream == null)
                throw new WebException(string.Format("Failed to load '{0}', response stream is null.", url));
            
            var reader = new StreamReader(respStream, Encoding.UTF8);

            return new PrerenderResult
                       {
                           Content = reader.ReadToEnd(),
                           StatusCode = response.StatusCode
                       };

        }
    }
}
