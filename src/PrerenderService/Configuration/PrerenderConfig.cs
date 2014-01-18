using System.Configuration;

namespace PrerenderService.Configuration
{
    public class PrerenderConfig
    {
        public PrerenderSection Configuration { get; set; }

        public static PrerenderConfig GetCurrent()
        {
            var config = ((PrerenderSection) (ConfigurationManager.GetSection("prerender")));
            return new PrerenderConfig {Configuration = config ?? new PrerenderSection()};
        }
    }
}