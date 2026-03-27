namespace CNA.Contracts.Requests.Categories
{
    public record CreateCategoryRequest
    {
        public string Name { get; private set; } = default!;
        public string Slug { get; private set; } = default!;
        public Guid? ParentCategoryId { get; private set; }
    }
}
