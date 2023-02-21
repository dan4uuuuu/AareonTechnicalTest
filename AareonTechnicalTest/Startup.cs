using AareonTechnicalTest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using NLog.Web;
using System;

namespace AareonTechnicalTest
{
    public class Startup
    {
        public static readonly ILoggerFactory CustomLoggerFactory
        = LoggerFactory.Create(builder => builder.AddConsole());
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<ApplicationContext>(c => c.UseSqlite(o =>
            {
                o.CommandTimeout(10000);
            }).EnableSensitiveDataLogging()
            .UseLoggerFactory(CustomLoggerFactory));

            services.AddIdentity<Person, IdentityRole>(
             options =>
             {
                 options.SignIn.RequireConfirmedAccount = false;
                 options.User.RequireUniqueEmail = false;
             })
            .AddEntityFrameworkStores<ApplicationContext>();

            services.AddAuthentication();
            services.AddAuthorization();


            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.SetMinimumLevel(LogLevel.Trace);
                string? environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                if (environmentName == "Production")
                {
                    loggingBuilder.AddNLog("nlog.Production.config");
                }
                else
                {
                    loggingBuilder.AddNLog("nlog.config");
                }
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AareonTechnicalTest", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AareonTechnicalTest v1"));
            }

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
