using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurement.Model.DTO
{
    public class RefreshTokenRequestDto
    {
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}
