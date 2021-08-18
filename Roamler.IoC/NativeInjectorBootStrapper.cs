using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Roamler.Application.Interfaces;
using Roamler.Application.Services;
using Roamler.Data;
using Roamler.Domain.Interfaces;

namespace Equinox.Infra.CrossCutting.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, bool useEfInMemoryRepository)
        {
            // Application
            services.AddScoped<ILocationAppService, LocationAppService>();

            // Infra - Data
            if (useEfInMemoryRepository)
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("Roamler"));
                services.AddScoped<ILocationRepository, EfLocationRepository>();

                var dbContext = services.BuildServiceProvider().GetService<ApplicationContext>();
                dbContext.Database.EnsureCreated();
            }
            else
                services.AddScoped<ILocationRepository, LocationRepository>();
        }
    }
}