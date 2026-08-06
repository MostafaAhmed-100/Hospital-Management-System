using HospitalManagementSystem.Data.Models.Clinics_Doctors;

namespace HospitalManagementSystem.Data.Models.Physiotherapy
{
    public class Therapist
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Department Department { get; set; }
        public ICollection<PhysioSession> PhysioSessions { get; set; }
    }
}
