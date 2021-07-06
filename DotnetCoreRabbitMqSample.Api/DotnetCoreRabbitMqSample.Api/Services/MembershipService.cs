using AutoMapper;
using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;
using DotnetCoreRabbitMqSample.Api.Data;
using MassTransit;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DotnetCoreRabbitMqSample.Api.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IBusControl _busControl;
        private readonly IMapper _mapper;
        private readonly AppDbContext _appDbContext;

        public MembershipService(IBusControl busControl, IMapper mapper)
        {
            _busControl = busControl;
            _mapper = mapper;
        }

        public async Task<MembershipResponse> PostMembership(PostMembershipRequest postMembershipRequest)
        {
            //We suppose we saved user's data
            MembershipResponse membershipResponse = _mapper.Map<MembershipResponse>(postMembershipRequest);
            MembershipStartedEvent membershipStartedEvent = new MembershipStartedEvent
            {
                Id = Guid.NewGuid(),
                MembershipResponse = membershipResponse
            };

            _appDbContext.Messages.Add(new Model.Message
            {
                Id = membershipStartedEvent.Id,
                Type = membershipStartedEvent.GetType().ToString(),
                Content = JsonConvert.SerializeObject(membershipStartedEvent),
                CreatedOn = DateTime.UtcNow
            });

            await _appDbContext.SaveChangesAsync();

            await _busControl.Publish<MembershipStartedEvent>(membershipStartedEvent);

            return membershipResponse;
        }
    }
}
