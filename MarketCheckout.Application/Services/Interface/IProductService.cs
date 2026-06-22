using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services.Interface
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken);
        Task AddProductAsync(Product product, CancellationToken cancellationToken);
        Task UpdateProductAsync(Product product, CancellationToken cancellationToken);
        Task DeleteProductAsync(int id, CancellationToken cancellationToken);
        Task<Product?> ProductConsist(int id, CancellationToken cancellationToken);
    }
}