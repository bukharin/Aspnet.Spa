using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;
using Microsoft.Owin;

namespace Spa.Demo.Owin
{
    /// <summary>
    ///     Returns index.html if request is virtual (not api or static resource)
    ///     Using client routing, if user refresh the page he should still get index.html page
    /// </summary>
    public class SpaMiddleware : OwinMiddleware
    {
        public SpaMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            string path = HostingEnvironment.MapPath("~/index.html");
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync(File.ReadAllBytes(path));
        }
    }
}