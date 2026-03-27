namespace CNA.Domain.Catalog.ValueObjects
{
    public class ShippingAddressSnapshot
    {
        // Parameterless constructor for EF Core
        private ShippingAddressSnapshot() { }

        public ShippingAddressSnapshot(
            string fullName, string phoneNumber,
            string addressLine1, string addressLine2,
            string city, string region,
            string postalCode, string countryCode)
        {
            FullName = fullName;
            PhoneNumber = phoneNumber;
            AddressLine1 = addressLine1;
            AddressLine2 = addressLine2;
            City = city;
            Region = region;
            PostalCode = postalCode;
            CountryCode = countryCode;
        }

        public string FullName { get; }
        public string PhoneNumber { get; }
        public string AddressLine1 { get; }
        public string AddressLine2 { get; }
        public string City { get; }
        public string Region { get; }
        public string PostalCode { get; }
        public string CountryCode { get; }
    }
}
