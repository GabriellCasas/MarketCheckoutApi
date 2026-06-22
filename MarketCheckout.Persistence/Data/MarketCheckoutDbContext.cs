using MarketCheckoutApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MarketCheckout.Infrastructure.Persistence.Data
{
    public class MarketCheckoutDbContext : DbContext
    {
        public MarketCheckoutDbContext(DbContextOptions<MarketCheckoutDbContext> options) : base(options)
        {
        }
   
        public DbSet<Product> Products { get; set; }
        public DbSet<ItemCart> ItensCart { get; set; }
        public DbSet<Cart> Carts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        
    }
}


