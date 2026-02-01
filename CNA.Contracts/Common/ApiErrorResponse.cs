namespace CNA.Contracts.Common
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; init; }
        public string Message { get; init; } = default!;
        public string? Details { get; init; }
    }
}
