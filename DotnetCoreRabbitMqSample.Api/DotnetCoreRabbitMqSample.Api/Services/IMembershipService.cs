using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public interface IMembershipService
    {
        Task<MembershipResponse> PostMembership(PostMembershipRequest postMembershipRequest);
    }
}
