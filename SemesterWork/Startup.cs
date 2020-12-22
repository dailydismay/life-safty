using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SemesterWork.Services;
using Npgsql;
using SemesterWork.DAL.Repository;
using SemesterWork.DAL.Repository.Interfaces;
using SemesterWork.Libs.Filters;

using SemesterWork.Libs;

namespace SemesterWork
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            try
            {
                var conn = new NpgsqlConnection("Host=localhost;Username=dev;Password=dev;Database=dev;");
                conn.Open();
                services.AddSingleton(typeof(HelloService));
                services.AddSingleton<UserRepo>(new UserRepo(conn));
                services.AddSingleton<DoctorRepo>(new DoctorRepo(conn));
                services.AddSingleton(typeof(TokenService));
                services.AddSingleton(typeof(AuthService));
                services.AddSingleton(typeof(Libs.Filters.AuthorizeFilter));
                services.AddSingleton(typeof(OnlyAuthorized));
                services.AddSingleton(typeof(DoctorService));
                services.AddSingleton(typeof(UserService));
                services.AddLogging(log =>
                {
                    log.AddConsole();
                });
                services.AddRazorPages(opts =>
                {
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
