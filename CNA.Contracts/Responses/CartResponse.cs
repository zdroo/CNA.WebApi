namespace CNA.Contracts.Responses
{
    public record CartResponse
    {
        public Guid UserId { get; set; }
        public List<CartItemResponse> Items { get; set; } = new List<CartItemResponse>();
    }
}
