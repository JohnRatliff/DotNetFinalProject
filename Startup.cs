using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DotNetFinalProject.Models;

namespace DotNetFinalProject
{
    public class Startup
    {
        // Used to access the data from appsettings.json.  To access the connection string itself, use Configuration["DBInfo:ConnectionString"]
        public IConfiguration Configuration {get;}

        // "inject" the default IConfiguration service, which will bind to appsettings.json by default and assign it to the cofiguration property.
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
                // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddDbContext<DotNetFinalProjectDbContext>(options => options.UseMySql (Configuration["DBInfo:ConnectionString"]));
            services.AddDbContext<UserDbContext>(options => options.UseMySql (Configuration["DBInfo:ConnectionString"]));
            services.AddSession();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc();
        }
    }
}
