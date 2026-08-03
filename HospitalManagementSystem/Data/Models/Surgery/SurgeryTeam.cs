using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Nursing_Staff;

namespace HospitalManagementSystem.Data.Models.Surgery
{
    public class SurgeryTeam
    {
        public int Id { get; set; }
        public int SurgeryId { get; set; }
        public int StaffId { get; set; }
        public StaffRole RoleInSurgery { get; set; }
        public bool IsDeleted { get; set; } = false;

        public SurgeryRecord SurgeryRecord { get; set; }
        public Staff Staff { get; set; }
    }
}