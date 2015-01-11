using System.Configuration;
using System.Text.RegularExpressions;

namespace PrerenderService.Configuration
{
    public class PrerenderSection : ConfigurationSection
    {
        public PrerenderSection()
        {
            IgnoreExtentions = new IgnoreExtentionsColletion();
            Crawlers = new CrawlersColletion();
            Endpoint = new EndpointElement {Url = "http://service.prerender.io"};
        }

        [ConfigurationProperty("crawlers", IsRequired = false)]
        public CrawlersColletion Crawlers
        {
            get { return ((CrawlersColletion) (this["crawlers"])); }
            set { this["crawlers"] = value; }
        }

        [ConfigurationProperty("ignoreExtentions", IsRequired = false)]
        public IgnoreExtentionsColletion IgnoreExtentions
        {
            get { return ((IgnoreExtentionsColletion) (this["ignoreExtentions"])); }
            set { this["ignoreExtentions"] = value; }
        }

        [ConfigurationProperty("endpoint", IsRequired = false)]
        public EndpointElement Endpoint
        {
            get { return this["endpoint"] as EndpointElement; }
            set { this["endpoint"] = value; }
        }

        #region Nested type: CrawlerElement

        public sealed class CrawlerElement : ConfigurationElement
        {
            [ConfigurationProperty("useragent", IsRequired = true)]
            public string UserAgent
            {
                get { return this["useragent"].ToString(); }
                set { this["useragent"] = value; }
            }

            /// <summary>
            ///     Determine if specified user agent match this config element rule
            /// </summary>
            /// <param name="userAgent">Useragent to determine</param>
            public bool IsMatch(string userAgent)
            {
                var regEx = new Regex(UserAgent, RegexOptions.IgnoreCase);
                return regEx.IsMatch(userAgent);
            }
        }

        #endregion

        #region Nested type: CrawlersColletion

        [ConfigurationCollection(typeof (CrawlerElement))]
        public sealed class CrawlersColletion : ConfigurationElementCollection
        {
            public CrawlersColletion()
            {
                BaseAdd(new CrawlerElement {UserAgent = "googlebot"});

                BaseAdd(new CrawlerElement {UserAgent = "yandexbot"});
                BaseAdd(new CrawlerElement {UserAgent = "bingbot"});
                BaseAdd(new CrawlerElement {UserAgent = "^baiduspider$"});
                BaseAdd(new CrawlerElement {UserAgent = @"^baiduspider\+"});
                BaseAdd(new CrawlerElement {UserAgent = "^facebookexternalhit"});
                BaseAdd(new CrawlerElement {UserAgent = "^twitterbot"});
                BaseAdd(new CrawlerElement {UserAgent = "^yahoo"});

                // Google plus
                BaseAdd(new CrawlerElement {UserAgent = @"^Google \(\+"});

                // Google Adsense
                BaseAdd(new CrawlerElement {UserAgent = "adsbot-google"});
                BaseAdd(new CrawlerElement {UserAgent = "mediapartners-google"});
                
                // Wayback Machine
                BaseAdd(new CrawlerElement {UserAgent = "ia_archiver"});
            }

            public CrawlerElement this[int i]
            {
                get { return ((CrawlerElement) (BaseGet(i))); }
            }

            protected override ConfigurationElement CreateNewElement()
            {
                return new CrawlerElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((CrawlerElement) element).UserAgent;
            }
        }

        #endregion

        #region Nested type: EndpointElement

        public sealed class EndpointElement : ConfigurationElement
        {
            [ConfigurationProperty("url", IsRequired = true)]
            public string Url
            {
                get { return this["url"].ToString(); }
                set { this["url"] = value; }
            }

            [ConfigurationProperty("proxy", IsRequired = false)]
            public string Proxy
            {
                get { return this["proxy"].ToString(); }
                set { this["proxy"] = value; }
            }
        }

        #endregion

        #region Nested type: IgnoreExtentionElement

        public sealed class IgnoreExtentionElement : ConfigurationElement
        {
            [ConfigurationProperty("extention", IsRequired = true)]
            public string Extention
            {
                get { return this["extention"].ToString(); }
                set { this["extention"] = value; }
            }
        }

        #endregion

        #region Nested type: IgnoreExtentionsColletion

        [ConfigurationCollection(typeof (IgnoreExtentionElement))]
        public sealed class IgnoreExtentionsColletion : ConfigurationElementCollection
        {
            public IgnoreExtentionsColletion()
            {
                var extentions = new[]
                                     {
                                         ".js", ".css", ".less", ".html", ".htm",
                                         ".png", ".jpg", ".jpeg", ".gif", ".tif",
                                         ".pdf", ".doc", ".txt", ".zip", ".mp3", ".rar", ".exe", ".wmv", ".doc", ".avi",
                                         ".ppt", ".mpg",
                                         ".mpeg", ".wav", ".mov", ".psd", ".ai", ".xls", ".mp4", ".m4a", ".swf", ".dat",
                                         ".dmg",
                                         ".iso", ".flv", ".m4v", ".torrent"
                                     };
                foreach (string extention in extentions)
                {
                    BaseAdd(new IgnoreExtentionElement {Extention = extention});
                }
            }

            public IgnoreExtentionElement this[int i]
            {
                get { return ((IgnoreExtentionElement) (BaseGet(i))); }
            }

            protected override ConfigurationElement CreateNewElement()
            {
                return new IgnoreExtentionElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((IgnoreExtentionElement) element).Extention;
            }
        }

        #endregion
    }
}