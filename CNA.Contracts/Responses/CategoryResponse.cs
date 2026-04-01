namespace CNA.Contracts.Responses
{
    public record CategoryResponse(Guid CategoryId, string Name, string Slug, bool IsActive, string? ImageUrl)
    {
        public CategoryResponse()
            : this(Guid.Empty, string.Empty, string.Empty, false, null)
        {
        }   
    }
}
            