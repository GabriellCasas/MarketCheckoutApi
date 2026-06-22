using System;
using System.Collections.Generic;
using System.Text;

namespace MarketCheckout.Application.Response
{
    public class ProductResponse
    {
        public int id { get; set; }
        public string? title { get; set; }
        public decimal price { get; set; }
    }
}
