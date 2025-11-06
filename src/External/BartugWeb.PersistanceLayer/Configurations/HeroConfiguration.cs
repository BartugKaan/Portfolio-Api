using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class HeroConfiguration : IEntityTypeConfiguration<Hero>
{
    public void Configure(EntityTypeBuilder<Hero> builder)
    {
        builder.ToTable("Heroes");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.HeroImageUrl)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(h => h.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(h => h.Name)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(h => h.JobTitles)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(h => h.Description)
            .HasMaxLength(1000)
            .IsRequired();
    }
}

