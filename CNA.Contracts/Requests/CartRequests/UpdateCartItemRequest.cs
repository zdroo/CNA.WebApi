namespace CNA.Contracts.Requests.Cart
{
    public record UpdateCartItemRequest(Guid CartItemId, int Quantity);
}
