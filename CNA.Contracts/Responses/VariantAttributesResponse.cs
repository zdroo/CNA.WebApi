namespace CNA.Contracts.Responses
{
    public class VariantFiltersResponse
    {
        public List<AttributeFilter> Attributes { get; set; } = new();
    }

    public class AttributeFilter
    {
        public string Name { get; set; }
        public List<string> Values { get; set; } = new();
    }
}
