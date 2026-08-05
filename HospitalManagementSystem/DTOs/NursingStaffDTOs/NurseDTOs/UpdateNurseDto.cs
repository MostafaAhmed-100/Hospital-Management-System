using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs
{
    public class UpdateNurseDto
    {
        public int Id { get; set; }
        public int StaffId { get; set; }
        public string LicenseNumber { get; set; }
        public ShiftType Shift { get; set; }
        public string WardSpecialization { get; set; }
    }
}
