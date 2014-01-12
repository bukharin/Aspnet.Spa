namespace Seo.PrerenderService.Web
{
    /// <summary>
    ///     Determine identity of the robots (google, bing, etc)
    /// </summary>
    public class CrawlerIdentity
    {
        /// <summary>
        ///     User agent of client
        /// </summary>
        public string UserAgent { get; set; }
        /// <summary>
        ///     Regular expression that compare user agent, 
        ///     null or empty - use simple string comparsion
        /// </summary>
        public string Match { get; set; }

        /// <summary>
        ///     Determines if client useragent match current crawler identity
        /// </summary>
        /// <param name="userAgent">User agent of the client</param>
        public bool IsMatch(string userAgent)
        {
            return false;//TODO
        }
    }
}
