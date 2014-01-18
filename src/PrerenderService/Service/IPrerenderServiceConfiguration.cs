namespace PrerenderService.Service
{
    public interface IPrerenderServiceConfiguration
    {
        string ServiceUrl { get; }

        string ProxyUrl { get; }
    }
}