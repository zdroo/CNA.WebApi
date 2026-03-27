using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities.Localization
{
    public class Address : BaseEntity
    {
        public Guid UserId { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }

        public bool IsDefault { get; set; }

        public User User { get; set; }
    }
}
