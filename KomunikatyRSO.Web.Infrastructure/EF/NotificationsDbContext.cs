using KomunikatyRSO.Web.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace KomunikatyRSO.Web.Infrastructure.EF
{
    public class NotificationsDbContext : DbContext
    {
        public NotificationsDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var itemBuilder = modelBuilder.Entity<User>();
            itemBuilder.HasKey(u => u.Id);
            itemBuilder.HasIndex(u => u.UserId);
        }
    }
}
