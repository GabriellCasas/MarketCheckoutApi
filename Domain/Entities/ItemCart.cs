using System.Collections.Generic;

namespace MarketCheckoutApi.Domain.Entities
{
    public class ItemCart
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
