using CNA.Domain.Catalog.Enums;

namespace CNA.Domain.Catalog.Entities
{
    public class Payment
    {
        public Guid OrderId { get; private set; }
        public PaymentStatus Status { get; private set; }
        public string Provider { get; private set; }
        public string TransactionId { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public void MarkAsSucceeded(string transactionId)
        {
            Status = PaymentStatus.Succeeded;
            TransactionId = transactionId;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
