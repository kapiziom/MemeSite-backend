using MemeSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Data.DbContext.Mapping
{
    public class FavouriteMap : IEntityTypeConfiguration<Favourite>
    {
        public void Configure(EntityTypeBuilder<Favourite> builder)
        {

            builder.HasKey(t => new { t.MemeRefId, t.UserId });

            builder.HasOne(t => t.Meme)
                .WithMany(t => t.Favourites)
                .HasForeignKey(t => t.MemeRefId)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(t => t.Favourites)
                .HasForeignKey(t => t.UserId)
                .IsRequired();
        }
    }
}
