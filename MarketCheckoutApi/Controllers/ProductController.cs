using MarketCheckout.Application.Services.Interface;
using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckoutApi.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketCheckout.Api.Controllers
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.ProductConsist(id, cancellationToken: default);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}

