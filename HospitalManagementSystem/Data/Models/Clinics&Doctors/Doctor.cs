using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Models.Clinics_Doctors
{
    public class Doctor
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public int SpecialtyId { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DoctorType DoctorType { get; set; }
        public Decimal ConsultationFee { get; set; }
        public Department Department { get; set; }
        public Specialty Specialty { get; set; }
        public ICollection<Appointment> Appointments{ get; set; }
        public ICollection<MedicalRecord> MedicalRecords{ get; set; }
        public ICollection<Prescription> Prescriptions{ get; set; }
    }
}
