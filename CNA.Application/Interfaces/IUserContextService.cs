namespace CNA.Application.Interfaces
{
    public interface IUserContextService
    {
        Guid GetUserId();
        string? GetUserEmail();
        bool IsAuthenticated();
    }
}
