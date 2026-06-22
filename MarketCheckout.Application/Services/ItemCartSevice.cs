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
    public class ItemCartService : IItemCartService
    {
        private readonly IBaseRepository<ItemCart> _repository;
        private readonly IBaseRepository<Cart> _cartRepository;

        public ItemCartService(IBaseRepository<ItemCart> repository, IBaseRepository<Cart> cartRepository)
        {
            _repository = repository;
            _cartRepository = cartRepository;
        }
        public async Task AddItemCartAsync(int id, List<ItemCart> itens, CancellationToken cancellationToken)
        {
            var cart = await _cartRepository.GetByIdAsync(id, cancellationToken);

            if(cart == null)
            {
                throw new OperationCanceledException("Cart not found.");
            }
            try
            {
                foreach (var item in itens)
                {
                    await _repository.AddAsync(item, cancellationToken);
                }

                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
