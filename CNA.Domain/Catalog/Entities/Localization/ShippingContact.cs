using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities.Localization
{
    public class ShippingContact : BaseEntity
    {
        public Guid UserId { get; set; }
        public Guid AddressId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
    }
}
