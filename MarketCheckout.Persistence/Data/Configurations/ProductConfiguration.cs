using MarketCheckoutApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketCheckout.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Produto");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id");

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("Nome");

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("numeric(18,2)")
                .HasColumnName("Valor");
        }
    }
}