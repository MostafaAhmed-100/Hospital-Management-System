using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs
{
    public class CreateNurseDto
    {
        public int StaffId { get; set; }
        public string LicenseNumber { get; set; }
        public ShiftType Shift { get; set; }
        public string WardSpecialization { get; set; }
    }
}
