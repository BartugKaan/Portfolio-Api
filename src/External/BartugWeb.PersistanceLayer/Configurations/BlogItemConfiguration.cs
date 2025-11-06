using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class BlogItemConfiguration : IEntityTypeConfiguration<BlogItem>
{
    public void Configure(EntityTypeBuilder<BlogItem> builder)
    {
        builder.ToTable("BlogItems");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.CoverImgUrl)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(b => b.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.Description)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(b => b.BlogCategory)
            .IsRequired();
    }
}