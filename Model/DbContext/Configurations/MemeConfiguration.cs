using MemeSite.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Data.DbContext.Configurations
{
    public class MemeConfiguration : IEntityTypeConfiguration<Meme>
    {
        public void Configure(EntityTypeBuilder<Meme> builder)
        {
            builder.HasKey(t => t.MemeId);
            builder.Property(t => t.MemeId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.Title)
                .IsRequired();

            builder.Property(t => t.Txt);

            builder.Property(t => t.CreationDate)
                .IsRequired();

            builder.Property(t => t.IsAccepted);

            builder.Property(t => t.IsArchived);

            builder.Property(t => t.AccpetanceDate);

            builder.Property(t => t.Uri);

            builder.Property(t => t.ImageName);

            builder.Property(t => t.ByteHead)
                .IsRequired();

            builder.Property(t => t.ImageByte)
                .IsRequired();

            builder.HasOne(t => t.PageUser)
                .WithMany(t => t.Memes)
                .HasForeignKey(t => t.UserID)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.Category)
                .WithMany(t => t.Memes)
                .HasForeignKey(t => t.CategoryId);

            builder.HasMany(t => t.Comments)
                .WithOne(t => t.Meme)
                .HasForeignKey(t => t.MemeRefId);

            builder.HasMany(t => t.Votes)
                .WithOne(t => t.Meme)
                .HasForeignKey(t => t.MemeRefId);

            builder.HasMany(t => t.Favourites)
                .WithOne(t => t.Meme)
                .HasForeignKey(t => t.MemeRefId);
        }
    }
}
