using MarketCheckout.Application.Response;
using MarketCheckout.Application.Services.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MarketCheckout.Application.Services
{
    public class ApiService : IApiService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ProductResponse> GetProductAsync(int id)
        {
            HttpClient client = _httpClientFactory.CreateClient("ProductDummy");

            try
            {
                ProductResponse? productResponse = await client.GetFromJsonAsync<ProductResponse> ($"{id}");
                return productResponse ?? throw new InvalidOperationException("Product not found");
            }
            catch (Exception)
            {

                throw new Exception("Error occurred while fetching product");
            }
        }
    }
}
