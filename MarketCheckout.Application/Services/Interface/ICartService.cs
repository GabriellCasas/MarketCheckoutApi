using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services.Interface
{
    public interface ICartService
    {
        Task AddCartAsync(Cart cart, CancellationToken cancellationToken);
    }
}
