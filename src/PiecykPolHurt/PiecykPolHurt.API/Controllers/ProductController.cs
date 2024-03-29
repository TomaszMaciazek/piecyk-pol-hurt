﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PiecykPolHurt.API.Authorization;
using PiecykPolHurt.ApplicationLogic.Result;
using PiecykPolHurt.ApplicationLogic.Services;
using PiecykPolHurt.Model.Commands;
using PiecykPolHurt.Model.Dto;
using PiecykPolHurt.Model.Dto.Product;
using PiecykPolHurt.Model.Queries;

namespace PiecykPolHurt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductSendPointService _productSendPointService;
        private readonly IUser _user;

        public ProductController(IProductService productService, IUser user, IProductSendPointService productSendPointService)
        {
            _productService = productService;

            _user = user;
            _productSendPointService = productSendPointService;
        }

        [HttpGet]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginatedList<ProductListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaginatedList<ProductListItemDto>>> GetProducts([FromQuery] ProductQuery query)
        {
            try
            {
                return Ok(await _productService.GetProductsAsync(query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SimpleProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SimpleSendPointDto>>> GetAllProducts()
        {
            try
            {
                return Ok(await _productService.GetAllProductsAsync(false));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("sendPoint/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<ProductSendPointListItemDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<ProductSendPointListItemDto>>> GetTodayProductsFromSendPoint([FromRoute] int id)
        {
            try
            {
                return Ok(await _productSendPointService.GetTodaysProductsFromSendPoint(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("active")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<SimpleProductDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IList<SimpleSendPointDto>>> GetAllActiveProducts()
        {
            try
            {
                return Ok(await _productService.GetAllProductsAsync(true));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct([FromRoute] int id)
        {
            try
            {
                var sendPoint = await _productService.GetProductByIdAsync(id);
                if (sendPoint == null)
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

        [HttpPost]
        [Authorize(Policy = Policy.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(bool))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<bool>> AddProduct([FromBody] CreateProductCommand command)
        {
            try
            {
                var result = await _productService.CreateProductAsync(command, _user.Email);
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
        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommand command)
        {
            try
            {
                var result = await _productService.UpdateProductAsync(command, _user.Email);
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
        public async Task<ActionResult<bool>> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
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
