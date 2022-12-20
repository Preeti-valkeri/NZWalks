
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User_Role>().
                HasOne(x => x.role)
                .WithMany(y => y.userRole)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User_Role>().
                HasOne(x => x.user)
                .WithMany(y => y.userRole)
                .HasForeignKey(x => x.UserId);
        }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<WalkDifficulty> WalkDifficulty { get; set; }

        public DbSet<User_Role> userRoles { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Role> roles { get; set; }

    }
}
