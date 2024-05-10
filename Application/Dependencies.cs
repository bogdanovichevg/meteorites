using Application.DTO;
using Application.Interfaces;
using Application.MappingProfile;
using Application.Services;
using Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace Application
{
    public static class Dependencies
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<IMeteoriteServices, MeteoriteServices>();
            services.AddAutoMapper(typeof(MeteoriteProfile));
            services.AddFluentValidationAutoValidation();
            services.AddScoped<IValidator<Pagination>, PaginationValidation>();
            services.AddScoped<IValidator<ReqFilterMeteorites>, GetMeteoritesInfoValidation>();
            services.AddMemoryCache();

            services.AddHttpClient("MeteoriteClient")
                .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(5, _ => TimeSpan.FromSeconds(10)));
        }
    }
}
