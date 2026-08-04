namespace HospitalManagementSystem.DTOs.InvoiceDTOs
{
    public class InvoicePaymentDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
