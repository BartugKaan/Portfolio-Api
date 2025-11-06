using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BartugWeb.PersistanceLayer.Configurations;

public class AboutConfiguration : IEntityTypeConfiguration<About>
{
    public void Configure(EntityTypeBuilder<About> builder)
    {
        builder.ToTable("Abouts");
        builder.HasKey(a => a.Id);
        builder.Property(builder => builder.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(builder => builder.ImageUrl)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(builder =>builder.Stacks)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(builder =>builder.Educations)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(builder =>builder.Experience)
            .HasMaxLength(500)
            .IsRequired();
    }
}