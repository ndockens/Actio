using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Actio.Common.RabbitMq;
using Actio.Common.Commands;
using Actio.Common.Mongo;
using Actio.Services.Activities.Domain.Repositories;
using Actio.Services.Activities.Repositories;
using Actio.Services.Activities.Services;
using Actio.Services.Activities.Handlers;

namespace Actio.Services.Activities
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
            services.AddControllers();
            services.AddMongoDB(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddSingleton<ICommandHandler<CreateActivity>, CreateActivityHandler>();
            services.AddSingleton<IActivityRepository, ActivityRepository>();
            services.AddSingleton<ICategoryRepository, CategoryRepository>();
            services.AddSingleton<IDatabaseSeeder, CustomMongoSeeder>();
            services.AddSingleton<IActivityService, ActivityService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.ApplicationServices
                .GetService<IDatabaseInitializer>()
                .InitializeAsync();
        }
    }
}
