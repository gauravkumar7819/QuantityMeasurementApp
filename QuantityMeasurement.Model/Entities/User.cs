using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurement.Model.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(256)]
        public string Email { get; set; } = string.Empty;

        public string? PasswordHash { get; set; }

        public string? ProfilePicture { get; set; }

        [Required]
        [MaxLength(50)]
        public string Provider { get; set; } = "Local";

        [Required]
        [MaxLength(50)]
        public string Role { get; set; } = "User";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? LastLoginAt { get; set; }

        public bool IsActive { get; set; } = true;

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpiry { get; set; }

        // Navigation property for History
        public ICollection<History> Histories { get; set; } = new List<History>();
    }
}