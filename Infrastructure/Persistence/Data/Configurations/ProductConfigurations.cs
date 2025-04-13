using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
    internal class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            //    builder.Property(p => p.Id)
            //.ValueGeneratedOnAdd();

            builder.HasOne(p => p.ProductBrand)
                .WithMany()
                .HasForeignKey(p => p.BrandId);
            //.OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(p => p.TypeId);
                //.OnDelete(DeleteBehavior.Cascade);


            builder.Property( P => P.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }

    
    }
 
}
