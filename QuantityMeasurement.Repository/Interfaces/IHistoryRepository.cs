using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Repository.Interfaces
{
    public interface IHistoryRepository
    {
        Task<History> AddAsync(History history);
        Task<History?> GetByIdAsync(Guid id);
        Task<List<History>> GetByUserIdAsync(Guid userId, int page, int pageSize);
        Task<int> GetTotalCountByUserIdAsync(Guid userId);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteAllByUserIdAsync(Guid userId);
        Task<bool> SoftDeleteAsync(Guid id);
        Task<bool> SoftDeleteAllByUserIdAsync(Guid userId);
    }
}
