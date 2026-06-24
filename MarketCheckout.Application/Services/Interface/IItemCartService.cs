using MarketCheckout.Application.Request;
using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services.Interface
{
    public interface IItemCartService
    {
        Task AddItemCartAsync(int id, List<ItemCartRequest> itens, CancellationToken cancellationToken);
    }
}