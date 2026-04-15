using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurement.Model.Entities
{
    public class History
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(500)]
        public string InputValue { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string OutputValue { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string OperationType { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

        // Navigation property
        public User User { get; set; } = null!;
    }
}
