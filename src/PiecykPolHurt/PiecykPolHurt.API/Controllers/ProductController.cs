using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Dto;

namespace PiecykPolHurt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SimpleProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SimpleSendPointDto>>> GetAllSendPoints()
        {
            try
            {
                return Ok(await _productService.GetAllProducts());
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
