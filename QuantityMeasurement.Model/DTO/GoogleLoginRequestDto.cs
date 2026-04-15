using System.ComponentModel.DataAnnotations;

namespace QuantityMeasurement.Model.DTO
{
    public class GoogleLoginRequestDto
    {
        [Required(ErrorMessage = "Google ID token is required")]
        public string Token { get; set; } = string.Empty;
    }
}
