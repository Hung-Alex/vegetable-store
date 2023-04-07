using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Data.Mappings
{
    public class OrderItemMap : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.HasKey(x => new { x.OrderId, x.FoodId });
            builder.Property(x=>x.Quantity).IsRequired();
            builder.Property(x=>x.Price).IsRequired();

            builder.HasOne(x => x.Order)
                .WithMany(s => s.Items)
                .HasForeignKey(x=>x.OrderId);

            builder.HasOne(x => x.Food)
                .WithMany(s => s.Items)
                .HasForeignKey(x => x.FoodId);
        }
    }
}
