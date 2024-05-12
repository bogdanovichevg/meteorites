using Application.DTO;
using Application.Interfaces;
using Application.MappingProfiles;
using Application.Policies;
using Application.Services;
using Application.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

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
            services.AddScoped<IValidator<MeteoritesFiltersReq>, MeteoritesFiltersReqValidation>();
            services.AddMemoryCache();

            services.AddHttpClient("MeteoriteClient")
                .AddPolicies();
        }
    }
}
