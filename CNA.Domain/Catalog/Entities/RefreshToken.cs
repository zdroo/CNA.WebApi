using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; private set; } = default!;
        public DateTime ExpiresAt { get; private set; }
        public bool IsRevoked { get; private set; } = false;
        public Guid UserId { get; private set; }
        public User User { get; private set; } = default!;

        protected RefreshToken() { }

        public RefreshToken(string token, DateTime expiresAt, Guid userId)
        {
            Token = token;
            ExpiresAt = expiresAt;
            UserId = userId;
        }

        public bool IsActive => !IsRevoked && DateTime.UtcNow < ExpiresAt;

        public void Revoke() => IsRevoked = true;
    }

}
