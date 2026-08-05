namespace HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs
{
    public class SurgeryTeamResponseDto
    {
        public int Id { get; set; }
        public int SurgeryId { get; set; }
        public int StaffId { get; set; }
        public string StaffName { get; set; }
        public string RoleInSurgery { get; set; }
    }
}
