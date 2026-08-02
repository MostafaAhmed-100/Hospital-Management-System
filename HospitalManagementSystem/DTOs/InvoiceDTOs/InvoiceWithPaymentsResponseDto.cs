namespace HospitalManagementSystem.DTOs.InvoiceDTOs
{
    public class InvoiceWithPaymentsResponseDto : InvoiceResponseDto
    {
        public IEnumerable<InvoicePaymentDto> Payments { get; set; }
    }
}
