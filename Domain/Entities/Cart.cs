using System;
using System.Collections.Generic;

namespace MarketCheckoutApi.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCpf { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt{ get; set; }
        public List<ItemCart> Items { get; set; } = new List<ItemCart>();
    }
}