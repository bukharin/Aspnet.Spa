using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Seo.PrerenderService.Web;
using Seo.PrerenderService.Web.Configuration;

namespace Seo.PrerenderService
{
    public class HtmlSnapshotMiddleware : OwinMiddleware
    {
        public HtmlSnapshotMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var config = PrerenderConfig.GetCurrent();
            var requestParams = new RequestParams
                                    {
                                        RequestUri = context.Request.Uri,
                                        UserAgent = context.Request.Headers["user-agent"]
                                    };
            if (Utility.IsRequestShouldBePrerendered(requestParams, config))
            {
                var rendererConfig = new WebConfigPrerenderServiceConfiguration(config);
                var renderer = new SnapshotRenderer(rendererConfig);


                var snapshotUrl = Utility.GetSnapshotUrl(context.Request.Uri);
                //render page html
                PrerenderResult response;
                try
                {
                    response = await renderer.RenderPage(snapshotUrl);
                }
                catch (WebException e)
                {
                    context.Response.Write(string.Format("Failed to prerender request '{0}'. Error message: '{1}'. Stack trace: '{2}'", snapshotUrl, e.Message, e.StackTrace));
                    context.Response.StatusCode = 500;
                    return;
                }

                await context.Response.WriteAsync(response.Content);
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = (int)response.StatusCode;
            }
            else
            {
                await Next.Invoke(context);
            }
        }
    }
}
