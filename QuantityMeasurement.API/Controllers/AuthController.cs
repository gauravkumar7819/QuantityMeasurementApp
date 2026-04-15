using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.API.Services;
using QuantityMeasurement.Model.DTO;

namespace QuantityMeasurement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Registration attempt with null request body");
                return BadRequest(new { error = "Request body is required." });
            }

            if (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                _logger.LogWarning("Registration attempt with missing required fields");
                return BadRequest(new { error = "Name, Email, and Password are required." });
            }

            try
            {
                var result = await _authService.RegisterAsync(dto);
                _logger.LogInformation("User registered successfully: {Email}", dto.Email);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Registration failed for email: {Email}", dto.Email);
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registration error for email: {Email}", dto.Email);
                return StatusCode(500, new { error = "Registration failed.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (dto == null)
            {
                _logger.LogWarning("Login attempt with null request body");
                return BadRequest(new { error = "Request body is required." });
            }

            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            {
                _logger.LogWarning("Login attempt with missing required fields");
                return BadRequest(new { error = "Email and Password are required." });
            }

            try
            {
                var result = await _authService.LoginAsync(dto);

                if (result == null)
                {
                    _logger.LogWarning("Login failed for email: {Email}", dto.Email);
                    return Unauthorized(new { error = "Invalid credentials" });
                }

                _logger.LogInformation("User logged in successfully: {Email}", dto.Email);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt for email: {Email}", dto.Email);
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Login error for email: {Email}", dto.Email);
                return StatusCode(500, new { error = "Login failed.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Token))
            {
                _logger.LogWarning("Google login attempt with invalid request");
                return BadRequest(new { error = "Google ID token is required." });
            }

            try
            {
                _logger.LogInformation("Google login attempt initiated");
                var result = await _authService.GoogleLoginAsync(dto);
                _logger.LogInformation("Google login successful");
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Google authentication failed: Invalid token");
                return Unauthorized(new { error = "Invalid Google token", details = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Google login error");
                return StatusCode(500, new { error = "Google authentication failed.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.RefreshToken))
            {
                _logger.LogWarning("Refresh token attempt with invalid request");
                return BadRequest(new { error = "Refresh token is required." });
            }

            try
            {
                _logger.LogInformation("Refresh token attempt");
                var result = await _authService.RefreshTokenAsync(dto.RefreshToken);

                if (result == null)
                {
                    _logger.LogWarning("Refresh token failed: Invalid or expired token");
                    return Unauthorized(new { error = "Invalid or expired refresh token" });
                }

                _logger.LogInformation("Refresh token successful");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Refresh token error");
                return StatusCode(500, new { error = "Token refresh failed.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
    }
}