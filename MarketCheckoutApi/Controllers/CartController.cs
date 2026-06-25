using MarketCheckout.Application.Request;
using MarketCheckout.Application.Services;
using MarketCheckout.Application.Services.Interface;
using MarketCheckoutApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketCheckout.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IItemCartService _itemCartService;
        public CartController(ICartService cartService, IItemCartService itemCartService)
        {
            _cartService = cartService;
            _itemCartService = itemCartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCart([FromBody] CartRequest cartResponse)
        {
            if (cartResponse == null)
            {
                return BadRequest("Cart cannot be null.");
            }
            try
            {
                await _cartService.ProcessCart(cartResponse, cancellationToken: default);
                return Ok("Cart added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while adding the cart.");
            }
        }

        [HttpPost("{id}/items")]
        public async Task<IActionResult> AddItemCart(int id, List<ItemCartRequest> itens)
        {
            try
            {
                await _itemCartService.AddItemCartAsync(id, itens, cancellationToken: default);
                return Ok();
            }
            catch (OperationCanceledException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTotalValue(int id)
        {
            var totalValue = await _cartService.GetTotalValueAsync(id, cancellationToken: default);
            if (totalValue == null)
            {
                return NotFound("Cart not found.");
            }
            else
            {
                return Ok(totalValue);
            }
        }
    }
}
