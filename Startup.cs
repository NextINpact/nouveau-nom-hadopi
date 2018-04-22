﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.ClassicBundles;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NomHadopi.Models.EF;

namespace NomHadopi
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
            //services.AddDbContext<NameSuggestDbContext>(options => options.UseSqlite("Data Source=Database.db"));
            var connection = Configuration["ConnectionStrings:MainDatabase"];

            services.AddDbContext<NameSuggestDbContext>(options => options.UseMySql(connection));
            services.AddMvc();
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
            }

            app.UseBundles(env, bundles =>
            {

                bundles.Add(new ScriptBundle("~/js/site.bundle.js")

                    .Include("~/js/site.js"));

                bundles.Add(new StyleBundle("~/css/site.bundle.css")
                      .Include("~/css/site.css"));


                bundles.Add(new ScriptBundle("~/js/validators.bundle.js").Include("~/js/validators.js"));



            });

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}