using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FileBagWebApi.Bussiness.Implementation;
using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.DataAccess.Context;
using FileBagWebApi.DataAccess.EntityFramework;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Services;
using FileBagWebApi.Services.Interfaces;
using FileBagWebApi.Utilities.NetCore;
using FileBagWebApi.Utilities.NetCore.Interfaces;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace FileBagWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton(typeof(IApplicationDataAccess), typeof(ApplicationDataAccess));
            services.AddSingleton(typeof(IApplicationBussiness), typeof(ApplicationBussiness));
            services.AddSingleton(typeof(IInMemoryCache), typeof(InMemoryCache));
            services.AddSingleton(typeof(FileBagContext), typeof(FileBagContext));


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileBagWebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FileBagWebAPI V1");
            });
        }
    }
}
