using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.DTOs.AppointmentDTOs
{
    public class AppointmentResponseDto
    {
        public int Id { get; set; }
        public int ClinicId { get; set; }
        public string ClinicName { get; set; }
        public int DoctorId { get; set; }

        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
