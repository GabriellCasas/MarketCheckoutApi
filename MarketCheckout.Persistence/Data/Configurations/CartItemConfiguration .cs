using MarketCheckoutApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketCheckout.Infrastructure.Data.Configurations
{
    public class CartItemConfiguration : IEntityTypeConfiguration<ItemCart>
    {
        public void Configure(EntityTypeBuilder<ItemCart> builder)
        {
            builder.ToTable("ItemCarrinho");

            builder.HasKey(ic => ic.Id);

            builder.Property(ic => ic.Id)
                .HasColumnName("Id");

            builder.Property(ic => ic.CartId)
                .IsRequired()
                .HasColumnName("CarrinhoId");

            builder.Property(ic => ic.ProductId)
                .IsRequired()
                .HasColumnName("ProdutoId");

            builder.Property(ic => ic.Quantity)
                .IsRequired()
                .HasColumnName("Quantidade");

            builder.HasOne<Cart>()
                .WithMany(c => c.Items)
                .HasForeignKey(ic => ic.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ic => ic.Product)
                .WithMany(p => p.ItemCarts)
                .HasForeignKey(ic => ic.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}