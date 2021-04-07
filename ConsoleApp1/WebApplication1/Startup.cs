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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using WebApplication1.Services;

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
            var connectionString = Configuration.GetValue<string>("SpecialConnectionStrings");
            services.AddDbContext<KzDbContext>(option => option.UseSqlServer(connectionString));

            services.AddScoped<CitizenRepository>(x =>
                new CitizenRepository(x.GetService<KzDbContext>())
                );

            services.AddScoped<AdressRepository>(x =>
                new AdressRepository(x.GetService<KzDbContext>())
                );
           services.AddScoped<FiremanRepository>(x =>
                new FiremanRepository(x.GetService<KzDbContext>())
            );


            services.AddScoped<UserService>(x =>
                new UserService(
                    x.GetService<CitizenRepository>(),
                    x.GetService<IHttpContextAccessor>())
                );

            RegisterAutoMapper(services);

            services.AddAuthentication(AuthMethod)
                .AddCookie(AuthMethod, config =>
                {
                    config.Cookie.Name = "Smile";
                    config.LoginPath = "/Citizen/Login";
                });

            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
        }

        private void RegisterAutoMapper(IServiceCollection services)
        {
            var configurationExp = new MapperConfigurationExpression();

            configurationExp.CreateMap<Adress, AdressViewModel>()
                .ForMember(nameof(AdressViewModel.CitizenCount),
                    opt => opt.MapFrom(adress => adress.Citizens.Count()));
            configurationExp.CreateMap<AdressViewModel, Adress>();

            configurationExp.CreateMap<Fireman, FiremanViewModel>();
            configurationExp.CreateMap<FiremanViewModel, Fireman>();

            configurationExp.CreateMap<Fireman, FiremanShowViewModel>()
             .ForMember(nameof(FiremanShowViewModel.Name),
                    opt => opt.MapFrom(fireman => fireman.Citizen.Name))
            .ForMember(nameof(FiremanShowViewModel.Age),
                    opt => opt.MapFrom(fireman => fireman.Citizen.Age));

            configurationExp.CreateMap<FiremanShowViewModel, Fireman>();

            var config = new MapperConfiguration(configurationExp);
            var mapper = new Mapper(config);
            services.AddScoped<IMapper>(x => mapper);
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
