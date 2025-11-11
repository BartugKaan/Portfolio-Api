using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences");
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Company)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Position)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.Location)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired(false);

        builder.HasOne(x => x.About)
            .WithMany(a => a.Experiences)
            .HasForeignKey(x => x.AboutId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}