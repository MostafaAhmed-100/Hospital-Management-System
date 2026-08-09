using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs
{
    public class AssignmentShiftDistributionDto
    {
        public ShiftType Shift { get; set; }
        public int Count { get; set; }
    }
}
