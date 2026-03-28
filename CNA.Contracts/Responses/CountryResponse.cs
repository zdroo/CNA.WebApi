namespace CNA.Contracts.Responses
{
    public record CountryResponse(
        string Name,
        string CountryCode,
        string CurrencyCode,
        string PhonePrefix,
        bool IsShippingAvailable
    );
}
