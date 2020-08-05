using MemeSite.Domain;
using MemeSite.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Data.Mapping
{
    public class VoteMap : IEntityTypeConfiguration<Vote>
    {
        public void Configure(EntityTypeBuilder<Vote> builder)
        {
            builder.HasKey(t => t.VoteId);
            builder.Property(t => t.VoteId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.Value)
                .IsRequired();

            builder.HasOne(t => t.Meme)
                .WithMany(t => t.Votes)
                .HasForeignKey(t => t.MemeRefId)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(t => t.Votes)
                .HasForeignKey(t => t.UserId)
                .IsRequired();
        }

        
    }
}
