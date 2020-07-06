using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Subscriber.Data;
using Subscriber.Services.Interfaces;
using Subscriber.Data.Models;
using Subscriber.Services;

namespace Subscriber.WebApi
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
            services.AddDbContext<WeightWatchers>
            (options => options.UseSqlServer(Configuration.GetConnectionString("WeightWatchersContext")));
            services.AddScoped<ICardService, CardService>();
            services.AddScoped<ICardRepository, CardRepository>();
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();
            services.AddScoped<ISubscriberServices, SubscriberServices>(); 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc(
                    "WeightWatchersOpenAPI",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "WeightWatchers API",
                        Version ="1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint(
                    "/swagger/WeightWatchersOpenAPI/swagger.json",
                    "WeightWatchers API");
            });

            app.UseRouting();

            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
