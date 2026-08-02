namespace HospitalManagementSystem.DTOs.PaymentDTOs
{
    public class UpdatePaymentDto
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
    }
}
