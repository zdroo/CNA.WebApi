namespace CNA.Domain.Catalog.Enums
{
    public enum PaymentStatus
    {
        Pending = 0,          // creat dar neprocesat încă
        RequiresAction = 1,   // 3D Secure / confirmare suplimentară
        Processing = 2,       // provider procesează plata
        Succeeded = 3,        // plata reușită
        Failed = 4,           // plata eșuată
        Cancelled = 5,        // anulată de user
        Refunded = 6,         // rambursare completă
        PartiallyRefunded = 7 // rambursare parțială
    }
}
