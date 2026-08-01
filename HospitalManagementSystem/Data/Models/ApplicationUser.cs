using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Data.Models.OutpatientVisits;
using Microsoft.AspNetCore.Identity;

namespace HospitalManagementSystem.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public int? LinkedStaffId { get; set; }
        public int? LinkedPatientId { get; set; }
        public int? LinkedDoctorId { get; set; }
        //public int? LinkedTherapistId { get; set; }
        public Doctor? Doctor { get; set; }
        public Patient? Patient { get; set; }
        //public Staff? Staff { get; set; }
        //public Therapist? Therapist { get; set; }

    }
}
