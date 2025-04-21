using HurghadaStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HurghadaStore.Repository.Data.Config
{
    internal class CarConfigurations : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.Property(C => C.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(C => C.Description)
                .IsRequired();

            builder.Property(C => C.PictureUrl)
                .IsRequired();

            builder.Property(C => C.Price)
                .HasColumnType("decimal(18, 2)");


            builder.HasOne(C => C.Brand) // Navigational Property
                .WithMany()
                .HasForeignKey(C => C.BrandId);

            builder.HasOne(C => C.Category)
                .WithMany()
                .HasForeignKey(C => C.CategoryId)
                /*.OnDelete(DeleteBehavior.SetNull)*/;
        }
    }
}
