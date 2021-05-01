using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Presentation.Police;

namespace WebApplication1.Extensions
{
	public static class PoliceServiceExtensions
    {
        public static IServiceCollection AddPoliceServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IPolicePresentation, PolicePresentation>();

            return services;
        }
    }
}
