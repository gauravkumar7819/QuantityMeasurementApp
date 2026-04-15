using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Interfaces;

namespace QuantityMeasurement.Repository.Implementations
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HistoryRepository> _logger;

        public HistoryRepository(AppDbContext context, ILogger<HistoryRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<History> AddAsync(History history)
        {
            try
            {
                _context.Histories.Add(history);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("History record created successfully for user: {UserId}", history.UserId);
                return history;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating history record for user: {UserId}", history.UserId);
                throw;
            }
        }

        public async Task<History?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _context.Histories
                    .Include(h => h.User)
                    .FirstOrDefaultAsync(h => h.Id == id && !h.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting history record by ID: {HistoryId}", id);
                throw;
            }
        }

        public async Task<List<History>> GetByUserIdAsync(Guid userId, int page, int pageSize)
        {
            try
            {
                var skip = (page - 1) * pageSize;
                
                return await _context.Histories
                    .Where(h => h.UserId == userId && !h.IsDeleted)
                    .OrderByDescending(h => h.CreatedAt)
                    .Skip(skip)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting history records for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<int> GetTotalCountByUserIdAsync(Guid userId)
        {
            try
            {
                return await _context.Histories
                    .CountAsync(h => h.UserId == userId && !h.IsDeleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting history count for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var history = await _context.Histories.FindAsync(id);
                if (history == null)
                {
                    _logger.LogWarning("History record not found for deletion: {HistoryId}", id);
                    return false;
                }

                _context.Histories.Remove(history);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("History record deleted permanently: {HistoryId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting history record: {HistoryId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAllByUserIdAsync(Guid userId)
        {
            try
            {
                var histories = await _context.Histories
                    .Where(h => h.UserId == userId)
                    .ToListAsync();

                if (!histories.Any())
                {
                    _logger.LogWarning("No history records found for user: {UserId}", userId);
                    return false;
                }

                _context.Histories.RemoveRange(histories);
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("All history records deleted permanently for user: {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all history records for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAsync(Guid id)
        {
            try
            {
                var history = await _context.Histories.FindAsync(id);
                if (history == null || history.IsDeleted)
                {
                    _logger.LogWarning("History record not found or already soft deleted: {HistoryId}", id);
                    return false;
                }

                history.IsDeleted = true;
                history.DeletedAt = DateTime.UtcNow;
                
                await _context.SaveChangesAsync();
                
                _logger.LogInformation("History record soft deleted: {HistoryId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting history record: {HistoryId}", id);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAllByUserIdAsync(Guid userId)
        {
            try
            {
                var histories = await _context.Histories
                    .Where(h => h.UserId == userId && !h.IsDeleted)
                    .ToListAsync();

                if (!histories.Any())
                {
                    _logger.LogWarning("No active history records found for user: {UserId}", userId);
                    return false;
                }

                foreach (var history in histories)
                {
                    history.IsDeleted = true;
                    history.DeletedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync();
                
                _logger.LogInformation("All history records soft deleted for user: {UserId}", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting all history records for user: {UserId}", userId);
                throw;
            }
        }
    }
}
