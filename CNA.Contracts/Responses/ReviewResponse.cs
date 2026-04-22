namespace CNA.Contracts.Responses
{
    public record ReviewResponse
    {
        public Guid ReviewId { get; init; }
        public Guid UserId { get; init; }
        public string UserName { get; init; } = default;
        public int Rating { get; init; }
        public string? Comment { get; init; }
        public DateTime CreatedAt { get; init; } = default!;
    }
}