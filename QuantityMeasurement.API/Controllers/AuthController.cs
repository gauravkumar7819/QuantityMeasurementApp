using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.API.Services;
using QuantityMeasurement.Model.DTO;

namespace QuantityMeasurement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;

        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { error = "Name, Email, and Password are required." });

            try
            {
                var result = await _service.Register(dto);
                return Ok(new { message = result });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Registration failed.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null)
                return BadRequest(new { error = "Request body is required." });

            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest(new { error = "Email and Password are required." });

            try
            {
                var token = await _service.Login(dto);

                if (token == null)
                    return Unauthorized(new { error = "Invalid credentials" });

                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Login failed.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}