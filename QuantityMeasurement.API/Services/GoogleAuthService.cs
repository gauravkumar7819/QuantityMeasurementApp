using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;

namespace QuantityMeasurement.API.Services
{
    public interface IGoogleAuthService
    {
        Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token);
    }

    public class GoogleAuthService : IGoogleAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<GoogleAuthService> _logger;

        public GoogleAuthService(IConfiguration configuration, ILogger<GoogleAuthService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new ArgumentException("Google ID token cannot be null or empty");
                }

                var clientId = _configuration["Authentication:Google:ClientId"];
                
                if (string.IsNullOrWhiteSpace(clientId))
                {
                    throw new InvalidOperationException("Google Client ID is not configured");
                }

                _logger.LogInformation("Verifying Google token for client: {ClientId}", clientId);

                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new[] { clientId }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);

                _logger.LogInformation("Google token verified successfully for email: {Email}", payload.Email);

                return payload;
            }
            catch (InvalidJwtException ex)
            {
                _logger.LogError(ex, "Invalid Google JWT token");
                throw new UnauthorizedAccessException("Invalid Google token", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error verifying Google token");
                throw new InvalidOperationException("Failed to verify Google token", ex);
            }
        }
    }
}
