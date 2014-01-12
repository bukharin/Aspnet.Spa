namespace Seo.PrerenderService
{
    public interface IPrerenderServiceConfiguration
    {
        string ServiceUrl { get; }

        string ProxyUrl { get; }
    }
}
