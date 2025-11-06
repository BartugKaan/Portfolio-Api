using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class GetInTouchConfiguration : IEntityTypeConfiguration<GetInTouch>
{
    public void Configure(EntityTypeBuilder<GetInTouch> builder)
    {
        builder.ToTable("GetInTouch");
        builder.HasKey(g => g.Id);
        builder.Property(g => g.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(g => g.Description)
            .HasMaxLength(1000)
            .IsRequired();
        builder.Property(g => g.ContactName)
            .HasMaxLength(100)
            .IsRequired();
        builder.Property(g => g.ContactEmail)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(g => g.ContactMessage)
            .HasMaxLength(2000)
            .IsRequired();
    }
}

