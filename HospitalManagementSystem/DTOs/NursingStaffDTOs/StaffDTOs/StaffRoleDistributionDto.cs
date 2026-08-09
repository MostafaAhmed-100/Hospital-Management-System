using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs
{
    public class StaffRoleDistributionDto
    {
        public StaffRole Role { get; set; }
        public int Count { get; set; }
    }
}
