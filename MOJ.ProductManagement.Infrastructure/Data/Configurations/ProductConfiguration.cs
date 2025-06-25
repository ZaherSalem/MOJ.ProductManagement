using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Infrastructure.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(p => p.QuantityPerUnit)
                   .HasConversion<string>()
                   .IsRequired();

            builder.Property(p => p.UnitPrice)
                   .HasPrecision(18, 2)
                   .IsRequired();

            builder.Property(p => p.UnitsInStock)
                   .IsRequired();

            builder.Property(p => p.UnitsOnOrder)
                   .IsRequired();

            builder.Property(p => p.ReorderLevel)
                   .IsRequired();

            builder.HasOne(p => p.Supplier)
                   .WithMany(s => s.Products)
                   .HasForeignKey(p => p.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => p.Name)
                   .IsUnique();

        }
    }

}