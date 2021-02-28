using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public interface IMembershipService
    {
        MembershipResponse PostMembership(PostMembershipRequest postMembershipRequest);
    }
}
