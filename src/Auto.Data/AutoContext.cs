using Auto.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auto.Data
{
    public class AutoDbContext : DbContext
    {
        public AutoDbContext(DbContextOptions<AutoDbContext> options)
            : base(options)
        {
        }

        public DbSet<AutoEntity> Autos { get; set; }
        public DbSet<OfferEntity> Offers { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the foreign key relationship
            modelBuilder.Entity<AutoEntity>()
                .ToTable("Autos")
                .HasKey(a => a.AutoId);

            modelBuilder.Entity<AutoEntity>()
                .HasOne(a => a.User)
                .WithMany(u => u.Autos)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserEntity>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<OfferEntity>()
                .ToTable("Offers")
                .HasKey(o => o.OfferId);
        }
    }
}
