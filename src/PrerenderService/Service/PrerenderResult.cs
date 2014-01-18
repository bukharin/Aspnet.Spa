using System.Net;

namespace PrerenderService.Service
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