using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Model.DTO;
using QuantityMeasurement.API.Services;
using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace QuantityMeasurement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class QuantityController : ControllerBase
    {
        private readonly IQuantityMeasurementService _service;
        private readonly IHistoryService _historyService;
        private readonly ILogger<QuantityController> _logger;

        public QuantityController(IQuantityMeasurementService service, IHistoryService historyService, ILogger<QuantityController> logger)
        {
            _service = service;
            _historyService = historyService;
            _logger = logger;
        }

        private Guid GetUserIdFromToken()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                throw new UnauthorizedAccessException("Invalid user ID in token");
            }
            return userId;
        }

        private async Task SaveHistoryAsync(Guid userId, string input, string output, string operationType)
        {
            try
            {
                await _historyService.SaveHistoryAsync(userId, input, output, operationType);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to save history for user: {UserId}", userId);
                // Don't throw - history saving failure shouldn't break the main operation
            }
        }

        [HttpPost("compare")]
        public async Task<IActionResult> Compare([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Compare(request.Q1, request.Q2);
            
            try
            {
                var userId = GetUserIdFromToken();
                var input = $"Compare {request.Q1.Value} {request.Q1.Unit} and {request.Q2.Value} {request.Q2.Unit}";
                var output = result.ToString();
                await SaveHistoryAsync(userId, input, output, "Compare");
            }
            catch (UnauthorizedAccessException)
            {
                // User not authenticated - skip history saving
            }

            return Ok(new { result });
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Add(request.Q1, request.Q2, request.TargetUnit);
            
            try
            {
                var userId = GetUserIdFromToken();
                var input = $"Add {request.Q1.Value} {request.Q1.Unit} and {request.Q2.Value} {request.Q2.Unit} to {request.TargetUnit}";
                var output = result.ToString();
                await SaveHistoryAsync(userId, input, output, "Add");
            }
            catch (UnauthorizedAccessException)
            {
                // User not authenticated - skip history saving
            }

            return Ok(new { result });
        }

        [HttpPost("subtract")]
        public async Task<IActionResult> Subtract([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Subtract(request.Q1, request.Q2, request.TargetUnit);
            
            try
            {
                var userId = GetUserIdFromToken();
                var input = $"Subtract {request.Q2.Value} {request.Q2.Unit} from {request.Q1.Value} {request.Q1.Unit} to {request.TargetUnit}";
                var output = result.ToString();
                await SaveHistoryAsync(userId, input, output, "Subtract");
            }
            catch (UnauthorizedAccessException)
            {
                // User not authenticated - skip history saving
            }

            return Ok(new { result });
        }

        [HttpPost("divide")]
        public async Task<IActionResult> Divide([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Divide(request.Q1, request.Q2);
            
            try
            {
                var userId = GetUserIdFromToken();
                var input = $"Divide {request.Q1.Value} {request.Q1.Unit} by {request.Q2.Value} {request.Q2.Unit}";
                var output = result.ToString();
                await SaveHistoryAsync(userId, input, output, "Divide");
            }
            catch (UnauthorizedAccessException)
            {
                // User not authenticated - skip history saving
            }

            return Ok(new { result });
        }

        [HttpPost("convert")]
        public async Task<IActionResult> Convert([FromBody] QuantityRequest request)
        {
            if (request == null)
                return BadRequest("Request cannot be null");

            var result = _service.Convert(request.Q1, request.TargetUnit);
            
            try
            {
                var userId = GetUserIdFromToken();
                var input = $"Convert {request.Q1.Value} {request.Q1.Unit} to {request.TargetUnit}";
                var output = result.ToString();
                await SaveHistoryAsync(userId, input, output, "Convert");
            }
            catch (UnauthorizedAccessException)
            {
                // User not authenticated - skip history saving
            }

            return Ok(new { result });
        }
    }
}