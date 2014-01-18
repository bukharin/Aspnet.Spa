using System.Web.Http;
using Owin;
using PrerenderService.Owin;
using Spa.Demo.Owin.App_Start;

namespace Spa.Demo.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            WebApiConfig.Configure(app, config);

            app.UsePrerenderer();

            app.Use<SpaMiddleware>();
        }
    }
}