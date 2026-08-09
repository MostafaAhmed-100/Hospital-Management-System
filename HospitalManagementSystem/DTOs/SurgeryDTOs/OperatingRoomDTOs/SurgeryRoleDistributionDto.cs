using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.SurgeryDTOs.OperatingRoomDTOs
{
    public class SurgeryRoleDistributionDto
    {
        public StaffRole Role { get; set; }
        public int Count { get; set; }
    }
}
