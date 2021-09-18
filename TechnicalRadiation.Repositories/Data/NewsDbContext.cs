using Microsoft.EntityFrameworkCore;
using TechnicalRadiation.Models.Entities;

namespace TechnicalRadiation.Repositories.Data
{

    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options)
            : base(options)
        {
        }
        public DbContextOptions<DbContext> Context { get; set; }
        public DbSet<NewsItem> NewsItems { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.NewsItems)
                .WithMany(n => n.Authors);
            modelBuilder.Entity<NewsItem>()
                .HasMany(n => n.Categories)
                .WithMany(c => c.NewsItems);
        }
    }
}