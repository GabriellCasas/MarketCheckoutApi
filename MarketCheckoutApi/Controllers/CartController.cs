using MarketCheckout.Application.Request;
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
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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
    }
}
