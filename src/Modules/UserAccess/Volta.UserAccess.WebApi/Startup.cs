using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Volta.UserAccess.Application.Setup;
using Volta.UserAccess.Infrastructure;
using Volta.UserAccess.Infrastructure.Setup;

namespace Volta.UserAccess.WebApi
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
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddHealthChecks();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ApplicationAutofacModule(Configuration.GetConnectionString("Database")));
            builder.RegisterModule(new InfrastructureAutofacModule(Configuration.GetConnectionString("Database")));

            //builder.RegisterModule(new RabbitMQAutofacModule(
            //    Configuration["EventBusRetryCount"],t
            //    Configuration["SubscriptionClientName"],
            //    Configuration["EventBusConnection"],
            //    Configuration["EventBusUserName"],
            //    Configuration["EventBusPassword"]));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //migrate
            var container = app.ApplicationServices.GetRequiredService<ILifetimeScope>();
            using var scope = container.BeginLifetimeScope();
            var stocksContext = scope.Resolve<UsersContext>();

            if (stocksContext.Database.IsSqlServer())
            {
                stocksContext.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock API");
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
