using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreRabbitMqSample.Api.Controllers
{
    [Route("membership")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        IMembershipService _membershipService;
        public MembershipController(IMembershipService membershipService)
        {
            _membershipService = membershipService;
        }

        [HttpPost("")]
        public ActionResult Post([FromBody] PostMembershipRequest popstMembershipRequest)
        {
            var result = _membershipService.PostMembership(popstMembershipRequest);

            return Ok(result);
        }
    }
}