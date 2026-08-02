namespace HospitalManagementSystem.DTOs.InvoiceDTOs
{
    public class CreateInvoiceDto
    {
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int? SurgeryId { get; set; }
        public int? AdmissionId { get; set; }
        public decimal Amount { get; set; }
    }
}
