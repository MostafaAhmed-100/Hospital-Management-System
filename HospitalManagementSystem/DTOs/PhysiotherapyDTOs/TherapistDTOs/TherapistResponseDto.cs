namespace HospitalManagementSystem.DTOs.PhysiotherapyDTOs.TherapistDTOs
{
    public class TherapistResponseDto
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
    }
}
