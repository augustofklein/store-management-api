namespace StoreManagement.Domain.Entities
{
    public class IncoicePaymentsEntity
    {
        public int CompanyId { get; set; }
        public int InvoiceId { get; set; }
        public int PaymentTypeId { get; set; }
        public int Id { get; set; }
        public decimal Amount { get; set; } = 0;
        public DateTimeOffset PaymentDate { get; set; }

        public virtual InvoiceEntity Invoice { get; set; } = null!;
        public virtual PaymentTypeEntity PaymentType { get; set; } = null!;
    }
}
