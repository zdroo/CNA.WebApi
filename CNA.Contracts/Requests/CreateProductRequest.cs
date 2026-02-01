namespace CNA.Contracts.Requests
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Brand { get; set; } = default!;
        public Guid CategoryId { get; set; }
    }
}
