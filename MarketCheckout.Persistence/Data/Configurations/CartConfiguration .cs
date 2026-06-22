using MarketCheckoutApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketCheckout.Infrastructure.Data.Configurations
{
    public class CartConfiguration : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder.ToTable("Carrinho");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.BuyerName)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("NomeComprador");

            builder.Property(c => c.BuyerCpf)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnName("CpfComprador");

            builder.Property(c => c.CreatedBy)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("CriadoPor");

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnName("DataCriacao");
        }
    }
}