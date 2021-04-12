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
using System.Collections.Generic;

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

                List<TimeSpan> timeSpans = GenerateUniqueTimeList();

                x.UsingRabbitMq((context, cfg) => {
                    cfg.UseMessageRetry(r => r.Intervals(timeSpans.ToArray()));

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

        List<TimeSpan> GenerateUniqueTimeList()
        {
            List<int> randomTimeList = new List<int>();
            List<TimeSpan> timeSpans = new List<TimeSpan>();
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int randomTimeValue = random.Next(0, 10);

                randomTimeValue = ConvertRandomTimeValueToUniqueValue(randomTimeList, randomTimeValue);

                timeSpans.Add(TimeSpan.FromSeconds(randomTimeValue));
            }

            return timeSpans;
        }

        int ConvertRandomTimeValueToUniqueValue(List<int> randomTimeList, int randomTimeValue)
        {
            bool isRandomTimeValueUnique = false;

            while (!isRandomTimeValueUnique)
            {
                randomTimeValue += 2;

                if (!randomTimeList.Contains(randomTimeValue))
                {
                    isRandomTimeValueUnique = true;
                }
            }

            return randomTimeValue;
        }
    }
}
