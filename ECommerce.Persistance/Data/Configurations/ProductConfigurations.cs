using ECommerce.Domain.Entities.ProductModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Persistance.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(X => X.Name).HasMaxLength(100);
            
            builder.Property(X => X.Description).HasMaxLength(500);

            builder.Property(X => X.PictureURL).HasMaxLength(200);

            builder.Property(X => X.Price).HasPrecision(18,2);

            builder.HasOne(X => X.ProductBrand).WithMany()
                .HasForeignKey(X => X.ProductBrandId);

            builder.HasOne(X => X.ProductType).WithMany()
                .HasForeignKey(X => X.ProductTypeId);
        }
    }
}
