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
    public class FeedbackMap : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.ToTable("Feedback");
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Title).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(500).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(256).IsRequired();
            builder.Property(x => x.UrlSlug).HasMaxLength(256).IsRequired();
            builder.Property(x => x.Meta).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ShippingDate).IsRequired().HasColumnType("datetime").HasDefaultValueSql("getdate()");
            builder.Property(x => x.Status).HasColumnType("bit").HasDefaultValue(false);



        }
    }
}
