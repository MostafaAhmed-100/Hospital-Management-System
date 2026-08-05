using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs
{
    public class CreateErVisitDto
    {
        public int PatientId { get; set; }
        public int AttendingDoctorId { get; set; }
        public TriageLevel TriageLevel { get; set; }
        public string ChiefComplaint { get; set; }
    }
}
