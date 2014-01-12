using System.Net;

namespace Seo.PrerenderService
{
    public class PrerenderResult
    {
        /// <summary>
        ///     HTML content of the page
        /// </summary>
        public string Content { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
