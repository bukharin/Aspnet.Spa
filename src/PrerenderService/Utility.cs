using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using PrerenderService.Configuration;

namespace PrerenderService
{
    public static class Utility
    {
        private const string EscapedQueryStringParameterName = "_escaped_fragment_";

        public static bool IsRequestShouldBePrerendered(RequestParams requestParams, PrerenderConfig config)
        {
            // Check '_escaped_fragment_' query string parameter

            if (!string.IsNullOrEmpty(requestParams.RequestUri.Query) &&
                requestParams.RequestUri.Query.Contains(EscapedQueryStringParameterName))
            {
                return true;
            }

            // Check ignore file extentions
            string extention = Path.GetExtension(requestParams.RequestUri.AbsoluteUri);
            if (!string.IsNullOrEmpty(extention))
            {
                for (int i = 0; i < config.Configuration.IgnoreExtentions.Count; i++)
                {
                    PrerenderSection.IgnoreExtentionElement extentionElm = config.Configuration.IgnoreExtentions[i];
                    if (string.Equals(extentionElm.Extention, extention, StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }
                }
            }

            // Check crawler user agent
            if (requestParams.UserAgent != null)
            {
                for (int i = 0; i < config.Configuration.Crawlers.Count; i++)
                {
                    PrerenderSection.CrawlerElement crawlerElm = config.Configuration.Crawlers[i];
                    if (crawlerElm.IsMatch(requestParams.UserAgent))
                        return true;
                }
            }

            return false;
        }


        public static string GetSnapshotUrl(Uri currentUri)
        {
            string query = currentUri.Query;
            if (string.IsNullOrEmpty(query))
                return currentUri.ToString();

            if (!query.Contains(EscapedQueryStringParameterName))
                return currentUri.ToString();

            NameValueCollection queryString = HttpUtility.ParseQueryString(query);
            //Get current escaped fragment parameter value (#!/path/to/route)
            string fragment = queryString[EscapedQueryStringParameterName];
            if (fragment == "")
                fragment = currentUri.AbsolutePath;
            //TODO support query string parameters in fragment
            var builder = new UriBuilder(currentUri.Scheme, currentUri.Host, currentUri.Port)
                              {
                                  Query = GetQuery(queryString, EscapedQueryStringParameterName),
                                  Path = fragment //replace current path with fragment
                              };
            return builder.Uri.ToString();
        }


        private static string GetQuery(NameValueCollection queryString, string except)
        {
            string[] array = (from key in queryString.AllKeys
                              from value in queryString.GetValues(key)
                              where key != except
                              select string.Format("{0}={1}", key, value)).ToArray();
            return string.Join("&", array);
        }
    }
}