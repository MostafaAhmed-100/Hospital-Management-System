namespace HospitalManagementSystem.Data.Models.Billing_Insurance
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public Decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Method { get; set; }
        public Invoice Invoice { get; set; }

    }
}
