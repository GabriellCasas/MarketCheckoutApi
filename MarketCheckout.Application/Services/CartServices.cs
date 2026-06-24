using MarketCheckout.Application.Request;
using MarketCheckout.Application.Services.Interface;
using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices.Swift;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IBaseRepository<Cart> _repository;
        private readonly IProductService _productService;
        private readonly IItemCartService _itemCartService;

        public CartService(IBaseRepository<Cart> repository, IProductService productService, IItemCartService itemCartService)
        {
            _repository = repository;
            _productService = productService;
            _itemCartService = itemCartService;
        }

        public async Task ProcessCart(CartRequest cartRequest, CancellationToken cancellationToken)
        {
            try
            {
                if (cartRequest == null)
                {
                    throw new ArgumentException("Cart cannot be null.", nameof(Cart));
                }

                var emptyItem = cartRequest.Items.Any(x => x.ProductId == 0);

                if (emptyItem)
                {
                    throw new ArgumentException("Cart items cannot be empty.", nameof(Cart));
                }

                var cart = Cart.Create(cartRequest.BuyerName, cartRequest.BuyerCpf, cartRequest.CreatedBy, DateTime.UtcNow);
                await this.AddCartAsync(cart, cancellationToken);
                
                await _itemCartService.AddItemCartAsync(cart.Id, cartRequest.Items, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
            }            
            catch (Exception ex)
            {
                throw new ArgumentException($"Error adding cart. {ex.Message}", nameof(Cart));
            }
        }
        public async Task AddCartAsync(Cart cart, CancellationToken cancellationToken)
        {
            try
            {
                 await _repository.AddAsync(cart, cancellationToken);
                 await _repository.SaveChangesAsync(cancellationToken);             
            }
            catch (Exception)
            {
                throw new ArgumentException("Error adding cart.", nameof(Cart));
            }
        }
    }
}
