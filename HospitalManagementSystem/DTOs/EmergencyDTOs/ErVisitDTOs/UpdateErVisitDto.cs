using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs
{
    public class UpdateErVisitDto
    {
        public int Id { get; set; }
        public TriageLevel TriageLevel { get; set; }
        public string ChiefComplaint { get; set; }
        public ErVisitStatus Status { get; set; }
    }
}
