using MarketCheckout.Application.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services.Interface
{
    public interface IApiService
    {
        Task<ProductResponse> GetProductAsync(int id);
    }
}
