using System;
using System.Collections.Generic;
using System.Linq;
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

                //TODO process escaped url
                var response = await renderer.RenderPage(context.Request.Uri);

                context.Response.Write(response.Content);
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
