using Microsoft.EntityFrameworkCore;
using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<QuantityMeasurementEntity> QuantityMeasurements { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasIndex(e => e.Email).IsUnique();
                
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(256);
                
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(500);
                
                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(500);
                
                entity.Property(e => e.Provider)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("Local");
                
                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("User");
                
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.Property(e => e.IsActive)
                    .HasDefaultValue(true);
                
                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(500);
                
                entity.Property(e => e.RefreshTokenExpiry);
            });

            // Configure History entity
            modelBuilder.Entity<History>(entity =>
            {
                entity.HasKey(e => e.Id);
                
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.CreatedAt);
                entity.HasIndex(e => e.IsDeleted);
                
                entity.Property(e => e.UserId)
                    .IsRequired();
                
                entity.Property(e => e.InputValue)
                    .IsRequired()
                    .HasMaxLength(500);
                
                entity.Property(e => e.OutputValue)
                    .IsRequired()
                    .HasMaxLength(500);
                
                entity.Property(e => e.OperationType)
                    .IsRequired()
                    .HasMaxLength(100);
                
                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                entity.Property(e => e.IsDeleted)
                    .HasDefaultValue(false);
                
                entity.Property(e => e.DeletedAt);
                
                // Configure relationship with User
                entity.HasOne(e => e.User)
                    .WithMany(u => u.Histories)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}