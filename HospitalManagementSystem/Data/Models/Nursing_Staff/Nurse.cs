using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Data.Models.Nursing_Staff
{
    public class Nurse
    {
        public int Id { get; set; }
        public int StaffId { get; set; } 
        public string LicenseNumber { get; set; }
        public ShiftType Shift { get; set; }
        public string WardSpecialization { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Staff Staff { get; set; }
        public ICollection<NurseAssignment> Assignments { get; set; }
    }
}