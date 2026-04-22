namespace CNA.Domain.Exceptions
{
    public class FavoriteItemNotFoundException : DomainException
    {
        public FavoriteItemNotFoundException(Guid favoriteItemId)
            : base($"Favorite item not found '{favoriteItemId}'") { }
    }
}
