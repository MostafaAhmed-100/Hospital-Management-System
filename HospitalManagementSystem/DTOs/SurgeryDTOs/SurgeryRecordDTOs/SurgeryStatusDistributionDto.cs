using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryRecordDTOs
{
    public class SurgeryStatusDistributionDto
    {
        public SurgeryStatus Status { get; set; }
        public int Count { get; set; }
    }
}
