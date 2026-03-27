namespace CNA.Domain.Exceptions
{
    public class VariantsNotFoundException : DomainException
    {
        public VariantsNotFoundException() : base("SomeVariantsWereNotFound") { }
    }
}
