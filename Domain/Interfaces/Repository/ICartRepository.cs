using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Domain.Interfaces.Repository
{
    public interface ICartRepository : IBaseRepository<Cart>
    {
        Task<Cart?> GetByIdWithItens(int id, CancellationToken cancellationToken);
    }
}
