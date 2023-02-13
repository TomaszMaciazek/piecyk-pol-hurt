using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PiecykPolHurt.API.Authorization;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SendPointController : ControllerBase
    {
        private readonly ISendPointService _sendPointService;

        public SendPointController(ISendPointService sendPointService)
        {
            _sendPointService = sendPointService;
        }

        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<SendPointListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedList<SendPointListItemDto>>> GetSendPoints([FromQuery] SendPointQuery query)
        {
            try
            {
                return Ok(await _sendPointService.GetSendPointsAsync(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SendPointDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaginatedList<SendPointDto>>> GetSendPoint([FromRoute] int id)
        {
            try
            {
                var sendPoint = await _sendPointService.GetSendPointByIdAsync(id);
                if(sendPoint == null)
                {
                    return NotFound();
                }
                return Ok(sendPoint);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SendPointDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SendPointDto>>> GetAllActiveSendPoints()
        {
            try
            {
                return Ok(await _sendPointService.GetAllSendPointsAsync());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SimpleSendPointDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SimpleSendPointDto>>> GetAllSendPoints()
        {
            try
            {
                return Ok(await _sendPointService.GetAllSendPointsAsync(false));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> AddSendPoint([FromBody] CreateSendPointCommand command)
        {
            try
            {
                var result = await _sendPointService.CreateSendPointAsync(command);
                if (!result)
                {
                    return Conflict();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> UpdateSendPoint([FromBody] UpdateSendPointCommand command)
        {
            try
            {
                var result = await _sendPointService.UpdateSendPointAsync(command);
                if (!result)
                {
                    return Conflict();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> DeleteSendPoint([FromRoute] int id)
        {
            try
            {
                var result = await _sendPointService.DeleteSendPointAsync(id);
                if (!result)
                {
                    return Conflict();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
