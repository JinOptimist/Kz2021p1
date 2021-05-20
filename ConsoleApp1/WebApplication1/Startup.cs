using AutoMapper;
using AutoMapper.Configuration;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Reflection;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Model.Firemen;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.EfStuff.Repositoryies.Interface;
using WebApplication1.Extensions;
using WebApplication1.Models;
using WebApplication1.Models.Education;
using WebApplication1.Models.FiremanModels;
using WebApplication1.Presentation;
using WebApplication1.Presentation.Airport;
using WebApplication1.Presentation.FirePresentation;
using WebApplication1.Profiles;
using WebApplication1.Profiles.Airport;
using WebApplication1.Services;
using WebApplication1.Utils.MiniTimeline;

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

            services.AddSingleton(x =>
                new BlobServiceClient(Configuration.GetValue<string>("AzureBlobStorageConnectionString")));

            var connectionString = Configuration.GetValue<string>("SpecialConnectionStrings");
            services.AddDbContext<KzDbContext>(option =>  option.UseSqlServer(connectionString));

            RegisterRepositories(services);
            services.AddPoliceServices(Configuration);
            services.AddScoped<ICitizenRepository, CitizenRepository>();
            services.AddScoped<IHCWorkerRepository, HCWorkerRepository>();
            services.AddScoped<IHCEstablishmentsRepository, HCEstablishmentsRepository>();

            services.AddScoped<IBlobService, BlobService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICitizenPresentation, CitizenPresentation>();
            services.AddScoped<IAirportPresentation, AirportPresentation>();
            services.AddScoped<IFireIncidentPresentation, FireIncidentPresentation>();
            services.AddScoped<IFiremanPresentation, FiremanPresentation>();
            services.AddScoped<IFireTruckPresentation, FireTruckPresentation>();
            services.AddScoped<IFiremanTeamPresentation, FiremanTeamPresentation>();
            services.AddScoped<IPupilPresentation, PupilPresentation>();
            services.AddScoped<IStudentPresentation, StudentPresentation>();
            services.AddScoped<IStudentPresentation, StudentPresentation>();
            services.AddHostedService<MiniTimeline>();
            services.AddPoliceServices(Configuration);
            services.AddScoped<IBusRepository, BusRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ITripRouteRepository, TripRouteRepository>();


            services.AddScoped<HCEstablishmentsPresentation>(x =>
                new HCEstablishmentsPresentation(
                    x.GetService<IHCEstablishmentsRepository>(),
                    x.GetService<IUserService>(),
                    x.GetService<IMapper>()));
            services.AddScoped<IFireIncidentPresentation, FireIncidentPresentation>();
            services.AddScoped<IFiremanPresentation, FiremanPresentation>();
            services.AddScoped<IFireTruckPresentation, FireTruckPresentation>();
            services.AddScoped<IFiremanTeamPresentation, FiremanTeamPresentation>();

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

            configurationExp.AddProfile<PoliceProfiles>();


            configurationExp.CreateMap<FireIncident, FireIncidentViewModel>()
                .ForMember(nameof(FireIncidentViewModel.TeamName),
                    opt => opt.MapFrom(incident => incident.FiremanTeam.TeamName));
            configurationExp.CreateMap<Fireman, FiremanViewModel>()
                .ForMember(nameof(FiremanViewModel.TeamName),
                    opt => opt.MapFrom(fireman => fireman.FiremanTeam.TeamName))
                .ForMember(nameof(FiremanViewModel.Name),
                    opt => opt.MapFrom(fireman => fireman.Citizen.Name))
                .ForMember(nameof(FiremanViewModel.Age),
                    opt => opt.MapFrom(fireman => fireman.Citizen.Age));
            configurationExp.CreateMap<FiremanTeam, FiremanTeamViewModel>()
                .ForMember(nameof(FiremanTeamViewModel.TruckState),
                    opt => opt.MapFrom(t => t.FireTruck.TruckState))
                .ForMember(nameof(FiremanTeamViewModel.FiremanCount),
                    opt => opt.MapFrom(t => t.Firemen.Count()));
            configurationExp.CreateMap<FiremanViewModel, Fireman>();
            configurationExp.CreateMap<FiremanTeamViewModel, FiremanTeam>();
            configurationExp.CreateMap<FireIncidentViewModel, FireIncident>();

            MapBothSide<FireTruck, FireTruckViewModel>(configurationExp);
            configurationExp.AddProfile<AirportProfiles>();             
            MapBothSide<Citizen, FullProfileViewModel>(configurationExp);
            MapBothSide<Bus, BusParkViewModel>(configurationExp);
            MapBothSide<Order, OrderViewModel>(configurationExp);
            MapBothSide<TripRoute, TripViewModel>(configurationExp);
            MapBothSide<HCWorker, HCWorkerViewModel>(configurationExp);
            MapBothSide<HCEstablishmentsViewModel, HCEstablishments>(configurationExp);


            MapBothSide<Student, StudentViewModel>(configurationExp);
            MapBothSide<Pupil, PupilViewModel>(configurationExp);
            MapBothSide<Student, Pupil>(configurationExp);
            MapBothSide<University, UniversityViewModel>(configurationExp);
            MapBothSide<School, SchoolViewModel>(configurationExp);
            MapBothSide<Certificate, CertificateViewModel>(configurationExp);

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