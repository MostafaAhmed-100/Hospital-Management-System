using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.Clinics_Doctors
{
    public class Clinic
    {
        public int Id { get; set; }
        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public ClinicType ClinicType { get; set; }
        public Department Department { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
