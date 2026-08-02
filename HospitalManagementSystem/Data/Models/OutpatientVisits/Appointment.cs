using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Data.Models.OutpatientVisits
{
    public class Appointment
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public Doctor Doctor { get; set; }
        public Patient Patient { get; set; }
        public Clinic Clinic { get; set; }
        public Invoice Invoice { get; set; }
        public MedicalRecord MedicalRecord { get; set; }
    }
}
