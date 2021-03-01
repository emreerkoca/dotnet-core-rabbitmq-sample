using AutoMapper;
using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Contracts.Responses;

namespace DotnetCoreRabbitMqSample.Api.Services.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PostMembershipRequest, MembershipResponse>();
            CreateMap<TextResponse, PutTextRequest>();
        }
    }
}
