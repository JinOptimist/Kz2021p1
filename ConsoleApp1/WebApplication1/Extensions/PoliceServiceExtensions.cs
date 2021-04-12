using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Services.Police;

namespace WebApplication1.Extensions
{
    public static class PoliceServiceExtensions
    {
        public static IServiceCollection AddPoliceServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPoliceService, PoliceService>();

            services.AddScoped<PoliceRepository>(p => new PoliceRepository(
                p.GetService<KzDbContext>()));
            services.AddScoped<PoliceAcademyRepository>(p => new PoliceAcademyRepository(
                p.GetService<KzDbContext>()));
            services.AddScoped<ViolationsRepository>(p => new ViolationsRepository(
                p.GetService<KzDbContext>()));
            services.AddScoped<PoliceCallRepo>(p => new PoliceCallRepo(
                p.GetService<KzDbContext>()));
            return services;
        }
    }
}
