using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantityMeasurement.Repository;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Model.DTO;
using Google.Apis.Auth;

namespace QuantityMeasurement.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginRequestDto dto);
        Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IGoogleAuthService _googleAuthService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthService> _logger;

        public AuthService(
            AppDbContext context,
            IUserRepository userRepository,
            IGoogleAuthService googleAuthService,
            IConfiguration configuration,
            ILogger<AuthService> logger)
        {
            _context = context;
            _userRepository = userRepository;
            _googleAuthService = googleAuthService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            try
            {
                _logger.LogInformation("Attempting to register user: {Email}", dto.Email);

                var emailExists = await _userRepository.EmailExistsAsync(dto.Email);
                if (emailExists)
                {
                    _logger.LogWarning("Registration failed: Email already exists: {Email}", dto.Email);
                    throw new InvalidOperationException("A user with this email already exists.");
                }

                var user = new User
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                    Provider = "Local",
                    Role = "User",
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };

                await _userRepository.CreateAsync(user);

                _logger.LogInformation("User registered successfully: {Email}", dto.Email);

                return await GenerateAuthResponseAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during registration for email: {Email}", dto.Email);
                throw;
            }
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            try
            {
                _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

                var user = await _userRepository.GetByEmailAsync(dto.Email);

                if (user == null)
                {
                    _logger.LogWarning("Login failed: User not found: {Email}", dto.Email);
                    return null;
                }

                if (user.Provider == "Local" && !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                {
                    _logger.LogWarning("Login failed: Invalid password for: {Email}", dto.Email);
                    return null;
                }

                if (!user.IsActive)
                {
                    _logger.LogWarning("Login failed: Account inactive for: {Email}", dto.Email);
                    throw new UnauthorizedAccessException("Account is inactive");
                }

                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                _logger.LogInformation("Login successful for: {Email}", dto.Email);

                return await GenerateAuthResponseAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", dto.Email);
                throw;
            }
        }

        public async Task<AuthResponseDto> GoogleLoginAsync(GoogleLoginRequestDto dto)
        {
            try
            {
                _logger.LogInformation("Google login attempt with token");

                if (string.IsNullOrWhiteSpace(dto.Token))
                {
                    throw new ArgumentException("Google ID token is required");
                }

                var payload = await _googleAuthService.VerifyGoogleTokenAsync(dto.Token);

                if (payload == null || string.IsNullOrWhiteSpace(payload.Email))
                {
                    throw new UnauthorizedAccessException("Invalid Google token payload");
                }

                _logger.LogInformation("Google token verified for email: {Email}", payload.Email);

                var user = await _userRepository.GetByEmailAsync(payload.Email);

                if (user == null)
                {
                    _logger.LogInformation("Creating new user from Google login: {Email}", payload.Email);

                    user = new User
                    {
                        Email = payload.Email,
                        Name = payload.Name ?? payload.Email.Split('@')[0],
                        ProfilePicture = payload.Picture,
                        Provider = "Google",
                        Role = "User",
                        CreatedAt = DateTime.UtcNow,
                        IsActive = true
                    };

                    await _userRepository.CreateAsync(user);
                }
                else
                {
                    _logger.LogInformation("Existing user found for Google login: {Email}", payload.Email);

                    user.Name = payload.Name ?? user.Name;
                    user.ProfilePicture = payload.Picture ?? user.ProfilePicture;
                    user.LastLoginAt = DateTime.UtcNow;

                    await _userRepository.UpdateAsync(user);
                }

                return await GenerateAuthResponseAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Google login");
                throw;
            }
        }

        public async Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            try
            {
                _logger.LogInformation("Refresh token attempt");

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.RefreshTokenExpiry > DateTime.UtcNow);

                if (user == null)
                {
                    _logger.LogWarning("Refresh token failed: Invalid or expired token");
                    return null;
                }

                user.LastLoginAt = DateTime.UtcNow;
                await _userRepository.UpdateAsync(user);

                _logger.LogInformation("Refresh token successful for: {Email}", user.Email);

                return await GenerateAuthResponseAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during refresh token");
                throw;
            }
        }

        private async Task<AuthResponseDto> GenerateAuthResponseAsync(User user)
        {
            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);

            await _userRepository.UpdateAsync(user);

            return new AuthResponseDto
            {
                JwtToken = jwtToken,
                RefreshToken = refreshToken,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    ProfilePicture = user.ProfilePicture,
                    Provider = user.Provider,
                    Role = user.Role,
                    CreatedAt = user.CreatedAt
                },
                ExpiresAt = DateTime.UtcNow.AddHours(2)
            };
        }

        private string GenerateJwtToken(User user)
        {
            var key = _configuration["Authentication:Jwt:Secret"] 
                ?? throw new InvalidOperationException("JWT Secret is not configured");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("provider", user.Provider),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Authentication:Jwt:Issuer"] ?? "QuantityMeasurementAPI",
                audience: _configuration["Authentication:Jwt:Audience"] ?? "QuantityMeasurementClients",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}