﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Identity");

            builder.Property(p => p.UserName)
                .HasColumnType("VARCHAR(25)")
                .IsRequired();

            builder.Property(p => p.Email)
                .HasColumnType("VARCHAR(350)")
                .IsRequired();

            builder.Property(p => p.PhoneNumber)
                .HasColumnType("VARCHAR(25)");

            builder.Property(p => p.FirstName)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.LastName)
                .HasColumnType("VARCHAR(250)")
                .IsRequired();

            builder.Property(p => p.Created)
                .HasColumnType("DATETIMEOFFSET")
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

        }
    }
}
