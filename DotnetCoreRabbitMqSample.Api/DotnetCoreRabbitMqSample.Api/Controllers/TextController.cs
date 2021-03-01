using DotnetCoreRabbitMqSample.Api.Contracts.Requests;
using DotnetCoreRabbitMqSample.Api.Services;
using Microsoft.AspNetCore.Mvc;


namespace DotnetCoreRabbitMqSample.Api.Controllers
{
    [Route("text")]
    [ApiController]
    public class TextController : ControllerBase
    {
        ITextService _textService;

        public TextController(ITextService textService)
        {
            _textService = textService;
        }

        [HttpPut("")]
        public ActionResult Put([FromBody] PutTextRequest putTextRequest)
        {
            var result = _textService.PutText(putTextRequest);

            return Ok(result);
        }
    }
}
