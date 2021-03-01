using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using MassTransit;
using MassTransit.ConsumeConfigurators;
using MassTransit.Definition;
using Microsoft.Extensions.Logging;
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

    public class TextCreatedEventConsumerDefinition : ConsumerDefinition<TextCreatedEventConsumer>
    {
        public TextCreatedEventConsumerDefinition()
        {
            Endpoint(x => x.PrefetchCount = 1000);
        }

        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator, 
            IConsumerConfigurator<TextCreatedEventConsumer> consumerConfigurator)
        {
            consumerConfigurator.Options<BatchOptions>(options => options
                .SetMessageLimit(100)
                .SetTimeLimit(1000)
                .SetConcurrencyLimit(10));
        }
    }
}
