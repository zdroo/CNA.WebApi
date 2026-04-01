namespace CNA.Application.Models
{
    public record PriceRange
    {
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }
    }
}
