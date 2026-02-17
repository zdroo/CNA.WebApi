using CNA.Domain.Catalog.Entities;

namespace CNA.Application.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }

}
