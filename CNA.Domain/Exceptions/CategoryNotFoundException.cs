namespace CNA.Domain.Exceptions
{
    public class CategoryNotFoundException : DomainException
    {
        public CategoryNotFoundException(Guid categoryId) : base($"Category not found '{categoryId}'") { }
    }
}
