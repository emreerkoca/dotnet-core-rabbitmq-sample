using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Post([FromBody] PostMembershipRequest popstMembershipRequest)
        {
            var result = await _membershipService.PostMembership(popstMembershipRequest);

            return Ok(result);
        }
    }
}