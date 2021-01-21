using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Consumers
{
    public class SampleEventConsumer : IConsumer<SampleEvent>
    {
        ILogger<SampleEventConsumer> _logger;

        public SampleEventConsumer(ILogger<SampleEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<SampleEvent> context)
        {
            _logger.LogInformation("Value: {Value}", context.Message.SampleProperty);
            _logger.LogInformation("Time: " + DateTime.UtcNow.ToString());
        }
    }   
}
