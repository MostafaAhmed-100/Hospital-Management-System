using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Billing_Insurance
{
    public class Invoice
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int AppointmentId { get; set; }
        public int? SurgeryId { get; set; }
        public int? AdmissionId { get; set; }
        public decimal Amount { get; set; }
        public InvoiceStatus Status { get; set; }
        public Appointment Appointment { get; set; }
        public Patient Patient { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
