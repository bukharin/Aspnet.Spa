using System.Configuration;

namespace Seo.PrerenderService.Web.Configuration
{
    public class PrerenderSection : ConfigurationSection
    {
        public PrerenderSection()
        {
            this.StaticExtentions = new StaticExtentionsColletion();
            this.Crawlers = new CrawlersColletion();
            this.Endpoint = new EndpointElement { Url = "http://service.prerender.io" };
        }

        [ConfigurationProperty("crawlers", IsRequired = false)]
        public CrawlersColletion Crawlers
        {
            get
            {
                return ((CrawlersColletion)(this["crawlers"]));
            }
            set
            {
                this["crawlers"] = value;
            }
        }

        [ConfigurationProperty("static-extentions", IsRequired = false)]
        public StaticExtentionsColletion StaticExtentions
        {
            get
            {
                return ((StaticExtentionsColletion)(this["static-extentions"]));
            }
            set
            {
                this["static-extentions"] = value;
            }
        }

        [ConfigurationProperty("endpoint", IsRequired = false)]
        public EndpointElement Endpoint
        {
            get
            {
                return this["endpoint"] as EndpointElement;
            }
            set
            {
                this["endpoint"] = value;
            }
        }

        public sealed partial class EndpointElement : ConfigurationElement
        {
            [ConfigurationProperty("url", IsRequired = true)]
            public string Url
            {
                get
                {
                    return this["url"].ToString();
                }
                set
                {
                    this["url"] = value;
                }
            }

            [ConfigurationProperty("proxy", IsRequired = true)]
            public string Proxy
            {
                get
                {
                    return this["proxy"].ToString();
                }
                set
                {
                    this["proxy"] = value;
                }
            }

        }

        [ConfigurationCollection(typeof(CrawlerElement))]
        public sealed partial class CrawlersColletion : ConfigurationElementCollection
        {
            public CrawlersColletion()
            {
                BaseAdd(new CrawlerElement { UserAgent = "googlebot", Match = "/googlebot/i" });
                BaseAdd(new CrawlerElement { UserAgent = "yandexbot", Match = "/YandexBot/i" });
                BaseAdd(new CrawlerElement { UserAgent = "bingbot" });
                BaseAdd(new CrawlerElement { UserAgent = "baiduspider" });
                BaseAdd(new CrawlerElement { UserAgent = "facebookexternalhit" });
                BaseAdd(new CrawlerElement { UserAgent = "twitterbot" });
                BaseAdd(new CrawlerElement { UserAgent = "yahoo" });
            }

            public CrawlerElement this[int i]
            {
                get
                {
                    return ((CrawlerElement)(BaseGet(i)));
                }
            }

            protected override ConfigurationElement CreateNewElement()
            {
                return new CrawlerElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((CrawlerElement)element).UserAgent;
            }
        }

        [ConfigurationCollection(typeof(StaticExtentionElement))]
        public sealed partial class StaticExtentionsColletion : ConfigurationElementCollection
        {
            public StaticExtentionsColletion()
            {
                var extentions = new[]
                                     {
                                        ".js", ".css", ".less", ".html",".htm",
                                        ".png", ".jpg", ".jpeg", ".gif", ".tif", 
                                        ".pdf", ".doc", ".txt", ".zip", ".mp3", ".rar", ".exe", ".wmv", ".doc", ".avi", ".ppt", ".mpg",
                                        ".mpeg", ".wav", ".mov", ".psd", ".ai", ".xls", ".mp4", ".m4a", ".swf", ".dat", ".dmg",
                                        ".iso", ".flv", ".m4v", ".torrent"
                                     };
                foreach (var extention in extentions)
                {
                    BaseAdd(new StaticExtentionElement { Extention = extention });
                }
            }

            public StaticExtentionElement this[int i]
            {
                get
                {
                    return ((StaticExtentionElement)(BaseGet(i)));
                }
            }

            protected override ConfigurationElement CreateNewElement()
            {
                return new StaticExtentionElement();
            }

            protected override object GetElementKey(ConfigurationElement element)
            {
                return ((StaticExtentionElement)element).Extention;
            }
        }

        public sealed class CrawlerElement : ConfigurationElement
        {
            [ConfigurationProperty("useragent", IsRequired = true)]
            public string UserAgent
            {
                get
                {
                    return this["useragent"].ToString();
                }
                set
                {
                    this["useragent"] = value;
                }
            }

            [ConfigurationProperty("match", IsRequired = false)]
            public string Match
            {
                get
                {
                    return this["match"].ToString();
                }
                set
                {
                    this["match"] = value;
                }
            }

            /// <summary>
            ///     Determine if specified user agent match this config element rule
            /// </summary>
            /// <param name="userAgent">Useragent to determine</param>
            /// <returns></returns>
            public bool IsMatch(string userAgent)
            {
                return false;//TODO
            }
        }

        public sealed class StaticExtentionElement : ConfigurationElement
        {
            [ConfigurationProperty("extention", IsRequired = true)]
            public string Extention
            {
                get
                {
                    return this["extention"].ToString();
                }
                set
                {
                    this["extention"] = value;
                }
            }
        }
    }
}
