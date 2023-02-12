using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PiecykPolHurt.API.Authorization;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto.Order;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IUser _user;

        public OrderController(IOrderService orderService, IUser user)
        {
            _orderService = orderService;
            _user = user;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<OrderListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedList<OrderListItemDto>>> GetOrders([FromQuery] OrderQuery query)
        {
            try
            {
                return Ok(await _orderService.GetOrders(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OrderDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDto>> GetOrder([FromRoute] int id)
        {
            try
            {
                var order = await _orderService.GetOrderById(id);
                if (order == null)
                {
                    return NotFound();
                }
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Authorize(Policy = Policy.Customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> CreateOrder([FromBody] CreateOrderCommand command)
        {
            try
            {
                var result = await _orderService.CreateOrderAsync(command, _user.Id ?? throw new Exception("empty user id"), _user.Email);
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

        [HttpPut("approve")]
        [Authorize(Policy = Policy.Seller)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> ApproveOrde([FromBody] ApproveOrderCommand command)
        {
            try
            {
                var result = await _orderService.ApproveOrderAsync(command, _user.Email);
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

        [HttpPatch("reject/{id}")]
        [Authorize(Policy = Policy.Seller)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> RejectOrder([FromRoute] int id)
        {
            try
            {
                var result = await _orderService.RejectOrderAsync(id, _user.Email);
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

        [HttpPatch("finish/{id}")]
        [Authorize(Policy = Policy.Seller)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> FinishOrder([FromRoute] int id)
        {
            try
            {
                var result = await _orderService.FinishOrderAsync(id, _user.Email);
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

        [HttpPatch("cancel/{id}")]
        [Authorize(Policy = Policy.Customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> CancelOrder([FromRoute] int id)
        {
            try
            {
                var result = await _orderService.CancelOrderAsync(id, _user.Email);
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
