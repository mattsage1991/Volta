using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Volta.BuildingBlocks.Application;
using Volta.BuildingBlocks.Infrastructure;
using Volta.Stocks.Application.Commands.CreateStock;
using Volta.Stocks.Application.Setup;
using Volta.Stocks.Domain.Stocks;
using Volta.Stocks.Domain.Stocks.Services;
using Volta.Stocks.Infrastructure;
using Volta.Stocks.Infrastructure.Repositories;
using Volta.Stocks.Infrastructure.Setup;
using Volta.Stocks.WebApi.Services;

namespace Volta.Stocks.WebApi
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
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddHttpClient();
            //services.AddMediatR(typeof(CreateStockCommand));
            services.AddHostedService<TimedHostedService>();
            //services.AddDbContext<StockDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("Stocks"));
            //});
            //services.AddScoped<IUnitOfWork, UnitOfWork<StockDbContext>>();
            //services.AddHttpClient<IStockLookup, IEXCloudService>();
            //services.AddScoped<IStockRepository, StockRepository>();
            //services.AddTransient<IRequestExecutor, RequestExecutor>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationAutofacModule(Configuration.GetConnectionString("StockDatabase")));
            builder.RegisterModule(new InfrastructureAutofacModule(
                Configuration.GetConnectionString("StockDatabase"), 
                Configuration["EventBusRetryCount"], 
                Configuration["SubscriptionClientName"],
                Configuration["EventBusConnection"],
                Configuration["EventBusUserName"],
                Configuration["EventBusPassword"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //migrate
                //var dbContext = app.ApplicationServices.GetService<StockDbContext>();
                //dbContext?.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stocks Service");
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/healthcheck");
            });
        }
    }
}
