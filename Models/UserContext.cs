using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BaseWebApi.Models
{
    public class UserContext : DbContext
    {

        public UserContext (DbContextOptions<UserContext> options) : base(options)
        {
        }

       public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(u => u.Email).IsUnique();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Email).IsRequired();
                entity.Property(e => e.Hash);
            });
        }
    }

}