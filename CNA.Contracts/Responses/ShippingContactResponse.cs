namespace CNA.Contracts.Responses;

public record ShippingContactResponse(
    Guid ShippingContactId,
    string FullName,
    string PhoneNumber,
    string AddressLine1,
    string AddressLine2,
    string City,
    string Region,
    string PostalCode,
    string CountryCode,
    bool IsDefault
);
