using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using MemeSite.Data.DbContext.Mapping;
using MemeSite.Domain;

namespace MemeSite.Data.DbContext
{
    public class ApplicationDbContext : IdentityDbContext<PageUser, PageRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Meme> Memes { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Favourite> Favourites { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new CommentMap());
            modelBuilder.ApplyConfiguration(new FavouriteMap());
            modelBuilder.ApplyConfiguration(new MemeMap());
            modelBuilder.ApplyConfiguration(new VoteMap());
        }
    }
}
