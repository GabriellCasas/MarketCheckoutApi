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
    public class ProductService : IProductService
    {
        private readonly IBaseRepository<Product> _repository;
        private readonly IApiService _apiService;

        public ProductService(IBaseRepository<Product> repository, IApiService apiService)
        {
            _repository = repository;
            _apiService = apiService;
        }

        public async Task<Product?> ProductConsist(int id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await this.GetProductByIdAsync(id, cancellationToken);
                if (product == null)
                {
                    var productResponse = await _apiService.GetProductAsync(id);
                    if (productResponse != null)
                    {
                        product = Product.Create(productResponse.id, productResponse.title, productResponse.price);
                        await this.AddProductAsync(product, cancellationToken);
                    }
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error searching for product.", nameof(Product));
            }
        }
        public async Task<Product?> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                return await _repository.GetByIdAsync(id, cancellationToken);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error searching for product.", nameof(Product));
            }            
        }

        public async Task AddProductAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                await _repository.AddAsync(product, cancellationToken);
                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error adding product.", nameof(Product));
            }            
        }

        public async Task UpdateProductAsync(Product product, CancellationToken cancellationToken)
        {
            try
            {
                _repository.Update(product);
                await _repository.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw new ArgumentException("Error updating product.", nameof(Product));
            }
        }

        public async Task DeleteProductAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _repository.GetByIdAsync(id, cancellationToken);
                if (product != null)
                {
                    _repository.Delete(product);
                    await _repository.SaveChangesAsync(cancellationToken);
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error deleting product.", nameof(Product));
            }
        }
    }
}