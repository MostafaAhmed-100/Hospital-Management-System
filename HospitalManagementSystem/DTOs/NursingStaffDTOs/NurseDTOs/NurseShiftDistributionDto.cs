using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseDTOs
{
    public class NurseShiftDistributionDto
    {
        public ShiftType Shift { get; set; }
        public int Count { get; set; }
    }
}
