using CNA.Domain.Catalog.Enums;
using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserRole Role { get; private set; } = UserRole.User;
        public Cart? Cart { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<RefreshToken> _refreshTokens = new();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;

        public void AddRefreshToken(RefreshToken token)
        {
            _refreshTokens.Add(token);
        }

        protected User() { }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = UserRole.User;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetRole(UserRole role)
        {
            Role = role;
        }

        public void AssignCart(Cart cart)
        {
            Cart = cart;
        }
    }
}
