using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.InvoiceDTOs
{
    public class UpdateInvoiceDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int? SurgeryId { get; set; }
        public int? AdmissionId { get; set; }
        public decimal Amount { get; set; }
        public InvoiceStatus Status { get; set; }
    }
}
