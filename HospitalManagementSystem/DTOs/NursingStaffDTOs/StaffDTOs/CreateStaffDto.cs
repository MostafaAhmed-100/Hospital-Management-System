using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs
{
    public class CreateStaffDto
    {
        public int ClinicId { get; set; }
        public string FullName { get; set; }
        public StaffRole Role { get; set; }
    }
}
