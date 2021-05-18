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
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Presentation.Television;
using WebApplication1.EfStuff.Repositoryies.Television;
using WebApplication1.EfStuff.Model.Television;
using WebApplication1.Models.Television;

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
            services.AddScoped<ICitizenRepository, CitizenRepository>();
            services.AddScoped<IHCWorkerRepository, HCWorkerRepository>();
            services.AddScoped<IHCEstablishmentsRepository, HCEstablishmentsRepository>();

            services.AddScoped<IUserService>(x =>
                new UserService(
                    x.GetService<ICitizenRepository>(),
                    x.GetService<IHttpContextAccessor>())
                );

            services.AddScoped<CitizenPresentation>(x =>
                new CitizenPresentation(
                    x.GetService<ICitizenRepository>(),
                    x.GetService<IUserService>(),
                    x.GetService<IMapper>()));

            services.AddScoped<HCEstablishmentsPresentation>(x =>
                new HCEstablishmentsPresentation(
                    x.GetService<IHCEstablishmentsRepository>(),
                    x.GetService<IUserService>(),
                    x.GetService<IMapper>()));

            services.AddScoped<TvChannelPresentation>(x =>
                new TvChannelPresentation(
                    x.GetService<TvChannelRepository>(),
                    x.GetService<IMapper>(),
                    x.GetService<ICitizenRepository>(),
                    x.GetService<TvStaffRepository>()));

            services.AddScoped<TvProgrammePresentation>(x =>
                new TvProgrammePresentation(
                    x.GetService<TvProgrammeRepository>(),
                    x.GetService<IMapper>(),
                    x.GetService<IUserService>(),
                    x.GetService<IWebHostEnvironment>()));

            services.AddScoped<TvStaffPresentation>(x =>
                new TvStaffPresentation(
                    x.GetService<TvStaffRepository>(),
                    x.GetService<TvProgrammeStaffRepository>(),
                    x.GetService<IMapper>(),
                    x.GetService<IUserService>(),
                    x.GetService<ICitizenRepository>(),
                    x.GetService<TvProgrammeRepository>()));

            services.AddScoped<TvSchedulePresentation>(x =>
                new TvSchedulePresentation(
                    x.GetService<TvScheduleRepository>(),
                    x.GetService<IMapper>(),
                    x.GetService<TvProgrammeRepository>(),
                    x.GetService<IUserService>()));

            services.AddScoped<TvCelebrityPresentation>(x =>
                new TvCelebrityPresentation(
                    x.GetService<IMapper>(),
                    x.GetService<IUserService>(),
                    x.GetService<ICitizenRepository>(),
                    x.GetService<TvCelebrityRepository>(),
                    x.GetService<TvProgrammeCelebrityRepository>(),
                    x.GetService<TvProgrammeRepository>()));

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
            var repositoryTypes = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(type =>
                        type.BaseType?.IsGenericType == true
                        && type.BaseType.GetGenericTypeDefinition() == typeof(BaseRepository<>));

            foreach (var repositoryType in repositoryTypes)
            {
                var repositortInterfaces = repositoryType.GetInterfaces()
                    .FirstOrDefault(i => i.Name != typeof(IBaseRepository<>).Name);

                if (repositortInterfaces != null)
                {
                    services.AddScoped(repositortInterfaces, repositoryType);
                }
                else
                {
                    services.AddScoped(repositoryType, x =>
                    {
                        var constructor = repositoryType.GetConstructors().Single();
                        var parameters = new object[] { x.GetService<KzDbContext>() };
                        return constructor.Invoke(parameters);
                    });
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
            MapBothSide<HCWorker, HCWorkerViewModel>(configurationExp);
            MapBothSide<HCEstablishmentsViewModel, HCEstablishments>(configurationExp);

            configurationExp.CreateMap<TvStaff, TvStaffViewModel>()
               .ForMember(nameof(TvStaffViewModel.Name),
                           opt => opt.MapFrom(staff => staff.Citizen.Name));
            configurationExp.CreateMap<TvStaffViewModel, TvStaff>();

            configurationExp.CreateMap<TvCelebrity, TvCelebrityViewModel>()
                .ForMember(nameof(TvCelebrityViewModel.Name),
                            opt => opt.MapFrom(staff => staff.Citizen.Name));
            configurationExp.CreateMap<TvCelebrityViewModel, TvCelebrity>();

            configurationExp.CreateMap<TvChannel, TvChannelViewModel>()
                .ForMember(nameof(TvChannelViewModel.StaffCount),
                        opt => opt.MapFrom(channel => channel.Staff.Count))
                .ForMember(nameof(TvChannelViewModel.ProgrammeCount),
                        opt => opt.MapFrom(channel => channel.Programmes.Count));
            configurationExp.CreateMap<TvChannelViewModel, TvChannel>();

            MapBothSide<TvProgramme, TvProgrammeViewModel>(configurationExp);
            MapBothSide<TvProgramme, TvProgrammeShortViewModel>(configurationExp);
            MapBothSide<TvSchedule, TvScheduleViewModel>(configurationExp);
            MapBothSide<TvProgrammeStaff, TvProgrammeStaffViewModel>(configurationExp);
            MapBothSide<TvProgrammeCelebrity, TvProgrammeCelebrityViewModel>(configurationExp);

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
