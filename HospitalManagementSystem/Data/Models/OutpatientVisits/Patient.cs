using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Models.OutpatientVisits
{
    public class Patient
    {
        public int Id { get; set; }
        public int? InsuranceId { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; } = false;

        public InsuranceProvider? InsuranceProvider { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<MedicalRecord> MedicalRecords{ get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
        public ICollection<PharmacySale> PharmacySales { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
