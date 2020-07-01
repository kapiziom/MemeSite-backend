using MemeSite.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MemeSite.Data.DbContext.Mapping
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(t => t.CommentId);
            builder.Property(t => t.CommentId)
                .UseIdentityColumn(1, 1);

            builder.Property(t => t.Txt)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(t => t.CreationDate)
                .IsRequired();

            builder.Property(t => t.LastTxt);

            builder.Property(t => t.EditDate);

            builder.Property(t => t.IsArchived);

            builder.HasOne(t => t.Meme)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.MemeRefId)
                .IsRequired();

            builder.HasOne(t => t.PageUser)
                .WithMany(t => t.Comments)
                .HasForeignKey(t => t.UserID)
                .IsRequired();

        }
    }
}
