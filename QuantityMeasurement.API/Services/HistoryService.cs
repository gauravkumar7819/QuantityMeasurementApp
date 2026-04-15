using Microsoft.Extensions.Logging;
using QuantityMeasurement.Model.DTO;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Interfaces;

namespace QuantityMeasurement.API.Services
{
    public interface IHistoryService
    {
        Task<HistoryResponseDto> SaveHistoryAsync(Guid userId, string input, string output, string type);
        Task<PaginatedResponseDto<HistoryResponseDto>> GetUserHistoryAsync(Guid userId, int page, int pageSize);
        Task<bool> DeleteHistoryAsync(Guid id, Guid userId);
        Task<bool> DeleteAllHistoryAsync(Guid userId);
        Task<bool> SoftDeleteHistoryAsync(Guid id, Guid userId);
        Task<bool> SoftDeleteAllHistoryAsync(Guid userId);
    }

    public class HistoryService : IHistoryService
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly ILogger<HistoryService> _logger;

        public HistoryService(IHistoryRepository historyRepository, ILogger<HistoryService> logger)
        {
            _historyRepository = historyRepository;
            _logger = logger;
        }

        public async Task<HistoryResponseDto> SaveHistoryAsync(Guid userId, string input, string output, string type)
        {
            try
            {
                var history = new History
                {
                    UserId = userId,
                    InputValue = input,
                    OutputValue = output,
                    OperationType = type,
                    CreatedAt = DateTime.UtcNow,
                    IsDeleted = false
                };

                var savedHistory = await _historyRepository.AddAsync(history);

                _logger.LogInformation("History saved successfully for user: {UserId}, operation: {Operation}", userId, type);

                return new HistoryResponseDto
                {
                    Id = savedHistory.Id,
                    UserId = savedHistory.UserId,
                    InputValue = savedHistory.InputValue,
                    OutputValue = savedHistory.OutputValue,
                    OperationType = savedHistory.OperationType,
                    CreatedAt = savedHistory.CreatedAt
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving history for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<PaginatedResponseDto<HistoryResponseDto>> GetUserHistoryAsync(Guid userId, int page, int pageSize)
        {
            try
            {
                _logger.LogInformation("Fetching history for user: {UserId}, page: {Page}, pageSize: {PageSize}", userId, page, pageSize);

                var histories = await _historyRepository.GetByUserIdAsync(userId, page, pageSize);
                var totalCount = await _historyRepository.GetTotalCountByUserIdAsync(userId);
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

                var historyDtos = histories.Select(h => new HistoryResponseDto
                {
                    Id = h.Id,
                    UserId = h.UserId,
                    InputValue = h.InputValue,
                    OutputValue = h.OutputValue,
                    OperationType = h.OperationType,
                    CreatedAt = h.CreatedAt
                }).ToList();

                return new PaginatedResponseDto<HistoryResponseDto>
                {
                    Data = historyDtos,
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching history for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> DeleteHistoryAsync(Guid id, Guid userId)
        {
            try
            {
                _logger.LogInformation("Deleting history record: {HistoryId} for user: {UserId}", id, userId);

                var history = await _historyRepository.GetByIdAsync(id);
                if (history == null || history.UserId != userId)
                {
                    _logger.LogWarning("History record not found or user unauthorized: {HistoryId}", id);
                    return false;
                }

                return await _historyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting history record: {HistoryId}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAllHistoryAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation("Deleting all history for user: {UserId}", userId);

                return await _historyRepository.DeleteAllByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting all history for user: {UserId}", userId);
                throw;
            }
        }

        public async Task<bool> SoftDeleteHistoryAsync(Guid id, Guid userId)
        {
            try
            {
                _logger.LogInformation("Soft deleting history record: {HistoryId} for user: {UserId}", id, userId);

                var history = await _historyRepository.GetByIdAsync(id);
                if (history == null || history.UserId != userId)
                {
                    _logger.LogWarning("History record not found or user unauthorized: {HistoryId}", id);
                    return false;
                }

                return await _historyRepository.SoftDeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting history record: {HistoryId}", id);
                throw;
            }
        }

        public async Task<bool> SoftDeleteAllHistoryAsync(Guid userId)
        {
            try
            {
                _logger.LogInformation("Soft deleting all history for user: {UserId}", userId);

                return await _historyRepository.SoftDeleteAllByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error soft deleting all history for user: {UserId}", userId);
                throw;
            }
        }
    }
}
