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
    public class FoodMap : IEntityTypeConfiguration<Food>
    {
        public void Configure(EntityTypeBuilder<Food> builder)
        {
            builder.ToTable("Food");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Name).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Unit).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.Image).HasMaxLength(500).IsRequired(false);
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.UrlSlug).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.ShowOnPage).HasColumnType("bit").HasDefaultValue(false);







        }
    }
}
