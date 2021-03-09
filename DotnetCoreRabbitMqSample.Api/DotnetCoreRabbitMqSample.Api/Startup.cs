using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MassTransit;
using DotnetCoreRabbitMqSample.Api.Consumers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using GreenPipes;
using DotnetCoreRabbitMqSample.Api.Services;

namespace DotnetCoreRabbitMqSample.Api
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

            services.AddHealthChecks();

            services.Configure<HealthCheckPublisherOptions>(options =>
            {
                options.Delay = TimeSpan.FromSeconds(2);
                options.Predicate = (check) => check.Tags.Contains("ready");
            });
                
            services.AddMassTransit(x =>
            {
                x.AddConsumer<MembershipStartedEventConsumer>();

                x.SetKebabCaseEndpointNameFormatter();

                x.UsingRabbitMq((context, cfg) => {
                    cfg.UseMessageRetry(r => r.Incremental(5, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(2)));

                    cfg.ReceiveEndpoint("MembershipStartedEventConsumerQueue", e =>
                    {
                        e.ConfigureConsumer<MembershipStartedEventConsumer>(context);
                    });
                });
            });

            services.AddMassTransitHostedService();
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IMembershipService, MembershipService>();
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

                endpoints.MapHealthChecks("/health/ready", new HealthCheckOptions()
                {
                    Predicate = (check) => check.Tags.Contains("ready"),
                });

                endpoints.MapHealthChecks("/health/live", new HealthCheckOptions());
            });
        }
    }
}
