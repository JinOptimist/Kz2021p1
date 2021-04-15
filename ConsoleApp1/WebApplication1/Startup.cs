using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using System.Linq;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Airport;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Airport;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Airport;
using WebApplication1.ViewModels;
using WebApplication1.Services;
using WebApplication1.Profiles;
using Newtonsoft.Json;
using WebApplication1.Presentation;
using System.Reflection;

namespace WebApplication1
{
	public class Startup
    {
        public const string AuthMethod = "Smile";

        public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddNewtonsoftJson();
			services.AddOpenApiDocument();
			services.AddRazorPages()
				 .AddRazorRuntimeCompilation();

			var connectionString = Configuration.GetValue<string>("SpecialConnectionStrings");
            services.AddDbContext<KzDbContext>(option => option.UseSqlServer(connectionString));

            RegisterRepositories(services);

            services.AddScoped<UserService>(x =>
                new UserService(
                    x.GetService<CitizenRepository>(),
                    x.GetService<IHttpContextAccessor>())
                );

            services.AddScoped<CitizenPresentation>(x => 
                new CitizenPresentation(x.GetService<CitizenRepository>()));

            services.AddPoliceServices(Configuration);
            RegisterAutoMapper(services);

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "Smile";
                    config.LoginPath = "/Citizen/Login";
                    config.AccessDeniedPath = "/Citizen/Login";
                });

            services.AddHttpContextAccessor();
            services.AddPaging();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
            foreach (var repositoryType in Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type =>
                        type.BaseType?.IsGenericType == true
                        && type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>)))
            {
                services.AddScoped(repositoryType, x =>
                {
                    var constructor = repositoryType.GetConstructors().Single();
                    var parameters = new object[] { x.GetService<KzDbContext>() };
                    return constructor.Invoke(parameters);
                });
            }
        }

        private void RegisterAutoMapper(IServiceCollection services)
        {
            var configurationExp = new MapperConfigurationExpression();

            configurationExp.CreateMap<Adress, AdressViewModel>()
                .ForMember(nameof(AdressViewModel.CitizenCount),
                    opt => opt.MapFrom(adress => adress.Citizens.Count()));
            configurationExp.CreateMap<AdressViewModel, Adress>();

            configurationExp.CreateMap<IncomingFlightInfo, IncomingFlightInfoViewModel>();
            configurationExp.CreateMap<IncomingFlightInfoViewModel, IncomingFlightInfo>();

            configurationExp.CreateMap<DepartingFlightInfo, DepartingFlightInfoViewModel>();
            configurationExp.CreateMap<DepartingFlightInfoViewModel, DepartingFlightInfo>();
            
            configurationExp.AddProfile<PoliceProfiles>();

            configurationExp.CreateMap<Fireman, FiremanShowViewModel>()
                .ForMember(nameof(FiremanShowViewModel.Name),
                        opt => opt.MapFrom(fireman => fireman.Citizen.Name))
                .ForMember(nameof(FiremanShowViewModel.Age),
                        opt => opt.MapFrom(fireman => fireman.Citizen.Age));

            configurationExp.CreateMap<FiremanShowViewModel, Fireman>();

            MapBothSide<Fireman, FiremanViewModel>(configurationExp);
            MapBothSide<Citizen, FullProfileViewModel>(configurationExp);
            MapBothSide<Bus, BusParkViewModel>(configurationExp);
            MapBothSide<TripRoute, TripViewModel>(configurationExp);

            var config = new MapperConfiguration(configurationExp);
            var mapper = new Mapper(config);
            services.AddScoped<IMapper>(x => mapper);
        }

        public void MapBothSide<Type1, Type2>(MapperConfigurationExpression configurationExp)
        {
            configurationExp.CreateMap<Type1, Type2>();
            configurationExp.CreateMap<Type2, Type1>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
