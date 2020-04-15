using LocationEventService.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.IoC;
using MediatR;
using LocationEventService.Data.Contexts;
using LocationEventService.Domain.CommandHandlers;
using LocationEventService.Domain.Commands;
using LocationEventService.Domain.EventHandlers;
using LocationEventService.Domain.Events;
using LocationEventService.Domain.Models;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Repositories;

namespace LocationEventService.Api
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
                new MongoClient(Configuration.GetConnectionString("LocationEventServiceMongo"))
                    .GetDatabase("LocationEventServiceMongo"));

            services.AddDbContext<DbContext, LocationEventServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocationEventServiceContext")));

            services.AddTransient<EventService>();
            services.AddTransient<IGenericRepository<Location, string>, EfGenericRepository<Location, string>>();

            //Events
            services.AddTransient<CowUpsertedEventHandler>();

            //Commands
            services.AddTransient<IRequestHandler<UpsertLocationCommand, bool>, UpsertCowLocationCommandHandler>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocationEventService Microservice", Version = "v1" }); });
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "LocationEventService API V1"); });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Subscribe<CowUpsertedEvent, CowUpsertedEventHandler>();
        }
    }
}
