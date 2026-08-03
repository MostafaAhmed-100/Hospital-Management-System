using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Inpatient
{
    public class Admission
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int BedId { get; set; }
        public int RecordId { get; set; } 
        public DateTime AdmissionDate { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string Reason { get; set; }
        public AdmissionStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Bed Bed { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}
