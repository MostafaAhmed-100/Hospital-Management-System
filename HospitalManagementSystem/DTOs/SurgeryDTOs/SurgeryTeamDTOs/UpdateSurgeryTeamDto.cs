using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.SurgeryDTOs.SurgeryTeamDTOs
{
    public class UpdateSurgeryTeamDto
    {
        public int Id { get; set; }
        public int SurgeryId { get; set; }
        public int StaffId { get; set; }
        public StaffRole RoleInSurgery { get; set; }
    }
}
