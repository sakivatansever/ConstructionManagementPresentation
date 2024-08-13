namespace ConstructionManagementPresentation.HttpClientConfiguration
{
    public static class HttpClientExtensions
    {

        public static IHttpClientBuilder AddCustomHttpClient<TService>(this IServiceCollection services, string baseAddress) where TService : class
        {
            return services.AddHttpClient<TService>(client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            });
        }
    }
}
