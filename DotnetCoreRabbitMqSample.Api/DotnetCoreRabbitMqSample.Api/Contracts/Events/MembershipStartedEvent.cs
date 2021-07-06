using DotnetCoreRabbitMqSample.Api.Contracts.Responses;
using System;

namespace DotnetCoreRabbitMqSample.Api.Contracts.Events
{
    public class MembershipStartedEvent
    {
        public Guid Id { get; set; }
        public MembershipResponse MembershipResponse { get; set; }
    }
}
