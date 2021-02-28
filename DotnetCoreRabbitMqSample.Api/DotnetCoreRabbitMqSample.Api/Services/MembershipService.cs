using AutoMapper;
using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;
using MassTransit;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IBusControl _busControl;
        private readonly IMapper _mapper;

        public MembershipService(IBusControl busControl, IMapper mapper)
        {
            _busControl = busControl;
            _mapper = mapper;
        }

        public MembershipResponse PostMembership(PostMembershipRequest postMembershipRequest)
        {
            //We suppose we saved user's data
            MembershipResponse membershipResponse = _mapper.Map<MembershipResponse>(postMembershipRequest);

            _busControl.Publish<MembershipStartedEvent>(new MembershipStartedEvent
            {
                MembershipResponse = membershipResponse
            });

            return membershipResponse;
        }
    }
}
