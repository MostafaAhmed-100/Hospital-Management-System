using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Data.Models.Nursing_Staff
{
    public class Staff
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string FullName { get; set; }
        public StaffRole Role { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Clinic Clinic { get; set; }
        public Nurse NurseDetails { get; set; }
    }
}