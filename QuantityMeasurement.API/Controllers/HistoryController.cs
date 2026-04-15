using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuantityMeasurement.API.Services;
using QuantityMeasurement.Model.DTO;
using System.Security.Claims;

namespace QuantityMeasurement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryService _historyService;
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(IHistoryService historyService, ILogger<HistoryController> logger)
        {
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

        [HttpGet]
        public async Task<IActionResult> GetUserHistory([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 1 || pageSize > 100) pageSize = 10;

                var userId = GetUserIdFromToken();
                _logger.LogInformation("Fetching history for user: {UserId}, page: {Page}, pageSize: {PageSize}", userId, page, pageSize);

                var result = await _historyService.GetUserHistoryAsync(userId, page, pageSize);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching user history");
                return StatusCode(500, new { error = "Failed to fetch history", details = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistory(Guid id)
        {
            try
            {
                var userId = GetUserIdFromToken();
                _logger.LogInformation("Deleting history record: {HistoryId} for user: {UserId}", id, userId);

                var result = await _historyService.DeleteHistoryAsync(id, userId);
                
                if (!result)
                {
                    return NotFound(new { error = "History record not found or unauthorized" });
                }

                return Ok(new { message = "History record deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting history record: {HistoryId}", id);
                return StatusCode(500, new { error = "Failed to delete history", details = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllHistory()
        {
            try
            {
                var userId = GetUserIdFromToken();
                _logger.LogInformation("Deleting all history for user: {UserId}", userId);

                var result = await _historyService.DeleteAllHistoryAsync(userId);
                
                if (!result)
                {
                    return NotFound(new { error = "No history records found" });
                }

                return Ok(new { message = "All history records deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all history for user");
                return StatusCode(500, new { error = "Failed to delete all history", details = ex.Message });
            }
        }

        [HttpPatch("{id}/soft-delete")]
        public async Task<IActionResult> SoftDeleteHistory(Guid id)
        {
            try
            {
                var userId = GetUserIdFromToken();
                _logger.LogInformation("Soft deleting history record: {HistoryId} for user: {UserId}", id, userId);

                var result = await _historyService.SoftDeleteHistoryAsync(id, userId);
                
                if (!result)
                {
                    return NotFound(new { error = "History record not found or unauthorized" });
                }

                return Ok(new { message = "History record soft deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting history record: {HistoryId}", id);
                return StatusCode(500, new { error = "Failed to soft delete history", details = ex.Message });
            }
        }

        [HttpPatch("soft-delete-all")]
        public async Task<IActionResult> SoftDeleteAllHistory()
        {
            try
            {
                var userId = GetUserIdFromToken();
                _logger.LogInformation("Soft deleting all history for user: {UserId}", userId);

                var result = await _historyService.SoftDeleteAllHistoryAsync(userId);
                
                if (!result)
                {
                    return NotFound(new { error = "No history records found" });
                }

                return Ok(new { message = "All history records soft deleted successfully" });
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, "Unauthorized access attempt");
                return Unauthorized(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting all history for user");
                return StatusCode(500, new { error = "Failed to soft delete all history", details = ex.Message });
            }
        }
    }
}
