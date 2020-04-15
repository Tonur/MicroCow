using LocationQueryService.Application.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using Newtonsoft.Json;
using RabbitMQ.IoC;
using LocationQueryService.Data.Contexts;
using LocationQueryService.Domain.EventHandlers;
using LocationQueryService.Domain.Events;
using LocationQueryService.Domain.Models;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using RabbitMQ.Bus.Bus.Interfaces;
using Shared.Interfaces;
using Shared.Repositories;

namespace LocationQueryService.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            services.AddControllers().AddNewtonsoftJson(options => 
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddRabbitMq();
            services.AddMediatR(typeof(Startup));

            services.AddTransient(provider =>
                new MongoClient(Configuration.GetConnectionString("LocationQueryServiceMongo"))
                    .GetDatabase("LocationQueryServiceMongo"));

            services.AddDbContext<DbContext, LocationQueryServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocationQueryServiceContext")));

            services.AddTransient<QueryService>();
            services.AddTransient<IGenericRepository<CowLocation, string>, EfGenericRepository<CowLocation, string>>();

            //Events
            services.AddTransient<CowUpsertedEventHandler>();
            services.AddTransient<LocationUpsertedEventHandler>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationQueryService Microservice", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "LocationQueryService API V1"); });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Subscribe<CowUpsertedEvent, CowUpsertedEventHandler>();
            app.Subscribe<LocationUpsertedEvent, LocationUpsertedEventHandler>();
        }
    }
}
