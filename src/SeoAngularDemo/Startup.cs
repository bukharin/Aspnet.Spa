using System.Web.Http;
using Owin;
using Seo.PrerenderService;
using SeoAngularDemo.App_Start;

namespace SeoAngularDemo
{
    public class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Configure(app, config);

            app.UsePrerenderer();
        }

    }
}