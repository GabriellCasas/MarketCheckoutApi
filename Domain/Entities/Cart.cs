using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MarketCheckoutApi.Domain.Entities
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        public string BuyerName { get; set; }
        [StringLength(14)]
        [MinLength(14)]
        [RegularExpression(@"^\d{14}$", ErrorMessage = "CPF must contain exactly 14 numeric digits.")]
        public string BuyerCpf { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt{ get; set; }
        public virtual List<ItemCart> Items { get; set; } = new List<ItemCart>();

        public static Cart Create(string buyerName, string buyerCpf, string createdBy, DateTime createdAt)
        {
            Cart cart = new Cart();
            cart.Validate(buyerName, buyerCpf);
            return new Cart
            {
                BuyerName = buyerName,
                BuyerCpf = buyerCpf,
                CreatedBy = createdBy,
                CreatedAt = createdAt,
                Items = new List<ItemCart>()
            };
        }

        private void Validate( string buyerName, string buyerCpf)
        {
            if (!string.IsNullOrWhiteSpace(buyerCpf) && string.IsNullOrWhiteSpace(buyerName))
            {
                throw new ArgumentException("The buyer's name must be filled in.");
            }
        }
    }
}