using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Seo.PrerenderService.Web.Configuration;

namespace Seo.PrerenderService.Web
{
    public static class Utility
    {
        private const string EscapedQueryString = "_escaped_fragment_";

        public static bool IsRequestShouldBePrerendered(RequestParams requestParams, PrerenderConfig config)
        {
            // Checke query string parameter

            //TODO

            // Check ignore files
            var extention = Path.GetExtension(requestParams.RequestUri.AbsoluteUri);
            if (!string.IsNullOrEmpty(extention))
            {
                for (int i = 0; i < config.Configuration.StaticExtentions.Count; i++)
                {
                    var extentionElm = config.Configuration.StaticExtentions[i];
                    if (string.Compare(extentionElm.Extention, extention, StringComparison.OrdinalIgnoreCase) > 0)
                    {
                        return false;
                    }
                }
            }

            // Check crawler user agent

            for (int i = 0; i < config.Configuration.Crawlers.Count; i++)
            {
                var crawlerElm = config.Configuration.Crawlers[i];
                if (crawlerElm.IsMatch(requestParams.UserAgent))
                    return true;
            }

            return false;
        }


        public static string GetRequestUrl(Uri currentUri)
        {
            if(currentUri.Query.Contains(EscapedQueryString))
            {
                
            }
            return "";//TODO
        }
    }
}
