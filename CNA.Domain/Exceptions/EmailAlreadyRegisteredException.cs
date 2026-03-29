namespace CNA.Domain.Exceptions
{
    public class EmailAlreadyRegisteredException : DomainException
    {
        public EmailAlreadyRegisteredException(string email)
            : base($"Email already registered '{email}'.") { }
    }
}
