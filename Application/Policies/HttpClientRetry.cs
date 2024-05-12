using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Application.Policies
{
    public static class HttpClientRetry
    {
        public static void AddPolicies(this IHttpClientBuilder httpClientBuilder)
        {
            httpClientBuilder
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(5, _ => TimeSpan.FromSeconds(10)));
        }
    }
}
