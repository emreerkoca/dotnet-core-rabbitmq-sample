using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Consumers
{
    public class TextCreatedEventConsumer : IConsumer<Batch<TextCreatedEvent>>
    {
        ILogger<TextCreatedEventConsumer> _logger;

        public TextCreatedEventConsumer(ILogger<TextCreatedEventConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<Batch<TextCreatedEvent>> context)
        {

            for (int i = 0; i < context.Message.Length; i++)
            {
                ConsumeContext<TextCreatedEvent> consumeContext = context.Message[i];
                TextCreatedEvent textCreatedEvent = consumeContext.Message;

                _logger.LogInformation(JsonSerializer.Serialize(textCreatedEvent.TextResponse));
            }
        }
    }
}
