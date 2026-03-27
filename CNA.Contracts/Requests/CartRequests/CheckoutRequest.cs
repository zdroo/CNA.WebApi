namespace CNA.Contracts.Requests.Cart
{
    public record CheckoutRequest(List<Guid> CartItemIds, Guid ShippingContactId);
}
