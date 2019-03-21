using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using TheWall.Models;
using Microsoft.EntityFrameworkCore;

namespace TheWall {
    public class Startup {
        public IConfiguration Configuration { get; }
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<TheWallContext>(options => options.UseMySql(Configuration["DBInfo:ConnectionString"]));
            services.AddMvc ();
            services.AddSession ();
        }

        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }
            app.UseStaticFiles ();
            app.UseSession ();
            app.UseMvc ();
        }
    }
}