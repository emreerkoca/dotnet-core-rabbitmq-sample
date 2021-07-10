using DotnetCoreRabbitMqSample.Api.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.HostedServices
{
    public class MissingMessagePublisher : IHostedService
    {
        private Timer _timer;
        private readonly IServiceProvider _serviceProvider;

        public MissingMessagePublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async o => await PublishMissingMessages(), null, TimeSpan.Zero, TimeSpan.FromMinutes(10));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        private async Task PublishMissingMessages()
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                IMessageBrokerService _messageBrokerService = scope.ServiceProvider.GetRequiredService<IMessageBrokerService>();

                await _messageBrokerService.PublishMissingMessages();
            }
        }
    }
}
