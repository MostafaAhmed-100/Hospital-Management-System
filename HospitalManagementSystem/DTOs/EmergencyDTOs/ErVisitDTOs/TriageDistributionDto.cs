using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.EmergencyDTOs.ErVisitDTOs
{
    public class TriageDistributionDto
    {
        public TriageLevel TriageLevel { get; set; }
        public int Count { get; set; }
    }
}
