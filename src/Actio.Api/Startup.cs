using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Actio.Api.Handlers;
using Actio.Api.Repositories;
using Actio.Common.Auth;
using Actio.Common.Events;
using Actio.Common.Mongo;
using Actio.Common.RabbitMq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Actio.Api
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
            services.AddRabbitMq(Configuration);
            services.AddLogging();
            services.AddMongoDb(Configuration);
            services.AddScoped<IEventHandler<UserCreated>, UserCreatedHandler>();
            services.AddScoped<IEventHandler<ActivityCreated>, ActivityCreatedHandler>();
            services.AddScoped<IEventHandler<UserAuthenticated>, UserAuthenticatedHandler>();
            services.AddScoped<IActivityRepository, ActivityRepository>();
            services.AddJwt(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
