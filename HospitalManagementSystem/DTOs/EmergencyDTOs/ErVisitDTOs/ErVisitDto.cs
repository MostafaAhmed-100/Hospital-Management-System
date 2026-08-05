using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs
{
    public class ErVisitDto
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int AttendingDoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime ArrivalTime { get; set; }
        public TriageLevel TriageLevel { get; set; }
        public string ChiefComplaint { get; set; }
        public ErVisitStatus Status { get; set; }
    }
}
