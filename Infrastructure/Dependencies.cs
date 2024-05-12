using Domain.Interfaces;
using Infrastructure.DTO;
using Infrastructure.Interfaces;
using Infrastructure.Services.Api;
using Infrastructure.Services.Provider;
using Infrastructure.Services.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Dependencies
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddTransient<IPostgreSqlProvider, PostgreSqlProvider>();
            services.AddTransient<IMeteoriteRepository, MeteoriteRepository>();
            services.AddTransient<INasaApiService, NasaApiServices>();
        }
    }
}
