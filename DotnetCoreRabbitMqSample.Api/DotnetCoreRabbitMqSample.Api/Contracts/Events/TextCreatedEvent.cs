using DotnetCoreRabbitMqSample.Api.Contracts.Responses;

namespace DotnetCoreRabbitMqSample.Api.Contracts.Events
{
    public class TextCreatedEvent
    {
        public TextResponse TextResponse { get; set; }
    }
}
