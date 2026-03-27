using CNA.Domain.Common;

namespace CNA.Domain.Catalog.Entities.Localization
{
    public class Country : BaseEntity
    {
        public string Name { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }   // ISO 4217 (e.g. "RON", "USD")
        public string PhonePrefix { get; set; }
        public bool IsShippingAvailable { get; set; }
    }
}
