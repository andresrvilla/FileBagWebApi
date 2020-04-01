using FileBagWebApi.Bussiness.Implementation;
using FileBagWebApi.Bussiness.Interfaces;
using FileBagWebApi.DataAccess.Context;
using FileBagWebApi.DataAccess.EntityFramework;
using FileBagWebApi.DataAccess.Interfaces;
using FileBagWebApi.Infraestructure;
using FileBagWebApi.Utilities.NetCore;
using FileBagWebApi.Utilities.NetCore.Interfaces;
using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Reflection;

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
            services.AddControllers();

            services.AddSingleton(typeof(IApplicationDataAccess), typeof(ApplicationDataAccess));
            services.AddSingleton(typeof(ITokenProvider), (x => new JWTProvider(Configuration.GetValue<double>("tokenExpiration"))));
            services.AddSingleton<IApplicationBussiness>(x => new ApplicationBussiness(x.GetRequiredService<IApplicationDataAccess>(), x.GetRequiredService<ITokenProvider>(), Configuration.GetValue<string>("secret")));
            services.AddSingleton(typeof(IServiceTokenValidator), typeof(ServiceTokenValidator));
            services.AddSingleton(typeof(IMemoryCache), typeof(MemoryCache));
            services.AddSingleton(typeof(IInMemoryCache), typeof(InMemoryCache));
            services.AddSingleton(typeof(FileBagContext), typeof(FileBagContext));

            // Add API Versioning to as service to your project 
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FileBagWebAPI", Version = "v1" });
            });
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
