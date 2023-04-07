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
    public class CategoriesMap : IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(c => c.Id);
            builder.Property(x => x.Name).HasMaxLength(256).IsRequired();
            builder.Property(x=>x.Description).HasMaxLength(256).IsRequired();
            builder.Property(x=>x.UrlSlug).HasMaxLength(256).IsRequired();
            builder.Property(x=>x.Image).HasMaxLength(500).IsRequired(false);
            builder.Property(x=>x.ShowOnMenu).HasColumnType("bit").HasDefaultValue(true);

            builder.HasMany(x=>x.Foods)
                .WithOne(s=>s.Categories)
                .HasForeignKey(x=>x.CategoriesId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
