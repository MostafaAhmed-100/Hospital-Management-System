namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.NurseAssignmentDTOs
{
    public class NurseAssignmentResponseDto
    {
        public int Id { get; set; }
        public int NurseId { get; set; }
        public string NurseName { get; set; }
        public int? AdmissionId { get; set; }
        public int? ErVisitId { get; set; }
        public DateTime AssignedAt { get; set; }
        public string Shift { get; set; }
    }
}
