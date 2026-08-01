using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Pharmacys
{
    public class Prescription
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public PrescriptionStatus Status { get; set; }
        public MedicalRecord Record { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public PharmacySale? PharmacySale { get; set; }
        public ICollection<PrescriptionItem> PrescriptionItems { get; set; }

    }
}
