namespace CNA.Contracts.Responses
{
    public record CartResponse(Guid UserId, decimal Total, List<CartItemResponse> Items);
}
