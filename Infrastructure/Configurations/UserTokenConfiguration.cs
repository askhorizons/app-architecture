﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UserTokenConfiguration : IEntityTypeConfiguration<UserToken>
    {
        public void Configure(EntityTypeBuilder<UserToken> builder)
        {
            builder.ToTable("UserToken", "Identity");

            builder.HasKey(u => new { u.LoginProvider, u.UserId, u.Name });

            builder.HasOne(ur => ur.User)
                .WithMany(ur => ur.UserTokens)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
