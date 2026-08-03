using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Emergency
{
    public class ErVisit
    {
        public int Id { get; set; }
        public int PatientId { get; set; } 
        public int AttendingDoctorId { get; set; } 
        public DateTime ArrivalTime { get; set; }
        public TriageLevel TriageLevel { get; set; }
        public string ChiefComplaint { get; set; }
        public ErVisitStatus Status { get; set; }
        public bool IsDeleted { get; set; } = false;
        public Patient Patient { get; set; }
        public Doctor AttendingDoctor { get; set; }
    }
}