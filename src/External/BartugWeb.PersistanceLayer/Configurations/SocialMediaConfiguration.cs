using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class SocialMediaConfiguration : IEntityTypeConfiguration<SocialMedia>
{
    public void Configure(EntityTypeBuilder<SocialMedia> builder)
    {
        builder.ToTable("SocialMedia");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.IconUrl)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(s => s.LinkUrl)
            .HasMaxLength(2000)
            .IsRequired();
    }
}
