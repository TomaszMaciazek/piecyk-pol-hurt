using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Dto;

namespace PiecykPolHurt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendPointController : ControllerBase
    {
        private readonly ISendPointService _sendPointService;

        public SendPointController(ISendPointService sendPointService)
        {
            _sendPointService = sendPointService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SimpleSendPointDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SimpleSendPointDto>>> GetAllSendPoints()
        {
            try
            {
                return Ok(await _sendPointService.GetAllSendPoints());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
