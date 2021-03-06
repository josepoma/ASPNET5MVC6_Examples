﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using ASPNET5MVC6_Examples.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;
using ASPNET5MVC6_Examples.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using ASPNET5MVC6_Examples.ViewModel;

namespace ASPNET5MVC6_Examples
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;
        public Startup(IApplicationEnvironment appEnv)
        {
            var builder = new ConfigurationBuilder().
                SetBasePath(appEnv.ApplicationBasePath).
                AddJsonFile("config.json").
                AddEnvironmentVariables();

            Configuration = builder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Añadiendo MVC
            services.AddMvc().AddJsonOptions(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            //Añadiendo EF7 y agregando un DBContext
            services.AddEntityFramework().AddSqlServer().AddDbContext<WorldContext>();

            //Añadir un Mapper
            Mapper.Initialize(config =>
            {
                config.CreateMap<Trip, TripViewModel>().ReverseMap();
                config.CreateMap<Stop, StopViewModel>().ReverseMap();
            });

            services.AddTransient<WorldContextSeedData>();

            services.AddScoped<IMailService, DebugMailService>();
            services.AddScoped<IWorldRepository, WorldRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, WorldContextSeedData seeder,ILoggerFactory loggerFactory)
        {
            //app.UseIISPlatformHandler();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});

            //app.UseDefaultFiles();

            loggerFactory.AddDebug(LogLevel.Debug);

            app.UseStaticFiles();

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new { controller = "App", action = "Index" }
                );
            });

            seeder.EnsureSeedData();

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
