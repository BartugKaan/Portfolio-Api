using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
{
    public void Configure(EntityTypeBuilder<BlogPost> builder)
    {
        builder.ToTable("BlogPosts");
        builder.HasKey(b => b.Id);
        builder.Property(b => b.HeaderImageUrl)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(b => b.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(b => b.BlogContent)
            .IsRequired();
        builder.Property(b => b.Keywords)
            .HasMaxLength(500)
            .IsRequired();
    }
}

