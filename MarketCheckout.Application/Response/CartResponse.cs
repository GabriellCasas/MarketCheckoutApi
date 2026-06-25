using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MarketCheckout.Application.Response
{
    public class CartResponse
    {
        public int Id { get; set; }
        public string BuyerCpf { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalValue { get; set; }
        public List<ItemCartResponse> Items { get; set; } = new List<ItemCartResponse>();

        public static CartResponse ToCartResponse(Cart cart)
        {
            return new CartResponse
            {
                Id = cart.Id,
                BuyerCpf = cart.BuyerCpf,
                CreatedBy = cart.CreatedBy,
                CreatedAt = cart.CreatedAt,
                TotalValue = cart.Items.Sum(i => i.Product != null ? i.Product.Price * i.Quantity : 0),
                Items = cart.Items.Select(ic => new ItemCartResponse
                {
                    Id = ic.Id,
                    Quantity = ic.Quantity,
                    Product = ic.Product != null ? new ProductResponse
                    {
                        id = ic.Product.Id,
                        title = ic.Product.Name,
                        price = ic.Product.Price
                    } : null
                }).ToList()
            };
        }
    }
}
