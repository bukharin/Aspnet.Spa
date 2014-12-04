using Owin;

namespace PrerenderService.Owin
{
    public static class Extentions
    {
        /// <summary>
        ///     Adds snapshot middleware, that intercepts all request and generate
        ///     html layout for search machines.
        /// </summary>
        public static void UsePrerenderer(this IAppBuilder builder, bool useHtml5Mode = false)
        {
            builder.Use<HtmlSnapshotMiddleware>(useHtml5Mode);
        }
    }
}