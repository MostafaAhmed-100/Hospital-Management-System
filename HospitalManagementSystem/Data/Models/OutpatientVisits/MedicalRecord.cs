using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Pharmacys;

namespace HospitalManagementSystem.Data.Models.OutpatientVisits
{
    public class MedicalRecord
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int AppointmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Diagnosis { get; set; }
        public Patient Patient { get; set; }
        public Appointment Appointment { get; set; }
        public Doctor Doctor { get; set; }
        public ICollection<Prescription> Prescriptions { get; set; }
    }
}
