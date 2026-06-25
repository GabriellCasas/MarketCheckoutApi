using MarketCheckout.Application.Request;
using MarketCheckout.Application.Response;
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
        private readonly ICartRepository _repository;
        private readonly IProductService _productService;
        private readonly IItemCartService _itemCartService;

        public CartService(ICartRepository repository, IProductService productService, IItemCartService itemCartService)
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

        public async Task<CartResponse> GetTotalValueAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await this.GetCartByIdAsync(id, cancellationToken);

                if (cart == null)
                {
                    throw new ArgumentException("Cart not found.", nameof(Cart));
                }

                var cartResponse = CartResponse.ToCartResponse(cart);

                return cartResponse;
            }
            catch
            {
                throw new ArgumentException("Error getting total value.", nameof(Cart));
            }
        }

        public async Task<Cart> GetCartByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var cart = await _repository.GetByIdWithItens(id, cancellationToken);

                if (cart == null)
                {
                    throw new ArgumentException("Cart not found.", nameof(Cart));
                }
                return cart;
            }
            catch (Exception)
            {
                throw new ArgumentException("Error getting cart.", nameof(Cart));
            }
        }
    }
}
