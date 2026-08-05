namespace HospitalManagementSystem.DTOs.NursingStaffDTOs.StaffDTOs
{
    public class StaffResponseDto
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
