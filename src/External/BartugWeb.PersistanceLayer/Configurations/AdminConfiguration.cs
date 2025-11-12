using System;
using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
  public void Configure(EntityTypeBuilder<Admin> builder)
  {
    builder.ToTable("Admins");
    builder.HasKey(a => a.Id);

    builder.Property(x => x.Username)
            .IsRequired()
            .HasMaxLength(50);

    builder.HasIndex(x => x.Username).IsUnique();

    builder.Property(x => x.Email)
        .IsRequired()
        .HasMaxLength(100);

    builder.HasIndex(x => x.Email).IsUnique();

    builder.Property(x => x.PasswordHash)
        .IsRequired();

    builder.Property(x => x.LastLoginAt)
        .IsRequired(false);
  }
}
