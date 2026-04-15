namespace QuantityMeasurement.Model.DTO
{
    public class HistoryResponseDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string InputValue { get; set; } = string.Empty;
        public string OutputValue { get; set; } = string.Empty;
        public string OperationType { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }

    public class PaginatedResponseDto<T>
    {
        public List<T> Data { get; set; } = new();
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
