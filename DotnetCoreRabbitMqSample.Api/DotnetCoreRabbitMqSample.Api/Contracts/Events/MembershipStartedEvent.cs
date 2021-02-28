using DotnetCoreRabbitMqSample.Api.Contracts.Responses;

namespace DotnetCoreRabbitMqSample.Api.Contracts.Events
{
    public class MembershipStartedEvent
    {
        public MembershipResponse MembershipResponse { get; set; }
    }
}
