using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Infrastructure.Data.Configurations
{
    public class LookupConfiguration : IEntityTypeConfiguration<Lookup>
    {
        public void Configure(EntityTypeBuilder<Lookup> builder)
        {
            builder.ToTable("Lookups");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(p => p.Parent)
                .WithMany(l => l.Children)
                .HasForeignKey(p => p.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}

