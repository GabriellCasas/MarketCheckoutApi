using MarketCheckoutApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCheckout.Application.Request
{
    public class ItemCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
