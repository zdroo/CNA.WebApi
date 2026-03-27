using CNA.Domain.Catalog.Entities.Localization;
using CNA.Domain.Catalog.Enums;
using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; private set; } = default!;
        public string PasswordHash { get; private set; } = default!;
        public UserRole Role { get; private set; } = UserRole.User;
        public Cart? Cart { get; private set; }
        public bool IsActive { get; private set; }

        private readonly List<RefreshToken> _refreshTokens = new();
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens;

        private readonly List<Address> _addresses = new();
        public IReadOnlyCollection<Address> Addresses => _addresses;

        private readonly List<ShippingContact> _shippingContacts = new();
        public IReadOnlyCollection<ShippingContact> ShippingContacts => _shippingContacts;

        protected User() { }

        public User(string email, string passwordHash)
        {
            Email = email;
            PasswordHash = passwordHash;
            Role = UserRole.User;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddRefreshToken(RefreshToken token)
        {
            _refreshTokens.Add(token);
        }

        public void AddAddress(Address address)
        {
            _addresses.Add(address);
        }

        public void RemoveAddress(Address address)
        {
            _addresses.Remove(address);
        }

        public void AddShippingContact(ShippingContact contact)
        {
            _shippingContacts.Add(contact);
        }

        public void RemoveShippingContact(ShippingContact contact)
        {
            _shippingContacts.Remove(contact);
        }

        public void SetRole(UserRole role)
        {
            Role = role;
        }

        public Cart GetOrCreateCart()
        {
            Cart ??= new Cart(Id);
            return Cart;
        }
    }
}
