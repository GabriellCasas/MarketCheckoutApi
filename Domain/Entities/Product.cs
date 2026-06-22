using System;
using System.ComponentModel.DataAnnotations;

namespace MarketCheckoutApi.Domain.Entities
{
    public class Product
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }

        public static Product Create(int id, string name, decimal price)
        {
            Product product = new Product();
            product.Validate(id);

            return new Product
            {
                Id = id,
                Name = name,
                Price = price                                                                                                   
            };
        }

        private void Validate(int id)
        {
            if(id <= 0)
            {
                throw new ArgumentException("Id inválido");
            }
        }
    }
}
