using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs
{
    public class UpdateStaffDto
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string FullName { get; set; }
        public StaffRole Role { get; set; }
    }
}
