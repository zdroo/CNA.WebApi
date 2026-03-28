namespace CNA.Domain.Models
{
    public record PriceRange
    {
        public decimal? MinPrice { get; init; }
        public decimal? MaxPrice { get; init; }

        //public PriceRange() { }
    }
}
