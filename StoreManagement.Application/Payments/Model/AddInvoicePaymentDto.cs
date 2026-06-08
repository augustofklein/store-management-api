namespace StoreManagement.Application.Payments.Model
{
    public class AddInvoicePaymentDto
    {
        public int PaymentTypeId { get; set; }
        public decimal Amount { get; set; } = 0;
    }
}
