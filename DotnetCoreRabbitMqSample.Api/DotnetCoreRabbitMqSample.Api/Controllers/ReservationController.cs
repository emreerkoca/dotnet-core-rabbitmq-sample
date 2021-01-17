using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DotnetCoreRabbitMqSample.Api.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCoreRabbitMqSample.Api.Controllers
{
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        readonly IPublishEndpoint _publishEndpoint;

        public ReservationController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(1);
        }

        [HttpPost("")]
        public async Task<ActionResult> Post(string value)
        {
            await _publishEndpoint.Publish<SampleEvent>(new SampleEvent
            {
                SampleProperty = "sample"
            });

            return Ok();
        }
    }
}