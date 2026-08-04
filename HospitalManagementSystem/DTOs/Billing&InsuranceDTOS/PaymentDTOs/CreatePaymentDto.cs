namespace HospitalManagementSystem.DTOs.PaymentDTOs
{
    public class CreatePaymentDto
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
