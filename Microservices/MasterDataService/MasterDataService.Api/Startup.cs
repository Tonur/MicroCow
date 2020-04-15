using MasterDataService.Application.Service;
using MasterDataService.Data.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MediatR;
using RabbitMQ.IoC;
using MasterDataService.Domain.CommandHandlers;
using MasterDataService.Domain.Commands;
using MasterDataService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using Shared.Interfaces;
using Shared.Repositories;

namespace MasterDataService.Api
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
                new MongoClient(Configuration.GetConnectionString("MasterDataServiceMongo"))
                    .GetDatabase("MasterDataServiceMongo"));

            services.AddDbContext<DbContext, MasterDataServiceContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("MasterDataServiceContext")));

            services.AddTransient<DataService>();
            services.AddTransient<IGenericRepository<Cow, string>, EfGenericRepository<Cow, string>>();

            services.AddTransient<IRequestHandler<UpsertCowCommand, bool>, UpsertCowCommandHandler>();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "MasterDataService Microservice", Version = "v1" }); });
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
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "MasterDataService API V1"); });

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
