using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.ProjectImgUrl)
            .HasMaxLength(2000)
            .IsRequired();
        builder.Property(p => p.Title)
            .HasMaxLength(200)
            .IsRequired();
        builder.Property(p => p.Description)
            .HasMaxLength(1000)
            .IsRequired();
        builder.Property(p => p.Keyword)
            .HasMaxLength(500)
            .IsRequired();
        builder.Property(p => p.ProjectUrl)
            .HasMaxLength(2000)
            .IsRequired();
    }
}

