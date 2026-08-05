using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs
{
    public class UpdateNurseAssignmentDto
    {
        public int Id { get; set; }
        public int NurseId { get; set; }
        public int? AdmissionId { get; set; }
        public int? ErVisitId { get; set; }
        public ShiftType Shift { get; set; }
    }
}
