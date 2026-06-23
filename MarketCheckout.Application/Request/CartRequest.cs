using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MarketCheckout.Application.Request
{
    public class CartRequest
    {
        public string BuyerName { get; set; }
        [StringLength(14)]
        [MinLength(14)]
        public string BuyerCpf { get; set; }
        public string CreatedBy { get; set; }
        public List<ItemCart> Items { get; set; } = new List<ItemCart>();
    }
}
