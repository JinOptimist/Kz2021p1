using AutoMapper;
using AutoMapper.Configuration;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Airport;
using WebApplication1.Services;
using WebApplication1.Profiles;
using WebApplication1.Presentation;
using WebApplication1.Presentation.Airport;
using WebApplication1.Profiles.Airport;

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
            services.AddPoliceServices(Configuration);

           // services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICitizenPresentation, CitizenPresentation>();
            services.AddScoped<IAirportPresentation, AirportPresentation>();

            services.AddScoped<IPupilPresentation, PupilPresentation>();
            services.AddScoped<IStudentPresentation, StudentPresentation>();
            services.AddScoped<IStudentPresentation, StudentPresentation>();

            RegisterAutoMapper(services);

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "Smile";
                    config.LoginPath = "/Citizen/Login";
                    config.AccessDeniedPath = "/Citizen/Login";
                });

            services.AddHttpContextAccessor();
        }

        private void RegisterRepositories(IServiceCollection services)
        {
			IEnumerable<Type> implementationsType = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type =>
                        !type.IsInterface && type.GetInterface(typeof(IBaseRepository<>).Name) != null);

            foreach (Type implementationType in implementationsType)
            {
				IEnumerable<Type> servicesType = implementationType
                    .GetInterfaces()
                    .Where(r => !r.Name.Contains(typeof(IBaseRepository<>).Name));

				foreach (Type serviceType in servicesType)
				{
                    services.AddScoped(serviceType, implementationType);
                }
            }
        }

        private void RegisterAutoMapper(IServiceCollection services)
        {
            var configurationExp = new MapperConfigurationExpression();

            configurationExp.CreateMap<Adress, AdressViewModel>()
                .ForMember(nameof(AdressViewModel.CitizenCount),
                    opt => opt.MapFrom(adress => adress.Citizens.Count()));
            configurationExp.CreateMap<AdressViewModel, Adress>();

            configurationExp.AddProfile<PoliceProfiles>();
            configurationExp.AddProfile<AirportProfiles>();
            configurationExp.CreateMap<Fireman, FiremanShowViewModel>()
                .ForMember(nameof(FiremanShowViewModel.Name),
                        opt => opt.MapFrom(fireman => fireman.Citizen.Name))
                .ForMember(nameof(FiremanShowViewModel.Age),
                        opt => opt.MapFrom(fireman => fireman.Citizen.Age));

            MapBothSide<Fireman, FiremanViewModel>(configurationExp);
            MapBothSide<Citizen, FullProfileViewModel>(configurationExp);
            MapBothSide<Bus, BusParkViewModel>(configurationExp);
            MapBothSide<TripRoute, TripViewModel>(configurationExp);

            MapBothSide<Student, StudentViewModel>(configurationExp);
            MapBothSide<Pupil, PupilViewModel>(configurationExp);
            MapBothSide<University, UniversityViewModel>(configurationExp);
            MapBothSide<School, SchoolViewModel>(configurationExp);

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