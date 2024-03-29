﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using store_vegetable.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store_vegetable.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(30);
            builder.Property(x=>x.Password).IsRequired().HasMaxLength(2732);
            builder.Property(x=>x.Role).IsRequired().HasMaxLength(60).HasDefaultValue("user");

            builder.HasMany(x=>x.Orders)
                .WithOne(s=>s.User)
                .HasForeignKey(x=>x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Cart>(x => x.Cart)
                 .WithOne(s => s.User)
                 .HasForeignKey<Cart>(x => x.UserId);

            builder.HasOne<UserToken>(x => x.UserToken)
                .WithOne(s => s.User)
                .HasForeignKey<UserToken>(x => x.UserId);

        }
    }
}
