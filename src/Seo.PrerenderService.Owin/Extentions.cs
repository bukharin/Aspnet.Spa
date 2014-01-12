using Owin;

namespace Seo.PrerenderService
{
    public static class Extentions
    {
        public static void UsePrerenderer(this IAppBuilder builder)
        {
            builder.Use<HtmlSnapshotMiddleware>();
        }
    }
}
