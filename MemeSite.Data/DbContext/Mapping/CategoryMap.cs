using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MemeSite.Domain;

namespace MemeSite.Data.DbContext.Mapping
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(t => t.CategoryId);
            builder.Property(t => t.CategoryId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.CategoryName)
                .HasMaxLength(14)
                .IsRequired();

            builder.HasMany(t => t.Memes)
                .WithOne(t => t.Category)
                .HasForeignKey(t => t.CategoryId);
        }


    }
}
