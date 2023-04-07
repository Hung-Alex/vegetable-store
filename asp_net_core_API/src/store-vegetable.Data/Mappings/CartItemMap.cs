using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Data.Mappings
{
    public class CartItemMap : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.ToTable("CartItems");
            builder.HasKey(x => new { x.FoodId, x.CartId });
            builder.Property(x => x.Quantity).IsRequired();

            builder.HasOne(x => x.Cart)
               .WithMany(s => s.CartItems)
               .HasForeignKey(x => x.CartId);

            builder.HasOne(x => x.Food)
                .WithMany(s => s.CartItems)
                .HasForeignKey(x => x.FoodId);

        }
    }
}
