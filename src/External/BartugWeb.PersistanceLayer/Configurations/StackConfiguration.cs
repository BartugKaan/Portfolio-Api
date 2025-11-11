using BartugWeb.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BartugWeb.PersistanceLayer.Configurations;

public class StackConfiguration : IEntityTypeConfiguration<Stack>
{
    public void Configure(EntityTypeBuilder<Stack> builder)
    {
        builder.ToTable("Stacks");

        builder.HasKey(s => s.Id);

        // Category
        builder.Property(s => s.Category)
            .HasMaxLength(100)
            .IsRequired();

        // Technology
        builder.Property(s => s.Technology)
            .HasMaxLength(200)
            .IsRequired();

        // Order
        builder.Property(s => s.Order)
            .IsRequired()
            .HasDefaultValue(0);

        // Relationship: One About -> Many Stacks
        builder.HasOne(s => s.About)
            .WithMany(a => a.Stacks)
            .HasForeignKey(s => s.AboutId)
            .OnDelete(DeleteBehavior.Cascade);

        // Index for better query performance
        builder.HasIndex(s => new { s.AboutId, s.Category, s.Order })
            .HasDatabaseName("IX_Stacks_AboutId_Category_Order");
    }
}
