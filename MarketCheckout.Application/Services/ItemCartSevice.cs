using MarketCheckout.Application.Request;
using MarketCheckout.Application.Services.Interface;
using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services
{
    public class ItemCartService : IItemCartService
    {
        private readonly IBaseRepository<ItemCart> _repository;
        private readonly IBaseRepository<Cart> _cartRepository;
        private readonly IProductService _productService;

        public ItemCartService(IBaseRepository<ItemCart> repository, IBaseRepository<Cart> cartRepository, IProductService productService)
        {
            _repository = repository;
            _cartRepository = cartRepository;
            _productService = productService;
        }
        public async Task AddItemCartAsync(int id, List<ItemCartRequest> itens, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(id, cancellationToken);

            if (cart == null)
            {
                throw new OperationCanceledException("Cart not found.");
            }
            try
            {
                var itensCart = itens.Select(i => ItemCart.Create(id, i.ProductId, i.Quantity)).ToList();

                foreach (var item in itensCart)
                {
                    var product = await _productService.ProductConsist(item.ProductId, cancellationToken);
                    if (product == null)
                    {
                        throw new ArgumentException("Invalid product in cart.", nameof(Cart));
                    }

                    await _repository.AddAsync(item, cancellationToken);
                }

                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Error adding item to cart.");
            }
        }
    }
}
