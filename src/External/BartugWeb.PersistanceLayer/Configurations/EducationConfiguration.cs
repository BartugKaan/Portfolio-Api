using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Educations");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.SchoolName)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(x => x.StartYear)
            .IsRequired();
        
        builder.Property(e => e.EndYear)
            .IsRequired(false);

        builder.HasOne(x => x.About)
            .WithMany(x => x.Educations)
            .HasForeignKey(x => x.AboutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}