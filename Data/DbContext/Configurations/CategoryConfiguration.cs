using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using MemeSite.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MemeSite.Data.DbContext.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.CategoryId);
            builder.Property(t => t.CategoryId)
                .UseIdentityColumn(1, 1);

            builder.HasAlternateKey(t => t.CategoryName);
            builder.Property(t => t.CategoryName)
                .HasMaxLength(14)
                .IsRequired();

            builder.HasMany(t => t.Memes)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);
        }


    }
}
