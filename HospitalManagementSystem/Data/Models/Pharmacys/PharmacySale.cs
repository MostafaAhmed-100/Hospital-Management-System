using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class PharmacySale
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public int PatientId { get; set; }
        public int? PrescriptionId { get; set; }
        public Decimal TotalAmount { get; set; }
        public Pharmacy Pharmacy { get; set; }
        public Patient Patient { get; set; }
        public Prescription Prescription { get; set; }
        public ICollection<SaleItem> SaleItems { get; set; }
    }
}
