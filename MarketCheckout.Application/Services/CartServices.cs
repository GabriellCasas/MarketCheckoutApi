using MarketCheckout.Application.Services.Interface;
using MarketCheckout.Domain.Interfaces.Repository;
using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services
{
    public class CartServices : ICartService
    {
        private readonly IBaseRepository<Cart> _repository;

        public CartServices(IBaseRepository<Cart> repository)
        {
            _repository = repository;
        }

        public async Task AddCartAsync(Cart cart, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.AddAsync(cart, cancellationToken);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error adding cart.", nameof(Cart));
            }
        }
    }
}
