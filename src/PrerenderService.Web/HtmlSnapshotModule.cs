using System;
using System.Configuration;
using System.Web;
using PrerenderService.Configuration;
using PrerenderService.Service;

namespace PrerenderService.Web
{
    public class HtmlSnapshotModule : IHttpModule
    {
        private PrerenderConfig _config;
        private HttpApplication _context;

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            _context = context;

            _context.BeginRequest += ContextBeginRequest;
            _config = PrerenderConfig.GetCurrent();
        }

        public void Dispose()
        {
            //do nothing
        }

        #endregion

        protected void ContextBeginRequest(object sender, EventArgs e)
        {
            HttpContext httpContext = _context.Context;
            //read current

            var requestParams = new RequestParams
                                    {
                                        RequestUri = httpContext.Request.Url,
                                        UserAgent = httpContext.Request.UserAgent
                                    };
            if (Utility.IsRequestShouldBePrerendered(requestParams, _config))
            {
                var rendererConfig = new PrerenderServiceConfiguration(_config);
                var renderer = new SnapshotRenderer(rendererConfig);

                string snapshotUrl = Utility.GetSnapshotUrl(httpContext.Request.Url,
                    string.Equals(ConfigurationManager.AppSettings["UseHtml5Mode"], "true"));
                //render page html
                PrerenderResult response;
                try
                {
                    response = renderer.RenderPage(snapshotUrl).Result;
                }
                catch (AggregateException ex)
                {
                    string messages = "";
                    for (int i = 0; i < ex.InnerExceptions.Count; i++)
                    {
                        messages += ex.InnerExceptions[i].Message + Environment.NewLine;
                    }
                    httpContext.Response.Write(
                        string.Format(
                            "Failed to prerender request '{0}'. One or more exeptions has occured: '{1}'. Stack trace: '{2}'",
                            snapshotUrl, messages, ex.StackTrace));
                    httpContext.Response.StatusCode = 500;
                    httpContext.Response.Flush();
                    _context.CompleteRequest();
                    return;
                }

                httpContext.Response.Write(response.Content);
                httpContext.Response.ContentType = "text/html";
                httpContext.Response.StatusCode = (int) response.StatusCode;
                httpContext.Response.Flush();
                _context.CompleteRequest();
            }
        }
    }
}