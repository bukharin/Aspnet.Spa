using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using Seo.PrerenderService.Web.Configuration;

namespace Seo.PrerenderService.Web
{
    public static class Utility
    {
        private const string EscapedQueryStringParameterName = "_escaped_fragment_";

        public static bool IsRequestShouldBePrerendered(RequestParams requestParams, PrerenderConfig config)
        {
            // Check '_escaped_fragment_' query string parameter

            if(!string.IsNullOrEmpty(requestParams.RequestUri.Query) &&
                requestParams.RequestUri.Query.Contains(EscapedQueryStringParameterName))
            {
                return true;
            }

            // Check ignore file extentions
            var extention = Path.GetExtension(requestParams.RequestUri.AbsoluteUri);
            if (!string.IsNullOrEmpty(extention))
            {
                for (int i = 0; i < config.Configuration.IgnoreExtentions.Count; i++)
                {
                    var extentionElm = config.Configuration.IgnoreExtentions[i];
                    if (string.Equals(extentionElm.Extention, extention, StringComparison.OrdinalIgnoreCase))
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


        public static string GetSnapshotUrl(Uri currentUri)
        {
            var query = currentUri.Query;
            if (string.IsNullOrEmpty(query))
                return currentUri.ToString();

            if (!query.Contains(EscapedQueryStringParameterName))
                return currentUri.ToString();

            var queryString = ParseQueryString(query);
            //Get current escaped fragment parameter value (#!/path/to/route)
            var fragment = queryString[EscapedQueryStringParameterName];
            //TODO support query string parameters in fragment
            var builder = new UriBuilder(currentUri.Scheme, currentUri.Host, currentUri.Port)
                              {
                                  Query = GetQuery(queryString, EscapedQueryStringParameterName),
                                  Path = fragment//replace current path with fragment
                              };
            return builder.Uri.ToString();
        }


        /// <summary>
        ///     Parses query string into namevaluecollection, like HttpUtility.ParseQueryString
        ///     but without dependency on System.Web.dll
        /// </summary>
        private static NameValueCollection ParseQueryString(string queryString)
        {
            var queryParameters = new NameValueCollection();
            string[] querySegments = queryString.Split('&');
            foreach (string segment in querySegments)
            {
                string[] parts = segment.Split('=');
                if (parts.Length > 0)
                {
                    string key = parts[0].Trim(new[] { '?', ' ' });
                    string val = parts[1].Trim();

                    queryParameters.Add(key, val);
                }
            }
            return queryParameters;
        }

        private static string GetQuery(NameValueCollection queryString, string except)
        {
            var array = (from key in queryString.AllKeys
                         from value in queryString.GetValues(key)
                         where key != except
                         select string.Format("{0}={1}", key, value)).ToArray();
            return string.Join("&", array);
        }
    }
}
