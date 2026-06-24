using System;
using System.Collections.Generic;

namespace MarketCheckoutApi.Domain.Entities
{
    public class ItemCart
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public static ItemCart Create(int cartId, int productId, int quantity)
        {
            ItemCart itemCart = new ItemCart();

            itemCart.Validate(productId, quantity);

            return new ItemCart
            {
                CartId = cartId,
                ProductId = productId,
                Quantity = quantity
            };
        }

        public void Validate(int productId, int quantity)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Invalid product ID.");
            }
            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }
        }
    }
}
 